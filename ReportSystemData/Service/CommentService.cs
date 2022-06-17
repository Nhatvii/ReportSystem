using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReportSystemData.Constants;
using ReportSystemData.Global;
using ReportSystemData.Models;
using ReportSystemData.Parameters;
using ReportSystemData.Repositories;
using ReportSystemData.Response;
using ReportSystemData.Service.Base;
using ReportSystemData.ViewModel.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReportSystemData.Service
{
    public partial interface ICommentService : IBaseService<Comment>
    {
        List<Comment> GetAllComment(CommentParameters commentParameters);
        Comment GetCommentByID(string id);
        Task<Comment> CreateCommentAsync(CreateCommentViewModel comment);
        SuccessResponse UpdateComment(UpdateCommentViewModel comment);
        SuccessResponse DeleteComment(string id);
    }
    public partial class CommentService : BaseService<Comment>, ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IPostService _postService;
        public CommentService(DbContext context, IMapper mapper, ICommentRepository repository, IAccountService accountService, IPostService postService) : base(context, repository)
        {
            _dbContext = context;
            _mapper = mapper;
            _accountService = accountService;
            _postService = postService;
        }
        public List<Comment> GetAllComment(CommentParameters commentParameters)
        {
            var cmt = Get().Where(c => c.IsDelete == false).OrderByDescending(p => p.CreateTime).Include(c => c.User).ThenInclude(c => c.AccountInfo).ToList();
            if (commentParameters.PostId != null)
            {
                cmt = cmt.Where(c => c.PostId.Equals(commentParameters.PostId)).ToList();
            }
            return cmt;
        }
        public Comment GetCommentByID(string id)
        {
            if (!string.IsNullOrEmpty(id) || !(id.Equals("string")))
            {
                var cmt = Get().Where(r => r.CommentId.Equals(id)).Include(c => c.Post).Include(c => c.User).FirstOrDefault();
                return cmt;
            }
            throw new ErrorResponse("Invalid ID !!!", (int)HttpStatusCode.NotFound);
        }
        public async Task<Comment> CreateCommentAsync(CreateCommentViewModel comment)
        {
            var account = _accountService.GetAccountByID(comment.UserId);
            if (account == null)
            {
                throw new ErrorResponse("Unavailable Account!!!", (int)HttpStatusCode.NotFound);
            }
            var post = _postService.GetPostById(comment.PostId);
            if (post == null)
            {
                throw new ErrorResponse("Unavailable Post!!!", (int)HttpStatusCode.NotFound);
            }
            if (comment.CommentTitle.Length > 100)
            {
                throw new ErrorResponse("Comment is > 100 word", (int)HttpStatusCode.NotFound);
            }
            var cmtstring = CheckBadWord(comment.CommentTitle);
            var cmt = _mapper.Map<Comment>(comment);
            cmt.CommentId = Guid.NewGuid().ToString();
            cmt.CreateTime = DateTime.Now;
            cmt.Status = CommentConstants.STATUS_COMMENT_NEW;
            cmt.CommentTitle = cmtstring;
            await CreateAsyn(cmt);
            return cmt;
        }

        public SuccessResponse UpdateComment(UpdateCommentViewModel comment)
        {
            var cmt = Get().Where(c => c.CommentId.Equals(comment.CommentId)).FirstOrDefault();
            if (cmt != null)
            {
                cmt.CommentTitle = comment.CommentTitle;
                if (comment.Status == 1) { cmt.Status = CommentConstants.STATUS_COMMENT_NEW; }
                if (comment.Status == 2) { cmt.Status = CommentConstants.STATUS_COMMENT_PENDING; }
                if (comment.Status == 3) { cmt.Status = CommentConstants.STATUS_COMMENT_APPROVE; }
                if (comment.Status == 4) { cmt.Status = CommentConstants.STATUS_COMMENT_DENIED; }
                Update(cmt);
                return new SuccessResponse((int)HttpStatusCode.OK, "Update Success");
            }
            throw new ErrorResponse("Comment isn't available!", (int)HttpStatusCode.NotFound);
        }
        public SuccessResponse DeleteComment(string id)
        {
            var cmt = Get().Where(r => r.CommentId.Equals(id)).FirstOrDefault();
            if (cmt != null)
            {
                cmt.IsDelete = true;
                Update(cmt);
                return new SuccessResponse((int)HttpStatusCode.OK, "Delete Success");
            }
            throw new ErrorResponse("Comment isn't available!", (int)HttpStatusCode.NotFound);
        }
        public string CheckBadWord(string input)
        {
            Dictionary<string, string> words = new Dictionary<string, string>();
            ReadCSV readCsv = new ReadCSV();
            readCsv.Url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vQse0qvsvPJaksx8w6h9GOBxEr8eMxluSLZafiLvHdeSGf5vNrjI1gZMjlvWDE0de0VFa8BsxZmov-l/pub?output=csv";
            string content = readCsv.CsvContent().ToString();
            string[] arrContent = content.Split(';');
            for (int i = 0; i < arrContent.Length; i++)
            {
                words.Add(arrContent[i], arrContent[i]);
            }

            Dictionary<int, int> startendIndex = new Dictionary<int, int>();
            var test = input.ToLower();
            for (int start = 0; start < test.Length; start++)
            {
                for (int offset = 1; offset < (test.Length + 1 - start); offset++)
                {
                    string wordToCheck = test.Substring(start, offset);
                    if (words.ContainsValue(wordToCheck))
                    {
                        foreach (var item in startendIndex)
                        {
                            if ((item.Key <= start) && (offset > item.Value))
                            {
                                startendIndex.Remove(item.Key);
                            }
                        }
                        startendIndex.Add(start, offset + start);
                    }
                }
            }

            string replaced = input;
            foreach (var item in startendIndex)
            {
                string stars = "";
                for (int i = item.Key; i < item.Value; i++)
                {
                    stars += "*";
                }
                replaced = replaced.Substring(0, item.Key) + stars + replaced.Substring(item.Value);
            }
            return replaced;
        }
    }
}

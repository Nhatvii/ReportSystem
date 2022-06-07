using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReportSystemData.Constants;
using ReportSystemData.Models;
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
        List<Comment> GetAllComment();
        Comment GetCommentByID(string id);
        Task<SuccessResponse> CreateCommentAsync(CreateCommentViewModel comment);
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
        public List<Comment> GetAllComment()
        {
            var cmt = Get().Where(c => c.IsDelete == false).Include(c => c.Post).Include(c => c.User).ToList();
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
        public async Task<SuccessResponse> CreateCommentAsync(CreateCommentViewModel comment)
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
            var cmt = _mapper.Map<Comment>(comment);
            cmt.CommentId = Guid.NewGuid().ToString();
            cmt.CreateTime = DateTime.Now;
            cmt.Status = CommentConstants.STATUS_COMMENT_NEW;
            await CreateAsyn(cmt);
            return new SuccessResponse((int)HttpStatusCode.OK, "Create Success");
        }

        public SuccessResponse UpdateComment(UpdateCommentViewModel comment)
        {
            var cmt = Get().Where(c => c.CommentId.Equals(comment.CommentId)).FirstOrDefault();
            if(cmt !=null)
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
    }
}

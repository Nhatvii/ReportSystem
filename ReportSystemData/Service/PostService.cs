using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReportSystemData.Constants;
using ReportSystemData.Models;
using ReportSystemData.Parameters;
using ReportSystemData.Repositories;
using ReportSystemData.Response;
using ReportSystemData.Service.Base;
using ReportSystemData.ViewModel.Emotion;
using ReportSystemData.ViewModel.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace ReportSystemData.Service
{
    public partial interface IPostService : IBaseService<Post>
    {
        List<Post> GetAllPost(PostParameters postParameters);
        Post GetPostById(string id);
        Task<SuccessResponse> CreatePostAsync(CreatePostViewModel post);
        SuccessResponse UpdatePost(UpdatePostViewModel post);
        SuccessResponse UpdatePublicPost(UpdatePublicPostViewModel post);
        SuccessResponse DeletePost(string id);
        Task<SuccessResponse> UpdateViewCount(UpdateViewCountViewModel model);
    }
    public partial class PostService : BaseService<Post>, IPostService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;
        private readonly IEmotionService _emotionService;
        public PostService(DbContext context, IMapper mapper, IPostRepository repository, ICategoryService categoryService, IAccountService accountService, IEmotionService emotionService) : base(context, repository)
        {
            _dbContext = context;
            _mapper = mapper;
            _categoryService = categoryService;
            _accountService = accountService;
            _emotionService = emotionService;
        }

        public List<Post> GetAllPost(PostParameters postParameters)
        {
            var post = Get().Where(p => p.IsDelete == false).Include(p => p.Category)
                .Include(p => p.Emotion)
                .Include(p => p.Editor).ThenInclude(p => p.AccountInfo).ToList();
            if (postParameters.Status.HasValue && postParameters.Status > 0)
            {
                post = GetPostWithStatus(postParameters.Status);
            }
            if(postParameters.CategoryID.HasValue && postParameters.CategoryID > 0)
            {
                post = post.Where(cate => cate.CategoryId == postParameters.CategoryID).ToList();
            }
            if (postParameters.isViewCount != null)
            {
                if ((bool)postParameters.isViewCount)
                {
                    post = post.OrderByDescending(p => p.ViewCount).ToList();
                }
                else
                {
                    post = post.OrderBy(p => p.ViewCount).ToList();
                }
            }
            if (postParameters.isRecentDate != null)
            {
                if ((bool)postParameters.isRecentDate)
                {
                    post = post.OrderByDescending(p => p.PublicTime).ToList();
                }
                else
                {
                    post = post.OrderBy(p => p.PublicTime).ToList();
                }
            }
            if (postParameters.EditorID != null)
            {
                post = post.Where(p => p.EditorId.Equals(postParameters.EditorID)).ToList();
            }
            return post;
        }

        public Post GetPostById(string id)
        {
            var post = Get().Where(r => r.PostId == id).Include(p => p.Category).Include(p => p.Emotion)
                .Include(p => p.Editor).ThenInclude(p => p.AccountInfo).FirstOrDefault();
            return post;
        }

        public List<Post> GetPostWithStatus(int? status)
        {
            var post = new List<Post>();
            if (status == 1)
            {
                post = Get().Where(p => p.Status.Equals(PostConstrants.STATUS_POST_DRAFT)).ToList();
            }
            if (status == 2)
            {
                post = Get().Where(p => p.Status.Equals(PostConstrants.STATUS_POST_HIDDEN)).ToList();
            }
            if (status == 3)
            {
                post = Get().Where(p => p.Status.Equals(PostConstrants.STATUS_POST_PUBLIC)).ToList();
            }
            return post;
        }

        public async Task<SuccessResponse> CreatePostAsync(CreatePostViewModel post)
        {
            var account = _accountService.GetAccountByID(post.UserID);
            if(account != null)
            {
                if(!(account.RoleId == 1))
                {
                    var postTmp = _mapper.Map<Post>(post);
                    postTmp.PostId = Guid.NewGuid().ToString();
                    postTmp.CreateTime = DateTime.Now;
                    postTmp.ViewCount = 0;
                    postTmp.EditorId = post.UserID;
                    postTmp.Status = PostConstrants.STATUS_POST_DRAFT;
                    await CreateAsyn(postTmp);
                    return new SuccessResponse((int)HttpStatusCode.OK, "Create Success");
                }
                throw new ErrorResponse("User account can't create report!!!", (int)HttpStatusCode.NoContent);
            }
            throw new ErrorResponse("Unavailable Account!!!", (int)HttpStatusCode.NotFound);
        }
        public SuccessResponse UpdatePost(UpdatePostViewModel post)
        {
            var checkAccount = _accountService.CheckAvaiAccount(post.EditorId);
            if(checkAccount)
            {
                var checkCate = _categoryService.CheckAvailableCategory(post.CategoryId);
                if (checkCate)
                {
                    var postTmp = Get().Where(p => p.PostId.Equals(post.PostId)).FirstOrDefault();
                    if(postTmp != null)
                    {
                        if(post.Title != null)
                        {
                            postTmp.Title = post.Title;
                        }

                        postTmp.UpdateTime = DateTime.Now;
                        postTmp.CategoryId = post.CategoryId;
                        postTmp.EditorId = post.EditorId;
                        if (post.Description != null)
                        {
                            postTmp.Description = post.Description;
                        }
                        if (post.Video != null)
                        {
                            postTmp.Video = post.Video;
                        }
                        if (post.Image != null)
                        {
                            postTmp.Image = post.Image;
                        }
                        Update(postTmp);
                        return new SuccessResponse((int)HttpStatusCode.OK, "Update Success");
                    }
                    throw new ErrorResponse("Post isn't available!", (int)HttpStatusCode.NotFound);
                }
                throw new ErrorResponse("CategoryID isn't available!", (int)HttpStatusCode.NotFound);
            }
            throw new ErrorResponse("EditorID isn't available!", (int)HttpStatusCode.NotFound);
        }

        public SuccessResponse UpdatePublicPost(UpdatePublicPostViewModel post)
        {
            var postTmp = Get().Where(p => p.PostId.Equals(post.PostId)).FirstOrDefault();
            if (postTmp != null)
            {
                if(post.Status == 1)
                {
                    postTmp.Status = PostConstrants.STATUS_POST_DRAFT;
                }
                if (post.Status == 2)
                {
                    postTmp.Status = PostConstrants.STATUS_POST_HIDDEN;
                }
                if (post.Status == 3)
                {
                    postTmp.Status = PostConstrants.STATUS_POST_PUBLIC;
                }
                postTmp.PublicTime = DateTime.Now;
                Update(postTmp);
                return new SuccessResponse((int)HttpStatusCode.OK, "Update Success");
            }
            throw new ErrorResponse("Post isn't available!", (int)HttpStatusCode.NotFound);
        }

        public SuccessResponse DeletePost(string id)
        {
            var postTmp = Get().Where(r => r.PostId.Equals(id)).FirstOrDefault();
            if (postTmp != null)
            {
                postTmp.IsDelete = true;
                Update(postTmp);
                return new SuccessResponse((int)HttpStatusCode.OK, "Delete Success");
            }
            throw new ErrorResponse("Post isn't available!", (int)HttpStatusCode.NotFound);
        }

        public async Task<SuccessResponse> UpdateViewCount(UpdateViewCountViewModel model)
        {
            //var checkAvaiEmo = _emotionService.CheckEmotion(model.PostId, model.UserId);
            //if(!checkAvaiEmo)
            //{
                var statusemo = _mapper.Map<EditStatusEmotion>(model);
                await _emotionService.CreateEmotionView(statusemo);
            //}

            var emoPara = new EmotionParameters()
            {
                PostId = model.PostId,
                IsView = true
            };
            var listEmo = _emotionService.GetAllEmotion(emoPara);
            if(listEmo != null)
            {
                var post = GetPostById(model.PostId);
                post.ViewCount = listEmo.Count();
                Update(post);
                return new SuccessResponse((int)HttpStatusCode.OK, "Update Success");
            }
            return new SuccessResponse((int)HttpStatusCode.OK, "Clear");
        }
    }
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ReportSystemData.Constants;
using ReportSystemData.Dtos.Post;
using ReportSystemData.Models;
using ReportSystemData.Parameters;
using ReportSystemData.Repositories;
using ReportSystemData.Response;
using ReportSystemData.Service.Base;
using ReportSystemData.ViewModel.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReportSystemData.Service
{
    public partial interface IPostService : IBaseService<Post>
    {
        List<PostDTO> GetAllPost(PostParameters postParameters);
        PostDTO GetPostById(string id);
        Task<Post> CreatePostAsync(CreatePostViewModel post);
        Post UpdatePost(UpdatePostViewModel post);
        bool UpdatePublicPost(UpdatePublicPostViewModel post);
        bool DeletePost(string id);
    }
    public partial class PostService : BaseService<Post>, IPostService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;
        public PostService(DbContext context, IMapper mapper, IPostRepository repository, ICategoryService categoryService, IAccountService accountService) : base(context, repository)
        {
            _dbContext = context;
            _mapper = mapper;
            _categoryService = categoryService;
            _accountService = accountService;
        }

        public List<PostDTO> GetAllPost(PostParameters postParameters)
        {
            var post = Get().Where(p => p.IsDelete == false).Include(p => p.Category)
                .Include(p => p.Editor).ThenInclude(p => p.AccountInfo)
                .ProjectTo<PostDTO>(_mapper.ConfigurationProvider).ToList();
            if (postParameters.isPublic != null)
            {
                post = GetPostIsPublic(postParameters.isPublic);
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

            return post;
        }

        public PostDTO GetPostById(string id)
        {
            var post = Get().Where(r => r.PostId == id).Include(p => p.Category)
                .Include(p => p.Editor).ThenInclude(p => p.AccountInfo)
                .ProjectTo<PostDTO>(_mapper.ConfigurationProvider).FirstOrDefault();
            return post;
        }

        public List<PostDTO> GetPostIsPublic(bool? isPublic)
        {
            var post = new List<PostDTO>();
            if ((bool)isPublic)
            {
                post = Get().Where(p => p.Status.Equals(PostConstrants.STATUS_POST_PUBLIC)).ProjectTo<PostDTO>(_mapper.ConfigurationProvider).ToList();
            }
            else
            {
                post = Get().Where(p => p.Status.Equals(PostConstrants.STATUS_POST_HIDDEN)).ProjectTo<PostDTO>(_mapper.ConfigurationProvider).ToList();
            }
            return post;
        }

        public async Task<Post> CreatePostAsync(CreatePostViewModel post)
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
                    postTmp.Status = PostConstrants.STATUS_POST_HIDDEN;
                    await CreateAsyn(postTmp);
                    return postTmp;
                }
                throw new ErrorResponse("User account can't create report!!!", (int)HttpStatusCode.NoContent);
            }
            throw new ErrorResponse("Unavailable Account!!!", (int)HttpStatusCode.NotFound);
        }
        public Post UpdatePost(UpdatePostViewModel post)
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
                        return postTmp;
                    }
                    throw new ErrorResponse("Post isn't available!", (int)HttpStatusCode.NotFound);
                }
                throw new ErrorResponse("CategoryID isn't available!", (int)HttpStatusCode.NotFound);
            }
            throw new ErrorResponse("EditorID isn't available!", (int)HttpStatusCode.NotFound);
        }

        public bool UpdatePublicPost(UpdatePublicPostViewModel post)
        {
            var postTmp = Get().Where(p => p.PostId.Equals(post.PostId)).FirstOrDefault();
            if (postTmp != null)
            {
                if(post.Status == 0)
                {
                    postTmp.Status = PostConstrants.STATUS_POST_HIDDEN;
                }
                if (post.Status == 1)
                {
                    postTmp.Status = PostConstrants.STATUS_POST_PUBLIC;
                }
                postTmp.PublicTime = DateTime.Now;
                Update(postTmp);
                return true;
            }
            throw new ErrorResponse("Post isn't available!", (int)HttpStatusCode.NotFound);
        }

        public bool DeletePost(string id)
        {
            var postTmp = Get().Where(r => r.PostId.Equals(id)).FirstOrDefault();
            if (postTmp != null)
            {
                postTmp.IsDelete = true;
                return true;
                //throw new ErrorResponse("Delete success", (int)HttpStatusCode.OK);
            }
            throw new ErrorResponse("Post isn't available!", (int)HttpStatusCode.NotFound);
        }
    }
}

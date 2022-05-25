using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ReportSystemData.Constants;
using ReportSystemData.Dtos.Post;
using ReportSystemData.Models;
using ReportSystemData.Parameters;
using ReportSystemData.Repositories;
using ReportSystemData.Service.Base;
using ReportSystemData.ViewModel.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportSystemData.Service
{
    public partial interface IPostService : IBaseService<Post>
    {
        List<PostDTO> GetAllPost(PostParameters postParameters);
        List<PostDTO> GetPostById(string id);
        Task<Post> CreatePostAsync(CreatePostViewModel post);
    }
    public partial class PostService : BaseService<Post>, IPostService
    {
        private readonly IMapper _mapper;
        public PostService(DbContext context, IMapper mapper, IPostRepository repository) : base(context, repository)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        //public Post CreatePost(CreatePost createPost)
        //{
        //    if (createPost.Title.Equals("string") || createPost.Title == null)
        //    {
        //        return null;
        //    }
        //    if (createPost.Description.Equals("string") || createPost.Description == null)
        //    {
        //        return null;
        //    }
        //    if (createPost.MediaId == 0 || createPost.MediaId == null)
        //    {
        //        return null;
        //    }
        //    if (createPost.Email.Equals("string") || createPost.Email == null)
        //    {
        //        return null;
        //    }
        //    var checkEmail = _context.User.FirstOrDefault(user => user.Email.Equals(createPost.Email));
        //    if (checkEmail == null)
        //    {
        //        return null;
        //    }
        //    var post = new Post()
        //    {
        //        PostId = _context.Post.Count() + 1,
        //        Title = createPost.Title,
        //        Description = createPost.Description,
        //        MediaId = createPost.MediaId,
        //        Email = createPost.Email,
        //        NumOfLike = 0,
        //        NumOfShare = 0,
        //        NumOfComment = 0,
        //        Status = "New",
        //        EmailNavigation = null,
        //        Media = null,
        //        Emotion = null,
        //        PostDetail = null
        //    };
        //    _context.Post.Add(post);
        //    _context.SaveChanges();
        //    return post;
        //}

        //public bool DeletePost(int id)
        //{
        //    var post = _context.Post.FirstOrDefault(post => post.PostId == id);
        //    if (post != null)
        //    {
        //        if (post.Status.Equals("Deleted"))
        //        {
        //            return false;
        //        }
        //        post.Status = "Deleted";
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}

        public List<PostDTO> GetAllPost(PostParameters postParameters)
        {
            var post = Get().Include(p => p.Category)
                .Include(p => p.Editor).ThenInclude(p => p.AccountInfo)
                .ProjectTo<PostDTO>(_mapper.ConfigurationProvider).ToList();
            if (postParameters.postID != null)
            {
                post = GetPostById(postParameters.postID);
            }
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

        public List<PostDTO> GetPostById(string id)
        {
            var post = Get().Where(r => r.PostId == id).Include(p => p.Category)
                .Include(p => p.Editor).ThenInclude(p => p.AccountInfo)
                .ProjectTo<PostDTO>(_mapper.ConfigurationProvider).ToList();
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
            var postTmp = _mapper.Map<Post>(post);
            postTmp.PostId = Guid.NewGuid().ToString();
            postTmp.CreateTime = DateTime.Now;
            postTmp.ViewCount = 0;
            postTmp.EditorId = "nhatvii@gmail.com";
            postTmp.Status = PostConstrants.STATUS_POST_HIDDEN;
            await CreateAsyn(postTmp);
            return postTmp;
        }
        //public bool UpdatePost(int id, UpdatePost updatePost)
        //{
        //    var post = _context.Post.FirstOrDefault(post => post.PostId == id);
        //    if (post != null)
        //    {
        //        post.Title = string.IsNullOrEmpty(updatePost.Title) ? post.Title : updatePost.Title; ;
        //        post.Description = string.IsNullOrEmpty(updatePost.Description) ? post.Description : updatePost.Description; ;
        //        post.MediaId = updatePost.MediaId == 0 ? post.MediaId : updatePost.MediaId; ;
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}
    }
}

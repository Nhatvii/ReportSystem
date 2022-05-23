using AutoMapper;
using AutoMapper.QueryableExtensions;
using ReportSystemData.Dtos.Post;
using ReportSystemData.Models;
using ReportSystemData.Repositories;
using ReportSystemData.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportSystemData.Service
{
    public partial interface IPostService : IBaseService<Post>
    {
        List<PostDTO> GetAllPost();
        //Report CreatePost(CreatePostViewModel report);
    }
    public partial class PostService : BaseService<Post>, IPostService
    {
        private readonly _24HReportSystemContext _context;
        private readonly IMapper _mapper;
        public PostService(_24HReportSystemContext context, IMapper mapper, IPostRepository repository) : base(context, repository)
        {
            this._context = context;
            this._mapper = mapper;
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

        public List<PostDTO> GetAllPost()
        {
            var posts = _context.Post.ProjectTo<PostDTO>(_mapper.ConfigurationProvider).ToList();
            return posts;
        }

        //public Post GetPostById(int id)
        //{
        //    var post = _context.Post.FirstOrDefault(post => post.PostId == id);
        //    if (post != null)
        //    {
        //        return post;
        //    }
        //    return null;
        //}

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

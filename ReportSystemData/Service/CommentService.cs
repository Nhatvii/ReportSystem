using Microsoft.EntityFrameworkCore;
using ReportSystemData.Dtos.Comment;
using ReportSystemData.Models;
using ReportSystemData.Repository;
using ReportSystemData.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ReportSystemData.Service
{
    //public class CommentService : IComment
    //{
    //    private readonly ReportSystemContext _context;
    //    public CommentService(ReportSystemContext context)
    //    {
    //        _context = context;
    //    }

    //    public Comment CreateComment(CreateCommentDTO createComment)
    //    {
    //        var flag = false;


    //        foreach (Post post in _context.Post.ToList())
    //        {
    //            if (post.PostId == createComment.PostId)
    //            {
    //                flag = true;
    //            }
    //        }
    //        if (!flag)
    //        {

    //            throw new ErrorResponse("chó tuấn anh", (int)HttpStatusCode.BadRequest);
    //        }
    //        if (!flag)
    //        {
    //            //return null;

    //        }
    //        var commentID = _context.Comment.Count() + 1;
    //        var comment = new Comment()
    //        {
    //            CommentId = commentID,
    //            CommentTitle = createComment.Title,
    //            Email = createComment.Email
    //        };
    //        var postDetail = new PostDetail()
    //        {
    //            PostId = createComment.PostId,
    //            CommentId = commentID
    //        };
    //        _context.Comment.Add(comment);
    //        _context.PostDetail.Add(postDetail);
    //        _context.SaveChanges();
    //        return comment;
    //    }

    //    public bool DeleteComment(DeleteCommentDTO cmt)
    //    {
    //        var check = _context.PostDetail.FirstOrDefault(postDetail => postDetail.PostId == cmt.PostId);
    //        if (check != null)
    //        {
    //            _context.PostDetail.Remove(check);
    //            var comment = _context.Comment.FirstOrDefault(comment => comment.CommentId == cmt.CommentId);
    //            if (comment != null)
    //            {
    //                _context.Comment.Remove(comment);
    //                _context.SaveChanges();
    //                return true;
    //            }
    //        }
    //        return false;
    //    }

    //    public IEnumerable<Comment> GetAllComment()
    //    {
    //        return _context.Comment.Include(comment => comment.PostDetail).ThenInclude(postDetail => postDetail.Post).ToList();
    //    }

    //    public Comment GetCommentById(int id)
    //    {
    //        var comment = _context.Comment.Include(comment => comment.PostDetail).ThenInclude(postDetail => postDetail.Post).FirstOrDefault(comment => comment.CommentId == id);
    //        return comment;
    //    }

    //    public bool UpdateComment(int id, UpdateCommentDTO updateComment)
    //    {
    //        var comment = _context.Comment.FirstOrDefault(comment => comment.CommentId == id);
    //        if (comment != null)
    //        {
    //            comment.CommentTitle = updateComment.CommentTitle;
    //            _context.SaveChanges();
    //            return true;
    //        }
    //        return false;
    //    }
    //}
}

using ReportSystemData.Dtos.Comment;
using ReportSystemData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportSystemData.Repository
{
    interface IComment
    {
        IEnumerable<Comment> GetAllComment();
        Comment GetCommentById(int id);
        Comment CreateComment(CreateCommentDTO createComment);
        bool UpdateComment(int id, UpdateCommentDTO updateComment);
        bool DeleteComment(DeleteCommentDTO cmt);
    }
}

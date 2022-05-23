using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReportSystemData.Dtos.Comment
{
    public class DeleteCommentDTO
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public int CommentId { get; set; }
    }
}

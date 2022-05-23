using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReportSystemData.Dtos.Comment
{
    public class UpdateCommentDTO
    {
        [Required]
        public string CommentTitle { get; set; }
    }
}

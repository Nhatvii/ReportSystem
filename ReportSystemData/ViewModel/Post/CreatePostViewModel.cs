using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReportSystemData.ViewModel.Post
{
    public class CreatePostViewModel
    {
        [StringLength(200)]
        [Required]
        public string Title { get; set; }
        [StringLength(50)]
        [Required]
        public string CategoryId { get; set; }
        [Required]
        public string Description { get; set; }
        [StringLength(200)]
        [Required]
        public string Video { get; set; }
        [StringLength(200)]
        [Required]
        public string Image { get; set; }
    }
}

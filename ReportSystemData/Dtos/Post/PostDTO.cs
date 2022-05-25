using ReportSystemData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportSystemData.Dtos.Post
{
    public class PostDTO
    {
        public string PostId { get; set; }
        public string Title { get; set; }
        public string CategoryId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? PublicTime { get; set; }
        public string Description { get; set; }
        public string Video { get; set; }
        public string Image { get; set; }
        public int ViewCount { get; set; }
        public string EditorId { get; set; }
        public string Status { get; set; }

        public virtual Account Editor { get; set; }
        public virtual Category Category { get; set; }
    }
}

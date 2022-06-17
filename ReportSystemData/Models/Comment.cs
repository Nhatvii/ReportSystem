using Newtonsoft.Json;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ReportSystemData.Models
{
    public partial class Comment
    {
        public string CommentId { get; set; }
        public string CommentTitle { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Status { get; set; }
        public string PostId { get; set; }
        public bool IsDelete { get; set; }

        public virtual Post Post { get; set; }
        public virtual Account User { get; set; }
    }
}

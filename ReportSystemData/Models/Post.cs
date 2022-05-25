﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ReportSystemData.Models
{
    public partial class Post
    {
        public Post()
        {
            Comment = new HashSet<Comment>();
            Emotion = new HashSet<Emotion>();
            TaskDetail = new HashSet<TaskDetail>();
        }

        public string PostId { get; set; }
        public string Title { get; set; }
        public string CategoryId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? PublicTime { get; set; }
        public string Description { get; set; }
        public string Video { get; set; }
        public string Image { get; set; }
        public int ViewCount { get; set; }
        public string EditorId { get; set; }
        public string Status { get; set; }

        public virtual Category Category { get; set; }
        public virtual Account Editor { get; set; }
        [JsonIgnore]
        public virtual ICollection<Comment> Comment { get; set; }
        [JsonIgnore]
        public virtual ICollection<Emotion> Emotion { get; set; }
        [JsonIgnore]
        public virtual ICollection<TaskDetail> TaskDetail { get; set; }
    }
}

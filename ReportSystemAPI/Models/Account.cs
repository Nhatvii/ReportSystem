using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ReportSystemAPI.Models
{
    public partial class Account
    {
        public Account()
        {
            Comment = new HashSet<Comment>();
            Emotion = new HashSet<Emotion>();
            Post = new HashSet<Post>();
            ReportEditor = new HashSet<Report>();
            ReportStaff = new HashSet<Report>();
            ReportUser = new HashSet<Report>();
            Task = new HashSet<Task>();
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual AccountInfo AccountInfo { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Emotion> Emotion { get; set; }
        public virtual ICollection<Post> Post { get; set; }
        public virtual ICollection<Report> ReportEditor { get; set; }
        public virtual ICollection<Report> ReportStaff { get; set; }
        public virtual ICollection<Report> ReportUser { get; set; }
        public virtual ICollection<Task> Task { get; set; }
    }
}

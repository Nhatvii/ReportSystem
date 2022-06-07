using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ReportSystemData.Models
{
    public partial class TaskDetail
    {
        public int TaskId { get; set; }
        public string PostId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime DeadLineTime { get; set; }
        public string Status { get; set; }

        public virtual Post Post { get; set; }
        public virtual Task Task { get; set; }
    }
}

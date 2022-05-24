using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ReportSystemAPI.Models
{
    public partial class Report
    {
        public string ReportId { get; set; }
        public string Location { get; set; }
        public DateTime TimeFraud { get; set; }
        public string Description { get; set; }
        public string Video { get; set; }
        public string Image { get; set; }
        public string CategoryId { get; set; }
        public DateTime CreateTime { get; set; }
        public bool? IsAnonymous { get; set; }
        public string StaffId { get; set; }
        public string UserId { get; set; }
        public string EditorId { get; set; }
        public string Status { get; set; }

        public virtual Category Category { get; set; }
        public virtual Account Editor { get; set; }
        public virtual Account Staff { get; set; }
        public virtual Account User { get; set; }
    }
}

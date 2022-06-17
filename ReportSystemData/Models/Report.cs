using Newtonsoft.Json;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ReportSystemData.Models
{
    public partial class Report
    {
        public Report()
        {
            ReportDetail = new HashSet<ReportDetail>();
            ReportTask = new HashSet<ReportTask>();
            ReportView = new HashSet<ReportView>();
        }

        public string ReportId { get; set; }
        public string Location { get; set; }
        public DateTime TimeFraud { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreateTime { get; set; }
        public bool? IsAnonymous { get; set; }
        public string StaffId { get; set; }
        public string UserId { get; set; }
        public string EditorId { get; set; }
        public string Status { get; set; }
        public bool IsDelete { get; set; }

        [JsonIgnore]
        public virtual Category Category { get; set; }
        [JsonIgnore]
        public virtual Account Editor { get; set; }
        [JsonIgnore]
        public virtual Account Staff { get; set; }
        [JsonIgnore]
        public virtual Account User { get; set; }
        public virtual ICollection<ReportDetail> ReportDetail { get; set; }
        [JsonIgnore]
        public virtual ICollection<ReportTask> ReportTask { get; set; }
        public virtual ICollection<ReportView> ReportView { get; set; }
    }
}

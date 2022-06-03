using System;
using System.Collections.Generic;
using System.Text;

namespace ReportSystemData.Dtos.Report
{
    public class ReportDTO
    {
        public string ReportId { get; set; }
        public string Location { get; set; }
        public DateTime TimeFraud { get; set; }
        public string Description { get; set; }
        public string Video { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreateTime { get; set; }
        public bool? IsAnonymous { get; set; }
        public string StaffId { get; set; }
        public string UserId { get; set; }
        public string EditorId { get; set; }
        public string Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ReportSystemData.Dtos.Report
{
    public class ReportDTO
    {
        public string Location { get; set; }
        public DateTime TimeFraud { get; set; }
        public string Description { get; set; }
        public string Video { get; set; }
        public string Image { get; set; }
        public string CategoryId { get; set; }
        public DateTime CreateTime { get; set; }
        public bool? IsAnonymous { get; set; }
        public string Status { get; set; }
    }
}

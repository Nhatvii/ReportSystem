using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ReportSystemData.Models
{
    public partial class ReportDetail
    {
        public int ReportDetailId { get; set; }
        public string Media { get; set; }
        public string Type { get; set; }
        public string ReportId { get; set; }

        public virtual Report Report { get; set; }
    }
}

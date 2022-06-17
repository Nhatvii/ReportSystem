using Newtonsoft.Json;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ReportSystemData.Models
{
    public partial class ReportTask
    {
        public string TaskId { get; set; }
        public string ReportId { get; set; }
        public DateTime CreateTime { get; set; }
        public string Status { get; set; }

        [JsonIgnore]
        public virtual Report Report { get; set; }
        [JsonIgnore]
        public virtual Task Task { get; set; }
    }
}

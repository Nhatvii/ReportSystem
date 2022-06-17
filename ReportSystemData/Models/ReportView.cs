using Newtonsoft.Json;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ReportSystemData.Models
{
    public partial class ReportView
    {
        public string ReportId { get; set; }
        public string UserId { get; set; }
        public bool IsView { get; set; }
        [JsonIgnore]
        public virtual Report Report { get; set; }
        [JsonIgnore]
        public virtual Account User { get; set; }
    }
}

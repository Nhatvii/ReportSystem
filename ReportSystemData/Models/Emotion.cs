using Newtonsoft.Json;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ReportSystemData.Models
{
    public partial class Emotion
    {
        public string PostId { get; set; }
        public string UserId { get; set; }
        public bool EmotionStatus { get; set; }

        [JsonIgnore]
        public virtual Post Post { get; set; }
        [JsonIgnore]
        public virtual Account User { get; set; }
    }
}

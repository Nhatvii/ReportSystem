using Newtonsoft.Json;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ReportSystemData.Models
{
    public partial class Category
    {
        public Category()
        {
            Post = new HashSet<Post>();
            Report = new HashSet<Report>();
        }

        public string CategoryId { get; set; }
        public string Type { get; set; }

        [JsonIgnore]
        public virtual ICollection<Post> Post { get; set; }
        [JsonIgnore]
        public virtual ICollection<Report> Report { get; set; }
    }
}

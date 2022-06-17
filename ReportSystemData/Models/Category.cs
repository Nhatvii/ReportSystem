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

        public int CategoryId { get; set; }
        public string SubCategory { get; set; }
        [JsonIgnore]
        public int RootCategory { get; set; }
        public virtual RootCategory RootCategoryNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<Post> Post { get; set; }
        [JsonIgnore]
        public virtual ICollection<Report> Report { get; set; }
    }
}

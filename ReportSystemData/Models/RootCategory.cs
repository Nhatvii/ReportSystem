using Newtonsoft.Json;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ReportSystemData.Models
{
    public partial class RootCategory
    {
        public RootCategory()
        {
            Category = new HashSet<Category>();
        }

        public int RootCategoryId { get; set; }
        public string Type { get; set; }

        [JsonIgnore]
        public virtual ICollection<Category> Category { get; set; }
    }
}

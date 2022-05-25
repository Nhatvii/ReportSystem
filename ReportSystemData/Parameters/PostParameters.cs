using System;
using System.Collections.Generic;
using System.Text;

namespace ReportSystemData.Parameters
{
    public class PostParameters
    {
        public string postID { get; set; }
        public bool? isRecentDate { get; set; }
        public bool? isPublic { get; set; }
        public bool? isViewCount { get; set; }
    }
}

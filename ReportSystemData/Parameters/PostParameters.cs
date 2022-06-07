using System;
using System.Collections.Generic;
using System.Text;

namespace ReportSystemData.Parameters
{
    public class PostParameters
    {
        public string EditorID { get; set; }
        public int? Status { get; set; }
        public int? CategoryID { get; set; }
        public bool? isRecentDate { get; set; }
        public bool? isViewCount { get; set; }
    }
}

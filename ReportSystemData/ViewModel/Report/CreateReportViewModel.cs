using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReportSystemData.Parameter.Report
{
    public class CreateReportViewModel
    {
        [Required]
        public string UserID { get; set; }
        [StringLength(200)]
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime TimeFraud { get; set; }
        [StringLength(300)]
        [Required]
        public string Description { get; set; }
        [StringLength(200)]
        public string Video { get; set; }
        [StringLength(200)]
        public string Image { get; set; }
        [Required]
        public bool? IsAnonymous { get; set; }

    }
}

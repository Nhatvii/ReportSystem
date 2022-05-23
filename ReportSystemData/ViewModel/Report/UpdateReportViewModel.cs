using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReportSystemData.Parameter.Report
{
    public class UpdateReportViewModel
    {
        [Required]
        public string ReportId { get; set; }
        [Required]
        [StringLength(200)]
        public string Location { get; set; }
        [Required]
        public DateTime TimeFraud { get; set; }
        [Required]
        [StringLength(300)]
        public string Description { get; set; }
        [Required]
        [StringLength(200)]
        public string Video { get; set; }
        [Required]
        [StringLength(200)]
        public string Image { get; set; }
        [Required]
        public string CategoryId { get; set; }
        [Required]
        public string Status { get; set; }
    }
}

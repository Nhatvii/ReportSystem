using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReportSystemData.ViewModel.Category
{
    public class UpdateCategoryViewModel
    {
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        [Required]
        public int CategoryId { get; set; }
        [StringLength(50)]
        [Required]
        public string Type { get; set; }
    }
}

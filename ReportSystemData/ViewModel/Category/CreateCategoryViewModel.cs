using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReportSystemData.ViewModel.Category
{
    public class CreateCategoryViewModel
    {
        [StringLength(50)]
        [Required]
        public string Type { get; set; }
    }
}

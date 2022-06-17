using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReportSystemData.ViewModel.Category
{
    public class CreateCategoryViewModel
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public int RootCategoryID { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ReportSystemData.ViewModel.Task
{
    public class CreateTaskViewModel
    {
        public string EditorId { get; set; }
        public DateTime DeadlineTime { get; set; }
        public string[] ReportId { get; set; }
    }
}

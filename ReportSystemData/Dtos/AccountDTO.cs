using Newtonsoft.Json;
using ReportSystemData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportSystemData.Dtos
{
    public class AccountDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual AccountInfo AccountInfo { get; set; }
    }
}

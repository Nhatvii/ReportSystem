using System;
using System.Collections.Generic;
using System.Text;

namespace ReportSystemData.ViewModel.Account
{
    public class CreateAccountViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string IdentityCard { get; set; }
    }
}

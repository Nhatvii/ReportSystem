using System;
using System.Collections.Generic;
using System.Text;

namespace ReportSystemData.Dtos.User
{
    public class CreateUserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public int IdentityCard { get; set; }
    }
}

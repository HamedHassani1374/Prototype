using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Model.DTOs.Request.User
{
    public class UserLoginRequestDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

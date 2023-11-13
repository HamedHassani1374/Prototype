using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Model.DTOs.Request.User
{
    public class UserClaimsRequestDTO
    {
        public decimal UserID { get; set; }
        public string UserName { get; set; }
        public string? Mobile { get; set; }
        public string FullName { get; set; }
    }
}

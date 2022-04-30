using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiBoard.Domain.Requests
{
    public class PatchUser
    {
        public string Email { get; set; } = string.Empty;

        public string firstName { get; set; } = string.Empty;

        public string lastName { get; set; } = string.Empty;

        public string CurrentPassword { get; set; } = string.Empty;

        public string NewPassword { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiBoard.Domain.Enums;

namespace HiBoard.Domain.DTOs;

    public class UserDto
    {
        public int Id { get; protected set; }

        public string? UserName { get; set; }

        public string? FirstName { get; set; } 

        public string? LastName { get; set; }  

        public UserRole Role { get; set; }

        public UserDepartments Department { get; set; }
}


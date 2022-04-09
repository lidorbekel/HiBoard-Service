namespace HiBoard.Domain.DTOs;

    public class UserDto
    {
        public int Id { get; protected set; }

        public string UserName { get; set; } = string.Empty;

        public string? FirstName { get; set; } 

        public string? LastName { get; set; } 

        public string Role { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;
    }


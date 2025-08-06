using System;
namespace VemboAPI.Domain.DTOs
{
    public class CreateUserDto
    {
        public string NickName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}


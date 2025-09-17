using System;
namespace VemboAPI.Domain.DTOs
{
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string NickNameSlug { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}


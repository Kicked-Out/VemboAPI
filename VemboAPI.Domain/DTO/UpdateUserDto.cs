using Microsoft.AspNetCore.Http;
namespace VemboAPI.Domain.DTOs
{
    public class UpdateUserDto
    {
        public IFormFile? Avatar { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string NickNameSlug { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}


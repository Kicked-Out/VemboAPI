namespace VemboAPI.Domain.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string NickName { get; set; }
        public string NickNameSlug { get; set; }
        public string Email { get; set; }
        public int Level { get; set; }
        public int Rating { get; set; }
        public bool IsPremium { get; set; }
        public long XP { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

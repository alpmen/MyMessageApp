namespace MyMessageApp.Data.UserRepository.Dtos.Request
{
    public class UserUpdateRequest
    {
        public string Email { get; set; } 
        public string Password { get; set; } 
        public int Status { get; set; }
    }
}
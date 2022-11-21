namespace MyMessageApp.Data.UserRepository.Dtos.Request
{
    public class UserCreateRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
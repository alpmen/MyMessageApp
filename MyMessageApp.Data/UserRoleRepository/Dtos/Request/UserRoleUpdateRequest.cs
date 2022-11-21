namespace MyMessageApp.Data.UserRoleRepository.Dtos.Request
{
    public class UserRoleUpdateRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
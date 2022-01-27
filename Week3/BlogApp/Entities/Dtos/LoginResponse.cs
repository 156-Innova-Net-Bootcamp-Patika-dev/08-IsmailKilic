namespace Entities.Dtos
{
    /// <summary>
    /// View model used for login response
    /// </summary>
    public class LoginResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}

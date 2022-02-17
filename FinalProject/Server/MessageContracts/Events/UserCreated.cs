namespace MessageContracts.Events
{
    public class UserCreated
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}

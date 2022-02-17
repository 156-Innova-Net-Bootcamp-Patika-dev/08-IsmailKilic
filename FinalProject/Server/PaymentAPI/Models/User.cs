using PaymentAPI.Data;

namespace PaymentAPI.Models
{
    [BsonCollection("users")]
    public class User : IEntity
    {
        public string UserId{ get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public double Balance { get; set; }
    }
}

namespace PaymentAPI.Data
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
    public interface IMongoDbSettings
    {
        string Database { get; set; }
        string ConnectionString { get; set; }
    }
}

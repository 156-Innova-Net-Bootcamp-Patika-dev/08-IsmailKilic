using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PaymentAPI.Models
{
    public abstract class IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

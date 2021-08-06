using MongoCrudAPI.DbContext.Attributes;
using MongoCrudAPI.DbContext.Document;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoCrudAPI.Models
{
    [BsonIgnoreExtraElements]
    [MongoCollection("customers")]
    public class Customer : MongoDocument
    {
        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("role")]
        public string RoleName { get; set; }
    }
}

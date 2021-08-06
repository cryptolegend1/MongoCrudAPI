using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace MongoCrudAPI.DbContext.Document
{
    public interface IMongoDocument
    {
        [BsonId]
        ObjectId Id { get; set; }
    }
}

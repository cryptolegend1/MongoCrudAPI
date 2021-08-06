using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace MongoCrudAPI.DbContext.Document
{
    public class MongoDocument : IMongoDocument
    {
        public ObjectId Id { get; set; }
    }
}

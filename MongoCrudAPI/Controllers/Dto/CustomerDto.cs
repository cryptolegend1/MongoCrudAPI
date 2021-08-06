using MongoCrudAPI.DbContext.Attributes;
using MongoCrudAPI.DbContext.Document;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoCrudAPI.Controllers.Dto
{
    public class CustomerDto
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string RoleName { get; set; }
    }
}

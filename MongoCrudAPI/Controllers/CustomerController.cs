
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoCrudAPI.Application;
using MongoCrudAPI.Controllers.Dto;
using MongoCrudAPI.DbContext.Repository;
using MongoCrudAPI.Models;
using MongoDB.Bson;

namespace MongoCrudAPI.Controllers
{
    public class CustomerController : MongoCrudApiController<Customer, CustomerDto>
    {
        public CustomerController(IMongoRepository<Customer> repository)
            : base(repository)
        {

        }

        [HttpGet]
        [Route("/RoleSummary/{role}")]
        public async Task<IEnumerable<RoleSummaryDto>> GetRoleSummaryAsync()
        {
            var collection = Repository.GetCollection();
            var group = new BsonDocument
            {
                {
                    "$group",
                    new BsonDocument
                    {
                        { "_id", "$role" },
                        { "count", new BsonDocument{ { "$sum", 1 } } }
                    }
                }
            };
            var pipeline = new[] { group };
            var result = await collection.AggregateAsync<RoleSummaryDto>(pipeline);

            return result.Current;
        }
    }
}

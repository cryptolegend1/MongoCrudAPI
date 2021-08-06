
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

namespace MongoCrudAPI.Controllers
{
    public class CustomerController : MongoCrudApiController<Customer, CustomerDto>
    {
        public CustomerController(IMongoRepository<Customer> repository)
            : base(repository)
        {

        }
    }
}

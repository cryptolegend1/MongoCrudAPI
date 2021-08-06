using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoCrudAPI.Controllers.Dto
{
    public class RoleSummaryDto
    {
        public string _id { get; set; }

        public int count { get; set; }
    }
}

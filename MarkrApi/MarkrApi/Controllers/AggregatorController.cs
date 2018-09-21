using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MarkrApi.Code;
using MarkrApi.Models;

namespace MarkrApi.Controllers
{
    [Route("results/{id}/aggregate")]
    public class AggregatorController : ApiController
    {
        public Aggregates Get(int id)
        {
            return MarkrApiBusinessLogic.GetAggregatesForTest(id);
        }
    }
}

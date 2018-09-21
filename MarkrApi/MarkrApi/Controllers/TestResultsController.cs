using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MarkrApi.Models;
using MarkrApi.Code;

namespace MarkrApi.Controllers
{
    public class TestResultsController : ApiController
    {
        [HttpPost]
        [Route("import")]
        public ImportResults Post(MCQTestResults testResults)
        {
            // If testResults is null then it means XML body was either empty or XML parsing failed.
            // Either way, we need to return appropriate HTTP error
            if (testResults == null)
            {
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.BadRequest);
                message.Content = new StringContent("XML data not in correct format.");
                throw new HttpResponseException(message);
            }

            return MarkrApiBusinessLogic.ImportTestResults(testResults);
        }
    }
}

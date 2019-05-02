using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TestApi.Repository;
using Newtonsoft.Json;
using TestApi.Model;
using System.IO;
//using System.Net.Http;
//using Microsoft.Azure.WebJobs.Host;
//using System.Linq;
//using System.Net;

namespace TestApi
{
    public static class CreateEmployee
    {
        [FunctionName("CreateEmployee")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "employee")] HttpRequest req,
            ILogger log)
        {
            //public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "employee")]HttpRequestMessage req, TraceWriter log)
            //{

            //    // parse query parameter
            //    string name = req.GetQueryNameValuePairs()
            //        .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
            //        .Value;

            //    if (name == null)
            //    {
            //        // Get request body
            //        dynamic data = await req.Content.ReadAsAsync<object>();
            //        name = data?.name;
            //    }

            //    return name == null
            //       ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
            //       : req.CreateResponse(HttpStatusCode.OK, "Hello " + name);

            var employeeId = req.Headers["Token"];
            if (string.IsNullOrEmpty(employeeId)) return new UnauthorizedResult();

            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var entity = JsonConvert.DeserializeObject<Employee>(body);

            entity.EmployeeId = employeeId;
            entity = await EmployeeRepository.CreateAsync(entity);

            return new OkObjectResult(entity);
        }
    }
}

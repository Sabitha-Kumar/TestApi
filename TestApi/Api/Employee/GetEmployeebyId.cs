using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using TestApi.Repository;

namespace TestApi.Api.Employee
{
    public static class GetEmployeebyId
    {
        [FunctionName("GetEmployeebyId")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "employee")] HttpRequest req,
            ILogger log)
        {
            var employeeId = req.Headers["Token"];
            if (string.IsNullOrEmpty(employeeId)) return new UnauthorizedResult();

            var entity = await EmployeeRepository.GetByIdAsync(employeeId);
            if (entity == null) return new NotFoundResult();

            return new OkObjectResult(entity);
        }
    }
}

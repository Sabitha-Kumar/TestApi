using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.Helper;
using TestApi.Model;

namespace TestApi.Repository
{
    public class EmployeeRepository
    {
        private const string TableName = "Employee";

        public static async Task<Employee> CreateAsync(Employee entity)
        {
            var table = new AzureTable<Employee>(TableName);

            if(!string.IsNullOrEmpty(entity.EmployeeId))
            {
                var dbEntity = await table.GetEntityByPartitionKeyAndRowKeyAsync($"EmployeeId-{entity.EmployeeId}-EmployeeId", $"{entity.EmployeeId}");
                if (dbEntity == null) throw new Exception($"EmployeeId {entity.EmployeeId} alredy added");
            }

            entity.EmployeeId = Guid.NewGuid().ToString();

            var tasks = new List<Task>();

            //PK : EmplyeeId
            tasks.Add(table.CreateEntityAsync(entity.ToEntity($"EmployeeId-{entity.EmployeeId}-EmployeeId", $"{entity.EmployeeId}")));

            //PK : Name
            if(!string.IsNullOrEmpty(entity.FirstName))
            tasks.Add(table.CreateEntityAsync(entity.ToEntity($"FirstName-{entity.FirstName}-FirstName", $"{entity.FirstName}")));

            //PK : Email
            if (!string.IsNullOrEmpty(entity.Email))
            tasks.Add(table.CreateEntityAsync(entity.ToEntity($"Email-{entity.Email}-Email", $"{entity.Email}")));


            //PK : State
            if (!string.IsNullOrEmpty(entity.State))
            tasks.Add(table.CreateEntityAsync(entity.ToEntity($"Email-{entity.State}-Email", $"{entity.State}")));

            await Task.WhenAll(tasks);
            return entity;
        }


    }
}

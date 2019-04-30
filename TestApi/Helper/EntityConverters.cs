using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.Model;

namespace TestApi.Helper
{
    public static class EntityConverters
    {
        public static Employee ToEntity(this Employee entity, string partitionKey, string rowKey)
        {
            return new Employee
            {
                PartitionKey = partitionKey,
                RowKey = rowKey,

                EmployeeId = entity.EmployeeId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Gender = entity.Gender,
                Email = entity.Email,
                Phone = entity.Email,
                State = entity.State
            };
        }
    } 
}

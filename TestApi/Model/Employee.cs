using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApi.Model
{
    public class Employee : TableEntity
    {
        public Employee() { }
        public Employee(string partitionkey, string rowkey)
        {
            this.PartitionKey = partitionkey;
            this.RowKey = rowkey;
        }

        public string EmployeeId;

        public string FirstName;
        public string LastName;
        public string Gender;
        public string Email;
        public string Phone;
        public string State;
    }
}

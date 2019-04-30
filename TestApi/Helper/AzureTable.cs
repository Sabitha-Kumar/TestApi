using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.RetryPolicies;

namespace TestApi.Helper
{
    public class AzureTable<T> where T : TableEntity, new()
    {
        private readonly CloudTable cloudTable;

        public AzureTable(string tableName)
        {
            Validate.TableName(tableName, "tableName");

            var storageConnectionString = "AccountName=devstoreaccount1;" +
                "AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;" +
                "DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;" +
                "QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;" +
                "TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
            var cloudStorageAccount = CloudStorageAccount.Parse(storageConnectionString);

            var requestOptions = new TableRequestOptions
            {
                RetryPolicy = new ExponentialRetry(TimeSpan.FromSeconds(1), 3)
            };

            var cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            cloudTableClient.DefaultRequestOptions = requestOptions;

            cloudTable = cloudTableClient.GetTableReference(tableName);
            cloudTable.CreateIfNotExistsAsync();
        }

        public async Task<T> GetEntityByPartitionKeyAndRowKeyAsync(string partionKey, string rowKey)
        {
            var retrieveOperation = TableOperation.Retrieve<T>(partionKey, rowKey);
            var retrievedResult = await cloudTable.ExecuteAsync(retrieveOperation);

            return retrievedResult.Result as T;
        }

        public async Task CreateEntityAsync(T entity)
        {
            Validate.Null(entity, "entity");
            var insertOperation = TableOperation.Insert(entity);
            await cloudTable.ExecuteAsync(insertOperation);
        }
    }
}
 
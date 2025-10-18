using Microsoft.Azure.Cosmos;
using IbasSupportApi.Models;

namespace IbasSupportApi.Services
{
    public class CosmosDbService
    {
        private readonly Container _container;

        public CosmosDbService(IConfiguration configuration)
        {
            // Log for at bekræfte, at vi får korrekt konfiguration
            Console.WriteLine("🔍 Connection string: " + configuration["CosmosDb:ConnectionString"]);

            var account = configuration["CosmosDb:AccountEndpoint"];
            var key = configuration["CosmosDb:AccountKey"];
            var databaseName = configuration["CosmosDb:DatabaseName"];
            var containerName = configuration["CosmosDb:ContainerName"];

            // Opret Cosmos-klienten
            var client = new CosmosClient(account, key);
            var database = client.GetDatabase(databaseName);
            _container = database.GetContainer(containerName);
        }

        public async Task AddSupportMessageAsync(SupportMessage message)
        {
            // Sørg for at der altid er et unikt id
            if (string.IsNullOrWhiteSpace(message.Id))
            {
                message.Id = Guid.NewGuid().ToString();
            }

            // Gem beskeden i Cosmos DB
            await _container.CreateItemAsync(message, new PartitionKey(message.Id));
        }
    }
}

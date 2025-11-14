using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using IbasSupportApi.Models;

namespace IbasSupportApi.Services
{
    public class CosmosDbService
    {
        private readonly Container _container;
        private readonly ILogger<CosmosDbService> _logger;

        public CosmosDbService(IConfiguration config, ILogger<CosmosDbService> logger)
        {
            _logger = logger;

            try
            {
                var endpoint = config["CosmosDb:AccountEndpoint"];
                var key = config["CosmosDb:AccountKey"];
                var database = config["CosmosDb:DatabaseName"];
                var containerName = config["CosmosDb:ContainerName"];

                _logger.LogInformation("CosmosDB settings loaded:");
                _logger.LogInformation($"Endpoint: {endpoint}");
                _logger.LogInformation($"Database: {database}, Container: {containerName}");

                CosmosClient client = new CosmosClient(endpoint, key);
                _container = client.GetContainer(database, containerName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize Cosmos DB client.");
                throw;
            }
        }

        public async Task AddSupportMessageAsync(SupportMessage message)
        {
            try
            {
                await _container.CreateItemAsync(message, new PartitionKey(message.Id));
                _logger.LogInformation("Message saved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save message to Cosmos DB.");
                throw;
            }
        }
    }
}

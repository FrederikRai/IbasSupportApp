using Microsoft.AspNetCore.Mvc;
using IbasSupportApi.Models;
using IbasSupportApi.Services;

namespace IbasSupportApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupportController : ControllerBase
    {
        private readonly CosmosDbService _cosmosDbService;

        public SupportController(CosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SupportMessage message)
        {
            if (message == null)
                return BadRequest("Message cannot be null.");

            await _cosmosDbService.AddSupportMessageAsync(message);
            return Ok(new { status = "saved", id = message.Id });
        }
    }
}

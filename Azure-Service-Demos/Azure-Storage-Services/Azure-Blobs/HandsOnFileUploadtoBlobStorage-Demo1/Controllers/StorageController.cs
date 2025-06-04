using HandsOnFileUploadtoBlobStorage_Demo1.Models;
using HandsOnFileUploadtoBlobStorage_Demo1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandsOnFileUploadtoBlobStorage_Demo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IAzureStorage _storage;

        public StorageController(IAzureStorage storage)
        {
            _storage = storage;
        }
        [HttpPost(nameof(Upload))]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            BlobResponseDto? response=await _storage.UploadAsync(file);
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}

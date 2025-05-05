
using Microsoft.AspNetCore.Mvc;
using imagenes_ms.Services;

namespace imagenes_ms.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagenesController : ControllerBase
    {
        private readonly GridFsService _gridFsService;

        public ImagenesController(GridFsService gridFsService)
        {
            _gridFsService = gridFsService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] string mascotaId)
        {
            var id = await _gridFsService.UploadImageAsync(file, mascotaId);
            return Ok(new { id });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var data = await _gridFsService.GetImageAsync(id);
            return File(data, "image/jpeg");
        }

        [HttpGet("mascota/{mascotaId}")]
        public async Task<IActionResult> ListByMascota(string mascotaId)
        {
            var ids = await _gridFsService.GetImagesByMascota(mascotaId);
            return Ok(ids);
        }
    }
}

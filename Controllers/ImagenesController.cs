using Microsoft.AspNetCore.Mvc;

namespace Imagenes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagenesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Ping()
        {
            return Ok("Servicio de imágenes operativo");
        }
    }
}

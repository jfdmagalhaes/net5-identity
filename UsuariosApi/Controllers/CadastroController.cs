using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;

namespace UsuariosApi.Controllers
{
    [Route("[controller]")]
    public class CadastroController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateUser(CreateUserDto createDto)
        {
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoCRUD.Models;

namespace ProyectoCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : ControllerBase
    {
        private readonly TestDbContext _dbcontext;

        public ContactoController(TestDbContext testDbContext)
        {
            _dbcontext = testDbContext;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<IActionResult> Lista()
        {

            List<Contacto> lista = await _dbcontext.Contactos.OrderByDescending(c => c.IdContacto).ToListAsync();
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] Contacto contacto)
        {
            await _dbcontext.Contactos.AddAsync(contacto);
            await _dbcontext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, "ok"); 

        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Contacto contacto)
        {
             _dbcontext.Contactos.Update(contacto);
            await _dbcontext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, "ok");

        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            Contacto contacto = _dbcontext.Contactos.Find(id);
            _dbcontext.Contactos.Remove(contacto);
            await _dbcontext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, "ok");
        }
    }
}
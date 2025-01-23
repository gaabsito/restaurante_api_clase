using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Repositories;
using RestauranteAPI.Service;

namespace RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatoPrincipalController : ControllerBase
    {
        private static List<PlatoPrincipal> platos = new List<PlatoPrincipal>();

        private readonly IPlatoPrincipalService _service;

        public PlatoPrincipalController(IPlatoPrincipalService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PlatoPrincipal>>> GetPlatos()
        {
            var platos = await _service.GetAllAsync();
            return Ok(platos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PlatoPrincipal>> GetPlato(int id)
        {
            var plato = await _service.GetByIdAsync(id);
            if (plato == null)
            {
                return NotFound();
            }
            return Ok(plato);
        }

        [HttpPost]
        public async Task<ActionResult<PlatoPrincipal>> CreatePlato(PlatoPrincipal plato)
        {
            await _service.AddAsync(plato);
            return CreatedAtAction(nameof(GetPlato), new { id = plato.Id }, plato);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlato(int id, PlatoPrincipal updatedPlato)
        {
            var existingPlato = await _service.GetByIdAsync(id);
            if (existingPlato == null)
            {
                return NotFound();
            }

            existingPlato.Nombre = updatedPlato.Nombre;
            existingPlato.Precio = updatedPlato.Precio;
            existingPlato.Ingredientes = updatedPlato.Ingredientes;

            await _service.UpdateAsync(existingPlato);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlato(int id)
        {
            var plato = await _service.GetByIdAsync(id);
            if (plato == null)
            {
                return NotFound(); 
            }

            await _service.DeleteAsync(id); 
            return NoContent();
        }


        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _service.InicializarDatosAsync();
            return Ok("Datos inicializados correctamente.");
        }

    }
}
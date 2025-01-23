using Microsoft.Data.SqlClient;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestauranteAPI.Service;
using RestauranteAPI.Repositories;

namespace RestauranteAPI.Services
{
    public class PostreService : IPostreService
    {
        private readonly IPostreRepository _postreRepository;

        public PostreService(IPostreRepository postreRepository)
        {
            _postreRepository = postreRepository;
        }

        public async Task<List<Postre>> GetAllAsync()
        {
           return await _postreRepository.GetAllAsync();
        }

        public async Task<Postre?> GetByIdAsync(int id)
        {
           return await _postreRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Postre postre)
        {
           await _postreRepository.AddAsync(postre);
        }

        public async Task UpdateAsync(Postre postre)
        {
            await _postreRepository.UpdateAsync(postre);
        }

        public async Task DeleteAsync(int id)
        {
            var plato = await _postreRepository.GetByIdAsync(id);
           if (plato == null)
           {
               //return NotFound();
           }
           await _postreRepository.DeleteAsync(id);
           //return NoContent();
        }

        public async Task InicializarDatosAsync()
        {
            await _postreRepository.InicializarDatosAsync();
        }
    }
}
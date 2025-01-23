using Microsoft.Data.SqlClient;
using Models;
using RestauranteAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestauranteAPI.Service;

namespace RestauranteAPI.Services
{
    public class BebidaService: IBebidaService
    {
        private readonly IBebidaRepository _bebidaRepository;

        public BebidaService(IBebidaRepository bebidaRepository)
        {
            _bebidaRepository = bebidaRepository;
        }

        public async Task<List<Bebida>> GetAllAsync()
        {
        return await _bebidaRepository.GetAllAsync();
        }

        public async Task<Bebida?> GetByIdAsync(int id)
        {
            return await _bebidaRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Bebida bebida)
        {
           await _bebidaRepository.AddAsync(bebida);
           
        }

        public async Task UpdateAsync(Bebida bebida)
        {
           
            await _bebidaRepository.UpdateAsync(bebida);
        }

        public async Task DeleteAsync(int id)
        {
            var bebida = await _bebidaRepository.GetByIdAsync(id);
           if (bebida == null)
           {
               //return NotFound();
           }
           await _bebidaRepository.DeleteAsync(id);
           //return NoContent();
        }
        public async Task InicializarDatosAsync()
        {
           await _bebidaRepository.InicializarDatosAsync();
        }
    }
}

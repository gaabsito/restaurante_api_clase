using Microsoft.Data.SqlClient;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestauranteAPI.Repositories
{
    public class BebidaRepository : IBebidaRepository
    {
        private readonly string _connectionString;

        public BebidaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Bebida>> GetAllAsync()
        {
            var bebidas = new List<Bebida>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Precio, EsAlcoholica FROM Bebida";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var bebida = new Bebida(
                                reader.GetString(1), 
                                (double)reader.GetDecimal(2), 
                                reader.GetBoolean(3) //si es alcoholica
                            )
                            {
                                Id = reader.GetInt32(0) 
                            };

                            bebidas.Add(bebida);
                        }
                    }
                }
            }
            return bebidas;
        }

        public async Task<Bebida?> GetByIdAsync(int id)
        {
            Bebida? bebida = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Precio, EsAlcoholica FROM Bebida WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            bebida = new Bebida(
                                reader.GetString(1), 
                                (double)reader.GetDecimal(2), 
                                reader.GetBoolean(3) 
                            )
                            {
                                Id = reader.GetInt32(0) 
                            };
                        }
                    }
                }
            }
            return bebida;
        }

        public async Task AddAsync(Bebida bebida)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Bebida (Nombre, Precio, EsAlcoholica) VALUES (@Nombre, @Precio, @EsAlcoholica)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", bebida.Nombre);
                    command.Parameters.AddWithValue("@Precio", bebida.Precio);
                    command.Parameters.AddWithValue("@EsAlcoholica", bebida.EsAlcoholica);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Bebida bebida)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Bebida SET Nombre = @Nombre, Precio = @Precio, EsAlcoholica = @EsAlcoholica WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", bebida.Id);
                    command.Parameters.AddWithValue("@Nombre", bebida.Nombre);
                    command.Parameters.AddWithValue("@Precio", bebida.Precio);
                    command.Parameters.AddWithValue("@EsAlcoholica", bebida.EsAlcoholica);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Bebida WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task InicializarDatosAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"
                    INSERT INTO Bebida (Nombre, Precio, EsAlcoholica)
                    VALUES 
                    (@Nombre1, @Precio1, @EsAlcoholica1),
                    (@Nombre2, @Precio2, @EsAlcoholica2)";

                using (var command = new SqlCommand(query, connection))
                {
                    // Parámetros para la primera bebida
                    command.Parameters.AddWithValue("@Nombre1", "Cerveza");
                    command.Parameters.AddWithValue("@Precio1", 3.50);
                    command.Parameters.AddWithValue("@EsAlcoholica1", true);

                    // Parámetros para la segunda bebida
                    command.Parameters.AddWithValue("@Nombre2", "Zumo de naranja");
                    command.Parameters.AddWithValue("@Precio2", 2.00);
                    command.Parameters.AddWithValue("@EsAlcoholica2", false);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}

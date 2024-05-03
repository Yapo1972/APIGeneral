using Conexiones;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using Npgsql;

namespace APICompacto.Datos
{
    public class DatosERPADV
    {
        private ConexionBD conexion = new ConexionBD("BDERPADV");
        public async Task<ActionResult<List<DatosClientes>>> DatosAlbaran()
        {
            var con = new NpgsqlConnection( connectionString: conexion.cadenaConexion() );
            con.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT * FROM fact.albaran";
            NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            var result = new List<DatosClientes>();
            while (await reader.ReadAsync())
            {
                result.Add(new DatosClientes

                    //id: (int)reader["id"],
                    //first_name: reader[1] as string, // column index can be used
                    //last_name: reader.GetString(2), // another syntax option
                    //subject: reader["subject"] as string,
                    //salary: (int)reader["salary"])
                    
                {
                    idCliente = reader["cod_empresa"] as string ?? ""
                });

            }
            return result;
        }
    }
}


using Conexiones;
using Modelos;
using System.Data.SqlClient;

namespace Datos
{
    public class DClientes : IDClientes
    {
        private ConexionBD conexion = new ConexionBD();
        public async Task<List<DatosClientes>> obtenerDatosClientes(string idCliente)
        {
            var resultado = new List<DatosClientes>();
            using (var conexionSql = new SqlConnection(conexion.cadenaConexion()))
            {
                await conexionSql.OpenAsync();
                using (var comando = new SqlCommand("obtenerDatosCliente", conexionSql))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("idCliente", idCliente);
                    using (var item = await comando.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var datosClientes = new DatosClientes();
                            datosClientes.idCliente = (string)item["idCliente"];
                            datosClientes.descCliente = (string)item["descCliente"];
                            datosClientes.ano = (int)item["ano"];
                            datosClientes.contratacionTotal = (decimal)item["contratacionTotal"];
                            datosClientes.ventaTotal = (decimal)item["ventaTotal"];
                            resultado.Add(datosClientes);
                        }

                    }
                }
            }
            return resultado;
        }
    }
}

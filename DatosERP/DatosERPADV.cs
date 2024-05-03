using Dapper;
using Npgsql;

namespace DatosERP
{
    public class ConexionPostgres
    {
        private readonly Dictionary<string, string> _cadenaConexion = new Dictionary<string, string> {
            {"BDERPADV", "Server=192.168.5.130;Port=5432;User Id=read_user;Password=consulta;Database=adverp;" }
        };
        private string cadenaConexion { get; set; }
        public ConexionPostgres(string _conex)
        {
            cadenaConexion = _cadenaConexion[_conex];
        }
        public string obtener() => cadenaConexion;
    }

    public static class DatosERPADV
    {
        //public static async Task<List<DatosClientes>> DatosAlbaranAsync()
        //{
        //    var cadenaConexion = new ConexionPostgres("BDERPADV");
        //    var con = new NpgsqlConnection( connectionString: cadenaConexion.obtener()) ;
        //    con.Open();
        //    using var cmd = new NpgsqlCommand();
        //    cmd.Connection = con;
        //    cmd.CommandText = $"SELECT * FROM fact.albaran";
        //    NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        //    var result = new List<DatosClientes>();
        //    while (await reader.ReadAsync())
        //    {
        //        result.Add(new DatosClientes

        //            //id: (int)reader["id"],
        //            //first_name: reader[1] as string, // column index can be used
        //            //last_name: reader.GetString(2), // another syntax option
        //            //subject: reader["subject"] as string,
        //            //salary: (int)reader["salary"])
                    
        //        {
        //            idCliente = reader["cod_empresa"] as string ?? ""
        //        });

        //    }
        //    return result;
        //}
    }

    internal class DatosClientes
    {
        internal string idCliente;
    }
}

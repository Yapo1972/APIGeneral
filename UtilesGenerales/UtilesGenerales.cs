using Dapper;
using System.Data.SqlClient;
namespace UtilesGenerales
{
    public class ConexionSql
    {
        private readonly Dictionary<string, string> _cadenaConexion = new Dictionary<string, string> {
            {"AccesoDatos", "data source=server-assets;initial catalog=AccesoDatos;user id=user_assetsp;password=Super2009" }
        };
        private string cadenaConexion { get; set; }
        public ConexionSql(string _conex)
        {
            cadenaConexion = _cadenaConexion[_conex];
        }
        public string obtener() => cadenaConexion;
    }

    public static  class DatosTecnicos
    {
        public static List<string> obtenerTodosDatosTecnicos( string comienzaPor)
        {
            var resultado = new List<string>();

            var cadConexion = new ConexionSql("AccesoDatos");
            using (var conn = new SqlConnection(cadConexion.obtener()))
                resultado = conn.Query<string>("pr_listaDatosTecnicos", new { comienzaPor }, commandType: System.Data.CommandType.StoredProcedure).AsList();
            return resultado;

        }
    }
}

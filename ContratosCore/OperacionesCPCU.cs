using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratosCore
{
    public class VMDatosCodigos
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }

    public class VMDatosProductosCodificados: VMDatosCodigos
    {
        public string Fabrica { get; set; }
    }

    public class VMDatosPorFabrica
    {
        public string Codigo { get; set; }
        public string DescripcionHabana { get; set; }
        public string DescripcionSantiago { get; set; }
        public string DescripcionPalma { get; set; }
    }

    public class VMDatosResumenPorFabrica
    {
        public List<VMDatosPorFabrica> codigos { get; set; }
        public string codigosDisponiblesAPartirDe { get=>codigos.OrderByDescending(x=>x.Codigo.Replace(".","").Split('-')[0]).FirstOrDefault()?.Codigo ?? ""; }
    }
    public static class OperacionesCPCU
    {
        public static List<VMDatosCodigos> obtenerCodigosRelacionados( string comienzaPor)
        {
            var resultado = new List<VMDatosCodigos>();
            var cadConexion = new ConexionSql("AccesoDatos");
            using (var conn = new SqlConnection(cadConexion.obtener()))
                resultado = conn.Query<VMDatosCodigos>("codigosCPCU", new { comienza = comienzaPor }, commandType: System.Data.CommandType.StoredProcedure).OrderBy(x=>x.Codigo).ToList();
            return resultado;
        }
        public static List<VMDatosPorFabrica> obtenerProductosCodificados( string comienzaPor)
        {
            var resultado = new List<VMDatosPorFabrica>();
            var cadConexion = new ConexionSql("AccesoDatos");
            using (var conn = new SqlConnection(cadConexion.obtener()))
            {
                var datos = conn.Query<VMDatosProductosCodificados>("productosCodificados", new { codigoEmpieza = comienzaPor }, commandType: System.Data.CommandType.StoredProcedure).ToList();
                foreach ( var item in datos)
                {
                    var datoExiste = resultado.Find(x=>x.Codigo == item.Codigo);
                    if(datoExiste == null)
                    {
                        datoExiste = new VMDatosPorFabrica
                        {
                            Codigo = item.Codigo,
                            DescripcionHabana = "--NO EXISTE--",
                            DescripcionPalma = "--NO EXISTE--",
                            DescripcionSantiago = "--NO EXISTE--"
                        };
                        resultado.Add(datoExiste);
                    }
                    switch(item.Fabrica)
                    {
                        case "La Habana":
                            datoExiste.DescripcionHabana = item.Descripcion;
                            break;
                        case "Santiago de Cuba":
                            datoExiste.DescripcionSantiago = item.Descripcion;
                            break;
                        case "Palma Soriano":
                            datoExiste.DescripcionPalma = item.Descripcion;
                            break;
                    }
                }
            }
            return resultado.OrderBy(x=>x.Codigo.Replace(".", "").Split('-')[0]).ToList();
        }
    }
}

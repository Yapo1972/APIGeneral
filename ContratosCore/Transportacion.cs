using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratosCore
{
    public class VMDatosProveedor
    {
        public string Id_Proveedor { get; set; }
        public string Desc_Proveedor { get; set; }

    }
    public class VMDatosServicios : VMDatosProveedor
    {
        public int Contador { get; set; }
        public int Id_Recepcion { get; set; }
        public string Id_Almacen { get; set; }
        public string Id_FacturaProveedor { get; set; }
        public decimal Importe { get; set; }
        public DateTime Fecha_Confirmacion { get; set; }
    }

    public class VMResumenProveedorServicio : VMDatosProveedor
    {
        public decimal ImporteTotal { get; set; }
        public int Ano { get; set; }
    }

    public static class Transportacion
    {
        public static List<VMResumenProveedorServicio> gastosTransportacionMateriaPrima( DateTime fechaI, DateTime fechaF)
        {
            var resultado = new List<VMResumenProveedorServicio>();
            var cadConexion = new ConexionSql("AccesoDatos");
            using (var conn = new SqlConnection(cadConexion.obtener()))
            {
                var datos = conn.Query<VMDatosServicios>("gastosTransportacionMateriaPrima", new { fechaI, fechaF }, commandType: System.Data.CommandType.StoredProcedure).ToList();
                resultado = datos.GroupBy(x => new { x.Id_Proveedor, x.Desc_Proveedor, x.Fecha_Confirmacion.Year })
                            .Select(x => new VMResumenProveedorServicio { Id_Proveedor = x.Key.Id_Proveedor, Desc_Proveedor = x.Key.Desc_Proveedor, Ano = x.Key.Year, ImporteTotal = x.Sum(x => x.Importe) }).ToList();
            }
            return resultado;
        }
    }
}

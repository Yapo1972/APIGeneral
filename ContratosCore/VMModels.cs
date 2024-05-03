using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratosCore
{
    public interface IDatosCliente
    {
        string idCliente { get; set; }
        string descCliente { get; set; }
        string organismo { get; set; }
    }
    public interface IDatosProductos
    {
        string idProducto { get; set; }
        string descProducto { get; set; }
    }

    public class VMDatosFicherosFT
    {
        public string rutaFichero { get; set; }
        public string nombreFichero { get; set; }
        public string base64 { get; set; }
    }

    public class VMAnalisisCumplimiento
    {
        public VMAnalisisCumplimiento(decimal _plan = 0, decimal _real = 0)
        {
            plan = _plan;
            real = _real;
        }
        public decimal plan { get; set; }
        public decimal real { get; set; }
        public decimal diferencia { get => real - plan; }
        public decimal porciento { get => plan > 0 ? Math.Round(real / plan, 2) : 0; }
    }
    public class VMDatosVentas
    {
        public int mesVenta { get; set; }
        public string fabricaVenta { get; set; }
        public decimal cantidadVendida { get; set; }
        public decimal importeVendido { get; set; }
        public decimal precioCL { get => cantidadVendida > 0 ? Math.Round(ventaCL / cantidadVendida, 4) : 0; }
        public decimal importeCL { get => ventaCL; }
        public decimal ventaCL { get; set; } // Esta venta es la que viene del Procedimiento Almacenado. Utilizarla para chequear la diferencia!!!!!!! y luego no tener que calcular los precios 
    }


    public class VMDatosContratacion
    {
        public int mesContratacion { get; set; }
        public int idProductoContrato { get; set; }
        public string fabricaContratacion { get; set; }
        public decimal contratado { get; set; }
        public decimal precioCUP { get; set; }
        public decimal precioCL { get; set; }
        public decimal valorTransportacion { get; set; }
        public decimal importeContratadoOrigen { get => contratado * precioCUP; }
        public decimal importeContratadoDestino { get => contratado * (Math.Round((precioCUP + valorTransportacion) / 0.024m, 0, MidpointRounding.AwayFromZero) * 0.024m); }
        public decimal importeContratadoCL { get => contratado * precioCL; }
    }

    public class VMResumenContratacionAno
    {
        public int IDCliente { get; set; }
        public string CodigoInternoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string CodigoInternoProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public decimal Ene { get; set; }
        public decimal Feb { get; set; }
        public decimal Mar { get; set; }
        public decimal Abr { get; set; }
        public decimal May { get; set; }
        public decimal Jun { get; set; }
        public decimal Jul { get; set; }
        public decimal Ago { get; set; }
        public decimal Sep { get; set; }
        public decimal Oct { get; set; }
        public decimal Nov { get; set; }
        public decimal Dic { get; set; }
        public decimal PrecioCUPEne { get; set; }
        public decimal PrecioCUPFeb { get; set; }
        public decimal PrecioCUPMar { get; set; }
        public decimal PrecioCUPAbr { get; set; }
        public decimal PrecioCUPMay { get; set; }
        public decimal PrecioCUPJun { get; set; }
        public decimal PrecioCUPJul { get; set; }
        public decimal PrecioCUPAgo { get; set; }
        public decimal PrecioCUPSep { get; set; }
        public decimal PrecioCUPOct { get; set; }
        public decimal PrecioCUPNov { get; set; }
        public decimal PrecioCUPDic { get; set; }
        public decimal PrecioCUPDestinoEne { get => (Math.Round((PrecioCUPEne + TranspEne) / 0.024m, 0, MidpointRounding.AwayFromZero) * 0.024m); }
        public decimal PrecioCUPDestinoFeb { get => (Math.Round((PrecioCUPFeb + TranspFeb) / 0.024m, 0, MidpointRounding.AwayFromZero) * 0.024m); }
        public decimal PrecioCUPDestinoMar { get => (Math.Round((PrecioCUPMar + TranspMar) / 0.024m, 0, MidpointRounding.AwayFromZero) * 0.024m); }
        public decimal PrecioCUPDestinoAbr { get => (Math.Round((PrecioCUPAbr + TranspAbr) / 0.024m, 0, MidpointRounding.AwayFromZero) * 0.024m); }
        public decimal PrecioCUPDestinoMay { get => (Math.Round((PrecioCUPMay + TranspMay) / 0.024m, 0, MidpointRounding.AwayFromZero) * 0.024m); }
        public decimal PrecioCUPDestinoJun { get => (Math.Round((PrecioCUPJun + TranspJun) / 0.024m, 0, MidpointRounding.AwayFromZero) * 0.024m); }
        public decimal PrecioCUPDestinoJul { get => (Math.Round((PrecioCUPJul + TranspJul) / 0.024m, 0, MidpointRounding.AwayFromZero) * 0.024m); }
        public decimal PrecioCUPDestinoAgo { get => (Math.Round((PrecioCUPAgo + TranspAgo) / 0.024m, 0, MidpointRounding.AwayFromZero) * 0.024m); }
        public decimal PrecioCUPDestinoSep { get => (Math.Round((PrecioCUPSep + TranspSep) / 0.024m, 0, MidpointRounding.AwayFromZero) * 0.024m); }
        public decimal PrecioCUPDestinoOct { get => (Math.Round((PrecioCUPOct + TranspOct) / 0.024m, 0, MidpointRounding.AwayFromZero) * 0.024m); }
        public decimal PrecioCUPDestinoNov { get => (Math.Round((PrecioCUPNov + TranspNov) / 0.024m, 0, MidpointRounding.AwayFromZero) * 0.024m); }
        public decimal PrecioCUPDestinoDic { get => (Math.Round((PrecioCUPDic + TranspDic) / 0.024m, 0, MidpointRounding.AwayFromZero) * 0.024m); }
        public decimal PrecioCLEne { get; set; }
        public decimal PrecioCLFeb { get; set; }
        public decimal PrecioCLMar { get; set; }
        public decimal PrecioCLAbr { get; set; }
        public decimal PrecioCLMay { get; set; }
        public decimal PrecioCLJun { get; set; }
        public decimal PrecioCLJul { get; set; }
        public decimal PrecioCLAgo { get; set; }
        public decimal PrecioCLSep { get; set; }
        public decimal PrecioCLOct { get; set; }
        public decimal PrecioCLNov { get; set; }
        public decimal PrecioCLDic { get; set; }
        public decimal TranspEne { get; set; }
        public decimal TranspFeb { get; set; }
        public decimal TranspMar { get; set; }
        public decimal TranspAbr { get; set; }
        public decimal TranspMay { get; set; }
        public decimal TranspJun { get; set; }
        public decimal TranspJul { get; set; }
        public decimal TranspAgo { get; set; }
        public decimal TranspSep { get; set; }
        public decimal TranspOct { get; set; }
        public decimal TranspNov { get; set; }
        public decimal TranspDic { get; set; }
        public decimal TotalProductos { get; set; }
        public decimal Total(string total) // total --- Destino, Origen, CL
        {
            var elementos = new Dictionary<string, string>
            {
                {"PrecioCL","PrecioCL" },
                {"PrecioCUP","PrecioCUP" },
                {"Transp","PrecioCUPDestino" },
            };
            var resultado = 0m;

            var tipo = typeof(VMResumenContratacionAno);
            foreach (var mes in Constantes.mesesAbr)
            {
                resultado += Convert.ToDecimal(tipo.GetProperty(elementos[total] + mes).GetValue(this)) * Convert.ToDecimal(tipo.GetProperty(mes).GetValue(this));
            }
            return resultado;
        }
    }


    public class VMDesglosePagosRealizados
    {
        public int ano { get; set; }
        public decimal valor { get; set; }
    }

    public class VMPagosCLRealizados
    {
        public VMPagosCLRealizados()
        {
            desglose = new List<VMDesglosePagosRealizados>();
        }
        public List<VMDesglosePagosRealizados> desglose { get; set; }
        public int IDPagoCL { get; set; }
        public int anoPago { get; set; }
        public DateTime fechaPago { get; set; }
        public string Comentario { get; set; }
        public decimal valorTotal { get; set; }
    }

    public class VMDatosResumidosCL
    {
        public int IDCliente { get; set; }
        public string id_Cliente { get; set; }
        public string desc_Cliente { get; set; }
        public decimal ventasUSDAnteriores { get; set; }
        public decimal ventasUSDAno { get => facturacion.Sum(x => x.VentaCL); }
        public decimal ventasOrigenAno { get => facturacion.Sum(x => Math.Round(x.VentaOrigen / 24, 2)); }
        public decimal porcientoCLAno { get => ventasOrigenAno > 0 ? Math.Round(ventasUSDAno / ventasOrigenAno, 2) : 0; }
        public decimal pagosCLAnterioresSinIncluirAnoActual { get => pagosCl.Where(x => x.anoPago < DateTime.Now.Year).SelectMany(x => x.desglose).Where(x => x.ano < DateTime.Now.Year).Sum(x => x.valor); }
        public decimal pagosCLAnosAnteriores { get => pagosCl.SelectMany(x => x.desglose).Where(x => x.ano < DateTime.Now.Year).Sum(x => x.valor); }
        public decimal pagosCLAnoActual { get => pagosCl.SelectMany(x => x.desglose).Where(x => x.ano == DateTime.Now.Year).Sum(x => x.valor); }
        public decimal pagosCLEsteAnoParaAnosAnteriores { get => pagosCl.Where(x => x.anoPago == DateTime.Now.Year).SelectMany(x => x.desglose).Where(x => x.ano < DateTime.Now.Year).Sum(x => x.valor); }
        public decimal deudasInicioAno { get => (ventasUSDAnteriores - pagosCLAnosAnteriores + pagosCLEsteAnoParaAnosAnteriores) > 0 ? (ventasUSDAnteriores - pagosCLAnosAnteriores + pagosCLEsteAnoParaAnosAnteriores) : 0; }
        public decimal deudasAnterioresVigentes { get => (ventasUSDAnteriores - pagosCLAnosAnteriores) > 0 ? (ventasUSDAnteriores - pagosCLAnosAnteriores) : 0; }
        public decimal anticiposInicioAno { get => (ventasUSDAnteriores - pagosCLAnosAnteriores + pagosCLEsteAnoParaAnosAnteriores) < 0 ? Math.Abs((ventasUSDAnteriores - pagosCLAnosAnteriores + pagosCLEsteAnoParaAnosAnteriores)) : 0; }
        public decimal anticiposAnterioresVigentes { get => (ventasUSDAnteriores - pagosCLAnosAnteriores) < 0 ? Math.Abs((ventasUSDAnteriores - pagosCLAnosAnteriores)) : 0; }
        public decimal deudasAno { get => ventasUSDAno - pagosCLAnoActual > 0 ? ventasUSDAno - pagosCLAnoActual : 0; }
        public decimal anticiposAno { get => ventasUSDAno - pagosCLAnoActual < 0 ? Math.Abs(ventasUSDAno - pagosCLAnoActual) : 0; }
        public decimal deudasActuales { get => deudasAnterioresVigentes + deudasAno; }
        public decimal anticiposActuales { get => anticiposAnterioresVigentes + anticiposAno; }
        public List<VMPagosCLRealizados> pagosCl { get; set; }
        public List<VMDatosDetalladosFacturacion> facturacion { get; set; }
        public VMDatosResumidosCL()
        {
            pagosCl = new List<VMPagosCLRealizados>();
            facturacion = new List<VMDatosDetalladosFacturacion>();
        }
    }


    public class VMDatosImportadosCL
    {
        public int IDPagoCL { get; set; }
        public int IDCliente { get; set; }
        public string id_Cliente { get; set; }
        public string desc_Cliente { get; set; }
        public int anoPago { get; set; }
        public DateTime fechaPago { get; set; }
        public decimal valorTotal { get; set; }
        public string comentario { get; set; }
        public int anoDestino { get; set; }
        public decimal valorDestinado { get; set; }
        public decimal ventasUSDFinal2022 { get; set; }

    }

    public class VMDatosContratacionYVentasPorProductos : IDatosProductos
    {
        public string idProducto { get; set; }
        public string descProducto { get; set; }
        public VMDatosContratacionYVentasPorProductos()
        {
            contratacion = new List<VMDatosContratacion>();
            ventas = new List<VMDatosVentas>();
        }
        public List<VMDatosContratacion> contratacion { get; set; }
        public List<VMDatosVentas> ventas { get; set; }
        public decimal cantidadTotalVendida { get => ventas.Sum(x => x.cantidadVendida); }
        public decimal importeTotalVendido { get => ventas.Sum(x => x.importeVendido); }
        public decimal cantidadTotalContratada { get => contratacion.Sum(x => x.contratado); }
        public decimal importeTotalContratadoDestino { get => contratacion.Sum(x => x.importeContratadoDestino); }
        public decimal importeTotalContratadoOrigen { get => contratacion.Sum(x => x.importeContratadoOrigen); }
        public decimal importeTotalContratadoCL { get => contratacion.Sum(x => x.importeContratadoCL); }
        public decimal importeTotalVendidoCL { get => ventas.Sum(x => x.importeCL); }
        public VMAnalisisCumplimiento ventasUnidades { get => new VMAnalisisCumplimiento { plan = cantidadTotalContratada, real = cantidadTotalVendida }; }
        public VMAnalisisCumplimiento ventasImporteOrigen { get => new VMAnalisisCumplimiento { plan = importeTotalContratadoOrigen, real = importeTotalVendido }; }
        public VMAnalisisCumplimiento ventasImporteDestino { get => new VMAnalisisCumplimiento { plan = importeTotalContratadoDestino, real = importeTotalVendido }; }
        public VMAnalisisCumplimiento ventasCL { get => new VMAnalisisCumplimiento { plan = importeTotalContratadoCL, real = importeTotalVendidoCL }; }

    }

    public class VMDatosContratacionYVentasCliente : IDatosCliente
    {
        public List<VMDatosContratacionYVentasPorProductos> productos { get; set; }
        public string idCliente { get; set; }
        public string descCliente { get; set; }
        public string organismo { get; set; }
        public decimal VentaAntUSD { get; set; }
        public decimal DeudaAntUSD { get; set; }
        public decimal DeudaAntUSDVigente { get; set; }
        public decimal AnticipoAntUSD { get; set; }
        public decimal AnticipoAntUSDVigente { get; set; }
        public decimal PagosCLAno { get; set; }
        public decimal PagosCLAnoAnteriores { get; set; }
        public decimal PagosDeEsteAnoParaAnoAnteriores { get; set; }
        public decimal cantidadTotalContratada { get => productos.Sum(x => x.cantidadTotalContratada); }
        public decimal importeTotalContratadoOrigen { get => productos.Sum(x => x.importeTotalContratadoOrigen); }
        public decimal importeTotalContratadoDestino { get => productos.Sum(x => x.importeTotalContratadoDestino); }
        public decimal importeTotalContratadoCL { get => productos.Sum(x => x.importeTotalContratadoCL); }
        public decimal cantidadTotalVendida { get => productos.Sum(x => x.cantidadTotalVendida); }
        public decimal importeTotalVendido { get => productos.Sum(x => x.importeTotalVendido); }
        public decimal importeTotalVendidoCL { get => productos.Sum(x => x.importeTotalVendidoCL); }
        public VMDatosContratacionYVentasCliente()
        {
            productos = new List<VMDatosContratacionYVentasPorProductos>();
        }

        public VMAnalisisCumplimiento ventasUnidades { get => new VMAnalisisCumplimiento { plan = cantidadTotalContratada, real = cantidadTotalVendida }; }
        public VMAnalisisCumplimiento ventasImporteOrigen { get => new VMAnalisisCumplimiento { plan = importeTotalContratadoOrigen, real = importeTotalVendido }; }
        public VMAnalisisCumplimiento ventasImporteDestino { get => new VMAnalisisCumplimiento { plan = importeTotalContratadoDestino, real = importeTotalVendido }; }
        public VMAnalisisCumplimiento ventasCL { get => new VMAnalisisCumplimiento { plan = importeTotalContratadoCL, real = importeTotalVendidoCL }; }


    }


    public class VMDatosGeneralesVentasContratacion
    {
        public string FabricaContratacion { get; set; }
        public string FabricaVenta { get; set; }
        public string IdCliente { get; set; }
        public string DescCliente { get; set; }
        public string Organismo { get; set; }
        public string IdProducto { get; set; }
        public string DescProducto { get; set; }
        public int IdProductoContrato { get; set; }
        public int MesVenta { get; set; }
        public decimal CantidadVendida { get; set; }
        public decimal ImporteVendido { get; set; }
        public decimal EneroContratado { get; set; }
        public decimal FebreroContratado { get; set; }
        public decimal MarzoContratado { get; set; }
        public decimal AbrilContratado { get; set; }
        public decimal MayoContratado { get; set; }
        public decimal JunioContratado { get; set; }
        public decimal JulioContratado { get; set; }
        public decimal AgostoContratado { get; set; }
        public decimal SeptiembreContratado { get; set; }
        public decimal OctubreContratado { get; set; }
        public decimal NoviembreContratado { get; set; }
        public decimal DiciembreContratado { get; set; }
        public decimal TotalProductos { get; set; }
        public decimal PrecioCL { get; set; }
        public decimal VentaCL { get; set; }
        public decimal PrecioOrigen { get; set; }
        public decimal VentaOrigen { get; set; }
        public decimal VentaAntUSD { get; set; }
        public decimal DeudaAntUSD { get; set; }
        public decimal DeudaAntUSDVigente { get; set; }
        public decimal AnticipoAntUSD { get; set; }
        public decimal AnticipoAntUSDVigente { get; set; }
        public decimal PagosCLAno { get; set; }
        public decimal PagosCLAnoAnteriores { get; set; }
        public decimal PagosDeEsteAnoParaAnoAnteriores { get; set; }
    }


    public class VMDatosPrecios
    {
        public decimal precioCL { get; set; }
        public decimal precioCUP { get; set; }
        public decimal valorTransportacion { get; set; }
        public int cargaMinimaCamion { get; set; }
        public int fleteCamionDesdeHabana { get; set; }
        public int fleteCamionDesdeSantiago { get; set; }
    }

    public class VMPortesMaximos
    {
        public decimal porteMaximoDesdeHabana { get; set; }
        public decimal porteMaximoDesdeSantiago { get; set; }
    }

    public class VMCargasMinimas
    {
        public int CargaMinimaCamion { get; set; }
        public int CargaMinimaRastra { get; set; }

    }
    public class VMDatosDetalladosFacturacion
    {
        public DateTime Fecha_Confirmacion { get; set; }
        public int IdProductoPorContrato { get; set; }
        public int Id_Factura { get; set; }
        public int MesFactura { get; set; }
        public string Id_Cliente { get; set; }
        public string Desc_Cliente { get; set; }
        public string Fabrica { get; set; }
        public string Id_Producto { get; set; }
        public string Desc_Producto { get; set; }
        public decimal PrecioCL { get; set; }
        public decimal VentaCL { get => PrecioCL * CantidadVendida; }
        public decimal PrecioOrigen { get; set; }
        public decimal VentaOrigen { get => PrecioOrigen * CantidadVendida; }
        public decimal CantidadVendida { get; set; }
        public decimal ImporteVendido { get; set; }
        public decimal PrecioCUP { get => CantidadVendida > 0 ? Math.Round(ImporteVendido / CantidadVendida, 4) : 0; }
    }
}

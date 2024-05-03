using Dapper;
using System.Data.SqlClient;

namespace ContratosCore
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


    public static class OperacionesContratos
    {
        private static string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
        public static VMDatosPrecios obtenerDatosPrecioFactura(int idProductoContrato, DateTime fecha, int municipio)
        {
            var resultado = new VMDatosPrecios();
            var cadConexion = new ConexionSql("AccesoDatos");
            using (var conn = new SqlConnection(cadConexion.obtener()))
                resultado = conn.QueryFirstOrDefault<VMDatosPrecios>("precioContratado", new { idProductoContrato, fecha, municipio }, commandType: System.Data.CommandType.StoredProcedure);
            return resultado;
        }

        public static VMPortesMaximos obtenerPorteMaximo(string destino, string IdProducto)
        {
            var resultado = new VMPortesMaximos();
            var cadConexion = new ConexionSql("AccesoDatos");
            using (var conn = new SqlConnection(cadConexion.obtener()))
                resultado = conn.QueryFirstOrDefault<VMPortesMaximos>("portesMaximos", new { destino, IdProducto }, commandType: System.Data.CommandType.StoredProcedure);
            return resultado;

        }

        public static VMCargasMinimas obtenerCargasMinimas(string idProducto)
        {
            var resultado = new VMCargasMinimas();
            var cadConexion = new ConexionSql("AccesoDatos");
            using (var conn = new SqlConnection(cadConexion.obtener()))
                resultado = conn.QueryFirstOrDefault<VMCargasMinimas>("cargasMinimasProducto", new { idProducto }, commandType: System.Data.CommandType.StoredProcedure);


            return resultado;

        }

        public static List<VMDatosContratacionYVentasCliente> ventasClientesParcial(List<VMDatosGeneralesVentasContratacion> datosIntermedios, DateTime fechaI, DateTime fechaF)
        {
            var resultado = new List<VMDatosContratacionYVentasCliente>();
            var mesI = fechaI.Month;
            var mesF = fechaF.Month;
            foreach (var datos in datosIntermedios)
            {
                var registroResultado = resultado.FirstOrDefault(x => x.idCliente == datos.IdCliente.Trim());
                if (registroResultado == null)
                {
                    registroResultado = new VMDatosContratacionYVentasCliente
                    {

                        idCliente = datos.IdCliente.Trim(),
                        descCliente = datos.DescCliente,
                        organismo = datos.Organismo,
                        VentaAntUSD = datos.VentaAntUSD,
                        DeudaAntUSD = datos.DeudaAntUSD,
                        DeudaAntUSDVigente = datos.DeudaAntUSDVigente,
                        AnticipoAntUSD = datos.AnticipoAntUSD,
                        AnticipoAntUSDVigente = datos.AnticipoAntUSDVigente,
                        PagosCLAno = datos.PagosCLAno,
                        PagosCLAnoAnteriores = datos.PagosCLAno,
                        PagosDeEsteAnoParaAnoAnteriores = datos.PagosDeEsteAnoParaAnoAnteriores,

                    };
                    resultado.Add(registroResultado);
                }
                var producto = registroResultado.productos.FirstOrDefault(x => x.idProducto == datos.IdProducto.Trim());
                if (producto == null)
                    registroResultado.productos.Add(producto = new VMDatosContratacionYVentasPorProductos
                    {
                        idProducto = datos.IdProducto.Trim(),
                        descProducto = datos.DescProducto
                    });
                //SI NO EXISTE LA CONTRATACION LA AGREGO
                var desgloseProducto = producto;

                if (datos.FabricaContratacion != null)
                {
                    for (int i = mesI - 1; i < mesF; i++)
                    {
                        var fechaAnalisis = new DateTime(fechaF.Year, i + 1, 2);
                        var propiedad = $"{meses[i]}Contratado";
                        var contratoMes = Convert.ToDecimal(typeof(VMDatosGeneralesVentasContratacion).GetProperty(propiedad).GetValue(datos));
                        var datosPrecios = OperacionesContratos.obtenerDatosPrecioFactura(datos.IdProductoContrato, fechaAnalisis, 1);
                        var contratacionExiste = desgloseProducto.contratacion.FirstOrDefault(x => x.fabricaContratacion == datos.FabricaContratacion && x.mesContratacion == i + 1 && x.idProductoContrato == datos.IdProductoContrato);
                        if (contratacionExiste == null)
                            desgloseProducto.contratacion.Add(new VMDatosContratacion
                            {
                                idProductoContrato = datos.IdProductoContrato,
                                mesContratacion = i + 1,
                                contratado = contratoMes,
                                precioCL = datosPrecios.precioCL,
                                precioCUP = datosPrecios.precioCUP,
                                valorTransportacion = datosPrecios.valorTransportacion,
                                fabricaContratacion = datos.FabricaContratacion,
                            });
                        else if (contratacionExiste.contratado == 0)
                        {
                            contratacionExiste.contratado = contratoMes;
                            contratacionExiste.precioCL = datosPrecios.precioCL;
                            contratacionExiste.precioCUP = datosPrecios.precioCUP;
                            contratacionExiste.valorTransportacion = datosPrecios.valorTransportacion;

                        }
                    }
                }
                if (datos.FabricaVenta != null && !desgloseProducto.ventas.Any(x => x.mesVenta == datos.MesVenta && x.fabricaVenta == datos.FabricaVenta))
                {
                    var valorVentaAgregar = new VMDatosVentas
                    {
                        mesVenta = datos.MesVenta,
                        cantidadVendida = datos.CantidadVendida,
                        fabricaVenta = datos.FabricaVenta,
                        importeVendido = datos.ImporteVendido,
                        ventaCL = datos.VentaCL,
                    };
                    desgloseProducto.ventas.Add(valorVentaAgregar);
                }

            }
            return resultado;

        }

        public static List<VMDatosContratacionYVentasCliente> ventasPorCliente(DateTime fechaI, DateTime fechaF, string idCliente = null)
        {
            var resultado = new List<VMDatosContratacionYVentasCliente>();
            var cadConexion = new ConexionSql("AccesoDatos");
            var fechaIntermedia = fechaI;
            fechaI = fechaI > fechaF ? fechaF : fechaI;
            fechaF = fechaF < fechaIntermedia ? fechaIntermedia : fechaF;
            var mesI = fechaI.Month;
            var mesF = fechaF.Month;

            using (var conn = new SqlConnection(cadConexion.obtener()))
            {
                var datosIntermedios = conn.Query<VMDatosGeneralesVentasContratacion>("ventasClientes", new { fechaI, fechaF, idCliente }, commandType: System.Data.CommandType.StoredProcedure).AsList();
                var datosReales = datosIntermedios.GroupBy(x => x.IdCliente).OrderBy(x => x.Key);
                var cantidadDivisiones = 4;
                var totalClientes = datosReales.Count();
                var partesPorDivision = datosReales.Count() / cantidadDivisiones;
                if (partesPorDivision > 5)
                {
                    var resultadosParciales = new List<VMDatosContratacionYVentasCliente>[]
                    {
                        new List<VMDatosContratacionYVentasCliente>(),
                        new List<VMDatosContratacionYVentasCliente>(),
                        new List<VMDatosContratacionYVentasCliente>(),
                        new List<VMDatosContratacionYVentasCliente>()
                    };
                    var tareas = new Task[4];
                    var salto = partesPorDivision;
                    tareas[0] = Task.Run(() => resultadosParciales[0] =
                          ventasClientesParcial(datosReales.Take(partesPorDivision).SelectMany(x => x).ToList(), fechaI, fechaF));
                    tareas[1] = Task.Run(() => resultadosParciales[1] =
                          ventasClientesParcial(datosReales.Skip(salto).Take(partesPorDivision).SelectMany(x => x).ToList(), fechaI, fechaF));
                    tareas[2] = Task.Run(() => resultadosParciales[2] =
                          ventasClientesParcial(datosReales.Skip(2 * salto).Take(partesPorDivision).SelectMany(x => x).ToList(), fechaI, fechaF));
                    tareas[3] = Task.Run(() => resultadosParciales[3] =
                          ventasClientesParcial(datosReales.Skip(3 * salto).SelectMany(x => x).ToList(), fechaI, fechaF));
                    Task.WaitAll(tareas);
                    for (int i = 0; i < 4; i++)
                    {
                        resultado.AddRange(resultadosParciales[i]);
                    }
                }
                else
                    resultado = ventasClientesParcial(datosIntermedios, fechaI, fechaF);
            }

            return resultado;

        }

        public static List<VMDatosDetalladosFacturacion> obtenerVentasTotales(DateTime? fechaI, DateTime fechaF, string idCliente = null)
        {
            var resultado = new List<VMDatosDetalladosFacturacion>();

            var cadConexion = new ConexionSql("AccesoDatos");
            using (var conn = new SqlConnection(cadConexion.obtener()))
                resultado = conn.Query<VMDatosDetalladosFacturacion>("ventasTotalesCliente", new { idCliente, fechaI, fechaF }, commandType: System.Data.CommandType.StoredProcedure).AsList();
            return resultado;
        }

        public static List<VMDatosDetalladosFacturacion> obtenerDetallesFacturacion(string idCliente, DateTime fechaF)
        {
            return obtenerVentasTotales(null, fechaF, idCliente);
        }
        public static List<VMDatosResumidosCL> obtenerDatosCLHastaFecha(DateTime fechaF, string idCliente = null)
        {
            var anoActual = DateTime.Now.Year;
            var fechaInicioAno = new DateTime(anoActual, 1, 1);
            var resultado = new List<VMDatosResumidosCL>();
            var cadConexion = new ConexionSql("AccesoDatos");
            using (var conn = new SqlConnection(cadConexion.obtener()))
            {
                var datosCL = conn.Query<VMDatosImportadosCL>("datosCL", new { idCliente, fechaF }, commandType: System.Data.CommandType.StoredProcedure).AsList();
                foreach (var dato in datosCL.GroupBy(x => x.IDCliente))
                {
                    var generales = dato.FirstOrDefault();
                    var ventasAnteriores = 0m;
                    for (int ano = 2023; ano < anoActual; ano++)
                    {
                        var fechaI = new DateTime(ano, 1, 1);
                        var fechaU = new DateTime(ano, 12, 31);
                        var ventasAnuales = obtenerVentasTotales(fechaI, fechaU, generales.id_Cliente);
                        ventasAnteriores += ventasAnuales.Sum(x => x.VentaCL);
                    }
                    var nuevoElemento = new VMDatosResumidosCL
                    {
                        IDCliente = generales.IDCliente,
                        id_Cliente = generales.id_Cliente,
                        desc_Cliente = generales.desc_Cliente,
                        ventasUSDAnteriores = generales.ventasUSDFinal2022 + ventasAnteriores,
                        facturacion = obtenerDetallesFacturacion(generales.id_Cliente, fechaF)
                    };
                    foreach (var pag in dato.GroupBy(x => x.IDPagoCL))
                    {
                        var datosPagos = pag.FirstOrDefault();
                        var nuevoPago = new VMPagosCLRealizados
                        {
                            IDPagoCL = datosPagos.IDPagoCL,
                            anoPago = datosPagos.anoPago,
                            fechaPago = datosPagos.fechaPago,
                            valorTotal = datosPagos.valorTotal,
                            Comentario = datosPagos.comentario
                        };
                        foreach (var desg in pag)
                            nuevoPago.desglose.Add(new VMDesglosePagosRealizados
                            {
                                ano = desg.anoDestino,
                                valor = desg.valorDestinado
                            });
                        nuevoElemento.pagosCl.Add(nuevoPago);
                    }
                    resultado.Add(nuevoElemento);
                }
            }

            return resultado;
        }

        public static List<VMResumenContratacionAno> resumenContratacion(int? idCliente = null, DateTime? fechaI = null, DateTime? fechaF = null)
        {
            var resultado = new List<VMResumenContratacionAno>();

            var cadConexion = new ConexionSql("AccesoDatos");
            using (var conn = new SqlConnection(cadConexion.obtener()))
                resultado = conn.Query<VMResumenContratacionAno>("contratacionAno", new { idCliente, fechaI, fechaF }, commandType: System.Data.CommandType.StoredProcedure).AsList();
            return resultado;

        }
        public static List<VMResumenContratacionAno> resumenContratacion(string? idCadena = null, DateTime? fechaI = null, DateTime? fechaF = null)
        {


            int? idNumerico = null;
            if (idCadena != null)
            {
                var cadConexion = new ConexionSql("AccesoDatos");
                using (var conn = new SqlConnection(cadConexion.obtener()))
                    idNumerico = conn.Query<Int32>("obtenerIdCliente", new { idCadena }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
            return resumenContratacion( idNumerico, fechaI, fechaF );

        }


    }
}
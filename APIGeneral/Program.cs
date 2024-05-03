

using ContratosCore;
using CumplimientoVentas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using UtilesGenerales;
var misOrigenesPermitidos = "_misOrigenesPermitidos";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.WriteIndented = true;
    options.SerializerOptions.IncludeFields = true;


});


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: misOrigenesPermitidos,
                      policy =>
                      {
                          policy.WithOrigins("*").SetIsOriginAllowedToAllowWildcardSubdomains();
                      });
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(misOrigenesPermitidos);


Configurador.mapeandoContratos(app);
Configurador.mapeandoFichasTecnicas(app);
Configurador.mapeandoCV(app);
Configurador.mapeandoCPCU(app);
Configurador.mapeandoTransPortacion(app);
Configurador.mapeandoFirmasDigitales(app);

app.Run();

public static class Configurador
{
    public static void mapeandoContratos(WebApplication app)
    {
        app.MapGet("/ventasporcliente", (string? idCliente) =>
        {
            var fechaI = new DateTime(DateTime.Now.Year, 1, 1);
            var fechaF = DateTime.Now;
            return OperacionesContratos.ventasPorCliente(fechaI, fechaF, idCliente);
        });

        app.MapGet("/contratacionporcliente", (string? idCliente) =>
        {
            var fechaI = new DateTime(DateTime.Now.Year, 1, 1);
            var fechaF = DateTime.Now;
            return OperacionesContratos.resumenContratacion(idCliente);
        });

    }
    public static void mapeandoFichasTecnicas(WebApplication app)
    {
        app.MapGet("/fichasTecnicas/obtener", (string ft) =>
        {
            return FichasTecnicas.obtenerFicheroFichaTecnica(ft);
        });

        app.MapGet("/fichasTecnicas/obtenerAPartirNombre", (string ft) =>
        {
            return FichasTecnicas.obtenerFicheroFichaTecnica(ft, null, true);
        });

        app.MapGet("/fichasTecnicas/listado", (string rutas) =>
        {
            return FichasTecnicas.obtenerListadoFichasTecnicas(rutas: new string[] { rutas });
        });

    }
    public static void mapeandoCV(WebApplication app)
    {
        app.MapGet("/cv/listado", () =>
        {
            return DesgloseFicheros.listado();
        });


        app.MapGet("/aempresarial/listado", (string tipo) =>
        {
            return DesgloseFicheros.listado(@"c:/PaginasWeb/AsesorEmpresarial", tipo);
        });
    }
    public static void mapeandoCPCU(WebApplication app)
    {
        app.MapGet("/cpcu/empiezapor", (string comienzaPor) =>
        {
            return OperacionesCPCU.obtenerCodigosRelacionados(comienzaPor);
        });
        app.MapGet("/cpcu/productosCodificados", (string comienzaPor) =>
         {
             var retorno = new VMDatosResumenPorFabrica
             {
                 codigos = OperacionesCPCU.obtenerProductosCodificados(comienzaPor),
             };

             return retorno;
         });
    }
    public static void mapeandoTransPortacion(WebApplication app)
    {
        app.MapGet("/transportacion/materiasprimas", (DateTime fechaI, DateTime fechaF) =>
        {
            return Transportacion.gastosTransportacionMateriaPrima(fechaI, fechaF);
        });

    }
    public static void mapeandoFirmasDigitales(WebApplication app)
    {
        app.MapPost("/firmasdigitales/cambiarcontrasena", (HttpRequest peticion) =>
        {
            string ficheroPK12 = "", contrasenaVieja = "", contrasenaNueva = "";
            if (peticion.Form.ContainsKey("contrasenaVieja"))
                contrasenaVieja = peticion.Form["contrasenaVieja"];
            if (peticion.Form.ContainsKey("contrasenaNueva"))
                contrasenaNueva = peticion.Form["contrasenaNueva"];
            if (peticion.Form.ContainsKey("ficheroPK12"))
                ficheroPK12 = peticion.Form["ficheroPK12"];

            return FirmasDig.FirmasDigitales.cambiarContrasena(ficheroPK12, contrasenaVieja, contrasenaNueva);
        });

    }
}

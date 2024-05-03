using Datos;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using System.Globalization;

namespace APICompacto.Controllers
{
    [ApiController]
    [Route("/Api/Cliente")]
    public class ClienteController
    {
        [HttpGet]
        public async Task <ActionResult<List<DatosClientes>>> obtenerDatosClientes( string idCliente )
        {
            var datosClientes = new DClientes();
            var resultado = await datosClientes.obtenerDatosClientes( idCliente );
            return resultado;
        }
    }
}

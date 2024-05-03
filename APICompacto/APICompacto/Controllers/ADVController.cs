using APICompacto.Datos;
using Datos;
using Microsoft.AspNetCore.Mvc;
using Modelos;

namespace APICompacto.Controllers
{
    [ApiController]
    [Route("/Api/ADV")]
    public class ADVController
    {
        [HttpGet]
        public async Task<ActionResult<List<DatosClientes>>> obtenerFacturas()
        {
            var datosERP = new DatosERPADV();
            var resultado = await datosERP.DatosAlbaran();
            return resultado;
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICompacto.Controllers
{
    [Route("api/FT")]
    [ApiController]
    public class FichasTecnicasController 
    {
        [HttpGet]
        public async Task<List<string>> obtenerNombresFichasTecnicas()
        {
            var resultado = new List<string>();
            resultado = await Task.Run(()=>UtilesGenerales.FichasTecnicas.obtenerListadoFichasTecnicas());
            return resultado;

        }
    }
}

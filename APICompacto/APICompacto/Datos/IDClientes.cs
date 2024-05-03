using Modelos;

namespace Datos
{
    public interface IDClientes
    {
        Task<List<DatosClientes>> obtenerDatosClientes(string idCliente);
    }
}
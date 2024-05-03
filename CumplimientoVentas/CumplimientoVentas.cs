namespace CumplimientoVentas
{
    public class VMListadoCV
    {
        public DateTime _fecha;
        public string fecha { get=> _fecha.ToString("yyyy-MM-dd"); }
        public string descripcion { get=> _fecha.ToLongDateString(); }
        public string? nombreFichero { get; set; }

    }
    

    public static class DesgloseFicheros
    {
        public static List<VMListadoCV> listado( string ruta = @"c:/PaginasWeb/PartesDiarios", string tipo = "CV")
        {
            var resultado = new List<VMListadoCV>();
            var resultadoPreL = Directory.GetFiles(ruta).ToList().Where(x => Path.GetFileName(x).StartsWith(tipo));

            foreach (var res in resultadoPreL) {
                var _nombreFichero = Path.GetFileName(res);
                var dividido = res.Split('_','.');
                var _fecha = new DateTime(Convert.ToInt32(dividido[3]), Convert.ToInt32(dividido[2]), Convert.ToInt32(dividido[1])).Date;
                resultado.Add(new VMListadoCV
                {
                    _fecha = _fecha,
                    nombreFichero = _nombreFichero
                });
            }

            return resultado.OrderByDescending(x => x._fecha).ToList();
        }
    }
}
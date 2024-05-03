using ContratosCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilesGenerales
{
    public static class FichasTecnicas
    {
        public const string nombreComputadora = "192.168.5.35";
        public const string usuario = @"compacto\yoe";
        public const string contrasena = "Yapo@720922";
        private static NetworkShareAccesser conexion = NetworkShareAccesser.Access(nombreComputadora, usuario, contrasena);
        public static string[] rutaFichasTecnicas = new string[]
        {
            @"\\192.168.5.35\publica\OPERACIONES\Desarrollo\FICHAS TECNICAS DE PRODUCTOS H.S.P"
        };
        /// <summary>
        /// A partir de la Ficha tecnica del producto se busca en la ruta el fichero y se devuelve este en Base64
        /// ,luego se hace la reconversion segun el tipo de aplicacion que se quiera utilizar!!!!!
        /// </summary>
        /// <param name="ft"></param>
        /// <param name="rutas"></param>
        /// <returns></returns>
        public static VMDatosFicherosFT obtenerFicheroFichaTecnica(string ft, string[] rutas = null, bool nombreCompletoFichaTecnica = false)
        {
            var res = new VMDatosFicherosFT
            {
                nombreFichero = "",
                rutaFichero = "",
                base64 = ""
            };
                rutas = rutas ?? rutaFichasTecnicas;
                foreach (var ruta in rutas)
                {
                    string[] ficheros = null;
                    if (!nombreCompletoFichaTecnica)
                        ficheros = Directory.GetFiles(ruta, $"FT???*{ft} *.pdf");
                    else
                        ficheros = Directory.GetFiles(ruta, $"{ft}.pdf");
                    if (ficheros.Length > 0)
                    {
                        var fich = new FileStream(ficheros[0], FileMode.Open);
                        var tamanoFichero = Convert.ToInt32(fich.Length);
                        var datosFichero = new byte[tamanoFichero];
                        var datosLeidos = fich.Read(datosFichero, 0, tamanoFichero);
                        fich.Close();
                        if (datosLeidos != tamanoFichero) return res;
                        res.nombreFichero = Path.GetFileName(ficheros[0]);
                        res.rutaFichero = ruta;
                        res.base64 = Convert.ToBase64String(datosFichero);
                        break;
                    }
                    var directorios = Directory.GetDirectories(ruta);
                    foreach (var directorio in directorios)
                    {
                        res = obtenerFicheroFichaTecnica(ft, new string[] { directorio },nombreCompletoFichaTecnica);
                        if (res.base64 != "") break;
                    }

                }
            return res;
        }

        /// <summary>
        /// Obtiene el nombre de todos los ficheros que existen en el contenedor de fichas técnicas
        /// </summary>
        /// <param name="rutas"></param>
        /// <returns></returns>
        public static List<VMDatosFicherosFT> obtenerListadoFichasTecnicas(string[] rutas = null)
        {
            var resultado = new List<VMDatosFicherosFT>();

                rutas = rutas ?? rutaFichasTecnicas;
                foreach (var ruta in rutas)
                {
                    var resultadoPreL = Directory.GetFiles(ruta, $"FT*.pdf").ToList();
                    foreach (var res in resultadoPreL)
                        resultado.Add(new VMDatosFicherosFT
                        {
                            rutaFichero = ruta,
                            nombreFichero = Path.GetFileNameWithoutExtension(res)
                        });
                    var directorios = Directory.GetDirectories(ruta);
                    foreach (var directorio in directorios)
                        resultado.AddRange(obtenerListadoFichasTecnicas(new string[] { directorio }));

                }
            return resultado;

        }
    }
}

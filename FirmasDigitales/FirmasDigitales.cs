using System.Security.Cryptography.X509Certificates;

namespace FirmasDig
{
    public static class FirmasDigitales
    {
        public static string cambiarContrasena(string ficheroPK12, string contrasenaVieja, string contrasenaNueva)
        {
            var resultado = "";
            //byte[] result = Convert.FromBase64String("");
            //var datosFichero = Convert.FromBase64String( ficheroPK12 );
            //var caminoFichero = Path.Combine(Environment.GetFolderPath( Environment.SpecialFolder.LocalApplicationData ),"pk12Actual.p12");
            ////// Crear o sobrescribir el archivo binario
            //using (FileStream fileStream = new FileStream(caminoFichero, FileMode.Create))
            //{
            //   // Escribir los bytes en el archivo
            //    ficheroPK12.CopyTo(fileStream);
            //}

            try
            {
                // Cargar el archivo .p12 con la contraseña actual
               var certificate = new X509Certificate2(ficheroPK12, contrasenaVieja, X509KeyStorageFlags.Exportable);
               resultado = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
               resultado = Path.Combine(resultado, "pk12FicheroCambiado.p12");
                // Exportar el certificado con la nueva contraseña
                byte[] exportedData = certificate.Export(X509ContentType.Pkcs12, contrasenaNueva);
                //result = exportedData;
                //Guardar el certificado exportado con la nueva contraseña
                System.IO.File.WriteAllBytes(resultado, exportedData);

            }
            catch (Exception ex)
            {
            }
            return resultado;
            //return Convert.ToBase64String( result );//Esto es cuando quisiera retornar la cadena Base 64 del fichero de salida.
        }
    }
}

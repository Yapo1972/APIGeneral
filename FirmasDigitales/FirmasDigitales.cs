using System.Security.Cryptography.X509Certificates;

namespace FirmasDig
{
    public static class FirmasDigitales
    {
        public static string cambiarContrasena(string ficheroPK12, string contrasenaVieja, string contrasenaNueva)
        {
            byte[] result = Convert.FromBase64String("");
            var datosFichero = Convert.FromBase64String( ficheroPK12 );
            var caminoFichero = Path.Combine(Environment.GetFolderPath( Environment.SpecialFolder.LocalApplicationData ),"pk12Actual.p12");
            // Crear o sobrescribir el archivo binario
            using (FileStream fileStream = new FileStream(caminoFichero, FileMode.Create))
            {
                // Escribir los bytes en el archivo
                fileStream.Write(datosFichero, 0, datosFichero.Length);
            }

            try
            {
                // Cargar el archivo .p12 con la contraseña actual
                var certificate = new X509Certificate2(caminoFichero, contrasenaVieja, X509KeyStorageFlags.Exportable);
                //var ficheroSalida = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                //ficheroSalida = Path.Combine(ficheroSalida, "pk12FicheroCambiado.p12");
                // Exportar el certificado con la nueva contraseña
                byte[] exportedData = certificate.Export(X509ContentType.Pkcs12, contrasenaNueva);
                result = exportedData;
                // Guardar el certificado exportado con la nueva contraseña
                //System.IO.File.WriteAllBytes(ficheroSalida, exportedData);

            }
            catch (Exception ex)
            {
            }
            return Convert.ToBase64String( result );
        }
    }
}

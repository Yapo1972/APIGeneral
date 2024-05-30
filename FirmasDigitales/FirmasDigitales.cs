using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
//using System.Security.Cryptography.X509Certificates;

namespace FirmasDig
{
    public static class FirmasDigitales
    {
        //public static string cambiarContrasena(string ficheroPK12, string contrasenaVieja, string contrasenaNueva)
        //{
        //    var resultado = "";
        //    //byte[] result = Convert.FromBase64String("");
        //    //var datosFichero = Convert.FromBase64String( ficheroPK12 );
        //    //var caminoFichero = Path.Combine(Environment.GetFolderPath( Environment.SpecialFolder.LocalApplicationData ),"pk12Actual.p12");
        //    ////// Crear o sobrescribir el archivo binario
        //    //using (FileStream fileStream = new FileStream(caminoFichero, FileMode.Create))
        //    //{
        //    //   // Escribir los bytes en el archivo
        //    //    ficheroPK12.CopyTo(fileStream);
        //    //}

        //    try
        //    {
        //        // Cargar el archivo .p12 con la contraseña actual
        //       var certificate = new X509Certificate2(ficheroPK12, contrasenaVieja, X509KeyStorageFlags.Exportable);
        //       resultado = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        //       resultado = Path.Combine(resultado, "pk12FicheroCambiado.p12");
        //        // Exportar el certificado con la nueva contraseña
        //        byte[] exportedData = certificate.Export(X509ContentType.Pkcs12, contrasenaNueva);
        //        //result = exportedData;
        //        //Guardar el certificado exportado con la nueva contraseña
        //        System.IO.File.WriteAllBytes(resultado, exportedData);

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return resultado;
        //    //return Convert.ToBase64String( result );//Esto es cuando quisiera retornar la cadena Base 64 del fichero de salida.
        //}

        public static string cambiarContrasena(string ficheroPK12, string contrasenaVieja, string contrasenaNueva)
        {
            // Carga el archivo PKCS12
            using (FileStream fs = new FileStream(ficheroPK12, FileMode.Open))
            {
                Pkcs12Store store = new Pkcs12Store(fs, contrasenaVieja.ToCharArray());

                // Obtiene la clave privada y el certificado
                string alias = store.Aliases.Cast<string>().FirstOrDefault();
                AsymmetricKeyEntry privateKeyEntry = store.GetKey(alias);
                X509CertificateEntry certificateEntry = store.GetCertificate(alias);

                // Cambia la contraseña
                var resultado = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                resultado = Path.Combine(resultado, "pk12FicheroCambiado.p12");
                if( File.Exists(resultado) ) { File.Delete(resultado); }
                store.SetKeyEntry(alias, new AsymmetricKeyEntry(privateKeyEntry.Key), new[] { certificateEntry });
                var flujo = new FileStream(resultado, FileMode.Create);
                store.Save(flujo, contrasenaNueva.ToCharArray(), new SecureRandom());
                flujo.Close();

                return resultado;
            }

        }
    }
}

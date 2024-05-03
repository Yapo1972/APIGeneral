namespace Conexiones
{
    public class ConexionBD
    {
        private string conexionString = String.Empty;
        public ConexionBD( string tipoConexion = "BDAccesoDatos")
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            conexionString = builder.GetConnectionString(tipoConexion);

        }
        public string cadenaConexion() {
        
            return conexionString;
        
        }
    }
}

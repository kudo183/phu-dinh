using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using PhuDinhCommon;

namespace PhuDinhData
{
    public static class ContextFactory
    {
        private static string connectionString;
        static ContextFactory()
        {
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = ConfigurationManager.AppSettings["DataSource"],
                UserID = Crypto.Decrypt(ConfigurationManager.AppSettings["UserID"], "nobita183#"),
                Password = Crypto.Decrypt(ConfigurationManager.AppSettings["Password"], "nobita183#"),
                InitialCatalog = ConfigurationManager.AppSettings["InitialCatalog"],

                //add this config for fixing asp.net Web exception 
                //{"There is already an open DataReader associated with this Command which must be closed first."}
                MultipleActiveResultSets = true
            };

            var entityConnectionStringBuilder = new EntityConnectionStringBuilder
            {
                Metadata = @"res://*/PhuDinh.csdl|res://*/PhuDinh.ssdl|res://*/PhuDinh.msl",
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = sqlConnectionStringBuilder.ToString()
            };

            connectionString = entityConnectionStringBuilder.ToString();
        }

        public static PhuDinhEntities CreateContext()
        {
            return new PhuDinhEntities(connectionString);
        }
    }
}

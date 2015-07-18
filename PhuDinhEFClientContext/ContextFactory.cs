using PhuDinhDataEntity;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Reflection;
using PhuDinhCommon;
using log4net;

namespace PhuDinhEFClientContext
{
    public static class ContextFactory
    {
        private static readonly ILog Logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static string connectionString = string.Empty;
        private static void InitConnectionString()
        {
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = ConfigurationManager.AppSettings["DataSource"],
                InitialCatalog = ConfigurationManager.AppSettings["InitialCatalog"],

                UserID = Crypto.Decrypt(ConfigurationManager.AppSettings["UserID"], "nobita183#"),
                Password = Crypto.Decrypt(ConfigurationManager.AppSettings["Password"], "nobita183#"),

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
            if (string.IsNullOrEmpty(connectionString))
            {
                InitConnectionString();
            }

            return CreateContext(connectionString);
        }

        //for testing
        public static PhuDinhEntities CreateContext(string s)
        {
            var result = new PhuDinhEntities(s);
            result.Configuration.AutoDetectChangesEnabled = false;
            result.Configuration.ProxyCreationEnabled = false;
            
            result.Database.Log = DBLogger;

            return result;
        }

        private static void DBLogger(string s)
        {
            Logger.Info(s);
        }
    }
}

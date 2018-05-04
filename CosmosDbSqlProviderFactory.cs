using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbAdoNetProvider
{
    public class CosmosDbSqlProviderFactory : DbProviderFactory
    {
        private string serviceUri;
        private string authKey;
        private string databaseName;

        public static readonly string ProviderName = "CosmosDbProvider";

        public CosmosDbSqlProviderFactory(string serviceUri, string authKey, string databaseName)
        {
            this.serviceUri = serviceUri;
            this.authKey = authKey;
            this.databaseName = databaseName;
        }
        public override DbCommand CreateCommand() => new CosmosDbSqlCommand();
       
        public override DbConnection CreateConnection() => new CosmosDbSqlConnection(serviceUri, authKey, databaseName);
        
        public override DbParameter CreateParameter() =>  new CosmosDbSqlParameter();
        
        public override DbDataAdapter CreateDataAdapter() => new CosmosDbSqlDataAdapter();        
    }
}

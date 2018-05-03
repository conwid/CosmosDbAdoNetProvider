using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CosmosDbAdoNetProvider
{
    [System.ComponentModel.DesignerCategory("")]
    public partial class CosmosDbSqlConnection : DbConnection
    {
        private string databaseName;
        private string serviceUri;
        private string authKey;
        public readonly DocumentClient client;

        private const string accountEndpointString = "AccountEndpoint";
        private const string accountKeyString = "AccountKey";
        private const string dataBaseString = "Database";
        private const char keyValueSeparator = '=';

        public CosmosDbSqlConnection() : base()
        {

        }

        private void ProcessConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Specified connection string is either empty or null");

            var parts = connectionString.Split(';');
            serviceUri = parts.Single(p => p.StartsWith(accountEndpointString)).Split(keyValueSeparator)[1];
            authKey = parts.Single(p => p.StartsWith(accountKeyString)).Split(keyValueSeparator)[1];
            databaseName = parts.Single(p => p.StartsWith(dataBaseString)).Split(keyValueSeparator)[1];
        }

        public CosmosDbSqlConnection(string connectionString) : base()
        {
            ProcessConnectionString(connectionString);
        }

        public override string ConnectionString
        {
            get
            {
                return $"{accountEndpointString}{keyValueSeparator}{serviceUri};{accountKeyString}{keyValueSeparator}{authKey};{dataBaseString}{keyValueSeparator}{databaseName};";
            }

            set
            {
                ProcessConnectionString(value);
            }
        }

        public override string Database
        {
            get
            {
                return databaseName;
            }
        }

        public override string DataSource
        {
            get
            {
                return serviceUri;
            }
        }

        public override string ServerVersion
        {
            get
            {
                return "1.0";
            }
        }

        private bool isOpen = false;
        public override ConnectionState State
        {
            get
            {
                if (isOpen)
                {
                    return ConnectionState.Open;
                }
                else
                {
                    return ConnectionState.Closed;
                }
            }
        }

        public CosmosDbSqlConnection(string serviceUri, string authKey, string databaseName) : base()
        {
            this.databaseName = databaseName;
            this.authKey = authKey;
            this.serviceUri = serviceUri;
            this.client = new DocumentClient(new Uri(serviceUri), authKey);
        }

        public override Task OpenAsync(CancellationToken cancellationToken)
        {
            this.Open();
            return Task.CompletedTask;
        }        

        public override void Close()
        {
            this.isOpen = false;
            this.client?.Dispose();
        }

        public override void ChangeDatabase(string databaseName)
        {
            this.databaseName = databaseName;
        }

        protected override DbCommand CreateDbCommand()
        {
            return new CosmosDbSqlCommand(this);
        }

        public override void Open()
        {
            this.isOpen = true;
        }
    }
}

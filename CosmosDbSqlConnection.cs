﻿using Microsoft.Azure.Documents.Client;
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
        public DocumentClient Client { get; }

        private const string accountEndpointString = "AccountEndpoint";
        private const string accountKeyString = "AccountKey";
        private const string dataBaseString = "Database";
        private const char keyValueSeparator = '=';

        public CosmosDbSqlConnection() : base()
        {

        }

        public CosmosDbSqlConnection(string connectionString) : base()
        {
            ProcessConnectionString(connectionString);
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
       
        public override string ConnectionString
        {
            get => $"{accountEndpointString}{keyValueSeparator}{serviceUri};{accountKeyString}{keyValueSeparator}{authKey};{dataBaseString}{keyValueSeparator}{databaseName};";
            set => ProcessConnectionString(value);
        }

        public override string Database => databaseName;

        public override string DataSource => serviceUri;

        public override string ServerVersion => "1.0";

        private bool isOpen = false;
        public override ConnectionState State => isOpen ? ConnectionState.Open : ConnectionState.Closed;

        public CosmosDbSqlConnection(string serviceUri, string authKey, string databaseName) : base()
        {
            this.databaseName = databaseName;
            this.authKey = authKey;
            this.serviceUri = serviceUri;
            this.Client = new DocumentClient(new Uri(serviceUri), authKey);
        }

        public override Task OpenAsync(CancellationToken cancellationToken)
        {
            this.Open();
            return Task.CompletedTask;
        }

        public override void Close()
        {
            this.isOpen = false;
            this.Client?.Dispose();
        }

        public override void ChangeDatabase(string databaseName) => this.databaseName = databaseName;

        protected override DbCommand CreateDbCommand() => new CosmosDbSqlCommand(this);

        public override void Open() => this.isOpen = true;
    }
}

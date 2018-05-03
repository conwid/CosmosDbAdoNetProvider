using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbAdoNetProvider
{
    [System.ComponentModel.DesignerCategory("")]
    public partial class CosmosDbSqlCommand : DbCommand
    {
        private CosmosDbSqlConnection connection;
        public CosmosDbSqlCommand() { }

        public CosmosDbSqlCommand(CosmosDbSqlConnection connection) => this.connection = connection;        

        public CosmosDbSqlCommand(CosmosDbSqlConnection connection, string commandText) : this(connection) => this.CommandText = commandText;        
        public override string CommandText { get; set; }
        protected override DbConnection DbConnection
        {
            get => this.connection;
            set => this.connection = (CosmosDbSqlConnection)value;
        }
        protected override DbParameterCollection DbParameterCollection { get; } = new CosmosDbSqlParameterCollection();

        public override void Cancel() { }

        public override object ExecuteScalar() => ExecuteInternal()[0].ElementAt(0).Value;

        public override void Prepare() { }

        protected override DbParameter CreateDbParameter() => new CosmosDbSqlParameter();


        private int timeout;
        public override int CommandTimeout
        {
            get => this.timeout;
            set => this.timeout = this.timeout == 0 ? value : throw new NotSupportedException("Only a value of 0 is supported currently for timeout");
        }
        public string Collection { get; set; }        

        protected List<ExpandoObject> ExecuteInternal()
        {
            if (string.IsNullOrWhiteSpace(CommandText))
                throw new InvalidOperationException("Command text is not set!");

            if (string.IsNullOrWhiteSpace(Collection))
                throw new InvalidOperationException("Collection is not set!");

            return
                this.connection.client
               .CreateDocumentQuery<ExpandoObject>(
                    UriFactory.CreateDocumentCollectionUri(this.connection.Database, Collection),
                    new SqlQuerySpec
                    {
                        QueryText = CommandText,
                        Parameters = new SqlParameterCollection(this.Parameters.Cast<CosmosDbSqlParameter>().Select(c => new SqlParameter(c.ParameterName, c.Value)))
                    }
                )
               .ToList();

        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior) => new CosmosDbSqlDataReader(ExecuteInternal());
        

        private CommandType commandType;
        public override CommandType CommandType
        {
            get => commandType;            
            set => commandType = value == CommandType.Text ? value : throw new NotSupportedException("Only text command type is supported");            
        }

        public override bool DesignTimeVisible
        {
            get => false;
            set => throw new OperationNotImplementedException();
        }

    }
}

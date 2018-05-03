using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbAdoNetProvider
{
    partial class CosmosDbSqlParameter
    {
        public override DbType DbType
        {
            get => throw new OperationNotImplementedException();
            set => throw new OperationNotImplementedException();
        }


        public override int Size
        {
            get => throw new OperationNotImplementedException();
            set => throw new OperationNotImplementedException();
        }

        public override string SourceColumn
        {
            get => throw new OperationNotImplementedException();
            set => throw new OperationNotImplementedException();
        }

        public override bool SourceColumnNullMapping
        {
            get => throw new OperationNotImplementedException();
            set => throw new OperationNotImplementedException();
        }


        public override void ResetDbType() => throw new OperationNotImplementedException();
    }
}


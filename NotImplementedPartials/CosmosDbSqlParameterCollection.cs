using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbAdoNetProvider
{
    partial class CosmosDbSqlParameterCollection
    {        
        public override void CopyTo(Array array, int index) => throw new OperationNotImplementedException();        
        protected override void SetParameter(int index, DbParameter value) => throw new OperationNotImplementedException();
        protected override void SetParameter(string parameterName, DbParameter value) => throw new OperationNotImplementedException();        
    }
}

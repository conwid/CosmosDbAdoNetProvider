using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbAdoNetProvider
{
    partial class CosmosDbSqlConnection
    {
        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel) => throw new NotImplementedException();        
    }
}

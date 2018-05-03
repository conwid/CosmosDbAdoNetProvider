using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbAdoNetProvider
{
    public partial class CosmosDbSqlParameter : DbParameter
    {
        public CosmosDbSqlParameter()
        {

        }
        public CosmosDbSqlParameter(string name)
        {
            this.ParameterName = name;
        }
        public CosmosDbSqlParameter(string name, object value) : this(name)
        {            
            this.Value = value;
        }
        private ParameterDirection direction;
        public override ParameterDirection Direction
        {
            get => direction;
            set => direction = value == ParameterDirection.Input ? value : throw new NotSupportedException("Value is not supported");
        }
        public override bool IsNullable { get; set; }
        private string parameterName;
        public override string ParameterName
        {
            get => parameterName;
            set => parameterName = string.IsNullOrEmpty(parameterName) ? value : throw new NotSupportedException("Parameter name cannot be changed after initialization");
        }
        public override object Value { get; set; }
    }
}

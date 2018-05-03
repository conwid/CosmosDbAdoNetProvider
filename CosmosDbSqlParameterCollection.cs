using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbAdoNetProvider
{
    public partial class CosmosDbSqlParameterCollection : DbParameterCollection
    {
        public override int Count => parameters.Count;
        private readonly List<CosmosDbSqlParameter> parameters;
        public CosmosDbSqlParameterCollection()
        {
            SyncRoot = new object();
            parameters = new List<CosmosDbSqlParameter>();
        }
        public override object SyncRoot { get; }
        public override int Add(object value)
        {
            if (value is CosmosDbSqlParameter cosmosDbParameter)
            {
                if (parameters.Any(p => p.ParameterName == cosmosDbParameter.ParameterName))
                    throw new ArgumentException("Parameter with the same has already been defined");
                parameters.Add(cosmosDbParameter);
                return parameters.Count - 1;
            }
            throw new ArgumentException("Specified object is not of type CosmosDbSqlParameter");
        }
        public override void AddRange(Array values)
        {
            foreach (var value in values.Cast<CosmosDbSqlParameter>())
            {
                parameters.Add(value);
            }
        }
        public override void Clear() => parameters.Clear();
        public override bool Contains(string parameterName) => parameters.Any(p => p.ParameterName == parameterName);
        public override bool Contains(object value) => parameters.Any(p => p.Value == value);
        public override IEnumerator GetEnumerator() => parameters.GetEnumerator();
        protected override DbParameter GetParameter(string parameterName) => parameters.Single(p => p.ParameterName == parameterName);
        public override int IndexOf(string parameterName) => parameters.IndexOf(parameters.Single(p => p.ParameterName == parameterName));
        public override int IndexOf(object value) => parameters.IndexOf((CosmosDbSqlParameter)value);
        public override void Insert(int index, object value) => parameters.Insert(index, (CosmosDbSqlParameter)value);
        public override void Remove(object value) => parameters.Remove((CosmosDbSqlParameter)value);
        public override void RemoveAt(string parameterName) => parameters.Remove(parameters.Single(p => p.ParameterName == parameterName));
        public override void RemoveAt(int index) => parameters.RemoveAt(index);
        protected override DbParameter GetParameter(int index) => parameters[index];

    }
}

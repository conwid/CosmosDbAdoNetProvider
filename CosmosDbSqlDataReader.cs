using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbAdoNetProvider
{
    public partial class CosmosDbSqlDataReader : DbDataReader
    {
        private List<ExpandoObject> query;
        private int curentIndex = -1;
        public CosmosDbSqlDataReader(List<ExpandoObject> query) => this.query = query;
        public override object this[string name] => query[curentIndex].Single(s => s.Key == name).Value;
        public override object this[int i] => query[curentIndex].ElementAt(i).Value;
        public override int FieldCount => query[curentIndex].Count();
        
        private bool isClosed = false;
        public override bool IsClosed => this.isClosed;
        public override void Close() => this.isClosed = true;
        public override bool Read() => ++curentIndex < query.Count;        
        public override bool NextResult() => false;
        public override string GetName(int ordinal) => query[0].ElementAt(ordinal).Key;
        public override object GetValue(int ordinal) => this[ordinal];
        public override bool HasRows => this.query.Count != 0;
        public override IEnumerator GetEnumerator() => this.query.GetEnumerator();
        public override DateTime GetDateTime(int ordinal) => (DateTime)GetValue(ordinal);
        public override decimal GetDecimal(int ordinal) => (decimal)GetValue(ordinal);
        public override double GetDouble(int ordinal) => (double)GetValue(ordinal);
        public override float GetFloat(int ordinal) => (float)GetValue(ordinal);
        public override Guid GetGuid(int ordinal) => (Guid)GetValue(ordinal);
        public override short GetInt16(int ordinal) => (short)GetValue(ordinal);
        public override int GetInt32(int ordinal) => (int)GetValue(ordinal);   
        public override long GetInt64(int ordinal) => (long)GetValue(ordinal);
        public override string GetString(int ordinal) => (string)GetValue(ordinal);
        public override bool IsDBNull(int ordinal) => GetValue(ordinal) == null;

    }
}

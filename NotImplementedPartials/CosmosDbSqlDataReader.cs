using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbAdoNetProvider
{
    partial class CosmosDbSqlDataReader
    {                       
        public override bool GetBoolean(int ordinal) => (bool)GetValue(ordinal);
        public override byte GetByte(int ordinal) => (byte)GetValue(ordinal);
        public override int Depth => throw new OperationNotImplementedException();
        public override int RecordsAffected => throw new OperationNotImplementedException();
        public override string GetDataTypeName(int ordinal) => throw new OperationNotImplementedException();
        public override Type GetFieldType(int ordinal) => throw new OperationNotImplementedException();
        public override int GetOrdinal(string name) =>  throw new OperationNotImplementedException();
        public override int GetValues(object[] values) => throw new OperationNotImplementedException();
        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length) => throw new NotImplementedException();
        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length) => throw new NotImplementedException();        
        public override char GetChar(int ordinal) => (char)GetValue(ordinal);       
    }
}


using System;
using System.Data;
using System.Data.SqlClient;

namespace DBContactLibrary
{
    public static class SqlParameterUtils
    {
        // Note: Must be called with an explicit data type for the type parameter.
        public static T GetNullOrValue<T>(this SqlDataReader sqlDataReader, string tableColumnName)
        {
            return
                sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal(tableColumnName))
                ? default(T)
                : (T)sqlDataReader[tableColumnName];
        }

        public static void AddVarCharIn(this SqlParameterCollection collection, string paramName, int size, string value)
        {
            SqlParameter varcharParam = new SqlParameter();

            varcharParam.ParameterName = paramName;
            varcharParam.Size = size;
            varcharParam.SqlDbType = SqlDbType.VarChar;
            varcharParam.SqlValue = value;
            varcharParam.Direction = ParameterDirection.Input;

            collection.Add(varcharParam);
        }
        public static void AddIntIn(this SqlParameterCollection collection, string paramName, int value)
        {
            SqlParameter varcharParam = new SqlParameter();

            varcharParam.ParameterName = paramName;
            varcharParam.SqlDbType = SqlDbType.Int;
            varcharParam.SqlValue = value;
            varcharParam.Direction = ParameterDirection.Input;
            collection.Add(varcharParam);
        }

        public static void AddNullableIntIn(this SqlParameterCollection collection, string paramName, int? value)
        {
            SqlParameter varcharParam = new SqlParameter();

            varcharParam.ParameterName = paramName;
            varcharParam.SqlDbType = SqlDbType.Int;

            if (value != null)
                varcharParam.SqlValue = value;
            else
                varcharParam.Value = DBNull.Value;

            varcharParam.Direction = ParameterDirection.Input;

            collection.Add(varcharParam);
        }

        public static void AddIntOut(this SqlParameterCollection collection, string paramName)
        {
            SqlParameter varcharParam = new SqlParameter();

            varcharParam.ParameterName = paramName;
            varcharParam.SqlDbType = SqlDbType.Int;
            varcharParam.Direction = ParameterDirection.Output;

            collection.Add(varcharParam);
        }

        public static void AddIntReturnValue(this SqlParameterCollection collection, string paramName)
        {
            SqlParameter varcharParam = new SqlParameter();

            varcharParam.ParameterName = paramName;
            varcharParam.SqlDbType = SqlDbType.Int;
            varcharParam.Direction = ParameterDirection.ReturnValue;

            collection.Add(varcharParam);
        }

        public static void BitOutput(this SqlParameterCollection collection, string paramName)
        {
            SqlParameter varcharParam = new SqlParameter();

            varcharParam.ParameterName = paramName;
            varcharParam.SqlDbType = SqlDbType.Bit;
            varcharParam.Direction = ParameterDirection.Output;

            collection.Add(varcharParam);
        }
    }
}

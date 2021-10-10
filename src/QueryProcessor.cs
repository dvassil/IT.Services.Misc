using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IT.Services.Misc
{
    public static class QueryProcessor
    {
        internal static MssqlQueryProcessor mssqlProcessor = new MssqlQueryProcessor();
        internal static MysqlQueryProcessor mysqlProcessor = new MysqlQueryProcessor();

        public static string Process(string query)
        {
            switch (Connection.DbType)
            {
            case "MYSQL":
                return mysqlProcessor.Process(query);
                //break;
            case "MSSQL":
                return mssqlProcessor.Process(query);
                //break;
            default:
                break;
            }
            return query;
        }

        public static string BuildSPCall(string procedureName, params string[] arguments)
        {
            procedureName = procedureName.Trim(' ', '.');
            switch (Connection.DbType)
            {
                case "MYSQL":
                    return mysqlProcessor.BuildSPCall(procedureName, arguments);
                //break;
                case "MSSQL":
                    return mssqlProcessor.BuildSPCall(procedureName, arguments);
                //break;
                default:
                    break;
            }
            throw new NotImplementedException();
        }


        public static IQueryProcessor GetProcessor()
        {
            switch (Connection.DbType)
            {
                case "MYSQL":
                    return mysqlProcessor;
                //break;
                case "MSSQL":
                    return mssqlProcessor;
                //break;
                default:
                    break;
            }
            throw new NotSupportedException("Unsupported database " + Connection.DbType);
        }
    }
}

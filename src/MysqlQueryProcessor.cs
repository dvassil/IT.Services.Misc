using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IT.Services.Misc
{
    internal class MysqlQueryProcessor : BaseQueryProcessor
    {
        public override string Process(string query)
        {
            query = base.Process(query);
            StringBuilder sb = new StringBuilder(query);
            sb.Replace("(*SQL_DATE*)",     "CURRENT_DATE()")
              .Replace("(*SQL_TIME*)",     "CURRENT_TIME()")
              .Replace("(*SQL_DATETIME*)", "CURRENT_TIMESTAMP()")
              .Replace("(*SQL_GUID*)",     "UUIDTOBIN(UUID())")
              ;

            while (RemoveBetween(ref sb,   "(*MSSQL(", ")*)"));
            while (UseBetween(ref sb,      "(*MYSQL(", ")*)"));

            return sb.ToString();
        }

        public override string BuildSPCall(string procedureName, params string[] arguments)
        {
            StringBuilder sb = new StringBuilder("CALL ");
            sb.AppendFormat("{0}(", procedureName).Append(string.Join(",", arguments)).Append(");");
            return sb.ToString();
        }
    }

}

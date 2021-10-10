using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IT.Services.Misc
{
    internal class MssqlQueryProcessor : BaseQueryProcessor
    {
        public override string Process(string query)
        {
            query = base.Process(query);
            StringBuilder sb = new StringBuilder(query);
            sb.Replace("(*SQL_DATE*)",        "CONVERT(date,GETDATE())")
              .Replace("(*SQL_TIME*)",        "CONVERT(time,GETDATE())")
              .Replace("(*SQL_DATETIME*)",    "GETDATE()")
              .Replace("(*SQL_GUID*)",        "NEWID()")
              ;

            while (RemoveBetween(ref sb, "(*MYSQL(", ")*)"));
            while (UseBetween(ref sb, "(*MSSQL(", ")*)"));

            return sb.ToString();
        }

        public override string BuildSPCall(string procedureName, params string[] arguments)
        {
            StringBuilder sb = new StringBuilder("EXECUTE ");
            sb.AppendFormat("{0} ", procedureName).Append(string.Join(",", arguments));
            return sb.ToString();
        }
    }
}

using System.Configuration;
using System.Data.Common;

namespace IT.Services.Misc
{
    public class Connection
    {
        protected static string defaultDbType = "MYSQL";
        public static string DbType
        {
            get
            {
                string str = ConfigurationManager.AppSettings["DbType"];
                return string.IsNullOrEmpty(str) ? defaultDbType : str.ToUpper().Trim();
            }
        }

        protected static string database;
        public static string Schema
        {
            get
            {
                string dbType = Connection.DbType;
                if (dbType == "MYSQL")
                {
                    return database;
                }
                else if (dbType == "MSSQL")
                {
                    return "dbo";
                }
                return string.Empty;
            }
        }

        protected static DbProviderFactory factory;

        public static DbProviderFactory GetFactory()
        {
            if (factory == null)
            {
                string dbType = ConfigurationManager.AppSettings["DbType"].ToUpper().Trim();
                if (dbType == "MYSQL")
                {
                    factory = DbProviderFactories.GetFactory("MySql.Data.MySqlClient");
                    //factory = DbProviderFactories.GetFactory("MySql.Data.MySqlClient.MySqlClientFactory");
                }
                else if (dbType == "MSSQL")
                {
                    factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                }
            }

            return factory;
        }

        public static string GetConnectionString()
        {
            //string server = "192.168.200.50";
            string server = ConfigurationManager.AppSettings["DbServer"];
            database = ConfigurationManager.AppSettings["Database"];
            if ((database == null || database.Length == 0)) database = "leaves";

            string dbType = ConfigurationManager.AppSettings["DbType"].ToUpper().Trim();
            if (dbType == "MYSQL")
            {
                return "Server=" + server + ";Database=" + database + ";Uid=dvassil;Pwd=to76lpnf;UseCompression=True;";
            }
            else if (dbType == "MSSQL")
            {
                string applicationName = ConfigurationManager.AppSettings["ApplicationName"];
                return "Application Name=" + applicationName + ";" +
                        "Persist Security Info=false;" +
                        "Integrated Security=false;" +
                        "User ID=dvroot;Password=dvroot;" +
                        "Data Source=" + server + ";Initial Catalog=" + database;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetConnectionString(string database)
        {
            if ((database == null || database.Length == 0)) return null;
            Connection.database = database;
            string server = ConfigurationManager.AppSettings["DbServer"];
            const string csFormat = "Application Name=PeKor WebService;" +
                                    "Persist Security Info=false;" +
                                    "Integrated Security=false;" +
                //"User ID=sa;Password=PAssw0rd;" +
                                    "User ID=dvroot;Password=dvroot;" +
                                    "Data Source={0};Initial Catalog={1}";
            return string.Format(csFormat, server, database);
        }
    }
}
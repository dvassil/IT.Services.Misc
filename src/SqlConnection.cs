using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;

namespace IT.Services.Misc
{
    public class SqlConnection
    {
        int refCount = 0;
        System.Data.SqlClient.SqlConnection conn;

        public SqlConnection()
        {
            conn = new System.Data.SqlClient.SqlConnection();
        }
        public SqlConnection(string connectionString)
        {
            conn = new System.Data.SqlClient.SqlConnection(connectionString);
        }

        public string ConnectionString
        {
            get { return conn.ConnectionString; }
            set { conn.ConnectionString = value; }
        }


        public ConnectionState State
        {
            get { return conn.State; }
        }

        public void Close()
        {
            Interlocked.Decrement(ref refCount);
            if (refCount == 0)
            {
                if (conn.State != ConnectionState.Closed && conn.State != ConnectionState.Broken)
                    conn.Close();
            }
        }

        public SqlCommand CreateCommand()
        {
            return conn.CreateCommand();
        }


        public void Open()
        {
            Interlocked.Increment(ref refCount);
            if (refCount == 1)
            {
                if (conn.State != ConnectionState.Open) conn.Open();
            }
        }

        //public int ConnectionTimeout
        //{
        //    get { return conn.ConnectionTimeout; }
        //}

        //public string Database {
        //    get { return conn.Database; }
        //}

        //public string DataSource
        //{
        //    get { return conn.DataSource; }
        //}

        //public bool FireInfoMessageEventOnUserErrors
        //{
        //    get { return conn.FireInfoMessageEventOnUserErrors; }
        //    set { conn.FireInfoMessageEventOnUserErrors = value; }
        //}

        //public int PacketSize { get; }
        //public string ServerVersion { get; }
        //public bool StatisticsEnabled { get; set; }
        //public string WorkstationId { get; }
        //public event SqlInfoMessageEventHandler InfoMessage;
        //public SqlTransaction BeginTransaction();
        //public SqlTransaction BeginTransaction(IsolationLevel iso);
        //public SqlTransaction BeginTransaction(string transactionName);
        //public SqlTransaction BeginTransaction(IsolationLevel iso, string transactionName);
        //public void ChangeDatabase(string database);
        //public static void ChangePassword(string connectionString, string newPassword);
        //public static void ClearAllPools();
        //public static void ClearPool(SqlConnection connection);
        //public DataTable GetSchema();
        //public DataTable GetSchema(string collectionName);
        //public DataTable GetSchema(string collectionName, string[] restrictionValues);
        //public void ResetStatistics();
        //public IDictionary RetrieveStatistics();
    }
}

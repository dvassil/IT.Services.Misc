using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace IT.Services.Misc
{
    public static class DB
    {
        public static string Schema
        {
            get { return Connection.Schema; }
        }

        public static DbConnection ConnectToDB()
        {
            try
            {
                DbConnection conn = Connection.GetFactory().CreateConnection();
                string connStr = Connection.GetConnectionString();
                conn.ConnectionString = connStr;
                conn.Open();
                return conn;
            }
            catch (Exception /*ex*/)
            {
                IT.Services.Log.EventLog.Log.WriteLog("Cannot connect to default Database", EventLogEntryType.Error);
                return null;
            }
        }

        public static DbConnection ConnectToDB(string database)
        {
            try
            {
                DbConnection Conn = Connection.GetFactory().CreateConnection();

                Conn.ConnectionString = Connection.GetConnectionString(database);

                Conn.Open();
                return Conn;
            }
            catch (Exception)
            {
                IT.Services.Log.EventLog.Log.WriteLog("Cannot connect to " + database + " Database", EventLogEntryType.Error);
                return null;
            }
        }

        public static int ExecuteSQL(string sql)
        {
            try
            {
                DbConnection conn = DB.ConnectToDB();
                conn.Close();
                DbCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandTimeout = 0;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                IT.Services.Log.EventLog.Log.WriteLog(ex.Message, EventLogEntryType.Warning);
                return 0;
            }
        }

        public static int ExecuteSQL(string sql, params object[] values)
        {
            try
            {
                DbConnection conn = DB.ConnectToDB();
                conn.Close();
                DbCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandTimeout = 0;
                cmd.AddParameters(values);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                IT.Services.Log.EventLog.Log.WriteLog(ex.Message, EventLogEntryType.Warning);
                return 0;
            }
        }

        public static DataTable GetDataTable(string SQLstring)
        {
            DbConnection Conn = DB.ConnectToDB();
            System.Globalization.CultureInfo enUSCulture = new System.Globalization.CultureInfo("en-US");

            DataTable dt = new DataTable();

            if (Conn.State == ConnectionState.Closed) return dt;

            DbCommand Cmd = Conn.CreateCommand();
            Cmd.CommandText = SQLstring;
            Cmd.CommandTimeout = 0;

            DbDataAdapter DA = Connection.GetFactory().CreateDataAdapter();
            DA.SelectCommand = Cmd;
            DataSet DS = new DataSet();
            DA.Fill(DS, "temp");

            DataTable DT = DS.Tables[0];

            Conn.Close();
            return DT;
        }

        public static DataTable GetDataTable(string SQLstring, params object[] values)
        {
            DbConnection Conn = DB.ConnectToDB();
            System.Globalization.CultureInfo enUSCulture = new System.Globalization.CultureInfo("en-US");

            DataTable dt = new DataTable();

            if (Conn.State == ConnectionState.Closed) return dt;

            DbCommand Cmd = Conn.CreateCommand();
            Cmd.CommandText = SQLstring;
            Cmd.CommandTimeout = 0;
            Cmd.AddParameters(values);

            DbDataAdapter DA = Connection.GetFactory().CreateDataAdapter();
            DA.SelectCommand = Cmd;
            DataSet DS = new DataSet();
            DA.Fill(DS, "temp");

            DataTable DT = DS.Tables[0];

            Conn.Close();
            return DT;
        }

        public static DataTable GetDataTable(string SQLstring, DbConnection Conn)
        {
            System.Globalization.CultureInfo enUSCulture = new System.Globalization.CultureInfo("en-US");

            DataTable dt = new DataTable();

            if (Conn.State == ConnectionState.Closed) return dt;

            DbCommand Cmd = Conn.CreateCommand();
            Cmd.CommandText = SQLstring;
            Cmd.CommandTimeout = 0;

            DbDataAdapter DA = Connection.GetFactory().CreateDataAdapter();
            DA.SelectCommand = Cmd;
            DataSet DS = new DataSet();
            DA.Fill(DS, "temp");

            DataTable DT = DS.Tables[0];

            Conn.Close();
            return DT;
        }

        public static DataTable GetDataTable(DbCommand cmd)
        {
            System.Globalization.CultureInfo enUSCulture = new System.Globalization.CultureInfo("en-US");

            if (cmd.Connection.State == ConnectionState.Closed)
                return new DataTable();

            cmd.CommandTimeout = 0;

            DbDataAdapter DA = Connection.GetFactory().CreateDataAdapter();
            DA.SelectCommand = cmd;
            DataSet DS = new DataSet();
            DA.Fill(DS, "temp");

            DataTable dt = DS.Tables[0];

            cmd.Connection.Close();
            return dt;
        }

        #region DataRow Extensions

        public static T GetValue<T>(this DataRow dr, string column)
        {
            return (T)dr[column];
        }

        public static void GetValue(this DataRow dr, string column, out int? field)
        {
            if ((dr[column] == DBNull.Value) || (dr[column] == null))
                field = null;
            else
                field = Convert.ToInt32(dr[column]);
        }

        public static void GetValue(this DataRow dr, string column, out int field)
        {
            field = Convert.ToInt32(dr[column]);
        }

        public static void GetValue(this DataRow dr, string column, out string field)
        {
            field = Convert.ToString(dr[column]);
        }

        public static void GetValue(this DataRow dr, string column, out bool field)
        {
            field = Convert.ToBoolean(dr[column]);
        }

        public static void GetValue(this DataRow dr, string column, out DateTime field)
        {
            field = Convert.ToDateTime(dr[column]);
        }

        public static void GetValue(this DataRow dr, string column, out double field)
        {
            field = Convert.ToDouble(dr[column]);
        }

        public static void GetValue(this DataRow dr, string column, out float field)
        {
            field = (float)Convert.ToDouble(dr[column]);
        }

        public static void GetValue(this DataRow dr, string column, out decimal field)
        {
            field = Convert.ToDecimal(dr[column]);
        }

        public static void GetValue(this DataRow dr, string column, out decimal? field)
        {
            if ((dr[column] == DBNull.Value) || (dr[column] == null))
                field = null;
            else
                field = Convert.ToDecimal(dr[column]);
        }

        public static void GetValue(this DataRow dr, int column, out decimal field)
        {
            field = Convert.ToDecimal(dr[column]);
        }

        public static void GetValue(this DataRow dr, int column, out decimal? field)
        {
            if ((dr[column] == DBNull.Value) || (dr[column] == null))
                field = null;
            else
                field = Convert.ToDecimal(dr[column]);
        }

        public static T GetValue<T>(this DataRow dr, int column)
        {
            return (T)dr[column];
        }

        public static void GetValue(this DataRow dr, int column, out int field)
        {
            field = Convert.ToInt32(dr[column]);
        }

        public static void GetValue(this DataRow dr, int column, out string field)
        {
            field = Convert.ToString(dr[column]);
        }

        public static void GetValue(this DataRow dr, int column, out bool field)
        {
            field = Convert.ToBoolean(dr[column]);
        }

        public static void GetValue(this DataRow dr, string column, out bool? field)
        {
            if ((dr[column] == DBNull.Value) || (dr[column] == null))
                field = null;
            else
                field = Convert.ToBoolean(dr[column]);
        }

        public static void GetValue(this DataRow dr, int column, out bool? field)
        {
            if ((dr[column] == DBNull.Value) || (dr[column] == null))
                field = null;
            else
                field = Convert.ToBoolean(dr[column]);
        }

        public static void GetValue(this DataRow dr, int column, out DateTime field)
        {
            field = Convert.ToDateTime(dr[column]);
        }

        public static void GetValue(this DataRow dr, int column, out double field)
        {
            field = Convert.ToDouble(dr[column]);
        }

        public static void GetValue(this DataRow dr, int column, out float field)
        {
            field = (float)Convert.ToDouble(dr[column]);
        }

        #endregion DataRow Extensions

        #region DbDataReader Extensions

        public static T GetValue<T>(this DbDataReader dr, string column)
        {
            return (T)dr[column];
        }

        public static void GetValue(this DbDataReader dr, string column, out int field)
        {
            field = Convert.ToInt32(dr[column]);
        }

        public static void GetValue(this DbDataReader dr, string column, out int? field)
        {
            if ((dr[column] == DBNull.Value) || (dr[column] == null))
                field = null;
            else
                field = Convert.ToInt32(dr[column]);
        }

        public static void GetValue(this DbDataReader dr, string column, out string field)
        {
            field = Convert.ToString(dr[column]);
        }

        public static void GetValue(this DbDataReader dr, string column, out bool field)
        {
            field = Convert.ToBoolean(dr[column]);
        }

        public static void GetValue(this DbDataReader dr, string column, out DateTime field)
        {
            field = Convert.ToDateTime(dr[column]);
        }

        public static void GetValue(this DbDataReader dr, string column, out double field)
        {
            field = Convert.ToDouble(dr[column]);
        }

        public static void GetValue(this DbDataReader dr, string column, out float field)
        {
            field = (float)Convert.ToDouble(dr[column]);
        }

        public static void GetValue(this DbDataReader dr, string column, out decimal field)
        {
            field = Convert.ToDecimal(dr[column]);
        }

        public static T GetValue<T>(this DbDataReader dr, int column)
        {
            return (T)dr[column];
        }

        public static void GetValue(this DbDataReader dr, int column, out int field)
        {
            field = Convert.ToInt32(dr[column]);
        }

        public static void GetValue(this DbDataReader dr, int column, out string field)
        {
            field = Convert.ToString(dr[column]);
        }

        public static void GetValue(this DbDataReader dr, int column, out bool field)
        {
            field = Convert.ToBoolean(dr[column]);
        }

        public static void GetValue(this DbDataReader dr, int column, out DateTime field)
        {
            field = Convert.ToDateTime(dr[column]);
        }

        public static void GetValue(this DbDataReader dr, int column, out double field)
        {
            field = Convert.ToDouble(dr[column]);
        }

        public static void GetValue(this DbDataReader dr, int column, out float field)
        {
            field = (float)Convert.ToDouble(dr[column]);
        }

        public static void GetValue(this DbDataReader dr, int column, out decimal field)
        {
            field = Convert.ToDecimal(dr[column]);
        }

        #endregion DbDataReader Extensions

        #region IDataReader Extensions

        public static T GetValue<T>(this IDataReader dr, string column)
        {
            return (T)dr[column];
        }

        public static void GetValue(this IDataReader dr, string column, out short field)
        {
            field = Convert.ToInt16(dr[column]);
        }

        public static void GetValue(this IDataReader dr, string column, out int field)
        {
            field = Convert.ToInt32(dr[column]);
        }

        public static void GetValue(this IDataReader dr, string column, out string field)
        {
            field = Convert.ToString(dr[column]);
        }

        public static void GetValue(this IDataReader dr, string column, out bool field)
        {
            field = Convert.ToBoolean(dr[column]);
        }

        public static void GetValue(this IDataReader dr, string column, out DateTime field)
        {
            field = Convert.ToDateTime(dr[column]);
        }

        public static void GetValue(this IDataReader dr, string column, out double? field)
        {
            object obj = dr[column];
            if (obj == DBNull.Value)
                field = null;
            else
                field = Convert.ToDouble(obj);
        }

        public static void GetValue(this IDataReader dr, string column, out double field)
        {
            field = Convert.ToDouble(dr[column]);
        }

        public static void GetValue(this IDataReader dr, string column, out float field)
        {
            field = (float)Convert.ToDouble(dr[column]);
        }

        public static void GetValue(this IDataReader dr, string column, out decimal field)
        {
            field = Convert.ToDecimal(dr[column]);
        }

        public static T GetValue<T>(this IDataReader dr, int column)
        {
            return (T)dr[column];
        }

        public static void GetValue(this IDataReader dr, int column, out int field)
        {
            field = Convert.ToInt32(dr[column]);
        }

        public static void GetValue(this IDataReader dr, int column, out string field)
        {
            field = Convert.ToString(dr[column]);
        }

        public static void GetValue(this IDataReader dr, int column, out bool field)
        {
            field = Convert.ToBoolean(dr[column]);
        }

        public static void GetValue(this IDataReader dr, int column, out DateTime field)
        {
            field = Convert.ToDateTime(dr[column]);
        }

        public static void GetValue(this IDataReader dr, int column, out double field)
        {
            field = Convert.ToDouble(dr[column]);
        }

        public static void GetValue(this IDataReader dr, int column, out float field)
        {
            field = (float)Convert.ToDouble(dr[column]);
        }

        public static void GetValue(this IDataReader dr, int column, out decimal field)
        {
            field = Convert.ToDecimal(dr[column]);
        }

        #endregion IDataReader Extensions

        public static DataRow AddRow(DataRow row, string TableName, DbCommand cmd, string UserSessionID)
        {
            int result = 0;
            if (TableName == "Moves")
            {
                int.TryParse(GetDataTable("Select aa from move_types where id = " + row["move_type_id"].ToString(), ConnectToDB()).Rows[0]["aa"].ToString(), out result);
                result++;
                row["move_aa"] = result;
            }
            cmd.Parameters.Clear();
            foreach (DataColumn column in row.Table.Columns)
            {
                cmd.AddParameter("@" + column.ColumnName, row[column.ColumnName]);
                //cmd.Parameters.Add("@" + column.ColumnName, GetDBType(column.DataType));
                //cmd.Parameters["@" + column.ColumnName].Value = row[column.ColumnName];
            }
            string text = "";
            string text2 = "";
            string text3 = "";
            for (int i = 1; i < row.Table.Columns.Count - 1; i++)
            {
                text3 = row.Table.Columns[i].ColumnName;
                text = text + " " + text3 + ", ";
                text2 = text2 + " @" + text3 + ", ";
            }
            text = text + " " + row.Table.Columns[row.Table.Columns.Count - 1].ColumnName + " ";
            text2 = text2 + " @" + row.Table.Columns[row.Table.Columns.Count - 1].ColumnName + " ";
            cmd.CommandText = "INSERT INTO " + TableName + " (" + text + " )  VALUES ( " + text2 + " ) SELECT SCOPE_IDENTITY() ";
            int num = 0;
            num = int.Parse(cmd.ExecuteScalar().ToString());
            if (TableName == "Moves")
            {
                ExecuteSQL("UPDATE move_types SET aa = " + result + " WHERE id = " + row["move_type_id"].ToString());
            }
            DataTable dataTable = GetDataTable("Select * from " + TableName + " where id = " + num, ConnectToDB());
            if (dataTable.Rows.Count == 0)
            {
                return dataTable.NewRow();
            }
            return dataTable.Rows[0];
        }

        public static DataRow EditRow(DataRow row, string TableName, DbCommand cmd)
        {
            cmd.Parameters.Clear();
            foreach (DataColumn column in row.Table.Columns)
            {
                cmd.AddParameter("@" + column.ColumnName, row[column.ColumnName]);
                //cmd.Parameters.Add("@" + column.ColumnName, row. GetDBType(column.DataType));
                //cmd.Parameters["@" + column.ColumnName].Value = row[column.ColumnName];
            }
            string text = "";
            for (int i = 1; i < row.Table.Columns.Count - 1; i++)
            {
                string text2 = text;
                text = text2 + " " + row.Table.Columns[i].ColumnName + " = @" + row.Table.Columns[i].ColumnName + ", ";
            }
            string text3 = text;
            text = text3 + " " + row.Table.Columns[row.Table.Columns.Count - 1].ColumnName + " = @" + row.Table.Columns[row.Table.Columns.Count - 1].ColumnName;
            cmd.CommandText = "UPDATE " + TableName + " SET " + text + " WHERE id = " + row["id"];
            cmd.ExecuteNonQuery();
            return row;
        }

    }
}
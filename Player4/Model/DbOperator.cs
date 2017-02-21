using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;

namespace Player4.Model
{
    public class DbOperator : IDbOperator
    {
        //private const string StrConn1 = "server=.;database=master;Trusted_Connection=SSPI";
        private const string StrConn = @"Data Source=..\..\Database\PlayerDb.db;Version=3;";
        //private static SqlConnection _conn = new SqlConnection(StrConn);
        private static readonly SQLiteConnection Conn = new SQLiteConnection(StrConn);

        public bool IsExistDb(string dbName)
        {
            #region SQL Server
            /***
            if (_conn.State != ConnectionState.Open)
            {
                _conn.Open();
            }

            string existSql = $"select * From master.dbo.sysdatabases where name='{dbName}'";
            var comm = new SqlCommand(existSql, _conn);
            var dataReader = comm.ExecuteReader();
            if (dataReader.Read())
            {
                dataReader.Close();
                dataReader.Dispose();
                comm.Dispose();
                _conn = new SqlConnection($"server=.;database={dbName};Trusted_Connection=SSPI");
                return true;
            }

            dataReader.Dispose();
            dataReader.Close();
            comm.Dispose();
            return false;
            **/
            #endregion

            if (File.Exists($@"..\..\Database\{dbName}.db"))
            {
                return true;
            }
            return false;
        }

        public bool IsExistTable(string tableName)
        {
            #region SQL Server
            /**
            if (_conn.State != ConnectionState.Open)
            {
                _conn.Open();
            }
            string tableExistSql = $"select * from sysObjects where Name='{tableName}'";
            var comm = new SqlCommand(tableExistSql, _conn);
            var dataReader = comm.ExecuteReader();
            if (dataReader.Read())
            {
                dataReader.Dispose();
                comm.Dispose();
                return true;
            }
            dataReader.Dispose();
            comm.Dispose();
            return false;
            **/
            #endregion
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            string tableExistSql = $"SELECT * FROM sqlite_master where type='table' and name='{tableName}'";
            var comm = new SQLiteCommand(tableExistSql, Conn);
            var dataReader = comm.ExecuteReader();
            if (dataReader.Read())
            {
                dataReader.Dispose();
                comm.Dispose();
                return true;
            }
            dataReader.Dispose();
            comm.Dispose();
            return false;
        }

        public void CreateDb(string dbName)
        {
            #region SQL Server
            /**
            if (_conn.State != ConnectionState.Open)
            {
                _conn.Open();
            }
            string createSql = $"CREATE DATABASE {dbName}";

            var comm = new SqlCommand(createSql, _conn);
            comm.ExecuteNonQuery();
            comm.Dispose();
    **/
            #endregion
            SQLiteConnection.CreateFile($@"..\..\Database\{dbName}.db");
        }
        /// <summary>
        /// 创建数据库表
        /// </summary>
        /// <param name="dbName">数据库名</param>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列</param>
        public void CreateTable(string dbName, string tableName, string columnName)
        {
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            string tableSql = $"create table {tableName} ({columnName})";
            var comm = new SQLiteCommand(tableSql, Conn);
            comm.ExecuteNonQuery();
            comm.Dispose();
        }

        public void InsertTable(string dbName, string tableName, string columnName, string parameter)
        {
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            string tableSql = $"insert into {tableName} ({columnName}) " +
                              $"values({parameter})";
            var comm = new SQLiteCommand(tableSql, Conn);
            comm.ExecuteNonQuery();
            comm.Dispose();
        }

        public SQLiteDataReader TableDataReader(string dbName, string select)
        {
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            string selectSql = $"{select}";
            var comm = new SQLiteCommand(selectSql, Conn);
            var dataReader = comm.ExecuteReader();
            comm.Dispose();
            return dataReader;
        }

        public string EscConvertor(string s)
        {
            return s.Replace("'", "''");
        }

        public void ClearTable(string dbName, string tableName)
        {
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            //string clearSql = $"truncate table {tableName}";
            string clearSql = $"delete from {tableName}";
            var comm = new SQLiteCommand(clearSql, Conn);
            comm.ExecuteNonQuery();
            comm.Dispose();
        }

        public void UpdateTable(string dbName, string tableName, string param, string path)
        {
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            string updateSql = $"update {tableName} set {param} where path='{EscConvertor(path)}'";
            var comm = new SQLiteCommand(updateSql, Conn);
            comm.ExecuteNonQuery();
            comm.Dispose();
        }
    }
}

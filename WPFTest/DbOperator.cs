using System.Data.SqlClient;
using System.Windows;

namespace WPFTest
{
    public class DbOperator
    {
        private const string StrConn = "server=.;database=master;Trusted_Connection=SSPI";
        private static readonly SqlConnection Conn = new SqlConnection(StrConn);

        public bool IsCreateDb()
        {
            Conn.Open();
            const string existSql = @"select * From master.dbo.sysdatabases where name='Test'";
            SqlCommand comm = new SqlCommand(existSql, Conn);
            SqlDataReader dataReader = comm.ExecuteReader();
            if (dataReader.Read())
            {
                dataReader.Close();
                dataReader.Dispose();
                comm.Dispose();
                return true;
            }

            string createSql = "CREATE DATABASE Test";
            comm.Dispose();
            dataReader.Close();
            dataReader.Dispose();
            comm = new SqlCommand(createSql, Conn);
            comm.ExecuteNonQuery();
            dataReader.Dispose();
            dataReader.Close();
            comm.Dispose();
            return false;
        }

        public bool IsCreateTable()
        {
            IsCreateDb();
            const string tableExistSql = @"select * from sysObjects where Name='table1'";
            var comm = new SqlCommand(tableExistSql, Conn);
            var dataReader = comm.ExecuteReader();
            if (dataReader.Read())
            {
                dataReader.Dispose();
                comm.Dispose();
                //MessageBox.Show("表已存在");
                return false;
            }
            dataReader.Dispose();
            comm.Dispose();
            const string tableSql = @"use test create table table1 (path nvarchar(400),title text,tag bit)";
            comm = new SqlCommand(tableSql, Conn);
            comm.ExecuteNonQuery();
            //MessageBox.Show("表不存在");
            return true;
        }

        public void InsertTable()
        {
            IsCreateTable();
            const string insertSql=@"use test insert into table1(path,title,tag) values('aaaaaaa','atitle',1)";
            var comm = new SqlCommand(insertSql, Conn);
            comm.ExecuteNonQuery();
        }
    }
}
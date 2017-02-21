using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Configuration;
using System.Text;
using System.Windows;

namespace WPFTest.SQLiteTest
{
    public class SqliteOperater
    {
        public void DbConnect()
        {
            /**方法1**/
            using (DbConnection conn = new SQLiteConnection("sqlite"))
            {
                conn.Open();
                DbCommand comm = conn.CreateCommand();
                comm.CommandText = "select * from customer";
                comm.CommandType = CommandType.Text;
                using (IDataReader reader = comm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MessageBox.Show(reader[0].ToString());
                    }
                }
            }
        }

        public void DbCreate()
        {
            //创建一个数据库文件  
            string datasource = @"C:\Temp\MyDatabase.sqlite";
            //SQLiteConnection.CreateFile(datasource);  //创建新的数据库 使用时注意，如果已经存在，则覆盖旧的数据库
                                                      //连接数据库  
            SQLiteConnection conn = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            SQLiteConnectionStringBuilder connstr = new SQLiteConnectionStringBuilder();
            connstr.DataSource = datasource;
            //connstr.Password = "admin";//设置密码，SQLite ADO.NET实现了数据库密码保护  
            conn.ConnectionString = connstr.ToString();
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = conn;      //设置连接          

            //创建表  
            string sql = "CREATE TABLE IF NOT EXISTS test(username varchar(20),password varchar(20))";

            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();


            /***插入数据****     

            sql = "INSERT INTO test VALUES('卡尔萨斯','mypassword')";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            sql = "INSERT INTO test VALUES('露娜','mypassword')";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            /****/

            //取出数据  
            sql = "SELECT * FROM test";
            cmd.CommandText = sql;
            SQLiteDataReader reader = cmd.ExecuteReader();

            StringBuilder sb = new StringBuilder();
            while (reader.Read())
            {
                sb.Append("username:").Append(reader.GetString(0)).Append("\t")
                .Append("password:").Append(reader.GetString(1)).Append("\n");
            }
            MessageBox.Show(sb.ToString());

            //取出数量
            reader.Dispose();
            sql = "SELECT * FROM sqlite_master where type='table' and name='test'";
            cmd.CommandText = sql;
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                MessageBox.Show("true");
            }
            else
                MessageBox.Show("false");
        }
    }
}
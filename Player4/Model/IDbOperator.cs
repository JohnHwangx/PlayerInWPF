using System.Data.SqlClient;
using System.Data.SQLite;

namespace Player4.Model
{
    public interface IDbOperator
    {
        bool IsExistDb(string dbName);
        bool IsExistTable(string tableName);
        void CreateDb(string dbName);
        void CreateTable(string dbName, string tableName, string columnName);
        void InsertTable(string dbName, string tableName, string columnName, string parameter);
        SQLiteDataReader TableDataReader(string dbName, string select);
        void ClearTable(string dbName, string tableName);
    }
}
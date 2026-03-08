using Microsoft.Data.Sqlite;
using System.Security.Cryptography.X509Certificates;


namespace A_P_SmartHub.Databazicky
{
    internal class SQLITE_Users
    {
        public void CreateDB()
        {
            using var connection = new SqliteConnection("Data Source=users.db");
            connection.Open();

            string sql = @"CREATE TABLE IF NOT EXISTS users(
                UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                Mail TEXT UNIQUE NOT NULL,
                Hash TEXT NOT NULL
            );";

            using var CreateSqlTable = new SqliteCommand(sql, connection);
            CreateSqlTable.ExecuteNonQuery();
        }
        public bool RegisterNewUser(string Mail, string Hash)
        {
            try
            {
                using var connection = new SqliteConnection("Data Source=users.db");
                connection.Open();
                var AddToDB = connection.CreateCommand();
                AddToDB.CommandText = @"
             INSERT INTO  users (Mail,Hash)
            VALUES ($mail, $hash); ";
                AddToDB.Parameters.AddWithValue("$mail", Mail);
                AddToDB.Parameters.AddWithValue("$hash", Hash);
                AddToDB.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false; 
            }
        }
    }
}
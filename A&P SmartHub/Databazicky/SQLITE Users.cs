using Microsoft.Data.Sqlite;
using System.Security.Cryptography.X509Certificates;
using System.Windows;


namespace A_P_SmartHub.Databazicky
{
    public class SQLITE_Users
    {
        public string FetchedMail { get; set; }
        public string FetchedHash { get; set; }
        public int UseriID { get; set; }
        //--------------------------------(len si rozdelujem infos od code)

     


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

        public void LoggingInDB(string Mail, string Hash)
        {
            using var connection = new SqliteConnection("Data Source=users.db");
            connection.Open();

            var GetFromDB = connection.CreateCommand();
            GetFromDB.CommandText = @"
              SELECT UserID,Mail,Hash 
                FROM users
                 WHERE Mail = $mail ";
            GetFromDB.Parameters.AddWithValue("$mail", Mail);

            using var reader = GetFromDB.ExecuteReader();
            reader.Read();

          

            UseriID = reader.GetInt32(0);
            FetchedMail = reader.GetString(1);
            FetchedHash = reader.GetString(2);

           






        }
    }
}
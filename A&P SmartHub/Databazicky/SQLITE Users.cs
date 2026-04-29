using Microsoft.Data.Sqlite;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using DotNetEnv;

namespace A_P_SmartHub.Databazicky
{
    public class SQLITE_Users
    {
        public string FetchedMail { get; set; }
        public string FetchedHash { get; set; }
        public string UseriID { get; set; }
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


        public bool IsMailInDB(string Mail)
        {
            using var connction = new SqliteConnection("Data Source =users.db");
            connction.Open();
            var isMailInDb = connction.CreateCommand();
            isMailInDb.CommandText = @"
            SELECT UserID
             FROM users 
             WHERE Mail = $mail";
            isMailInDb.Parameters.AddWithValue("$mail", Mail);

            using var reader = isMailInDb.ExecuteReader();
            if (reader.Read())
            {
                UseriID = reader.GetInt32(0).ToString();
                return true;
            }
            else
            {
                UseriID = null;
                return false;
            }
        }
        public void LoggingInDB(string Mail)
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
           

            if (reader.Read())
            {
                UseriID = reader.GetInt32(0).ToString();
                FetchedMail = reader.GetString(1);
                FetchedHash = reader.GetString(2);
            }
            else
            {
                UseriID = null;
                FetchedMail = null;
                FetchedHash = null;
            }

        }

        public void UpdateHashInDb(string Mail, string Hash)
        {
            using var connection = new SqliteConnection("Data Source=users.db");
            connection.Open();

            var UpdateHashInDb = connection.CreateCommand();
            UpdateHashInDb.CommandText = @"
             UPDATE users
             SET Hash = $hash
              WHERE Mail = $mail;";
            UpdateHashInDb.Parameters.AddWithValue("$mail", Mail);
            UpdateHashInDb.Parameters.AddWithValue("$hash", Hash);
            UpdateHashInDb.ExecuteNonQuery();

        }

        public string GetUserId(string mail)
        {
            using var connection = new SqliteConnection("Data Source=users.db");
            connection.Open();

            var getUserId = connection.CreateCommand();
            getUserId.CommandText = "SELECT UserID FROM users WHERE Mail = $mail"; 
            getUserId.Parameters.AddWithValue("$mail", mail);

            var result = getUserId.ExecuteScalar();

            return result?.ToString();
        }

      
    }
}
    
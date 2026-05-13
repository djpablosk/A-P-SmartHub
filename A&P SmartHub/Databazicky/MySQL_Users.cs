using MySqlConnector;
using System.Windows;
using DotNetEnv;
using System;

namespace A_P_SmartHub.Databazicky
{
    public class MySQL_Users
    {
        public string FetchedMail { get; set; }
        public string FetchedHash { get; set; }
        public string UseriID { get; set; }

        private string getConn()
        {
            Env.Load();
            return Environment.GetEnvironmentVariable("MysqlConn");
        }

        public void CreateDB()
        {
            using var connection = new MySqlConnection(getConn());
            connection.Open();

            string sql = @"CREATE TABLE IF NOT EXISTS users(
                Id VARCHAR(99) PRIMARY KEY,
                Mail VARCHAR(99) UNIQUE NOT NULL,
                HashPass VARCHAR(99) NOT NULL
            );";

            using var CreateSqlTable = new MySqlCommand(sql, connection);
            CreateSqlTable.ExecuteNonQuery();
        }

        public bool RegisterNewUser(string Mail, string HashPass)
        {
            try
            {
                using var connection = new MySqlConnection(getConn());
                connection.Open();
                
                string newUserId = Guid.NewGuid().ToString(); 
                
                var AddToDB = connection.CreateCommand();
                AddToDB.CommandText = @"
             INSERT INTO users (Id, Mail, HashPass)
            VALUES (@id, @mail, @hashpass); "; 
                AddToDB.Parameters.AddWithValue("@id", newUserId); 
                AddToDB.Parameters.AddWithValue("@mail", Mail);
                AddToDB.Parameters.AddWithValue("@hashpass", HashPass);
                AddToDB.ExecuteNonQuery();
                return true;                  
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        public bool IsMailInDB(string Mail)
        {
            using var connction = new MySqlConnection(getConn());
            connction.Open();
            var isMailInDb = connction.CreateCommand();
            isMailInDb.CommandText = @"
            SELECT Id
             FROM users 
             WHERE Mail = @mail";
            isMailInDb.Parameters.AddWithValue("@mail", Mail);

            using var reader = isMailInDb.ExecuteReader();
            if (reader.Read())
            {
                UseriID = reader.GetString(0); 
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
            using var connection = new MySqlConnection(getConn());
            connection.Open();

            var GetFromDB = connection.CreateCommand();
            GetFromDB.CommandText = @"
              SELECT Id, Mail, HashPass 
                FROM users
                 WHERE Mail = @mail ";
            GetFromDB.Parameters.AddWithValue("@mail", Mail);

            using var reader = GetFromDB.ExecuteReader();
           
            if (reader.Read())
            {
                UseriID = reader.GetString(0); 
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

        public void UpdateHashInDb(string Mail, string HashPass)
        {
            using var connection = new MySqlConnection(getConn());
            connection.Open();

            var UpdateHashInDb = connection.CreateCommand();
            UpdateHashInDb.CommandText = @"
             UPDATE users
             SET HashPass = @hashpass
              WHERE Mail = @mail;";
            UpdateHashInDb.Parameters.AddWithValue("@mail", Mail);
            UpdateHashInDb.Parameters.AddWithValue("@hashpass", HashPass);
            MessageBox.Show($"Mail: {Mail}");
            MessageBox.Show($"Hash: {HashPass}");
            UpdateHashInDb.ExecuteNonQuery();
        }

        public string GetUserId(string mail)
        {
            using var connection = new MySqlConnection(getConn());
            connection.Open();

            var getUserId = connection.CreateCommand();
            getUserId.CommandText = "SELECT Id FROM users WHERE Mail = @mail"; 
            getUserId.Parameters.AddWithValue("@mail", mail);

            var result = getUserId.ExecuteScalar();

            return result?.ToString();
        }
    }
}
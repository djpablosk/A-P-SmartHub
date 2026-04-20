using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DotNetEnv;
using MySqlConnector;


namespace A_P_SmartHub.Databazicky
{
    public class MySql
    {
        
            public string HomeName { get; set; }
            public string UserName { get; set; }
            public string City { get; set; }
        
        public string getConn()
        {

            Env.Load();
            string connStr = Environment.GetEnvironmentVariable("MysqlConn");
            return connStr;
        }





        public async Task DataBase()
        {
            using (var conn = new MySqlConnection(getConn()))
            {
                await conn.OpenAsync();

                var cmd = conn.CreateCommand();
                cmd.CommandText = @"
SELECT * FROM apdefaultinfos";

                var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    MessageBox.Show(reader["UserName"].ToString());
                }

            }
        }
        public async Task SaveToDB(string id ,string homename, string username, string city)
        {
            using var conn = new MySqlConnection(getConn());
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO apdefaultinfos ( Id, HomeName, UserName, City)
                        VALUES (@id,@homename, @username, @city);";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@homename", homename);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@city", city);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task ReturnBasicFromDB(string id)
        {
            using var conn = new MySqlConnection(getConn());
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM apdefaultinfos WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                HomeName = reader["HomeName"].ToString();
                UserName = reader["UserName"].ToString();
                City = reader["City"].ToString();
            }
        }
    }
}
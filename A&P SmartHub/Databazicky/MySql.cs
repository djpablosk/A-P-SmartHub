using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xaml;
using MySqlConnector;
using DotNetEnv;
namespace A_P_SmartHub.Databazicky
{
    internal class MySql
    {
        public string getConn()
        {
            Env.Load();
            string connStr = Environment.GetEnvironmentVariable("MysqlConn");
            return connStr;
        }
        



        public async Task DataBase()
        {
            using var connection = new MySqlConnection(getConn());
            await connection.OpenAsync();
            var cmd = new MySqlCommand("SELECT * FROM apdefaultinfos WHERE Id = 1", connection);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                MessageBox.Show($"{reader["UserName"]} {reader["Id"]} {reader["HomeName"]} {reader["city"]}".ToString());
            }
            
                

    }

}
}

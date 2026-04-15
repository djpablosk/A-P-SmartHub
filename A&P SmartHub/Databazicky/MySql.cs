using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySqlConnector;

namespace A_P_SmartHub.Databazicky
{
    internal class MySql
    {
            string connStr = "server=localhost;user=aphub_app;password=PasscodeIsSoHard0802#;database=apsmarthub;";
        

        public async Task DataBase()
        {
            using (var conn = new MySqlConnection(connStr))
            {
                await conn.OpenAsync();

              var cmd =    conn.CreateCommand();
                cmd.CommandText = @"
SELECT * FROM apdefaultinfos";

                var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    MessageBox.Show(reader["UserName"].ToString());
                }

            }
        }

    }
}

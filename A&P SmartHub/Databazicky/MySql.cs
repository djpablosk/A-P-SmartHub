using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using A_P_SmartHub.Type_devices_with_graphics;
using DotNetEnv;
using MySqlConnector;


namespace A_P_SmartHub.Databazicky
{
    public class MySql
    {
        
            public string HomeName { get; set; }
            public string UserName { get; set; }
            public bool Islogged_ {  get; set; }
            public string City { get; set; }
        
        public string getConn()
        {

            Env.Load();
            string connStr = Environment.GetEnvironmentVariable("MysqlConn");
            return connStr;
        }

        public async Task AddDevice(string id, string devicename, string ipadress, string devicetype)
        {
           using (var conn = new MySqlConnection(getConn()))
            {
                await conn.OpenAsync();
                var addDevice = conn.CreateCommand();
                addDevice.CommandText = @"
                INSERT INTO devices (Id, DeviceName,IpAddress,DeviceType)
                 VALUES (@id, @devicename, @ipadress, @devicetype);
";
                addDevice.Parameters.AddWithValue("@id", id);
                addDevice.Parameters.AddWithValue("@devicename", devicename);
                addDevice.Parameters.AddWithValue("@ipadress", ipadress);
                addDevice.Parameters.AddWithValue("@devicetype", devicetype);

                await addDevice.ExecuteNonQueryAsync();


            }
        }

        public async Task<List<DeviceType>> LoadDevices(string id)
        {
            var devices = new List<DeviceType>();

            using var conn = new MySqlConnection(getConn());
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
        SELECT DeviceName, IpAddress, DeviceType
        FROM devices 
        WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                
                devices.Add(new DeviceType
                {
                    DeviceName = reader.GetString("DeviceName"),
                    IpAddress = reader.GetString("IpAddress"),  
                    Type = Enum.Parse<DeviceTypeEnum>(reader.GetString("DeviceType"))
                });
              //  MessageBox.Show($"pridane zariadenia");
            }

            return devices;
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
                  //  MessageBox.Show(reader["UserName"].ToString());
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
        public async Task UpdateUser(string HomeName,string UserName,string City,string Id)
        {
            using var conn = new MySqlConnection(getConn());
            await conn.OpenAsync();
            using var updateUser = conn.CreateCommand();
            updateUser.CommandText = @"
            UPDATE apdefaultinfos
            SET HomeName = COALESCE(@HomeName, HomeName),
            UserName = COALESCE(@UserName, UserName),
            City = COALESCE(@City,City)
            
        WHERE Id = @Id;";
            updateUser.Parameters.AddWithValue("@Id", Id);
            updateUser.Parameters.AddWithValue("@UserName", UserName);
            updateUser.Parameters.AddWithValue("@HomeName", HomeName);
            updateUser.Parameters.AddWithValue("@City",City);

         await   updateUser.ExecuteNonQueryAsync();


        }
        public async Task IsLogged(string id)
        {
            using var conn = new MySqlConnection(getConn());
            await conn.OpenAsync();
            using var loggerd = conn.CreateCommand();
            loggerd.CommandText = @"
         SELECT SpotifyLogged
         FROM apdefaultinfos
        WHERE @id = Id;";
            loggerd.Parameters.AddWithValue("@id", id);
            using var Reader = await loggerd.ExecuteReaderAsync();


            if (await Reader.ReadAsync())
            {
                Islogged_ = Convert.ToBoolean(Reader["SpotifyLogged"]);
            }
                

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
        public async Task SpotifyLogin(string id, string refreshtoken,bool islogged)
        {
            using var connection = new MySqlConnection(getConn());
           await connection.OpenAsync();
            var spotifyLogin = connection.CreateCommand();
            spotifyLogin.CommandText = @"
            UPDATE apdefaultinfos
             SET RefreshToken = @refreshtoken,
                SpotifyLogged = @islogged
            WHERE Id = @id;";
            spotifyLogin.Parameters.AddWithValue("@refreshtoken", refreshtoken);
            spotifyLogin.Parameters.AddWithValue("@islogged", islogged);
            spotifyLogin.Parameters.AddWithValue("@id", id);
             spotifyLogin.ExecuteNonQuery();
        }

        public async Task ReturnSpotifyRefresh(string id)
        {
            using var conn = new MySqlConnection(getConn());
            await conn.OpenAsync();
            var returnid = conn.CreateCommand();
            returnid.CommandText = @"
        SELECT RefreshToken
        FROM apdefaultinfos
        WHERE Id = @id;";
            returnid.Parameters.AddWithValue("@id", id);
            using var reader = await returnid.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                SmartHubRAM.SpotifyRefreshKey = reader["RefreshToken"].ToString();
            }
            else
            {
                SmartHubRAM.SpotifyRefreshKey = "Err404";
            }
        }

        public async Task DeleteDevice(string id, string DeviceIp,string devicetype)
        {
            using var conn = new MySqlConnection(getConn());
            await conn.OpenAsync();
            var deleteDevice = conn.CreateCommand();
            deleteDevice.CommandText = @"
        DELETE FROM devices
        WHERE Id = @id AND
        IpAddress = @DeviceIp
        AND DeviceType =@type;";
            deleteDevice.Parameters.AddWithValue("@id", id);
            deleteDevice.Parameters.AddWithValue("@DeviceIp",DeviceIp);
            deleteDevice.Parameters.AddWithValue("@type",devicetype);
            await deleteDevice.ExecuteNonQueryAsync();
        }
    } // trosku spagetka ci ? 
}
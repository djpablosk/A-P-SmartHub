using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json.Linq;
using A_P_SmartHub.Databazicky;
using A_P_SmartHub;
using System.Text.Json;

namespace A_P_SmartHub.spotify
{
    public class SpotifyConnector // akoze snad vam je jasne ze som si pomohol aj Ai Aj dokumentaciou ale pre mna je Oauth Momentalne dost narocny a snazil som sa pochopit co to robi kde ako preco dost dlho a nakoniec sa celkom podarilo <33
    {
        public string accesKey { get; set; }
        public string RefreshKey { get; set; }
        private const string spotifyClientId = "033085e7f0c54e6dbc9b00da00bc3f6b";
        private const string spotifyRedirectUri = "http://127.0.0.1:5000/callback/";

        private string pkceCodeVerifier;
        private string spotifyAccessToken;

        private HttpListener listener;

        public async Task SpotifyLogin()
        {
            pkceCodeVerifier = CreatePkceCodeVerifier();
            string pkceCodeChallenge = CreatePkceCodeChallenge(pkceCodeVerifier);

            string spotifyLoginUrl =
                "https://accounts.spotify.com/authorize" +
                "?response_type=code" +
                "&client_id=" + spotifyClientId +
                "&scope=user-read-currently-playing" +
                "&redirect_uri=" + Uri.EscapeDataString(spotifyRedirectUri) +
                "&code_challenge_method=S256" +
                "&code_challenge=" + pkceCodeChallenge;

            Process.Start(new ProcessStartInfo(spotifyLoginUrl)
            {
                UseShellExecute = true
            });



            var authorizationCode = await WaitForSpotifyCallback();

            spotifyAccessToken = await ExchangeCodeForAccessToken(authorizationCode);

            await LoadCurrentlyPlaying();
        }

        private async Task<string> WaitForSpotifyCallback()
        {
            listener = new HttpListener();
           listener.Prefixes.Add("http://127.0.0.1:5000/");
            listener.Start();

            try
            {
                var context = await listener.GetContextAsync();
                string code = context.Request.QueryString["code"];

                byte[] buffer = Encoding.UTF8.GetBytes("OK, You May Return to the A&P SmartHub APP");
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Close();

                return code;
            }
            finally
            {
                StopListener();
            }
        }

        private void StopListener()
        {
            try
            {
                if (listener != null)
                {
                    if (listener.IsListening)
                        listener.Stop();

                    listener.Close();
                    listener = null;
                }
            }
            catch { }
        }

        private async Task<string> ExchangeCodeForAccessToken(string authorizationCode)
        {
            using var httpClient = new HttpClient();

            var requestData = new List<KeyValuePair<string, string>>
            {
                new("client_id", spotifyClientId),
                new("grant_type", "authorization_code"),
                new("code", authorizationCode),
                new("redirect_uri", spotifyRedirectUri),
                new("code_verifier", pkceCodeVerifier)
            };

            var response = await httpClient.PostAsync(
                "https://accounts.spotify.com/api/token",
                new FormUrlEncodedContent(requestData)
            );

            string json = await response.Content.ReadAsStringAsync();
            var obj = JObject.Parse(json);
            SmartHubRAM.spotifyAcceskey = obj["access_token"]?.ToString();
            SmartHubRAM.SpotifyRefreshKey = obj["refresh_token"]?.ToString();

           
            if (!string.IsNullOrEmpty(SmartHubRAM.SpotifyRefreshKey) && !string.IsNullOrEmpty(SessionInfo.ID))
            {
                MySql sql = new MySql();
                await sql.SpotifyLogin(SessionInfo.ID, SmartHubRAM.SpotifyRefreshKey,true);
            }

            return obj["access_token"].ToString();
        }

        public async Task LoadCurrentlyPlaying()
        {
            try
            {
                if (string.IsNullOrEmpty(SmartHubRAM.spotifyAcceskey))
                {
                    if (string.IsNullOrEmpty(SmartHubRAM.SpotifyRefreshKey) || SmartHubRAM.SpotifyRefreshKey == "Err404")
                        return;
                    bool refreshed = await RefreshAccessToken();
                    if (!refreshed) return;
                }



               
                    
                

                using var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", SmartHubRAM.spotifyAcceskey);

                var response = await httpClient.GetAsync(
                    "https://api.spotify.com/v1/me/player/currently-playing"
                );

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                   

                   
                    return;
                }

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                   
                    SmartHubRAM.currentlyPlaying = $"No Songs Are Playing At The Moment";
                }
                

                string json = await response.Content.ReadAsStringAsync();
                var obj = JObject.Parse(json);

                var item = obj["item"];
                if (item == null) return;

               

                string songName = item["name"]?.ToString();
                string artistName = item["artists"]?[0]?["name"]?.ToString();

                SmartHubRAM.currentlyPlaying = $"Now Playing {songName} -- {artistName}";

           //  MessageBox.Show($"Now playing: {songName} - {artistName}");
            }
            catch
            {
                // nič nenechaj crashnúť UI
            }
                }

        private string CreatePkceCodeVerifier()
        {
            byte[] bytes = new byte[32];
            RandomNumberGenerator.Fill(bytes);
            return Base64UrlEncode(bytes);
        }

        private string CreatePkceCodeChallenge(string verifier)
        {
            using var sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(verifier));
            return Base64UrlEncode(hash);
        }

        private string Base64UrlEncode(byte[] data)
        {
            return Convert.ToBase64String(data)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");

        }
        public async Task<bool> RefreshAccessToken()
        {
            try
            {
                using var httpClient = new HttpClient();
                var requestData = new List<KeyValuePair<string, string>>
                {
                    new("client_id", spotifyClientId),
                    new("grant_type", "refresh_token"),
                    new("refresh_token", SmartHubRAM.SpotifyRefreshKey)
                };

                var response = await httpClient.PostAsync(
                    "https://accounts.spotify.com/api/token",
                    new FormUrlEncodedContent(requestData)
                );

                string json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    // token je dead → force re-login
                    SmartHubRAM.spotifyAcceskey = null;
                    SmartHubRAM.SpotifyRefreshKey = null;
                    return false;
                }

                var obj = JObject.Parse(json);
                SmartHubRAM.spotifyAcceskey = obj["access_token"]?.ToString();

                
                var newRefresh = obj["refresh_token"]?.ToString();
                if (!string.IsNullOrEmpty(newRefresh))
                {
                    SmartHubRAM.SpotifyRefreshKey = newRefresh;
                    
                    // IF rotating refresh token, SAVE TO DB AGAIN!
                    if (!string.IsNullOrEmpty(SessionInfo.ID))
                    {
                        MySql sql = new MySql();
                        await sql.SpotifyLogin(SessionInfo.ID, SmartHubRAM.SpotifyRefreshKey, true);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
using A_P_SmartHub.Databazicky;
using DotNetEnv;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace A_P_SmartHub.Weather
{
    public class getData
    {
        public int Temperature { get; set; }
     
        public async Task  getTemperature(string city)
        {
          //  Env.Load();
            
          //  using HttpClient client = new HttpClient();
          //  string weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={Environment.GetEnvironmentVariable("WeatherApi")}&units=metric";
          // HttpResponseMessage httpResponseMessage = await client.GetAsync(weatherUrl);
          //  string data  = await httpResponseMessage.Content.ReadAsStringAsync();
          // JsonDocument jsonDocument = JsonDocument.Parse(data);
          //  double temp = jsonDocument.RootElement.GetProperty("main").GetProperty("temp").GetDouble();
          //Temperature = (int)Math.Round(temp);
         

            

        }
    }
}

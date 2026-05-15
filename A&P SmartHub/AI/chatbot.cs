using A_P_SmartHub.Databazicky;
using A_P_SmartHub.Weather;
using DotNetEnv;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows;

namespace A_P_SmartHub.AI
{
    internal class Chatbot
    {
       
        MySql sql = new MySql();
        public int currentWeather {  get; set; }
        public string currentCity { get; set; }
        public string username {  get; set; }

        public string GasLevel {  get; set; }
        public string DHT11Temp { get; set; }
        public async Task <string> AiChat(string userInput,getData weather) // asi to je jasne ale trosku som si pomohol ai :)
        {
           await Setter(weather);
            Env.Load();
            var api = Environment.GetEnvironmentVariable("GroqApi");
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {api}");

                var body = new
                {
                    model = "llama-3.3-70b-versatile",
                    messages = new[]
                    {
                    new {role = "system",content = @$"{prompt}, current Temperature in Users City is {currentWeather} degree celsius, Users Home / City is in
                {currentCity}, user has set its username to {username}" }, // potom pridam senzory
                    new {role = "user",content = userInput}
                }

                };
                var serializer = JsonSerializer.Serialize(body);
                var content = new StringContent(serializer, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://api.groq.com/openai/v1/chat/completions", content);
                var json = await response.Content.ReadAsStringAsync();
                var aiDoc = JsonDocument.Parse(json);
                // MessageBox.Show(json);
                string AIAnswer = aiDoc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
                return AIAnswer;
            }
            catch (Exception ex)
            {

                return "Looks Like We Couldn't Connect to the Server, Please Try Again Later";
            }
        }
       public async Task Setter(getData Weather)
        {
           await sql.ReturnBasicFromDB(SessionInfo.ID);
            currentCity = sql.City;
            username = sql.UserName;

            await Weather.getTemperature(currentCity);
            currentWeather = Weather.Temperature;
        }

        

        private string prompt = @"# ROLE AND IDENTITY
You are ""James"", a professional, smart, and highly efficient AI Assistant for the ""A&P Smart Hub"". Your primary mission is to help users optimize their smart home environment, focusing heavily on energy conservation, optimal ventilation practices, indoor air quality (IAQ), and the efficient operation of connected smart appliances.

# STRICT LANGUAGE RULE
- You must ALWAYS respond in English, regardless of the language the user uses to address you. Even if the user speaks in Slovak or any other language, your response must remain strictly in professional and clear English.

# INITIAL GREETING (STRICT AND ABSOLUTE RULE)
In your very first message of the conversation, you must greet the user EXACTLY with this text, word-for-word, without any variations or additions:
""Hi! I am James, your AI assistant for the A&P Smart Hub. How can I help you today?""

# CORE OPERATIONAL PRINCIPLES

1. TONE MATCHING & PROFESSIONALISM
- While keeping your language strictly English, adapt to the user's conversational style (casual, formal, or technical) while maintaining a polite, respectful, and helpful demeanor. Avoid overly robotic, generic, or bureaucratic phrasing.

2. ACCURACY, FACT-CHECKING & CONTEXT AWARENESS
- Never hallucinate features, invent non-existent smart hub capabilities, or provide unsafe advice.
- When advising on ventilation, recommend real-world best practices: advocate for intensive, short-duration shock ventilation or cross-ventilation (5–10 minutes) rather than leaving windows tilted open all day, which wastes energy.
- Data Dependency: If a user asks for precise automation rules or specific advice that depends on environmental factors, politely ask for their current sensor data (e.g., indoor CO2 levels, humidity, or outdoor temperature) if it is not provided in the context.

3. SMART HOME EFFICIENCY & VALUE DELIVERY
- Deliver proactive, eco-friendly, and cost-effective recommendations.
- Always provide the ""why"" behind an action. Explain the energy-saving or health logic (e.g., ""Turning down your smart thermostat while windows are open prevents your heating system from working in overdrive and wasting energy"").
- Structure your responses logically. Use concise bullet points and short paragraphs optimized for easy scanning on a smart hub dashboard or mobile application screen.

4. MANNER & RESOLUTION
- Avoid using excessive slang unless explicitly initiated by the user.
- Maintain a non-confrontational attitude. If a user disagrees or expresses a preference, adapt gracefully and offer alternative, safe, and efficient solutions.

# EXAMPLE SCENARIOS & SYSTEM LOGIC

- Scenario A (Slovak Input):
  User: ""Ako mám teraz správne vyvetrať?""
  James's Logic: Detect Slovak language but enforce the English rule. Respond in English. Provide structured advice on shock ventilation and remind them to temporarily turn off smart thermostatic valves (TRVs) to save heating energy.

- Scenario B (English Input):
  User: ""How can I reduce my electricity bill with the smart hub?""
  James's Logic: Respond in English. Provide 3–4 highly actionable, scannable tips (e.g., configuring automated heating/cooling schedules, utilizing eco-modes on smart appliances, and leveraging occupancy sensors).";
    }

   

}
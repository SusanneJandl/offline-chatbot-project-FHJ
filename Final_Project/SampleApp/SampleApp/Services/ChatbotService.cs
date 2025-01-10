using System.Net.Http;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace SampleApp.Services
{
    public class ChatbotService
    {
        private readonly HttpClient _httpClient;

        // Constructor injection for HttpClient
        public ChatbotService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task StartChatbot(string page)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/startbot");

                // Build the JSON body
                var jsonBody = JsonConvert.SerializeObject(new
                {
                    topic = page
                });
                request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                // Send the request
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                // Read the response
                await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Chatbot started successfully.");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error starting chatbot: {errorMessage}");
                }
            
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }
}
    }
}

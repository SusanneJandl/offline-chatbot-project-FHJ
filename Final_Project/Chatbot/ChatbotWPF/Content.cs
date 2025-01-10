using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ChatbotWPF
{

    internal class Content
    {

        public async Task<string> RetrieveVSAsync(string query)
        {
            string newContent = "";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // POST request (not GET)
                    var request = new HttpRequestMessage(HttpMethod.Post, Consts.SEND);

                    // Build the JSON body
                    var jsonBody = JsonConvert.SerializeObject(new { question = query, topic = Environment.GetEnvironmentVariable("TOPIC")
                });
                    request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Send the request
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    // Read the response
                    newContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(newContent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                newContent = "Something went wrong.";
            }

            return newContent;
        }


    }
}

using Microsoft.AspNetCore.Components;

namespace SampleApp.Components.Pages
{
    partial class Dog
    {
        [Inject]
        private Services.ChatbotService? DogBotService { get; set; }
        public async Task StartDogBot()
        {
            await DogBotService!.StartChatbot("Dogs");
        }

    }
}

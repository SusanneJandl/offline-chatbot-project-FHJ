using Microsoft.AspNetCore.Components;

namespace SampleApp.Components.Pages
{
    partial class Bird
    {
        [Inject]
        private Services.ChatbotService? BirdBotService { get; set; }
        public async Task StartBirdBot()
        {
            await BirdBotService!.StartChatbot("Birds");
        }
    }
}

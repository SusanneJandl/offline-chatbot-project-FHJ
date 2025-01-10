using OllamaSharp;

namespace ChatbotWPF
{
    internal class Conversation
    {
        public async Task WithContextAsync()
        {
            string memory = "";
            Content content = new();

            while (true)
            {
                memory += "\n";

                // Chat with Ollama
                var chat = new Chat(Consts.OLLAMA);
                Console.WriteLine("\nGib eine Frage ein: ");
                string? input = Console.ReadLine();

                string newContent = await content.RetrieveVSAsync(input!);
                memory += $"User: {input} \n Assistant: ";
                string prompt = $"{input} \n Nutze in erster Linie folgende Informationen für die Antwort: {newContent}. Beachte auch den Inhalt der bisherigen Konversation, falls nicht leer: {memory}";
                Console.WriteLine($"\nPrompt: {prompt}");
                try
                {
                    await foreach (var answerToken in chat.SendAsync(prompt))
                    {
                        Console.Write(answerToken);
                        memory += answerToken;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in chat response: {ex.Message}");
                    memory += "Error: Unable to retrieve response.\n";
                }
            }
        }

        public async Task WithoutContextAsync()
        {
            string message;
            var chat = new Chat(Consts.OLLAMA);
            var messages = chat.Messages;

            while (true)
            {
                chat = new Chat(Consts.OLLAMA);

                Console.WriteLine("\nGib eine Frage ein: ");
                message = Console.ReadLine();
                messages = chat.Messages;

                if (message.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                else
                {
                    await foreach (var answerToken in chat.SendAsync(message))
                        Console.Write(answerToken);
                }
            }
        }

    }
}

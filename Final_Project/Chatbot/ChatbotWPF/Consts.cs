using OllamaSharp;

namespace ChatbotWPF
{
    internal class Consts
    {
        public static readonly Uri URI = new("http://localhost:11434");
        public static readonly OllamaApiClient OLLAMA = new OllamaApiClient(URI);
        public static readonly Uri SEND = new("http://localhost:5000/query");
        public static readonly string OLLAMA_MODEL = "llama3.2:1b";
    }
}

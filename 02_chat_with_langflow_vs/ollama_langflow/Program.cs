using OllamaSharp;
using Python.Included;
using Python.Runtime;

Installer.InstallPath = Path.GetFullPath(".");

// Install Python and required modules
await Installer.SetupPython();
await Installer.PipInstallModule("requests");
await Installer.PipInstallModule("langflow_prompt");

AppContext.SetSwitch("System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization", true);

// Function to generate context using Python
string Context(string question)
{
    string context = "";
    // Initialize Python engine
    PythonEngine.Initialize();
    using (Py.GIL())
    {
        try
        {
            dynamic prompt_generator = Py.Import("langflow_prompt");
            context = prompt_generator.query_langflow(question).ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching context: {ex.Message}");
            context = "Keine zusätzlichen Informationen verfügbar.";
        }
    }
    PythonEngine.Shutdown();
    return context;
}

// Chat variables
string memory = "";
string message = "";
var uri = new Uri("http://localhost:11434");
var ollama = new OllamaApiClient(uri);

ollama.SelectedModel = "llama3.2:3b";

while (true)
{
    memory += "\n";

    // Chat with Ollama
    var chat = new Chat(ollama);
    Console.WriteLine("\nGib eine Frage ein: ");
    var input = Console.ReadLine();
    
    message = $"message: {input}";
    var context = $"context: {Context(message)}";
    string prompt = $"Answer the question: {message} in the language it was asked. \n User the following information for your answer: {context}. Be aware of the conversation so far: {memory}.";
    memory = $"User: {message} \n Assistant: ";

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

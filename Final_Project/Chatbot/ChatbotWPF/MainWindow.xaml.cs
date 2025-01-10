using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using OllamaSharp;

namespace ChatbotWPF
{
    public partial class MainWindow : Window
    {
        private Chat _chat;

        // ObservableCollection to automatically update UI
        public ObservableCollection<string> ChatHistory { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            _chat = new Chat(Consts.OLLAMA); // Initialize the chat object
            Consts.OLLAMA.SelectedModel = Consts.OLLAMA_MODEL;
            ChatHistory = new ObservableCollection<string>();
            DataContext = this;
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            await SendMessage();
        }

        private async void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                await SendMessage();
            }
        }

        private async Task SendMessage()
        {
            string input = UserInput.Text;
            if (string.IsNullOrWhiteSpace(input))
                return;

            // Display user prompt in UI
            ChatHistory.Add($"You: {input}");
            string history = "";
            foreach (string item in ChatHistory)
            {
                history += item;
            }
            Content content = new();
            string context = await content.RetrieveVSAsync(input);
            Debug.WriteLine($"\n\n\n\nContext: {context}\n\n\n\n");
            Debug.WriteLine($"\n\n\n\nHistory: {history}\n\n\n\n");

            string prompt = $"Use the following information primarily for the answer: {context}. Also consider the content of the previous conversation, if not empty: {history}";
            ChatHistory.Add("Bot: ");
            int botIndex = ChatHistory.Count - 1;

            UserInput.Clear();

            // Create a new chat each time, matching your console logic
            var chat = new Chat(Consts.OLLAMA);

            // Stream tokens
            await foreach (var token in chat.SendAsync(prompt))
            {
                //Debug.Write(" new token: " + token);

                Dispatcher.Invoke(() =>
                {
                    ChatHistory[botIndex] += token;
                    ChatHistoryListBox.Items.Refresh();
                    ChatHistoryListBox.UpdateLayout();
                    ChatHistoryListBox.ScrollIntoView(ChatHistory[botIndex]);
                });

                // Artificial delay so you *see* the tokens arrive over time
                await Task.Delay(50);
            }

        }
    }
}

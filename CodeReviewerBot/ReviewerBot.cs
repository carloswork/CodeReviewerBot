using OpenAI.Chat;

namespace CodeReviewerBot
{
    /// <summary>
    /// A bot that reviews C# code snippets using OpenAI's GPT-4o-mini model.
    /// </summary>
    public class ReviewerBot
    {
        private readonly ChatClient _chatClient;
        private readonly List<ChatMessage> _history;
        private readonly List<ChatEntry> _entries;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewerBot"/> class.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="history"></param>
        /// <param name="entries"></param>
        public ReviewerBot(string apiKey, List<ChatMessage>? history = null, List<ChatEntry>? entries = null)
        {
            _chatClient = new ChatClient("gpt-4o-mini", apiKey);
            _history = history ?? new List<ChatMessage> { PromptBuilder.BuildReviewerPrompt() };
            _entries = entries ?? new List<ChatEntry> { new ChatEntry("system", _history[0].Content[0].Text) };
        }

        public List<ChatEntry> GetEntries() => _entries;

        /// <summary>
        /// Reviews the provided C# code snippet and prints the review to the console.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ReviewCodeAsync(string input)
        {
            var (command, code) = CommandParser.Parse(input);

            if (command == ReviewerCommand.Help)
            {
                HelpMenu.ShowHelp();
                return;
            }

            string formattedInput = PromptFormatHelper.GetFormattedPrompt(command, code, input);

            _history.Add(new UserChatMessage(formattedInput));
            _entries.Add(new ChatEntry("user", formattedInput));

            var response = await _chatClient.CompleteChatAsync(_history);
            var reply = response.Value.Content[0].Text;

            Console.WriteLine("Assistant:\n");
            Console.WriteLine(reply);
            Console.WriteLine();

            _history.Add(new AssistantChatMessage(reply));
            _entries.Add(new ChatEntry("assistant", reply));

            UsageMeter.ShowUseage(response.Value.Usage);
        }
    }
}

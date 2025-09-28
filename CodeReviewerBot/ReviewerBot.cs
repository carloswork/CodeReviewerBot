
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

        // Pricing constants (gpt-4o-mini)
        private const decimal InputCostPer1M = 0.15m;
        private const decimal OutputCostPer1M = 0.60m;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewerBot"/> class.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="history"></param>
        /// <param name="entries"></param>
        public ReviewerBot(string apiKey, List<ChatMessage>? history = null, List<ChatEntry>? entries = null)
        {
            _chatClient = new ChatClient("gpt-4o-mini", apiKey);
            _history = history ?? new List<ChatMessage>
            {
                PromptBuilder.BuildReviewerPrompt()
            };

            _entries = entries ?? new List<ChatEntry>
            {
                new ChatEntry
                {
                    Role = "system",
                    Content = _history[0].Content[0].Text,
                    Timestamp = DateTime.UtcNow
                }
            };
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
                ShowHelp();
                return;
            }

            string formattedInput = GetFormattedPrompt(command, code, input);

            _history.Add(new UserChatMessage(formattedInput));
            _entries.Add(new ChatEntry
            {
                Role = "user",
                Content = formattedInput,
                Timestamp = DateTime.UtcNow
            });

            var response = await _chatClient.CompleteChatAsync(_history);
            var reply = response.Value.Content[0].Text;

            Console.WriteLine("Assistant:\n");
            Console.WriteLine(reply);
            Console.WriteLine();

            _history.Add(new AssistantChatMessage(reply));
            _entries.Add(new ChatEntry
            {
                Role = "assistant",
                Content = reply,
                Timestamp = DateTime.UtcNow
            });

            ShowUseage(response.Value.Usage);
        }

        /// <summary>
        /// Display token usage and estimated cost.
        /// </summary>
        /// <param name="usage"></param>
        private void ShowUseage(ChatTokenUsage usage)
        {
            var inputTokens = usage.InputTokenCount;
            var outputTokens = usage.OutputTokenCount;
            var totalTokens = usage.TotalTokenCount;

            decimal inputCost = (inputTokens / 1_000_000m) * InputCostPer1M;
            decimal outputCost = (outputTokens / 1_000_000m) * OutputCostPer1M;
            decimal totalCost = inputCost + outputCost;

            Console.WriteLine($"Tokens: {inputTokens} in, {outputTokens} out (total {totalTokens})");
            Console.WriteLine($"Cost: ${totalCost:F6}");
            Console.WriteLine();
        }

        /// <summary>
        /// Display available commands.
        /// </summary>
        private void ShowHelp()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("  refactor <code>    - Refactor the code");
            Console.WriteLine("  review <code>      - Review the code");
            Console.WriteLine("  unittest <code>    - Create unit tests");
            Console.WriteLine("  help               - Show this help message");
            Console.WriteLine("  exit               - Quit the reviewer");
            Console.WriteLine();
        }

        /// <summary>
        /// Format the user input based on the command type.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="code"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetFormattedPrompt(ReviewerCommand command, string code, string input)
        {
            return command switch
            {
                ReviewerCommand.Review => 
                    $"Review the following C# code. Provide:\n" +
                    $"1. Strengths\n2. Issues\n3. Suggestions\n\nCode:\n{code}",

                ReviewerCommand.Refactor =>
                    $"Refactor the following C# code to improve clarity, style, and performance.\n" +
                    $"Only output the improved code, without explanation.\n\nCode:\n{code}",

                ReviewerCommand.UnitTest => 
                    $"Write MSTest unit tests for the following C# code. " +
                    $"Include test class and methods.\n\nCode:\n{code}",

                ReviewerCommand.Explain => $"Explain step by step what the following C# code does:\n\n{code}",

                _ => input // fallback, just pass it directly
            };
        }
    }
}

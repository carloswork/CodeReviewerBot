using CodeReviewerBot;
using OpenAI.Chat;

class Program
{
    static async Task Main()
    {
        var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

        // Ask user if they want to load an existing session
        Console.WriteLine("Welcome to Code Reviewer Bot");
        Console.WriteLine("Type 'new' for a fresh session or 'load <filename>' to continue a saved one.");
        Console.Write("> ");
        var startCmd = Console.ReadLine();

        List<ChatMessage>? history = null;
        List<ChatEntry>? entries = null;

        if (!string.IsNullOrWhiteSpace(startCmd) && startCmd.StartsWith("load "))
        {
            var fileName = startCmd.Substring(5).Trim();
            if (File.Exists(fileName))
            {
                history = HistorySaver.LoadFromJson(fileName, out entries);
                Console.WriteLine($"Loaded session from {fileName}");
            }
            else
            {
                Console.WriteLine("File not found. Starting a new session.");
            }
        }

        var reviewer = new ReviewerBot(apiKey, history, entries);

        Console.WriteLine("Type 'help' for commands, 'exit' to quit.\n");

        while (true)
        {
            Console.Write("You: ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input) ||
                input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                break;

            await reviewer.ReviewCodeAsync(input);
        }

        // Save when quitting
        var saveName = $"ReviewerBot_{DateTime.Now:yyyyMMdd_HHmmss}.json";
        HistorySaver.SaveToJson(reviewer.GetEntries(), saveName);
        Console.WriteLine($"Conversation saved to {saveName}");
    }
}

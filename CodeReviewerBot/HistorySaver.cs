using OpenAI.Chat;
using System.Text.Json;

namespace CodeReviewerBot
{
    /// <summary>
    /// Saves and loads chat history to/from JSON files.
    /// </summary>
    public static class HistorySaver
    {
        /// <summary>
        /// Saves chat history to a JSON file.
        /// </summary>
        /// <param name="entries"></param>
        /// <param name="filePath"></param>
        public static void SaveToJson(List<ChatEntry> entries, string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(filePath, JsonSerializer.Serialize(entries, options));
        }

        /// <summary>
        /// Loads chat history from a JSON file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="entries"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static List<ChatMessage> LoadFromJson(string filePath, out List<ChatEntry> entries)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("The specified history file does not exist.", filePath);
            var json = File.ReadAllText(filePath);
            entries = JsonSerializer.Deserialize<List<ChatEntry>>(json);

            return entries!.Select<ChatEntry, ChatMessage>(e => e.Role switch
            {
                "user" => new UserChatMessage(e.Content),
                "assistant" => new AssistantChatMessage(e.Content),
                "system" => new SystemChatMessage(e.Content),
                _ => throw new InvalidOperationException($"Unknown role: {e.Role}")
            }).ToList();
        }
    }
}

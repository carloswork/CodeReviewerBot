namespace CodeReviewerBot
{
    /// <summary>
    /// Represents a single entry in a chat conversation.
    /// </summary>
    public class ChatEntry
    {
        public ChatEntry(string role, string content, DateTime timestamp) 
        {
            Role = role;
            Content = content;
            Timestamp = timestamp;
        }

        public ChatEntry(string role, string content) : this(role, content, DateTime.UtcNow)
        {
        }

        public string Role { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    }
}

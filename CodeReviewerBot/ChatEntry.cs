namespace CodeReviewerBot
{
    /// <summary>
    /// Represents a single entry in a chat conversation.
    /// </summary>
    public class ChatEntry
    {
        public string Role { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}

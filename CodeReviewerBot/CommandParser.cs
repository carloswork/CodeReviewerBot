namespace CodeReviewerBot
{
    /// <summary>
    /// Parses user commands for the code reviewer bot.
    /// </summary>
    public static class CommandParser
    {
        /// <summary>
        /// Parses the input string and returns the corresponding command and code snippet.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static (ReviewerCommand command, string code) Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return (ReviewerCommand.None, string.Empty);
            
            var parts = input.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) return (ReviewerCommand.None, string.Empty);

            return parts[0].ToLower() switch
            {
                "refactor" => (ReviewerCommand.Refactor, parts.Length > 1 ? parts[1] : string.Empty),
                "review" => (ReviewerCommand.Review, parts.Length > 1 ? parts[1] : string.Empty),
                "unittest" => (ReviewerCommand.UnitTest, parts.Length > 1 ? parts[1] : string.Empty),
                "explain" => (ReviewerCommand.Explain, parts.Length > 1 ? parts[1] : string.Empty),
                "help" => (ReviewerCommand.Help, string.Empty),
                _ => (ReviewerCommand.None, input)
            };
        }
    }
}

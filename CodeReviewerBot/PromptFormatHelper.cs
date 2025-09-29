namespace CodeReviewerBot
{
    /// <summary>
    /// Formats prompts based on the command type.
    /// </summary>
    public static class PromptFormatHelper
    {
        /// <summary>
        /// Formats the prompt based on the command and code snippet.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="code"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetFormattedPrompt(ReviewerCommand command, string code, string input)
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

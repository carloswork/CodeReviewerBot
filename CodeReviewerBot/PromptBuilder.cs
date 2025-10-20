using OpenAI.Chat;

namespace CodeReviewerBot
{
    /// <summary>
    /// Builds prompts for the AI code reviewer.
    /// </summary>
    public static class PromptBuilder
    {
        /// <summary>
        /// Builds the system prompt for code review.
        /// </summary>
        /// <returns></returns>
        public static SystemChatMessage BuildReviewerPrompt()
        {
            string prompt = "You are a senior C# code reviewer. " +
            "When I paste C# code, review it in 3 sections:\n" +
            "1. Strengths\n" +
            "2. Issues / Bugs\n" +
            "3. Suggested Improvements\n" +
            "Be concise but specific. Use bullet points." +
            "If I ask some questions that are not related to C# code, let me know and do not answer it.";

            return new SystemChatMessage(prompt);
        }
    }
}

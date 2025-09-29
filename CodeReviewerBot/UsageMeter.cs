using OpenAI.Chat;

namespace CodeReviewerBot
{
    /// <summary>
    /// Tracks and displays token usage and estimated costs for OpenAI API calls.
    /// </summary>
    public static class UsageMeter
    {
        // Pricing constants (gpt-4o-mini)
        private const decimal InputCostPer1M = 0.15m;
        private const decimal OutputCostPer1M = 0.60m;

        /// <summary>
        /// Displays token usage and estimated costs to the console.
        /// </summary>
        /// <param name="usage"></param>
        public static void ShowUsage(ChatTokenUsage usage)
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
    }
}

namespace CodeReviewerBot
{
    /// <summary>
    /// Displays help menu with available commands.
    /// </summary>
    public static class HelpMenu
    {
        /// <summary>
        /// Display available commands.
        /// </summary>
        public static void ShowHelp()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("  refactor <code>    - Refactor the code");
            Console.WriteLine("  review <code>      - Review the code");
            Console.WriteLine("  unittest <code>    - Create unit tests");
            Console.WriteLine("  help               - Show this help message");
            Console.WriteLine("  exit               - Quit the reviewer");
            Console.WriteLine();
        }
    }
}

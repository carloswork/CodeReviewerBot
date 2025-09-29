# 🤖 CodeReviewerBot – C# AI Code Assistant

CodeReviewerBot is a **C# console application** powered by the **OpenAI API**.  
It helps developers by reviewing, refactoring, explaining, and generating unit tests for C# code snippets.  
All conversations are saved in structured **JSON format** with timestamps for future reference.

---

## ✨ Features
- ✅ **Review**: Analyze strengths, issues, and improvements in your C# code.
- 🔄 **Refactor**: Rewrite code for better readability, maintainability, or performance.
- 🧪 **UnitTest**: Generate MSTest unit tests automatically.
- 📖 **Explain**: Step-by-step explanation of what code does.
- 💾 **Session Saving**: Each conversation is saved as a JSON file with timestamps.

---

## 🚀 Getting Started

### 1. Clone the repo
```bash
git clone https://github.com/carloswork/CodeReviewerBot.git
cd CodeReviewerBot
```

### 2. Install dependencies
The project uses the official **OpenAI .NET SDK**:
```powershell
dotnet add package OpenAI
```

### 3. Set your API key
Set the `OPENAI_API_KEY` environment variable.

**Windows (PowerShell):**
```powershell
setx OPENAI_API_KEY "your_api_key_here"
```

**macOS/Linux (bash/zsh):**
```bash
export OPENAI_API_KEY="your_api_key_here"
```

Restart your terminal/IDE after setting the variable.

### 4. Run the app
```powershell
dotnet run
```

---

## 💡 Usage Examples

```
Welcome to Code Reviewer Bot
Type 'help' for commands, 'exit' to quit.

You: review public string GetName(int id) { return id.ToString(); }

Assistant:
✅ Strengths
- Simple implementation
⚠️ Issues
- Method name misleading
💡 Suggestions
- Rename to ConvertIdToString
```

```
You: refactor public string GetName(int id) { return id.ToString(); }

Assistant:
public string ConvertIdToString(int id)
{
    return id.ToString();
}
```

```
You: unittest public int Add(int a, int b) { return a + b; }

Assistant:
[TestClass]
public class AddTests
{
    [TestMethod]
    public void Add_PositiveNumbers_ReturnsSum()
    {
        var result = Add(2, 3);
        Assert.AreEqual(5, result);
    }
}
```

---

## 📂 Saved Sessions
- Conversations are saved automatically when you exit:
  ```
  ReviewerBot_20250928_154512.json
  ```
- To resume a session:
  ```
  load ReviewerBot_20250928_154512.json
  ```

Example JSON structure:
```json
[
  {
    "Role": "user",
    "Content": "review public int Add(int a, int b) { return a + b; }",
    "Timestamp": "2025-09-28T15:45:12Z"
  },
  {
    "Role": "assistant",
    "Content": "✅ Strengths ...",
    "Timestamp": "2025-09-28T15:45:15Z"
  }
]
```

---

## ⚠️ Notes
- Keep your **API key private**. Do not commit it to GitHub.
- API usage costs are small but real. Use `gpt-4o-mini` for low cost.

---

## 📜 License
This project is licensed under the MIT License.

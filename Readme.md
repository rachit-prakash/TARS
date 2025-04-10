# 🛡️ Privacy Policy Chatbot (.NET + Gemini API)

This project is a simple, rule-based chatbot that helps employees query their company's privacy policy. It uses the **Gemini API** (Google AI) to provide smart, context-aware answers by parsing the official policy document (PDF). The backend is built using **ASP.NET Core**.

---

## 🔧 Features

- 🧠 Integrates Google Gemini API for question answering
- 📄 Parses company privacy policy from PDF
- 💬 Accepts natural language questions
- 🚀 Built using .NET for easy enterprise integration
- 🖥️ Web-based interface (MVC / Razor Pages or API)

---

## 📁 Project Structure

/PrivacyPolicyChatbot │ ├── Controllers/ # API or MVC controllers ├── Services/ # Gemini + PDF extraction logic ├── Models/ # Question model / response DTOs ├── wwwroot/ # Static files (if using MVC) ├── Views/ # Razor Views (if applicable) ├── privacy_policy.pdf # Sample privacy policy └── appsettings.json # Gemini API config

yaml
Copy
Edit

---

## 🧪 How It Works

1. User types a question about the privacy policy.
2. The system extracts relevant content from the PDF.
3. Sends the user query + context to Gemini API.
4. Displays an AI-generated answer in the chat window.

---

## 🔑 Setup

### 1. Clone the Repo
```bash
git clone https://github.com/your-username/privacy-policy-chatbot.git
cd privacy-policy-chatbot
2. Configure Gemini API
Update appsettings.json:

json
Copy
Edit
"Gemini": {
  "ApiKey": "YOUR_GEMINI_API_KEY",
  "Model": "gemini-pro"
}
3. Install NuGet Dependencies
bash
Copy
Edit
dotnet restore
4. Run the Project
bash
Copy
Edit
dotnet run
💬 Example Prompt
Q: What does the company do with user personal data?
A: The company stores personal data securely and uses it only for internal HR and analytics purposes. Refer to Section 3.2 of the privacy policy.

📚 Tech Stack
ASP.NET Core

Google Gemini API

iTextSharp or PDFSharp (for PDF parsing)

C#

HTML/CSS/JS (frontend)

📜 License
MIT License. Feel free to fork and improve it!

🤝 Contribution
Contributions are welcome! Submit a PR or open an issue to get involved.

🔗 Contact
Made with ❤️ by [Your Name]
📧 your.email@example.com

yaml
Copy
Edit

---

Let me know:
- Are you using **MVC or Web API**?
- Want me to include sample Gemini integration code too?

I'll tailor it further if needed.

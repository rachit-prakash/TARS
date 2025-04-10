using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using UglyToad.PdfPig;
using System.Collections.Generic;

public class AIAnalysisService
{
    private readonly HttpClient _httpClient;
    private readonly string _geminiApiKey;
    private readonly string _pdfFilePath;

    public AIAnalysisService(HttpClient httpClient, IConfiguration configuration, IWebHostEnvironment env)
    {
        _httpClient = httpClient;
        _geminiApiKey = configuration["Gemini:ApiKey"];
        _pdfFilePath = Path.Combine(env.WebRootPath, "text.pdf");
    }

    public async Task<string> GetResponseAsync(string userQuery)
    {
        if (IsCompanyRelated(userQuery))
        {
            string extractedText = ExtractTextFromPdf();
            return await SearchCompanyPolicies(userQuery, extractedText);
        }
        else
        {
            return await GetGeneralResponseFromGemini(userQuery);
        }
    }

    private bool IsCompanyRelated(string query)
    {
        var keywords = new[] { "policy", "rules", "leave", "benefits", "salary", "hr", "office timing", "work hours", "holidays", "company regulations", "company", "organization", "office", "csharptek", "cloudgarner", "working days", "work days", "Attendance and Punctuality", "National/Festival Holiday", "Casual Leaves", "Planned Leaves", "Sick Leaves", "Total Leaves", "Leave Procedure", "Maternity Leave", " Probationary Period", "Training Period", " Annual Incentive", "Dress Code", "Pay Day", "Retirement", "Notice Period", "Internet Usage", "ID cards", "Sick Leave", "Casual Leave", "Planned Leave", "Total Leave", "attendance and punctuality", "national/festival holiday", "casual leaves", "planned leaves", "sick leaves", "total leaves", "leave procedure", "maternity leave", "probationary period", "training period", " annual incentive", "dress code", "pay day", "retirement", "notice period", "internet usage", "id cards", "id card", "sick leave", "casual leave", "planned leave", "total leave", "vision", "mission", "address", "phone number", "official website","working hours","working hour","Working Hours","Working Hour" };
        return keywords.Any(keyword => query.ToLower().Contains(keyword));
    }

    private string ExtractTextFromPdf()
    {
        if (!File.Exists(_pdfFilePath))
        {
            return "";
        }

        StringBuilder textBuilder = new StringBuilder();
        using (var document = PdfDocument.Open(_pdfFilePath))
        {
            foreach (var page in document.GetPages())
            {
                textBuilder.AppendLine(page.Text);
            }
        }

        return textBuilder.ToString();
    }

    private async Task<string> SearchCompanyPolicies(string query, string documentText)
    {
        if (string.IsNullOrWhiteSpace(documentText))
        {
            return "Company policy document is empty or not found.";
        }

        string prompt = $"Search the document for information related to the query. Return only the relevant details in concise plain-text bullet points using hyphens. Do not include extra formatting, summaries, or paragraph-style responses.\n\nDocument:\n{documentText}\n\nQuery:\n{query}";

        return await QueryGemini(prompt);
    }

    private async Task<string> GetGeneralResponseFromGemini(string query)
    {
        return await QueryGemini(query);
    }

    private async Task<string> QueryGemini(string prompt)
    {
        var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={_geminiApiKey}";

        var requestData = new
        {
            contents = new[]
            {
                new {
                    parts = new[] {
                        new { text = prompt }
                    }
                }
            }
        };

        var json = JsonConvert.SerializeObject(requestData);
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            return "Error fetching response from Gemini.";
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        dynamic result = JsonConvert.DeserializeObject(jsonResponse);
        return result.candidates[0].content.parts[0].text;
    }
}

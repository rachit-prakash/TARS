using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace TARS.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HttpClient _httpClient;

        public IndexModel(ILogger<IndexModel> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;

        }
        public List<ChatMessage> ChatHistory { get; set; } = new List<ChatMessage>();

        public class ChatMessage
        {
            public string Content { get; set; }
            public bool IsUser { get; set; }
        }

        public class UserInputModel
        {
            public string Message { get; set; }
        }

        public async Task<IActionResult> OnPostAsync([FromBody] UserInputModel userInput)
        {
            if (string.IsNullOrEmpty(userInput.Message))
            {
                return new JsonResult(new { response = "Invalid input." });
            }

            // Call API
            var jsonRequest = JsonSerializer.Serialize(new { Message = userInput.Message });
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7287/api/Tally/webhook", content);
            var apiResponse = await response.Content.ReadAsStringAsync();

            return new JsonResult(new { response = apiResponse });
        }

        public void OnGet()
        {

        }
    }
}

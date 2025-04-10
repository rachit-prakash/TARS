using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TARS.Controllers
{
    public class TallyController : Controller
    {
        private readonly AIAnalysisService _aiAnalysisService;

        public TallyController(AIAnalysisService aiAnalysisService)
        {
            _aiAnalysisService = aiAnalysisService;
        }

        // Render the chatbot view
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Tally/GetChatbotResponse")]
        public async Task<IActionResult> GetChatbotResponse([FromBody] QueryModel query)
        {
            if (query == null || string.IsNullOrWhiteSpace(query.Text))
                return BadRequest(new { error = "Invalid query." });

            var response = await _aiAnalysisService.GetResponseAsync(query.Text);
            return Json(new { response });
        }
    }

    public class QueryModel
    {
        public string Text { get; set; }
    }

}

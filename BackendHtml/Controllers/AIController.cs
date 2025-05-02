namespace BackendHtml.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using BackendHtml.Models;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using System.Security.Claims;


    [Authorize]  // Ensure that the user is authenticated to access this controller
    public class AIController : Controller
    {
        private AIRepository _aiRepository;


        public AIController(IConfiguration configuration)
        {
            _aiRepository = new AIRepository(configuration);

        }
        public IActionResult Index()
        {
            return View(_aiRepository.GetAllAIContents());
        }
        public IActionResult Add()
        {
            return View();
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAIContentById(int id)
        {
            var result = await _aiRepository.GetAIContentById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAIContents()
        {
            var result = await _aiRepository.GetAllAIContents();
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AIContent aiContent)
        {
            if (aiContent == null)
            {
                return BadRequest("Invalid AI content data.");
            }

            string? userId;
            if (User.Identity.IsAuthenticated)
            {
                userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userName = User.FindFirst(ClaimTypes.Name)?.Value;
                System.Console.WriteLine($"User ID: {userId}, User Name: {userName}");
            }
            else
            {
                userId = "UnknownUser"; // or handle unauthenticated users as needed
            }
            AIContent temp = new AIContent();
            String response = await Gemini.CallGeminiApi(aiContent.Content,
            "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent", "AIzaSyA9EM4-R8YeI6YwkDJ8qvK84sOeKh1Cl4Y");
            temp.Content = response;
            temp.UserID = userId;
            AIContent result = await _aiRepository.InsertAIContent(temp);
            return CreatedAtAction(nameof(GetAIContentById), new { id = result.Id }, result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAIContent(int id)
        {
            var existingContent = await _aiRepository.GetAIContentById(id);
            if (existingContent == null)
            {
                return NotFound();
            }
            await _aiRepository.DeleteAIContent(id);
            return NoContent();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;

namespace ChatGPTApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OpenAIController : ControllerBase
{
    private readonly ILogger<OpenAIController> _logger;
    private IConfiguration _configuration;

    public OpenAIController(ILogger<OpenAIController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    //[HttpPost]
    //[Route("getanswer")]
    //public IActionResult GetResult([FromBody] string prompt)
    //{
    //    string apiKey = _configuration["OPENAIKEY"];
    //    string answer = string.Empty;
    //    var openai = new OpenAIAPI(apiKey);
    //    CompletionRequest completion = new CompletionRequest();
    //    completion.Prompt = prompt;
    //    completion.Model = OpenAI_API.Models.Model.DavinciText;
    //    completion.MaxTokens = 4000;
    //    var result = openai.Completions.CreateCompletionAsync(completion);
    //    if (result != null)
    //    {
    //        foreach (var item in result.Result.Completions)
    //        {
    //            answer = item.Text;
    //        }
    //        return Ok(answer);
    //    }
    //    else
    //    {
    //        return BadRequest("Not found");
    //    }
    //}

    [HttpGet]
    public async Task<IActionResult> GetData(string input)
    {
        string apiKey = _configuration["OPENAIKEY"];
        string response = "";
        OpenAIAPI openai = new OpenAIAPI(apiKey);
        CompletionRequest completion = new CompletionRequest();
        completion.Prompt = input;
        completion.Model = "text-davinci-003";
        completion.MaxTokens = 4000;
        var output = await openai.Completions.CreateCompletionAsync(completion);
        if (output != null)
        {
            foreach (var item in output.Completions)
            {
                response = item.Text;
            }
            return Ok(response);
        }
        else
        {
            return BadRequest("Not found");
        }
    }
}
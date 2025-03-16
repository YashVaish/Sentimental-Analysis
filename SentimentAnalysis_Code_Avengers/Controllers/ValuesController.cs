using Microsoft.AspNetCore.Mvc;
using SentimentAnalysis_Code_Avengers.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SentimentAnalysis_Code_Avengers.Controllers
{
    [Route("api/Sentiment")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [Route("TestSentiment")]
        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> TestSentiment([FromBody] inputData data)
        {
            return Content("{test}");
        }
    }
}

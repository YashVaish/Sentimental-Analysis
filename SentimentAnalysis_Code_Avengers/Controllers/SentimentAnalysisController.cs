using Amazon.Comprehend.Model;
using Amazon.Comprehend;
using Microsoft.AspNetCore.Mvc;
using Amazon.Runtime;
using SentimentAnalysis_Code_Avengers.Models;
using Newtonsoft.Json;

namespace SentimentAnalysis_Code_Avengers.Controllers
{
    [ApiController]
    public class SentimentAnalysisController:ControllerBase
    {
        [HttpPost]
        [Route("api/GetSentiment")]
        public async Task<IActionResult> DetectingPII([FromBody] inputData data)
        {
            var res = new ContentResult();
            try
            {

                float totalScore;
                var accessKey = "";
                var secretKey = "";
                var credentials = new BasicAWSCredentials(accessKey, secretKey);
                var comprehendClient = new AmazonComprehendClient(credentials, Amazon.RegionEndpoint.USEast1);
                //var text = @"Hello Paul Santos. The latest statement for your credit card account 1111-0000-1111-0000 was
                //            mailed to 123 Any Street, Seattle, WA 98109.";
                SentimentAnalysisResponse sentimentAnalysisResponse = new SentimentAnalysisResponse();

                var replaceText = data.text;
                var request = new DetectPiiEntitiesRequest
                {
                    Text = data.text,
                    LanguageCode = "EN",
                };
                var response = await comprehendClient.DetectPiiEntitiesAsync(request);
                if (response.Entities.Count > 0)
                {
                    sentimentAnalysisResponse.PII = true;
                    foreach (var entity in response.Entities)
                    {
                        replaceText = replaceText.Substring(0, entity.BeginOffset) + new string('*', entity.EndOffset - entity.BeginOffset + 1) + data.text.Substring(entity.EndOffset + 1);
                    }
                }
                else
                {
                    sentimentAnalysisResponse.PII = false;
                }
                sentimentAnalysisResponse.maskedPII = replaceText;
                var detectSentimentRequest = new DetectSentimentRequest()
                {
                    Text = replaceText,
                    LanguageCode = "en",
                };
                var detectSentimentResponse1 = await comprehendClient.DetectSentimentAsync(detectSentimentRequest);
                totalScore = detectSentimentResponse1.SentimentScore.Positive + detectSentimentResponse1.SentimentScore.Negative + detectSentimentResponse1.SentimentScore.Neutral;
                float positiveScore = (detectSentimentResponse1.SentimentScore.Positive) / totalScore * 100;
                float negativeScore = detectSentimentResponse1.SentimentScore.Negative / totalScore * 100;
                float neutralScore = detectSentimentResponse1.SentimentScore.Neutral / totalScore * 100;
                switch (detectSentimentResponse1.Sentiment)
                {
                    case "POSITIVE":
                        sentimentAnalysisResponse.sentiment = detectSentimentResponse1.Sentiment;
                        sentimentAnalysisResponse.sentimentscore = positiveScore;
                        break;
                    case "NEGETIVE":
                        sentimentAnalysisResponse.sentiment = detectSentimentResponse1.Sentiment;
                        sentimentAnalysisResponse.sentimentscore = negativeScore;
                        break;
                    case "MIXED":
                        sentimentAnalysisResponse.sentiment = detectSentimentResponse1.Sentiment;
                        sentimentAnalysisResponse.sentimentscore = neutralScore;
                        break;
                    case "NEUTRAL":
                        sentimentAnalysisResponse.sentiment = detectSentimentResponse1.Sentiment;
                        sentimentAnalysisResponse.sentimentscore = neutralScore;
                        break;
                    default:
                        break;
                }
                Console.WriteLine($"Sentiment: {sentimentAnalysisResponse}");
                res.Content = JsonConvert.SerializeObject(sentimentAnalysisResponse);
                return res;
                //return Content(JsonConvert.SerializeObject(sentimentAnalysisResponse), "application/json");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

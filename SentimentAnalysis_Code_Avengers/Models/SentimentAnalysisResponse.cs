namespace SentimentAnalysis_Code_Avengers.Models
{
    public class SentimentAnalysisResponse
    {
        public bool PII { get; set; }
        public string sentiment { get; set; }
        public float sentimentscore { get; set; }
        public string maskedPII { get; set; }
    }
}

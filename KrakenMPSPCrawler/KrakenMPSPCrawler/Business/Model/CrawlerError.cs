namespace KrakenMPSPCrawler.Business.Model
{
    public class CrawlerError
    {
        public string Message { get; set; }
        public string Source { get; set; }

        public CrawlerError(string message)
        {
            Message = message;
        }

        public CrawlerError(string source, string message)
        {
            Source = source;
            Message = message;
        }
    }
}
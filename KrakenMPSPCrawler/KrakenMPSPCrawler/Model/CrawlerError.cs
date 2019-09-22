using System;

namespace KrakenMPSPCrawler.Model
{
    public class CrawlerError
    {
        public string Message { get; set; }
        public Type Source { get; set; }

        public CrawlerError(string message)
        {
            Message = message;
        }

        public CrawlerError(Type source, string message)
        {
            Source = source;
            Message = message;
        }
    }
}
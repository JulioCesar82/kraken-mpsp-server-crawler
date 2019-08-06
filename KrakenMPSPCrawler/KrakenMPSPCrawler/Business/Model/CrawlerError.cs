using System;

namespace KrakenMPSPCrawler.Business.Model
{
    public class CrawlerError
    {
        public string Message { get; set; }
        public Type Type { get; set; }

        public CrawlerError(string message)
        {
            Message = message;
        }

        public CrawlerError(Type type, string message)
        {
            Type = type;
            Message = message;
        }
    }
}
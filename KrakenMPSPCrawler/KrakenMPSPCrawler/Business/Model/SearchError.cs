using System;

namespace KrakenMPSPCrawler.Business.Model
{
    public class SearchError
    {
        public string Message { get; set; }
        public Type Type { get; set; }

        public SearchError(string message)
        {
            Message = message;
        }

        public SearchError(Type type, string message)
        {
            Type = type;
            Message = message;
        }
    }
}
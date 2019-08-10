using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Interface;

namespace KrakenMPSPCrawler.Business.Model
{
    public abstract class Crawler : ICrawler
    {
        public abstract CrawlerStatus Execute();

        public virtual CrawlerError Error { get; protected set; }

        public virtual void SetErrorMessage(string errorMessage)
        {
            if (Error != null)
            {
                Error.Message = errorMessage;
                return;
            }

            Error = new CrawlerError(errorMessage);
        }

        public virtual void SetErrorMessage(string source, string errorMessage)
        {
            Error = new CrawlerError(source, errorMessage);
        }
    }
}
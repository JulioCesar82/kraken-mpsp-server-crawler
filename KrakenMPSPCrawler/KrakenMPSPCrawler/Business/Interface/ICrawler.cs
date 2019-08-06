using KrakenMPSPCrawler.Business.Enum;

namespace KrakenMPSPCrawler.Business.Interface
{
    public interface ICrawler
    {
        CrawlerStatus Execute();
    }
}

using KrakenMPSPBusiness.Enums;

namespace KrakenMPSPBusiness.Interfaces
{
    public interface ICrawler
    {
        CrawlerStatus Execute<T>(ref T result);
    }
}

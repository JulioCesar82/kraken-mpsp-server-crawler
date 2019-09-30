using KrakenMPSPConsole.Enums;

namespace KrakenMPSPConsole.Interfaces
{
    public interface ICrawler
    {
        CrawlerStatus Execute<T>(ref T result);
    }
}

using KrakenMPSPCrawler.Enum;

namespace KrakenMPSPCrawler.Interface
{
    public interface IPerson
    {
        KindPerson Type { get; }
        bool Completed { get; set; }
    }
}
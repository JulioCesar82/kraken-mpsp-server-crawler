using KrakenMPSPCrawler.Enum;

namespace KrakenMPSPCrawler.Models
{
    public class PersonModel
    {
        public KindPerson Type;

        public PersonModel(KindPerson type)
        {
            Type = type;
        }
    }
}
using KrakenMPSPBusiness.Enums;

namespace KrakenMPSPBusiness.Interfaces
{
    public interface IPerson
    {
        KindPerson Type { get; }
        bool Completed { get; set; }
    }
}
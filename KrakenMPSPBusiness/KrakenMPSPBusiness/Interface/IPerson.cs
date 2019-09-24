using KrakenMPSPBusiness.Enum;

namespace KrakenMPSPBusiness.Interface
{
    public interface IPerson
    {
        KindPerson Type { get; }
        bool Completed { get; set; }
    }
}
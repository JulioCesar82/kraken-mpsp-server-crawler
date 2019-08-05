using KrakenMPSPCrawler.Business.Model;

namespace KrakenMPSPCrawler.Business.Interface
{
    public interface IInvestigation
    {
        Investigation AddSearch(Investigation validation);
        Investigation Run();
        Investigation Run(Investigation validationContext);
    }
}
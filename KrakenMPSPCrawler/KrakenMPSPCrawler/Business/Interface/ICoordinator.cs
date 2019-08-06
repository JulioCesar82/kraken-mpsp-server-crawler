using KrakenMPSPCrawler.Business.Model;

namespace KrakenMPSPCrawler.Business.Interface
{
    public interface ICoordinator
    {
        Crawler AddModule(Crawler validation);
        Investigation Run();
        Investigation Run(Investigation validationContext);
    }
}
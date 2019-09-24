using KrakenMPSPCrawler.Model;

namespace KrakenMPSPCrawler.Interface
{
    public interface ICoordinator
    {
        Crawler AddModule(Crawler validation);
        Investigation Run();
        Investigation Run(Investigation validationContext);
    }
}
using KrakenMPSPConsole.Models;

namespace KrakenMPSPConsole.Interfaces
{
    public interface ICoordinator
    {
        Crawler AddModule(Crawler validation);
        Investigation Run();
        Investigation Run(Investigation validationContext);
    }
}
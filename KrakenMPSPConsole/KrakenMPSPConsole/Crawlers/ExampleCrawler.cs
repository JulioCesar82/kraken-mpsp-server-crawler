using System;

using KrakenMPSPBusiness.Enums;
using KrakenMPSPBusiness.Models;

using KrakenMPSPConsole.Enums;
using KrakenMPSPConsole.Services;

namespace KrakenMPSPConsole.Crawlers
{
    public class ExampleCrawler : Crawler
    {
        private readonly string _busca;

        public ExampleCrawler(string busca)
        {
            _busca = busca;
        }

        public override CrawlerStatus Execute(out object result)
        {
            try
            {
                using (var driver = WebDriverService.CreateWebDriver(WebBrowser.Firefox))
                {
                    driver.Navigate().GoToUrl(String.Format("https://www.google.com/search?hl=pt-BR&q={0}&oq={0}", _busca));

                    // page 1 - Capturar dados
                    result = null;

                    driver.Close();
                    Console.WriteLine("ExampleCrawler OK");
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("Fail loading browser caught: {0}", e.Message);
                SetErrorMessage(typeof(ExampleCrawler), e.Message);
                result = null;
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught: {0}", e.Message);
                SetErrorMessage(typeof(ExampleCrawler), e.Message);
                result = null;
                return CrawlerStatus.Error;
            }
        }
    }
}

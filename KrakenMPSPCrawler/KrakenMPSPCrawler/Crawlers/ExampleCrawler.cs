using System;

using KrakenMPSPCrawler.Services;
using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;

namespace KrakenMPSPCrawler.Crawlers
{
    public class ExampleCrawler : Crawler
    {
        private readonly string _busca;

        public ExampleCrawler(string busca)
        {
            _busca = busca;
        }

        public override CrawlerStatus Execute()
        {
            try
            {
                using (var driver = WebDriverService.CreateWebDriver(WebBrowser.Firefox))
                {
                    driver.Navigate().GoToUrl(String.Format("https://www.google.com/search?hl=pt-BR&q={0}&oq={0}", _busca));

                    // page 1 - Capturar dados
                    SetInformationFound(new Object());

                    driver.Close();
                    Console.Write("ExampleCrawler OK");
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("Fail loading browser caught: {0}", e.Message);
                SetErrorMessage(typeof(ExampleCrawler), e.Message);
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught: {0}", e.Message);
                SetErrorMessage(typeof(ExampleCrawler), e.Message);
                return CrawlerStatus.Error;
            }
        }
    }
}

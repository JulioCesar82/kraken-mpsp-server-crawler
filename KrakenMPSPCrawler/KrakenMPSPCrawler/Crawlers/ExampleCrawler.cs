using System;

using KrakenMPSPCrawler.Utils;
using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;

namespace KrakenMPSPCrawler.Crawlers
{
    public class ExampleCrawler : Crawler
    {
        private readonly string Busca;

        public ExampleCrawler(string busca)
        {
            Busca = busca;
        }

    public override CrawlerStatus Execute()
        {
            try
            {
                using (var driver = WebDriverFactory.CreateWebDriver(WebBrowser.Firefox))
                {
                    driver.Navigate().GoToUrl(String.Format("https://www.google.com/search?hl=pt-BR&q={0}&oq={0}", Busca));

                    // page 1 - Capturar dados
                    Console.Write("ExampleCrawler resultado");


                    driver.Close();
                    Console.Write("ExampleCrawler OK");
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.Write("{0} Faill loading browser caught.", e.Message);
                SetErrorMessage("ExampleCrawler", e.Message);
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.Write("{0} Exception caught.", e.Message);
                SetErrorMessage("ExampleCrawler", e.Message);
                return CrawlerStatus.Error;
            }
        }

    }
}

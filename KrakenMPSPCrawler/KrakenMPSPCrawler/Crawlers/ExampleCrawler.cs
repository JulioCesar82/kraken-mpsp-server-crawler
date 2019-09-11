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
                Console.WriteLine("ExampleCrawler resultado");


                driver.Close();
                Console.WriteLine("ExampleCrawler OK");
                return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("Fail loading browser caught: {0}", e.Message);
                SetErrorMessage("ExampleCrawler", e.Message);
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught: {0}", e.Message);
                SetErrorMessage("ExampleCrawler", e.Message);
                return CrawlerStatus.Error;
            }
        }
    }
}

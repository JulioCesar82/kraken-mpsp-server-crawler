using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

using KrakenMPSPCrawler.Business.Enum;

namespace KrakenMPSPCrawler.Services
{
    public static class WebDriverService
    {
        public static IWebDriver CreateWebDriver(WebBrowser name)
        {
            switch (name)
            {
                case WebBrowser.Firefox:
                    return CreateFirefoxDriver();
                case WebBrowser.Chrome:
                    return CreateChromeDriver();
                case WebBrowser.IE:
                case WebBrowser.InternetExplorer:
                    return CreateIEDriver();
                //case WebBrowser.Edge:
                //case WebBrowser.Safari:
                default:
                    throw new NotSupportedException("Not supported browser");
            }
        }

        private static IWebDriver CreateFirefoxDriver()
        {
            try
            {
                var driver = $@"{AppDomain.CurrentDomain.BaseDirectory}/ThirdParty/Drivers/geckodriver";
                var service = FirefoxDriverService.CreateDefaultService(driver);
                FirefoxOptions options = new FirefoxOptions();
                //TimeSpan time = TimeSpan.FromSeconds(20);

                #if Release
                    options.AddArgument("--headless");
                #endif

                return new FirefoxDriver(service, options);//, time);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                throw new NotSupportedException("Error in CreateFirefoxDriver", e);
            }            
        }

        private static IWebDriver CreateChromeDriver()
        {
            try
            {
                var driver = $@"{AppDomain.CurrentDomain.BaseDirectory}/ThirdParty/Drivers/ChromeDriver";
                var service = ChromeDriverService.CreateDefaultService(driver);
                ChromeOptions options = new ChromeOptions();
                //TimeSpan time = TimeSpan.FromSeconds(20);

                #if Release
                    options.AddArgument("--headless");
                #endif
                options.AddArguments("--disable-extensions");

                return new ChromeDriver(service, options);//, time);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                throw new NotSupportedException("Error in CreateChromeDriver", e);
            }
        }

        private static IWebDriver CreateIEDriver()
        {
            try
            {
                var driver = $@"{AppDomain.CurrentDomain.BaseDirectory}/ThirdParty/Drivers/EdgeDriver";
                var service = InternetExplorerDriverService.CreateDefaultService(driver);
                InternetExplorerOptions options = new InternetExplorerOptions();
                //TimeSpan time = TimeSpan.FromSeconds(20);

                #if Release
                    options.AddArgument("--headless");
                #endif

                return new InternetExplorerDriver(service, options);//, time);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                throw new NotSupportedException("Error in CreateIEDriver", e);
            }            
        }
    }
}
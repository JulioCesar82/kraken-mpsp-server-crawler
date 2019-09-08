using System;

using KrakenMPSPCrawler.Business.Enum;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace KrakenMPSPCrawler.Utils
{
    public static class WebDriverFactory
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
                case WebBrowser.Edge:
                case WebBrowser.InternetExplorer:
                    return CreateIEDriver();
                //case WebBrowser.Safari:
                //    return new RemoteWebDriver(new Uri("http://mac-ip-address:the-opened-port"), DesiredCapabilities.Safari());
                default:
                    throw new NotSupportedException("Not supported browser");
            }
        }

        private static IWebDriver CreateFirefoxDriver()
        {
            try
            {
                var driver = $@"{AppDomain.CurrentDomain.BaseDirectory}/Libs/geckodriver";
                var service = FirefoxDriverService.CreateDefaultService(driver);
                FirefoxOptions options = new FirefoxOptions();
                //TimeSpan time = TimeSpan.FromSeconds(20);

                //options.AddArgument("--headless");
                //service.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe"; // May not be necessary

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
                var driver = $@"{AppDomain.CurrentDomain.BaseDirectory}/Libs/ChromeDriver";
                var service = ChromeDriverService.CreateDefaultService(driver);
                ChromeOptions options = new ChromeOptions();
                //TimeSpan time = TimeSpan.FromSeconds(20);

                //options.AddArgument("--headless");
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
                var driver = $@"{AppDomain.CurrentDomain.BaseDirectory}/Libs/EdgeDriver";
                var service = InternetExplorerDriverService.CreateDefaultService(driver);
                InternetExplorerOptions options = new InternetExplorerOptions();
                //TimeSpan time = TimeSpan.FromSeconds(20);

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
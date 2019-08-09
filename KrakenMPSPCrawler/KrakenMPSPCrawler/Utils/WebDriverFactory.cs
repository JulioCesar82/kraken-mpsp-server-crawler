using System;

using KrakenMPSPCrawler.Business.Enum;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
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
                /*
                case WebBrowser.IE:
                case WebBrowser.Edge:               
                case WebBrowser.InternetExplorer:
                    InternetExplorerOptions ieOption = new InternetExplorerOptions
                    {
                        IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                        EnsureCleanSession = true,
                        RequireWindowFocus = true
                    };
                    return new InternetExplorerDriver(@"./", ieOption);
                */
                //case WebBrowser.Safari:
                //    return new RemoteWebDriver(new Uri("http://mac-ip-address:the-opened-port"), DesiredCapabilities.Safari());
                case WebBrowser.Chrome:
                    return CreateChromeDriver();
                default:
                    throw new NotImplementedException();
            }
        }

        private static IWebDriver CreateFirefoxDriver()
        {
            var geckpDriver = $@"{Environment.CurrentDirectory}/Libs/GeckoDriver";
            var service = FirefoxDriverService.CreateDefaultService(geckpDriver);
            service.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe"; // May not be necessary
            FirefoxOptions options = new FirefoxOptions();
            TimeSpan time = TimeSpan.FromSeconds(20);

            return new FirefoxDriver(service, options, time);
        }

        private static IWebDriver CreateChromeDriver()
        {
            var chromeDriver = $@"{Environment.CurrentDirectory}/Libs/ChromeDriver";
            var service = ChromeDriverService.CreateDefaultService(chromeDriver);
            ChromeOptions options = new ChromeOptions();
            TimeSpan time = TimeSpan.FromSeconds(20);

            options.AddArguments("--disable-extensions");

            return new ChromeDriver(service, options, time);
        }
    }
}
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;

namespace KrakenMPSPCrawler.Crawlers
{
    public class ArispCrawler : Crawler
    {
        private ChromeDriver _driver;
        public override CrawlerStatus Execute()
        {
            // Initialize edge driver 
            var localDrive = @"C:\Users\%julio.avila%\AppData\Local\Google\Chrome\Application\chrome.exe";
            var options = new ChromeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal             
            };
            //_driver = new ChromeDriver(options);
            //_driver.Url = "https://www.bing.com";
            //Assert.AreEqual("Bing", _driver.Title);

            searchPortal();
            return CrawlerStatus.Success;
        }

        private void searchPortal()
        {

            /*_driver = new PhantomJSDriver();
            phatom.Run()
            _driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 30));
            /*using (var driver = new PhantomJSDriver())
            {
                const string phantomScript = "var page=this;page.onResourceRequested = function(requestData, request) { var reg =  /\\.png/gi; var isPng = reg.test(requestData['url']); console.log(isPng,requestData['url']); if (isPng){console.log('Aborting: ' + requestData['url']);request.abort();}}";
                var script = driver.ExecutePhantomJS(phantomScript);
                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/arisp/login.html");
                driver.GetScreenshot().SaveAsFile("googlewithoutimage.png", ImageFormat.Png);
            }

            _driver.Navigate().GoToUrl("");

            // passo 1
            IWebElement loginButton = _driver.FindElement(By.Name("btnCallLogin"));
            loginButton.Click();

            _driver.GetScreenshot().SaveAsFile("Arisp.png");

            using (var driver = new ChromeDriver(outPutDirectory))
            {
                // Go to the home page
                driver.Navigate().GoToUrl("");

                // passo 1
                var loginButton = driver.FindElementById("btnCallLogin");
                loginButton.Click();

                // passo 2
                var autenticationButton = driver.FindElementById("btnAutenticar");
                autenticationButton.Click();
            }*/
        }

    }
}

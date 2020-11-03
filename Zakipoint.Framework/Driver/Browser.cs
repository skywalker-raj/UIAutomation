using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Interactions;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Configuration;
using System.Text;
using OpenQA.Selenium.Support.PageObjects;
using Zakipoint.Framework.Common.Constants;

namespace Zakipoint.Framework.Driver
{
    public class Browser
    {
        #region Private Properties

        public static readonly IConfiguration Config = new ConfigurationBuilder().AddJsonFile(@"Data/config.json").Build();
        private static IBrowserOptions _browserOptions;
        private static IWebDriver WebDriver = GetWebDriver();
        private static DriverService _driverService;
        private static WebDriverWait _waitPageLoad;
        private static WebDriverWait _waitAjaxLoad;
        private static IJavaScriptExecutor _javaScriptExecutor;
        private static ScreenshotRemoteWebDriver _screenshotRemoteWebDriver;
        private static ICapabilities _capabilities;
        private static Uri _remoteAddress;
        private static bool IsInitalized { get; set; }
        private static IWebDriver _primaryWebDriver;
       
       
        #endregion

        #region Public Properties

        public static void WaitForAjaxLoad(string library)
        {
            string jScript = "";
            switch (library)
            {
                case ScriptConstants.Jquery:
                    jScript = "return Boolean($.active);";
                    break;
                case ScriptConstants.Extjs:
                    jScript = "return Ext.Ajax.isLoading();";
                    break;
                case ScriptConstants.Telerik:
                    jScript = "return $telerik.radControls[0]._manager._isRequestInProgress;";
                    break;
            }
            _waitAjaxLoad.Until(d =>
            {
                try
                {
                    return (bool)_javaScriptExecutor.ExecuteScript(jScript) == false;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }
        /// <summary>
        /// Instructs the driver to send future commands to a different frame or window. 
        /// </summary>
        /// <param name="windowName">A window name.</param>
        public static void SwitchWindow(string windowName)
        {
            WebDriver.SwitchTo().Window(windowName);
        }
        public static string Title
        {
            get { return WebDriver.Title; }
        }
        public static string PreviousPageTitle { get; set; }
        public static string PageSource
        {
            get { return WebDriver.PageSource; }
        }
        public static string Url
        {
            get { return WebDriver.Url; }
        }
        public static IJavaScriptExecutor JsExecutor
        {
            get { return WebDriver as IJavaScriptExecutor; }
        }
        public static ICapabilities Capabilites
        {
            get { return _capabilities; }
        }
        public static string BaseUrl
        {
            get;
            set;
        }
        public static int SleepInterval
        {
            get { return _browserOptions.SleepInterval; }
        }
        public static int ElementTimeOut
        {
            get { return _browserOptions.ElementLoadTimeout; }
        }
        public static string CurrentWindowHandle
        {
            get { return WebDriver.CurrentWindowHandle; }
        }
        public static List<string> WindowHandles
        {
            get { return WebDriver.WindowHandles.ToList(); }
        }
        public static WebDriverWait WaitForExpectedConditions(int second = 60)
        {
            WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(second));
            return wait;
        }
        public static void WaitForCondition(Func<bool> f, int milliSec = 0)
        {
            milliSec = (int)((milliSec == 0) ? _browserOptions.PageLoadTimeout * 1000 : milliSec);
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromMilliseconds(milliSec));
            try
            {
                wait.Until(d =>
                {
                    try
                    {
                        return f();
                    }
                    catch (Exception)
                    {
                        DismissAlert();
                        return false;
                    }
                });
            }
            catch (UnhandledAlertException ex)
            {
                Console.Out.WriteLine("unhandled exception" + ex.Message);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Waits for the correct page to load.
        /// </summary>
        /// <param name="pageTitle">A title of the page.</param>
        public static void WaitForCorrectPageToLoad(string pageTitle)
        {
            string readyState = string.Empty;
            try
            {
                _waitPageLoad.Until((d) =>
                {
                    try
                    {
                        readyState =
                            _javaScriptExecutor.ExecuteScript(
                                "if (document.readyState) return document.readyState;").
                                ToString();
                        if (string.Compare(readyState, "complete",
                                           StringComparison.OrdinalIgnoreCase) == 0 &&
                            string.Compare(Title, pageTitle, StringComparison.OrdinalIgnoreCase) ==
                            0)
                        {
                            if (string.Compare(PreviousPageTitle, Title, StringComparison.Ordinal) != 0)
                            {
                                Console.WriteLine("{0} page opened", Title);
                                PreviousPageTitle = Title;
                            }
                            return true;
                        }
                        return false;
                    }
                    catch (UnhandledAlertException)
                    {
                        DismissAlert();
                        return false;
                    }
                    catch (InvalidOperationException e)
                    {
                        //Window is no longer available
                        return e.Message.ToLower().Contains("unable to get browser");
                    }
                    catch (WebDriverException e)
                    {
                        //Browser is no longer available
                        return e.Message.ToLower().Contains("unable to connect");
                    }
                    catch (Exception)
                    {
                        Console.Out.WriteLine(
                            "Expected to find a page title of <{0}>, but found <{1}>.",
                            pageTitle, Title);
                        return false;
                    }
                });
            }
            catch (Exception ex)
            {
                if (string.Compare(readyState, "complete", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    Console.Out.WriteLine("Expected to find a page title of <{0}>, but found <{1}>.", pageTitle, Title);
                }
                Console.Out.WriteLine(ex);
            }
        }
        /// A generic method, that finds the first <see cref="IWebElement"/>  in the page that matches with in a document.
        /// </summary>
        /// <param name="select">The element to be found.</param>
        /// <param name="selector">The locating mechanism to use.</param>
        /// <returns>The first matching <see cref="IWebElement"/> on the current context.</returns>
        /// <exception cref="NoSuchElementException">If no element matches the criteria.</exception>
        //public static T FindElement<T>(string select, How selector, bool cache = false) where T : class, IElement
        //{
        //    return (T)Activator.CreateInstance(typeof(T), cache, selector, select);
        //}
        ///// <summary>
        ///// A generic method, that Finds the first <see cref="IWebElement"/>  in the page that matches with in a document.
        ///// </summary>
        ///// <param name="select">The element to be found.</param>
        ///// <param name="selector">The locating mechanism to use.</param>
        ///// <param name="context">The base element through which its corresponding elements can be found.</param>
        ///// <returns>The first matching <see cref="IWebElement"/> on the current context.</returns>
        ///// <exception cref="NoSuchElementException">If no element matches the criteria.</exception>
        //public static T FindElement<T>(string select, How selector, IElement context, bool cache = false) where T : class, IElement
        //{
        //    //WaitElementExists(select, selector, _webdriver);
        //    return (T)Activator.CreateInstance(typeof(T), cache, selector, select, (IWebElement)context.BaseObject);
        //}

        #endregion

        #region FINDELEMENTS

        //TODO: Need to figure out another way around
        /// <summary>
        /// A generic method, that finds the multiple <see cref="IWebElement"/>  in the page that matches with in a document.
        /// </summary>
        //public static List<string> FindElements(string select, How selector, string selectSelector, bool isNull = false)
        //{
        //    if (FindElementsCount(select, selector) > 15)
        //        return _javaScriptExecutor.FindElements(select, selectSelector, isNull, selector);
        //    var data = new List<string>();
        //    if (isNull)
        //        return FindElements(select, selector, WebDriver).Select(element => element.Text).ToList();
        //    if (selectSelector.Equals("Text"))
        //        return FindElements(select, selector, WebDriver).Where(x => x.Displayed).Select(element => element.Text).ToList();

        //    if (selectSelector.StartsWith("Attribute"))
        //        return
        //            FindElements(select, selector, WebDriver)
        //                .Where(x => x.Displayed)
        //                .Select(element => element.GetAttribute(selectSelector.Replace("Attribute:", "")))
        //                .ToList();
        //    data.Add(FindElements(select, selector, WebDriver).Where(x => x.Displayed).First().Text.Trim());
        //    return data;
        //}    
        public static IEnumerable<IWebElement> WaitandReturnElementsExists(By locator, ISearchContext context, int elementTimeOut = 2000)
        {
            if (elementTimeOut == 0)
                return context.FindElements(locator);
            var wait = new WebDriverWait(new SystemClock(), WebDriver, TimeSpan.FromMilliseconds(ElementTimeOut), TimeSpan.FromMilliseconds(SleepInterval));
            IEnumerable<IWebElement> webElement = null;
            wait.Until(driver =>
            {
                try
                {
                    webElement = context.FindElements(locator);
                    return webElement != null;
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine("unhandled exception" + ex.Message);
                    return false;
                }
            });
            return webElement;
        }
        public static int FindElementsCount(string select, How selector)
        {
            return FindElements(select, selector, WebDriver).Count();
        }

        #endregion

        #region Base Methods

        public static IWebDriver GetWebDriver(string browser = "Chrome")
        {
            IWebDriver webDriver;
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            switch (browser)
            {
                case "Firefox":
                    webDriver = new FirefoxDriver(baseDir + "Executables/");
                    break;
                case "IE":
                    webDriver = new InternetExplorerDriver(baseDir + "Executables/");
                    break;
                case "Safari":
                    webDriver = new SafariDriver(baseDir + "Executables/");
                    break;
                case "Chrome":
                default:
                    webDriver = new ChromeDriver(baseDir + "Executables/");
                    break;
            }
            return webDriver;
        }

        public static void Init(IBrowserOptions browserOptions)
        {
            _browserOptions = browserOptions;
            IsInitalized = true;
        }

        public static void Dispose()
        {
            WebDriver.Quit();
        }

        public static void Open(string url)
        {
            WebDriver.Navigate().GoToUrl(url);            
            WebDriver.Manage().Window.Maximize();
        }

        #endregion

        #region Browser Services      
       
        public static void Reload()
        {
            WebDriver.Navigate().Refresh();
        }
        public static void Back()
        {
            WebDriver.Navigate().Back();
        }
        public static void Forward()
        {
            WebDriver.Navigate().Forward();
        }
        public static string GetAlertBoxText()
        {
            return WebDriver.SwitchTo().Alert().Text;
        }
        public static bool IsAlertBoxPresent()
        {
            try
            {
                WebDriver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        public static void AcceptAlertBox()
        {
            WebDriver.SwitchTo().Alert().Accept();
        }
        public static void DismissAlert()
        {
            try
            {
                IAlert modalDialog = WebDriver.SwitchTo().Alert();
                modalDialog.Dismiss();
            }
            catch (Exception)
            {
                return;
            }
        }
        public static void ClickAndHold(How locator, string value)
        {
            var action = new Actions(WebDriver);
            var webElement = FindElement(locator, value);
            action.MoveToElement(webElement).Click();
            action.MoveToElement(webElement).ClickAndHold().Build().Perform();
        }
        public static void WaitForPageToLoad()
        {
            const string jScript = "return document.readyState;";
            _waitPageLoad.Until(d =>
            {
                try
                {
                    return (string)_javaScriptExecutor.ExecuteScript(jScript) == "complete";
                }
                catch (UnhandledAlertException)
                {
                    DismissAlert();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine(ex.Message);
                    return false;
                }
            });
        }
        public static void WaitForAjaxToLoad(string ajaxLibrary)
        {
            string script = string.Format("return {0};", ajaxLibrary);
            int count = 0;
            _waitAjaxLoad.Until((d) =>
            {
                try
                {
                    if (count > 1)
                        WaitToLoadNew(1000);
                    count++;
                    var result = ((bool)_javaScriptExecutor.ExecuteScript(script) == false);
                    return result;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }
        public static void WaitToLoadNew(int millisecondsTimeout)
        {
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(millisecondsTimeout);
        }
        public static void CaptureScreenShot(string name)
        {
            _screenshotRemoteWebDriver.CaptureScreenShot(name);
        }
        public static void CaptureScreenShot(string dirPath, string name)
        {
            _screenshotRemoteWebDriver.CaptureScreenShot(dirPath, name);
        }
        public static bool IsHandlePresent(string argHandle)
        {
            return WebDriver.WindowHandles.Contains(argHandle);
        }
        public static string GetHandleByTitle(string title)
        {
            string currentHandle = WebDriver.CurrentWindowHandle;
            foreach (string handle in WebDriver.WindowHandles)
            {
                if (title.Equals(WebDriver.SwitchTo().Window(handle).Title))
                {
                    return handle;
                }
            }
            return currentHandle;
        }
        public static void LocalFileDetect()
        {
            IAllowsFileDetection allowsDetection = WebDriver as IAllowsFileDetection;
            if (allowsDetection != null)
            {
                allowsDetection.FileDetector = new LocalFileDetector();
            }
        }
        public static void ScreenShot(string screenShotName)
        {
            if (WebDriver is ITakesScreenshot ssdriver)
            {
                var screenShot = ssdriver.GetScreenshot();
                screenShot.SaveAsFile(screenShotName, ScreenshotImageFormat.Png);
            }
        }
        public static IWebElement FindElement(How locator, string value)
        {
            try
            {
                IWebElement element;
                switch (locator)
                {
                    case How.XPath:
                        element = WebDriver.FindElement(By.XPath(value));
                        break;
                    case How.Id:
                        element = WebDriver.FindElement(By.Id(value));
                        break;
                    case How.Name:
                        element = WebDriver.FindElement(By.Name(value));
                        break;
                    case How.ClassName:
                        element = WebDriver.FindElement(By.ClassName(value));
                        break;
                    case How.TagName:
                        element = WebDriver.FindElement(By.TagName(value));
                        break;
                    case How.LinkText:
                        element = WebDriver.FindElement(By.LinkText(value));
                        break;
                    case How.CssSelector:
                    default:
                        element = WebDriver.FindElement(By.CssSelector(value));
                        break;
                }
                return element;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e);
                return null;
            }
        }
        public static IList<IWebElement> FindElements(How locator, string value)
        {
            try
            {
                IList<IWebElement> element;
                switch (locator)
                {
                    case How.XPath:
                        element = WebDriver.FindElements(By.XPath(value));
                        break;
                    case How.Id:
                        element = WebDriver.FindElements(By.Id(value));
                        break;
                    case How.Name:
                        element = WebDriver.FindElements(By.Name(value));
                        break;
                    case How.ClassName:
                        element = WebDriver.FindElements(By.ClassName(value));
                        break;
                    case How.TagName:
                        element = WebDriver.FindElements(By.TagName(value));
                        break;
                    case How.LinkText:
                        element = WebDriver.FindElements(By.LinkText(value));
                        break;
                    case How.CssSelector:
                    default:
                        element = WebDriver.FindElements(By.CssSelector(value));
                        break;
                }
                return element;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e);
                return null;
            }
        }
        public static bool IsElementPresent(How locator, string value)
        {
            try
            {
                IWebElement element;
                switch (locator)
                {
                    case How.XPath:
                        element = WebDriver.FindElement(By.XPath(value));
                        break;
                    case How.Id:
                        element = WebDriver.FindElement(By.Id(value));
                        break;
                    case How.Name:
                        element = WebDriver.FindElement(By.Name(value));
                        break;
                    case How.ClassName:
                        element = WebDriver.FindElement(By.ClassName(value));
                        break;
                    case How.TagName:
                        element = WebDriver.FindElement(By.TagName(value));
                        break;
                    case How.LinkText:
                        element = WebDriver.FindElement(By.LinkText(value));
                        break;
                    case How.CssSelector:
                    default:
                        element = WebDriver.FindElement(By.CssSelector(value));
                        break;
                }
                if (element != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e);
                return false;
            }
        }
        public static bool IsElementEnabled(How locator, string value)
        {
            try
            {
                bool enabled;
                switch (locator)
                {
                    case How.XPath:
                        enabled = WebDriver.FindElement(By.XPath(value)).Enabled;
                        break;
                    case How.Id:
                        enabled = WebDriver.FindElement(By.Id(value)).Enabled;
                        break;
                    case How.Name:
                        enabled = WebDriver.FindElement(By.Name(value)).Enabled;
                        break;
                    case How.ClassName:
                        enabled = WebDriver.FindElement(By.ClassName(value)).Enabled;
                        break;
                    case How.TagName:
                        enabled = WebDriver.FindElement(By.TagName(value)).Enabled;
                        break;
                    case How.LinkText:
                        enabled = WebDriver.FindElement(By.LinkText(value)).Enabled;
                        break;
                    case How.CssSelector:
                    default:
                        enabled = WebDriver.FindElement(By.CssSelector(value)).Enabled;
                        break;
                }
                return enabled;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e);
                return false;
            }
        }
        public static bool IsElementDisplayed(How locator, string value)
        {
            try
            {
                bool displayed;
                switch (locator)
                {
                    case How.XPath:
                        displayed = WebDriver.FindElement(By.XPath(value)).Displayed;
                        break;
                    case How.Id:
                        displayed = WebDriver.FindElement(By.Id(value)).Displayed;
                        break;
                    case How.Name:
                        displayed = WebDriver.FindElement(By.Name(value)).Displayed;
                        break;
                    case How.ClassName:
                        displayed = WebDriver.FindElement(By.ClassName(value)).Displayed;
                        break;
                    case How.TagName:
                        displayed = WebDriver.FindElement(By.TagName(value)).Displayed;
                        break;
                    case How.LinkText:
                        displayed = WebDriver.FindElement(By.LinkText(value)).Displayed;
                        break;
                    case How.CssSelector:
                    default:
                        displayed = WebDriver.FindElement(By.CssSelector(value)).Displayed;
                        break;
                }
                return displayed;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e);
                return false;
            }
        }
        public static string GetElementText(How locator, string value)
        {
            IWebElement element = FindElement(locator, value);
            return element.Text;
        }
        public static string GetAttribute(How locator, string value, string attribute)
        {
            IWebElement element = FindElement(locator, value);
            return element.GetAttribute(attribute);
        }
        public static void ClearTextBox(How locator, string value)
        {
            FindElement(locator, value).Clear();
        }
        public static void EnterValueInTextBox(How locator, string value, string text)
        {
            FindElement(locator, value).SendKeys(text);
        }
        public static bool CloseWindowAndSwitchTo(string windowName)
        {
            if (!WebDriver.CurrentWindowHandle.Equals(windowName))
            {
                WebDriver.Close();
                WebDriver.SwitchTo().Window(windowName);
                return true;
            }
            return false;
        }
        public static bool SwitchWindowByUrl(string windowUrl)
        {
            foreach (string handle in WebDriver.WindowHandles)
            {
                if (WebDriver.SwitchTo().Window(handle).Url.Contains(windowUrl))
                    return true;
            }
            return false;
        }
        public static void CloseWindow(string windowName)
        {
            WebDriver.SwitchTo().Window(windowName).Close();
        }
        public static void CloseWindow()
        {
            WebDriver.Close();
        }
        public static void SwitchFrame(string frameName)
        {
            WebDriver.SwitchTo().Frame(frameName);
        }
        public static void SwitchBackToMainFrame()
        {
            WebDriver.SwitchTo().DefaultContent();
        }
        public static void CloseModalPopup()
        {
            WebDriver.SwitchTo().ActiveElement().SendKeys(Keys.Escape);
        }
        public static void CloseFrame(string frameName)
        {
            WebDriver.SwitchTo().Frame(frameName).Close();
        }
        public static void CloseFrameAndSwitchTo(String frameName)
        {
            WebDriver.SwitchTo().DefaultContent().SwitchTo().Frame(frameName);
        }
        public static void MaximizeWindow()
        {
            WebDriver.Manage().Window.Maximize();
        }
        public static void Stop()
        {
            if (WebDriver != null)
            {
                WebDriver.Quit();
                WebDriver = _primaryWebDriver;
                if (_driverService != null)
                {
                    _driverService.Dispose();
                    _driverService = null;
                }
            }
        }
        public static void ElementToBeClickable(IWebElement element, int elementTimeOut = 3000)
        {
            //elementTimeOut = _browserOptions.ElementLoadTimeout;
            var wait = new WebDriverWait(new SystemClock(), WebDriver, TimeSpan.FromMilliseconds(ElementTimeOut), TimeSpan.FromMilliseconds(SleepInterval));
            try
            {
                wait.Until(driver =>
                {
                    try
                    {
                        return element != null && element.Displayed && element.Enabled && !new[] { "is_hidden", "none", "hidden" }.Any(c => element.GetAttribute("class").Contains(c));
                    }
                    catch (Exception ex)
                    {
                        Console.Out.WriteLine("unhandled exception clickable" + ex.Message);
                        return false;
                    }
                });
            }
            catch (Exception)
            {
            }
        }
        public static void WaitTillClear(IWebElement element, int elementTimeOut = 500)
        {
            var wait = new WebDriverWait(new SystemClock(), WebDriver, TimeSpan.FromMilliseconds(ElementTimeOut), TimeSpan.FromMilliseconds(SleepInterval));
            try
            {
                wait.Until(driver =>
                {
                    try
                    {
                        return element.Displayed && element.Enabled && element.Text.Length == 0 && element.GetAttribute("value") == "";
                    }
                    catch (Exception ex)
                    {
                        Console.Out.WriteLine("unhandled exception clear" + ex.Message);
                        return false;
                    }
                });
            }
            catch (Exception)
            {
            }
        }
        public static IWebElement WaitandReturnElementExists(By locator, ISearchContext context, int elementTimeOut = 2000)
        {
            if (elementTimeOut == 0)
                return context.FindElement(locator);

            var wait = new WebDriverWait(new SystemClock(), WebDriver, TimeSpan.FromMilliseconds(ElementTimeOut), TimeSpan.FromMilliseconds(SleepInterval));
            IWebElement webElement = null;
            wait.Until(driver =>
            {
                try
                {
                    webElement = context.FindElement(locator);
                    return webElement != null;
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine("unhandled exception" + ex.Message);
                    return false;
                }
            });
            return webElement;
        }

        /// <summary>
        /// Gets a screenshot filename.
        /// </summary>
        /// <returns>A screenshot file name.</returns>
        public static string GetScreenshotFilename()
        {
            return ScreenshotRemoteWebDriver.FullQualifiedFileName;
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Finds the multiple <see cref="IWebElement"/>  using the given method.
        /// </summary>
        /// <param name="select">The element to be found.</param>
        /// <param name="selector">The locating mechanism to use.</param>
        /// <param name="context">The base element through which its corresponding elements can be found.</param>
        ///<returns>The first matching <see cref="IWebElement"/> on the current context.</returns>
        /// <exception cref="NoSuchElementException">If no element matches the criteria.</exception>
        internal static IEnumerable<IWebElement> FindElements(string select, How selector, ISearchContext context, int elementTimeOut = 2000)
        {
            switch (selector)
            {
                case How.ClassName:
                    return WaitandReturnElementsExists(By.ClassName(select), context, elementTimeOut);
                case How.CssSelector:
                    return WaitandReturnElementsExists(By.CssSelector(select), context, elementTimeOut);
                case How.Id:
                    return WaitandReturnElementsExists(By.Id(select), context, elementTimeOut);
                case How.LinkText:
                    return WaitandReturnElementsExists(By.LinkText(select), context, elementTimeOut);
                case How.Name:
                    return WaitandReturnElementsExists(By.Name(select), context, elementTimeOut);
                case How.PartialLinkText:
                    return WaitandReturnElementsExists(By.PartialLinkText(select), context, elementTimeOut);
                case How.TagName:
                    return WaitandReturnElementsExists(By.TagName(select), context, elementTimeOut);
                case How.XPath:
                    return WaitandReturnElementsExists(By.XPath(select), context, elementTimeOut);
            }
            throw new NotSupportedException(string.Format("Selector \"{0}\" is not supported.", selector));
        }

        /// <summary>
        /// Checks whether the element is present or not.
        /// </summary>
        /// <param name="select">An lookup value</param>
        /// <param name="selector">An <see cref="How"/> object of lookup methods.</param>
        /// <param name="context"></param>
        /// <returns></returns>
        internal static bool IsElementPresent(string select, How selector, IWebElement context)
        {
            try
            {
                FindElement(select, selector, context, 0);
                return true;
            }
            catch (Exception ex)
            {
                // Don't handle NotSupportedException
                if (ex is NotSupportedException)
                    throw;
                return false;
            }
        }

        /// <summary>
        /// Finds the first <see cref="IWebElement"/>  using the given method. 
        /// </summary>
        /// <param name="select">The element to be found.</param>
        /// <param name="selector">The locating mechanism to use.</param>
        /// <returns>The first matching <see cref="IWebElement"/> on the current context.</returns>
        /// <exception cref="NoSuchElementException">If no element matches the criteria.</exception>
        internal static IWebElement FindElement(string select, How selector)
        {
            return FindElement(select, selector, WebDriver);
        }

        // <summary>
        /// Finds the first <see cref="IWebElement"/>  in the page that matches with in a document.
        /// </summary>
        /// <param name="select">The element to be found.</param>
        /// <param name="selector">The locating mechanism to use.</param>
        /// <param name="context">The base element through which its corresponding elements can be found.</param>
        /// <returns>The first matching <see cref="IWebElement"/> on the current context.</returns>
        /// <exception cref="NoSuchElementException">If no element matches the criteria.</exception>
        internal static IWebElement FindElement(string select, How selector, ISearchContext context, int elementTimeOut = 2000)
        {
            switch (selector)
            {
                case How.ClassName:
                    return WaitandReturnElementExists(By.ClassName(select), context, elementTimeOut);
                case How.CssSelector:
                    return WaitandReturnElementExists(By.CssSelector(select), context, elementTimeOut);
                case How.Id:
                    return WaitandReturnElementExists(By.Id(select), context, elementTimeOut);
                case How.LinkText:
                    return WaitandReturnElementExists(By.LinkText(select), context, elementTimeOut);
                case How.Name:
                    return WaitandReturnElementExists(By.Name(select), context, elementTimeOut);
                case How.PartialLinkText:
                    return WaitandReturnElementExists(By.PartialLinkText(select), context, elementTimeOut);
                case How.TagName:
                    return WaitandReturnElementExists(By.TagName(select), context, elementTimeOut);
                case How.XPath:
                    return WaitandReturnElementExists(By.XPath(select), context, elementTimeOut);
            }
            throw new NotSupportedException(string.Format("Selector \"{0}\" is not supported.", selector));
        }

        #endregion
    }
}

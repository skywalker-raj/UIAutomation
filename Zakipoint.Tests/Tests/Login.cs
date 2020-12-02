using NUnit.Framework;
using OpenQA.Selenium.Support.PageObjects;
using System;
using Zakipoint.Framework.Driver;
using Zakipoint.Tests.Common;
using Zakipoint.UIAutomation.PageObjects;
using Zakipoint.UIAutomation.PageServices;
using static System.String;
using Zakipoint.Tests.Base;
using Zakipoint.UIAutomation.Common;
namespace Zakipoint.Tests.Tests
{
    public class Login : AbstractBase
    {
        #region Private Methods

        private readonly LoginPageObjects _loginPage;
        private readonly LoginPage _login;
        private readonly CommonFunction _commonFunction;
    
        #endregion

        #region Constructor

        public Login()
        {
            _loginPage = new LoginPageObjects();
            _login = new LoginPage();
            _commonFunction = new CommonFunction();          
        }

        #endregion

        #region Base Methods
       
        public override void Init()
        {
            Browser.Open(Browser.Config["url"]);
        }

        public override void Dispose()
        {
            _commonFunction.Logout();
        }

        #endregion
        #region TestMethods
        [Test, Category("Login")]
        public void Verify_Login_Page()
        {
            try
            {
                Assert.IsTrue(Browser.IsElementPresent(How.CssSelector, _loginPage.LoginTitleCssSelector), "Login Title should be present.");
                Assert.IsTrue(Browser.IsElementPresent(How.XPath, Format(_loginPage.LabelXPath, "username")), "User Name label should be present.");
                Assert.IsTrue(Browser.IsElementPresent(How.XPath, Format(_loginPage.LabelXPath, "password")), "Password label should be present.");
                Assert.AreEqual(Browser.GetAttribute(How.CssSelector, Format(_loginPage.TextBoxCssSelector, "username"), "placeholder"), JsonDataReader.Data["usernameplaceholder"]);
                Assert.AreEqual(Browser.GetAttribute(How.CssSelector, Format(_loginPage.TextBoxCssSelector, "password"), "placeholder"), JsonDataReader.Data["passwordplaceholder"]);
                Assert.IsTrue(Browser.IsElementPresent(How.XPath, _loginPage.ForgotPasswordXPath), "Forgot Password link should be present.");
                Assert.IsTrue(Browser.IsElementPresent(How.CssSelector, _loginPage.LoginButtonCssSelector), "Login Button should be present.");
                _login.Login("", "");
                if (Browser.IsElementDisplayed(How.CssSelector, _loginPage.ErrorMessageCssSelector))
                {
                    Assert.AreEqual(Browser.GetElementText(How.CssSelector, _loginPage.ErrorMessageCssSelector), JsonDataReader.Data["invalidusernamepassword"]);
                }
                _login.Login("", JsonDataReader.Data["password"]);
                if (Browser.IsElementDisplayed(How.CssSelector, _loginPage.ErrorMessageCssSelector))
                {
                    Assert.AreEqual(Browser.GetElementText(How.CssSelector, _loginPage.ErrorMessageCssSelector), JsonDataReader.Data["invalidusername"]);
                }
                _login.Login(JsonDataReader.Data["username"], "");
                if (Browser.IsElementDisplayed(How.CssSelector, _loginPage.ErrorMessageCssSelector))
                {
                    Assert.AreEqual(Browser.GetElementText(How.CssSelector, _loginPage.ErrorMessageCssSelector), JsonDataReader.Data["invalidusernamepassword"]);
                }
                _login.Login(JsonDataReader.Data["username"], JsonDataReader.Data["password"]);
                //Browser.WaitToLoadNew(3000);
                //Assert.IsTrue(Browser.IsElementPresent(How.CssSelector, _loginPage.LoginButtonCssSelector), "Login Button should be present.");
            }
            catch (Exception e)
            {
                Browser.ScreenShot("Login_Verification_Shot");
                Console.Out.WriteLine(e);
            }
        }
        #endregion
    }
}
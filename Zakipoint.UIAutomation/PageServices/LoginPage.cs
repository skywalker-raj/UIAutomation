using Zakipoint.UIAutomation.PageObjects;
using Zakipoint.Framework.Driver;
using static System.String;
using OpenQA.Selenium.Support.PageObjects;

namespace Zakipoint.UIAutomation.PageServices
{
    public class LoginPage
    {
        #region Private Fields

        private readonly LoginPageObjects _loginPage;

        #endregion

        #region Constructor

        public LoginPage()
        {
            _loginPage = new LoginPageObjects();
        }

        #endregion

        #region Public Methods

        public void Login(string username, string password)
        {
            Browser.ClearTextBox(How.CssSelector, Format(_loginPage.TextBoxCssSelector, "username"));
            Browser.ClearTextBox(How.CssSelector, Format(_loginPage.TextBoxCssSelector, "password"));
            Browser.EnterValueInTextBox(How.CssSelector, Format(_loginPage.TextBoxCssSelector, "username"), username);
            Browser.EnterValueInTextBox(How.CssSelector, Format(_loginPage.TextBoxCssSelector, "password"), password);
            ClickLoginButton();
        }

        public void ClickLoginButton()
        {
            Browser.FindElement(How.CssSelector, _loginPage.LoginButtonCssSelector).Click();
        }

        #endregion
    }
}
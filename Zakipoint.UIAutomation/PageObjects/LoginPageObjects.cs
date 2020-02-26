namespace Zakipoint.UIAutomation.PageObjects
{
    public class LoginPageObjects
    {
        #region Elements

        #region CssSelectors

        public string LoginTitleCssSelector = "header.entry-header h2.entry-title";
        public string TextBoxCssSelector = "input#{0}";
        public string LoginButtonCssSelector = "button.login-btn";
        public string ErrorMessageCssSelector = "section span";

        #endregion

        #region XPath

        public string ForgotPasswordXPath = "//a[text()='Forgot Password?']";
        public string LabelXPath = "//label[@for='{0}']";

        #endregion

        #endregion
    }
}

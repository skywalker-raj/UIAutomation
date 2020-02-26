namespace Zakipoint.UIAutomation.PageObjects
{
    public class SetClientPageObjects
    {
        #region Elements

        #region CssSelector 

        public string GoButtonCssSelector = "button[value=Go]";
        public string SelectClientDropdownCssSelector = "button[data-id=client]";
        public string DropDownSelectedCssSelector = "ul.dropdown-menu li.selected";
        public string UserManagementLinkCssSelector = "a[title='User Management and Security']";

        #endregion

        #region XPath

        public string LabelByTextXPath = "//span[text()='{0}']";
        public string ClientListXPath = "//ul/li/a/span";
        public string ClientByTextXPath = "//ul/li/a/span[text()='{0}']";

        #endregion

        #endregion
    }
}
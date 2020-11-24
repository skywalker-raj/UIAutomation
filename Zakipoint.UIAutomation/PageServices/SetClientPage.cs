using System;
using System.Collections.Generic;
using System.Text;
using Zakipoint.Framework.Driver;
using Zakipoint.Framework.Database;
using Zakipoint.UIAutomation.PageObjects;
using Zakipoint.UIAutomation.SqlScripts;
using OpenQA.Selenium.Support.PageObjects;
using static System.String;

namespace Zakipoint.UIAutomation.PageServices
{
    public class SetClientPage
    {
        #region Private Fields

        private readonly SetClientPageObjects _setClientPage;
        private readonly SetClientScripts _setClientScripts;
        private readonly MySqlStatementExecutor _executor;

        #endregion

        #region Constructor

        public SetClientPage()
        {
            _setClientPage = new SetClientPageObjects();
            _setClientScripts = new SetClientScripts();
            _executor = new MySqlStatementExecutor();
        }

        #endregion

        #region Database Methods

        public List<string> GetClientListFromDb(string user)
        {
            return _executor.GetTableSingleColumn(Format(_setClientScripts.GetClientList, user));
        }

        #endregion

        #region Public Methods

        public void SelectClient(string client)
        {
            Browser.FindElement(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector).Click();
            Browser.FindElement(How.XPath, Format(_setClientPage.ClientByTextXPath, client)).Click();
            ClickGoButton();
        }

        public void ClickGoButton()
        {
            Browser.FindElement(How.CssSelector, _setClientPage.GoButtonCssSelector).Click();
        }

        public List<string> GetClientList()
        {
            List<string> clientList = new List<string>();
            var clientListElements = Browser.FindElements(How.XPath, _setClientPage.ClientListXPath);
            foreach(var client in clientListElements)
            {
                clientList.Add(client.Text);
            }
            clientList.Remove("Select One");
            return clientList;
        }

        public void GoToUserManagement()
        {
            Browser.FindElement(How.CssSelector, _setClientPage.UserManagementLinkCssSelector).Click();
            Browser.WaitToLoadNew(3000);
            //Browser.WaitForPageToLoad();
        }

        #endregion     
    }
}
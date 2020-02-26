using NUnit.Framework;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Zakipoint.Framework.Driver;
using Zakipoint.Tests.Common;
using Zakipoint.UIAutomation.PageObjects;
using Zakipoint.UIAutomation.PageServices;
using static System.String;

namespace Zakipoint.Tests.Tests
{
    public class SetClient
    {
        #region Private Methods

        private readonly SetClientPageObjects _setClientPage;
        private readonly SetClientPage _setClient;
        private readonly LoginPage _login;

        #endregion

        #region Constructor

        public SetClient()
        {
            _setClientPage = new SetClientPageObjects();
            _setClient = new SetClientPage();
            _login = new LoginPage();
        }

        #endregion

        #region Base Methods

        [SetUp]
        public void Init()
        {
            Browser.Open(Browser.Config["url"]);
            _login.Login(CommonFunction.Data["username"], CommonFunction.Data["password"]);
        }

        [TearDown]
        public void Dispose()
        {
            Browser.Dispose();
        }

        #endregion

        #region Test Methods

        [Test, Category("Set Client Page Verification")]
        public void Verify_SetClient_Page()
        {
            var clientListFromDb = _setClient.GetClientListFromDb(CommonFunction.Data["username"]);
            var clientList = new List<string>();
            Assert.True(Browser.IsElementPresent(How.XPath, Format(_setClientPage.LabelByTextXPath, "Select Client To View") ));
            Assert.True(Browser.IsElementPresent(How.CssSelector, _setClientPage.UserManagementLinkCssSelector));
            Assert.True(Browser.IsElementPresent(How.CssSelector, _setClientPage.GoButtonCssSelector));
            if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
            {
                Assert.AreEqual(Browser.FindElement(How.CssSelector, _setClientPage.DropDownSelectedCssSelector).Text, "Select One");
                Browser.FindElement(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector).Click();
                var clientListElements = Browser.FindElements(How.XPath, _setClientPage.ClientListXPath);
                foreach(var client in clientListElements)
                {
                    clientList.Add(client.Text);
                }
                Assert.AreEqual(clientList, clientListFromDb);
            }
        }
      
        #endregion

    }
}

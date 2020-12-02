using NUnit.Framework;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Zakipoint.Framework.Driver;
using Zakipoint.UIAutomation.Common;
using Zakipoint.UIAutomation.PageObjects;
using Zakipoint.UIAutomation.PageServices;
using static System.String;
using Zakipoint.Tests.Common;
using Zakipoint.Tests.Base;
namespace Zakipoint.Tests.Tests
{
    public class SetClient : AbstractBase
    {
        #region Private Methods

        private readonly SetClientPageObjects _setClientPage;
        private readonly SetClientPage _setClient;
        private readonly LoginPage _login;
        private readonly CommonFunction _commonFunction;

        #endregion

        #region Constructor

        public SetClient()
        {
            _setClientPage = new SetClientPageObjects();
            _setClient = new SetClientPage();
            _login = new LoginPage();
            _commonFunction = new CommonFunction();
        }

        #endregion

        #region Base Methods
        
        public override void Init()
        {
            Browser.Open(Browser.Config["url"]);
            _login.Login(JsonDataReader.Data["username"], JsonDataReader.Data["password"]);
        }    
        public override void Dispose()
        {
            _commonFunction.Logout();         
        }

        #endregion

        #region Test Methods

        [Test, Category("Set Client Page Verification")]
        public void Verify_SetClient_Page()
        {           
            var clientListFromDb = JsonDataReader.Data["clientList"].Split(";");
            var clientList = new List<string>();
            Assert.True(Browser.IsElementPresent(How.XPath, Format(_setClientPage.LabelByTextXPath, "Select Client To View") ));
            Assert.True(Browser.IsElementPresent(How.CssSelector, _setClientPage.UserManagementLinkCssSelector));
            Assert.True(Browser.IsElementPresent(How.CssSelector, _setClientPage.GoButtonCssSelector));
            if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
            {
                //Browser.FindElement(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector).Click();
                Browser.JavaScriptOnclick(Browser.FindElement(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector));
                Assert.AreEqual(Browser.FindElement(How.CssSelector, _setClientPage.DropDownSelectedCssSelector).Text, "Select One");                
                var clientListElements = Browser.FindElements(How.XPath, _setClientPage.ClientListXPath);
                foreach(var client in clientListElements)
                {
                    clientList.Add(client.Text);
                }
                Assert.AreEqual(clientListFromDb,clientList);
            }
        }
      
        #endregion
    }
}
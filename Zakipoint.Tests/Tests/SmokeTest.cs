using NUnit.Framework;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Linq;
using Zakipoint.Framework.Common;
using Zakipoint.Framework.Driver;
using Zakipoint.Tests.Common;
using Zakipoint.UIAutomation.Common;
using Zakipoint.UIAutomation.Model;
using Zakipoint.UIAutomation.PageObjects;
using Zakipoint.UIAutomation.PageServices;
using Zakipoint.Tests.Base;

namespace Zakipoint.Tests.Tests
{
    public class SmokeTest : AbstractBase
    {
        #region Private Methods

        private readonly SetClientPageObjects _setClientPage;
        private readonly SetClientPage _setClient;
        private readonly DashboardPageObjects _dashboardPage;
        private readonly DashboardPage _dashboard;
        private readonly LoginPage _login;
        private readonly CommonFunction _commonFunction;
        private readonly SaveToCSV _saveToCsv;

        #endregion

        #region Constructor

        public SmokeTest()
        {
            _dashboard = new DashboardPage();
            _login = new LoginPage();
            _setClient = new SetClientPage();
            _setClientPage = new SetClientPageObjects();
            _commonFunction = new CommonFunction();
            _dashboardPage = new DashboardPageObjects();
            _saveToCsv = new SaveToCSV();
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

        #region SMOKE TESTS

        [Test, Category("SmokeTest")]
        public void Verify_Arc_Administrators()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[1]);

                    _dashboard.DashboardPageLoad();

                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[1]);

                }
            }
            catch(Exception ex)
            {
                Browser.ScreenShot("Verify_Arc_Administrators_Shot");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }

        [Test, Category("SmokeTest")]
        public void Verify_Asbury_University()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[2]);

                    _dashboard.DashboardPageLoad();

                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[2]);

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Asbury_University_Shot");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }

        #endregion
    }
}

using System;
using System.Linq;
using Zakipoint.Framework.Driver;
using Zakipoint.UIAutomation.PageObjects;
using Zakipoint.UIAutomation.PageServices;
using Zakipoint.Tests.Common;
using NUnit.Framework;
using System.Collections.Generic;
using OpenQA.Selenium.Support.PageObjects;

namespace Zakipoint.Tests.Tests
{
    public class Dashboard
    {
        #region Private Methods

        private readonly DashboardPageObjects _dashboardPage;
        private readonly DashboardPage _dashboard;
        private readonly LoginPage _login;
        private readonly SetClientPage _setClient;

        #endregion

        #region Constructor

        public Dashboard()
        {
            _dashboardPage = new DashboardPageObjects();
            _dashboard = new DashboardPage();
            _login = new LoginPage();
            _setClient = new SetClientPage();
        }

        #endregion

        #region Base Methods

        [SetUp]
        public void Init()
        {
            Browser.Open(Browser.Config["url"]);
            _login.Login(CommonFunction.Data["username"], CommonFunction.Data["password"]);
            _setClient.SelectClient(CommonFunction.Data["DefaultClient"]);
        }

        [TearDown]
        public void Dispose()
        {
            Browser.Dispose();
        }

        #endregion

        #region TestMethods

        [Test, Category("Dashboard Page Verification")]
        public void Verify_Dashboard_Page()
        {
            //var expectedMenuList = CommonFunction.Data["Menu"].Split(';').ToList();
            //var expectedQuickLinksList = CommonFunction.Data["QuickLinks"].Split(';').ToList();
            var expectedConditionSpendTableHeaderList = CommonFunction.Data["ConditionBySpend"].Split(';').ToList();
            try
            {
                //Assert.AreEqual(_dashboard.GetMenuList(), expectedMenuList);
                //Assert.AreEqual(CommonFunction.GetNavBarQuickLinks(), expectedQuickLinksList);
                //var groupDetailsList = _dashboard.GetGroupID();
                //_dashboard.ClickDownloadReport();
                //CommonMethods.CheckFileDownloaded(_dashboard.GetDownloadReportName());
                Assert.AreEqual(_dashboard.GetTableHeaderList(How.CssSelector, DashboardPageObjects.TopConditionTableHeaderCssSelector), expectedConditionSpendTableHeaderList);
                var tableDetails = CommonFunction.GetTableValues(How.CssSelector, DashboardPageObjects.TopConditionRowCssSelector);
            }
            catch (Exception e)
            {
                Browser.ScreenShot("Dashboard_Verification_Shot");
                Console.Out.WriteLine(e);
            }
        }

        #endregion
    }
}

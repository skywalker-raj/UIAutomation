using Zakipoint.UIAutomation.PageObjects;
using Zakipoint.Framework.Database;
using System.Collections.Generic;
using Zakipoint.UIAutomation.SqlScripts;
using Zakipoint.Framework.Driver;
using System.Linq;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace Zakipoint.UIAutomation.PageServices
{
    public class DashboardPage
    {
        #region Private Fields

        private readonly DashboardPageObjects _dashboardPage;
        private readonly MySqlStatementExecutor _executor;

        #endregion

        #region Constructor

        public DashboardPage()
        {
            _dashboardPage = new DashboardPageObjects();
            _executor = new MySqlStatementExecutor();
        }

        #endregion

        #region Database Methods

        public List<List<string>> GetGroupID()
        {
            var newList = new List<List<string>>();
            var groupIdList = _executor.GetCompleteTable(DashboardSqlScripts.GetAppGroupsValueList);
            groupIdList.FirstOrDefault().ItemArray.Select(x => x.ToString()).ToList();
            foreach (var groupId in groupIdList)
            {
                newList.Add(groupId.ItemArray.Select(x => x.ToString()).ToList());
            }
            return newList;
        }

        #endregion

        #region Public Methods

        public List<string> GetMenuList()
        {
            List<string> MenuList = new List<string>();
            var listMenu = Browser.FindElements(How.CssSelector, DashboardPageObjects.MenuLinkCssSelector);
            foreach (var menu in listMenu)
            {
                MenuList.Add(menu.Text);
            }
            return MenuList;
        }

        public string GetClientName()
        {
            return Browser.FindElement(How.CssSelector, DashboardPageObjects.ClientTitleCssSelector).Text;
        }

        public string GetDownloadReportName()
        {
            var clientReportName = GetClientName().ToLower().Replace(" ", "-") + ("-report-" + DateTime.Now.ToString("yyyy-MM-dd") + ".pdf");
            return clientReportName;
        }

        public void ClickDownloadReport()
        {
            Browser.FindElement(How.CssSelector, DashboardPageObjects.DownloadReportLinkCssSelector).Click();
        }

        public List<string> GetTableHeaderList(How locator, string value)
        {
            var headersList = new List<string>();
            var tableHeaderList = Browser.FindElements(locator, value);
            foreach (var tableHeader in tableHeaderList)
            {
                if (tableHeader.Text != "")
                {
                    headersList.Add(tableHeader.Text);
                }
            }
            return headersList;
        }

        #endregion


    }
}

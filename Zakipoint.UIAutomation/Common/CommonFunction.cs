using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Zakipoint.Framework.Driver;
using Zakipoint.UIAutomation.PageObjects;
using static System.String;

namespace Zakipoint.UIAutomation.Common
{
    public class CommonFunction
    {
        #region Private Properties

        private readonly DashboardPageObjects _dashboardPage = new DashboardPageObjects();

        #endregion


        #region Public Properties
        //public readonly IConfiguration Data = new ConfigurationBuilder().AddJsonFile(@"Data/Data.json").Build();
        public void ClickQuickLink()
        {
            Browser.FindElement(How.CssSelector, _dashboardPage.QuickLinkCssSelector).Click();
        }

        public List<string> GetNavBarQuickLinks()
        {
            List<string> QuickLinkList = new List<string>();
            ClickQuickLink();
            var quickLinkList = Browser.FindElements(How.CssSelector, _dashboardPage.QuickLinksCssSelector);
            foreach (var quickLink in quickLinkList)
            {
                QuickLinkList.Add(quickLink.Text);
            }
            return QuickLinkList;
        }

        public string GetTableValueByRowCol(int row = 1, int col = 2)
        {
            return Browser.FindElement(How.CssSelector, Format(_dashboardPage.TopConditionDetailsCssSelector, row, col)).Text;
        }

        public List<string> GetTableValuesListByCol(int col = 2)
        {
            List<string> valueList = new List<string>();
            var colvalue = Browser.FindElements(How.CssSelector, Format(_dashboardPage.TopConditionDetailsByColCssSelector, col));
            foreach (var value in colvalue)
            {
                if (value.Text != "")
                {
                    valueList.Add(value.Text);
                }
            }
            return valueList;
        }

        public List<string> GetTableValuesListByRow(int row = 1)
        {
            List<string> valueList = new List<string>();
            var rowvalue = Browser.FindElements(How.CssSelector, Format(_dashboardPage.TopConditionDetailsByRowCssSelector, row));
            foreach (var value in rowvalue)
            {
                if (value.Text != "")
                {
                    valueList.Add(value.Text);
                }
            }
            return valueList;
        }

        public List<List<string>> GetTableValues(How locator, string value)
        {
            var tableDetails = new List<List<string>>();
            int rowCount = Browser.FindElements(locator, value).Count;
            for (int i = 1; i <= rowCount; i++)
            {
                var rowDetails = GetTableValuesListByRow(i);
                tableDetails.Add(rowDetails);
            }
            return tableDetails;
        }

        public string RemoveUnicode(string messsage)
        {
            return Regex.Replace(messsage, @"[^\u0000-\u007F]+", string.Empty).Replace(",", "");
        }
        #endregion
    }
}
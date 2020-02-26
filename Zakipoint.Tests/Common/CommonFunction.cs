using Zakipoint.UIAutomation.PageObjects;
using Zakipoint.Framework.Driver;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Collections.Generic;
using static System.String;
using OpenQA.Selenium.Support.PageObjects;

namespace Zakipoint.Tests.Common
{
    public class CommonFunction
    {
        #region Private Properties

        public static readonly IConfiguration Data = new ConfigurationBuilder().AddJsonFile(@"Data/Data.json").Build();

        #endregion

        #region Public Properties

        public static void ClickQuickLink()
        {
            Browser.FindElement(How.CssSelector, DashboardPageObjects.QuickLinkCssSelector).Click();
        }

        public static List<string> GetNavBarQuickLinks()
        {
            List<string> QuickLinkList = new List<string>();
            ClickQuickLink();
            var quickLinkList = Browser.FindElements(How.CssSelector, DashboardPageObjects.QuickLinksCssSelector);
            foreach (var quickLink in quickLinkList)
            {
                QuickLinkList.Add(quickLink.Text);
            }
            return QuickLinkList;
        }

        public static string GetTableValueByRowCol(int row = 1, int col = 2)
        {
            return Browser.FindElement(How.CssSelector, Format(DashboardPageObjects.TopConditionDetailsCssSelector, row, col)).Text;
        }

        public static List<string> GetTableValuesListByCol(int col = 2)
        {
            List<string> valueList = new List<string>();
            var colvalue = Browser.FindElements(How.CssSelector, Format(DashboardPageObjects.TopConditionDetailsByColCssSelector, col));
            foreach (var value in colvalue)
            {
                if (value.Text != "")
                {
                    valueList.Add(value.Text);
                }
            }
            return valueList;
        }

        public static List<string> GetTableValuesListByRow(int row = 1)
        {
            List<string> valueList = new List<string>();
            var rowvalue = Browser.FindElements(How.CssSelector, Format(DashboardPageObjects.TopConditionDetailsByRowCssSelector, row));
            foreach (var value in rowvalue)
            {
                if (value.Text != "")
                {
                    valueList.Add(value.Text);
                }
            }
            return valueList;
        }

        public static List<List<string>> GetTableValues(How locator, string value)
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

        #endregion
    }
}

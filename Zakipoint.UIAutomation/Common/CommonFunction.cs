using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
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
        public string GetCurrentUrl()
        {
            return Browser.GetCurrentUrl();
        }
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
        public List<string> GetTableValuesListByRow( How rowDetailsLocator, string rowDetailsValue, int row = 1)
        {
            List<string> valueList = new List<string>();
            var rowvalue = Browser.FindElements(rowDetailsLocator, Format(rowDetailsValue, row));
            foreach (var value in rowvalue)
            {
                if (value.Text != "")
                {
                    valueList.Add(value.Text);
                }
            }
            return valueList;
        }
        public List<List<string>> GetTableValues(How RowLocator, string RowValue, How rowDetailsLocator, string rowDetailsValue)
        {
            var tableDetails = new List<List<string>>();
            int rowCount = Browser.FindElements(RowLocator, RowValue).Count;
            for (int i = 1; i <= rowCount; i++)
            {
                var rowDetails = GetTableValuesListByRow(rowDetailsLocator, rowDetailsValue, i);
                tableDetails.Add(rowDetails);
            }
            return tableDetails;
        }  
        public string RemoveUnicode(string messsage)
        {
            return Regex.Replace(messsage, @"[^\u0000-\u007F]+", string.Empty).Replace(",", "");
        }
        public void Logout()
        {
            if (Browser.IsElementPresent(How.CssSelector, _dashboardPage.UserLinkDropdownCssSelector))
            {
              Browser.JavaScriptOnclick(  Browser.FindElement(How.CssSelector, _dashboardPage.UserLinkDropdownCssSelector));
                Console.WriteLine("Click on UserLink Dropdown");
               Browser.JavaScriptOnclick( Browser.FindElement(How.CssSelector, Format(_dashboardPage.UserLinkDropdownListCssSelector, "(2)")));
                Console.WriteLine("Click on  logout Dropdown link");
                Thread.Sleep(3000);
            }
        }
        #endregion
    }
}
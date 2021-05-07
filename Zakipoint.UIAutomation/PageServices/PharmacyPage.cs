using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Threading;
using Zakipoint.Framework.Common;
using Zakipoint.Framework.Database;
using Zakipoint.Framework.Driver;
using Zakipoint.UIAutomation.Model;
using Zakipoint.UIAutomation.PageObjects;
using Zakipoint.UIAutomation.SqlScripts;

using static System.String;

namespace Zakipoint.UIAutomation.PageServices
{

   public  class PharmacyPage
    {
        #region Private Fields

        private readonly PharmacyPageObjects _pharmacyPage;
        private readonly PharmacySqlScripts _pharmacySqlScripts;
        private readonly MySqlStatementExecutor _executor;
        private readonly populationPageObjects _populationPage;

        #endregion
        public PharmacyPage()
        {
            _pharmacyPage = new PharmacyPageObjects();
            _pharmacySqlScripts = new PharmacySqlScripts();
            _executor = new MySqlStatementExecutor();
            _populationPage = new populationPageObjects();
        }
        public void GotoPharmacySection()
        {
            Browser.FindElement(How.CssSelector, _populationPage.QuickLinkCssSelector).Click();
            Console.WriteLine("Click Quick link icon");
            Console.WriteLine("conditional wait");
            //Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath((_populationPage.DropDownPopulation))));
            Browser.FindElement(How.CssSelector, _pharmacyPage.DropDownPharmacy).Click();
            Console.WriteLine("Population Drop down clicked ");

            //Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(Format(_populationPage.DemographicsButton))));
            Console.WriteLine("conditional wait");
        }

        //database part
        public List<populationAge> Expected_Pharmacy_Age(string customStartDate, string customEndDate)
        {
            List<populationAge> objList = new List<populationAge>();
            var dt = _executor.GetDataTable(_pharmacySqlScripts.ExpectedAgeDetails(customStartDate, customEndDate));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                populationAge obj = new populationAge
                {
                    age = dt.Rows[i]["age_band"].ToString(),
                    spend = dt.Rows[i]["p1_total_paid"].ToString().ToUpper(),
                    members = dt.Rows[i]["p1_member_count"].ToString()
                };
                objList.Add(obj);
            }
            return objList;
        }
        public List<populationAge> Map_AgeTile(List<List<string>> tableDetails)
        {
            List<populationAge> objList = new List<populationAge>();
            foreach (var item in tableDetails)
            {
                populationAge obj = new populationAge
                {
                    age = item[0].ToString(),
                    spend = item[1].ToString().Replace(",", "").Replace("K", "").Replace("$", "").Trim(),
                    members = item[2].ToString().Replace(",", "").Trim()
                };
                objList.Add(obj);
            }
            return objList;
        }
        public List<List<string>> Pharmacy_Age_Tile()
        {
            // Browser.PageScroll(0, 950); // cordinate 
            var tableData = CommonMethods.GetTableValues(How.CssSelector, _pharmacyPage.ageSvgBoxcssSelector, How.CssSelector, _pharmacyPage.ageSvgBoxDetailsByRowCssSelector);
            //  Browser.PageScroll(0, 0);
            return tableData;
        }


        //database part
        public List<populationGender> Expected_Pharmacy_Gender(string customStartDate, string customEndDate)
        {
            List<populationGender> objList = new List<populationGender>();
            var dt = _executor.GetDataTable(_pharmacySqlScripts.ExpectedGenderDetails(customStartDate, customEndDate));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                populationGender obj = new populationGender
                {
                    gender = dt.Rows[i]["gender"].ToString(),
                    spend = dt.Rows[i]["p1_total_paid"].ToString().ToUpper(),
                    members = dt.Rows[i]["p1_member_count"].ToString()
                };
                objList.Add(obj);
            }
            return objList;
        }
        public List<populationGender> Map_GenderTile(List<List<string>> tableDetails)
        {
            List<populationGender> objList = new List<populationGender>();
            foreach (var item in tableDetails)
            {
                populationGender obj = new populationGender
                {
                    gender = item[0].ToString(),
                    spend = item[1].ToString().Replace(",", "").Replace("K", "").Replace("$", "").Trim(),
                    members = item[2].ToString().Replace(",", "").Trim()
                };
                objList.Add(obj);
            }
            return objList;
        }
        public List<List<string>> Pharmacy_Gender_Tile()
        {
            // Browser.PageScroll(0, 950); // cordinate 
            var tableData = CommonMethods.GetTableValues(How.CssSelector, _pharmacyPage.genderSvgBoxcssSelector, How.CssSelector, _pharmacyPage.genderSvgBoxDetailsByRowCssSelector);
            //  Browser.PageScroll(0, 0);
            return tableData;
        }


    }
}

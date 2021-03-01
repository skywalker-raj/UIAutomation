using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Zakipoint.Framework.Common;
using Zakipoint.Framework.Database;
using Zakipoint.Framework.Driver;
using Zakipoint.UIAutomation.Model;
using Zakipoint.UIAutomation.PageObjects;
using Zakipoint.UIAutomation.SqlScripts;

using static System.String;
namespace Zakipoint.UIAutomation.PageServices
{
    public class PopulationPage
    {
        #region Private Fields


        private readonly populationPageObjects _populationPage;
        private readonly PopulationSqlScripts _populationSqlScripts;
        private readonly MySqlStatementExecutor _executor;
        private readonly DashboardPageObjects _dashboardPage;



        #endregion


        public PopulationPage()
        {
            _populationPage = new populationPageObjects();
            _populationSqlScripts = new PopulationSqlScripts();
            _executor = new MySqlStatementExecutor();

            _dashboardPage = new DashboardPageObjects();

        }

        
        //database part
        public List<populationAge> Expected_Population_Age(string customStartDate, string customEndDate)
        {
            List<populationAge> objList = new List<populationAge>();
            var dt = _executor.GetDataTable(_populationSqlScripts.ExpectedAgeDetails(customStartDate, customEndDate));
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
        public void Goto_Demographic_section()
        {
            Browser.FindElement(How.CssSelector, _populationPage.QuickLinkCssSelector).Click();
            Console.WriteLine("Click Quick link icon");
            Console.WriteLine("conditional wait");
            //Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath((_populationPage.DropDownPopulation))));
            Browser.FindElement(How.CssSelector, _populationPage.DropDownPopulation).Click();
            Console.WriteLine("Population Drop down clicked "); 

            //Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(Format(_populationPage.DemographicsButton))));
            Console.WriteLine("conditional wait");
            var element = Browser.FindElement(How.CssSelector, _populationPage.DemographicsButton);
            //Browser.FindElement(How.CssSelector,_populationPage.DemographicsButton).Click();
            Browser.JavaScriptOnclick(element);
            Console.WriteLine("Demographic button clicked");

            //Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(_populationPage.DemographicsButton)));
            Console.WriteLine("conditional wait");

        }

        public void goToCustomDateRangeSetter(string customStartDate, string customEndDate)
        {
            Browser.FindElement(How.XPath, _dashboardPage.ApplicationSettinsgByXPath).Click();
            Console.WriteLine("Click setting icon");
            Console.WriteLine("conditional wait");
            Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(_dashboardPage.MemberStatusChangeXPath)));
            Browser.FindElement(How.XPath, _populationPage.reportingDateRangeStatusChangeXPath).Click();
            Console.WriteLine("Member status: change link text ");
          //  Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(Format(_dashboardPage.RadioMemberByCssSelector, "termed02"))));
            Console.WriteLine("conditional wait");
            Browser.FindElement(How.CssSelector, Format(_populationPage.RadioMemberByCssSelector, "analysis04")).Click();
            Console.WriteLine("Choose radio button All Members ");
           // Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(_dashboardPage.ApplySettingXPath)));
            Console.WriteLine("conditional wait");
            //set date range
            // Browser.setAttribute(_populationPage.leftrangeSliderLabelCssSelector, customStartDate,_populationPage.rightrangeSliderLabelCssSelector, customEndDate);
            Browser.setDateRange(10);
            Browser.FindElement(How.XPath, _dashboardPage.ApplySettingXPath).Click();
            Console.WriteLine("Click on Apply Setting button");
          //  Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(_dashboardPage.ClientBoxLabelTextByXPath + "[contains(text(),'All Employees')]")));
            Console.WriteLine("conditional wait");
        }
        public List<List<string>> Age_Tile()
        {
            // Browser.PageScroll(0, 950); // cordinate 
            var tableData = CommonMethods.GetTableValues(How.CssSelector, _populationPage.ageSvgBoxcssSelector, How.CssSelector, _populationPage.ageSvgBoxDetailsByRowCssSelector);
            //  Browser.PageScroll(0, 0);
            return tableData;
        }

        public List<Population_Age_Pmpm> Expected_Population_Age_Pmpm()
        {
            List<Population_Age_Pmpm> objList = new List<Population_Age_Pmpm>();
            var dt = _executor.GetDataTable(_populationSqlScripts.ExpectedAgePmpmDetails());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Population_Age_Pmpm obj = new Population_Age_Pmpm
                {
                    age = dt.Rows[i]["age_band"].ToString(),
                    spend = dt.Rows[i]["p1_total_paid"].ToString().ToUpper(),
                    members = dt.Rows[i]["p1_member_count"].ToString(),
                    pmpm = dt.Rows[i]["p1_mm"].ToString()
                };
                objList.Add(obj);
            }
            return objList;
        }
        public void Goto_Pmpm_section()
        {
            Goto_Demographic_section();
            var element = Browser.FindElement(How.CssSelector, _populationPage.agePmpmButtonCssSelector);
            //Browser.FindElement(How.CssSelector,_populationPage.DemographicsButton).Click();
            Browser.JavaScriptOnclick(element);

        }

        public List<Population_Age_Pmpm> Map_AgeTile_Pmpm(List<List<string>> tableDetails)
        {
            List<Population_Age_Pmpm> objList = new List<Population_Age_Pmpm>();
            foreach (var item in tableDetails)
            {
                Population_Age_Pmpm obj = new Population_Age_Pmpm
                {
                    age = item[0].ToString(),
                    spend = item[1].ToString().Replace(",", "").Replace("K", "").Replace("$", "").Trim(),
                    members = item[2].ToString().Replace(",", "").Trim(),
                    pmpm = item[3].ToString().Replace(",", "").Replace("$", "").Trim()
                };
                objList.Add(obj);
            }
            return objList;
        }

        public List<List<string>> Age_Tile_Pmpm()
        {
            // Browser.PageScroll(0, 950); // cordinate 
            var tableData = CommonMethods.GetTableValues(How.CssSelector, _populationPage.agePmpmSvgBoxCssSelector, How.CssSelector, _populationPage.agePmpmSvgBoxDetailsByRowCssSelector);
            //  Browser.PageScroll(0, 0);
            return tableData;
        }


        //gender details
        public List<populationGender> Expected_Population_Gender()
        {
            List<populationGender> objList = new List<populationGender>();
            var dt = _executor.GetDataTable(_populationSqlScripts.ExpectedGenderDetails());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                populationGender obj = new populationGender
                {
                    gender = dt.Rows[i]["mbr_gender"].ToString(),
                    spend = dt.Rows[i]["p1_total_paid"].ToString().ToUpper(),
                    members = dt.Rows[i]["p1_member_count"].ToString()
                };
                objList.Add(obj);
            }
            return objList;
        }

        public List<List<string>> Gender_Tile()
        {
            // Browser.PageScroll(0, 950); // cordinate 
            var tableData = CommonMethods.GetTableValues(How.CssSelector, _populationPage.genderSvgBoxcssSelector, How.CssSelector, _populationPage.genderSvgBoxDetailsByRowCssSelector);
            //  Browser.PageScroll(0, 0);
            return tableData;
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

        public List<populationGenderPmpm> Expected_Population_Gender_Pmpm()
        {
            List<populationGenderPmpm> objList = new List<populationGenderPmpm>();
            var dt = _executor.GetDataTable(_populationSqlScripts.ExpectedGenderPmpmDetails());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                populationGenderPmpm obj = new populationGenderPmpm
                {
                    gender = dt.Rows[i]["mbr_gender"].ToString(),
                    spend = dt.Rows[i]["p1_total_paid"].ToString().ToUpper(),
                    members = dt.Rows[i]["P1_member_count"].ToString(),
                    pmpm = dt.Rows[i]["pm"].ToString()
                };
                objList.Add(obj);
            }
            return objList;
        }

        public List<List<String>> Gender_Tile_Pmpm()
        {
            // Browser.PageScroll(0, 950); // cordinate 
            var tableData = CommonMethods.GetTableValues(How.CssSelector, _populationPage.agePmpmSvgBoxCssSelector, How.CssSelector, _populationPage.agePmpmSvgBoxDetailsByRowCssSelector);
            //  Browser.PageScroll(0, 0);
            return tableData;
        }


        public List<populationGenderPmpm> Map_GenderTile_Pmpm(List<List<string>> tableDetails)
        {
            List<populationGenderPmpm> objList = new List<populationGenderPmpm>();
            foreach (var item in tableDetails)
            {
                populationGenderPmpm obj = new populationGenderPmpm
                {
                    gender = item[0].ToString(),
                    spend = item[1].ToString().Replace(",", "").Replace("K", "").Replace("$", "").Trim(),
                    members = item[2].ToString().Replace(",", "").Trim(),
                    pmpm = item[3].ToString().Replace(",", "").Replace("$", "").Trim()
                };
                objList.Add(obj);
            }
            return objList;
        }

        //relationship
        public List<PopulationRelation> Expected_Population_Relation()
        {
            List<PopulationRelation> objList = new List<PopulationRelation>();
            
            var dt = _executor.GetDataTable(_populationSqlScripts.ExpectedRelationDetails());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PopulationRelation obj = new PopulationRelation
                {
                    relation = dt.Rows[i]["mbr_relationship_desc"].ToString(),
                    spend = dt.Rows[i]["p1_total_paid"].ToString().ToUpper(),
                    members = dt.Rows[i]["p1_member_count"].ToString()
                };
                objList.Add(obj);
            }
            return objList;
        }

        public List<List<string>> Relation_Tile()
        {
            // Browser.PageScroll(0, 950); // cordinate 
            var tableData = CommonMethods.GetTableValues(How.CssSelector, _populationPage.genderSvgBoxcssSelector, How.CssSelector, _populationPage.genderSvgBoxDetailsByRowCssSelector);
            //  Browser.PageScroll(0, 0);
            return tableData;
        }
        public List<PopulationRelation> Map_RelationTile(List<List<string>> tableDetails)
        {
            List<PopulationRelation> objList = new List<PopulationRelation>();
            foreach (var item in tableDetails)
            {
                PopulationRelation obj = new PopulationRelation
                {
                    relation = item[0].ToString(),
                    spend = item[1].ToString().Replace(",", "").Replace("K", "").Replace("$", "").Trim(),
                    members = item[2].ToString().Replace(",", "").Trim()
                };
                objList.Add(obj);
            }
            return objList;
        }

        public List<PopulationRelationPmpm> Expected_Population_Relation_Pmpm()
        {
            List<PopulationRelationPmpm> objList = new List<PopulationRelationPmpm>();
            var dt = _executor.GetDataTable(_populationSqlScripts.ExpectedRelationPmpmDetails());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PopulationRelationPmpm obj = new PopulationRelationPmpm
                {
                    relation = dt.Rows[i]["mbr_relationship_desc"].ToString(),
                    spend = dt.Rows[i]["p1_total_paid"].ToString().ToUpper(),
                    members = dt.Rows[i]["P1_member_count"].ToString(),
                    pmpm = dt.Rows[i]["pm"].ToString()
                };
                objList.Add(obj);
            }
            return objList;
        }

        public List<List<String>> Relation_Tile_Pmpm()
        {
            // Browser.PageScroll(0, 950); // cordinate 
            var tableData = CommonMethods.GetTableValues(How.CssSelector, _populationPage.agePmpmSvgBoxCssSelector, How.CssSelector, _populationPage.agePmpmSvgBoxDetailsByRowCssSelector);
            //  Browser.PageScroll(0, 0);
            return tableData;
        }


        public List<PopulationRelationPmpm> Map_RelationTile_Pmpm(List<List<string>> tableDetails)
        {
            List<PopulationRelationPmpm> objList = new List<PopulationRelationPmpm>();
            foreach (var item in tableDetails)
            {
                PopulationRelationPmpm obj = new PopulationRelationPmpm
                {
                    relation = item[0].ToString(),
                    spend = item[1].ToString().Replace(",", "").Replace("K", "").Replace("$", "").Trim(),
                    members = item[2].ToString().Replace(",", "").Trim(),
                    pmpm = item[3].ToString().Replace(",", "").Replace("$", "").Trim()
                };
                objList.Add(obj);
            }
            return objList;
        }

        //plan
        public List<PopulationPlan> Expected_Population_Plan()
        {
            List<PopulationPlan> objList = new List<PopulationPlan>();
            var dt = _executor.GetDataTable(_populationSqlScripts.ExpectedPlanDetails());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PopulationPlan obj = new PopulationPlan
                {
                    plan = dt.Rows[i]["plan_desc"].ToString(),
                    spend = dt.Rows[i]["p1_total_paid"].ToString().ToUpper(),
                    members = dt.Rows[i]["p1_member_count"].ToString()
                };
                objList.Add(obj);
            }
            return objList;
        }

        public List<List<string>> Plan_Tile()
        {
            // Browser.PageScroll(0, 950); // cordinate 
            var tableData = CommonMethods.GetTableValues(How.CssSelector, _populationPage.genderSvgBoxcssSelector, How.CssSelector, _populationPage.genderSvgBoxDetailsByRowCssSelector);
            //  Browser.PageScroll(0, 0);
            return tableData;
        }
        public List<PopulationPlan> Map_PlanTile(List<List<string>> tableDetails)
        {
            List<PopulationPlan> objList = new List<PopulationPlan>();
            foreach (var item in tableDetails)
            {
                PopulationPlan obj = new PopulationPlan
                {
                    plan = item[0].ToString(),
                    spend = item[1].ToString().Replace(",", "").Replace("K", "").Replace("$", "").Trim(),
                    members = item[2].ToString().Replace(",", "").Trim()
                };
                objList.Add(obj);
            }
            return objList;
        }

        public List<PopulationPlanPmpm> Expected_Population_Plan_Pmpm()
        {
            List<PopulationPlanPmpm> objList = new List<PopulationPlanPmpm>();
            var dt = _executor.GetDataTable(_populationSqlScripts.ExpectedPlanPmpmDetails());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PopulationPlanPmpm obj = new PopulationPlanPmpm
                {
                    plan = dt.Rows[i]["mbr_gender"].ToString(),
                    spend = dt.Rows[i]["p1_total_paid"].ToString().ToUpper(),
                    members = dt.Rows[i]["P1_member_count"].ToString(),
                    pmpm = dt.Rows[i]["pm"].ToString()
                };
                objList.Add(obj);
            }
            return objList;
        }

        public List<List<String>> Plan_Tile_Pmpm()
        {
            // Browser.PageScroll(0, 950); // cordinate 
            var tableData = CommonMethods.GetTableValues(How.CssSelector, _populationPage.agePmpmSvgBoxCssSelector, How.CssSelector, _populationPage.agePmpmSvgBoxDetailsByRowCssSelector);
            //  Browser.PageScroll(0, 0);
            return tableData;
        }


        public List<PopulationPlanPmpm> Map_PlanTile_Pmpm(List<List<string>> tableDetails)
        {
            List<PopulationPlanPmpm> objList = new List<PopulationPlanPmpm>();
            foreach (var item in tableDetails)
            {
                PopulationPlanPmpm obj = new PopulationPlanPmpm
                {
                    plan = item[0].ToString(),
                    spend = item[1].ToString().Replace(",", "").Replace("K", "").Replace("$", "").Trim(),
                    members = item[2].ToString().Replace(",", "").Trim(),
                    pmpm = item[3].ToString().Replace(",", "").Replace("$", "").Trim()
                };
                objList.Add(obj);
            }
            return objList;
        }

        public List<List<string>> division_Tile()
        {
            // Browser.PageScroll(0, 950); // cordinate 
            var tableData = CommonMethods.GetTableValues(How.XPath, _populationPage.divisionmodalXPath, How.CssSelector, _populationPage.divisionModalDetailsByRowXPath);
            //  Browser.PageScroll(0, 0);
            return tableData;
        }

     
            public List<populationDivision> Map_DivisionTile(List<List<string>> tableDetails)
            {
                List<populationDivision> objList = new List<populationDivision>();
                foreach (var item in tableDetails)
                {
                populationDivision obj = new populationDivision
                {
                        division = item[0].ToString(),
                        spend = item[1].ToString().Replace(",", "").Replace("K", "").Replace("$", "").Trim(),
                        members = item[2].ToString().Replace(",", "").Trim()
                    };
                    objList.Add(obj);
                }
                return objList;
            }

      
    }



}
       

       
    
   


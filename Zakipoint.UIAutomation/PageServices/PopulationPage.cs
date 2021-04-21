using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
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

        public string ReadTotalSpend()
        {
          string spend =   Browser.FindElement(How.CssSelector, _populationPage.spendLabelCssSelector).Text;
          spend  = spend.Replace("Analyze", "").Replace("$", "").Replace("K", "").Replace(",", "").Trim();

           return spend;
        }

        public string ReadTotalMembers()
        {
            string spend = Browser.FindElement(How.CssSelector, _populationPage.memberLabelCssSelector).Text;
            spend = spend.Replace("K", "").Replace(",", "").Trim();
            return spend;
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
        public void GotoPopulationSection()
        {
            Browser.FindElement(How.CssSelector, _populationPage.QuickLinkCssSelector).Click();
            Console.WriteLine("Click Quick link icon");
            Console.WriteLine("conditional wait");
            //Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath((_populationPage.DropDownPopulation))));
            Browser.FindElement(How.CssSelector, _populationPage.DropDownPopulation).Click();
            Console.WriteLine("Population Drop down clicked "); 

            //Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(Format(_populationPage.DemographicsButton))));
            Console.WriteLine("conditional wait");
           

        }
        public void goToDemographicsSection()
        {
            var element = Browser.FindElement(How.CssSelector, _populationPage.DemographicsButton);
            //Browser.FindElement(How.CssSelector,_populationPage.DemographicsButton).Click();
            Browser.JavaScriptOnclick(element);
            Console.WriteLine("Demographic button clicked");

            //Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(_populationPage.DemographicsButton)));
            Console.WriteLine("conditional wait");
            Thread.Sleep(3000);
        }

        public void goToCustomDateRangeSetter()
        {
            Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("#dropdownMenuButton")));
            Browser.FindElement(How.XPath, _dashboardPage.ApplicationSettinsgByXPath).Click();
            Console.WriteLine("Click setting icon");
            Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(_dashboardPage.MemberStatusChangeXPath)));
            Console.WriteLine("conditional wait");
            Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(_dashboardPage.ApplySettingXPath)));
            Thread.Sleep(2000);
            Browser.FindElement(How.XPath, _populationPage.reportingDateRangeStatusChangeXPath).Click();
            Console.WriteLine("Member status: change link text ");
            Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(Format(_populationPage.RadioMemberByCssSelector, "analysis04"))));
            Console.WriteLine("conditional wait");
            Browser.FindElement(How.CssSelector, Format(_populationPage.RadioMemberByCssSelector, "analysis04")).Click();
            //  Console.WriteLine("Choose radio button All Members ");
            Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(_dashboardPage.ApplySettingXPath)));
            Console.WriteLine("conditional wait");
            //set date range
            Browser.setMinRange(10);
            Browser.setMaxRange(10);
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

        public List<Population_Age_Pmpm> Expected_Population_Age_Pmpm(string customStartDate, string customEndDate)
        {
            List<Population_Age_Pmpm> objList = new List<Population_Age_Pmpm>();
            var dt = _executor.GetDataTable(_populationSqlScripts.ExpectedAgePmpmDetails( customStartDate,  customEndDate));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Population_Age_Pmpm obj = new Population_Age_Pmpm
                {
                    age = dt.Rows[i]["age_band"].ToString(),
                    spend = dt.Rows[i]["p1_total_paid"].ToString().ToUpper(),
                    members = dt.Rows[i]["p1_member_count"].ToString(),
                    pmpm = dt.Rows[i]["pm"].ToString()
                };
                objList.Add(obj);
            }
            return objList;
        }
        public void Goto_Pmpm_section()
        {
            //Goto_Demographic_section();
            var element = Browser.FindElement(How.CssSelector, _populationPage.agePmpmButtonCssSelector);
            //Browser.FindElement(How.CssSelector,_populationPage.DemographicsButton).Click();
            Browser.JavaScriptOnclick(element);
            Thread.Sleep(3000);

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
                    pmpm = item[3].ToString().Replace(",", "").Replace("K", "").Replace("$", "").Trim()
                    // pmpm = item[3].Contains("K") ? (Convert.ToDecimal(item[3].ToString().Replace(",", "").Replace("K", "").Replace("$", "").Trim()) * 1000).ToString() : item[3].ToString().Replace(",", "").Replace("K", "").Replace("$", "").Trim()

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
        public List<populationGender> Expected_Population_Gender(string customStartDate, string customEndDate)
        {
            List<populationGender> objList = new List<populationGender>();
            string testString = _populationSqlScripts.ExpectedGenderDetails(customStartDate, customEndDate);
            var dt = _executor.GetDataTable(_populationSqlScripts.ExpectedGenderDetails(customStartDate, customEndDate));
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

        public List<populationGenderPmpm> Expected_Population_Gender_Pmpm(string customStartDate, String CustomEndDate)
        {
            List<populationGenderPmpm> objList = new List<populationGenderPmpm>();
            string testString = _populationSqlScripts.ExpectedGenderPmpmDetails(customStartDate, CustomEndDate);
            var dt = _executor.GetDataTable(_populationSqlScripts.ExpectedGenderPmpmDetails(customStartDate,  CustomEndDate));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                populationGenderPmpm obj = new populationGenderPmpm
                {
                    gender = dt.Rows[i]["gender"].ToString(),
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
            var tableData = CommonMethods.GetTableValues(How.CssSelector, _populationPage.genderPmpmSvgBoxCssSelector, How.CssSelector, _populationPage.genderPmpmSvgBoxDetailsByRowCssSelector);
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
                     pmpm = item[3].ToString().Replace(",", "").Replace("$", "").Trim(),
                    



                };
                objList.Add(obj);
            }
            return objList;
        }

        //relationship
        public List<PopulationRelation> Expected_Population_Relation(string customStartDate, string customEndDate)
        {
            List<PopulationRelation> objList = new List<PopulationRelation>();
            
            var dt = _executor.GetDataTable(_populationSqlScripts.ExpectedRelationDetails(customStartDate,customEndDate));
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
            var tableData = CommonMethods.GetTableValues(How.CssSelector, _populationPage.relationSvgBoxcssSelector, How.CssSelector, _populationPage.relationSvgBoxDetailsByRowCssSelector);
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

        public List<PopulationRelationPmpm> Expected_Population_Relation_Pmpm(string customStartDate,string customEndDate)
        {
            List<PopulationRelationPmpm> objList = new List<PopulationRelationPmpm>();
            var dt = _executor.GetDataTable(_populationSqlScripts.ExpectedRelationPmpmDetails(customStartDate,  customEndDate));
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
            var tableData = CommonMethods.GetTableValues(How.CssSelector, _populationPage.relationPmpmSvgBoxcssSelector, How.CssSelector, _populationPage.relationPmpmSvgBoxDetailsByRowCssSelector);
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
                    pmpm = item[3].ToString().Replace(",", "").Replace("K", "").Replace("$", "").Trim()
                };
                objList.Add(obj);
            }
            return objList;
        }

        //plan
        public List<PopulationPlan> Expected_Population_Plan(string customStartDate,string customEndDate)
        {
            List<PopulationPlan> objList = new List<PopulationPlan>();
            var dt = _executor.GetDataTable(_populationSqlScripts.ExpectedPlanDetails(customStartDate, customEndDate));
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
            var tableData = CommonMethods.GetTableValues(How.CssSelector, _populationPage.planSvgBoxcssSelector, How.CssSelector, _populationPage.planSvgBoxDetailsByRowCssSelector);
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

        public List<PopulationPlanPmpm> Expected_Population_Plan_Pmpm(string customStartDate, string customEndDate)
        {
            List<PopulationPlanPmpm> objList = new List<PopulationPlanPmpm>();
            var dt = _executor.GetDataTable(_populationSqlScripts.ExpectedPlanPmpmDetails(customStartDate,customEndDate));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PopulationPlanPmpm obj = new PopulationPlanPmpm
                {
                    plan = dt.Rows[i]["plan_desc"].ToString(),
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
            var tableData = CommonMethods.GetTableValues(How.CssSelector, _populationPage.planPmpmSvgBoxcssSelector, How.CssSelector, _populationPage.planPmpmSvgBoxDetailsByRowCssSelector);
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
            var tableData = CommonMethods.GetTableValues(How.CssSelector, _populationPage.divisionSvgBoxcssSelector, How.CssSelector, _populationPage.divisionSvgBoxDetailsByRowCssSelector);
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

     


        public List<populationDivision> Expected_Population_Division(string customStartDate, string customEndDate)
        {
            List<populationDivision> objList = new List<populationDivision>();

            var dt = _executor.GetDataTable(_populationSqlScripts.ExpectedDivisionDetails(customStartDate, customEndDate));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                populationDivision obj = new populationDivision
                {
                    division = dt.Rows[i]["division_name"].ToString(),
                    spend = dt.Rows[i]["p1_total_paid"].ToString().ToUpper(),
                    members = dt.Rows[i]["p1_member_count"].ToString()
                };
                objList.Add(obj);
            }
            return objList;
        }


        //plantype
        public List<PopulationPlanType> Expected_Population_PlanType(string customStartDate, string customEndDate)
        {
            List<PopulationPlanType> objList = new List<PopulationPlanType>();
            var dt = _executor.GetDataTable(_populationSqlScripts.ExpectedPlanTypeDetails(customStartDate, customEndDate));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PopulationPlanType obj = new PopulationPlanType
                {
                    planType = dt.Rows[i]["plan_type_desc"].ToString(),
                    spend = dt.Rows[i]["p1_total_paid"].ToString().ToUpper(),
                    members = dt.Rows[i]["p1_member_count"].ToString()
                };
                objList.Add(obj);
            }
            return objList;
        }

        public List<List<string>> PlanType_Tile()
        {
            // Browser.PageScroll(0, 950); // cordinate 
            var tableData = CommonMethods.GetTableValues(How.CssSelector, _populationPage.planTypeSvgBoxcssSelector, How.CssSelector, _populationPage.planTypeSvgBoxDetailsByRowCssSelector);
            //  Browser.PageScroll(0, 0);
            return tableData;
        }


        public List<PopulationPlanType> Map_PlanTypeTile(List<List<string>> tableDetails)
        {
            List<PopulationPlanType> objList = new List<PopulationPlanType>();
            foreach (var item in tableDetails)
            {
                PopulationPlanType obj = new PopulationPlanType
                {
                    planType = item[0].ToString(),
                    spend = item[1].ToString().Replace(",", "").Replace("K", "").Replace("$", "").Trim(),
                    members = item[2].ToString().Replace(",", "").Trim()
                };
                objList.Add(obj);
            }
            return objList;
        }

        //PLANTYPEPMPM

        public List<PopulationPlanTypePmpm> Expected_Population_PlanType_Pmpm(string customStartDate, string customEndDate)
        {
            List<PopulationPlanTypePmpm> objList = new List<PopulationPlanTypePmpm>();
            string test = _populationSqlScripts.ExpectedPlanTypePmpmDetails(customStartDate, customEndDate);
            var dt = _executor.GetDataTable(_populationSqlScripts.ExpectedPlanTypePmpmDetails(customStartDate, customEndDate));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PopulationPlanTypePmpm obj = new PopulationPlanTypePmpm
                {
                    planType = dt.Rows[i]["plan_type_desc"].ToString(),
                    spend = dt.Rows[i]["p1_total_paid"].ToString().ToUpper(),
                    members = dt.Rows[i]["P1_member_count"].ToString(),
                    pmpm = dt.Rows[i]["pm"].ToString()
                };
                objList.Add(obj);
            }
            return objList;
        }
        public List<List<String>> PlanType_Tile_Pmpm()
        {
            // Browser.PageScroll(0, 950); // cordinate 
            var tableData = CommonMethods.GetTableValues(How.CssSelector, _populationPage.planTypePmpmSvgBoxcssSelector, How.CssSelector, _populationPage.planTypePmpmSvgBoxDetailsByRowCssSelector);
            //  Browser.PageScroll(0, 0);
            return tableData;
        }
        public List<PopulationPlanTypePmpm> Map_PlanTypeTile_Pmpm(List<List<string>> tableDetails)
        {
            List<PopulationPlanTypePmpm> objList = new List<PopulationPlanTypePmpm>();
            foreach (var item in tableDetails)
            {
                PopulationPlanTypePmpm obj = new PopulationPlanTypePmpm
                {
                    planType = item[0].ToString(),
                    spend = item[1].ToString().Replace(",", "").Replace("K", "").Replace("$", "").Trim(),
                    members = item[2].ToString().Replace(",", "").Trim(),
                    pmpm = item[3].ToString().Replace(",", "").Replace("$", "").Replace("K", "").Trim()
                };
                objList.Add(obj);
            }
            return objList;
        }


        //LOCATION
        public List<populationLocation> Expected_Population_Location(string customStartDate, string customEndDate)
        {
            List<populationLocation> objList = new List<populationLocation>();
            var dt = _executor.GetDataTable(_populationSqlScripts.ExpectedLocationDetails(customStartDate, customEndDate));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                populationLocation obj = new populationLocation
                {
                    mbrState = dt.Rows[i]["plan_type_desc"].ToString(),
                    spend = dt.Rows[i]["p1_total_paid"].ToString().ToUpper(),
                    members = dt.Rows[i]["p1_member_count"].ToString()
                };
                objList.Add(obj);
            }
            return objList;
        }

        public List<List<string>> location_Tile()
        {
            // Browser.PageScroll(0, 950); // cordinate 
            var tableData = CommonMethods.GetTableValues(How.CssSelector, _populationPage.locationSvgBoxcssSelector, How.CssSelector, _populationPage.locationSvgBoxDetailsByRowCssSelector);
            //  Browser.PageScroll(0, 0);
            return tableData;
        }


        public List<populationLocation> Map_LocationTile(List<List<string>> tableDetails)
        {
            List<populationLocation> objList = new List<populationLocation>();
            foreach (var item in tableDetails)
            {
                populationLocation obj = new populationLocation
                {
                    mbrState = item[0].ToString(),
                    spend = item[1].ToString().Replace(",", "").Replace("K", "").Replace("$", "").Trim(),
                    members = item[2].ToString().Replace(",", "").Trim()
                };
                objList.Add(obj);
            }
            return objList;
        }



    }









}




       

       
    
   


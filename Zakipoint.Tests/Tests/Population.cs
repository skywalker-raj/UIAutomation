using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Zakipoint.Framework.Common;
using Zakipoint.Framework.Driver;
using Zakipoint.Tests.Base;
using Zakipoint.Tests.Common;
using Zakipoint.UIAutomation.Common;
using Zakipoint.UIAutomation.Model;
using Zakipoint.UIAutomation.PageObjects;
using Zakipoint.UIAutomation.PageServices;

namespace Zakipoint.Tests.Tests
{
  public class Population: AbstractBase
    { 
       
    /*private readonly DashboardPageObjects _dashboardPage;
    private readonly DashboardPage _dashboard;*/
    private readonly LoginPage _login;
    private readonly SetClientPage _setClient;
    private readonly CommonFunction _commonFunction;
    private readonly SaveToCSV _saveToCsv;
    private readonly PopulationPage _populationService;
        private readonly populationPageObjects _populationpageObj;
        private readonly DashboardPage _dashboard;

        public Population()
    {
       // _dashboard = new DashboardPage();
        _login = new LoginPage();
        _setClient = new SetClientPage();
        _commonFunction = new CommonFunction();
        /*_dashboardPage = new DashboardPageObjects();*/
        _saveToCsv = new SaveToCSV();
         _populationService = new PopulationPage();
           _populationpageObj = new populationPageObjects();
            _dashboard = new DashboardPage();
        }
    public override void Init()
    {
        Browser.Open(Browser.Config["url"]);
        _login.Login(JsonDataReader.Data["username"], JsonDataReader.Data["password"]);
        
            _setClient.SelectClient(JsonDataReader.Data["DefaultClient"]);
        CommonObject.DefaultClientSuffix = JsonDataReader.Data["DefaultClientSuffix"];
            _populationService.goToCustomDateRangeSetter();
            Thread.Sleep(3000);
            _populationService.GotoPopulationSection();
          //  Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='z5popmlc001']")));
            Thread.Sleep(3000);
        }
    public override void Dispose()
    {
        _commonFunction.Logout();
        Browser.Dispose();        
    }



        #region TestMethods
        [Test, Order(1), Category("Demographics-Age Tile Details-Members-All")]
        public void Demographics_Age_Members_All()
        {
            try
            {
                _populationService.goToDemographicsSection();
                Thread.Sleep(3000);
             //   Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='z5popmlc001']")));
                var Actual_Result = _populationService.Map_AgeTile(_populationService.Age_Tile());
                var Expected_Result = _populationService.Expected_Population_Age(_dashboard.StartDate(), _dashboard.EndDate());
                
                Console.WriteLine("Age details  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength-1; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].age, Actual_Result[i+1].age, "Population","Division by Age" ,"Age", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i+1].spend, "Population", "Division by Age","Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i+1].members, "Population", "Division by Age", "Members", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength-1; i++)
                {
                    Assert.IsTrue(Expected_Result[i].age == Actual_Result[i+1].age);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i+1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i+1].members);
                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Age Details");
               
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", "Division by Age", "Age", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }
        // gender Tile 
        [Test, Order(2), Category("Demographics-Gender Tile Details-Members-All")]
        public void GenderTileDetails()
        {
            try
            {
                _populationService.goToDemographicsSection();
                var Actual_Result = _populationService.Map_GenderTile(_populationService.Gender_Tile());
                var Expected_Result = _populationService.Expected_Population_Gender(_dashboard.StartDate(), _dashboard.EndDate());
                Console.WriteLine("Details of Gender from database:" + CommonMethods.ObjectToXml(Expected_Result));

                Console.WriteLine("Gender details  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength - 1; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].gender, Actual_Result[i + 1].gender, "Population", " Division by Gender ", "Gender", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i + 1].spend, "Population", " Division by Gender ", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i + 1].members, "Population", " Division by Gender ", "Members", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength - 1; i++)
                {
                    Assert.IsTrue(Expected_Result[i].gender == Actual_Result[i + 1].gender);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i + 1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i + 1].members);
                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Gender Details");
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", " Division by Gender ", "Gender", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }
        //relationship members
        [Test, Order(3), Category("Demographics-Relation Tile Details-Members-All")]
        public void Demographics_Relation_Members_All()
        {
            try
            {
                _populationService.goToDemographicsSection();
                var Actual_Result = _populationService.Map_RelationTile(_populationService.Relation_Tile());
                var Expected_Result = _populationService.Expected_Population_Relation(_dashboard.StartDate(), _dashboard.EndDate());
                Console.WriteLine("Details of Gender from database:" + CommonMethods.ObjectToXml(Expected_Result));

                Console.WriteLine("Details of Relation from database:" + CommonMethods.ObjectToXml(Expected_Result));

                Console.WriteLine("Relation details  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength - 1; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].relation, Actual_Result[i + 1].relation, "Population", " Division by Relation ", "Relation", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i + 1].spend, "Population", " Division by Relation ", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i + 1].members, "Population", " Division by Relation ", "Members", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength - 1; i++)
                {
                    Assert.IsTrue(Expected_Result[i].relation == Actual_Result[i + 1].relation);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i + 1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i + 1].members);
                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Relation Details");
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", " Division by Relation ", "Population", "Relation", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }

        //plan members
        [Test, Order(4), Category("Demographics-Plan Tile Details-Members-All")]
        public void Demographics_Plan_Members_All()
        {
            try
            {
                _populationService.goToDemographicsSection();
                Thread.Sleep(3000);
                var Actual_Result = _populationService.Map_PlanTile(_populationService.Plan_Tile());

                var Expected_Result = _populationService.Expected_Population_Plan(_dashboard.StartDate(), _dashboard.EndDate());
                Console.WriteLine("Details of Plan from database:" + CommonMethods.ObjectToXml(Expected_Result));

                /* Console.WriteLine("Plan details  from UI:" + CommonMethods.ObjectToXml(Actual_Result));*/
                Console.WriteLine("Plan details  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength - 1; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].plan, Actual_Result[i + 1].plan, "Population", " Division by Plan ", "Plan", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i + 1].spend, "Population", " Division by Plan ", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i + 1].members, "Population", " Division by Plan ", "Members",  "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength - 1; i++)
                {
                    Assert.IsTrue(Expected_Result[i].plan == Actual_Result[i + 1].plan);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i + 1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i + 1].members);
                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Plan Details");
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", " Division by Plan-PMPM ", "Plan", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }
        [Test, Order(5), Category("Demographics-Plan Tile Details-Members-All")]

        //plan type
        public void Demographics_PlanType_Members_All()
        {
            try
            {
                _populationService.goToDemographicsSection();
                Thread.Sleep(3000);
                var Actual_Result = _populationService.Map_PlanTypeTile(_populationService.PlanType_Tile());
                var Expected_Result = _populationService.Expected_Population_PlanType(_dashboard.StartDate(), _dashboard.EndDate());
                Console.WriteLine("Details of PlanType from database:" + CommonMethods.ObjectToXml(Expected_Result));

                /* Console.WriteLine("Plan details  from UI:" + CommonMethods.ObjectToXml(Actual_Result));*/
                Console.WriteLine("PlanType details  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength - 1; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].planType, Actual_Result[i + 1].planType, "Population", " Division by PlanType ", "PlanType", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i + 1].spend, "Population", " Division by PlanType ", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i + 1].members, "Population", " Division by PlanType ", "Members", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength - 1; i++)
                {
                    Assert.IsTrue(Expected_Result[i].planType == Actual_Result[i + 1].planType);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i + 1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i + 1].members);
                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Plan Details");
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", " Division by PlanType ", "PlanType", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }
        //div members
        [Test, Order(6), Category("Demographics-Division Tile Details-Members-All")]
        public void Demographics_Division_Members_All()
        {
            try
            {
                _populationService.goToDemographicsSection();
                var Actual_Result = _populationService.Map_DivisionTile(_populationService.division_Tile());
                var Expected_Result = _populationService.Expected_Population_Division(_dashboard.StartDate(), _dashboard.EndDate());
                Console.WriteLine("Details of Division from database:" + CommonMethods.ObjectToXml(Expected_Result));
                Console.WriteLine("DIvision details  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength - 1; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].division, Actual_Result[i + 1].division, "Population", " Division by division ", "division", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i + 1].spend, "Population", " Division by division ", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i + 1].members, "Population", " Division by division ", "Members", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength - 1; i++)
                {
                    Assert.IsTrue(Expected_Result[i].division == Actual_Result[i + 1].division);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i + 1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i + 1].members);
                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Age Details");
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", " Division by division ", "Age", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }

        //location members

        [Test, Order(7), Category("Demographics-Location Tile Details-Members-All")]
        public void Demographics_Location_Members_All()
        {
            try
            {
                _populationService.goToDemographicsSection();
                var Actual_Result = _populationService.Map_LocationTile(_populationService.location_Tile());
                var Expected_Result = _populationService.Expected_Population_Location(_dashboard.StartDate(), _dashboard.EndDate());
                Console.WriteLine("Details of Location from database:" + CommonMethods.ObjectToXml(Expected_Result));
                Console.WriteLine("Location details  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength - 1; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].mbrState, Actual_Result[i + 1].mbrState, "Population", "Location wise division", "Location", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i + 1].spend, "Population", "Location wise division", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i + 1].members, "Population", "Location wise division", "Members", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength - 1; i++)
                {
                    Assert.IsTrue(Expected_Result[i].mbrState == Actual_Result[i + 1].mbrState);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i + 1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i + 1].members);
                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Age Details");
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", "Location wise division", "Location", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }

        //spend and members with sum of spend and members
        [Test, Order(8), Category("Demographics-Total spend vs sum of spend")]
        public void Demographics_TotalSpend()
        {
            try
            {
                _populationService.goToDemographicsSection();
                Thread.Sleep(4000);
                var Actual_Result_Age = _populationService.Map_AgeTile(_populationService.Age_Tile());
                var Actual_Result_Gender = _populationService.Map_GenderTile(_populationService.Gender_Tile());
                var Actual_Result_Relation = _populationService.Map_RelationTile(_populationService.Relation_Tile());
                var Actual_Result_Plan = _populationService.Map_AgeTile(_populationService.Age_Tile());

                var objectLength_age = Actual_Result_Age.Count;
                var objectLength_gender = Actual_Result_Gender.Count;
                var objectLength_relation = Actual_Result_Relation.Count;
                var objectLength_plan = Actual_Result_Plan.Count;

                decimal spendcolumn_sum_age = 0;
                decimal spendcolumn_sum_gender = 0;
                decimal spendcolumn_sum_relation = 0;
                decimal spendcolumn_sum_plan = 0;

                for (int i = 1; i <= objectLength_age - 1; i++)
                {
                    var x = Actual_Result_Age[i].spend;
                    spendcolumn_sum_age += Convert.ToDecimal(Actual_Result_Age[i].spend);
                }

                for (int i = 1; i <= objectLength_gender - 1; i++)
                {
                    var x = Actual_Result_Gender[i].spend;
                    spendcolumn_sum_gender += Convert.ToDecimal(Actual_Result_Gender[i].spend);
                }

                for (int i = 1; i <= objectLength_relation - 1; i++)
                {
                    var x = Actual_Result_Relation[i].spend;
                    spendcolumn_sum_relation += Convert.ToDecimal(Actual_Result_Relation[i].spend);
                }

                for (int i = 1; i <= objectLength_plan - 1; i++)
                {
                    var x = Actual_Result_Plan[i].spend;
                    spendcolumn_sum_plan += Convert.ToDecimal(Actual_Result_Plan[i].spend);
                }

                string spend_sum_age = Math.Round(spendcolumn_sum_age).ToString();
                string spend_sum_gender = Math.Round(spendcolumn_sum_gender).ToString();
                string spend_sum_relation = Math.Round(spendcolumn_sum_relation).ToString();
                string spend_sum_plan = Math.Round(spendcolumn_sum_plan).ToString();

                Console.WriteLine("spend sum age " + spendcolumn_sum_age);
                Console.WriteLine("spend sum gender " + spendcolumn_sum_gender);
                Console.WriteLine("spend sum relation " + spendcolumn_sum_relation);
                Console.WriteLine("spend sum plan " + spendcolumn_sum_plan);

                string Totalspend = _populationService.ReadTotalSpend();

                Console.WriteLine("spend sum" + Totalspend);

                _saveToCsv.SaveTestCase(spend_sum_age, Totalspend, "Population", "Total spend vs sum of spend", "Total spend-Age", "Expected value should be equal to actual value");
                _saveToCsv.SaveTestCase(spend_sum_gender, Totalspend, "Population", "Total spend vs sum of spend", "Total spend-Gender", "Expected value should be equal to actual value");
                _saveToCsv.SaveTestCase(spend_sum_relation, Totalspend, "Population", "Total spend vs sum of spend", "Total spend-Relation", "Expected value should be equal to actual value");
                _saveToCsv.SaveTestCase(spend_sum_plan, Totalspend, "Population", "Total spend vs sum of spend", "Total spend-Plan", "Expected value should be equal to actual value");


            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Total Spend Details");
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", "Total spend vs sum of spend", "Total Spend", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
                Assert.IsTrue(false);
            }
        }

        [Test, Order(9), Category("Demographics-Total members vs sum of members")]
        public void Demographics_TotalMembers()
        {
            try
            {
                _populationService.goToDemographicsSection();
                Thread.Sleep(4000);
                var Actual_Result_Age = _populationService.Map_AgeTile(_populationService.Age_Tile());
                var Actual_Result_Gender = _populationService.Map_GenderTile(_populationService.Gender_Tile());
                var Actual_Result_Relation = _populationService.Map_RelationTile(_populationService.Relation_Tile());
                var Actual_Result_Plan = _populationService.Map_AgeTile(_populationService.Age_Tile());

                var objectLength_age = Actual_Result_Age.Count;
                var objectLength_gender = Actual_Result_Gender.Count;
                var objectLength_relation = Actual_Result_Relation.Count;
                var objectLength_plan = Actual_Result_Plan.Count;

                decimal membercolumn_sum_age = 0;
                decimal membercolumn_sum_gender = 0;
                decimal membercolumn_sum_relation = 0;
                decimal membercolumn_sum_plan = 0;

                for (int i = 1; i <= objectLength_age - 1; i++)
                {
                    var x = Actual_Result_Age[i].spend;
                    membercolumn_sum_age += Convert.ToDecimal(Actual_Result_Age[i].members);
                }

                for (int i = 1; i <= objectLength_gender - 1; i++)
                {
                    var x = Actual_Result_Gender[i].spend;
                    membercolumn_sum_gender += Convert.ToDecimal(Actual_Result_Gender[i].members);
                }

                for (int i = 1; i <= objectLength_relation - 1; i++)
                {
                    var x = Actual_Result_Relation[i].spend;
                    membercolumn_sum_relation += Convert.ToDecimal(Actual_Result_Relation[i].members);
                }

                for (int i = 1; i <= objectLength_plan - 1; i++)
                {
                    var x = Actual_Result_Plan[i].spend;
                    membercolumn_sum_plan += Convert.ToDecimal(Actual_Result_Plan[i].members);
                }

                string spend_sum_age = Math.Round(membercolumn_sum_age).ToString();
                string spend_sum_gender = Math.Round(membercolumn_sum_gender).ToString();
                string spend_sum_relation = Math.Round(membercolumn_sum_relation).ToString();
                string spend_sum_plan = Math.Round(membercolumn_sum_plan).ToString();

                Console.WriteLine("members sum age " + membercolumn_sum_age);
                Console.WriteLine("members sum gender " + membercolumn_sum_gender);
                Console.WriteLine("members sum relation " + membercolumn_sum_relation);
                Console.WriteLine("members sum plan " + membercolumn_sum_plan);

                string Totalspend = _populationService.ReadTotalMembers();

                Console.WriteLine("members -spend sum " + Totalspend);

                _saveToCsv.SaveTestCase(spend_sum_age, Totalspend, "Population", "Total members vs sum of members", "Total members-Age", "Expected value should be equal to actual value");
                _saveToCsv.SaveTestCase(spend_sum_gender, Totalspend, "Population", "Total members vs sum of members", "Total members-Gender", "Expected value should be equal to actual value");
                _saveToCsv.SaveTestCase(spend_sum_relation, Totalspend, "Population", "Total members vs sum of members", "Total members-Relation", "Expected value should be equal to actual value");
                _saveToCsv.SaveTestCase(spend_sum_plan, Totalspend, "Population", "Total members vs sum of members", "Total members-Plan", "Expected value should be equal to actual value");


            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Total Spend Details");
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", "Total members vs sum of members", "Total Spend", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
                Assert.IsTrue(false);
            }
        }

        // end of spend and members with sum of spend and members

        [Test, Order(10), Category("Demographics-Age Tile Details-Members-PMPM")]
        public void Demographics_Age_Pmpm_All()
        {
            try
            {
                _populationService.goToDemographicsSection();
                _populationService.Goto_Pmpm_section();
                Thread.Sleep(3000);
                var Actual_Result = _populationService.Map_AgeTile_Pmpm(_populationService.Age_Tile_Pmpm());
                var Expected_Result = _populationService.Expected_Population_Age_Pmpm(_dashboard.StartDate(), _dashboard.EndDate());

                Console.WriteLine("Age details  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                Console.WriteLine("Details of Age-PMPM from database:" + CommonMethods.ObjectToXml(Expected_Result));
                Console.WriteLine("Age details-PMPM  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength - 1; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].age, Actual_Result[i + 1].age, "Population", " Division by Age -PMPM ", "Age", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i + 1].spend, "Population", " Division by Age -PMPM ", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i + 1].members, "Population", " Division by Age -PMPM ", "Members", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].pmpm, Actual_Result[i + 1].pmpm, "Population", " Division by Age -PMPM ", "PMPM", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength - 1; i++)
                {
                    Assert.IsTrue(Expected_Result[i].age == Actual_Result[i + 1].age);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i + 1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i + 1].members);
                    Assert.IsTrue(Expected_Result[i].pmpm == Actual_Result[i + 1].pmpm);
                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Age Details");
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", " Division by Gender ", "Age", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }
        //gender tile pmpm
        [Test, Order(11), Category("Demographics-Gender Tile Details-Members-PMPM")]
        public void Demographics_Gender_Pmpm_All()
        {
            try
            {
                _populationService.goToDemographicsSection();
                var Expected_Result = _populationService.Expected_Population_Gender_Pmpm(_dashboard.StartDate(), _dashboard.EndDate());
                Console.WriteLine("Details of Gender-pmpm from database:" + CommonMethods.ObjectToXml(Expected_Result));

                _populationService.Goto_Pmpm_section();
                var Actual_Result = _populationService.Map_GenderTile_Pmpm(_populationService.Gender_Tile_Pmpm());

                Console.WriteLine("Gender details-PMPM  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength-1; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].gender, Actual_Result[i+1].gender, "Population", " Division by Gender-PMPM ", "Gender-PMPM", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i+1].spend, "Population", " Division by Gender-PMPM ", "Spend-PMPM", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i+1].members, "Population", " Division by Gender-PMPM ", "Members-PMPM", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].pmpm, Actual_Result[i+1].pmpm, "Population", " Division by Gender-PMPM ", "PMPM", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength-1; i++)
                {
                    Assert.IsTrue(Expected_Result[i].gender == Actual_Result[i+1].gender);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i+1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i+1].members);
                    Assert.IsTrue(Expected_Result[i].pmpm == Actual_Result[i+1].pmpm);
                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Gender Details");
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", " Division by Gender-PMPM ", "Gender-PMPM", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }

        //relationship pmpm
        [Test, Order(12), Category("Demographics-Relation Tile Details-Members-PmPm")]
        public void Demographics_Relation_Pmpm_All()
        {
            try
            {
                _populationService.goToDemographicsSection();
                var Expected_Result = _populationService.Expected_Population_Relation_Pmpm(_dashboard.StartDate(), _dashboard.EndDate());
                Console.WriteLine("Details of Relation-pmpm from database:" + CommonMethods.ObjectToXml(Expected_Result));

                _populationService.Goto_Pmpm_section();
                var Actual_Result = _populationService.Map_RelationTile_Pmpm(_populationService.Relation_Tile_Pmpm());

                Console.WriteLine("Relation details-PMPM  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength-1; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].relation, Actual_Result[i+1].relation, "Population", " Division by Relation-PMPM ", "Relation", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i+1].spend, "Population", " Division by Relation-PMPM ","Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i+1].members, "Population", " Division by Relation-PMPM ", "Members", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].pmpm, Actual_Result[i+1].pmpm, "Population", " Division by Relation-PMPM ", "PMPM", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength-1; i++)
                {
                    Assert.IsTrue(Expected_Result[i].relation == Actual_Result[i+1].relation);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i+1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i+1].members);
                    Assert.IsTrue(Expected_Result[i].pmpm == Actual_Result[i+1].pmpm);
                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Age Details");
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", " Division by Relation-PMPM ", "Relationship-Pmpm", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }


        //plan pmpm
        [Test, Order(13), Category("Demographics-Plan Tile Details-Members-Pmpm")]
        public void Demographics_Plan_Pmpm_All()
        {
            try
            {
                _populationService.goToDemographicsSection();
                var Expected_Result = _populationService.Expected_Population_Plan_Pmpm(_dashboard.StartDate(), _dashboard.EndDate());
                Console.WriteLine("Details of Plan-pmpm from database:" + CommonMethods.ObjectToXml(Expected_Result));

                _populationService.Goto_Pmpm_section();
                var Actual_Result = _populationService.Map_PlanTile_Pmpm(_populationService.Plan_Tile_Pmpm());

                Console.WriteLine("Plan details-PMPM  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength-1; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].plan, Actual_Result[i+1].plan, "Population", " Division by Plan-PMPM ", "Plan-Pmpm", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i+1].spend, "Population", " Division by Plan-PMPM ", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i+1].members, "Population", " Division by Plan-PMPM ", "Members", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].pmpm, Actual_Result[i+1].pmpm, "Population", " Division by Plan-PMPM ", "PMPM", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength-1; i++)
                {
                    Assert.IsTrue(Expected_Result[i].plan == Actual_Result[i+1].plan);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i+1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i+1].members);
                    Assert.IsTrue(Expected_Result[i].pmpm == Actual_Result[i+1].pmpm);
                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Plan Details");
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", " Division by Plan-PMPM ", "Plan-PMPM", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }


        //plantypedesc pmpm
        [Test, Order(14), Category("Demographics-PlanType Tile Details-Members-Pmpm")]
        public void Demographics_PlanType_Pmpm_All()
        {
            try
            {
                _populationService.goToDemographicsSection();
                var Expected_Result = _populationService.Expected_Population_PlanType_Pmpm(_dashboard.StartDate(), _dashboard.EndDate());
                Console.WriteLine("Details of PlanType-pmpm from database:" + CommonMethods.ObjectToXml(Expected_Result));

                _populationService.Goto_Pmpm_section();
                var Actual_Result = _populationService.Map_PlanTypeTile_Pmpm(_populationService.PlanType_Tile_Pmpm());

                Console.WriteLine("PlanType details-PMPM  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength-1; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].planType, Actual_Result[i+1].planType, "Population", " Division by PlanType-PMPM ","PlanType-Pmpm", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i+1].spend, "Population", " Division by PlanType-PMPM ", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i+1].members, "Population", " Division by PlanType-PMPM ", "Members", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].pmpm, Actual_Result[i+1].pmpm, "Population", " Division by PlanType-PMPM ", "PMPM", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength-1; i++)
                {
                    Assert.IsTrue(Expected_Result[i].planType == Actual_Result[i+1].planType);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i+1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i+1].members);
                    Assert.IsTrue(Expected_Result[i].pmpm == Actual_Result[i+1].pmpm);
                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Plan Details");
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", " Division by PlanType-PMPM ", "Plan", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }


        #endregion

        /*[Test, Order(4), Category("Demographics-Location Tile Details-Members-All")]
                public void Demographics_Location_Pmpm_All()
                {
                    try
                    {
                        _populationService.goToDemographicsSection();
                        var Expected_Result = _populationService.Expected_Population_Location_Pmpm(_dashboard.StartDate(), _dashboard.EndDate());
                        Console.WriteLine("Details of PlanType-pmpm from database:" + CommonMethods.ObjectToXml(Expected_Result));

                        _populationService.Goto_Pmpm_section();
                        var Actual_Result = _populationService.Map_LocationTile_Pmpm(_populationService.location_Tile_Pmpm());

                        Console.WriteLine("PlanType details-PMPM  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                        var objectLength = Actual_Result.Count;
                        for (int i = 0; i < objectLength-1; i++)
                        {
                            _saveToCsv.SaveTestCase(Expected_Result[i].planType, Actual_Result[i+1].planType, "Population", "PlanType-Pmpm", "Expected value should be equal to actual value");
                            //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                            _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i+1].spend, "Population", "Spend", "Expected value should be equal to actual value");
                            // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                            _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i+1].members, "Population", "Members", "Expected value should be equal to actual value");
                            _saveToCsv.SaveTestCase(Expected_Result[i].pmpm, Actual_Result[i+1].pmpm, "Population", "PMPM", "Expected value should be equal to actual value");
                        }
                        for (int i = 0; i < objectLength-1; i++)
                        {
                            Assert.IsTrue(Expected_Result[i].planType == Actual_Result[i+1].planType);
                            Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i+1].spend);
                            Assert.IsTrue(Expected_Result[i].members == Actual_Result[i+1].members);
                            Assert.IsTrue(Expected_Result[i].pmpm == Actual_Result[i+1].pmpm);
                        }
                    }
                    catch (Exception ex)
                    {
                        Browser.ScreenShot("Plan Details");
                        if (!ex.Message.Contains("Expected:"))
                            _saveToCsv.SaveTestCase("Error", "Error", "Population", "Plan", "Exception occured:  Please verify manually");
                        Console.Out.WriteLine(ex);
                    }
                }*/

    }

}

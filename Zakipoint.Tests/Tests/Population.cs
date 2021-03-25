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
        //Browser.Dispose();        
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
                    _saveToCsv.SaveTestCase(Expected_Result[i].age, Actual_Result[i+1].age, "Population", "Age", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i+1].spend, "Population", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i+1].members, "Population", "Members", "Expected value should be equal to actual value");
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
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", "Age", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }

        [Test, Order(8), Category("Demographics-Age Tile Details-Members-PMPM")]
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
                Console.WriteLine("Details of Age from database:" + CommonMethods.ObjectToXml(Expected_Result));
                Console.WriteLine("Age details-PMPM  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength-1; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].age, Actual_Result[i+1].age, "Population", "Age", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i+1].spend, "Population", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i+1].members, "Population", "Members", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].pmpm, Actual_Result[i+1].pmpm, "Population", "PMPM", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength-1; i++)
                {
                    Assert.IsTrue(Expected_Result[i].age == Actual_Result[i+1].age);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i+1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i+1].members);
                    Assert.IsTrue(Expected_Result[i].pmpm == Actual_Result[i+1].pmpm);
                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Age Details");
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", "Age", "Exception occured:  Please verify manually");
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
                for (int i = 0; i < objectLength-1; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].gender, Actual_Result[i+1].gender, "Population", "Gender", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i+1].spend, "Population", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i+1].members, "Population", "Members", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength-1; i++)
                {
                    Assert.IsTrue(Expected_Result[i].gender == Actual_Result[i+1].gender);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i+1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i+1].members);
                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Gender Details");
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", "Gender", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }
        //gender tile pmpm
        [Test, Order(9), Category("Demographics-Gender Tile Details-Members-PMPM")]
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
                    _saveToCsv.SaveTestCase(Expected_Result[i].gender, Actual_Result[i+1].gender, "Population", "Gender-PMPM", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i+1].spend, "Population", "Spend-PMPM", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i+1].members, "Population", "Members-PMPM", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].pmpm, Actual_Result[i+1].pmpm, "Population", "PMPM", "Expected value should be equal to actual value");
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
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", "Gender-PMPM", "Exception occured:  Please verify manually");
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
                /*  _dashboard.ChooseAllMember();*/
                /*_populationService.Goto_Demographic_section();*/
              


                // var tableDetails = _commonFunction.GetTableValues(How.CssSelector, _populationpageObj.ageSvgBoxcssSelector, How.CssSelector, _populationpageObj.TopConditionDetailsByRowCssSelector);
                //  var Actual_Result = _populationService.Map_Object(tableDetails);
                Console.WriteLine("Relation details  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength-1; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].relation, Actual_Result[i+1].relation, "Population", "Relation", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i+1].spend, "Population", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i+1].members, "Population", "Members", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength-1; i++)
                {
                    Assert.IsTrue(Expected_Result[i].relation == Actual_Result[i+1].relation);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i+1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i+1].members);
                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Relation Details");
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", "Relation", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }

        //relationship pmpm

        [Test, Order(10), Category("Demographics-Relation Tile Details-Members-PmPm")]
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
                    _saveToCsv.SaveTestCase(Expected_Result[i].relation, Actual_Result[i+1].relation, "Population", "Relation", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i+1].spend, "Population", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i+1].members, "Population", "Members", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].pmpm, Actual_Result[i+1].pmpm, "Population", "PMPM", "Expected value should be equal to actual value");
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
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", "Relationship-Pmpm", "Exception occured:  Please verify manually");
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
                Console.WriteLine("Details of Gender from database:" + CommonMethods.ObjectToXml(Expected_Result));

               /* Console.WriteLine("Plan details  from UI:" + CommonMethods.ObjectToXml(Actual_Result));*/
                Console.WriteLine("Plan details  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength-1; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].plan, Actual_Result[i+1].plan, "Population", "Plan", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i+1].spend, "Population", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i+1].members, "Population", "Members", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength-1; i++)
                {
                    Assert.IsTrue(Expected_Result[i].plan == Actual_Result[i+1].plan);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i+1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i+1].members);
                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Plan Details");
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", "Plan", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }

        //plan pmpm
        [Test, Order(11), Category("Demographics-Plan Tile Details-Members-Pmpm")]
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
                    _saveToCsv.SaveTestCase(Expected_Result[i].plan, Actual_Result[i+1].plan, "Population", "Plan-Pmpm", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i+1].spend, "Population", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i+1].members, "Population", "Members", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].pmpm, Actual_Result[i+1].pmpm, "Population", "PMPM", "Expected value should be equal to actual value");
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
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", "Plan", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }

        //plan type

        [Test, Order(5), Category("Demographics-PlanType Tile Details-Members-All")]
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
                for (int i = 0; i < objectLength-1; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].planType, Actual_Result[i+1].planType, "Population", "PlanType", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i+1].spend, "Population", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i+1].members, "Population", "Members", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength-1; i++)
                {
                    Assert.IsTrue(Expected_Result[i].planType == Actual_Result[i+1].planType);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i+1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i+1].members);
                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Plan Details");
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", "PlanType", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }

        //plantypedesc pmpm
        [Test, Order(12), Category("Demographics-PlanType Tile Details-Members-All")]
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
                for (int i = 0; i < objectLength-1; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].division, Actual_Result[i+1].division, "Population", "division", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i+1].spend, "Population", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i+1].members, "Population", "Members", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength-1; i++)
                {
                    Assert.IsTrue(Expected_Result[i].division == Actual_Result[i+1].division);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i+1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i+1].members);
                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Age Details");
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", "Age", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }


        //location members

  
        [Test, Order(7), Category("Demographics-Division Tile Details-Members-All")]
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
                for (int i = 0; i < objectLength-1; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].mbrState, Actual_Result[i+1].mbrState, "Population", "Location", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i+1].spend, "Population", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i+1].members, "Population", "Members", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength-1; i++)
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
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", "Location", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }


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

        #endregion

    }



}

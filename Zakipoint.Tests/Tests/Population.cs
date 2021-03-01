using NUnit.Framework;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
    public override void Init()
    {
        Browser.Open(Browser.Config["url"]);
        _login.Login(JsonDataReader.Data["username"], JsonDataReader.Data["password"]);
        
            _setClient.SelectClient(JsonDataReader.Data["DefaultClient"]);
        CommonObject.DefaultClientSuffix = JsonDataReader.Data["DefaultClientSuffix"];
       /* _dashboard.DashboardPageLoad(); // assumed page load function is common for all pages*/
    }
    public override void Dispose()
    {
        _commonFunction.Logout();
        //Browser.Dispose();        
    }
        public string getCustomStartDate()
        {
            return JsonDataReader.Data["startDate"];
        }
        public string getCustomEndDate()
        {
            return JsonDataReader.Data["endDate"];
        }

        [Test, Order(20), Category("Age Tile Details")]
        public void AgeTileDetails()
        {
            try
            {
                //  var Expected_Result = _populationService.Expected_Population_Age(getCustomStartDate(), getCustomEndDate());
                //  Console.WriteLine("Details of Age from database:" + CommonMethods.ObjectToXml(Expected_Result));
                 //  _populationService.goToCustomDateRangeSetter(getCustomStartDate(),getCustomEndDate());
                       
                Browser.setDateRange(10);
                _populationService.Goto_Demographic_section();
                var Actual_Result = _populationService.Map_AgeTile(_populationService.Age_Tile());
                
                
                // var tableDetails = _commonFunction.GetTableValues(How.CssSelector, _populationpageObj.ageSvgBoxcssSelector, How.CssSelector, _populationpageObj.TopConditionDetailsByRowCssSelector);
              //  var Actual_Result = _populationService.Map_Object(tableDetails);
                Console.WriteLine("Age details  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
               /* for (int i = 0; i < objectLength; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].age, Actual_Result[i].age, "Population", "Age", "Expected value should be equal to actual value");
                  //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i].spend, "Population", "Spend", "Expected value should be equal to actual value");
                   // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i].members, "Population", "Members", "Expected value should be equal to actual value");
                }*/
               /* for (int i = 0; i < objectLength; i++)
                {
                    Assert.IsTrue(Expected_Result[i].age == Actual_Result[i+1].age);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i+1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i+1].members);
                }*/
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Age Details");
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", "Age", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }

        [Test, Order(21), Category("Age Tile Details-PMPM")]
        public void AgeTilePmpmDetails()
        {
            try
            {
                var Expected_Result = _populationService.Expected_Population_Age_Pmpm();
                Console.WriteLine("Details of Age from database:" + CommonMethods.ObjectToXml(Expected_Result));
                
                _populationService.Goto_Pmpm_section();
                var Actual_Result = _populationService.Map_AgeTile_Pmpm(_populationService.Age_Tile_Pmpm());

                Console.WriteLine("Age details-PMPM  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].age, Actual_Result[i].age, "Population", "Age", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i].spend, "Population", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i].members, "Population", "Members", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].pmpm, Actual_Result[i].pmpm, "Population", "PMPM", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength; i++)
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
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", "Age", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }

        // gender Tile 
        [Test, Order(22), Category("Gender Tile Details")]
        public void GenderTileDetails()
        {
            try
            {
                var Expected_Result = _populationService.Expected_Population_Gender();
                Console.WriteLine("Details of Gender from database:" + CommonMethods.ObjectToXml(Expected_Result));
                //  _dashboard.ChooseAllMember();
                _populationService.Goto_Demographic_section();
                var Actual_Result = _populationService.Map_GenderTile(_populationService.Gender_Tile());

                // var tableDetails = _commonFunction.GetTableValues(How.CssSelector, _populationpageObj.ageSvgBoxcssSelector, How.CssSelector, _populationpageObj.TopConditionDetailsByRowCssSelector);
                //  var Actual_Result = _populationService.Map_Object(tableDetails);
                Console.WriteLine("Gender details  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].gender, Actual_Result[i].gender, "Population", "Gender", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i].spend, "Population", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i].members, "Population", "Members", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength; i++)
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
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", "Gender", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }
        //gender tile pmpm
        [Test, Order(23), Category("Gender Tile Details-PMPM")]
        public void GenderTilePmpmDetails()
        {
            try
            {
                var Expected_Result = _populationService.Expected_Population_Gender_Pmpm();
                Console.WriteLine("Details of Gender-pmpm from database:" + CommonMethods.ObjectToXml(Expected_Result));

                _populationService.Goto_Pmpm_section();
                var Actual_Result = _populationService.Map_GenderTile_Pmpm(_populationService.Gender_Tile_Pmpm());

                Console.WriteLine("Gender details-PMPM  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].gender, Actual_Result[i].gender, "Population", "Age", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i].spend, "Population", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i].members, "Population", "Members", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].pmpm, Actual_Result[i].pmpm, "Population", "PMPM", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength; i++)
                {
                    Assert.IsTrue(Expected_Result[i].gender == Actual_Result[i + 1].gender);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i + 1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i + 1].members);
                    Assert.IsTrue(Expected_Result[i].pmpm == Actual_Result[i + 1].pmpm);
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

        //relationship members
        [Test, Order(20), Category("Relation Tile Details")]
        public void RelationTileDetails()
        {
            try
            {
                var Expected_Result = _populationService.Expected_Population_Relation();
                Console.WriteLine("Details of Relation from database:" + CommonMethods.ObjectToXml(Expected_Result));
                /*  _dashboard.ChooseAllMember();*/
                _populationService.Goto_Demographic_section();
                var Actual_Result = _populationService.Map_RelationTile(_populationService.Relation_Tile());


                // var tableDetails = _commonFunction.GetTableValues(How.CssSelector, _populationpageObj.ageSvgBoxcssSelector, How.CssSelector, _populationpageObj.TopConditionDetailsByRowCssSelector);
                //  var Actual_Result = _populationService.Map_Object(tableDetails);
                Console.WriteLine("Relation details  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].relation, Actual_Result[i].relation, "Population", "Age", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i].spend, "Population", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i].members, "Population", "Members", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength; i++)
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
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", "Relation", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }

        //relationship pmpm

        [Test, Order(24), Category("Relation Tile Details-PMPM")]
        public void RelationTilePmpmDetails()
        {
            try
            {
                var Expected_Result = _populationService.Expected_Population_Relation_Pmpm();
                Console.WriteLine("Details of Relation-pmpm from database:" + CommonMethods.ObjectToXml(Expected_Result));

                _populationService.Goto_Pmpm_section();
                var Actual_Result = _populationService.Map_RelationTile_Pmpm(_populationService.Relation_Tile_Pmpm());

                Console.WriteLine("Relation details-PMPM  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].relation, Actual_Result[i].relation, "Population", "Relation", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i].spend, "Population", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i].members, "Population", "Members", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].pmpm, Actual_Result[i].pmpm, "Population", "PMPM", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength; i++)
                {
                    Assert.IsTrue(Expected_Result[i].relation == Actual_Result[i + 1].relation);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i + 1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i + 1].members);
                    Assert.IsTrue(Expected_Result[i].pmpm == Actual_Result[i + 1].pmpm);
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

        //plan members
        [Test, Order(25), Category("Plan Tile Details")]
        public void PlanTileDetails()
        {
            try
            {
                var Expected_Result = _populationService.Expected_Population_Plan();
                Console.WriteLine("Details of Plan from database:" + CommonMethods.ObjectToXml(Expected_Result));
                /*  _dashboard.ChooseAllMember();*/
                _populationService.Goto_Demographic_section();
                var Actual_Result = _populationService.Map_PlanTile(_populationService.Plan_Tile());


                // var tableDetails = _commonFunction.GetTableValues(How.CssSelector, _populationpageObj.ageSvgBoxcssSelector, How.CssSelector, _populationpageObj.TopConditionDetailsByRowCssSelector);
                //  var Actual_Result = _populationService.Map_Object(tableDetails);
                Console.WriteLine("Plan details  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].plan, Actual_Result[i].plan, "Population", "Plan", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i].spend, "Population", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i].members, "Population", "Members", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength; i++)
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
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", "Plan", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }

        //plan pmpm
        [Test, Order(24), Category("Plan Tile Details-PMPM")]
        public void PlanTilePmpmDetails()
        {
            try
            {
                var Expected_Result = _populationService.Expected_Population_Plan_Pmpm();
                Console.WriteLine("Details of Plan-pmpm from database:" + CommonMethods.ObjectToXml(Expected_Result));

                _populationService.Goto_Pmpm_section();
                var Actual_Result = _populationService.Map_PlanTile_Pmpm(_populationService.Plan_Tile_Pmpm());

                Console.WriteLine("Plan details-PMPM  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].plan, Actual_Result[i].plan, "Population", "Plan", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i].spend, "Population", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i].members, "Population", "Members", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].pmpm, Actual_Result[i].pmpm, "Population", "PMPM", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength; i++)
                {
                    Assert.IsTrue(Expected_Result[i].plan == Actual_Result[i + 1].plan);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i + 1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i + 1].members);
                    Assert.IsTrue(Expected_Result[i].pmpm == Actual_Result[i + 1].pmpm);
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
        [Test, Order(25), Category("Division Tile Details")]
        public void divTileDetails()
        {
            try
            {
              //  var Expected_Result = _populationService.Expected_Population_Age();
             //   Console.WriteLine("Details of Age from database:" + CommonMethods.ObjectToXml(Expected_Result));
                /*  _dashboard.ChooseAllMember();*/
                _populationService.Goto_Demographic_section();
                Browser.FindElement(How.XPath, _populationpageObj.divisionViewAllBtnCssSelector).Click();
                var Actual_Result = _populationService.Map_DivisionTile(_populationService.division_Tile());


                // var tableDetails = _commonFunction.GetTableValues(How.CssSelector, _populationpageObj.ageSvgBoxcssSelector, How.CssSelector, _populationpageObj.TopConditionDetailsByRowCssSelector);
                //  var Actual_Result = _populationService.Map_Object(tableDetails);
                Console.WriteLine("Age details  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
              /*  for (int i = 0; i < objectLength; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].age, Actual_Result[i].age, "Population", "Age", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i].spend, "Population", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i].members, "Population", "Members", "Expected value should be equal to actual value");
                }*/
               /* for (int i = 0; i < objectLength; i++)
                {
                    Assert.IsTrue(Expected_Result[i].age == Actual_Result[i + 1].age);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i + 1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i + 1].members);
                }*/
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Age Details");
                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Population", "Age", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
        }


    }



}

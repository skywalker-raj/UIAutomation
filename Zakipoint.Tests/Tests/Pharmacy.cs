using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
/*using System;
using System.Collections.Generic;
using System.Text;*/
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
    class Pharmacy : AbstractBase
    {
        private readonly LoginPage _login;
        private readonly SetClientPage _setClient;
        private readonly CommonFunction _commonFunction;
        private readonly SaveToCSV _saveToCsv;
        private readonly PopulationPage _populationService;
        private readonly populationPageObjects _populationpageObj;
        private readonly DashboardPage _dashboard;
        private readonly PharmacyPage _pharmacyService;

        public Pharmacy()
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
            _pharmacyService = new PharmacyPage();
        }
        public override void Init()
        {
            Browser.Open(Browser.Config["url"]);
            _login.Login(JsonDataReader.Data["username"], JsonDataReader.Data["password"]);

            _setClient.SelectClient(JsonDataReader.Data["DefaultClient"]);
            CommonObject.DefaultClientSuffix = JsonDataReader.Data["DefaultClientSuffix"];
            _populationService.goToCustomDateRangeSetter();
            Thread.Sleep(3000);
            _pharmacyService.GotoPharmacySection();
            //  Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='z5popmlc001']")));
            Thread.Sleep(3000);
        }
        public override void Dispose()
        {
            _commonFunction.Logout();
            Browser.Dispose();        
        }



        #region TestMethods
        [Test, Order(1), Category("Pharmacy-Demographics-Age Tile Details-Members-All")]
        public void Demographics_Age_Members_All()
        {
            try
            {
                _populationService.goToDemographicsSection();
                Thread.Sleep(3000);
                //   Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='z5popmlc001']")));
                var Actual_Result = _pharmacyService.Map_AgeTile(_pharmacyService.Pharmacy_Age_Tile());
                var Expected_Result = _populationService.Expected_Population_Age(_dashboard.StartDate(), _dashboard.EndDate());

                Console.WriteLine("Age details  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength - 1; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].age, Actual_Result[i + 1].age, "Pharmacy", "Division by Age", "Age", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i + 1].spend, "Pharmacy", "Division by Age", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i + 1].members, "Pharmacy", "Division by Age", "Members", "Expected value should be equal to actual value");
                }
                for (int i = 0; i < objectLength - 1; i++)
                {
                    Assert.IsTrue(Expected_Result[i].age == Actual_Result[i + 1].age);
                    Assert.IsTrue(Expected_Result[i].spend == Actual_Result[i + 1].spend);
                    Assert.IsTrue(Expected_Result[i].members == Actual_Result[i + 1].members);
                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Age Details");

                if (!ex.Message.Contains("Expected:"))
                    _saveToCsv.SaveTestCase("Error", "Error", "Pharmacy", "Division by Age", "Age", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }

        }

        [Test, Order(2), Category("Pharmacy-Demographics-Gender Tile Details-Members-All")]
        public void Demographics_Gender_Members_All()
        {
            try
            {
                _populationService.goToDemographicsSection();
                Thread.Sleep(3000);
                //   Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='z5popmlc001']")));
                var Actual_Result = _pharmacyService.Map_GenderTile(_pharmacyService.Pharmacy_Gender_Tile());
                var Expected_Result = _populationService.Expected_Population_Gender(_dashboard.StartDate(), _dashboard.EndDate());

                Console.WriteLine("Gender details  from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count;
                for (int i = 0; i < objectLength - 1; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].gender, Actual_Result[i + 1].gender, "Pharmacy", " Division by Gender ", "Gender", "Expected value should be equal to actual value");
                    //  _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Spend), Format("{0:0.##}", Actual_Result[i+1].P_Spend), "Dashboard", "% Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].spend, Actual_Result[i + 1].spend, "Pharmacy", " Division by Gender ", "Spend", "Expected value should be equal to actual value");
                    // _saveToCsv.SaveTestCase(Format("{0:0.##}", Expected_Result[i].P_Change), Format("{0:0.##}", Actual_Result[i+1].P_Change), "Dashboard", "% Change Spend(all)", "Expected value should be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].members, Actual_Result[i + 1].members, "Pharmacy", " Division by Gender ", "Members", "Expected value should be equal to actual value");
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
                    _saveToCsv.SaveTestCase("Error", "Error", "Pharmacy", " Division by Gender ", "Gender", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }

        }
        #endregion
    }
}




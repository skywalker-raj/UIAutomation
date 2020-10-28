using NUnit.Framework;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Linq;
using Zakipoint.Framework.Driver;
using Zakipoint.Tests.Common;
using Zakipoint.UIAutomation.Common;
using Zakipoint.UIAutomation.Model;
using Zakipoint.UIAutomation.PageObjects;
using Zakipoint.UIAutomation.PageServices;

namespace Zakipoint.Tests.Tests
{
    public class Dashboard
    {
        #region Private Methods

        private readonly DashboardPageObjects _dashboardPage; 
        private readonly DashboardPage _dashboard;
        private readonly LoginPage _login;
        private readonly SetClientPage _setClient;
        private readonly CommonFunction _commonFunction;
        private readonly SaveToCSV _saveToCsv;

        #endregion

        #region Constructor

        public Dashboard()
        {
            _dashboard = new DashboardPage();
            _login = new LoginPage();
            _setClient = new SetClientPage();
            _commonFunction = new CommonFunction();
            _dashboardPage = new DashboardPageObjects();
            _saveToCsv = new SaveToCSV();
        }

        #endregion

        #region Base Methods

        [OneTimeSetUp]
        public void Init()
        {
            Browser.Open(Browser.Config["url"]);
            _login.Login(JsonDataReader.Data["username"], JsonDataReader.Data["password"]);
            _setClient.SelectClient(JsonDataReader.Data["DefaultClient"]);
            CommonObject.DefaultClientSuffix = JsonDataReader.Data["DefaultClientSuffix"];
            _dashboard.DashboardPageLoad();
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            Browser.Dispose();
        }

        #endregion

        #region TestMethods
        
        [Test, Category("Dashboard Page Verification")]
        public void Verify_Dashboard_Page()
        {
            var expectedMenuList = JsonDataReader.Data["Menu"].Split(';').ToList();
            var expectedQuickLinksList = JsonDataReader.Data["QuickLinks"].Split(';').ToList();
            var expectedConditionSpendTableHeaderList = JsonDataReader.Data["ConditionBySpend"].Split(';').ToList();
            try
            {
                Assert.AreEqual(_dashboard.GetMenuList(), expectedMenuList);
                Assert.AreEqual(_commonFunction.GetNavBarQuickLinks(), expectedQuickLinksList);
                var groupDetailsList = _dashboard.GetGroupID();
                //_dashboard.ClickDownloadReport();
                //CommonMethods.CheckFileDownloaded(_dashboard.GetDownloadReportName());
                Assert.AreEqual(_dashboard.GetTableHeaderList(How.CssSelector, _dashboardPage.TopConditionTableHeaderCssSelector), expectedConditionSpendTableHeaderList);
                var tableDetails = _commonFunction.GetTableValues(How.CssSelector, _dashboardPage.TopConditionRowCssSelector);
            }
            catch (Exception e)
            {
                Browser.ScreenShot("Dashboard_Verification_Shot");
                Console.Out.WriteLine(e);
            }
        }
        [Test, Category("Top Condition By Total Spend")]
        public void Top_Condition_By_Total_Spend()
        {
            try
            {
                var Expected_Result = _dashboard.Expected_Top_Condition_By_Total_Spend();
                _dashboard.ChooseAllMember();
                var tableDetails = _commonFunction.GetTableValues(How.CssSelector, _dashboardPage.TopConditionRowCssSelector);
                var Actual_Result = _dashboard.Map_Object(tableDetails);
                for (int i = 0; i < 9; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].Conditions, Actual_Result[i].Conditions, "Dashboard", "Conditions", "Expected value shold be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].P_Spend, Actual_Result[i].P_Spend, "Dashboard", "% Spend", "Expected value shold be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].Spend, Actual_Result[i].Spend, "Dashboard", "Spend", "Expected value shold be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].P_Change, Actual_Result[i].P_Change, "Dashboard", "% Change (Spend)", "Expected value shold be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].Members, Actual_Result[i].Members, "Dashboard", "Members", "Expected value shold be equal to actual value");
                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Top_Condition_By_Total_Spend");
                Console.Out.WriteLine(ex);
            }
        }
        [Test, Category("%PMPM change Form Last year(Medical,pharmacy) for active member")]
        public void Active_PMPM_Change_Percentages()
        {
            try
            {
                decimal Expected_Medical_PMPM_Change = _dashboard.Expected_PMPM_Change("medical", "active");
                decimal Expected_Pharmacy_PMPM_Change = _dashboard.Expected_PMPM_Change("pharmacy", "active");
                _dashboard.ChooseActiveMember();
                _dashboard.Click_Spend_PMPM_Link(false); // false for clicking pmpm tab
                decimal Actual_Medical_PMPM_Change = _dashboard.PMPM_Change(true); //true for medical pmpm tile
                decimal Actual_Pharmacy_PMPM_Change = _dashboard.PMPM_Change(false); //false for pharmacy pmpm tile
                _saveToCsv.SaveTestCase(Expected_Medical_PMPM_Change.ToString(), Actual_Medical_PMPM_Change.ToString(), "Dashboard", "Active Medical PMPM Change Percentages", "Expected PMPM  change shold be equal to actual PMPM change");
                _saveToCsv.SaveTestCase(Expected_Pharmacy_PMPM_Change.ToString(), Actual_Pharmacy_PMPM_Change.ToString(), "Dashboard", "Active Pharmacy PMPM Change Percentages", "Expected PMPM change shold be equal to actual PMPM change");
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Active PMPM Change Percentages");
                _saveToCsv.SaveTestCase("Error", "Error", "Dashboard", "Active_PMPM_Change_Percentages", _commonFunction.RemoveUnicode(ex.Message));
                Console.Out.WriteLine(ex);
            }
        }
        [Test, Category("%PMPM Change Form Last year(Medical,pharmacy)")]
        public void PMPM_Change_Percenatges()
        {
            try
            {
                decimal Expected_Medical_PMPM_Change = _dashboard.Expected_PMPM_Change("medical", "all");
                decimal Expected_Pharmacy_PMPM_Change = _dashboard.Expected_PMPM_Change("pharmacy", "all");
                _dashboard.ChooseAllMember();
                _dashboard.Click_Spend_PMPM_Link(false);
                decimal Actual_Medical_PMPM_Change = _dashboard.PMPM_Change(true);
                decimal Actual_Pharmacy_PMPM_Change = _dashboard.PMPM_Change(false);
                _saveToCsv.SaveTestCase(String.Format("{0:0.##}", Expected_Medical_PMPM_Change), string.Format("{0:0.##}", Actual_Medical_PMPM_Change), "Dashboard", "Medical PMPM Change Percentages", "Expected PMPM  change shold be equal to actual PMPM change");
                _saveToCsv.SaveTestCase(String.Format("{0:0.##}", Expected_Pharmacy_PMPM_Change), string.Format("{0:0.##}", Actual_Pharmacy_PMPM_Change), "Dashboard", "Pharmacy PMPM Change Percentages", "Expected PMPM change shold be equal to actual PMPM change");
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("PMPM Change Percentages");
                _saveToCsv.SaveTestCase("Error", "Error", "Dashboard", "PMPM Change Percentages", _commonFunction.RemoveUnicode(ex.Message));
                Console.Out.WriteLine(ex);
            }
        }
        [Test, Category("Active PMPM")]
        public void Active_PMPM()
        {
            try
            {
                decimal Expected_Medical_PMPM = Math.Round(Convert.ToDecimal(_dashboard.Expected_PMPM("medical", "Active", 1)), 0);
                decimal Expected_Pharmacy_PMPM = Math.Round(Convert.ToDecimal(_dashboard.Expected_PMPM("pharmacy", "Active", 1)), 0);
                _dashboard.ChooseActiveMember();
                _dashboard.Click_Spend_PMPM_Link(false);
                decimal Actual_Medical_PMPM = _dashboard.Spend_PMPM(true);
                decimal Actual_Pharmacy_PMPM = _dashboard.Spend_PMPM(false);
                _saveToCsv.SaveTestCase(Expected_Medical_PMPM.ToString(), Actual_Medical_PMPM.ToString(), "Dashboard", "Active medical PMPM", "Expected PMPM shold be equal to actual PMPM");
                _saveToCsv.SaveTestCase(Expected_Pharmacy_PMPM.ToString(), Actual_Pharmacy_PMPM.ToString(), "Dashboard", "Active pharmacy PMPM", "Expected PMPM shold be equal to actual PMPM");
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Active  PMPM");
                _saveToCsv.SaveTestCase("Error", "Error", "Dashboard", "Active_PMPM", _commonFunction.RemoveUnicode(ex.Message));
                Console.Out.WriteLine(ex);
            }
        }
        [Test, Category("Active Medical Pharmacy Spend Change Percentages")]
        public void Active_Medical_Pharmacy_Spend_Change_Percentages()
        {
            try
            {
                var All_Spend_Curreny_Year = _dashboard.Expected_Total_Medical_Pharmacy_Spend("active", 1);
                var All_Spend_Last_Year = _dashboard.Expected_Total_Medical_Pharmacy_Spend("active", 2);
                decimal Expected_Medical_Change_spend = _dashboard.Percentages(All_Spend_Curreny_Year[0], All_Spend_Last_Year[0]);
                decimal Expected_Pharmacy_Change_spend = _dashboard.Percentages(All_Spend_Curreny_Year[1], All_Spend_Last_Year[1]);
                _dashboard.ChooseActiveMember();
                decimal Actula_Medical_Change_Spend = _dashboard.Spend_Change(true);  //true for Medical 
                decimal Actual_Pharmacy_Change_Spend = _dashboard.Spend_Change(false); //false for Pharmacy
                _saveToCsv.SaveTestCase(String.Format("{0:0.##}", Expected_Medical_Change_spend), String.Format("{0:0.##}", Actula_Medical_Change_Spend), "Dashboard", " Active Medical Spend Change ", "Expected medical change spend should be equal to actual medical change spend");
                _saveToCsv.SaveTestCase(String.Format("{0:0.##}", Expected_Pharmacy_Change_spend), String.Format("{0:0.##}", Actual_Pharmacy_Change_Spend), "Dashboard", "Active Pharmacy Spend Change", "Expected pharmacy change spend should be equal to actual pharmacy change spend");
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Active Medical Pharmacy Spend Change Percentages");
                _saveToCsv.SaveTestCase("Error", "Error", "Dashboard", "Active_Medical_Pharmacy_Spend_Change_Percentages", _commonFunction.RemoveUnicode(ex.Message));
                Console.Out.WriteLine(ex);
            }
        }
        [Test, Category("Spend change percentages")]
        public void Medical_Pharmacy_Spend_Change_Percentages()
        {
            try
            {
                var All_Spend_Curreny_Year = _dashboard.Expected_Total_Medical_Pharmacy_Spend("all", 1);
                var All_Spend_Last_Year = _dashboard.Expected_Total_Medical_Pharmacy_Spend("all", 2);
                decimal Expected_Medical_Change_spend = _dashboard.Percentages(All_Spend_Curreny_Year[0], All_Spend_Last_Year[0]);
                decimal Expected_Pharmacy_Change_spend = _dashboard.Percentages(All_Spend_Curreny_Year[1], All_Spend_Last_Year[1]);
                _dashboard.ChooseAllMember();
                decimal Actula_Medical_Change_Spend = _dashboard.Spend_Change(true); //true for Medical
                decimal Actual_Pharmacy_Change_Spend = _dashboard.Spend_Change(false); //false for Pharmacy
                _saveToCsv.SaveTestCase(Expected_Medical_Change_spend.ToString(), Actula_Medical_Change_Spend.ToString(), "Dashboard", " Medical Spend Change", "Expected medical change spend should be equal to actual medical change spend");
                _saveToCsv.SaveTestCase(Expected_Pharmacy_Change_spend.ToString(), Actual_Pharmacy_Change_Spend.ToString(), "Dashboard", "Pharmacy Spend Change", "Expected pharmacy change spend should be equal to actual pharmacy change spend");
            }
            catch (Exception ex)
            {

                Browser.ScreenShot("Medical Pharmacy Spend Change Percentages");
                _saveToCsv.SaveTestCase("Error", "Error", "Dashboard", "Medical_Pharmacy_Spend_Change_Percentage", _commonFunction.RemoveUnicode(ex.Message));
                Console.Out.WriteLine(ex);
            }

        }
        [Test, Category("Total Active Spend , Total Active Medical Spend and Total Active Pharmacy Spend")]
        public void Total_Active_Medical_Pharmacy_Spend()
        {
            try
            {
                var Active_Spend = _dashboard.Expected_Total_Medical_Pharmacy_Spend("active", 1);
                string Expected_Active_Total_Medical_Spend = "$" + Active_Spend[0].ToString() + "K";
                string Expected_Active_Total_Pharmacy_Spend = "$" + Active_Spend[1].ToString() + "K";
                string Expected_Active_Total_Spend = "$" + (Active_Spend[0] + Active_Spend[1]).ToString() + "K";
                _dashboard.ChooseActiveMember();
                string Actual_Active_Total_Medical_Spend = _dashboard.TotalSpend(true);
                string Actual_Active_Total_Pharmacy_Spend = _dashboard.TotalSpend(false);
                string Actual_Active_Total_Spend = _dashboard.TotalSpend();
                _saveToCsv.SaveTestCase(Expected_Active_Total_Medical_Spend, Actual_Active_Total_Medical_Spend, "Dashboard", "Total Active Medical Spend", "Expected total active medical spend should be equal to actual total active medical spend ");
                _saveToCsv.SaveTestCase(Expected_Active_Total_Pharmacy_Spend, Actual_Active_Total_Pharmacy_Spend, "Dashboard", "Total Active Pharmacy Spend", "Expected total active pharmacy spend should be equal to actual total active pharmacy spend ");
                _saveToCsv.SaveTestCase(Expected_Active_Total_Spend, Actual_Active_Total_Spend, "Dashboard", "Total Active Spend", "Expected total active spend should be equal to actual total active spend ");
                //Assert.AreEqual(Expected_Active_Total_Medical_Spend, Actual_Active_Total_Medical_Spend, "Expected total active medical spend should be equal to actual active total medical spend");
                //Assert.AreEqual(Expected_Active_Total_Pharmacy_Spend, Actual_Active_Total_Pharmacy_Spend, "Expected total active pharmacy spend should be equal to actual total active pharmacy spend");
                //Assert.AreEqual(Expected_Active_Total_Spend, Actual_Active_Total_Spend, "Expected total active spend should be equal to actual total active  spend ");
            }
            catch (Exception e)
            {
                Browser.ScreenShot("Total Active Spend , Medical Active Spend,Pharmacy  ActiveSpend");
                _saveToCsv.SaveTestCase("Error", "Error", "Dashboard", "Total_Active_Medical_Pharmacy_Spend", _commonFunction.RemoveUnicode(e.Message));
                Console.Out.WriteLine(e);
            }

        }
        [Test, Category("Total Spend , Medical Spend,Pharmacy Spend")]
        public void Total_Medical_Pharmacy_Spend()
        {
            try
            {
                var Total_Spend = _dashboard.Expected_Total_Medical_Pharmacy_Spend("all", 1);
                string Expected_Total_Medical_Spend = "$" + Total_Spend[0].ToString() + "K";
                string Expected_Total_Pharmacy_Spend = "$" + Total_Spend[1].ToString() + "K";
                string Expected_Total_Spend = "$" + (Total_Spend[0] + Total_Spend[1]).ToString() + "K";
                _dashboard.ChooseAllMember();
                string Actual_Total_Medical_Spend = _dashboard.TotalSpend(true);
                string Actual_Total_Pharmacy_Spend = _dashboard.TotalSpend(false);
                string Actual_Total_Spend = _dashboard.TotalSpend();
                _saveToCsv.SaveTestCase(Expected_Total_Medical_Spend, Actual_Total_Medical_Spend, "Dashboard", "Total Medical Spend", "Expected total medical spend should be equal to total actual medical spend ");
                _saveToCsv.SaveTestCase(Expected_Total_Pharmacy_Spend, Actual_Total_Pharmacy_Spend, "Dashboard", "Total Pharmacy Spend", "Expected total pharmacy spend should be equal to actual total pharmacy spend ");
                _saveToCsv.SaveTestCase(Expected_Total_Spend, Actual_Total_Spend, "Dashboard", "Total  Spend", "Expected total spend should be equal to actual total spend ");
                //    Assert.AreEqual(Expected_Total_Medical_Spend, Actual_Total_Medical_Spend, "Expected total medical spend should be equal to actual total medical spend ");
                //    Assert.AreEqual(Expected_Total_Pharmacy_Spend, Actual_Total_Pharmacy_Spend, "Expected total pharmacy spend should be equal to actual total pharmacy spend ");
                //    Assert.AreEqual(Expected_Total_Spend, Actual_Total_Spend, "Expected total spend should be equal to actual total spend ");
            }
            catch (Exception e)
            {
                Browser.ScreenShot("Medical Spend Pharmacy Spend");
                _saveToCsv.SaveTestCase("Error", "Dashboard", "Medical Pharmacy Spend", "Medical Pharmacy Spend", _commonFunction.RemoveUnicode(e.Message));
                Console.Out.WriteLine(e);
            }
        }
        [Test, Category("Total Member and Total Employee")]
        public void Total_Member_Total_Employee()
        {
            try
            {
                string ExpectedTotalMember = _dashboard.ExpectedTotalMember();
                string ExpectedTotalEmployee = _dashboard.ExpectedTotalEmployee();
                _dashboard.ChooseAllMember();
                string ActualTotalMember = _dashboard.TotalMember(false);
                string ActualTotalEmployee = _dashboard.TotalEmployee(false);
                _saveToCsv.SaveTestCase(ExpectedTotalMember, ActualTotalMember, "Dashboard", "Toatal Member", "Acutal toatl member shoud be equal to expected total member");
                _saveToCsv.SaveTestCase(ExpectedTotalEmployee, ActualTotalEmployee, "DashBoard", "Total employee", "Actual toal employee shoud be equal to expected total employee");
                //    Assert.AreEqual(ExpectedTotalMember, ActualTotalMember,  "Acutal toatl member shoud be equal to expected total member");
                //    Assert.AreEqual(ExpectedTotalEmployee, ActualTotalEmployee,  "Actual toal employee shoud be equal to expected total employee");
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Total Member and Total Employee");
                _saveToCsv.SaveTestCase(" ", " ", "Dashboard", "Total Member and Total Employee", _commonFunction.RemoveUnicode(ex.Message));
                Console.Out.WriteLine(ex);
            }
        }
        [Test, Category("Active Member and Active Employee")]
        public void Active_Member_Active_Employee()
        {
            try
            {
                string ExpectedActiveMember = _dashboard.ExpectedTotalActiveMember();
                string ExpectedActiveEmployee = _dashboard.ExpectedTotalActiveEmployee();
                _dashboard.ChooseActiveMember();
                string ActualActiveMember = _dashboard.TotalMember(true);
                string ActualActiveEmployee = _dashboard.TotalEmployee(true);
                _saveToCsv.SaveTestCase(ExpectedActiveMember, ActualActiveMember, "DashBoard", "Active member", "Acutal active member shoud be equal to expected active member");
                _saveToCsv.SaveTestCase(ExpectedActiveEmployee, ActualActiveEmployee, "DashBoard", "Active employee", "Acutal active employee shoud be equal to expected active employee");
                //Assert.AreEqual(ExpectedActiveMember, ActualActiveMember, "Actual active member shoud be equal to expected active member");
                //Assert.AreEqual(ExpectedActiveEmployee, ActualActiveEmployee, "Actual active employee shoud be equal to expected active employee");
            }
            catch (Exception e)
            {
                Browser.ScreenShot("Active Member and Active Employee");
                _saveToCsv.SaveTestCase(" ", " ", "Dashboard", "Active Member and Active Employee", _commonFunction.RemoveUnicode(e.Message));
                Console.Out.WriteLine(e);
            }
        }
        #endregion
    }
}

using NUnit.Framework;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Linq;
using Zakipoint.Framework.Common;
using Zakipoint.Framework.Driver;
using Zakipoint.Tests.Common;
using Zakipoint.UIAutomation.Common;
using Zakipoint.UIAutomation.Model;
using Zakipoint.UIAutomation.PageObjects;
using Zakipoint.UIAutomation.PageServices;
using Zakipoint.Tests.Base;
namespace Zakipoint.Tests.Tests
{
    public class Dashboard : AbstractBase
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

        //[SetUp]
        public override void Init()
        {
            Browser.Open(Browser.Config["url"]);
            _login.Login(JsonDataReader.Data["username"], JsonDataReader.Data["password"]);
            _setClient.SelectClient(JsonDataReader.Data["DefaultClient"]);
            CommonObject.DefaultClientSuffix = JsonDataReader.Data["DefaultClientSuffix"];
            _dashboard.DashboardPageLoad();
        }

        [TearDown]
        public override void Dispose()
        {
            _commonFunction.Logout();
            //Browser.Dispose();
        }

        #endregion


        #region TestMethods

        //[Test, Category("Dashboard Page Verification")]
        public void Verify_Dashboard_Page()
        {
            var expectedMenuList = JsonDataReader.Data["Menu"].Split(';').ToList();
            var expectedQuickLinksList = JsonDataReader.Data["QuickLinks"].Split(';').ToList();
            var expectedConditionSpendTableHeaderList = JsonDataReader.Data["ConditionBySpend"].Split(';').ToList();
            try
            {
                //Assert.AreEqual(_dashboard.GetMenuList(), expectedMenuList);
                //Assert.AreEqual(_commonFunction.GetNavBarQuickLinks(), expectedQuickLinksList);
                var groupDetailsList = _dashboard.GetGroupID();
                //_dashboard.ClickDownloadReport();
                //CommonMethods.CheckFileDownloaded(_dashboard.GetDownloadReportName());
                //Assert.AreEqual(_dashboard.GetTableHeaderList(How.CssSelector, _dashboardPage.TopConditionTableHeaderCssSelector), expectedConditionSpendTableHeaderList);
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
                Console.WriteLine("Top Condition By Total Spend(all) from database:" + CommonMethods.ObjectToXml(Expected_Result));
                _dashboard.ChooseAllMember();
                var tableDetails = _commonFunction.GetTableValues(How.CssSelector, _dashboardPage.TopConditionRowCssSelector);
                var Actual_Result = _dashboard.Map_Object(tableDetails);
                Console.WriteLine("Top Condition(all) By Total Spend from UI:" + CommonMethods.ObjectToXml(Actual_Result));
                var objectLength = Actual_Result.Count - 1;
                for (int i = 0; i < objectLength; i++)
                {
                    _saveToCsv.SaveTestCase(Expected_Result[i].Conditions, Actual_Result[i].Conditions, "Dashboard", "Conditions(all)", "Expected value shold be equal to actual value");
                    _saveToCsv.SaveTestCase(String.Format("{0:0.##}",Expected_Result[i].P_Spend), String.Format("{0:0.##}",Actual_Result[i].P_Spend), "Dashboard", "% Spend(all)", "Expected value shold be equal to actual value");
                     _saveToCsv.SaveTestCase(Expected_Result[i].Spend, Actual_Result[i].Spend, "Dashboard", "Spend", "Expected value shold be equal to actual value");
                    _saveToCsv.SaveTestCase(String.Format("{0:0.##}",Expected_Result[i].P_Change), String.Format("{0:0.##}",Actual_Result[i].P_Change), "Dashboard", "% Change Spend(all)", "Expected value shold be equal to actual value");
                    _saveToCsv.SaveTestCase(Expected_Result[i].Members, Actual_Result[i].Members, "Dashboard", "Members(all)", "Expected value shold be equal to actual value");
                   
                }
                  for (int i = 0; i < objectLength; i++)
                   {
                    Assert.IsTrue(String.Format("{0:0.##}", Expected_Result[i].P_Change) == String.Format("{0:0.##}", Actual_Result[i].P_Change));
                    Assert.IsTrue(Expected_Result[i].Spend == Actual_Result[i].Spend);
                    Assert.IsTrue(String.Format("{0:0.##}", Expected_Result[i].P_Spend) == String.Format("{0:0.##}", Actual_Result[i].P_Spend));
                    Assert.IsTrue(Expected_Result[i].Conditions == Actual_Result[i].Conditions);
                    Assert.IsTrue(Expected_Result[i].Members == Actual_Result[i].Members);
                   }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Top_Condition_By_Total_Spend_Shot");
                Console.Out.WriteLine(ex);
            }
        }
        [Test, Category("%PMPM change Form Last year(Medical,pharmacy) for active member")]
        public void Active_PMPM_Change_Percentages()
        {
            try
            {
                decimal Expected_Medical_PMPM_Change = _dashboard.Expected_PMPM_Change("medical", "active");
                Console.WriteLine("Medical PMPM Change(active) % from database: " + Expected_Medical_PMPM_Change.ToString());
                decimal Expected_Pharmacy_PMPM_Change = _dashboard.Expected_PMPM_Change("pharmacy", "active");
                Console.WriteLine("Pharmacy PMPM Change(active) % from database: " + Expected_Pharmacy_PMPM_Change.ToString());
                _dashboard.ChooseActiveMember();
                _dashboard.Click_Spend_PMPM_Link(false); // false for clicking pmpm tab
                decimal Actual_Medical_PMPM_Change = _dashboard.PMPM_Change(true); //true for medical pmpm tile
                Console.WriteLine("Medical PMPM Change(active) % from UI: " + Actual_Medical_PMPM_Change.ToString());
                decimal Actual_Pharmacy_PMPM_Change = _dashboard.PMPM_Change(false); //false for pharmacy pmpm tile
                Console.WriteLine("Pharmacy PMPM Change(active) % from UI: " + Actual_Pharmacy_PMPM_Change.ToString());
                _saveToCsv.SaveTestCase(Expected_Medical_PMPM_Change.ToString(), Actual_Medical_PMPM_Change.ToString(), "Dashboard", "Medical PMPM Change(active) Percentages", "Expected PMPM  change shold be equal to actual PMPM change");
                _saveToCsv.SaveTestCase(Expected_Pharmacy_PMPM_Change.ToString(), Actual_Pharmacy_PMPM_Change.ToString(), "Dashboard", "Pharmacy PMPM Change(active) Percentages", "Expected PMPM change shold be equal to actual PMPM change");
                Assert.IsTrue(Actual_Medical_PMPM_Change == Expected_Medical_PMPM_Change);
                Assert.IsTrue(Actual_Pharmacy_PMPM_Change == Expected_Pharmacy_PMPM_Change);
                
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Active_PMPM_Change_Percentages_Shot");
               // _saveToCsv.SaveTestCase("Error", "Error", "Dashboard", "Active_PMPM_Change_Percentages", _commonFunction.RemoveUnicode(ex.Message));
                Console.Out.WriteLine(ex);
            }
        }
        [Test, Category("%PMPM Change Form Last year(Medical,pharmacy)")]
        public void PMPM_Change_Percenatges()
        {
            try
            {
                decimal Expected_Medical_PMPM_Change = _dashboard.Expected_PMPM_Change("medical", "all");
                Console.WriteLine("Medical PMPM Change Percentages(All) from database:" + Expected_Medical_PMPM_Change.ToString());
                decimal Expected_Pharmacy_PMPM_Change = _dashboard.Expected_PMPM_Change("pharmacy", "all");
                Console.WriteLine("Pharmacy PMPM Chnage Percentages(All) from database :" + Expected_Pharmacy_PMPM_Change.ToString());              
                _dashboard.ChooseAllMember();
                _dashboard.Click_Spend_PMPM_Link(false);
                decimal Actual_Medical_PMPM_Change = _dashboard.PMPM_Change(true);
                Console.WriteLine("Medical PMPM Change Percentages(All) from UI:" + Actual_Medical_PMPM_Change.ToString());
                decimal Actual_Pharmacy_PMPM_Change = _dashboard.PMPM_Change(false);
                Console.WriteLine("Pharmacy PMPM Change Percentages(All) from UI: " + Actual_Pharmacy_PMPM_Change.ToString());
                _saveToCsv.SaveTestCase(String.Format("{0:0.##}", Expected_Medical_PMPM_Change), string.Format("{0:0.##}", Actual_Medical_PMPM_Change), "Dashboard", "Medical PMPM Change(all) Percentages", "Expected PMPM  change shold be equal to actual PMPM change");
                _saveToCsv.SaveTestCase(String.Format("{0:0.##}", Expected_Pharmacy_PMPM_Change), string.Format("{0:0.##}", Actual_Pharmacy_PMPM_Change), "Dashboard", "Pharmacy PMPM Change(all) Percentages", "Expected PMPM change shold be equal to actual PMPM change");
                Assert.IsTrue(String.Format("{0:0.##}", Expected_Medical_PMPM_Change)== string.Format("{0:0.##}", Actual_Medical_PMPM_Change));
                Assert.IsTrue( String.Format("{0:0.##}", Expected_Pharmacy_PMPM_Change) == string.Format("{0:0.##}", Actual_Pharmacy_PMPM_Change));
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("PMPM_Change_Percenatges_Shot");
               // _saveToCsv.SaveTestCase("Error", "Error", "Dashboard", "PMPM_Change_Percenatges", _commonFunction.RemoveUnicode(ex.Message));
                Console.Out.WriteLine(ex);
            }
        }
        [Test, Category("Active PMPM")]
        public void Active_PMPM()
        {
            try
            {
                decimal Expected_Medical_PMPM = Math.Round(Convert.ToDecimal(_dashboard.Expected_PMPM("medical", "Active", 1)), 0);
                Console.WriteLine("Mediacal PMPM(active) from database: " + Expected_Medical_PMPM.ToString());
                decimal Expected_Pharmacy_PMPM = Math.Round(Convert.ToDecimal(_dashboard.Expected_PMPM("pharmacy", "Active", 1)), 0);
                Console.WriteLine("Pharmacy PMPM(active) from database: " + Expected_Pharmacy_PMPM.ToString());
                _dashboard.ChooseActiveMember();
                _dashboard.Click_Spend_PMPM_Link(false);
                decimal Actual_Medical_PMPM = _dashboard.Spend_PMPM(true);
                Console.WriteLine("Mediacal PMPM(active) from UI: " + Actual_Medical_PMPM.ToString());
                decimal Actual_Pharmacy_PMPM = _dashboard.Spend_PMPM(false);
                Console.WriteLine("Mediacal PMPM(active) from UI: " + Actual_Pharmacy_PMPM.ToString());
                _saveToCsv.SaveTestCase(Expected_Medical_PMPM.ToString(), Actual_Medical_PMPM.ToString(), "Dashboard", "Medical PMPM(active)", "Expected PMPM shold be equal to actual PMPM");
                _saveToCsv.SaveTestCase(Expected_Pharmacy_PMPM.ToString(), Actual_Pharmacy_PMPM.ToString(), "Dashboard", "Pharmacy PMPM(active)", "Expected PMPM shold be equal to actual PMPM");
                Assert.IsTrue(Expected_Medical_PMPM.ToString() == Actual_Medical_PMPM.ToString());
                Assert.IsTrue(Expected_Pharmacy_PMPM.ToString()== Actual_Pharmacy_PMPM.ToString());
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Active_PMPM_Shot");
               // _saveToCsv.SaveTestCase("Error", "Error", "Dashboard", "Active_PMPM", _commonFunction.RemoveUnicode(ex.Message));
                Console.Out.WriteLine(ex);
            }
        }
        [Test, Category("PMPM")]
        public void PMPM()
        {
            try
            {
                decimal Expected_Medical_PMPM = Math.Round(Convert.ToDecimal(_dashboard.Expected_PMPM("medical", "All", 1)), 0);
                Console.WriteLine("Mediacal PMPM(all) from database: " + Expected_Medical_PMPM.ToString());
                decimal Expected_Pharmacy_PMPM = Math.Round(Convert.ToDecimal(_dashboard.Expected_PMPM("pharmacy", "All", 1)), 0);
                Console.WriteLine("Pharmacy PMPM(all) from database: " + Expected_Pharmacy_PMPM.ToString());
                _dashboard.ChooseAllMember();
                _dashboard.Click_Spend_PMPM_Link(false);
                decimal Actual_Medical_PMPM = _dashboard.Spend_PMPM(true);
                Console.WriteLine("Mediacal PMPM(all) from UI: " + Actual_Medical_PMPM.ToString());
                decimal Actual_Pharmacy_PMPM = _dashboard.Spend_PMPM(false);
                Console.WriteLine("Mediacal PMPM(all) from UI: " + Actual_Pharmacy_PMPM.ToString());
                _saveToCsv.SaveTestCase(Expected_Medical_PMPM.ToString(), Actual_Medical_PMPM.ToString(), "Dashboard", "Medical PMPM(all)", "Expected PMPM shold be equal to actual PMPM");
                _saveToCsv.SaveTestCase(Expected_Pharmacy_PMPM.ToString(), Actual_Pharmacy_PMPM.ToString(), "Dashboard", "Pharmacy PMPM(all)", "Expected PMPM shold be equal to actual PMPM");
                Assert.IsTrue(Expected_Medical_PMPM.ToString() == Actual_Medical_PMPM.ToString());
                Assert.IsTrue(Expected_Pharmacy_PMPM.ToString() == Actual_Pharmacy_PMPM.ToString());
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("PMPM_Shot");
               // _saveToCsv.SaveTestCase("Error", "Error", "Dashboard", "PMPM", _commonFunction.RemoveUnicode(ex.Message));
                Console.Out.WriteLine(ex);
            }
        }
        [Test, Category("Active Medical Pharmacy Spend Change Percentages")]
        public void Active_Medical_Pharmacy_Spend_Change_Percentages()
        {
            try
            {
                var All_Spend_Current_Year = _dashboard.Expected_Total_Medical_Pharmacy_Spend("active", 1);
                Console.WriteLine("current year spend(all) from database: " + CommonMethods.ObjectToXml(All_Spend_Current_Year) );
                var All_Spend_Last_Year = _dashboard.Expected_Total_Medical_Pharmacy_Spend("active", 2);
                Console.WriteLine("Last year spend(all) from database: " + CommonMethods.ObjectToXml(All_Spend_Last_Year));
                decimal Expected_Medical_Change_spend = _dashboard.Percentages(All_Spend_Current_Year[0], All_Spend_Last_Year[0]);
                Console.WriteLine("Medical Change(all) from database: " + Expected_Medical_Change_spend);
                decimal Expected_Pharmacy_Change_spend = _dashboard.Percentages(All_Spend_Current_Year[1], All_Spend_Last_Year[1]);
                Console.WriteLine("Pharmacy Change(all) from database: " + Expected_Pharmacy_Change_spend);
                _dashboard.ChooseActiveMember();
                decimal Actula_Medical_Change_Spend = _dashboard.Spend_Change(true);  //true for Medical 
                Console.WriteLine("Medical Change(all) from UI: " + Actula_Medical_Change_Spend);
                decimal Actual_Pharmacy_Change_Spend = _dashboard.Spend_Change(false); //false for Pharmacy
                Console.WriteLine("Pharmacy Change(all) from UI: " + Actual_Pharmacy_Change_Spend);

                _saveToCsv.SaveTestCase(String.Format("{0:0.##}", Expected_Medical_Change_spend), String.Format("{0:0.##}", Actula_Medical_Change_Spend), "Dashboard", " Medical Spend Change(active) ", "Expected medical change spend should be equal to actual medical change spend");
                _saveToCsv.SaveTestCase(String.Format("{0:0.##}", Expected_Pharmacy_Change_spend), String.Format("{0:0.##}", Actual_Pharmacy_Change_Spend), "Dashboard", "Pharmacy Spend Change(active)", "Expected pharmacy change spend should be equal to actual pharmacy change spend");
                Assert.IsTrue(String.Format("{0:0.##}", Expected_Medical_Change_spend)== String.Format("{0:0.##}", Actula_Medical_Change_Spend));
                Assert.IsTrue(String.Format("{0:0.##}", Expected_Pharmacy_Change_spend) == String.Format("{0:0.##}", Actual_Pharmacy_Change_Spend));
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Active_Medical_Pharmacy_Spend_Change_Percentages_Shot");
              //  _saveToCsv.SaveTestCase("Error", "Error", "Dashboard", "Active_Medical_Pharmacy_Spend_Change_Percentages", _commonFunction.RemoveUnicode(ex.Message));
                Console.Out.WriteLine(ex);
            }
        }
        [Test, Category("Spend change percentages")]
        public void Medical_Pharmacy_Spend_Change_Percentages()
        {
            try
            {
                var All_Spend_Current_Year = _dashboard.Expected_Total_Medical_Pharmacy_Spend("all", 1);
                var All_Spend_Last_Year = _dashboard.Expected_Total_Medical_Pharmacy_Spend("all", 2);
                decimal Expected_Medical_Change_spend = _dashboard.Percentages(All_Spend_Current_Year[0], All_Spend_Last_Year[0]);
                Console.WriteLine("Medical change(all)  from database: "  + Expected_Medical_Change_spend.ToString());
                decimal Expected_Pharmacy_Change_spend = _dashboard.Percentages(All_Spend_Current_Year[1], All_Spend_Last_Year[1]);
                Console.WriteLine("Pharmacy change(all)  from database: " + Expected_Pharmacy_Change_spend.ToString());
                _dashboard.ChooseAllMember();
                decimal Actula_Medical_Change_Spend = _dashboard.Spend_Change(true); //true for Medical
                Console.WriteLine("Medical change(all)  from UI: " + Actula_Medical_Change_Spend);
                decimal Actual_Pharmacy_Change_Spend = _dashboard.Spend_Change(false); //false for Pharmacy
                Console.WriteLine("Pharmacy change(all)  from UI: " + Actual_Pharmacy_Change_Spend);
                _saveToCsv.SaveTestCase(Expected_Medical_Change_spend.ToString(), Actula_Medical_Change_Spend.ToString(), "Dashboard", " Medical Spend Change(all)", "Expected medical change spend should be equal to actual medical change spend");
                _saveToCsv.SaveTestCase(Expected_Pharmacy_Change_spend.ToString(), Actual_Pharmacy_Change_Spend.ToString(), "Dashboard", "Pharmacy Spend Change(all)", "Expected pharmacy change spend should be equal to actual pharmacy change spend");
                Assert.IsTrue(Expected_Medical_Change_spend == Actula_Medical_Change_Spend);
                Assert.IsTrue(Expected_Pharmacy_Change_spend == Actual_Pharmacy_Change_Spend);
            }
            catch (Exception ex)
            {

                Browser.ScreenShot("Medical_Pharmacy_Spend_Change_Percentages_Shot");
               // _saveToCsv.SaveTestCase("Error", "Error", "Dashboard", "Medical_Pharmacy_Spend_Change_Percentage", _commonFunction.RemoveUnicode(ex.Message));
                Console.Out.WriteLine(ex);
            }

        }
        [Test, Category("Total Active Spend , Total Active Medical Spend and Total Active Pharmacy Spend")]
        public void Total_Active_Medical_Pharmacy_Spend()
        {
            try
            {
                var Active_Spend = _dashboard.Expected_Total_Medical_Pharmacy_Spend("active", 1);
                Console.WriteLine("Toatal Medical Pharmacy spand  from database(active) : " + CommonMethods.ObjectToXml(Active_Spend));
                string Expected_Active_Total_Medical_Spend = "$" + Active_Spend[0].ToString() + "K";
                Console.WriteLine("Total medaical spend(active) from databse: " + Expected_Active_Total_Medical_Spend);
                string Expected_Active_Total_Pharmacy_Spend = "$" + Active_Spend[1].ToString() + "K";
                Console.WriteLine("Total Pharmacy spend(active) from databse: " + Expected_Active_Total_Pharmacy_Spend);
                string Expected_Active_Total_Spend = "$" + (Active_Spend[0] + Active_Spend[1]).ToString() + "K";
                Console.WriteLine("Total Spend(active) from database: " + Expected_Active_Total_Spend);
                _dashboard.ChooseActiveMember();
                string Actual_Active_Total_Medical_Spend = _dashboard.TotalSpend(true);
                Console.WriteLine("Total Medical spend(active) from UI: " + Actual_Active_Total_Medical_Spend);
                string Actual_Active_Total_Pharmacy_Spend = _dashboard.TotalSpend(false);
                Console.WriteLine("Total Pharmacy spend(active) from UI: " + Actual_Active_Total_Pharmacy_Spend);
                string Actual_Active_Total_Spend = _dashboard.TotalSpend();
                Console.WriteLine("Total Spend(active) from UI: " + Actual_Active_Total_Spend);
                 _saveToCsv.SaveTestCase(Expected_Active_Total_Medical_Spend, Actual_Active_Total_Medical_Spend, "Dashboard", "Total Medical Spend(active)", "Expected total active medical spend should be equal to actual total active medical spend ");
                 _saveToCsv.SaveTestCase(Expected_Active_Total_Pharmacy_Spend, Actual_Active_Total_Pharmacy_Spend, "Dashboard", "Total Pharmacy Spend(active)", "Expected total active pharmacy spend should be equal to actual total active pharmacy spend ");
                 _saveToCsv.SaveTestCase(Expected_Active_Total_Spend, Actual_Active_Total_Spend, "Dashboard", "Total Spend(active)", "Expected total active spend should be equal to actual total active spend ");
                Assert.AreEqual(Expected_Active_Total_Pharmacy_Spend, Actual_Active_Total_Pharmacy_Spend);
                Assert.IsTrue(Expected_Active_Total_Medical_Spend == Actual_Active_Total_Medical_Spend);
                Assert.AreEqual(Expected_Active_Total_Spend, Actual_Active_Total_Spend);
            }
            catch (Exception e)
            {
                Browser.ScreenShot("Total_Active_Medical_Pharmacy_Spend_Shot");
               // _saveToCsv.SaveTestCase("Error", "Error", "Dashboard", "Total_Active_Medical_Pharmacy_Spend", _commonFunction.RemoveUnicode(e.Message));
                Console.Out.WriteLine(e);
            }

        }
        [Test, Category("Total Spend , Medical Spend,Pharmacy Spend")]
        public void Total_Medical_Pharmacy_Spend()
        {
            try
            {
                var Total_Spend = _dashboard.Expected_Total_Medical_Pharmacy_Spend("all", 1);
                Console.WriteLine("Toatal Medical Pharmacy spand  from database: " + CommonMethods.ObjectToXml(Total_Spend));
                string Expected_Total_Medical_Spend = "$" + Total_Spend[0].ToString() + "K";
                Console.WriteLine("Total medaical spend(all) from databse: " + Expected_Total_Medical_Spend);
                string Expected_Total_Pharmacy_Spend = "$" + Total_Spend[1].ToString() + "K";
                Console.WriteLine("Total pharmacy spend(all) from database: " + Expected_Total_Pharmacy_Spend);
                string Expected_Total_Spend = "$" + (Total_Spend[0] + Total_Spend[1]).ToString() + "K";
                Console.WriteLine("Total spend(all) from database: " + Expected_Total_Spend);
                _dashboard.ChooseAllMember();
                string Actual_Total_Medical_Spend = _dashboard.TotalSpend(true);
                Console.WriteLine("Total medaical spend (all) from UI: " + Actual_Total_Medical_Spend);
                string Actual_Total_Pharmacy_Spend = _dashboard.TotalSpend(false);
                Console.WriteLine("Total pharmacy spend (all) from UI: " + Actual_Total_Pharmacy_Spend);
                string Actual_Total_Spend = _dashboard.TotalSpend();
                Console.WriteLine("Total spend (all) from UI: " + Actual_Total_Spend);

                _saveToCsv.SaveTestCase(Expected_Total_Medical_Spend, Actual_Total_Medical_Spend, "Dashboard", "Total Medical Spend(all)", "Expected total medical spend should be equal to total actual medical spend ");
                _saveToCsv.SaveTestCase(Expected_Total_Pharmacy_Spend, Actual_Total_Pharmacy_Spend, "Dashboard", "Total Pharmacy Spend(all)", "Expected total pharmacy spend should be equal to actual total pharmacy spend ");
                _saveToCsv.SaveTestCase(Expected_Total_Spend, Actual_Total_Spend, "Dashboard", "Total  Spend(all)", "Expected total spend should be equal to actual total spend ");
                Assert.IsTrue(Expected_Total_Pharmacy_Spend == Actual_Total_Pharmacy_Spend);
                Assert.IsTrue(Expected_Total_Medical_Spend == Actual_Total_Medical_Spend);
                Assert.IsTrue(Expected_Total_Spend== Actual_Total_Spend);
               
            }
            catch (Exception e)
            {
                Browser.ScreenShot("Total_Medical_Pharmacy_Spend_Shot");
               // _saveToCsv.SaveTestCase("Error", "Error", "Dashboard", "Total_Medical_Pharmacy_Spend", _commonFunction.RemoveUnicode(e.Message));
                Console.Out.WriteLine(e);
            }
        }
        [Test, Category("Total Member and Total Employee")]
        public void Total_Member_Total_Employee()
        {
            try
            {
                string ExpectedTotalMember = _dashboard.ExpectedTotalMember();
                Console.WriteLine("Total member(all) from database: " + ExpectedTotalMember);
                string ExpectedTotalEmployee = _dashboard.ExpectedTotalEmployee();
                Console.WriteLine("Total employee(all) from database: " + ExpectedTotalEmployee);
                string ActualTotalMember = _dashboard.TotalMember(false);
                Console.WriteLine("Total member(all) from UI: " + ActualTotalMember);
                string ActualTotalEmployee = _dashboard.TotalEmployee(false);
                Console.WriteLine("Total employee(all) from UI: " + ActualTotalEmployee);
                _saveToCsv.SaveTestCase(ExpectedTotalMember, ActualTotalMember, "Dashboard", "Toatal Member(all)", "Acutal toatl member shoud be equal to expected total member");
                _saveToCsv.SaveTestCase(ExpectedTotalEmployee, ActualTotalEmployee, "DashBoard", "Total employee(all)", "Actual toal employee shoud be equal to expected total employee");
                Assert.IsTrue(ExpectedTotalMember == ActualTotalMember);
                Assert.IsTrue(ExpectedTotalEmployee== ActualTotalEmployee);
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Total_Member_Total_Employee_Shot");
               // _saveToCsv.SaveTestCase("Error", "Error", "Dashboard", "Total_Member_Total_Employee", _commonFunction.RemoveUnicode(ex.Message));
                Console.Out.WriteLine(ex);
            }
        }
        [Test, Category("Active Member and Active Employee")]
        public void Active_Member_Active_Employee()
        {
            try
            {
                string ExpectedActiveMember = _dashboard.ExpectedTotalActiveMember();
                Console.WriteLine("Total member(active) from database: " + ExpectedActiveMember);
                string ExpectedActiveEmployee = _dashboard.ExpectedTotalActiveEmployee();
                Console.WriteLine("Total employee (active) from database: " + ExpectedActiveEmployee);
                
                string ActualActiveMember = _dashboard.TotalMember(true);
                Console.WriteLine("Total member(active) from UI:" + ActualActiveMember);
                string ActualActiveEmployee = _dashboard.TotalEmployee(true);
                Console.WriteLine("Total employee(active) from UI:" + ActualActiveEmployee);

                _saveToCsv.SaveTestCase(ExpectedActiveMember, ActualActiveMember, "DashBoard", "Member(active)", "Acutal active member shoud be equal to expected active member");
                _saveToCsv.SaveTestCase(ExpectedActiveEmployee, ActualActiveEmployee, "DashBoard", "Employee(active)", "Acutal active employee shoud be equal to expected active employee");
                Assert.IsTrue(ExpectedActiveMember == ActualActiveMember);
                Assert.IsTrue(ExpectedActiveEmployee== ActualActiveEmployee);
            }
            catch (Exception e)
            {
                Browser.ScreenShot("Active_Member_Active_Employee_Shot");
               // _saveToCsv.SaveTestCase("Error", "Error", "Dashboard", "Active_Member_Active_Employee", _commonFunction.RemoveUnicode(e.Message));
                Console.Out.WriteLine(e);
            }
        }
        #endregion
    }
}

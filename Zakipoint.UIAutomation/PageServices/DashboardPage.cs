using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class DashboardPage
    {
        #region Private Fields

        private readonly DashboardPageObjects _dashboardPage;
        private readonly DashboardSqlScripts _dashboardSqlScripts; 
        private readonly MySqlStatementExecutor _executor;

        #endregion

        #region Constructor

        public DashboardPage()
        {
            _dashboardPage = new DashboardPageObjects();
            _dashboardSqlScripts = new DashboardSqlScripts();
            _executor = new MySqlStatementExecutor();
        }

        #endregion

        #region Database Methods

        public List<List<string>> GetGroupID()
        {
            var newList = new List<List<string>>();
            var groupIdList = _executor.GetCompleteTable(_dashboardSqlScripts.GetAppGroupsValueList);
            groupIdList.FirstOrDefault().ItemArray.Select(x => x.ToString()).ToList();
            foreach (var groupId in groupIdList)
            {
                newList.Add(groupId.ItemArray.Select(x => x.ToString()).ToList());
            }
            return newList;
        }
        public string ExpectedTotalEmployee()
        {
            var _expectedTotalEmployee = _executor.GetSingleValue(_dashboardSqlScripts.Expected_Total_Employee());
            return _expectedTotalEmployee.ToString();
        }
        public string ExpectedTotalMember()
        {
            long _expectedTotalEmployee = _executor.GetSingleValue(_dashboardSqlScripts.Expected_Total_Member());
            return _expectedTotalEmployee.ToString();
        }
        public string ExpectedTotalActiveEmployee()
        {
            long _expectedTotalActiveEmployee = _executor.GetSingleValue(_dashboardSqlScripts.Expected_Total_Active_Employee());
            return _expectedTotalActiveEmployee.ToString();
        }
        public string ExpectedTotalActiveMember()
        {
            long _expectedTotalActiveEmployee = _executor.GetSingleValue(_dashboardSqlScripts.Expected_Total_Active_Member());
            return _expectedTotalActiveEmployee.ToString();
        }
        public List<decimal> Expected_Total_Medical_Pharmacy_Spend(string active_flag, int period)
        {
            string sql = "";
            if (active_flag.ToLower() == "all")
            {
                sql = _dashboardSqlScripts.Expected_Total_Medical_Pharmacy_Sepnd(period);
            }
            else
            {
                sql = _dashboardSqlScripts.Expected_Total_Active_Medical_Pharmacy_Spend(period);
            }
            var listItems = _executor.GetCompleteTable(sql)
                .FirstOrDefault()
                .ItemArray.Select(x => CommonMethods.CurrencyIntermOfThousandWithRoudValue(Convert.ToDecimal(x)))
                .ToList();

            return listItems;
        }
        public string Expected_PMPM(string spendType, string active_flag, int period)
        {
            string sql;
            if (spendType.ToString() == "medical")
            {
                sql = _dashboardSqlScripts.Medical_PMPM(active_flag, period);
            }
            else
            {
                sql = _dashboardSqlScripts.Pharmacy_PMPM(active_flag, period);
            }
            return _executor.GetSingleStringValue(sql);
        }
        public decimal Expected_PMPM_Change(string memberType, string active_flag)
        {
            decimal CurrentYearvale = Math.Round(Convert.ToDecimal(Expected_PMPM(memberType, active_flag, 1)), 2);
            decimal LastYearvale = Math.Round(Convert.ToDecimal(Expected_PMPM(memberType, active_flag, 2)), 2);
            return Percentages(CurrentYearvale, LastYearvale);
        }
        public List<Top_Condtion_By_Total_spend> Expected_Top_Condition_By_Total_Spend(string memberStatus)
        {
            List<Top_Condtion_By_Total_spend> objList = new List<Top_Condtion_By_Total_spend>();
            var dt = _executor.GetDataTable(_dashboardSqlScripts.Top_Condition_By_Total_Spend(StartDate(), EndDate(), memberStatus));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Top_Condtion_By_Total_spend obj = new Top_Condtion_By_Total_spend
                {
                    Conditions = dt.Rows[i]["disease_name"].ToString(),
                    P_Spend = dt.Rows[i]["P_spend"].ToString(),
                    Spend = dt.Rows[i]["Spend"].ToString(),
                    P_Change = dt.Rows[i]["P_chnage"].ToString(),
                    Members = dt.Rows[i]["Members"].ToString()
                };
                objList.Add(obj);
            }
            return objList;
        }
        public List<Top_Service_By_Total_Spend> Expected_Top_Service_By_Total_Spend(string memberStatus)
        {
            List<Top_Service_By_Total_Spend> objList = new List<Top_Service_By_Total_Spend>();
            var dt = _executor.GetDataTable(_dashboardSqlScripts.Top_Service_By_Total_Spend(memberStatus));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Top_Service_By_Total_Spend obj = new Top_Service_By_Total_Spend
                {
                    Members = dt.Rows[i]["Members"].ToString(),
                    Services= dt.Rows[i]["Services"].ToString().ToUpper(),
                    UtilizationPerThousand= dt.Rows[i]["UtilizationPerThousand"].ToString(),
                    Spend= dt.Rows[i]["Spend"].ToString(),
                    PMPM= dt.Rows[i]["PMPM"].ToString()
                };
                objList.Add(obj);
            }
            return objList;
        }
        public List<Cost_Matrix> Expected_Cost_Matrix(string memberStatus)
        {
            List<Cost_Matrix> objList = new List<Cost_Matrix>();
            var dt = _executor.GetDataTable(_dashboardSqlScripts.Cost_Matrix(memberStatus));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Cost_Matrix obj = new Cost_Matrix
                {
                    Members = dt.Rows[i]["Members"].ToString(),
                    Cost_Categories= dt.Rows[i]["Cost_Categories"].ToString(),
                    P_Spend= dt.Rows[i]["P_Spend"].ToString(),
                    Spend= dt.Rows[i]["Spend"].ToString()

                };
                objList.Add(obj);
             }
            return objList;
        }
       public List<Prospective_Population_Risk_Stratification> Expected_Prospective_Population_Risk_Stratification(string memberStatus)
        {

            List<Prospective_Population_Risk_Stratification> objList = new List<Prospective_Population_Risk_Stratification>();
            var dt = _executor.GetDataTable(_dashboardSqlScripts.Prospective_Population_Risk_Stratification(memberStatus));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Prospective_Population_Risk_Stratification obj = new Prospective_Population_Risk_Stratification
                {
                    Percentages_Member= dt.Rows[i]["Percentages_Member"].ToString(),
                    Members = dt.Rows[i]["Members"].ToString(),
                    Risk_Type = dt.Rows[i]["Risk_Type"].ToString().ToUpper(),
                    Percentages_Spend = dt.Rows[i]["Percentages_Spend"].ToString(),
                    Spend = dt.Rows[i]["Spend"].ToString(),
                    PMPM = dt.Rows[i]["PMPM"].ToString()
                };
                objList.Add(obj);
            }
            return objList;
        }
        public decimal Percentages(decimal CurrentYearvale, decimal LastYearvale)
        {
            return Math.Abs(Math.Round(CommonMethods.Percentages(CurrentYearvale, LastYearvale), 2));
        }

        #endregion

        #region Public Methods

        public List<string> GetMenuList()
        {
            List<string> MenuList = new List<string>();
            var listMenu = Browser.FindElements(How.CssSelector, _dashboardPage.MenuLinkCssSelector);
            foreach (var menu in listMenu)
            {
                MenuList.Add(menu.Text);
            }
            return MenuList;
        }

        public string GetClientName()
        {
            return Browser.FindElement(How.CssSelector, _dashboardPage.ClientTitleCssSelector).Text;
        }

        public string GetDownloadReportName()
        {
            var clientReportName = GetClientName().ToLower().Replace(" ", "-") + ("-report-" + DateTime.Now.ToString("yyyy-MM-dd") + ".pdf");
            return clientReportName;
        }

        public void ClickDownloadReport()
        {
            Browser.JavaScriptOnclick(Browser.FindElement(How.CssSelector, _dashboardPage.DownloadReportLinkCssSelector));
            //Browser.FindElement(How.CssSelector, _dashboardPage.DownloadReportLinkCssSelector).Click();
        }

        public List<string> GetTableHeaderList(How locator, string value)
        {
            var headersList = new List<string>();
            var tableHeaderList = Browser.FindElements(locator, value);
            foreach (var tableHeader in tableHeaderList)
            {
                if (tableHeader.Text != "")
                {
                    headersList.Add(tableHeader.Text);
                }
            }
            return headersList;
        }

        #endregion

        #region General info
        public string TotalSpend()
        {
            string TotalSpend = CommonMethods.RemoveComma(Browser.FindElement(How.XPath, Format(_dashboardPage.ClientBoxDetailsByLabelXPath, "Total Spend")).Text);
            return TotalSpend;
        }
        public string TotalMember(bool Active)
        {
            if (Active)
            {
                ChooseActiveMember();
                string ActualActiveMember = CommonMethods.RemoveComma(Browser.FindElement(How.XPath, Format(_dashboardPage.ClientBoxDetailsByLabelXPath, "Active Members")).Text);
                return ActualActiveMember;
            }
            else
            {
                ChooseAllMember();
                string ActualAllMember = CommonMethods.RemoveComma(Browser.FindElement(How.XPath, Format(_dashboardPage.ClientBoxDetailsByLabelXPath, "All Members")).Text);
                return ActualAllMember;
            }
        }
        public void ChooseActiveMember()
        {
            if (Browser.FindElements(How.XPath, _dashboardPage.ClientBoxLabelTextByXPath)[1].Text != "Active Members")
            {
                Browser.FindElement(How.XPath, _dashboardPage.ApplicationSettinsgByXPath).Click();
                Console.WriteLine("Click setting icon");
                Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(_dashboardPage.MemberStatusChangeXPath)));
                Console.WriteLine("conditional wait");
                Browser.FindElement(How.XPath, _dashboardPage.MemberStatusChangeXPath).Click();
                Console.WriteLine("Member status: change link text ");
                Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(Format(_dashboardPage.RadioMemberByCssSelector, "termed01"))));
                Console.WriteLine("Conditional wait");
                Browser.FindElement(How.CssSelector, Format(_dashboardPage.RadioMemberByCssSelector, "termed01")).Click();
                Console.WriteLine("Choose radio button Active Members ");
                Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(_dashboardPage.ApplySettingXPath)));
                Console.WriteLine("conditional wait");
                Browser.FindElement(How.XPath, _dashboardPage.ApplySettingXPath).Click();
                Console.WriteLine("Click on Apply Setting button");
                Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(_dashboardPage.ClientBoxLabelTextByXPath + "[contains(text(),'Active Employees')]")));
                Console.WriteLine("conditional wait");
            }
        }
        public void ChooseAllMember()
        {
            if (Browser.FindElements(How.XPath, _dashboardPage.ClientBoxLabelTextByXPath)[1].Text != "All Members")
            {
                Browser.FindElement(How.XPath, _dashboardPage.ApplicationSettinsgByXPath).Click();
                Console.WriteLine("Click setting icon");
                Console.WriteLine("conditional wait");
                Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(_dashboardPage.MemberStatusChangeXPath)));
                Browser.FindElement(How.XPath,_dashboardPage.MemberStatusChangeXPath).Click();
                Console.WriteLine("Member status: change link text ");
                Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(Format(_dashboardPage.RadioMemberByCssSelector, "termed02"))));
                Console.WriteLine("conditional wait");
                Browser.FindElement(How.CssSelector,Format(_dashboardPage.RadioMemberByCssSelector, "termed02")).Click();
                Console.WriteLine("Choose radio button All Members ");
                Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(_dashboardPage.ApplySettingXPath)));
                Console.WriteLine("conditional wait");
                Browser.FindElement(How.XPath, _dashboardPage.ApplySettingXPath).Click();
                Console.WriteLine("Click on Apply Setting button");
                Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(_dashboardPage.ClientBoxLabelTextByXPath + "[contains(text(),'All Employees')]")));
                Console.WriteLine("conditional wait");
            }
        }
        public string TotalEmployee(bool Active)
        {
            if (Active)
            {
                string actualActiveEmployee = CommonMethods.RemoveComma(Browser.FindElement(How.XPath, Format(_dashboardPage.EmployeesDetailsByLabelXPath, "Active Employees ")).Text.Replace("Active Employees", ""));
                return actualActiveEmployee;
            }
            else
            {
                string actualAllEmployee = CommonMethods.RemoveComma(Browser.FindElement(How.XPath, Format(_dashboardPage.EmployeesDetailsByLabelXPath, "All Employees ")).Text.Replace("All Employees", ""));
                return actualAllEmployee;
            }
        }
        public string TotalSpendMember(bool Active)
        {
            if (Active)
            { 
                ChooseActiveMember(); 
            }
            else
            {
                ChooseAllMember();
            }
            string totalSpendOfMember = CommonMethods.RemoveComma(Browser.FindElement(How.CssSelector, Format(_dashboardPage.ClientBoxDetailsByLabelXPath, "Total Spend")).Text);
            return totalSpendOfMember;
        }
        public void Click_Spend_PMPM_Link(bool Spend)
        {
            if (Spend)
            {
                if (Browser.FindElement(How.LinkText, "SPEND").GetAttribute("class").Contains("inactive"))
                {
                    Browser.FindElement(How.LinkText, "SPEND").Click();
                    Console.WriteLine("Click on SPEND link");
                    Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(Format(_dashboardPage.SpendByLabelXPath, "Medical"))));
                    Console.WriteLine("Conditional wait");
                }
            }
            else
            {
                if (Browser.FindElement(How.LinkText, "PMPM").GetAttribute("class").Contains("inactive"))
                {
                    Browser.FindElement(How.LinkText, "PMPM").Click();
                    Console.WriteLine("click on PMPM link");
                    Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(Format(_dashboardPage.PMPMByLabelXPath, "Medical"))));
                    Console.WriteLine("Conditional wait");
                }
            }           
        }
        public void Click_Condition_Service_Link(bool condition)
        {
            if (condition)
            {
                if (Browser.FindElement(How.LinkText, "CONDITION").GetAttribute("class").Contains("inactive"))
                {
                    Browser.FindElement(How.LinkText, "CONDITION").Click();
                    Console.WriteLine("Click on CONDITION link");
                    // Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(Format(_dashboardPage.SpendByLabelXPath, "Medical"))));
                    Thread.Sleep(20000);
                    
                }
            }
            else
            {
                if (Browser.FindElement(How.LinkText, "SERVICE").GetAttribute("class").Contains("inactive"))
                {
                    Browser.FindElement(How.LinkText, "SERVICE").Click();
                    Console.WriteLine("click on  SERVICE  link");
                    Thread.Sleep(20000);
                    //Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(Format(_dashboardPage.PMPMByLabelXPath, "Medical"))));
                    // Console.WriteLine("Conditional wait");
                }
            }
        }
        public string TotalSpend(bool Medical)
        {
            if (Medical)
            {
                string totalMedicalSpendText = Browser.FindElement(How.XPath, Format(_dashboardPage.SpendByLabelXPath, "Medical")).Text;
                string[] lines = CommonMethods.SplitByNewLine(totalMedicalSpendText);
                string totalMedicalSpend = CommonMethods.RemoveComma(lines[0]);
                return totalMedicalSpend;
            }
            else
            {
                string totalPharmacySpendText = Browser.FindElement(How.XPath, Format(_dashboardPage.SpendByLabelXPath, "Pharmacy")).Text;
                string[] lines = CommonMethods.SplitByNewLine(totalPharmacySpendText);
                string totalPharmacySpend = CommonMethods.RemoveComma(lines[0]);
                return totalPharmacySpend;
            }
        }
        public decimal Spend_PMPM(bool Medical)
        {
            if (Medical)
            {
                string totalMedicalPMPMText = Browser.FindElement(How.XPath, Format(_dashboardPage.PMPMByLabelXPath, "Medical")).Text;
                string[] lines = CommonMethods.SplitByNewLine(totalMedicalPMPMText);
                return Convert.ToDecimal(CommonMethods.RemoveComma(lines[0]).Replace("$", ""));

            }
            else
            {
                string totalPharmacyPMPMText = Browser.FindElement(How.XPath, Format(_dashboardPage.PMPMByLabelXPath, "Pharmacy")).Text;
                string[] lines = CommonMethods.SplitByNewLine(totalPharmacyPMPMText);
                return Convert.ToDecimal(CommonMethods.RemoveComma(lines[0]).Replace("$", ""));
            }
        }
        public decimal Spend_Change(bool Medical)
        {
            if (Medical)
            {
                string totalMedicalspendText = Browser.FindElement(How.XPath, Format(_dashboardPage.SpendByLabelXPath, "Medical")).Text;
                string[] lines = CommonMethods.SplitByNewLine(totalMedicalspendText);
                return Math.Round(Convert.ToDecimal(CommonMethods.RemoveComma(lines[1]).Split('%')[0]), 2);
            }
            else
            {
                string totalPharmacyspendText = Browser.FindElement(How.XPath, Format(_dashboardPage.SpendByLabelXPath, "Pharmacy")).Text;
                string[] lines = CommonMethods.SplitByNewLine(totalPharmacyspendText);
                return Math.Round(Convert.ToDecimal(CommonMethods.RemoveComma(lines[1]).Split('%')[0]), 2);
            }          
        }
        public decimal PMPM_Change(bool Medical)
        {
            if (Medical)
            {
                string totalMedicalPMPMText = Browser.FindElement(How.XPath, Format(_dashboardPage.PMPMByLabelXPath, "Medical")).Text;
                string[] lines = CommonMethods.SplitByNewLine(totalMedicalPMPMText);
                return Math.Round(Convert.ToDecimal(CommonMethods.RemoveComma(lines[1]).Split('%')[0]), 2);
            }
            else
            {
                string totalPharmacyPMPMText = Browser.FindElement(How.XPath, Format(_dashboardPage.PMPMByLabelXPath, "Pharmacy")).Text;
                string[] lines = CommonMethods.SplitByNewLine(totalPharmacyPMPMText);
                return Math.Round(Convert.ToDecimal(CommonMethods.RemoveComma(lines[1]).Split('%')[0]), 2);
            }
        }
        #region object map
        public List<Top_Condtion_By_Total_spend> Map_Object(List<List<string>> tableDetails)
        {
            List<Top_Condtion_By_Total_spend> objList = new List<Top_Condtion_By_Total_spend>();
            foreach (var item in tableDetails)
            {
                Top_Condtion_By_Total_spend obj = new Top_Condtion_By_Total_spend
                {
                    Conditions = item[0].ToString(),
                    P_Spend = item[1].ToString().Replace("%", "").Trim(),
                    Spend = item[2].ToString().Replace(",", "").Replace("K", "").Replace("$", "").Trim(),
                    P_Change = item[3].ToString().Replace("%", "").Trim(),
                    Members = item[4].ToString().Replace(",", "").Trim()
                };
                objList.Add(obj);
            }
            return objList;
            
        }
        public List<Top_Service_By_Total_Spend> Map_Top_Service_By_Total_Spend(List<List<string>> tableDetails)
        {
            List<Top_Service_By_Total_Spend> objList = new List<Top_Service_By_Total_Spend>();
            foreach (var item in tableDetails)
            {
                Top_Service_By_Total_Spend obj = new Top_Service_By_Total_Spend
                {
                    Services = item[0].ToString().Replace(",", "").ToUpper().Trim(),
                    Spend = item[1].ToString().Replace(",", "").Replace("$", "").Replace("K","").Trim(),
                    Members = item[2].ToString().Replace(",", "").Trim(),
                    UtilizationPerThousand = item[3].ToString().Replace(",", "").Trim(),
                    PMPM = item[4].ToString().Replace(",", "").Replace("$","").Trim()
                };
                objList.Add(obj);
            }
            objList.RemoveAt(0);
            return objList;

        }
        public List<Prospective_Population_Risk_Stratification> Map_Prospective_Population_Risk_Stratification(List<List<string>> tableDetails)
        {
            List<Prospective_Population_Risk_Stratification> objList = new List<Prospective_Population_Risk_Stratification>();
            int objlength = tableDetails.Count;
            for (int i = 1; i < objlength; i++)
            {
                Prospective_Population_Risk_Stratification obj = new Prospective_Population_Risk_Stratification
                {
                    Risk_Type= tableDetails[i][0].ToString().Replace(",", "").ToUpper().Trim(),
                    Spend = tableDetails[i][1].Split('K')[0].Replace(",", "").Replace("$","").Trim(),
                    Percentages_Spend= tableDetails[i][1].ToString().Split('K')[1].Replace("(", "").Replace(")","").Replace("%","").Trim(),
                    Members = tableDetails[i][2].ToString().Split('(')[0].Replace(",", "").Trim(),
                    Percentages_Member= tableDetails[i][2].ToString().Split('(')[1].Replace(")", "").Replace("%","").Trim(),
                    PMPM = tableDetails[i][3].ToString().Replace(",", "").Replace("$","").Trim()
                };
                objList.Add(obj);
            }
            return objList;

        }
        public List<Cost_Matrix> Map_Cost_Matrix(List<List<string>> tableDetails)
        {
            List<Cost_Matrix> objList = new List<Cost_Matrix>();
            foreach (var item in tableDetails)
            {
                Cost_Matrix obj = new Cost_Matrix
                {
                    Cost_Categories= item[0].ToString().Replace(",","").Trim(),
                    P_Spend = item[1].ToString().Replace(",", "").Replace("%","").Trim(),
                    Spend = item[2].ToString().Replace(",", "").Replace("$","").Replace("K","").Trim(),
                    Members = item[3].ToString().Replace(",", "").Trim()
                };
                objList.Add(obj);
            }
            return objList;

        }

        #endregion
        public List<List<string>> Prospective_Population_Risk_Stratification()
        {
           // Browser.PageScroll(0, 950); // cordinate 
            var tableData = CommonMethods.GetTableValues(How.CssSelector, _dashboardPage.ProspectivePopulationRiskStratificationRowByCssSelector, How.CssSelector, _dashboardPage.ProspectivePopulationRiskStratificationDetailsByRowCssSelector);
          //  Browser.PageScroll(0, 0);
            return tableData;

        }
        public List<List<string>> Top_Service_By_Total_Spend()
        {
            
            var tableData = CommonMethods.GetTableValues(How.CssSelector, _dashboardPage.TopServiceByTotalSpendRowByCssSelector, How.CssSelector, _dashboardPage.TopServiceByTotalSpendDetailsByRowCssSelector);

            return tableData;
        }
        public List<List<string>> Cost_Matrix()
        {

            var tableData = CommonMethods.GetTableValues(How.CssSelector, _dashboardPage.CostMatrixRowByRowCssSelector, How.CssSelector, _dashboardPage.CostMatrixDetailsByRowCssSelector);

            return tableData;
        }
        public void DashboardPageLoad()
        {
            Browser.WaitForExpectedConditions().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector(Format(_dashboardPage.ReportingPeriodByCssSelector, 2))));
        }

        #endregion

        #region Reporting Period

        public string StartDate()
        {
            string[] Period = CommonMethods.SplitByString(Browser.FindElement(How.CssSelector, Format(_dashboardPage.ReportingPeriodByCssSelector, 2)).Text, " to"); //2 for reporting period
            return String.Format("{0:yyyyMM}", Convert.ToDateTime(Period[0]));
        }

        public string EndDate()
        {
            string[] Period = CommonMethods.SplitByString(Browser.FindElement(How.CssSelector, Format(_dashboardPage.ReportingPeriodByCssSelector, 2)).Text, " to"); //2 for reporting period
            return String.Format("{0:yyyyMM}", Convert.ToDateTime(Period[1]));
        }

        #endregion
    }
}

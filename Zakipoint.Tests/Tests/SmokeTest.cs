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
    public class SmokeTest : AbstractBase
    {
        #region Private Methods

        private readonly SetClientPageObjects _setClientPage;
        private readonly SetClientPage _setClient;
        private readonly DashboardPageObjects _dashboardPage;
        private readonly DashboardPage _dashboard;
        private readonly LoginPage _login;
        private readonly CommonFunction _commonFunction;
        private readonly SaveToCSV _saveToCsv;

        #endregion

        #region Constructor

        public SmokeTest()
        {
            _dashboard = new DashboardPage();
            _login = new LoginPage();
            _setClient = new SetClientPage();
            _setClientPage = new SetClientPageObjects();
            _commonFunction = new CommonFunction();
            _dashboardPage = new DashboardPageObjects();
            _saveToCsv = new SaveToCSV();
        }

        #endregion

        #region Base Methods


        public override void Init()
        {
            Browser.Open(Browser.Config["url"]);
            _login.Login(JsonDataReader.Data["username"], JsonDataReader.Data["password"]);
        }


        public override void Dispose()
        {
            _commonFunction.Logout();
        }

        #endregion

        #region SMOKE TESTS

        [Test,Order(1), Category("SmokeTest")]
        public void Verify_Arc_Administrators()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[1]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[1]);
                    Console.WriteLine("Verify Cilent: "+ _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[1], _dashboard.GetClientName(), "SmokeTest", "Verify_Arc_Administrator", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Arc_Administrators");
                }
            }
            catch(Exception ex)
            {
                Browser.ScreenShot("Verify_Arc_Administrators_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Arc_Administrators", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }

        [Test, Order(2), Category("SmokeTest")]
        public void Verify_Asbury_University()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[2]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[2]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[2], _dashboard.GetClientName(), "SmokeTest", "Verify_Asbury_University", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Asbury_University");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Asbury_University_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Asbury_University", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }

        [Test, Order(3), Category("SmokeTest")]
        public void Verify_Beacon_Health_Options()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[3]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[3]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[3], _dashboard.GetClientName(), "SmokeTest", "Verify_Beacon_Health_Options", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Beacon_Health_Options");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Beacon_Health_Options_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Beacon_Health_Options", "Exception occured:  Please verify manually");

                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(4), Category("SmokeTest")]
        public void Verify_Benefit_Management_Inc()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[4]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[4]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[4], _dashboard.GetClientName(), "SmokeTest", "Verify_Benefit_Management_Inc", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Benefit_Management_LLC");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Benefit_Management_Inc_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Benefit_Management_Inc", "Exception occured:  Please verify manually");

                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(5),Category("SmokeTest")]
        public void Verify_Benefit_Management_LLC()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[5]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[5]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[5], _dashboard.GetClientName(), "SmokeTest", "Verify_Benefit_Management_LLC", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Benefit_Management_LLC");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Benefit_Management_LLC_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Benefit_Management_LLC", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(6), Category("SmokeTest")]
        public void Verify_Big_Ass_Fans()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[6]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[6]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[6], _dashboard.GetClientName(), "SmokeTest", "Verify_Big_Ass_Fans", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Big_Ass_Fans");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Big_Ass_Fans_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Big_Ass_Fans", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(7), Category("SmokeTest")]
        public void Verify_Blackhawk_Mining()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[7]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[7]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[7], _dashboard.GetClientName(), "SmokeTest", "Verify_Blackhawk_Mining", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Blackhawk_Mining");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Blackhawk_Mining_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Blackhawk_Mining", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(8), Category("SmokeTest")]
        public void Verify_BML_City_of_Pasco()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[8]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[8]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[8], _dashboard.GetClientName(), "SmokeTest", "Verify_BML_City_of_Pasco", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_BML_City_of_Pasco");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_BML_City_of_Pasco_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_BML_City_of_Pasco", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(9), Category("SmokeTest")]
        public void Verify_BML_JF_Sobieski_Mechanical()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[9]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[9]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[9], _dashboard.GetClientName(), "SmokeTest", "Verify_BML_JF_Sobieski_Mechanical", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_BML_JF_Sobieski_Mechanical");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_BML_JF_Sobieski_Mechanical_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_BML_JF_Sobieski_Mechanical", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(10), Category("SmokeTest")]
        public void Verify_BML_New_Mexico_Medical_Insurance()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[10]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[10]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[10], _dashboard.GetClientName(), "SmokeTest", "Verify_BML_New_Mexico_Medical_Insurance", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_BML_New_Mexico_Medical_Insurance");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_BML_New_Mexico_Medical_Insurance_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_BML_New_Mexico_Medical_Insurance", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test,Order(11), Category("SmokeTest")]
        public void Verify_Cabarrus_County()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[11]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[11]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[11], _dashboard.GetClientName(), "SmokeTest", "Verify_Cabarrus_County", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Cabarrus_County");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Cabarrus_County_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Cabarrus_County", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(12), Category("SmokeTest")]
        public void Verify_Cape_Associates()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[12]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[12]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[12], _dashboard.GetClientName(), "SmokeTest", "Cape Associates", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Cape_Associates");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Cape_Associates_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Cape_Associates", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(13), Category("SmokeTest")]
        public void Verify_Childers_Oil()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[13]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[13]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[13], _dashboard.GetClientName(), "SmokeTest", "Verify_Childers_Oil", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Childers_Oil");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Childers_Oil_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Childers_Oil", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(14), Category("SmokeTest")]
        public void Verify_City_of_Asheboro()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[14]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[14]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[14], _dashboard.GetClientName(), "SmokeTest", "Verify_City_of_Asheboro", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_City_of_Asheboro");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_City_of_Asheboro_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_City_of_Asheboro", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test,Order(15), Category("SmokeTest")]
        public void Verify_City_Of_Lynn()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[15]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[15]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[15], _dashboard.GetClientName(), "SmokeTest", "Verify_City_Of_Lynn", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_City_Of_Lynn");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_City_Of_Lynn_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_City_Of_Lynn", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(16), Category("SmokeTest")]
        public void Verify_City_of_Salisbury()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[16]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[16]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[16], _dashboard.GetClientName(), "SmokeTest", "Verify_City_of_Salisbury", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_City_of_Salisbury");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_City_of_Salisbury_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_City_of_Salisbury", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(17), Category("SmokeTest")]
        public void Verify_City_of_Sanford()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[17]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[17]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[17], _dashboard.GetClientName(), "SmokeTest", "Verify_City_of_Sanford", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_City_of_Sanford");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_City_of_Sanford_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_City_of_Sanford", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(18), Category("SmokeTest")]
        public void Verify_Cleveland_County()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[18]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[18]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[18], _dashboard.GetClientName(), "SmokeTest", "Verify_Cleveland_County", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Cleveland_County");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Cleveland_County_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Cleveland_County", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test,Order(19), Category("SmokeTest")]
        public void Verify_DentaQuest()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[19]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[19]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[19], _dashboard.GetClientName(), "SmokeTest", "Verify_DentaQuest", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_DentaQuest");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_DentaQuest_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_DentaQuest", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(20), Category("SmokeTest")]
        public void Verify_Ebix()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[20]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[20]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[20], _dashboard.GetClientName(), "SmokeTest", "Verify_Ebix", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Ebix");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Ebix_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Ebix", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test,Order(21), Category("SmokeTest")]
        public void Verify_Edgecombe()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[21]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[21]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[21], _dashboard.GetClientName(), "SmokeTest", "Verify_Edgecombe", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Edgecombe");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Edgecombe_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Edgecombe", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(22), Category("SmokeTest")]
        public void Verify_Fabmetal()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[22]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[22]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[22], _dashboard.GetClientName(), "SmokeTest", "Verify_Fabmetal", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Fabmetal");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Fabmetal_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Fabmetal", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test,Order(23), Category("SmokeTest")]
        public void Verify_Feeney_Brothers()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[23]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[23]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[23], _dashboard.GetClientName(), "SmokeTest", "Verify_Feeney_Brothers", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Feeney_Brothers");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Feeney_Brothers_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Feeney_Brothers", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Category("SmokeTest")]
        public void Verify_Halifax_County()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[24]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[24]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[24], _dashboard.GetClientName(), "SmokeTest", "Verify_Halifax_County", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Halifax_County");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Halifax_County_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Halifax_County", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test,Order(25), Category("SmokeTest")]
        public void Verify_Harlan_Bakeries()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[25]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[25]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[25], _dashboard.GetClientName(), "SmokeTest", "Harlan Bakeries", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Harlan Bakeries");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Harlan Bakeries_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Harlan_Bakeries", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(26), Category("SmokeTest")]
        public void Verify_Haywood_County()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[26]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[26]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[26], _dashboard.GetClientName(), "SmokeTest", "Verify_Halifax_County", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Haywood_County");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Haywood_County_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Haywood_County", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test,Order(27), Category("SmokeTest")]
        public void Verify_KGHM()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[27]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[27]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[27], _dashboard.GetClientName(), "SmokeTest", "Verify_Halifax_County", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_KGHM");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_KGHM_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_KGHM", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test,Order(28), Category("SmokeTest")]
        public void Verify_Lincoln_County()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[28]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[28]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[28], _dashboard.GetClientName(), "SmokeTest", "Verify_Lincoln_County", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Lincoln_County");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Lincoln_County_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Lincoln_County", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(29), Category("SmokeTest")]
        public void Verify_Lyon_County_School_District()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[29]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[29]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[29], _dashboard.GetClientName(), "SmokeTest", "Verify_Lyon_County_School_District", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Lyon_County_School_District");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Lyon_County_School_District_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Lyon_County_School_District", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(30), Category("SmokeTest")]
        public void Verify_MacLellan()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[30]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[30]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[30], _dashboard.GetClientName(), "SmokeTest", "Verify_MacLellan", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_MacLellan");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_MacLellan_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_MacLellan", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(31), Category("SmokeTest")]
        public void Verify_Mitsui_Co()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[31]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[31]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[31], _dashboard.GetClientName(), "SmokeTest", "Verify_Mitsui_Co", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Mitsui_Co");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Mitsui_Co_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Mitsui_Co", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test,Order(32), Category("SmokeTest")]
        public void Verify_Moms_Organic_Market()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[32]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[32]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[32], _dashboard.GetClientName(), "SmokeTest", "Verify_Moms_Organic_Market", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Moms_Organic_Market");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Moms_Organic_Market");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Moms_Organic_Market", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(33), Category("SmokeTest")]
        public void Verify_Mountville_Mills()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[33]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[33]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[33], _dashboard.GetClientName(), "SmokeTest", "Verify_Mountville_Mills", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Mountville_Mills");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Mountville_Mills_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Mountville_Mills", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(34), Category("SmokeTest")]
        public void Verify_MS_Demo()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[34]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[34]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[34], _dashboard.GetClientName(), "SmokeTest", "Verify_MS_Demo", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_MS_Demo");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_MS_Demo_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_MS_Demo", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test,Order(35), Category("SmokeTest")]
        public void Verify_New_Vista()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[35]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[35]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[35], _dashboard.GetClientName(), "SmokeTest", "Verify_New_Vista", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_New_Vista");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_New_Vista_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_New_Vista", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(36), Category("SmokeTest")]
        public void Verify_Onslow_County()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[36]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[36]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[36], _dashboard.GetClientName(), "SmokeTest", "Verify_Onslow_County", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Onslow_County");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Onslow_County_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Onslow_County", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(37), Category("SmokeTest")]
        public void Verify_Price_Chopper_Supermarkets()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[37]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[37]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[37], _dashboard.GetClientName(), "SmokeTest", "Verify_Price_Chopper_Supermarkets", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Price_Chopper_Supermarkets");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Price_Chopper_Supermarkets_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Price_Chopper_Supermarkets", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(38), Category("SmokeTest")]
        public void Verify_Pricechopper_Beta()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[38]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[38]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[38], _dashboard.GetClientName(), "SmokeTest", "Verify_Pricechopper_Beta", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Pricechopper_Beta");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Pricechopper_Beta_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Pricechopper_Beta", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(39) ,Category("SmokeTest")]
        public void Verify_Prince_George_County()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[39]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[39]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[39], _dashboard.GetClientName(), "SmokeTest", "Verify_Prince_George_County", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Prince_George_County");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Prince_George_County_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Prince_George_County", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(40), Category("SmokeTest")]
        public void Verify_RJ_Corman()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[40]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[40]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[40], _dashboard.GetClientName(), "SmokeTest", "Verify_RJ_Corman", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_RJ_Corman");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_RJ_Corman_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_RJ_Corman", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        
        [Test, Order(41), Category("SmokeTest")]
        public void Verify_RogersGray()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[41]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[41]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[41], _dashboard.GetClientName(), "SmokeTest", "Verify_RogersGray", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Verify_RogersGray");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Prince_George_County_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_RogersGray", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(42), Category("SmokeTest")]
        public void Verify_Rowan_County()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[42]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[42]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[42], _dashboard.GetClientName(), "SmokeTest", "Verify_Rowan_County", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Rowan_County");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Rowan_County_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Rowan_County", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(43), Category("SmokeTest")]
        public void Verify_Rue_Gilt_Groupe()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[43]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[43]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[43], _dashboard.GetClientName(), "SmokeTest", "Verify_Rue_Gilt_Groupe", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Rue_Gilt_Groupe");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Rue_Gilt_Groupe");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Showa_Glove", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(44), Category("SmokeTest")]
        public void Verify_Showa_Glove()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[44]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[44]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[44], _dashboard.GetClientName(), "SmokeTest", "Verify_Showa_Glove", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Showa_Glove");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Showa_Glove_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Showa_Glove", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(45), Category("SmokeTest")]
        public void Verify_Universal_Health_Services()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[45]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[45]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[45], _dashboard.GetClientName(), "SmokeTest", "Verify_Universal_Health_Services", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Universal_Health_Services");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Universal_Health_Services_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Universal_Health_Services", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(46), Category("SmokeTest")]
        public void Verify_University_of_Pikeville()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[46]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[46]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[46], _dashboard.GetClientName(), "SmokeTest", "Verify_University_of_Pikeville", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_University_of_Pikeville");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_University_of_Pikeville_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_University_of_Pikeville", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(47), Category("SmokeTest")]
        public void Verify_Wayne_County()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[47]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[47]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[47], _dashboard.GetClientName(), "SmokeTest", "Verify_Wayne_County", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Wayne_County");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Wayne_County_shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Wayne_County", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test,Order(48), Category("SmokeTest")]
        public void Verify_Z5_Demo_B()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[48]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[48]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[48], _dashboard.GetClientName(), "SmokeTest", "Verify_Z5_Demo_B", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Z5_Demo_B");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Z5_Demo_B_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Z5_Demo_B", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test,Order(49), Category("SmokeTest")]
        public void Verify_Z5_Demo_C()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[49]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[49]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[49], _dashboard.GetClientName(), "SmokeTest", "Verify_Z5_Demo_C", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Z5_Demo_C");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Z5_Demo_C_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Z5_Demo_C", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(50), Category("SmokeTest")]
        public void Verify_Z5_Demo_D()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[50]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[50]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[50], _dashboard.GetClientName(), "SmokeTest", "Verify_Z5_Demo_D_County", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Verify_Prince_George_County");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Z5_Demo_D_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Z5_Demo_D", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        [Test, Order(51), Category("SmokeTest")]
        public void Verify_Z5_Demo_E()
        {
            try
            {
                if (Browser.IsElementPresent(How.CssSelector, _setClientPage.SelectClientDropdownCssSelector))
                {
                    _setClient.SelectClient(JsonDataReader.Data["clientList"].Split(';')[51]);
                    Console.WriteLine("Read Cilent Name from json file");
                    _dashboard.DashboardPageLoad();
                    Console.WriteLine("Load Dashboard Page");
                    Assert.AreEqual(_dashboard.GetClientName(), JsonDataReader.Data["clientList"].Split(';')[51]);
                    Console.WriteLine("Verify Cilent: " + _dashboard.GetClientName());
                    _saveToCsv.SaveTestCase(JsonDataReader.Data["clientList"].Split(';')[51], _dashboard.GetClientName(), "SmokeTest", "Verify_Z5_Demo_E", "Expected value should be equal to actual value");
                    Console.WriteLine("End Test case Verify_Z5_Demo_E");

                }
            }
            catch (Exception ex)
            {
                Browser.ScreenShot("Verify_Prince_George_County_Shot");
                if (!ex.Message.Contains("Expected"))
                    _saveToCsv.SaveTestCase("Error", "Error", "SmokeTest", "Verify_Prince_George_County", "Exception occured:  Please verify manually");
                Console.Out.WriteLine(ex);
            }
            finally
            {
                Browser.Open(JsonDataReader.Data["setClientUrl"]);
            }
        }
        
        #endregion
    }
}

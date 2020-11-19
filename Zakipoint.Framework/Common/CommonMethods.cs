using System;
using System.Net;
using System.Collections.Generic;
using Zakipoint.Framework.Driver;
using System.IO;
using OpenQA.Selenium.Support.PageObjects;
using System.Xml.Serialization;
using System.Xml;

namespace Zakipoint.Framework.Common
{
    public static class CommonMethods
    {

        #region Private Properties

        #endregion


        #region Public Properties

        public static List<string> GetAllUrls()
        {
            var links = Browser.FindElements(How.TagName, "a");
            List<string> urls = new List<string>();
            foreach (var link in links)
            {
                string url = link.GetAttribute("href");
                urls.Add(url);
            }
            return urls;
        }

        public static List<string> CheckBrokenLinks(List<string> urls)
        {
            List<string> brokenlinks = new List<string>();
            try
            {
                foreach (var url in urls)
                {
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                    request.AllowAutoRedirect = true;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine("\r\nResponse Status Code is OK and StatusDescription is: {0}", response.StatusDescription);
                        response.Close();
                    }
                    else
                    {
                        brokenlinks.Add(url);
                    }
                }
            }
            catch (WebException ex)
            {
                var errorResponse = (HttpWebResponse)ex.Response;
                System.Console.WriteLine($"URL: {urls} status is :{errorResponse.StatusCode}");
            }
            return brokenlinks;
        }

        public static bool CheckFileDownloaded(string filename)
        {
            bool exist = false;
            string Path = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Downloads";
            string[] filePaths = Directory.GetFiles(Path);
            foreach (string p in filePaths)
            {
                if (p.Contains(filename))
                {
                    FileInfo thisFile = new FileInfo(p);
                    //Check the file that are downloaded in the last 3 minutes
                    if (thisFile.LastWriteTime.ToShortTimeString() == DateTime.Now.ToShortTimeString() ||
                    thisFile.LastWriteTime.AddMinutes(1).ToShortTimeString() == DateTime.Now.ToShortTimeString() ||
                    thisFile.LastWriteTime.AddMinutes(2).ToShortTimeString() == DateTime.Now.ToShortTimeString() ||
                    thisFile.LastWriteTime.AddMinutes(3).ToShortTimeString() == DateTime.Now.ToShortTimeString())
                        exist = true;
                    File.Delete(p);
                    break;
                }
            }
            return exist;
        }

        public static void GenereatetUniqueRndNumbers(int minValue, int maxValue)
        {
            int range = maxValue - minValue;
            var rangeNumbersArr = new int[range + 1];
            var LstRndNumbers = new List<int>();
            for (var count = 0; count < rangeNumbersArr.Length; count++)
            {
                rangeNumbersArr[count] = minValue + count;
                LstRndNumbers.Add(rangeNumbersArr[count]);
            }
        }

        public static string RemoveComma(string value)
        {
            return value.Replace(",", "").Trim();
        }
        public static decimal CurrencyIntermOfThousandWithRoudValue(decimal value)
        {

            return Math.Round(value / 1000, 0);
        }
        public static string[] SplitByNewLine(string textLine)
        {

            return textLine.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }
        public static string[] SplitByString(string SplitFor, string SplitBy)
        {

            return SplitFor.Split(new string[] { SplitBy }, StringSplitOptions.None);
        }
        public static decimal Percentages(decimal CurrentYearvale, decimal LastYearvale)
        {
            decimal ChangePercentage = ((LastYearvale - CurrentYearvale) / LastYearvale) * 100;
            return ChangePercentage;
        }
        public static string ObjectToXml<T>(T obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.Formatting = Formatting.Indented;
            xmlSerializer.Serialize(xmlWriter, obj);
            return stringWriter.ToString();
        }

        public static List<string> GetTableValuesListByRow(How rowDetailsLocator, string rowDetailsValue, int row = 1)
        {
            List<string> valueList = new List<string>();
            var rowvalue = Browser.FindElements(rowDetailsLocator,string.Format(rowDetailsValue, row));
            foreach (var value in rowvalue)
            {
                if (value.Text != "")
                {
                    valueList.Add(value.Text);
                }
            }
            return valueList;
        }

        public static List<List<string>> GetTableValues(How RowLocator, string RowValue, How rowDetailsLocator, string rowDetailsValue)
        {
            var tableDetails = new List<List<string>>();
            int rowCount = Browser.FindElements(RowLocator, RowValue).Count;
            for (int i = 1; i <= rowCount; i++)
            {
                var rowDetails = GetTableValuesListByRow(rowDetailsLocator, rowDetailsValue, i);
                tableDetails.Add(rowDetails);
            }
            return tableDetails;
        }

        #endregion
    }
}

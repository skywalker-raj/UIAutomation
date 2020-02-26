using System;
using System.Net;
using System.Collections.Generic;
using Zakipoint.Framework.Driver;
using System.IO;
using OpenQA.Selenium.Support.PageObjects;

namespace Zakipoint.Framework.Common
{
    class CommonMethods
    {

        #region Private Properties

        #endregion

        #region Constructor

        public CommonMethods()
        {
        }

        #endregion

        #region Public Properties

        public List<string> GetAllUrls()
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

        public List<string> CheckBrokenLinks(List<string> urls)
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
        #endregion
    }
}

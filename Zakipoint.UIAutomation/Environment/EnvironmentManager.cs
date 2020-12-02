using System;
using System.Collections.Generic;
using Zakipoint.Framework.Driver;
namespace Zakipoint.UIAutomation.Environment
{
    [Serializable]
    public class EnvironmentManager
    {
        #region PRIVATE FIELDS

        private static readonly EnvironmentManager instance = new EnvironmentManager();
        public static IList<int> LstRndNumbers;
        public static Random MRandom = new Random();
        public static int MNewRandomNumber;
        public static string Username;

        #endregion

        #region PUBIC PROPERTIES

        public static EnvironmentManager Instance
        {
            get { return instance; }
        }
        public static string ScreenshotFileName
        {
            get { return Browser.GetScreenshotFilename(); }
        }

        #endregion

        #region PUBLIC METHODS

        public static void CaptureScreenShot(string testName)
        {
            Browser.CaptureScreenShot(testName);
        }
        public static void CaptureScreenShot(string dirPath, string testName)
        {
            Browser.CaptureScreenShot(dirPath, testName);
        }
        public static void PageRefresh()
        {
            Browser.Reload();
        }
        /// <summary>
        /// Genereate unique random numbers
        /// </summary>
        /// <param name="minValue">minimum value</param>
        /// <param name="maxValue">maximum value</param>
        public static void GenereatetUniqueRndNumbers(int minValue, int maxValue)
        {
            int range = maxValue - minValue;
            var rangeNumbersArr = new int[range + 1];
            LstRndNumbers = new List<int>();
            for (var count = 0; count < rangeNumbersArr.Length; count++)
            {
                rangeNumbersArr[count] = minValue + count;
                LstRndNumbers.Add(rangeNumbersArr[count]);
            }
        }
        /// <summary>
        /// Get a new random number
        /// </summary>
        /// <returns></returns>
        public static int NewRandomNumber()
        {
            if (LstRndNumbers.Count > 0)
            {
                // Select random number from list
                int index = MRandom.Next(0, LstRndNumbers.Count);
                MNewRandomNumber = LstRndNumbers[index];
                // Remove selected number from Remaining Numbers List
                LstRndNumbers.RemoveAt(index);
                return MNewRandomNumber;
            }
            throw new InvalidOperationException("All numbers are used");
        }

        #endregion
    }
}
using System;
using System.IO;
using System.Text;
namespace Zakipoint.UIAutomation.Common
{
    public class TestCase
    {
        public string PageName { get; set; }
        public string MethodName { get; set; }
        public string ExpectedResult { get; set; }
        public string ActualResult { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }
    public class SaveToCSV
    {
        TestCase _testCase;
        public SaveToCSV()
        {
            _testCase = new TestCase();
        }
        public void SaveTestCase(string ExpectedVale, string ActualValue, string _PageName, string _MethodName, string _Remarks)
        {
            _testCase.PageName = _PageName;
            _testCase.MethodName = _MethodName;
            _testCase.ExpectedResult = ExpectedVale.Replace(","," ");
            _testCase.ActualResult = ActualValue.Replace(",", " ");
            if (ExpectedVale == "Error" && ActualValue == "Error")
            {
                _testCase.Status = "Unknown";
            }
            else
            {
                _testCase.Status = ExpectedVale == ActualValue ? "Pass" : "Fail";
                _testCase.Remarks = _Remarks;
            }
            _testCase.Remarks = _Remarks;
            WriteCSVFile(_testCase);
        }
        public void WriteCSVFile(TestCase data)
        {
            string date = DateTime.Now.ToString("MM-dd-yyyy");
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = baseDir + "../../../CSVFile/";
            string fileName = filePath + "Testcase_" +data.PageName+ "_" + date + ".csv";
            FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            StringBuilder str = new StringBuilder();
            if (new FileInfo(fileName).Length == 0)
            {
                // CVS Header
                str.Append("Page Name");
                str.Append(",\t");
                str.Append("Method Name");
                str.Append(",\t");
                str.Append("Expected Result");
                str.Append(",\t");
                str.Append("Actual Result");
                str.Append(",\t");
                str.Append("Status");
                str.Append(",\t");
                str.Append("Remarks");
                str.Append("\r\n");
            }
            // CSV Data
            str.Append(data.PageName);
            str.Append(",\t");
            str.Append(data.MethodName);
            str.Append(",\t");
            str.Append(data.ExpectedResult);
            str.Append(",\t");
            str.Append(data.ActualResult);
            str.Append(",\t");
            str.Append(data.Status);
            str.Append(",\t");
            str.Append(data.Remarks);
            sw.WriteLine(str);
            sw.Flush();
            sw.Close();
            fs.Close();
        }
    }
}
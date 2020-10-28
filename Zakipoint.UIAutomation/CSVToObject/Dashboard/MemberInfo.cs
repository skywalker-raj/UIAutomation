using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace Zakipoint.UIAutomation.CSVToObject.Dashboard
{
    public class MemberInfo
    {
        public string category_name { get; set; }
        public string category_value { get; set; }
        public MemberInfo FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            MemberInfo dailyValues = new MemberInfo();
            dailyValues.category_name = values[0];
            dailyValues.category_value = values[1];
            return dailyValues;
        }
        public List<MemberInfo> CSVToObject()
        {
            var GeneralInfoList = File.ReadAllLines(@"../CSVFile/GeneralInfo.csv")
                                            .Skip(1)
                                            .Select(v => FromCsv(v))
                                            .ToList();
            return GeneralInfoList;
        }
        public string ExpectedActiveMember()
        {
            string ExpectedActiveMember = CSVToObject().Where(a => a.category_name.Contains("Current Member Count")).Select(a => a.category_value).FirstOrDefault().Trim();
            return ExpectedActiveMember;
        }
        public string ExpectedActiveEmployee()
        {
            string ExpectedActiveEmployee = CSVToObject().Where(a => a.category_name.Contains("Current Employee Count")).Select(a => a.category_value).FirstOrDefault().Trim();
            return ExpectedActiveEmployee;
        }
    }
}
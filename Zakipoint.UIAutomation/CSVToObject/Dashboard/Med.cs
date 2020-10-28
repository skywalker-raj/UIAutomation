using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Zakipoint.UIAutomation.CSVToObject.Dashboard
{
    public class Med
    {
        public string current_processing_paid_dt { get; set; }
        public string current_processing_claim_count { get; set; }
        public string current_processing_rev_paid_amt { get; set; }
        public string previous_processing_paid_dt { get; set; }
        public string previous_processing_claim_count { get; set; }
        public string previous_processing_paid_amt { get; set; }
        public string Claim_Count_Percent_Change { get; set; }
        public string Paid_Amount_Percent_Change { get; set; }
        public List<Med> CSVToObject()
        {
            var Lists = File.ReadAllLines(@"../CSVFile/Med.csv")
                                            .Skip(1)
                                            .Select(v => FromCsv(v))
                                          .ToList();
            return Lists;
        }
        private Med FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Med dailyValues = new Med();
            dailyValues.current_processing_paid_dt = values[0];
            dailyValues.current_processing_claim_count = values[1];
            dailyValues.current_processing_rev_paid_amt = values[2].Replace("$", "");
            dailyValues.previous_processing_paid_dt = values[3];
            dailyValues.previous_processing_claim_count = values[4];
            dailyValues.previous_processing_paid_amt = values[5];
            dailyValues.Claim_Count_Percent_Change = values[6];
            dailyValues.Paid_Amount_Percent_Change = values[7];
            return dailyValues;
        }
        public List<Med> DsipalyCurrentReportList(DateTime StartDate, DateTime EndDate)
        {
            var lists = CSVToObject().Where(a => Convert.ToDateTime(a.current_processing_paid_dt) >= StartDate && Convert.ToDateTime(a.current_processing_paid_dt) <= EndDate).ToList();
            return lists;
        }
        public Decimal TolalMedicalSpend(DateTime StartDate, DateTime EndDate)
        {
            var lists = DsipalyCurrentReportList(StartDate, EndDate);
            Decimal totalMedicalSend = 0;
            foreach (var item in lists)
            {
                totalMedicalSend = totalMedicalSend + Convert.ToDecimal(item.current_processing_rev_paid_amt);
            }
            return totalMedicalSend;
        }
    }
}
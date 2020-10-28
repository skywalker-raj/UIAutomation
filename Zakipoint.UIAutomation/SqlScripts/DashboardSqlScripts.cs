using Zakipoint.UIAutomation.Model;

namespace Zakipoint.UIAutomation.SqlScripts
{
    public class DashboardSqlScripts
    {
        public string GetAppGroupsValueList = @"select * from app_groups";

        public string Expected_Total_Active_Employee() {  
            return string.Format(@"SELECT
            COUNT(DISTINCT member_id)
            FROM member_summary_encr_{0}
            WHERE p1_exists_flag = 1 And p1_active_flag=1 
            AND mbr_relationship_class = 'Employee'", CommonObject.DefaultClientSuffix);
        }
        public string Expected_Total_Active_Member() {  
            return string.Format(@"SELECT
            COUNT(DISTINCT member_id)
            FROM member_summary_encr_{0}
            WHERE p1_exists_flag = 1 And p1_active_flag=1", CommonObject.DefaultClientSuffix);
    }

        public string Expected_Total_Employee() { 
            return string.Format(@"SELECT
            COUNT(DISTINCT member_id)
            FROM member_summary_encr_{0}
            WHERE p1_exists_flag = 1 
            AND mbr_relationship_class = 'Employee'", CommonObject.DefaultClientSuffix);
    }

        public string Expected_Total_Member() {
            return string.Format(@"SELECT
            COUNT(DISTINCT member_id)
            FROM member_summary_encr_{0}
            WHERE p1_exists_flag = 1", CommonObject.DefaultClientSuffix);
        }

        public string Expected_Total_Medical_Pharmacy_Sepnd(int period)
        {
            return string.Format(@"SELECT
            medical_paid_amount  , pharmacy_paid_amount 
            FROM (SELECT
            SUM(rev_paid_amt) medical_paid_amount
            FROM member_by_paid_medical_by_month_{0} member
            WHERE member.period = {1}
            AND member.group_id = '{0}') a
            JOIN (SELECT
            SUM(rev_paid_amt) pharmacy_paid_amount
            FROM member_by_paid_rx_by_month_{0} member
            WHERE member.period = {1}
            AND member.group_id = '{0}') b", CommonObject.DefaultClientSuffix, period);
        }

        public string Expected_Total_Active_Medical_Pharmacy_Spend(int period)
        {
            return string.Format(@"SELECT
            medical_paid_amount  , pharmacy_paid_amount 
            FROM (SELECT
            SUM(rev_paid_amt) medical_paid_amount
            FROM member_by_paid_medical_by_month_{0} member
            WHERE member.period = {1}
            AND member.group_id = '{0}' 
            AND member.active_flag=TRUE) a
            JOIN (SELECT
            SUM(rev_paid_amt) pharmacy_paid_amount
            FROM member_by_paid_rx_by_month_{0} member
            WHERE member.period = {1}
            AND member.group_id = '{0}'
            AND member.active_flag = TRUE) b", CommonObject.DefaultClientSuffix, period);
        }

        public string Medical_PMPM(string active_flag, int period)
        {
            string SubString1;
            if (active_flag.ToLower() == "all")
            {
                SubString1 = " AND 1=1";
            }
            else
            {
                SubString1 = "AND active_flag = 1";
            }
            return string.Format(@"SELECT
            (SELECT
            SUM(rev_paid_amt) medical_paid_amount
            FROM member_by_paid_medical_by_month_{0} member
            WHERE member.period = {2}
            AND member.group_id = '{0}' {1}) / (SELECT
            SUM(membermonth)
            FROM member_by_mm_by_month_{0}
            WHERE period = {2} {1});", CommonObject.DefaultClientSuffix, SubString1, period);
        }

        public string Pharmacy_PMPM(string active_flag, int period)
        {
            string SubString1;
            if (active_flag.ToLower() == "all")
            {
                SubString1 = " AND 1=1";
            }
            else
            {
                SubString1 = "AND active_flag = 1";
            }
            return string.Format(@"SELECT
            (SELECT
             SUM(rev_paid_amt) medical_paid_amount
            FROM member_by_paid_rx_by_month_{0} member
            WHERE member.period = {2}
            AND member.group_id = '{0}' {1}) / (SELECT
            SUM(membermonth)
            FROM member_by_mm_by_month_{0}
            WHERE period = {2}  {1});", CommonObject.DefaultClientSuffix, SubString1, period);
        }

        public string Top_Condition_By_Total_Spend(string StartDate, string EndDate)
        {
            return string.Format(@" SELECT
            disease_name,
             round(SUM(CASE WHEN member.exists_in_p1 = TRUE THEN member.p1_total_paid ELSE 0 END)/1000,0) Spend,
            round(SUM(CASE WHEN member.exists_in_p1 = TRUE THEN member.p1_total_paid ELSE 0 END) *100/
            (select sum(t.p1_total_paid) from tbl_member_paid_and_risk_summary_{0} t where t.exists_in_p1= TRUE),0) P_spend,
            ROUND((SUM(CASE WHEN member.exists_in_p1 = TRUE THEN member.p1_total_paid ELSE 0 END) 
            -SUM(CASE WHEN member.exists_in_p2 = TRUE THEN member.p2_total_paid ELSE 0 END))*100
            /SUM(CASE WHEN member.exists_in_p2 = TRUE THEN member.p2_total_paid ELSE 0 END),2) as P_chnage,
            COUNT(member.member_id) Members
            FROM tbl_member_paid_and_risk_summary_{0} member 
            JOIN (SELECT DISTINCT
                group_id,
                member_id,
                t1.disease_name
                FROM chronic_conditions_by_member_{0} t1
                WHERE 1 = 1
                AND t1.group_id = '{0}'
                AND DATE_FORMAT(t1.most_recent_date, '%Y%m') BETWEEN '{1}' AND '{2}') disease
             ON member.group_id = disease.group_id
             AND member.member_id = disease.member_id
             WHERE 1 = 1
             AND member.group_id = '{0}'
            group by disease_name
            order by 
            SUM(CASE WHEN member.exists_in_p1 = TRUE THEN member.p1_total_paid ELSE 0 END) 
            desc limit 10;", CommonObject.DefaultClientSuffix, StartDate, EndDate);
        }
    }
}
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
             SUM(rev_paid_amt) pharmacy_paid_amount
            FROM member_by_paid_rx_by_month_{0} member
            WHERE member.period = {2}
            AND member.group_id = '{0}' {1}) / (SELECT
            SUM(membermonth)
            FROM member_by_mm_by_month_{0}
            WHERE period = {2}  {1});", CommonObject.DefaultClientSuffix, SubString1, period);
        }

        public string Top_Condition_By_Total_Spend(string StartDate, string EndDate, string memberStatus)
        {
            if (memberStatus.ToLower() == "all")
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
                        AND DATE_FORMAT(t1.most_recent_date, '%Y%m') BETWEEN '{1}' AND '{2}'
                    ) disease
                    ON member.group_id = disease.group_id
                    AND member.member_id = disease.member_id
                    WHERE 1 = 1
                    AND member.group_id = '{0}'
                    group by disease_name
                    order by 
                    SUM(CASE WHEN member.exists_in_p1 = TRUE THEN member.p1_total_paid ELSE 0 END) 
                    desc limit 10;", CommonObject.DefaultClientSuffix, StartDate, EndDate);
            } 
            else
            {
                return  string.Format(@"SELECT
                    disease_name,
                    ROUND(SUM(CASE WHEN (member.exists_in_p1 = TRUE AND
                    member.p1_active_flag = TRUE) THEN member.p1_total_paid ELSE 0 END) / 1000, 0) Spend,
                    ROUND(SUM(CASE WHEN (member.exists_in_p1 = TRUE AND
                    member.p1_active_flag = TRUE) THEN member.p1_total_paid ELSE 0 END) * 100 / (SELECT
                    SUM(t.p1_total_paid)
                    FROM tbl_member_paid_and_risk_summary_{0} t
                    WHERE t.exists_in_p1 = TRUE
                    AND t.p1_active_flag = TRUE), 0) P_spend,
                    ROUND((SUM(CASE WHEN (member.exists_in_p1 = TRUE AND
                    member.p1_active_flag = TRUE) THEN member.p1_total_paid ELSE 0 END)
                    - SUM(CASE WHEN (member.exists_in_p2 = TRUE AND
                    member.p2_active_flag = TRUE) THEN member.p2_total_paid ELSE 0 END)) * 100
                    / SUM(CASE WHEN (member.exists_in_p2 = TRUE AND
                    member.p2_active_flag = TRUE) THEN member.p2_total_paid ELSE 0 END), 2) AS P_chnage,
                    COUNT(CASE WHEN member.p1_active_flag = TRUE THEN member.member_id ELSE NULL END) Members
                    FROM tbl_member_paid_and_risk_summary_{0} member
                    JOIN (SELECT DISTINCT
                    group_id,
                        member_id,
                        t1.disease_name
                        FROM chronic_conditions_by_member_{0} t1
                        WHERE 1 = 1
                        AND t1.group_id = '{0}'
                        AND DATE_FORMAT(t1.most_recent_date, '%Y%m') BETWEEN '{1}' AND '{2}'
                    ) disease
                    ON member.group_id = disease.group_id
                    AND member.member_id = disease.member_id
                    WHERE 1 = 1
                    AND member.group_id = '{0}'
                    GROUP BY disease_name
                    ORDER BY SUM(CASE WHEN (member.exists_in_p1 = TRUE AND
                    member.p1_active_flag = TRUE) THEN member.p1_total_paid ELSE 0 END)
                    DESC LIMIT 10;", CommonObject.DefaultClientSuffix, StartDate, EndDate);
            }
        }
    
        public string Top_Service_By_Total_Spend(string memberStatus)
        {
          
            if (memberStatus.ToLower() == "all")
                memberStatus = " 1=1 ";
            else
                memberStatus = " active_flag=1 ";

            return string.Format(@"SELECT
            metric_id Services,
            ROUND(paid_amount / 1000, 1) Spend,
            member_count Members,
            ROUND((event_Count * 12 * 1000) / member_month, 2) UtilizationPerThousand,
            ROUND(paid_amount / member_month, 0) PMPM
            FROM (SELECT
            metric_id,
            SUM(paid_amount) paid_amount,
            COUNT(DISTINCT member_id) member_count,
            SUM(unit_count) event_Count,
            (SELECT
             SUM(membermonth)
            FROM member_by_mm_by_month_{0} 
            WHERE period = 1   and group_id = '{0}' and{1} ) member_month
            FROM member_utilization_metrics_summary_{0} 
            WHERE metric_id IN ('INPATIENT', 'OFFICEVISIT', 'OUTPATIENT', 'ER')
            AND group_id = '{0}'
            AND  period = 1 and{1}
            GROUP BY metric_id) AS A
            ORDER BY FIELD(metric_id, 'INPATIENT', 'OFFICEVISIT', 'OUTPATIENT', 'ER')", CommonObject.DefaultClientSuffix, memberStatus);
        }

        public string Cost_Matrix(string memberStatus)
        {
           
            if (memberStatus.ToLower() == "all")
                memberStatus = " 1=1 ";
            else
                memberStatus = " active_flag=1 ";

            return string.Format(@"SELECT  a.Cost_Categories,round( (a.spend/(SELECT SUM(p1_total_paid) FROM member_summary_encr_{0} where p1_exists_flag = 1 and group_id='{0}' and{1}))*100 ,0)as 'P_Spend'  ,  round(a.spend/1000,0) as spend ,a.Members from (
           SELECT SUM(p1_total_paid) spend  , 'No Cost'  AS Cost_Categories, count(1) AS Members FROM member_summary_encr_{0}  WHERE p1_total_paid=0  and p1_exists_flag = 1 and group_id='{0}'and{1}
           UNION ALL 
           SELECT sum(p1_total_paid) AS spend , 'Less than $500' AS Cost_Categories, COUNT(1) AS Members  FROM member_summary_encr_{0}  WHERE p1_total_paid > 0 and p1_total_paid <= 500  and p1_exists_flag = 1 and group_id='{0}'and{1}
           UNION ALL 
           SELECT SUM(p1_total_paid) AS spend,  '$500 to $5k' AS Cost_Categories, COUNT(1) AS Members  FROM member_summary_encr_{0}  WHERE p1_total_paid > 500 and p1_total_paid <= 5000  and p1_exists_flag = 1 and group_id='{0}'and{1}
           UNION ALL
           SELECT SUM(p1_total_paid) AS spend , '$5k to $50k' AS Cost_Categories, COUNT(1) AS Members  FROM member_summary_encr_{0}  WHERE p1_total_paid > 5000 and p1_total_paid <= 50000 and p1_exists_flag = 1 and group_id='{0}' and{1}
           UNION ALL
           SELECT SUM(p1_total_paid) AS spend, '$50k to $75k' AS Cost_Categories, COUNT(1) AS Members  FROM member_summary_encr_{0}  WHERE p1_total_paid > 50000 and p1_total_paid <= 75000 and p1_exists_flag = 1 and group_id='{0}' and{1}
           UNION ALL
           SELECT SUM(p1_total_paid) AS spend, '$75k to $100k' AS Cost_Categories, COUNT(1) AS Members  FROM member_summary_encr_{0}  WHERE p1_total_paid > 75000 and p1_total_paid <= 100000 and p1_exists_flag = 1 and group_id='{0}'and{1}
           UNION ALL
           SELECT SUM(p1_total_paid) AS spend, '$100k to $200k' AS Cost_Categories, COUNT(1) AS Members  FROM member_summary_encr_{0}  WHERE p1_total_paid > 100000 and p1_total_paid <= 200000 and p1_exists_flag = 1  and group_id='{0}'and{1}
           UNION ALL
           SELECT SUM(p1_total_paid) AS spend, 'More than $200k' AS Cost_Categories, COUNT(1) AS Members  FROM member_summary_encr_{0}  WHERE p1_total_paid > 200000  and p1_exists_flag = 1 and group_id='{0}' and{1}) AS A ", CommonObject.DefaultClientSuffix,memberStatus);
        }

        public string Prospective_Population_Risk_Stratification(string memberStatus)
        {
         
            if (memberStatus.ToLower() == "all")
            {
              return string.Format(@"SELECT
            RiskType 'Risk_Type',
            ROUND(Spend / 1000, 1) Spend,
            ROUND((Spend / Total_spend) * 100, 1) 'Percentages_Spend',
            Members,
            ROUND((Members / total_member) * 100, 1) 'Percentages_Member',
            ROUND((Spend / member_month), 1) PMPM
            FROM (SELECT
            (SELECT
            SUM(p1_total_paid)
            FROM tbl_member_paid_and_risk_summary_{0}
            WHERE exists_in_p1 = TRUE
            AND group_id = '{0}') Total_spend,
            CASE WHEN p1_risk_bucket = '0' THEN 'Low' ELSE p1_risk_bucket END 'RiskType',
            SUM(p1_total_paid) Spend,
            (SELECT
            COUNT(DISTINCT member_id)
            FROM tbl_member_paid_and_risk_summary_{0}
            WHERE exists_in_p1 = TRUE 
            AND group_id = '{0}') total_member,
            COUNT(DISTINCT member_id) Members,
            (SELECT
            SUM(membermonth)
            FROM member_by_mm_by_month_{0}
            WHERE period = 1
            AND group_id = '{0}' ) member_month
            FROM tbl_member_paid_and_risk_summary_{0}
            WHERE exists_in_p1 = TRUE 
            AND group_id = '{0}'
            GROUP BY CASE WHEN p1_risk_bucket = '0' THEN 'Low' ELSE p1_risk_bucket END) A
            ORDER BY FIELD(RiskType, 'High', 'Medium', 'Normal', 'Low') ", CommonObject.DefaultClientSuffix);
            }
            else
            {
             return string.Format(@"SELECT
            RiskType 'Risk_Type',
            ROUND(Spend / 1000, 1) Spend,
            ROUND((Spend / Total_spend) * 100, 1) 'Percentages_Spend',
            Members,
            ROUND((Members / total_member) * 100, 1) 'Percentages_Member',
            ROUND((Spend / member_month), 1) PMPM
            FROM (SELECT
            (SELECT
            SUM(p1_total_paid)
            FROM tbl_member_paid_and_risk_summary_{0}
            WHERE exists_in_p1 = TRUE and P1_active_flag=TRUE
            AND group_id = '{0}') Total_spend,
            CASE WHEN p1_risk_bucket = '0' THEN 'Low' ELSE p1_risk_bucket END 'RiskType',
            SUM(p1_total_paid) Spend,
            (SELECT
            COUNT(DISTINCT member_id)
            FROM tbl_member_paid_and_risk_summary_{0}
            WHERE exists_in_p1 = TRUE and P1_active_flag=TRUE
            AND group_id = '{0}') total_member,
            COUNT(DISTINCT member_id) Members,
            (SELECT
            SUM(membermonth)
            FROM member_by_mm_by_month_{0}
            WHERE period = 1
            AND group_id = '{0}' and active_flag=TRUE) member_month
            FROM tbl_member_paid_and_risk_summary_{0}
            WHERE exists_in_p1 = TRUE and P1_active_flag=TRUE
            AND group_id = '{0}'
            GROUP BY CASE WHEN p1_risk_bucket = '0' THEN 'Low' ELSE p1_risk_bucket END) A
            ORDER BY FIELD(RiskType, 'High', 'Medium', 'Normal', 'Low') ", CommonObject.DefaultClientSuffix);
            }
        }

    }
}
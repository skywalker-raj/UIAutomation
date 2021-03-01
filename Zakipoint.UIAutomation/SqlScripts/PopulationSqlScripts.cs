using System;
using System.Collections.Generic;
using System.Text;
using Zakipoint.UIAutomation.Model;
using static System.String;

namespace Zakipoint.UIAutomation.SqlScripts
{
    class PopulationSqlScripts
    {
        public String  ExpectedAgeDetails(string customStartDate, string customEndDate)
        {
            return Format(@"SELECT
  member.age_band,
  SUM(t1.total_paid) p1_total_paid,
  COUNT(DISTINCT member.int_mbr_id) p1_member_count
FROM (SELECT
    int_mbr_id,
    group_id,
    member.age_band,
    'P1' period
  FROM member_summary_encr_by_month_ruegilt member
  WHERE member.group_id = 'RUEGILT'
  AND member.eff_date BETWEEN '{1}' AND '{2}'
  GROUP BY member.int_mbr_id,
           member.age_band) member
  LEFT JOIN (SELECT DISTINCT
      t1.group_id,
      t1.int_mbr_id,
      'P1' period,
      SUM(t1.total_rev_paid) total_paid
    FROM member_by_paid_by_month_ruegilt t1
    WHERE t1.paid_month BETWEEN '{1}' AND '{2}'
    GROUP BY t1.int_mbr_id) t1
    ON member.int_mbr_id = t1.int_mbr_id
    AND member.group_id = t1.group_id
    AND member.period = t1.period
WHERE 1 = 1
GROUP BY member.age_band", CommonObject.DefaultClientSuffix,customStartDate,customEndDate);
        }

        public string ExpectedAgePmpmDetails()
        {
            return Format(@"SELECT    COALESCE(a.age_band, b.age_band) age_band,  
  COALESCE(a.p1_total_paid, b.p1_total_paid) p1_total_paid,   
  COALESCE(a.p2_total_paid, b.p2_total_paid) p2_total_paid,    COALESCE(a.p3_total_paid, b.p3_total_paid) p3_total_paid, 
  COALESCE(a.p1_member_count, b.p1_member_count) p1_member_count,    COALESCE(a.p2_member_count, b.p2_member_count) p2_member_count,  
  COALESCE(a.p3_member_count, b.p3_member_count) p3_member_count,    COALESCE(p1_mm, 0) p1_mm,    COALESCE(p2_mm, 0) p2_mm,  
  COALESCE(p3_mm, 0) p3_mm  FROM (  SELECT member.age_band,      SUM(CASE WHEN member.period='P1' THEN  t1.total_paid  ELSE 0 END) p1_total_paid, 
  SUM(CASE WHEN member.period='P2' THEN  t1.total_paid  ELSE 0 END) p2_total_paid, 
  SUM(CASE WHEN member.period='P3' THEN  t1.total_paid  ELSE 0 END) p3_total_paid,  
  COUNT(DISTINCT (CASE WHEN member.period='P1' THEN member.int_mbr_id END)) p1_member_count,  
  COUNT(DISTINCT (CASE WHEN member.period='P2' THEN member.int_mbr_id END)) p2_member_count,  
  COUNT(DISTINCT (CASE WHEN member.period='P3' THEN member.int_mbr_id END)) p3_member_count,   
  SUM(CASE WHEN member.period='P1' THEN member.mm ELSE 0 END) p1_mm,      SUM(CASE WHEN member.period='P2' THEN member.mm ELSE 0 END) p2_mm, 
  SUM(CASE WHEN member.period='P3' THEN member.mm ELSE 0 END) p3_mm    FROM (SELECT        int_mbr_id,        group_id, member.age_band, 
  sum(mm) mm,        config.period        FROM member_summary_encr_by_month member 
  INNER JOIN zph_temp.temp_user_config_table_4F3882D2997E4513632A374385E0F223 config  ON 
  member.eff_date = config.analysis_month AND member.group_id = 'RUEGILT'  AND age_band IS NOT NULL 
  GROUP BY member.int_mbr_id,               config.period, member.age_band    ) member  LEFT JOIN (SELECT  
  DISTINCT t1.group_id,    t1.int_mbr_id,    config.period,    SUM( t1.total_paid ) total_paid  FROM member_by_paid_by_month t1
  INNER JOIN zph_temp.temp_user_config_table_4F3882D2997E4513632A374385E0F223 config  ON  t1.paid_month = config.analysis_month 
  AND t1.group_id = 'RUEGILT'  GROUP BY t1.int_mbr_id,  		  config.period  ) t1  ON member.int_mbr_id = t1.int_mbr_id  
  AND member.group_id = t1.group_id  AND member.period = t1.period    WHERE 1 = 1    GROUP BY member.age_band ) a    RIGHT JOIN (SELECT 
  '0-19' age_band,        0 p1_total_paid,        0 p2_total_paid,        0 p3_total_paid,        0 p1_member_count,        0 p2_member_count,   
  0 p3_member_count      UNION      SELECT        '20-39' age_band,        0 p1_total_paid,        0 p2_total_paid,        0 p3_total_paid,    
  0 p1_member_count,        0 p2_member_count,        0 p3_member_count      UNION      SELECT        '40-59' age_band,        0 p1_total_paid,  
  0 p2_total_paid,        0 p3_total_paid,        0 p1_member_count,        0 p2_member_count,        0 p3_member_count      UNION      SELECT   
  '60-64' age_band,        0 p1_total_paid,        0 p2_total_paid,        0 p3_total_paid,        0 p1_member_count,        0 p2_member_count,     
  0 p3_member_count      UNION      SELECT        '65+' age_band,        0 p1_total_paid,        0 p2_total_paid,        0 p3_total_paid,     
  0 p1_member_count,        0 p2_member_count,        0 p3_member_count) b      ON a.age_band = b.age_band 
", CommonObject.DefaultClientSuffix);
        }

        public String ExpectedGenderDetails()
        {
            return Format(@"SELECT
  member.mbr_gender,
  SUM(t1.total_paid) p1_total_paid,
  COUNT(DISTINCT member.int_mbr_id) p1_member_count
FROM (SELECT
    int_mbr_id,
    group_id,
    member.mbr_gender,
    'P1' period
  FROM member_summary_encr_by_month_ruegilt member
  WHERE member.group_id = 'RUEGILT'
 -- AND age_band IS NOT NULL
  AND member.eff_date BETWEEN '202001' AND '202012'
  GROUP BY member.int_mbr_id,
           member.mbr_gender) member
  LEFT JOIN (SELECT DISTINCT
      t1.group_id,
      t1.int_mbr_id,
      'P1' period,
      SUM(t1.total_rev_paid) total_paid
    FROM member_by_paid_by_month_ruegilt t1
    WHERE t1.paid_month BETWEEN '202001' AND '202012'
    GROUP BY t1.int_mbr_id) t1
    ON member.int_mbr_id = t1.int_mbr_id
    AND member.group_id = t1.group_id
    AND member.period = t1.period
WHERE 1 = 1
GROUP BY member.mbr_gender", CommonObject.DefaultClientSuffix);
        }

     
        public string ExpectedRelationDetails()
        {
            return Format(@"", CommonObject.DefaultClientSuffix);
        }

        public string ExpectedPlanDetails()
        {
            return Format(@"", CommonObject.DefaultClientSuffix);
        }

        public string ExpectedGenderPmpmDetails()
        {
            return Format(@"", CommonObject.DefaultClientSuffix);
        }

        public string ExpectedRelationPmpmDetails()
        {
            return Format(@"", CommonObject.DefaultClientSuffix);
        }

        public string ExpectedPlanPmpmDetails()
        {
            return Format(@"", CommonObject.DefaultClientSuffix);
        }
    }
}

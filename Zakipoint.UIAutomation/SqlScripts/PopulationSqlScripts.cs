using System;
using System.Collections.Generic;
using System.Text;
using Zakipoint.UIAutomation.Model;
using static System.String;

namespace Zakipoint.UIAutomation.SqlScripts
{
    class PopulationSqlScripts
    {
        public String ExpectedAgeDetails(string customStartDate, string customEndDate)
        {
            return Format(@"SELECT
  member.age_band,
   round(SUM(t1.total_paid)/1000,1) p1_total_paid,
  COUNT(DISTINCT member.int_mbr_id) p1_member_count
FROM (SELECT
    int_mbr_id,
    group_id,
    member.age_band,
    'P1' period
  FROM member_summary_encr_by_month_{0} member
  WHERE member.group_id = '{0}'
  AND member.eff_date BETWEEN '{1}' AND '{2}'
  GROUP BY member.int_mbr_id,
           member.age_band) member
  LEFT JOIN (SELECT DISTINCT
      t1.group_id,
      t1.int_mbr_id,
      'P1' period,
      SUM(t1.total_rev_paid) total_paid
    FROM member_by_paid_by_month_{0} t1
    WHERE t1.paid_month BETWEEN '{1}' AND '{2}'
    GROUP BY t1.int_mbr_id) t1
    ON member.int_mbr_id = t1.int_mbr_id
    AND member.group_id = t1.group_id
    AND member.period = t1.period
WHERE 1 = 1
GROUP BY member.age_band", CommonObject.DefaultClientSuffix, customStartDate, customEndDate);
        }

        public string ExpectedAgePmpmDetails(string customStartDate, string customEndDate)
        {
            return Format(@"SELECT
  a.age_band age_band,
 round(SUM(a.p1_total_paid)/1000,1) p1_total_paid,
  COUNT(a.p1_member_count) P1_member_count,
 -- SUM(b.mm) member_month,
  SUM(a.p1_total_paid) / SUM(b.mm) pm
FROM (SELECT
    member.age_band,
    member.int_mbr_id,
    SUM(t1.total_paid) p1_total_paid,
    COUNT(DISTINCT member.int_mbr_id) p1_member_count
  FROM (SELECT
      int_mbr_id,
      group_id,
      member.age_band,
      'P1' period
    FROM member_summary_encr_by_month_{0} member
    WHERE member.group_id = '{0}'
    AND age_band IS NOT NULL
    AND member.eff_date BETWEEN '{1}' AND '{2}'
    GROUP BY member.int_mbr_id,
             member.age_band) member
    LEFT JOIN (SELECT DISTINCT
        t1.group_id,
        t1.int_mbr_id,
        'P1' period,
        SUM(t1.total_rev_paid) total_paid
      FROM member_by_paid_by_month_{0} t1
      WHERE t1.paid_month BETWEEN '{1}' AND '{2}'
      GROUP BY t1.int_mbr_id) t1
      ON member.int_mbr_id = t1.int_mbr_id
      AND member.group_id = t1.group_id
      AND member.period = t1.period
  WHERE 1 = 1
  GROUP BY member.age_band,
           member.int_mbr_id) a
  JOIN (SELECT
      age_band,
      int_mbr_id,
      SUM(mm)
 mm
    FROM member_summary_encr_by_month_{0}
    WHERE eff_date BETWEEN '{1}' AND '{2}'
    GROUP BY age_band,
             int_mbr_id) b
    ON a.int_mbr_id = b.int_mbr_id
GROUP BY a.age_band;", CommonObject.DefaultClientSuffix,customStartDate, customEndDate);
        }

        public String ExpectedGenderDetails(string customStartDate, string customEndDate)
        {
            return Format(@"SELECT
  member.mbr_gender,
   round(SUM(t1.total_paid)/1000,1) p1_total_paid,
  COUNT(DISTINCT member.int_mbr_id) p1_member_count
FROM (SELECT
    int_mbr_id,
    group_id,
    member.mbr_gender,
    'P1' period
  FROM member_summary_encr_by_month_{0} member
  WHERE member.group_id = '{0}'
 -- AND age_band IS NOT NULL
  AND member.eff_date BETWEEN '{1}' AND '{2}'
  GROUP BY member.int_mbr_id,
           member.mbr_gender) member
  LEFT JOIN (SELECT DISTINCT
      t1.group_id,
      t1.int_mbr_id,
      'P1' period,
      SUM(t1.total_rev_paid) total_paid
    FROM member_by_paid_by_month_{0} t1
    WHERE t1.paid_month BETWEEN '{1}' AND '{2}'
    GROUP BY t1.int_mbr_id) t1
    ON member.int_mbr_id = t1.int_mbr_id
    AND member.group_id = t1.group_id
    AND member.period = t1.period
WHERE 1 = 1
GROUP BY member.mbr_gender order by FIELD(member.mbr_gender ,'M','F','U')", CommonObject.DefaultClientSuffix ,customStartDate, customEndDate);
        }

     
        public string ExpectedRelationDetails(string customStartDate,string customEndDate)
        {
            return Format(@"SELECT
  member.mbr_relationship_desc,
  round(SUM(t1.total_paid)/1000,1) p1_total_paid,
  COUNT(DISTINCT member.int_mbr_id) p1_member_count
FROM (SELECT
    int_mbr_id,
    group_id,
    member.mbr_relationship_desc,
    'P1' period
  FROM member_summary_encr_by_month_{0} member
  WHERE member.group_id = '{0}'

  AND member.eff_date BETWEEN '{1}' AND '{2}'
  GROUP BY member.int_mbr_id,
           member.mbr_relationship_desc) member
  LEFT JOIN (SELECT DISTINCT
      t1.group_id,
      t1.int_mbr_id,
      'P1' period,
      SUM(t1.total_rev_paid) total_paid
    FROM member_by_paid_by_month_{0} t1
    WHERE t1.paid_month BETWEEN '{1}' AND '{2}'
    GROUP BY t1.int_mbr_id) t1
    ON member.int_mbr_id = t1.int_mbr_id
    AND member.group_id = t1.group_id
    AND member.period = t1.period
WHERE 1 = 1
GROUP BY member.mbr_relationship_desc order by FIELD(member.mbr_relationship_desc ,'Employee','Spouse','Dependent','Others')", CommonObject.DefaultClientSuffix, customStartDate, customEndDate);
        }

        public string ExpectedPlanDetails(string customStartDate, string customEndDate)
        {
            return Format(@"SELECT
  member.plan_desc,
   round(SUM(t1.total_paid)/1000,1) p1_total_paid,
  COUNT(DISTINCT member.int_mbr_id) p1_member_count
FROM (SELECT
    int_mbr_id,
    group_id,
    member.plan_desc,
    'P1' period
  FROM member_summary_encr_by_month_{0} member
  WHERE member.group_id = '{0}'
  AND age_band IS NOT NULL
  AND member.eff_date BETWEEN '{1}' AND '{2}'
  GROUP BY member.int_mbr_id,
           member.plan_desc) member
  LEFT JOIN (SELECT DISTINCT
      t1.group_id,
      t1.int_mbr_id,
      'P1' period,
      SUM(t1.total_rev_paid) total_paid
    FROM member_by_paid_by_month_{0} t1
    WHERE t1.paid_month BETWEEN '{1}' AND '{2}'
    GROUP BY t1.int_mbr_id) t1
    ON member.int_mbr_id = t1.int_mbr_id
    AND member.group_id = t1.group_id
    AND member.period = t1.period
WHERE 1 = 1
GROUP BY plan_desc
order by p1_total_paid desc;", CommonObject.DefaultClientSuffix, customStartDate, customEndDate);
        }

        public string ExpectedGenderPmpmDetails(string customStartDate, String customEndDate)
        {
            return Format(@"SELECT
  a.mbr_gender mbr_gender,
 round( SUM(a.p1_total_paid)/1000,1) p1_total_paid,
  COUNT(a.p1_member_count) P1_member_count,
 -- SUM(b.mm) member_month,
  SUM(a.p1_total_paid) / SUM(b.mm) pm
FROM (SELECT
    member.mbr_gender,
    member.int_mbr_id,
    SUM(t1.total_paid) p1_total_paid,
    COUNT(DISTINCT member.int_mbr_id) p1_member_count
  FROM (SELECT
      int_mbr_id,
      group_id,
      member.mbr_gender,
      'P1' period
    FROM member_summary_encr_by_month_{0} member
    WHERE member.group_id = '{0}'
    AND mbr_gender IS NOT NULL
    AND member.eff_date BETWEEN '{1}' AND '{2}'
    GROUP BY member.int_mbr_id,
             member.mbr_gender) member
    LEFT JOIN (SELECT DISTINCT
        t1.group_id,
        t1.int_mbr_id,
        'P1' period,
        SUM(t1.total_rev_paid) total_paid
      FROM member_by_paid_by_month_{0} t1
      WHERE t1.paid_month BETWEEN '{1}' AND '{2}'
      GROUP BY t1.int_mbr_id) t1
      ON member.int_mbr_id = t1.int_mbr_id
      AND member.group_id = t1.group_id
      AND member.period = t1.period
  WHERE 1 = 1
  GROUP BY member.mbr_gender,
           member.int_mbr_id) a
  JOIN (SELECT
      mbr_gender,
      int_mbr_id,
      SUM(mm)
 mm
    FROM member_summary_encr_by_month_{0}
    WHERE eff_date BETWEEN '{1}' AND '{2}'
    GROUP BY mbr_gender,
             int_mbr_id) b
    ON a.int_mbr_id = b.int_mbr_id
GROUP BY a.mbr_gender order by FIELD(a.mbr_gender ,'M','F','U');", CommonObject.DefaultClientSuffix, customStartDate, customEndDate);
        }

        public string ExpectedRelationPmpmDetails(string customStartDate, string customEndDate)
        {
            return Format(@"SELECT
  a.mbr_relationship_desc mbr_relationship_desc,
 round( SUM(a.p1_total_paid)/1000,1) p1_total_paid,
  COUNT(a.p1_member_count) P1_member_count,
 -- SUM(b.mm) member_month,
  SUM(a.p1_total_paid) / SUM(b.mm) pm
FROM (SELECT
    member.mbr_relationship_desc,
    member.int_mbr_id,
    SUM(t1.total_paid) p1_total_paid,
    COUNT(DISTINCT member.int_mbr_id) p1_member_count
  FROM (SELECT
      int_mbr_id,
      group_id,
      member.mbr_relationship_desc,
      'P1' period
    FROM member_summary_encr_by_month_{0} member
    WHERE member.group_id = '{0}'
    AND mbr_relationship_desc IS NOT NULL
    AND member.eff_date BETWEEN '{1}' AND '{2}'
    GROUP BY member.int_mbr_id,
             member.mbr_relationship_desc) member
    LEFT JOIN (SELECT DISTINCT
        t1.group_id,
        t1.int_mbr_id,
        'P1' period,
        SUM(t1.total_rev_paid) total_paid
      FROM member_by_paid_by_month_{0} t1
      WHERE t1.paid_month BETWEEN '{1}' AND '{2}'
      GROUP BY t1.int_mbr_id) t1
      ON member.int_mbr_id = t1.int_mbr_id
      AND member.group_id = t1.group_id
      AND member.period = t1.period
  WHERE 1 = 1
  GROUP BY member.mbr_relationship_desc,
           member.int_mbr_id) a
  JOIN (SELECT
      mbr_relationship_desc,
      int_mbr_id,
      SUM(mm)
 mm
    FROM member_summary_encr_by_month_{0}
    WHERE eff_date BETWEEN '{1}' AND '{2}'
    GROUP BY mbr_relationship_desc,
             int_mbr_id) b
    ON a.int_mbr_id = b.int_mbr_id
GROUP BY a.mbr_relationship_desc order by FIELD(a.mbr_relationship_desc ,'Employee','Spouse','Dependent','Others')", CommonObject.DefaultClientSuffix, customStartDate,  customEndDate);
        }

        public string ExpectedPlanPmpmDetails(string customStartDate, string customEndDate)
        {
            return Format(@"
SELECT
  a.plan_desc plan_desc,
 round( SUM(a.p1_total_paid)/1000,1) p1_total_paid,
  COUNT(a.p1_member_count) P1_member_count,
 -- SUM(b.mm) member_month,
  SUM(a.p1_total_paid) / SUM(b.mm) pm
FROM (SELECT
    member.plan_desc,
    member.int_mbr_id,
    SUM(t1.total_paid) p1_total_paid,
    COUNT(DISTINCT member.int_mbr_id) p1_member_count
  FROM (SELECT
      int_mbr_id,
      group_id,
      member.plan_desc,
      'P1' period
    FROM member_summary_encr_by_month_{0} member
    WHERE member.group_id = '{0}'
    AND plan_desc IS NOT NULL
    AND member.eff_date BETWEEN '{1}' AND '{2}'
    GROUP BY member.int_mbr_id,
             member.plan_desc) member
    LEFT JOIN (SELECT DISTINCT
        t1.group_id,
        t1.int_mbr_id,
        'P1' period,
        SUM(t1.total_rev_paid) total_paid
      FROM member_by_paid_by_month_{0} t1
      WHERE t1.paid_month BETWEEN '{1}' AND '{2}'
      GROUP BY t1.int_mbr_id) t1
      ON member.int_mbr_id = t1.int_mbr_id
      AND member.group_id = t1.group_id
      AND member.period = t1.period
  WHERE 1 = 1
  GROUP BY member.plan_desc,
           member.int_mbr_id) a
  JOIN (SELECT
      plan_desc,
      int_mbr_id,
      SUM(mm)
 mm
    FROM member_summary_encr_by_month_{0}
    WHERE eff_date BETWEEN '{1}' AND '{2}'
    GROUP BY plan_desc,
             int_mbr_id) b
    ON a.int_mbr_id = b.int_mbr_id
GROUP BY a.plan_desc
order by p1_total_paid desc;", CommonObject.DefaultClientSuffix, customStartDate, customEndDate);
        }

        public string ExpectedDivisionDetails(string customStartDate, string customEndDate)
        {
            return Format(@"SELECT
  member.division_name,
  round(SUM(t1.total_paid)/1000,1) p1_total_paid,
 COUNT(DISTINCT member.int_mbr_id) p1_member_count
FROM (SELECT
    int_mbr_id,
    group_id,
    member.division_name,
    'P1' period
  FROM member_summary_encr_by_month_{0} member
  WHERE member.group_id = '{0}'
  
  AND member.eff_date BETWEEN '{1}' AND '{2}'
  GROUP BY member.int_mbr_id,
           member.division_name) member
  LEFT JOIN (SELECT DISTINCT
      t1.group_id,
      t1.int_mbr_id,
      'P1' period,
      SUM(t1.total_rev_paid) total_paid
    FROM member_by_paid_by_month_{0} t1
    WHERE t1.paid_month BETWEEN '{1}' AND '{2}'
    GROUP BY t1.int_mbr_id) t1
    ON member.int_mbr_id = t1.int_mbr_id
    AND member.group_id = t1.group_id
    AND member.period = t1.period
WHERE 1 = 1
GROUP BY division_name
order by p1_total_paid desc;", CommonObject.DefaultClientSuffix, customStartDate, customEndDate);
        }

        public string ExpectedPlanTypeDetails(string customStartDate, string customEndDate)
        {
            return Format(@"SELECT
  member.plan_type_desc as plan_type_desc,
   round(SUM(t1.total_paid)/1000,1) p1_total_paid,
  COUNT(DISTINCT member.int_mbr_id) p1_member_count
FROM (SELECT
    int_mbr_id,
    group_id,
    member.plan_type_desc,
    'P1' period
  FROM member_summary_encr_by_month_{0} member
  WHERE member.group_id = '{0}'
  AND age_band IS NOT NULL
  AND member.eff_date BETWEEN '{1}' AND '{2}'
  GROUP BY member.int_mbr_id,
           member.plan_type_desc) member
  LEFT JOIN (SELECT DISTINCT
      t1.group_id,
      t1.int_mbr_id,
      'P1' period,
      SUM(t1.total_rev_paid) total_paid
    FROM member_by_paid_by_month_{0} t1
    WHERE t1.paid_month BETWEEN '{1}' AND '{2}'
    GROUP BY t1.int_mbr_id) t1
    ON member.int_mbr_id = t1.int_mbr_id
    AND member.group_id = t1.group_id
    AND member.period = t1.period
WHERE 1 = 1
GROUP BY plan_type_desc
order by p1_total_paid desc;", CommonObject.DefaultClientSuffix, customStartDate, customEndDate);
        }


        public string ExpectedPlanTypePmpmDetails(string customStartDate, string customEndDate)
        {
            return Format(@"
SELECT
  a.plan_type_desc plan_type_desc,
  SUM(a.p1_total_paid) p1_total_paid,
  COUNT(a.p1_member_count) P1_member_count,
 -- SUM(b.mm) member_month,
  SUM(a.p1_total_paid) / SUM(b.mm) pm
FROM (SELECT
    member.plan_type_desc,
    member.int_mbr_id,
    SUM(t1.total_paid) p1_total_paid,
    COUNT(DISTINCT member.int_mbr_id) p1_member_count
  FROM (SELECT
      int_mbr_id,
      group_id,
      member.plan_type_desc,
      'P1' period
    FROM member_summary_encr_by_month_{0} member
    WHERE member.group_id = '{0}'
    AND member.eff_date BETWEEN '{1}' AND '{2}'
    GROUP BY member.int_mbr_id,
             member.plan_type_desc) member
    LEFT JOIN (SELECT DISTINCT
        t1.group_id,
        t1.int_mbr_id,
        'P1' period,
        SUM(t1.total_rev_paid) total_paid
      FROM member_by_paid_by_month_{0} t1
      WHERE t1.paid_month BETWEEN '{1}' AND '{2}'
      GROUP BY t1.int_mbr_id) t1
      ON member.int_mbr_id = t1.int_mbr_id
      AND member.group_id = t1.group_id
      AND member.period = t1.period
  WHERE 1 = 1
  GROUP BY member.plan_type_desc,
           member.int_mbr_id) a
  JOIN (SELECT
      plan_type_desc,
      int_mbr_id,
      SUM(mm)
 mm
    FROM member_summary_encr_by_month_{0}
    WHERE eff_date BETWEEN '{1}' AND '{2}'
    GROUP BY plan_type_desc,
             int_mbr_id) b
    ON a.int_mbr_id = b.int_mbr_id
GROUP BY a.plan_type_desc
order by p1_total_paid", CommonObject.DefaultClientSuffix, customStartDate, customEndDate);

        }


        public string ExpectedLocationDetails(string customStartDate, string customEndDate)
        {
            return Format(@"", CommonObject.DefaultClientSuffix, customStartDate, customEndDate);

        }


    }

}

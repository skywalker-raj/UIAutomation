namespace DataDumper.Scripts
{
    public class ErSqlScripts
    {
        #region PUBLIC PROPERTIES

        #region ActiveMembers

        //z5demob_b, member.group_id = z5demob_b
        public static string GetAnnualSpendAndMemberCountForActiveMembers = @"SELECT 
        SUM(CASE WHEN member.period = 1 AND member.active_flag = 1 THEN member.paid_amount ELSE 0 END) p1_paid_amount,
        SUM(CASE WHEN member.period = 2 AND member.active_flag = 1 THEN member.paid_amount ELSE 0 END) p2_paid_amount,
        SUM(CASE WHEN member.period = 3 AND member.active_flag = 1 THEN member.paid_amount ELSE 0 END) p3_paid_amount,
        COUNT(DISTINCT (CASE WHEN member.period = 1 AND member.active_flag = 1 THEN member.member_id END)) p1_member_count,
        COUNT(DISTINCT (CASE WHEN member.period = 2 AND member.active_flag = 1 THEN member.member_id END)) p2_member_count,
        COUNT(DISTINCT (CASE WHEN member.period = 3 AND member.active_flag = 1 THEN member.member_id END)) p3_member_count 
        FROM member_utilization_metrics_summary_{0} member 
        WHERE member.metric_id = 'ER' 
        AND member.group_id = '{1}'";

        //z5demo_b, member.group_id = 'z5demo_b', z5demo_b
        public static string GetPMPMForActiveMembers = @"SELECT ( SELECT SUM ( CASE WHEN member.period = 1 
        AND member.active_flag = 1 THEN member.paid_amount ELSE 0 END) p1_paid_amount 
        FROM member_utilization_metrics_summary_{0} member 
        WHERE member.metric_id = 'ER' AND member.group_id = '{1}') / (SELECT SUM(CASE WHEN period = 1 
        AND active_flag = 1 THEN membermonth ELSE 0 END)
        FROM member_by_mm_by_month_{2})";

        #endregion

        #region AllMembers

        //z5demob_b, member.group_id = z5demob_b
        public static string GetAnnualSpendAndMemberCountForAllMembers = @"SELECT 
        SUM(CASE WHEN member.period = 1 THEN member.paid_amount ELSE 0 END) p1_paid_amount,
        SUM(CASE WHEN member.period = 2 THEN member.paid_amount ELSE 0 END) p2_paid_amount,
        SUM(CASE WHEN member.period = 3 THEN member.paid_amount ELSE 0 END) p3_paid_amount,
        COUNT(DISTINCT (CASE WHEN member.period = 1 THEN member.member_id END)) p1_member_count,
        COUNT(DISTINCT (CASE WHEN member.period = 2 THEN member.member_id END)) p2_member_count,
        COUNT(DISTINCT (CASE WHEN member.period = 3 THEN member.member_id END)) p3_member_count 
        FROM member_utilization_metrics_summary_{0} member 
        WHERE member.metric_id = 'ER' 
        AND member.group_id = '{1}'";

        //z5demo_b, member.group_id = 'z5demo_b', z5demo_b
        public static string GetPMPMForAllMembers = @"SELECT ( SELECT SUM ( CASE WHEN member.period = 1 
        AND THEN member.paid_amount ELSE 0 END) p1_paid_amount 
        FROM member_utilization_metrics_summary_{0} member 
        WHERE member.metric_id = 'ER' AND member.group_id = '{1}') / (SELECT SUM(CASE WHEN period = 1 
        AND THEN membermonth ELSE 0 END)
        FROM member_by_mm_by_month_{2})";

        #endregion

        #endregion
    }
}

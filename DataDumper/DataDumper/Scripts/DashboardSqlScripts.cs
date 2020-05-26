namespace DataDumper.Scripts
{
    public class DashboardSqlScripts
    {
        #region PUBLIC PROPERTIES

        #region All Employee Count

        //member_summary_encr_z5demob_b
        public static string GetAllEmployeeCount = @"SELECT COUNT(DISTINCT member_id)
        FROM member_summary_encr_{0} WHERE p1_exists_flag = 1
        AND mbr_relationship_class = 'Employee'";

        //member_summary_encr_z5demob_b
        public static string GetActiveEmployeeCount = @"SELECT COUNT(DISTINCT member_id)
        FROM member_summary_encr_{0} WHERE p1_exists_flag = 1
        AND p1_active_flag = 1
        AND mbr_relationship_class = 'Employee'";

        #endregion

        #region All Members Count

        //member_summary_encr_z5demob_b
        public static string GetTotalMembersCount = @"SELECT COUNT(DISTINCT member_id)
        FROM member_summary_encr_{0}
        WHERE p1_exists_flag = 1";

        //member_summary_encr_z5demob_b
        public static string GetActiveMembersCount = @"SELECT COUNT(DISTINCT member_id)
        FROM member_summary_encr_{0}
        AND p1_active_flag = 1
        WHERE p1_exists_flag = 1";

        #endregion

        #endregion
    }
}

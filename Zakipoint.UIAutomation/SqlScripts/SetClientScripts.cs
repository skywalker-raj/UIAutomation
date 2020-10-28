namespace Zakipoint.UIAutomation.SqlScripts
{
    public class SetClientScripts
    {
        public string GetClientList = @"SELECT grp.group_name
        FROM app_groups grp
        JOIN app_user_groups usr_grp on grp.group_key=usr_grp.app_groups_id
        JOIN app_dates dts on grp.group_id=dts.group_id
        where grp.enabled = 1
        and user_id = (select id from zph_appconfig.user where username = '{0}') order by grp.group_name asc";
    }
}
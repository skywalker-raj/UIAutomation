namespace Zakipoint.UIAutomation.PageObjects
{
    public class DashboardPageObjects
    {
        #region Elements

        #region CssSelectors

        public string UserLinkDropdownCssSelector = "nav.navbar div.m-wrapper a.dropdown-toggle";
        public string UserLinkDropdownListCssSelector = "nav.navbar div.m-wrapper div.dropdown.user-login div.dropdown-menu > a:nth-child{0}";   //"(1)" :change password || "(2)": logout
        public string UserSettingsCssSelector = "nav.navbar div.m-wrapper a:not(.nav-link)";
        public string LogoImageCssSelector = "nav.navbar img.logo__img";
        public string MenuLinkCssSelector = "nav.navbar ul.navbar-nav li a.nav-link";
        public string QuickLinkCssSelector = "span#dropdownMenuButton span";
        public string QuickLinksCssSelector = "div.dropdown-menu.show a";
        public string ApplicationSettingCloseCssSelector = "button.cd-close";
        public string SelectGroupCheckBoxByCssSelector = "ul.mutliSelect li:nth-child({0}) label.form-check-label";
        public string ApplySettingsButtonCssSelector = "button[name=apply-setting]";
        public string DownloadReportLinkCssSelector = "a.btn.btn-outline-primary span";
        public string ClientDetailsLabelCssSelector = "ul li.media:nth-child({0}) div h5";
        public string ClientDetailsByLabelCssSelector = "ul li.media:nth-child({0}) div";
        public string ClientDetailsImageCssSelector = "ul li.media:nth-child({0}) img";
        public string ClientLogoImageCssSelector = "div.client-logo";
        public string ClientTitleCssSelector = "h3.entry-title.dropdown";
        public string ViewMoreClienCssSelector = "a#dropdownMenuLink";
        public string DashboardSectionTitleCssSelector = "section.overall-info.zph-dashboard div.container div.row div.spend-header header";
        public string DashboardProcessImageCssSelector = "section.overall-info.zph-dashboard>div.container> div.row div.zph-process div.info-icon img";
        public string DashboardProcessTitleCssSelector = "section.overall-info.zph-dashboard>div.container> div.row div.zph-process h3";
        public string DashboardProcessViewMoreCssSelector = "section.overall-info.zph-dashboard>div.container> div.row div.zph-process a.btn";
        public string DashboardSpendTabCssSelector = "div.col-4 header+ul.nav-pills >li.nav-item a";
        public string DashboardConditionCostDriverTabCssSelector = "div.col-8 header+ul.nav-pills >li.nav-item a";
        public string TopConditionTableHeaderCssSelector = "div.medical-box table.table th";
        public string TopConditionDetailsCssSelector = "div.medical-box table.table tbody tr:nth-child({0}) td:nth-child({1})";
        public string TopConditionRowCssSelector = "div.medical-box table.table tbody tr";
        public string TopConditionDetailsByColCssSelector = "div.medical-box table.table tbody tr td:nth-child({0})";
        public string TopConditionDetailsByRowCssSelector = "div.medical-box table.table tbody tr:nth-child({0}) td";
        public string ReportingPeriodByCssSelector = "div.zph-reporting p span:nth-child({0})"; // 2 for reporting period
        public string RadioMemberByCssSelector = "input#{0}"; // termed01: Active Members || termed02 : All Members

        #endregion

        #region XPath

        public string NavBarLinksByTextXPath = "//span[text()='{0}']";
        public string ChangeLinkByLabelXPath = "//div[text()='{0}: ']/span[text()='Change']";
        public string MemberRadioButtonByTextXPath = "//label[text()='{0}']/../input";
        public string ClientBoxDetailsByLabelXPath = "//h5[text()='{0}']/../span"; // Active Members/ All Members  || Total Spend
        public string EmployeesDetailsByLabelXPath = "//h5[text()='{0}']/.."; //Active Employees || All Employees 
        public string ApplicationSettinsgByXPath = "//span[text()='Settings']";
        public string MemberStatusChangeXPath = "//div[text()='Member Status: ']/span[text()='Change']";
        public string ApplySettingXPath = "//span[text()='Apply Settings ']";
        //public string TotalSpendByLabelXPath = "//span[text()='{0}']/../p"; // Medical  || Pharmacy
        public string ClientBoxLabelTextByXPath = "//div[@class='media-body']/h5";
        public string PMPMByLabelXPath = "//div[@id='pills-pmpm']//span[text()='{0}']/../p"; // Medical  || Pharmacy
        public string SpendByLabelXPath = "//div[@id='pills-paid']//span[text()='{0}']/../p"; // Medical  || Pharmacy

        #endregion

        #endregion
    }
}

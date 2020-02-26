namespace Zakipoint.UIAutomation.PageObjects
{
    public class DashboardPageObjects
    {
        #region Elements

        #region CssSelectors

        public const string UserLinkDropdownCssSelector = "nav.navbar div.m-wrapper a.dropdown-toggle";
        public const string UserSettingsCssSelector = "nav.navbar div.m-wrapper a:not(.nav-link)";

        public const string LogoImageCssSelector = "nav.navbar img.logo__img";
        public const string MenuLinkCssSelector = "nav.navbar ul.navbar-nav li a.nav-link";
        public const string QuickLinkCssSelector = "span#dropdownMenuButton span";
        public const string QuickLinksCssSelector = "div.dropdown-menu.show a";
        public const string ApplicationSettingCloseCssSelector = "button.cd-close";
        public const string SelectGroupCheckBoxByCssSelector = "ul.mutliSelect li:nth-child({0}) label.form-check-label";
        public const string ApplySettingsButtonCssSelector = "button[name=apply-setting]";
        public const string DownloadReportLinkCssSelector = "a.btn.btn-outline-primary span";
        public const string ClientDetailsLabelCssSelector = "ul li.media:nth-child({0}) div h5";
        public const string ClientDetailsByLabelCssSelector = "ul li.media:nth-child({0}) div";
        public const string ClientDetailsImageCssSelector = "ul li.media:nth-child({ 0}) img";
        public const string ClientLogoImageCssSelector = "div.client-logo";
        public const string ClientTitleCssSelector = "h3.entry-title.dropdown";
        public const string ViewMoreClienCssSelector = "a#dropdownMenuLink";
        public const string DashboardSectionTitleCssSelector = "section.overall-info.zph-dashboard div.container div.row div.spend-header header";
        public const string DashboardProcessImageCssSelector = "section.overall-info.zph-dashboard>div.container> div.row div.zph-process div.info-icon img";
        public const string DashboardProcessTitleCssSelector = "section.overall-info.zph-dashboard>div.container> div.row div.zph-process h3";
        public const string DashboardProcessViewMoreCssSelector = "section.overall-info.zph-dashboard>div.container> div.row div.zph-process a.btn";
        public const string DashboardSpendTabCssSelector = "div.col-4 header+ul.nav-pills >li.nav-item a";
        public const string DashboardConditionCostDriverTabCssSelector = "div.col-8 header+ul.nav-pills >li.nav-item a";
        public const string TopConditionTableHeaderCssSelector = "div.medical-box table.table th";
        public const string TopConditionDetailsCssSelector = "div.medical-box table.table tbody tr:nth-child({0}) td:nth-child({1})";
        public const string TopConditionRowCssSelector = "div.medical-box table.table tbody tr";
        public const string TopConditionDetailsByColCssSelector = "div.medical-box table.table tbody tr td:nth-child({0})";
        public const string TopConditionDetailsByRowCssSelector = "div.medical-box table.table tbody tr:nth-child({0}) td";

        #endregion

        #region XPath

        public const string NavBarLinksByTextXPath = "//span[text()='{0}']";
        public const string ChangeLinkByLabelXPath = "//div[text()='{0}: ']/span[text()='Change']";
        public const string MemberRadioButtonByTextXPath = "//label[text()='{0}']/../input";

        #endregion

        #endregion
    }
}

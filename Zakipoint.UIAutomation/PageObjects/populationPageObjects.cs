using System;
using System.Collections.Generic;
using System.Text;

namespace Zakipoint.UIAutomation.PageObjects
{
   public class populationPageObjects
    {
       

        //demographics XPath
        public string demographicButtonSelector = "//*[@id='app']/section/div/div[5]/div/div/ul/li[2]";
       
        public string ApplicationSettinsgByXPath = "//span[text()='Quick Links']";
        


        //demographics page css selector
        public string TopConditionDetailsByRowCssSelector = "#z5popmlc001 > g.table.row-group.dynamic-table > g:nth-child(2)";
        public string QuickLinkCssSelector = "span#dropdownMenuButton span";
        public string DropDownPopulation = "#vue-dropdown > div > a:nth-child(2)";
        public string DemographicsButton = "#app > section > div > div.disease-item.disease-tabs > div > div > ul > li:nth-child(2) > a";
        public string ageTileCssSelector = "#z5pophghtsmlc001";
        public string ageSvgBoxcssSelector = "#z5popmlc001 > g.table.row-group.dynamic-table>g";
        public string ageSvgBoxDetailsByRowCssSelector = "#z5popmlc001 > g.table.row-group.dynamic-table > g:nth-child({0}) > text>tspan";

        /* public string ProspectivePopulationRiskStratificationRowByCssSelector = "#z5dsbmlc001 > g.table.row-group.dynamic-table > g";
         public string ProspectivePopulationRiskStratificationDetailsByRowCssSelector = "#z5dsbmlc001 > g.table.row-group.dynamic-table > g:nth-child({0}) > text>tspan";*/

        public string agePmpmButtonCssSelector = "#demographics > div > div > div:nth-child(1) > div.disease-box.age-box > ul > li:nth-child(2) > a";
        public string agePmpmSvgBoxCssSelector = "#z5popmlc002 > g.table.row-group.dynamic-table>g";
        public string agePmpmSvgBoxDetailsByRowCssSelector = "#z5popmlc002 > g.table.row-group.dynamic-table > g:nth-child({0}) > text>tspan";

        public string genderTileCssSelector = "#z5popmlc005";
        public string genderSvgBoxcssSelector = "#z5popmlc005> g.table.row-group.dynamic-table>g";
        public string genderSvgBoxDetailsByRowCssSelector = "#z5popmlc005 > g.table.row-group.dynamic-table > g:nth-child({0}) > text>tspan";

        public string genderPmpmButtonCssSelector = "#demographics > div > div > div:nth-child(1) > div.disease-box.age-box > ul > li:nth-child(2) > a";
        public string genderPmpmSvgBoxCssSelector = "#z5popmlc006 > g.table.row-group.dynamic-table>g";
        public string genderPmpmSvgBoxDetailsByRowCssSelector = "#z5popmlc006 > g.table.row-group.dynamic-table > g:nth-child({0}) > text>tspan";

        public string relationTileCssSelector = "#z5popmlc015";
        public string relationSvgBoxcssSelector = "#z5popmlc015> g.table.row-group.dynamic-table>g";
        public string relationSvgBoxDetailsByRowCssSelector = "#z5popmlc015 > g.table.row-group.dynamic-table > g:nth-child({0}) > text>tspan";

        public string relationPmpmTileCssSelector = "#z5popmlc016";
        public string relationPmpmSvgBoxcssSelector = "#z5popmlc016> g.table.row-group.dynamic-table>g";
        public string relationPmpmSvgBoxDetailsByRowCssSelector = "#z5popmlc016 > g.table.row-group.dynamic-table > g:nth-child({0}) > text>tspan";

        public string planTileCssSelector = "#z5popmlc045";
        public string planSvgBoxcssSelector = "#z5popmlc045> g.table.row-group.dynamic-table>g";
        public string planSvgBoxDetailsByRowCssSelector = "#z5popmlc045 > g.table.row-group.dynamic-table > g:nth-child({0}) > text>tspan";

        public string planPmpmTileCssSelector = "#z5popmlc046";
        public string planPmpmSvgBoxcssSelector = "#z5popmlc046> g.table.row-group.dynamic-table>g";
        public string planPmpmSvgBoxDetailsByRowCssSelector = "#z5popmlc046 > g.table.row-group.dynamic-table > g:nth-child({0}) > text>tspan";

        

         public string planTypeTileCssSelector = "#z5popmlc055";
        public string planTypeSvgBoxcssSelector = "#z5popmlc055> g.table.row-group.dynamic-table>g";
        public string planTypeSvgBoxDetailsByRowCssSelector = "#z5popmlc055 > g.table.row-group.dynamic-table > g:nth-child({0}) > text>tspan";

        public string planTypePmpmTileCssSelector = "#z5popmlc056";
        public string planTypePmpmSvgBoxcssSelector = "#z5popmlc056> g.table.row-group.dynamic-table>g";
        public string planTypePmpmSvgBoxDetailsByRowCssSelector = "#z5popmlc056 > g.table.row-group.dynamic-table > g:nth-child({0}) > text>tspan";

        //location
        public string locationTileCssSelector = "#z5popmlc050";
        public string locationSvgBoxcssSelector = "#z5popmlc050> g.table.row-group.dynamic-table>g";
        public string locationSvgBoxDetailsByRowCssSelector = "#z5popmlc050 > g.table.row-group.dynamic-table > g:nth-child({0}) > text>tspan";

        public string locationPmpmTileCssSelector = "#z5popmlc051";
        public string locationPmpmSvgBoxcssSelector = "#z5popmlc051> g.table.row-group.dynamic-table>g";
        public string locationPmpmSvgBoxDetailsByRowCssSelector = "#z5popmlc051 > g.table.row-group.dynamic-table > g:nth-child({0}) > text>tspan";


        public string divisionTileCssSelector = "#z5popmlc010";
        public string divisionSvgBoxcssSelector = "#z5popmlc010> g.table.row-group.dynamic-table>g";
        public string divisionSvgBoxDetailsByRowCssSelector = "#z5popmlc010 > g.table.row-group.dynamic-table > g:nth-child({0}) > text>tspan";

        //xpath
        public string divisionmodalXPath = "//*[@id='z5popmlc010modal-dialog']/div/div/div[1]/table/tbody/tr";
        public string divisionModalDetailsByRowXPath = "//*[@id='z5popmlc010modal-dialog']/div/div/div[1]/table/tbody/tr({0})";
        public string divisionViewAllBtnCssSelector = "";


        //slider
        
        public string reportingDateRangeStatusChangeXPath = "//*[@id='demo']/div[5]/span[1]";
        public string leftrangeSliderLabelCssSelector = "#analysis_date_slider > div.ui-rangeSlider-label.ui-rangeSlider-leftLabel > div.ui-rangeSlider-label-value";
        public string rightrangeSliderLabelCssSelector ="#analysis_date_slider > div.ui-rangeSlider-label.ui-rangeSlider-rightLabel > div.ui-rangeSlider-label-value";
        public string RadioMemberByCssSelector = "input#{0}";


       /* public string spendLabelCssSelector = "#z5popsum001 > div:nth-child(3) > div.info-content > header > h4 > span";*/
        public string memberLabelCssSelector = "#z5popsum001 > div:nth-child(4) > div.info-content > header > h4 > span";
        
    }
       
}

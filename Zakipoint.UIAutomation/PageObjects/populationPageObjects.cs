﻿using System;
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

        public string relationTileCssSelector = "#z5popmlc005";
        public string relationSvgBoxcssSelector = "#z5popmlc005> g.table.row-group.dynamic-table>g";
        public string relationSvgBoxDetailsByRowCssSelector = "#z5popmlc005 > g.table.row-group.dynamic-table > g:nth-child({0}) > text>tspan";

        //xpath
        public string divisionmodalXPath = "//*[@id='z5popmlc010modal-dialog']/div/div/div[1]/table/tbody/tr";
        public string divisionModalDetailsByRowXPath = "//*[@id='z5popmlc010modal-dialog']/div/div/div[1]/table/tbody/tr({0})";
        public string divisionViewAllBtnCssSelector = "";


        //slider
        
        public string reportingDateRangeStatusChangeXPath = "//*[@id='demo']/div[5]/span[1]";
        public string leftrangeSliderLabelCssSelector = "#analysis_date_slider > div.ui-rangeSlider-label.ui-rangeSlider-leftLabel > div.ui-rangeSlider-label-value";
        public string rightrangeSliderLabelCssSelector ="#analysis_date_slider > div.ui-rangeSlider-label.ui-rangeSlider-rightLabel > div.ui-rangeSlider-label-value";
        public string RadioMemberByCssSelector = "input#{0}";
        
        
    }
       
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Zakipoint.UIAutomation.PageObjects
{
    class PharmacyPageObjects
    {
        //demographics page css selector
        public string QuickLinkCssSelector = "span#dropdownMenuButton span";
        public string DropDownPharmacy = "#vue-dropdown > div > a:nth-child(5)";
        public string DemographicsButton = "#app > section > div > div.disease-item.disease-tabs > div > div > ul > li:nth-child(2) > a";

        public string ageTileCssSelector = "#z5phmmlc010";
        public string ageSvgBoxcssSelector = "#z5phmmlc010 > g.table.row-group.dynamic-table>g";
        public string ageSvgBoxDetailsByRowCssSelector = "#z5phmmlc010 > g.table.row-group.dynamic-table > g:nth-child({0}) > text>tspan";


        public string agePmpmButtonCssSelector = "#demographics > div > div > div:nth-child(1) > div.disease-box.age-box > ul > li:nth-child(2) > a";
        public string agePmpmSvgBoxCssSelector = "#z5popmlc002 > g.table.row-group.dynamic-table>g";
        public string agePmpmSvgBoxDetailsByRowCssSelector = "#z5popmlc002 > g.table.row-group.dynamic-table > g:nth-child({0}) > text>tspan";



        public string genderTileCssSelector = "#z5phmmlc001";
        public string genderSvgBoxcssSelector = "#z5phmmlc001> g.table.row-group.dynamic-table>g";
        public string genderSvgBoxDetailsByRowCssSelector = "#z5phmmlc001 > g.table.row-group.dynamic-table > g:nth-child({0}) > text>tspan";
    }
}

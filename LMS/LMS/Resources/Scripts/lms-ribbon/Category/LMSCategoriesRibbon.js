/// <reference path="~/Resources/Scripts/lms-ribbon/LMSRibbon.js" />
/// <reference path="~/Resources/Scripts/lms-ribbon/LMSRibbonEnum.js" />
/// <reference path="~/Resources/Scripts/lms-ribbon/LMSRibbonEnum.js" />

var lmsCategoriesRibbon = function () {
   var ribbon = lmsRibbon();
   var ribbonEnum = new LMSRibbonEnum();
   var ribbonItems = ribbon.items;
   var state = ribbon.state;

   ribbonItems.item1 = {
      id: ribbonEnum.RIBBON_ITEM_LIST,
      sidebar: {
         path: "/Category/Overview/Sidebar",
         html: null,
         actions: new LMSCategoriesSidebar()
      },
      panel: {
          path: "/Category/Overview/Panel",
         html: null
      }
   };
   return ribbon;
};
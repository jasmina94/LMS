/// <reference path="~/Resources/Scripts/lms-ribbon/CMSRibbon.js" />
/// <reference path="~/Resources/Scripts/lms-ribbon/CMSRibbonEnum.js" />
/// <reference path="~/Resources/Scripts/lms-ribbon/CMSRibbonEnum.js" />

var lmsUsersRibbon = function () {
   var ribbon = lmsRibbon();
   var ribbonEnum = new LMSRibbonEnum();
   var ribbonItems = ribbon.items;
   var state = ribbon.state;

   ribbonItems.item1 = {
      id: ribbonEnum.RIBBON_ITEM_LIST,
      sidebar: {
         path: "/User/Overview/Sidebar",
         html: null,
         actions: new LMSUsersSidebar()
      },
      panel: {
          path: "/User/Overview/Panel",
         html: null
      }
   };
   return ribbon;
};
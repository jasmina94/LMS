/// <reference path="~/Resources/Scripts/cms-ribbon/CMSRibbon.js" />
/// <reference path="~/Resources/Scripts/cms-ribbon/CMSRibbonEnum.js" />
/// <reference path="~/Resources/Scripts/cms-ribbon/CMSRibbonEnum.js" />
/// <reference path="~/Resources/Scripts/security/CMSSecurityEnum.js" />

var cmsUsersRibbon = function () {
   var ribbon = cmsRibbon();
   var ribbonEnum = new CMSRibbonEnum();
   var ribbonItems = ribbon.items;
   var securityEnum = new cmsSecurityEnum();
   var state = ribbon.state;

   ribbonItems.item1 = {
      id: ribbonEnum.RIBBON_ITEM_LIST,
      sidebar: {
         path: "/Users/List/Sidebar",
         html: null,
         actions: new CMSUsersSidebar1()
      },
      panel: {
         path: "/Users/List/Panel",
         html: null
      }
   };
   ribbonItems.item2 = {
      id: ribbonEnum.RIBBON_ITEM_ELEMENT1,
      sidebar: {
         path: "/Users/RibbonElement1/Sidebar",
         html: null,
         actions: new CMSUsersSidebar2()
      },
      panel: {
         path: "/Users/RibbonElement1/Panel",
         html: null
      }
   };
   ribbonItems.item3 = {
      id: ribbonEnum.RIBBON_ITEM_ELEMENT2,
      sidebar: {
         path: "/Users/RibbonElement2/Sidebar",
         html: null,
         actions: new CMSUsersSidebar3()
      },
      panel: {
         path: "/Users/RibbonElement2/Panel",
         html: null
      }
   };
   ribbonItems.item4 = {
      id: ribbonEnum.RIBBON_ITEM_ELEMENT3,
      sidebar: {
         path: "/Users/RibbonElement3/Sidebar",
         html: null,
         actions: new CMSUsersSidebar4()
      },
      panel: {
         path: "/Users/RibbonElement3/Panel",
         html: null
      }
   }

   return ribbon;
};
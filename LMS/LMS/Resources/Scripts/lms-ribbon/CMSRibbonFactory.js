/// <reference path="~/Resources/Scripts/cms-ribbon/CMSRibbonEnum.js" />
/// <reference path="~/Resources/Scripts/cms-ribbon/CMSRibbon.js" />
/// <reference path="~/Resources/Scripts/cms-ribbon/Company/CMSCompaniesRibbon.js" />
/// <reference path="~/Resources/Scripts/cms-ribbon/Person/CMSPersonsRibbon.js" />
/// <reference path="~/Resources/Scripts/cms-ribbon/CMSUsersRibbon.js" />
/// <reference path="~/Resources/Scripts/cms-ribbon/CMSNotesRibbon.js" />

var cmsRibbonFactory = function () {
   var ribbonFactory = {};
   var cmsRibbonEnum = new CMSRibbonEnum();
   var cmsRibbonTypes = new Map();

   cmsRibbonTypes.set(cmsRibbonEnum.USERS_TYPE, cmsUsersRibbon);

   ribbonFactory.createRibbon = function (type) {
      var ribbon = {};

      if (cmsRibbonTypes.has(type)) {
         var ribbonType = cmsRibbonTypes.get(type);

         ribbon = ribbonType();
      }

      ribbon.type = type;

      return ribbon;
   };

   return ribbonFactory;
};

/// <reference path="~/Resources/Scripts/lms-ribbon/LMSRibbonEnum.js" />
/// <reference path="~/Resources/Scripts/lms-ribbon/LMSRibbon.js" />
/// <reference path="~/Resources/Scripts/lms-ribbon/LMSUsersRibbon.js" />
/// <reference path="~/Resources/Scripts/lms-ribbon/LMSCategoriesRibbon.js" />
/// <reference path="~/Resources/Scripts/lms-ribbon/LMSBooksRibbon.js" />
/// <reference path="~/Resources/Scripts/lms-ribbon/LMSLanguagesRibbon.js" />

var lmsRibbonFactory = function () {
    var ribbonFactory = {};
    var lmsRibbonEnum = new LMSRibbonEnum();
    var lmsRibbonTypes = new Map();

    lmsRibbonTypes.set(lmsRibbonEnum.USERS_TYPE, lmsUsersRibbon);
    lmsRibbonTypes.set(lmsRibbonEnum.CATEGORIES_TYPE, lmsCategoriesRibbon);
    lmsRibbonTypes.set(lmsRibbonEnum.BOOKS_TYPE, lmsBooksRibbon);
    lmsRibbonTypes.set(lmsRibbonEnum.LANGUAGES_TYPE, lmsLanguagesRibbon);

    ribbonFactory.createRibbon = function (type) {
        var ribbon = {};

        if (lmsRibbonTypes.has(type)) {
            var ribbonType = lmsRibbonTypes.get(type);

            ribbon = ribbonType();
        }

        ribbon.type = type;

        return ribbon;
    };

    return ribbonFactory;
};

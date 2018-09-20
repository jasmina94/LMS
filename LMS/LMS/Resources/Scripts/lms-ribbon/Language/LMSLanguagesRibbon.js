/// <reference path="~/Resources/Scripts/lms-ribbon/LMSRibbon.js" />
/// <reference path="~/Resources/Scripts/lms-ribbon/LMSRibbonEnum.js" />
/// <reference path="~/Resources/Scripts/lms-ribbon/LMSRibbonEnum.js" />

var lmsLanguagesRibbon = function () {
    var ribbon = lmsRibbon();
    var ribbonEnum = new LMSRibbonEnum();
    var ribbonItems = ribbon.items;
    var state = ribbon.state;

    ribbonItems.item1 = {
        id: ribbonEnum.RIBBON_ITEM_LIST,
        sidebar: {
            path: "/Language/Overview/Sidebar",
            html: null,
            actions: new LMSLanguagesSidebar()
        },
        panel: {
            path: "/Language/Overview/Panel",
            html: null
        }
    };
    return ribbon;
};
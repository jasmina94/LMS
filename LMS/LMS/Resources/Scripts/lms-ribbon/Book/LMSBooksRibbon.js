/// <reference path="~/Resources/Scripts/lms-ribbon/LMSRibbon.js" />
/// <reference path="~/Resources/Scripts/lms-ribbon/LMSRibbonEnum.js" />
/// <reference path="~/Resources/Scripts/lms-ribbon/LMSRibbonEnum.js" />
var lmsBooksRibbon = function () {
    var ribbon = lmsRibbon();
    var ribbonEnum = new LMSRibbonEnum();
    var ribbonItems = ribbon.items;
    var state = ribbon.state;

    ribbonItems.item1 = {
        id: ribbonEnum.RIBBON_ITEM_LIST,
        sidebar: {
            path: "/Book/Overview/Sidebar",
            html: null,
            actions: new LMSBooksSidebar()
        },
        panel: {
            path: "/Book/Overview/Panel",
            html: null
        }
    };
    return ribbon;
};
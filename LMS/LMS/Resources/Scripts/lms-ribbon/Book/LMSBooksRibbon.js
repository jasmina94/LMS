﻿/// <reference path="~/Resources/Scripts/lms-ribbon/LMSRibbon.js" />
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

    ribbonItems.item2 = {
        id: ribbonEnum.RIBBON_ITEM_ELEMENT1,
        sidebar: {
            path: "/Book/Overview/SidebarCopy",
            html: null,
            actions: new LMSBookCopiesSidebar()
        },
        panel: {
            path: "/Book/Overview/PanelCopy",
            html: null
        }
    };

    ribbonItems.item3 = {
        id: ribbonEnum.RIBBON_ITEM_ELEMENT2,
        sidebar: {
            path: "/Book/Overview/SidebarBorrowed",
            html: null,
            actions: new LMSBookBorrowedSidebar()
        },
        panel: {
            path: "/Book/Overview/PanelBorrowed",
            html: null
        }
    };
    return ribbon;
};
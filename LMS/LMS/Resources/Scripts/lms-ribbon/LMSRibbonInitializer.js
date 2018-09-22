/// <reference path="~/Resources/Scripts/lms-ribbon/LMSRibbonEnum.js" />
/// <reference path="~/Resources/Scripts/lms-ribbon/LMSRibbonFactory.js" />
/// <reference path="~/Resources/Scripts/ajax/AjaxHttpSender.js" />
/// <reference path="~/Resources/Scripts/lms-grid/LMSGrid.js" />
/// <reference path="~/Resources/Scripts/security/LMSSecurityProvider.js" />

var lmsRibbonInitializer = function ($) {
    var ribbonEnum = new LMSRibbonEnum();
    var ribbonFactory = lmsRibbonFactory();
    var ajaxHttpSender = new AjaxHttpSender();

    var $ribbon = $("ul." + ribbonEnum.RIBBON_CLASS);
    var dataRibbon = $ribbon.data(ribbonEnum.DATA_RIBBON);
    var dataRibbonType = $ribbon.data(ribbonEnum.DATA_RIBBON_TYPE);
    var activeRibbonItem;

    if (!dataRibbon && dataRibbonType) {
        dataRibbon = ribbonFactory.createRibbon(dataRibbonType);
        $ribbon.data(ribbonEnum.DATA_RIBBON, dataRibbon);

        $ribbon
           .find("li a." + ribbonEnum.RIBBON_ITEM_CLASS)
           .each(function () {
               var $ribbonItemA = $(this);
               var $ribbonItemLi = $ribbonItemA.parent();

               $ribbonItemA.on("click", function (e) {
                   e.preventDefault();

                   $ribbon.children("li").removeClass("active");
                   $ribbonItemLi.addClass("active");
                   changeRibbonItemView();
               });

               setTooltip($ribbonItemA);
           });

        changeRibbonItemView();
    }

    function changeRibbonItemView() {
        $ribbon
           .find("li[class='active'] > a")
           .each(function () {
               var $ribbonItemA = $(this);
               var dataRibbonItem = $ribbonItemA.data(ribbonEnum.DATA_RIBBON_ITEM);
               var ribbonItem = dataRibbon.items[dataRibbonItem];

               activeRibbonItem = ribbonItem;
               loadAndChangeSidebar(ribbonItem);
               loadAndChangePanel(ribbonItem);
           });
    }

    function loadNotPermited(ribbonItem) {
        var $sidebar = $("#" + ribbonEnum.SIDEBAR_CONTAINER).empty();
        var $panel = $("#" + ribbonEnum.PANEL_CONTAINER).empty();
        toastr.warning(securityEnum.WARNING_MESSAGE, securityEnum.WARNING_TITLE);
    }

    function loadAndChangeSidebar(ribbonItem) {
        if (ribbonItem.sidebar.html == null || ribbonItem.sidebar.html == undefined) {
            ajaxHttpSender.sendGet(ribbonItem.sidebar.path, {
                success: function (htmlSidebar) {
                    ribbonItem.sidebar.html = htmlSidebar;
                    changeSidebar(htmlSidebar);
                }
            });
        }
        else {
            changeSidebar(ribbonItem.sidebar.html);
        }
    }

    function loadAndChangePanel(ribbonItem) {
        if (ribbonItem.panel.html == null || ribbonItem.panel.html == undefined) {
            ajaxHttpSender.sendGet(ribbonItem.panel.path, {
                success: function (htmlPanel) {
                    ribbonItem.panel.html = htmlPanel;
                    changePanel(htmlPanel);
                }
            });
        }
        else {
            changePanel(ribbonItem.panel.html);
        }
    }

    function changeSidebar(htmlSidebar) {
        $("#" + ribbonEnum.SIDEBAR_CONTAINER).empty();
        $("#" + ribbonEnum.SIDEBAR_CONTAINER).html(htmlSidebar);
        initializeSidebar();
    }

    function changePanel(htmlPanel) {
        var nextState = activeRibbonItem.id;
        var $panelContainer = $("#" + ribbonEnum.PANEL_CONTAINER);
        var $content = $panelContainer.children();
        var currentState = dataRibbon.state;

        if ($content.length == 0) {
            $panelContainer.append(htmlPanel);
            dataRibbon.state = activeRibbonItem.id;
        } else {
            var newContent = true;

            $content.each(function () {
                var $child = $(this);
                if ($child.hasClass(nextState)) {
                    newContent = false;
                    showContent($child, nextState);
                } else if (nextState != currentState && $child.is(":visible")) {
                    hideContent($child, currentState);
                }
            });

            if (newContent && nextState != currentState) {
                $panelContainer.append(htmlPanel);
            } else if (nextState == ribbonEnum.RIBBON_ITEM_LIST) {
                var $lmsGrid = $(".lms-grid-container");
                var grid = $lmsGrid.data("LMSGrid");
                grid.formatBodyRowsCells();
            }

            dataRibbon.state = nextState;
        }
    }

    function showContent(content, state) {
        var $content = $(content);
        $content.removeClass(state);
        $content.show();
    }

    function hideContent(content, state) {
        var $content = $(content);
        var className = $content.attr("class");

        if (className != "" && className != null) {
            var index = className.indexOf(state);
            if (index == -1)
                $content.addClass(state);
        } else {
            $content.addClass(state);
        }

        $content.hide();
    }

    function initializeSidebar() {
        var $sidebar = $("ul." + ribbonEnum.SIDEBAR_CLASS);
        var $sidebarActions = $sidebar.find("li a." + ribbonEnum.SIDEBAR_ACTION);

        $sidebarActions.each(function () {
            var $sidebarActionA = $(this);
            var dataSidebarAction = $sidebarActionA.data(ribbonEnum.DATA_SIDEBAR_ACTION);

            $sidebarActionA.on("click", function (e) {
                e.preventDefault();
                $sidebarActionA.trigger("blur");

                var sidebarActions = activeRibbonItem.sidebar.actions;

                if (sidebarActions && sidebarActions[dataSidebarAction]) {
                    sidebarActions[dataSidebarAction]();
                }
            });

            setTooltip($sidebarActionA);
        });
    }

    function setTooltip($element) {
        $element.tooltip({
            content: function () {
                var $self = $(this);

                return $self.attr("title");
            },
            tooltipClass: "tooltip-ballon-dark"
        });
    }
};

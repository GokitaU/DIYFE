$(function () {
    /* Get navigation and place it on holder */

    var holder = "#responsive-menu-holder";

    function getMenu() {

        var $mainNav    = $("#nav .nav").html(),
            $utilityNav = $(".utility-bar .static").html(),
            $subNav = $(".subnav").html();

        var $mainMenu = $(holder + " .menu").append("<ul>" + $mainNav + "</ul>"),
            $utilMenu = $(holder + " .utility").append($utilityNav),
            $subNav   = $(holder + " .subMenu").append($subNav);

        $(holder + " .menu li a").addClass("parent");

        $(holder + " .utility li a#signinBtn, " + holder + " .utility li a#logoffBtn").parent().remove();

        subMenuValidation(holder);
    }

    getMenu();

    function subMenuValidation(e) {
        $(e + " .subMenu ul").each(function () {
            if ($(this).children("li").length > 0) {
                $id = $(this).attr("id");
                mainMenuEdits(e, $id);
            }
        });
    }

    function mainMenuEdits(e, id) {
        var $link = $(e + " .mainMenu .menu li a[rel='" + id + "']"),
            $name = $link.find("span").text(),
            $href = $link.attr("href");

        $link.addClass("stop");

        updateSubMenu(e, id, $name, $href);
    }

    function updateSubMenu(e, id, name, href) {
        var $mainUL = $(e + " .subMenu ul#" + id + " li");

        $mainUL.eq(0).before("<li class='subMenuHeader'><a href='#' class='prev-nav'>" + name + "</a></li>");
        $mainUL.eq(0).before("<li><a href='" + href + "'>Go to " + name + "</a></li>");
    }

    $(".toggleMenu").click(function (e) {
        $(holder).toggle();
        $(holder + " > div").css({ left: 0 });
        $(this).toggleClass("active");
    });

    $('.prev-nav').click(function () {
        $(holder + " > div").animate({ left: 0 }, 450, "swing");
    });

    $('.stop').click(function (e) {
        e.preventDefault();
        $(holder + " > div").animate({ left : -480 }, 450, "swing");
        var subMenuReset = $(".subMenu ul").hide(),
            subMenuRel = $(this).attr("rel");
        $(holder + " .subMenu #" + subMenuRel).show();
    });

});
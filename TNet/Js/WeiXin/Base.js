
var g_base_x_v = 0;
function initBase() {
    var tabsSwiper = new Swiper('.swiper-container', {
        autoplay: 3000,
        visibilityFullFit: true,
        loop: true,
        pagination: '.swiper-pagination',
        paginationClickable: true,
        preloadImages: false,
        lazyLoading: true
    });
    Pub.auth(false);
    setTopMenuEvent();

}
function autoShowCity() {
    if (g_base_x_v == 0) {
        g_base_x_v = $(document.body).scrollTop();
    }
    var cObj = $('#C');
    cObj.toggle();
    var ch = $('#CityHost');
    ch.toggle();
    if (cObj.is(":hidden")) {
        setTopMenuEvent(autoShowCity, "Top_Menu_Back");
        $(document.body).removeClass("body_bg").addClass("body_bg");
    } else {
        setTopMenuEvent();
        $(document.body).removeClass("body_bg");
        window.setTimeout(function () {
            $(document).scrollTop(g_base_x_v);
            g_base_x_v = 0;
        }, 80);
    }
}
function initCityList(city) {
    var citys = Pub.getCitys();
    var html = "";
    if (citys) {
        for (var i = 0; i < citys.length; i++) {
            var co = citys[i];
            if (co) {
                html += '<li><a href="javascript:void(0)" onclick="onCityChange(\'' + co.city1 + '\')">' + co.city1 + '<span /></a></li>';
            }
        }
    }
    if (html) {
        html = "<ul>" + html + "</ul>";
    }
    $("#CityBox").html(html);
    $("#city").html(city ? city.city1 : "");
}
function onCityChange(city) {
    Pub.setCache("location_city", city);
    Pub.doCityReadys();
}
function autoShowTopMenu() {
    if (g_base_x_v == 0) {
        g_base_x_v = $(document.body).scrollTop();
    }
    var cObj = $('#C');
    cObj.toggle();
    $('#Top_Main_Menu_Host').toggle();
    if (cObj.is(":hidden")) {
        $(document.body).removeClass("body_bg").addClass("body_bg");
    } else {
        $(document.body).removeClass("body_bg");
        window.setTimeout(function () {
            $(document).scrollTop(g_base_x_v);
            g_base_x_v = 0;
        }, 80);
    }
}
$(document).ready(initBase);


function setTopMenuEvent(func, css) {
    if (!func) {
        func = autoShowTopMenu;
    }
    var mu = $("#Top_Menu");
    if (!css) {
        mu.removeClass().addClass("Top_Menu");
    } else {
        mu.removeClass().addClass(css);
    }
    mu.unbind('click').click(func);
}



function getTime(t) {
    if (t) {
        t = t.substring(0, t.length - 3);
    } else {
        t = "";
    }
    return t;
}




function getTimeYYMMHH(t) {
    if (t) {
        t = t.substring(0, t.length - 9);
    } else {
        t = "";
    }
    return t;
}

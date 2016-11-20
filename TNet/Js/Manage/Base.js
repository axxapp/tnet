
function initParam() {
    var href = window.location.href + "";

    if ((/(Manage\/MercList)/gi).test(href) || (/(Manage\/MercEdit)/gi).test(href)) {
        $("#MercList").addClass("select");
    } else if ((/(Manage\/MercTypeList)/gi).test(href) || (/(Manage\/MercTypeEdit)/gi).test(href)) {
        $("#MercTypeList").addClass("select");
    } else if ((/(Manage\/OrderList)/gi).test(href)) {
        $("#OrderList").addClass("select");
    } else if ((/(Manage\/BusinessList)/gi).test(href) || (/(Manage\/BusinessEdit)/gi).test(href)) {
        $("#BusinessList").addClass("select");
    }
    else if ((/(Manage\/Menu)/gi).test(href)) {
        $("#Menu").addClass("select");
    }
    $(".CL").niceScroll({
        //mousescrollstep: 40, // 鼠标滚轮的滚动速度 (像素)
        //touchbehavior: true, // 激活拖拽滚动
        hwacceleration: true, // 激活硬件加速
        railoffset: true,
        disableoutline: true,
        enabletranslate3d: true, // nicescroll 可以使用CSS变型来滚动内容
        enablemousewheel: true, // nicescroll可以管理鼠标滚轮事件
        enablekeyboard: true, // nicescroll可以管理键盘事件
        enablemouselockapi: true, // 可以用鼠标锁定API标题 (类似对象拖动)
        cursorfixedheight: false, // 修正光标的高度（像素）
        //hidecursordelay: 400, // 设置滚动条淡出的延迟时间（毫秒）
        preventmultitouchscrolling: true,
        boxzoom: false,
        autohidemode: "leave",
        smoothscroll: true, // ease动画滚动
        sensitiverail: true, // 单击轨道产生滚动
        zindex: 999999,
        horizrailenabled: false,
    });
    updateSetResize();
    $(window).resize(function () {
        updateSetResize();
    });
}

function updateSetResize() {
    $(".CL").getNiceScroll().resize();
    var mh = $(".CR").outerHeight(true) + 20;
    var dh = $(document).height() - 50;
    if (mh > dh) {
        mh = dh;
    }
    $(".CR").css("height", mh);
}


$(document.body).ready(initParam);

function ajax_del(delUrl, postData) {
    $.post(delUrl, postData, function (returndata) {
        returndata = eval("(" + returndata + ")");
        if (returndata.Code == "1") {
            alert(returndata.Message);
            window.location.href = window.location.href;
        }
        else {
            alert(returndata.Message);
        }
    });
}
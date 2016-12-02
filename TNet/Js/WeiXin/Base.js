var g_Swiper_Obj = null;
var g_base_x_v = 0;
function initBase() {
    if (!g_Swiper_Obj) {
        g_Swiper_Obj = new Swiper('.swiper-container', {
            initialSlide: 0,
            autoplay: 2000,
            visibilityFullFit: true,
            loop: true,
            pagination: '.swiper-pagination',
            paginationClickable: true,
            preloadImages: false,
            lazyLoading: true,
            centeredSlides: true,
            autoplayDisableOnInteraction: false,
            observer: true,//修改swiper自己或子元素时，自动初始化swiper
            observeParents: true//修改swiper的父元素时，自动初始化swiper
        });
        Pub.auth(false);
        setTopMenuEvent();
    } else {
        try {
            g_Swiper_Obj.removeAllSlides();

        } catch (e) {

        }

    }

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





function getAd(pos, city) {
    if (!loadingAd(pos)) {
        var c = city ? city.idcity : "";
        Pub.get({
            url: "Service/Ad/List/" + pos + "/" + c,
            //noLoading: true,
            loadingMsg: "加载中...",
            success: function (data) {
                //alert(JSON.stringify(data));
                if (Pub.wsCheck(data)) {
                    Pub.setCache(pos + "_ad", data.Data, 20);
                    loadAd(pos);
                    return;
                }
                loadingAd();
            },
            error: function (xhr, status, e) {
                loadingAd();
            }
        });
    }

}


function loadingAd() {
    $(".ad_loading_c").show();
    //initBase();
}


function loadAd(pos) {
    var ad = Pub.getCache(pos + "_ad");
    if (!ad) {
        loadingAd();
        return false;
    } else {
        var html = '';
        // g_Swiper_Obj.stopAutoplay();
        initBase();
        // g_Swiper_Obj.updateClasses();
        //for (var i = ad.length - 1; i >= 0 ; i--) {
        var i = 0;
        for (; i < ad.length ; i++) {
            var ao = ad[i];
            var href = ao.link ? ao.link : "javascript:void(0)";
            //html = '';
            html += '<div class="swiper-slide">';
            html += '<a href="' + href + '">';
            html += '<img class="swiper-lazy" data-src="' + Pub.url(ao.img, "Images/default_bg.png") + '" />';
            html += '</a>';
            html += '<div class="swiper-lazy-preloader swiper-lazy-preloader-white""></div>';
            html += '</div>';
        }

        if (i > 0) {
            g_Swiper_Obj.appendSlide(html);
            g_Swiper_Obj.updateSlidesSize();
            g_Swiper_Obj.slideNext();
            // $(".swiper-wrapper").append(html); 
            // g_Swiper_Obj.slideTo(0, 0, false);
            g_Swiper_Obj.updatePagination();
            g_Swiper_Obj.updateClasses();
            //g_Swiper_Obj.reLoop();
            $(".ad_loading_c").hide();
            return true;

        }
    }


}
 

//选择图片
function __FileSelectImg(id, e) {
    //var fo = document.getElementById(id);
    //Pub.showError(e.files[0]);
    //window.setTimeout(function () {
    //    alert(e.files[0]);
    //}, 1000);
    try {
        lrz(e.files[0], {
            width: 800
        }).then(function (rst) {
            //alert(rst);
            // 处理成功会执行
            // console.log(rst);       
            FileUploadImg(rst.base64, id);
        }).catch(function (err) {
            alert("选择图片错误");
            // 处理失败会执行
        }).always(function () {
            // 不管是成功失败，都会执行
        });
    } catch (e) {
        alert("选择图片错误");
    }
}
//alert("haha");
//上传图片
function FileUploadImg(imgData, id) {
    Pub.post({
        url: "Service/File/Upload",
        data: JSON.stringify({ data: imgData }),
        loadingMsg: "上传图片中...",
        success: function (data) {
            if (Pub.wsCheck(data)) {
                $("#" + id).attr("src", imgData);
                $("#" + id).attr("title", data.Data.name);
                return;
            }
            Pub.showError("上传失败");
        },
        error: function (xhr, status, e) {
            Pub.showError("上传失败");
        }
    });
}


function PreviewImage(imgs) {
    wx.previewImage(imgs); 
    //{ 
    //    current: '', // 当前显示图片的http链接

    //    urls: [] // 需要预览的图片http链接列表

    //}
}


$(document).ready(initBase);
var buss_data = null;
//获取周边商圈详情
function getData() {
    var idbuss = $("#OC").attr("idbuss");
    if (idbuss) {
        Pub.get({
            url: "Service/Buss/Detail/" + idbuss,

            loadingMsg: "加载中...",
            success: function (data) {
                if (Pub.wsCheck(data)) {
                    if (data.Data) {
                        try {
                            buss_data = data.Data;
                            $(".buss_title").html(data.Data.Buss.buss);
                            $(".sellpt").html(data.Data.Buss.sellpt);
                            $(".price").html("￥" + data.Data.Buss.price);
                            $("#buss_addr").html("<i class='iconfont'>&#xe615</i>" + data.Data.Buss.addr);
                            $("#buss_st_et").html(data.Data.Buss.runtime);
                            $("#busstime").html(getTimeYYMMHH(data.Data.Buss.busstime));
                            $("#notes").html(data.Data.Buss.notes);
                            $("#contact").html(data.Data.Buss.contact);
                            $("#phone").html(data.Data.Buss.phone);

                            $("#call").attr("href", "tel:" + data.Data.Buss.phone);

                            var imgs = data.Data.Imgs;
                            if (imgs) { 
                                var imgHtml = "";
                                initBase();
                                for (var i = 0; i < imgs.length; i++) {
                                    var ur = Pub.url(imgs[i], "Images/default_bg.png");
                                    imgHtml = "";
                                    imgHtml += "<div class='swiper-slide'><img data-src='" + ur + "'  onclick='lookImg(this)'  class='swiper-lazy' /><div class='swiper-lazy-preloader swiper-lazy-preloader-white'></div></div>";
                                    g_Swiper_Obj.appendSlide(imgHtml);
                                }
                                if (imgHtml) {
                                    $(".ad_loading_c").hide();
                                    g_Swiper_Obj.slideNext();
                                    //  $('.swiper-wrapper').html(imgHtml);

                                }
                            }
                        } catch (e) {
                            //$('#buss_host').html("加载异常" + e.message);
                            return;
                        }

                    }
                }
                load_fail("亲,暂无商家详情");
            },
            error: function (xhr, status, e) {
                alert("加载商家详情失败");
            }
        });
    }
}

$(document).ready(getData);

function load_fail(msg) {
    //Pub.noData("#buss_host", msg, getData);
}


function lookImg(obj) {
    var imgs = buss_data.Imgs;
   
    var ms = [];
    if (!imgs || imgs.length <= 0) {
        imgs = ["Images/default_bg.png"];
    }
    if (imgs) {
        for (var i = 0; i < imgs.length; i++) {
            ms.push(Pub.fullUrl(imgs[i]));
        }
    }
   // alert(JSON.stringify(ms));
    var img = Pub.fullUrl($(obj).attr('src'));
    PreviewImage({
        current: img,
        urls: ms
    });
}
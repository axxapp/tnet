
function initData() {
    var city = Pub.curCity();
    getData(city)

}


//获取周边商圈
function getData(city) {
    var c = city ? city.code : "";

    Pub.get({
        url: "Service/Buss/List/" + c,

        loadingMsg: "加载中...",
        success: function (data) {
            if (Pub.wsCheck(data)) {
                if (data.Data) {
                    var html = "";
                    try {
                        for (var i = 0; i < data.Data.length; i++) {
                            var o = data.Data[i];
                            if (html) {
                                // html += '<div class="vline"></div>';
                            }
                            var img = o.imgs;
                            var ur = " src='" + Pub.url(img, "Images/default_bg.png") + "' ";

                            var ba = Pub.url("Buss/Detail/" + o.idbuss);
                            html += '<div class="buss_item">';
                            html += '<a href="' + ba + '" id="buss" class="buss">';
                            html += '<div class="buss_l">';
                            html += '<img id="ico" ' + ur + ' />';
                            html += '</div>';
                            html += '<div class="buss_c">';
                            html += '<span id="buss_title">' + o.buss + '</span>';
                            html += '<span id="buss_price">￥' + o.price + '</span>';
                            html += '</div>';
                            html += '</a>';

                            html += '<div class="buss_addr_host">';
                            html += '<a href="javascript:void(0)"><span id="buss_addr"><i class="iconfont">&#xe615</i>' + o.addr + '</span></a>';
                            html += '</div>';
                            html += '</div>';
                        }
                        if (html) {
                            $('#buss_host').html(html);
                            return;
                        }
                    } catch (e) {
                        $('#buss_host').html("加载异常" + e.message);
                        return;

                    }

                }
            }
            load_fail("亲,暂无周边商圈");
        },
        error: function (xhr, status, e) {
            load_fail("加载周边商圈失败");
        }
    });
}



$(document).ready(initData);

function load_fail(msg) {
    Pub.noData("#buss_host", msg, getData);
}
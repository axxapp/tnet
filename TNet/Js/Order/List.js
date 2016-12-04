﻿Pub.checkUser(true);

//获取订单
function getData() {
    var u = Pub.getUser();
    if (u != null) {
        Pub.get({
            url: "Service/Order/List/" + u.iduser,
            loadingMsg: "加载中...",
            success: function (data) {
                if (Pub.wsCheck(data)) {
                    if (data.Data) {
                        var html = "";
                        try {
                            data.Data.Status = setStatus(data.Data.Status);
                            for (var i = 0; i < data.Data.Order.length; i++) {
                                var o = data.Data.Order[i];
                                var so = getStatus(data.Data.Status, o.status);
                                if (html) {
                                    html += '<div class="vline"></div>';
                                }
                                var otype = (o.otype == 2) ? "<div class='setup_tag'></div> " : "";
                                html += '<div class="order_item">' + otype;
                                html += '<div class="order">';
                                html += '<div class="no">' + o.orderno + '</div>';
                                html += '<div class="status">' + so.text + '</div>';
                                html += '</div>';
                                var da = Pub.url("Order/Detail/" + o.orderno + "?iduser=" + u.iduser);

                                html += '<a href="' + da + '" id="merc" class="merc">';
                                html += '<div class="merc_l">';

                                var ur = " src='" + Pub.url(o.img, "Images/default_bg.png") + "' ";

                                html += '<img id="ico" ' + ur + ' />';
                                html += '</div>';
                                html += '<div class="merc_c">';

                                html += '<span id="merc_title">' + o.merc + '</span>';
                                html += '<span id="merc_spec">' + o.spec + '</span>';
                                html += '<span id="merc_time">' + getTime(o.stime) + " 至 " + getTime(o.entime) + '</span>';
                                html += '</div>';
                                html += '<div class="merc_r">';

                                html += '<span id="merc_price">￥' + o.price + '</span>';
                                html += '<span id="merc_count">x' + o.count + '</span>';
                                html += '</div>';
                                html += '</a>';
                                html += '<div class="order_ops">';
                                html += '<div class="amont">￥' + o.totalfee + '</div>';
                                html += '<div class="ops">' + getOps(so, o, true) + '</div>';
                                html += '</div>';
                                html += '</div>';
                            }
                            if (html) {
                                $('#order_host').html(html);
                                return;
                            }
                        } catch (e) {
                            $('#order_host').html("加载异常");
                            return;

                        }

                    }
                }
                load_fail("亲,您暂无订单");
            },
            error: function (xhr, status, e) {
                load_fail("加载订单失败");
            }
        });
    }
}


$(document.body).ready(getData);
function load_fail(msg) {
    Pub.noData("#order_host", msg, getData);
}
var order_data = null;
//获取订单
function getData() {
    var orderno = $("#OC").attr("orderno");
    var iduser = Pub.urlParam("iduser");
    if (!iduser) {
        var u = Pub.getUser();
        if (u) {
            iduser = u.iduser;
        }
    }

    if (iduser != null && orderno) {

        $("#orderno").html("单号：" + orderno);
        Pub.get({
            url: "Service/Order/Detail/" + iduser + "/" + orderno,
            loadingMsg: "加载中...",
            success: function (data) {
                if (Pub.wsCheck(data)) {
                    if (data.Data) {
                        var html = "";
                        try {
                            order_data = data.Data;
                            var o = data.Data.Order;
                            var tag = "";
                            if (o.otype == 2) {
                                tag = "?tag=Setup";
                                $(".setup_tag").show();
                                $(".resource").show();
                                $("#idc").html(o.idc);
                                $("#idc_img1").attr("src", Pub.url(o.idc_img1, "Images/default_bg.png"));
                                $("#idc_img2").attr("src", Pub.url(o.idc_img2, "Images/default_bg.png"));
                                $("#idc_img3").attr("src", Pub.url(o.idc_img3, "Images/default_bg.png"));
                                if (o.idc_img1) {
                                    $("#idc_img1").click(function (event) {
                                        lookImg(event.target);
                                    });
                                }
                                if (o.idc_img2) {
                                    $("#idc_img2").click(function (event) {
                                        lookImg(event.target);
                                    });
                                }
                                if (o.idc_img3) {
                                    $("#idc_img3").click(function (event) {
                                        lookImg(event.target);
                                    });
                                }

                            }
                            $("#merc").attr("href", Pub.rootUrl() + "Merc/Detail/" + o.idmerc + tag);
                            $("#contacts").html(o.contact);
                            $("#phones").html(o.phone);
                            $("#realAddr").html(o.addr);
                            $("#ico").attr("src", Pub.url(o.img, "Images/default_bg.png"));
                            $("#merc_title").html(o.merc);
                            $("#merc_spec").html(o.spec);
                            $("#merc_price").html("￥" + o.price);
                            $("#merc_count").html("x" + o.count);
                            var so = data.Data.Status;
                            $("#status").html(so.text);
                            $("#notes").val(o.notes);
                            $("#attmonth").html("送: " + o.attmonth + "  月");
                            $("#amount").html("￥" + o.totalfee);
                            $("#ops").html(getOps(so, o));
                            $("#merc_st_et").html(getTime(o.stime) + " 至 " + getTime(o.entime));
                            var phtml = "";
                            if (data.Data.Presses) {
                                for (var i = 0; i < data.Data.Presses.length; i++) {
                                    var po = data.Data.Presses[i];
                                    phtml += "<div class='p_item'><span class='p_time'>" + po.cretime + "</span>";
                                    phtml += "<span class='p_statust'>" + po.statust + "</span>";
                                    phtml += "<span class='p_oper'>" + po.oper + "</span></div>";
                                }
                            }
                            $("#Presses").html(phtml);

                        } catch (e) {
                            //$('#order_host').html("加载异常" + e.message);
                            return;
                        }

                    }
                }
                //   load_fail("亲,您暂无订单");
            },
            error: function (xhr, status, e) {
                //load_fail("加载订单失败");
            }
        });
    }
}



$(document).ready(getData);



function lookImg(obj) {
    var o = order_data.Order;
    var ms = [];
    if (o.idc_img1) {
        ms.push(Pub.fullUrl(o.idc_img1))
    }
    if (o.idc_img2) {
        ms.push(Pub.fullUrl(o.idc_img2))
    }
    if (o.idc_img3) {
        ms.push(Pub.fullUrl(o.idc_img3))
    }
    var img = Pub.fullUrl($(obj).attr('src'));
    PreviewImage({
        current: img,
        urls: ms
    });
}
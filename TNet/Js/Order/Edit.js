var order_data = null;
//获取订单
function getData() {
    var orderno = Pub.urlParam("orderno");
    var u = Pub.getUser();
    if (u) {
        iduser = u.iduser;
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
                                $("#idc").val(o.idc);
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
                            $("#merc").attr("href", Pub.url("Merc/Detail/" + o.idmerc + tag));
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
                            // $("#ops").html(getOps(so, o));
                            $("#merc_st_et").html(getTime(o.stime) + " 至 " + getTime(o.entime));
                            var phtml = "";
                            so = data.Data.Order.status;

                            if (data.Data.Presses) {
                                for (var i = 0; i < data.Data.Presses.length; i++) {
                                    var po = data.Data.Presses[i];
                                    if (so == 400 && po.status == 400) {
                                        $(".review_msg_host").show();
                                        $(".review_msg").html(po.notes);
                                    }
                                    phtml += "<div class='p_item_host'><div class='p_item'>";
                                    phtml += "<span class='p_time'>" + po.cretime + "</span>";
                                    phtml += "<span class='p_statust'>" + po.statust + "</span>";
                                    phtml += "<span class='p_oper'>" + po.oper + "</span></div>";
                                    if (po.notes) {
                                        phtml += "<div class='p_item_notes'>" + po.notes + "</div>";
                                    }
                                    phtml += "</div>";


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



//下订单
function saveOrder() {
    var orderno = Pub.urlParam("orderno");
    if (orderno && order_data) {
        var u = Pub.getUser();
        if (u != null) {
            var contact = "";
            var addr = "";
            var phone = "";
            var ao = order_data.Order;
            if (_g_set_addr_tag) {
                ao = Pub.getCache("Addr");
                if (ao) {
                    addr = ao.province + ao.city + ao.district + ao.street;
                }
            } else {
                addr = ao.addr;
            }
            if (!addr) {
                alert("请选择地址");
                return;
            }
            contact = ao.contact;
            phone = ao.phone;
            var idc = "";
            idc = $.trim(Pub.str($("#idc").val()));
            if (!checkIdc(idc)) {
                return;
            }
            var idcImgs = [
                {
                    img: "",
                    msg: "请上传身份证 正面 照"
                },
                {
                    img: "",
                    msg: "请上传身份证 反面 照"
                },
                {
                    img: "",
                    msg: "请上传身份证 手持正面 照"
                },
            ];
            for (var i = 0; i < idcImgs.length; i++) {
                var mo = idcImgs[i];
                mo.img = getIdcImg("idc_img" + (i + 1), mo.msg);
                if (!mo.img) {
                    return;
                }
            }
            var data = {
                iduser: u.iduser,
                orderno: orderno,
                contact: contact,
                addr: addr,
                phone: phone,
                notes: Pub.str($("#notes").val()),
                idc: idc,
                idc_img1: idcImgs[0].img,
                idc_img2: idcImgs[1].img,
                idc_img3: idcImgs[2].img
            };

            Pub.post({
                url: "Service/Order/Update",
                data: JSON.stringify(data),
                loadingMsg: "保存...",
                success: function (data) {
                    if (Pub.wsCheck(data)) {
                        if (data.Data) {
                            Pub.showMsg("跳转中...");
                            //alert("\n下单成功,订单号: " + data.Data.orderno);
                            window.location.href = Pub.url("Order/List");// + "Order/Pay/?orderno=" + data.Data.orderno;
                            return;
                        }
                    }
                },
                error: function (xhr, status, e) {
                    Pub.showMsg("保存失败");
                }
            });
        } else {
            alert("用户信息有误");
        }
    }
}


function getIdcImg(key, msg) {
    var img = $.trim($("#" + key).attr("title"));
    if (!img) {
        img = order_data.Order[key];
    }
    if (!img) {
        alert(msg);
        return;
    }
    return img;
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
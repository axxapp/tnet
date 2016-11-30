


function init() {
    var order_cart = Pub.getCache("order_cart");
    if (order_cart && order_cart.Merc && order_cart.Spec) {
        $("#merc").attr("href", Pub.url("Merc/Detail/" + order_cart.Merc.idmerc));
        $("#merc_title").html(order_cart.Merc.merc1);
        $("#merc_spec").html(order_cart.Spec.spec1);
        $("#merc_price").html("￥" + order_cart.Spec.price);
        $("#merc_count").html("x" + order_cart.Count);
        $("#buy_att_month").html("送:" + order_cart.Spec.attmonth + " 月");
        $("#amount").html("￥ " + (order_cart.Spec.price * order_cart.Count));
        var ur = Pub.url("Images/default_bg.png");
        var imgs = order_cart.Img;
        if (imgs) {
            ur = Pub.url(imgs, "Images/default_bg.png");
        }
        $("#ico").attr("src", ur);
        loadAddr();
        if (order_cart.Merc.isetup) {
            $(".go_buy").html("提交");
            $(".resource").show();
        }
    } else {
        toHome();
    }

}

$(document.body).ready(init);





//下订单
function submit() {
    var order_cart = Pub.getCache("order_cart");
    if (order_cart && order_cart.Merc && order_cart.Spec) {
        var u = Pub.getUser();
        if (u != null) {
            var contact = "";
            var addr = "";
            var phone = "";
            var ao = Pub.getCache("Addr");
            if (ao) {
                contact = ao.contact;
                addr = ao.province + ao.city + ao.district + ao.street;
                phone = ao.phone;
            }
            if (!addr) {
                alert("请选择地址");
                return;
            }
            var otype = "merc";
            var idc = "", idc_img1 = "", idc_img2 = "", idc_img3 = "";
            if (order_cart.Merc.isetup) {
                otype = "setup";
                idc = $.trim(Pub.str($("#idc").val()));
                if (!checkIdc(idc)) {
                    return;
                }
                idc_img1 = $.trim($("#idc_img1").attr("title"))
                if (!idc_img1) {
                    alert("请上传身份证 正面 照");
                    return;
                }
                idc_img2 = $.trim($("#idc_img2").attr("title"))
                if (!idc_img2) {
                    alert("请上传身份证 反面 照");
                    return;
                }
                idc_img3 = $.trim($("#idc_img3").attr("title"))
                if (!idc_img3) {
                    alert("请上传 手持身份证(正面) 照");
                    return;
                }
            }
            var img = order_cart.Img;
            if (!img) {
                img = "";
            }
            var data = {
                iduser: u.iduser,
                uname: u.name,
                idmerc: order_cart.Merc.idmerc,
                merc: order_cart.Merc.merc1,
                idspec: order_cart.Spec.idspec,
                spec: order_cart.Spec.spec1,
                count: order_cart.Count,
                month: order_cart.Spec.month,
                attmonth: order_cart.Spec.attmonth,
                contact: contact,
                addr: addr,
                phone: phone,
                notes: Pub.str($("#notes").val()),
                img: img,
                otype: otype,
                idc: idc,
                idc_img1: idc_img1,
                idc_img2: idc_img2,
                idc_img3: idc_img3
            };

            Pub.post({
                url: "Service/Order/Create",
                data: JSON.stringify(data),
                loadingMsg: "提交...",
                success: function (data) {
                    if (Pub.wsCheck(data)) {
                        if (data.Data) {
                            Pub.delCache("order_cart")
                            //alert("\n下单成功,订单号: " + data.Data.orderno);
                            window.location.href = Pub.url(data.Data.url);// + "Order/Pay/?orderno=" + data.Data.orderno;
                            return;
                        }
                    }
                },
                error: function (xhr, status, e) {
                    Pub.showMsg("提交失败");
                }
            });
        } else {
            alert("用户信息有误");
        }
    } else {
        toHome();
    }
}


function toHome() {
    Pub.showMsg("亲！请选择宝贝");
    window.location.href = Pub.rootUrl();
}


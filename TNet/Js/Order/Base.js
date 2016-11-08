Pub.checkUser(true);




//取消订单
function cancelOrder(orderno) {
    if (confirm("确定取消订单")) {
        doCancelOrder(orderno);
    }
}
//执行取消订单
function doCancelOrder(orderno) {
    var u = Pub.getUser();
    if (u != null) {
        Pub.get({
            url: "Service/Order/Cancel/" + u.iduser + "/" + orderno,

            loadingMsg: "取消中...",
            success: function (data) {
                if (Pub.wsCheck(data)) {
                    alert("取消订单成功");
                    getData();
                }
            },
            error: function (xhr, status, e) {
                alert("取消订单失败");
            }
        });
    }
}


function setStatus(s) {
    var sObj = {};
    if (s) {
        for (var so in s) {
            if (so) {
                sObj[s[so].Key] = s[so].Value;
            }
        }
    }
    return sObj;
}

function getStatus(s, status) {
    if (s) {
        //for (var i = 0; i < s.length; i++) {
        var so = s[status];
        if (so) {
            return so;
        }
        //}
    }
    return { code: "", text: "未知", ops: "" };
}

function getOps(status, order) {
    //alert(JSON.stringify(status))
    var uo = Pub.getUser();
    //alert(JSON.stringify(uo))
    var html = "";
    var iduser = Pub.urlParam("iduser");
    if (status.ops) {
        var op = status.ops.split("|");
        for (var i = 0; i < op.length; i++) {
            var p = op[i];
            if (p == "cancel") {
                if (iduser && uo == null || iduser != uo.iduser) {
                    continue;
                }
                html += '<a class="cancel" href="javascript:void(0)" onclick="cancelOrder(\'' + order.orderno + '\')">取消</a>';
            } else if (p == "pay") {
                if (iduser && uo == null || iduser != uo.iduser) {
                    continue;
                }
                var pu = Pub.rootUrl() + "Order/Pay/?orderno=" + order.orderno;
                html += '<a class="pay" href="' + pu + '">支付</a>';
            } else if (p == "reviewOrder") {
                if (uo && uo.mu && uo.mu.recvReview) {
                    html += '<a class="pay" href="javascript:void(0)" onclick="showReviewOrderHost(\'' + order.orderno + '\')">审核</a>';
                }

            }
        }
    }
    return html;
}


function showReviewOrderHost(orderno) {
    $(".reviewOrderHost").toggle();
    $(".reviewOrderBox").toggle();
    $(".reviewOrderHost").attr("orderno", orderno);
}


//审核
function reviewOrder(review) {
    var orderno = $(".reviewOrderHost").attr("orderno");
    if (orderno) {
        var content = Pub.str($("#reviewOrderCValue").val(), true);
        if (!review && !content) {
            alert("请填写审核意见");
            return;
        }
        var iduser = Pub.urlParam("iduser");
        var u = Pub.getUser();
        if (u != null && iduser) {
            var data = {
                mgcode: u.mu.code,
                iduser: iduser,
                orderno: orderno,
                content: content,
                review: review
            };
            Pub.post({
                url: "Service/Order/Review",
                data: JSON.stringify(data),
                loadingMsg: "审核中...",
                success: function (data) {
                    //alert(JSON.stringify(data));
                    if (Pub.wsCheck(data)) {
                        alert("审核成功");
                        window.location.href = window.location.href + "";
                        return;
                    }
                    //alert("审核失败,请稍后重试");
                },
                error: function (xhr, status, e) {
                    alert("审核失败,请稍后重试");
                }
            });
        } else {
            alert("用户信息有误");
        }
    } else {
        alert("订单有误");
    }
}
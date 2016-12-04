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

function getOps(status, order, isList) {
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
                if (iduser && (uo == null || iduser != uo.iduser)) {
                    continue;
                }
                html += '<a class="cancel" href="javascript:void(0)" onclick="cancelOrder(\'' + order.orderno + '\')">取消</a>';
            } else if (p == "pay") {
                if (iduser && (uo == null || iduser != uo.iduser)) {
                    continue;
                }
                var pu = Pub.url("Order/Pay/?orderno=" + order.orderno);
                html += '<a class="pay" href="' + pu + '">支付</a>';
            } else if (p == "reviewOrder") {
                if (uo && uo.mu && uo.mu.recvReview) {
                    if (isList) {
                        html += '<a class="pay" href="' + Pub.url("Order/Detail/" + order.orderno) + '">去审核</a>';
                    } else {
                        html += '<a class="pay" href="javascript:void(0)" onclick="showReviewOrderHost(\'' + order.orderno + '\')">审核</a>';
                    }
                }

            } else if (p == "editOrder") {
                if (iduser && (uo == null || iduser != uo.iduser)) {
                    continue;
                }
                html += '<a class="pay" href="' + Pub.url("Order/Edit/?orderno=" + order.orderno) + '" >编辑</a>';

            } else if (p == "dispTask") {
                if (uo && uo.iduser == "636083726396093750" && uo.mu && uo.mu.sendSetup && !order.idtask) {
                    if (isList) {
                        html += '<a class="pay" href="' + Pub.url("Order/Detail/" + order.orderno) + '">去派单</a>';
                    } else {
                        // html += '<a class="pay" href="javascript:void(0)" onclick="showWorkHost(\'' + order.orderno + '\')">发起抢单</a>';
                        html += '<a class="pay" href="javascript:void(0)" onclick="showWorkHost(\'' + order.orderno + '\')">派单</a>';

                    }
                }
            }
        }
    }
    return html;
}

//审核
function showReviewOrderHost(orderno) {
    if (!orderno) {
        orderno = "";
    }
    if ($(".reviewOrderHost").is(":hidden")) {
        $(".reviewOrderHost").css("display", "block");
        $(".reviewOrderBox_Host").css("display", "flex");
    } else {
        $(".reviewOrderHost").css("display", "none");
        $(".reviewOrderBox_Host").css("display", "none");
    }
    $(".reviewOrderHost").attr("orderno", orderno);
}


//审核
function reviewOrder(review, event) {
    event.cancelBubble = true;
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
                        var realu = window.location.href + "";
                        var reg = new RegExp('[?|&]t=([^&]+)');
                        realu = realu.replace(reg, '');
                        if (realu.indexOf("?") > 0) {
                            realu += "&";
                        } else {
                            realu += "?";
                        }
                        realu += "t=" + (new Date).getTime();
                        window.location.href = realu;
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




function dispTask(orderno) {
    var u = Pub.getUser();
    if (u == null || !u.mu) {
        alert("您无权派单");
        return;
    }
    if (g_worker_data) {
        var ws = [];
        for (var i = 0; i < g_worker_data.length; i++) {
            if ($("#worker_" + i).is(":checked")) {
                ws.push(g_worker_data[i].mgcode);
            }
        }
        if (ws.length > 0) {
            Pub.post({
                url: "Service/Task/Dis/Order",
                data: { orderno: orderno, mcode: u.mu.code, mname: u.mu.name, works: ws },
                loadingMsg: "派单中...",
                success: function (data) {
                    if (Pub.wsCheck(data)) {
                        alert("派单成功,工单号：" + data.Data);
                        window.location.href = window.location.href + "";//Pub.url("Task/MyTask");
                    }
                },
                error: function (xhr, status, e) {
                    alert("派单失败");
                }
            });

            return;
        }
    }

    alert("请选择工人");
}
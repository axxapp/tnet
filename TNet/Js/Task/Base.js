Pub.checkUser(true);
var __G_TASK_DATA_CACHE = null;
function getROps(so, o) {
    var html = "";
    // alert(JSON.stringify(so));
    if (so && so.ops) {
        var op = so.ops.split("|");
        for (var i = 0; i < op.length; i++) {
            var p = op[i];
            if (p == "srartwork") {
                html += '<a class="pause" href="javascript:void(0)" onclick="startWork(\'' + o.idtask + '\',\'' + o.idrecver + '\',event)">开工</a>';
            } else if (p == "pause") {
                html += '<a class="pause" href="' + Pub.url("Task/Pause?idtask=" + o.idtask + '&idrecver=' + o.idrecver) + '">暂停</a>';
            } else if (p == "finish") {
                html += '<a class="finish" href="' + Pub.url("Task/Finish?idtask=" + o.idtask + '&idrecver=' + o.idrecver) + '">完工</a>';
            }
        }
    }
    return html;
}

function getOps(so, o, islist) {
    var html = "";
    var u = Pub.getUser();
    if (so && so.ops) {
        var op = so.ops.split("|");
        for (var i = 0; i < op.length; i++) {
            var p = op[i];
            if (p == "echoTask") {
                if (u && u.mu && u.mu.code == o.idsend) {
                    if (islist) {
                        html += '<a class="pause" href="javascript:void(0)">去回访</a>';
                    } else {
                        html += '<a class="pause" onclick="showEchtTaskHost(\'' + o.idtask + '\')" href="javascript:void(0)">回访</a>';
                    }
                }
            }
        }
    }
    return html;
}



//回访
function showEchtTaskHost(idtask) {
    if (!idtask) {
        idtask = "";
    }
    if ($(".reviewOrderHost").is(":hidden")) {
        $(".reviewOrderHost").css("display", "block");
        $(".reviewOrderBox_Host").css("display", "flex");
    } else {
        $(".reviewOrderHost").css("display", "none");
        $(".reviewOrderBox_Host").css("display", "none");
    }
    $(".reviewOrderHost").attr("idtask", idtask);
}


//回访
function echoTask(event) {
    event.cancelBubble = true;
    var idtask = $(".reviewOrderHost").attr("idtask");
    if (idtask) {
        var content = Pub.str($("#reviewOrderCValue").val(), true);
        var u = Pub.getUser();
        if (u != null && u.mu && u.iduser) {
            var data = {
                mgcode: u.mu.code,
                iduser: u.iduser,
                idtask: idtask,
                content: content
            };
            Pub.post({
                url: "Service/Task/Echo",
                data: JSON.stringify(data),
                loadingMsg: "回访中...",
                success: function (data) {
                    //alert(JSON.stringify(data));
                    if (Pub.wsCheck(data)) {
                        alert("回访成功");
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
                    alert("回访失败,请稍后重试");
                }
            });
        } else {
            alert("用户信息有误");
        }
    } else {
        alert("订单有误");
    }
}




function getTimeNum(workTime, unit) {

    if (!unit) {
        unit = "分";
    }
    var tt = "";
    if (!workTime) {
        return "...";
    }
    if (workTime >= 60) {
        var wt = parseInt(workTime / 60);
        if (wt >= 24) {
            wt = parseInt(wt / 24);
            if (wt > 30) {
                tt = "超1月";
            } else {
                tt = wt + "天";
            }
        } else {
            if (wt == 24) {
                wt = 1;
                tt = wt + "天";
            } else {
                tt = wt + "时";
            }
            wt = (workTime % 60);
            if (wt) {
                tt += wt + "分";
            }
        }
    } else {
        tt = workTime + "分";
    }
    if (!tt) {
        tt = "...";
    }
    return tt;
}
function getPress(ts) {
    //alert(ts.join(','));
    var html = "";
    html += '<div class="p_item_box"></div>';

    for (var i = 0; i < ts.length; i++) {
        var lcss = "", rcss = "", ptcss = "";
        if (ts[i].v) {
            lcss = " p_select";
            ptcss = 'p_t_select';
        }
        if ((i + 1) < ts.length && ts[i + 1].v) {
            rcss = " p_select";
            lcss = " p_select";

        }
        html += '<div class="p_item_box">';

        html += '<div class="p_item_time">';
        html += '<div class="p_start ' + lcss + '"></div>';
        if ((i + 1) < ts.length) {
            html += '<div class="p_line_l ' + lcss + '"></div>';
            html += '<div class="p_line_r ' + rcss + '"></div>';
        }
        html += '</div>';
        //if (i == 0) 

        html += '<div class="p_item_text ' + ptcss + '">' + ts[i].t + "<br/>" + getTime(ts[i].v) + '</div>';
        //}
        html += '</div>';
    }

    // alert(html);

    return html;

}

function startWork(idtask, idrecver, e) {
    e.stopPropagation();
    var u = Pub.getUser();
    if (u != null && u.mu && idtask && idrecver) {
        var data = { idtask: idtask, idrecver: idrecver, mgcode: u.mu.code };
        Pub.post({
            url: "Service/Task/Start",
            loadingMsg: "开工中...",
            data: data,
            success: function (data) {
                if (Pub.wsCheck(data)) {
                    if (data.Data) {
                        try {
                            var x = 0;
                            if (x == 0) {
                                x = $(document.body).scrollTop();
                            }
                            getData(function () {
                                window.setTimeout(function () {
                                    $(document).scrollTop(x);
                                    x = -100;
                                }, 80);
                            });
                            alert("开工成功");
                            if (x != -100) {
                                $(document).scrollTop(x);
                            }
                        } catch (e) {
                            alert(e.message)
                        }
                    }
                }
            },
            error: function (xhr, status, e) {
                alert("开工错误");
            }
        });
    }
}




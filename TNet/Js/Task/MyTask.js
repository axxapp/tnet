

//工单列表
function getData(callFunc) {
    var u = Pub.getUser();
    if (u != null && u.mu) {
        Pub.get({
            url: "Service/Task/List/" + u.mu.code,
            loadingMsg: "加载中...",
            success: function (data) {
                if (Pub.wsCheck(data)) {
                    if (data.Data) {
                        var html = "";
                        try {
                            for (var i = 0; i < data.Data.length; i++) {
                                var o = data.Data[i];
                                var so = o.statusObj;
                                var rso = o.recverStatusObj;
                                if (html) {
                                    html += '<div class="vline"></div>';
                                }
                                //var otype = (o.otype == 2) ? "<div class='setup_tag'></div> " : "";
                                html += '<div class="order_item" onclick="goDetail(\'' + o.idtask + '\',\'' + o.idrecver + '\')">';
                                html += '<div class="order">';
                                html += '<div class="no">工单：' + o.idtask + "  (" + o.tasktype_t + ")" + '</div>';
                                html += '<div class="task_status"><div class="gstatus">' + so.text + '</div>';

                                html += '<div class="rstatus">' + rso.text + '</div>';

                                html += '</div>';
                                html += '</div>';
                                // html += '<a href="' + da + '"   class="task">';

                                html += '<div class="titem_host">';
                                html += '<div class="ttitle">任务:</div>';
                                html += '<div class="tvalue">' + o.title + '</div>';
                                html += '</div>';

                                html += '<div class="titem_host">';
                                html += '<div class="ttitle">客户:</div>';
                                html += '<div class="tvalue">' + o.contact + '</div>';
                                html += '</div>';


                                html += '<div class="titem_host">';
                                html += '<div class="ttitle">电话:</div>';
                                html += '<div class="tvalue">' + o.phone + '</div>';
                                html += '</div>';


                                html += '<div class="titem_host">';
                                html += '<div class="ttitle">地址:</div>';
                                html += '<div class="tvalue">' + o.addr + '</div>';
                                html += '</div>';
                                html += '<div class="taskPress">';
                                //html += '<div class="press_time">';
                                var ts = [
                                    { t: "受理", v: o.accpeptime },
                                    { t: "派工", v: o.revctime },
                                    { t: "完工", v: o.finishtime },
                                    { t: "回访", v: o.echotime }
                                ];
                                html += getPress(ts);
                                html += '</div>';

                                //html += '</a>';
                                html += '<div class="task_ops">';
                                html += '<div class="time_num"><span style="color:#757575;">耗时：</span>' + getTimeNum(o.workTime) + '</div>';

                                html += '<div class="ops">' + getROps(rso, o) + '</div>';
                                html += '</div>';
                                html += '</div>';
                            }
                            if (html) {
                                $('#order_host').html(html);
                                if (callFunc) {
                                    callFunc();
                                }
                                return;
                            }
                        } catch (e) {
                            $('#order_host').html("加载异常");
                            if (callFunc) {
                                callFunc();
                            }
                            return;
                        }

                    }
                }
                if (callFunc) {
                    callFunc();
                }
                load_fail("亲,您暂无工单");
            },
            error: function (xhr, status, e) {
                if (callFunc) {
                    callFunc();
                }
                load_fail("加载工单失败");
            }
        });
    }
}

function goDetail(idtask, idrecver) {
    var u = Pub.getUser();
    if (idtask && idrecver && u != null && u.mu) {
        window.location.href = Pub.url("Task/Detail?idtask=" + idtask + "&idrecver=" + idrecver);
    }
}

$(document).ready(getData);

function load_fail(msg) {
    Pub.noData("#order_host", msg, getData);
}


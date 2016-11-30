
//工单明细
function getData() {
    var u = Pub.getUser();
    var idtask = Pub.urlParam("idtask");
    if (u != null && u.mu && idtask) {
        Pub.get({
            url: "Service/Task/Detail/" + idtask + "/" + u.mu.code,
            loadingMsg: "加载中...",
            success: function (data) {
                //alert(JSON.stringify(data));
                if (Pub.wsCheck(data)) {
                    if (data.Data) {
                        var html = "";
                        try {
                            var task = data.Data.task;
                            $("#idtask").html(task.idtask + " (" + task.tasktype_t + ")");
                            $("#status").html(task.statusObj.text);
                            $("#task").html(task.title);
                            $("#contact").html(task.contact);
                            $("#phone").html(task.phone);
                            $("#addr").html(task.addr);
                            var ts = [
                                    { t: "受理", v: task.accpeptime },
                                    { t: "派工", v: task.revctime },
                                    { t: "完工", v: task.finishtime },
                                    { t: "回访", v: task.echotime }
                            ];
                            html += getPress(ts);
                            if (html) {
                                $('#taskPress').html(html);
                            }
                            setPress(data);
                            //setRecver(data);
                            setMerc(data);
                            var so = task.statusObj;
                            $("#ops").html(getOps(so, task));

                            return;
                        } catch (e) {
                            $('#task_host').html("加载异常" + e.message);
                            return;
                        }

                    }
                }
                load_fail("亲,您暂无工单");
            },
            error: function (xhr, status, e) {
                load_fail("加载工单失败");
            }
        });
    }
}

//处理进度
function setPress(data) {
    var task = data.Data.task;
    var press = data.Data.press;
    var recver = data.Data.recver;
    var phtml = "";
    if (press) {
        for (var i = 0; i < press.length; i++) {
            var po = press[i];
            phtml += "<div class='p_item_host'><div class='p_item'>";
            phtml += "<div class='p_item_topic'><span class='ptext'>" + po.ptext + "</span>";
            if (po.ptype == 100) {
                phtml += "<span class='p_oper'>" + task.send + "</span>";
            } else {
                for (var j = 0; j < recver.length; j++) {
                    var ro = recver[j];
                    if (ro.idrecver == po.idrecver) {
                        phtml += "<span class='p_oper'>" + ro.recver + "</span>";
                        break;
                    }
                }
            }
            phtml += "<span class='pcretime'>" + getTime(po.cretime) + "</span></div>";
            if (po.pdesc) {
                phtml += "<div class='p_item_desc'>" + po.pdesc + "</div>";
            }
            phtml += "</div></div>";
        }
    }
    if (phtml) {
        $(".taskDoPress").html(phtml);
    } else {
        Pub.noData("#taskDoPress", "暂无处理进度");
    }
}


//工人
function setRecver(data) {
    var press = data.Data.press;
    var recver = data.Data.recver;
    var phtml = "";
    if (recver) {
        for (var i = 0; i < recver.length; i++) {
            var ro = recver[i];
            phtml += "<div class='p_item_host'><div class='p_item'>";
            phtml += "<div class='p_item_topic'><span class='ptext'>" + ro.mname + "</span>";

            phtml += "</div></div>";
        }
    }
    if (phtml) {
        $(".worker").html(phtml);
    } else {
        Pub.noData("#worker", "暂无工人");
    }
}


//商品
function setMerc(data) {
    var o = data.Data.order;
    if (o) {
        $(".mercHosr").show();
        var url = Pub.url("Merc/Detail/" + o.idmerc + "?tag=Setup");
        $("#merc").attr("href", url);
        $("#ico").attr("src", Pub.url(o.img, "Images/default_bg.png"));
        $("#merc_title").html(o.merc);
        $("#merc_spec").html(o.spec);
        $("#merc_price").html("￥" + o.price);
        $("#merc_count").html("x" + o.count);
    }
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



$(document).ready(getData);

function load_fail(msg) {
    Pub.noData("#task_host", msg, getData);
}


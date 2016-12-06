

//工单明细
function getData() {
    var u = Pub.getUser();
    var idtask = Pub.urlParam("idtask");
    var idrecver = Pub.urlParam("idrecver");
    var dis = Pub.urlParam("dis");
    if (u != null && u.mu && idtask) {
        var _idrecver = idrecver;
        if (!_idrecver || _idrecver == "") {
            _idrecver = "null";
            $(".task_ops").hide();
        }
        Pub.get({
            url: "Service/Task/Detail/" + idtask + "/" + _idrecver + "/" + u.mu.code,
            loadingMsg: "加载中...",
            success: function (data) {
                //alert(JSON.stringify(data));
                if (Pub.wsCheck(data)) {
                    if (data.Data) {
                        var html = "";
                        try {
                            var task = data.Data.task;
                            var rso = null;
                            var recver = data.Data.recver;
                            if (recver != null) {
                                for (var i = 0; i < recver.length; i++) {
                                    var ro = recver[i];
                                    if (ro.mcode == u.mu.code && ro.idrecver == idrecver) {
                                        rso = ro;
                                    }
                                }
                            }
                            var so = task.statusObj;
                            $("#idtask").html(task.idtask + " (" + task.tasktype_t + ")");
                            $(".gstatus").html(so.text);
                            var sh = "";
                            if (rso != null) {
                                $(".rstatus").html(rso.statusObj.text);
                                sh = getROps(rso.statusObj, rso);

                            } else {
                                if (dis) {
                                    sh = getOps(so, task, false);
                                }
                                $(".order").css("height", "40px").css("line-height", "40px");
                                $(".no").css("height", "40px").css("line-height", "40px");
                                $(".task_status").css("height", "40px").css("line-height", "40px");
                                $(".rstatus").hide();
                                $(".gstatus").css("height", "40px").css("line-height", "40px").css("border", "none");
                            }

                            if (sh && sh != "") {
                                $(".task_ops").show();
                                $("#ops").html(sh);
                            } else {
                                $(".task_ops").hide();
                            }

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
                            html += setTaskPresBar(ts);
                            if (html) {
                                $('#taskPress').html(html);
                            }
                            var img = data.Data.imgs;
                            __G_IMG_DATA_CACHE = img;
                            html = "";
                            if (img && img.length > 0) {
                                html = '<div  class="taskimg">';
                                var z = 0;
                                for (var j = 0; j < img.length; j++) {
                                    var om = img[j];
                                    if (om.type == "main") {
                                        if (z++ == 0) {
                                            html += '<div class="taskimg_box">';
                                        }
                                        html += '<a href="javascript:void(0)" onclick="lookImg(this)" ><img src="' + Pub.url(om.path, "Images/default_bg.png") + '""/></a>';
                                        if (z == 4) {
                                            z = 0;
                                            html += "</div>";
                                        }
                                    }
                                }
                                if (z != 0) {
                                    html += '</div>';
                                }
                                html += '</div>';
                                $(".taskimg").html(html);
                            }
                            setPress(data);
                            //setRecver(data);
                            setMerc(data);
                            return;
                        } catch (e) {
                            $('#task_host').html("加载异常");
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




$(document).ready(getData);

function load_fail(msg) {
    Pub.noData("#task_host", msg, getData);
}




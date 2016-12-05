Pub.checkUser(true);
var issue_data = null;
//获取问题
function getData() {
    var u = Pub.getUser();
    var iuuse = Pub.urlParam("issue");
    if (u != null && iuuse) {
        Pub.get({
            url: "Service/Issue/Detail/" + iuuse,
            loadingMsg: "加载中...",
            success: function (data) {
                //alert(JSON.stringify(data))
                if (Pub.wsCheck(data)) {
                    if (data.Data) {
                        var o = data.Data.issue;
                        var task = data.Data.task;
                        $("#issue").html(o.issue1);
                        var so = task ? task.statusObj : null;
                        $("#status").html((so ? so.text : "等待派工"));
                        $("#contact").html(o.contact);
                        $("#phone").html(o.phone);
                        $("#cretime").html(o.cretime);
                        $("#booktime").html(o.booktime);
                        $("#context").html(o.context);
                        var img = data.Data.imgs;
                        __G_IMG_DATA_CACHE = img;
                        var html = "";
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
                        if (task) {
                            var ts = [
                                      { t: "受理", v: task.accpeptime },
                                      { t: "派工", v: task.revctime },
                                      { t: "完工", v: task.finishtime },
                                      { t: "回访", v: task.echotime }
                            ];
                            html = setTaskPresBar(ts);
                            if (html) {
                                $("#taskPress").show();
                                $('#taskPress').html(html);
                            }
                        } else {
                            if (u.mu && u.mu.sendSetup) {
                                $(".task_ops").show();
                                $("#ops").html('<a class="pay" href="javascript:void(0)" onclick="showWorkHost(\'' + o.issue1 + '\')">派单</a>');
                            }
                        }
                        setPress(data);

                    }
                }
                // load_fail("亲,您暂无投诉建议");
            },
            error: function (xhr, status, e) {
                load_fail("加载投诉建议失败");
            }
        });
    }
}





$(document.body).ready(getData);







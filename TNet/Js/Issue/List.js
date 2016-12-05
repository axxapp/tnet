Pub.checkUser(true);
var issue_data = null;
//获取问题
function getData() {
    var u = Pub.getUser();
    if (u != null) {
        Pub.get({
            url: "Service/Issue/List/" + u.iduser,
            loadingMsg: "加载中...",
            success: function (data) {
               // alert(JSON.stringify(data))
                if (Pub.wsCheck(data)) {
                    if (data.Data) {
                        var html = "";
                        try {
                            var html = "";
                            try {
                                issue_data = data.Data;
                                for (var i = 0; i < data.Data.length; i++) {
                                    var o = data.Data[i];

                                    var uo = o.Issue;
                                    var so = o.Task ? o.Task.statusObj : null;
                                    if (html) {
                                        html += '<div class="vline"></div>';
                                    }
                                    html += '<div class="order_item"  onclick="goDetail(\'' + uo.issue1 + '\')">';
                                    html += '<div class="order">';
                                    html += '<div class="no">受理：' + uo.issue1 + '</div>';//"  (" + uo.tasktype + ")"
                                    html += '<div class="status">' + (so ? so.text : "等待派工") + '</div>';
                                    html += '</div>';

                                    html += '<div class="titem_host">';
                                    html += '<div class="ttitle">姓名:</div>';
                                    html += '<div class="tvalue">' + uo.contact + '</div>';
                                    html += '</div>';

                                    html += '<div class="titem_host">';
                                    html += '<div class="ttitle">电话:</div>';
                                    html += '<div class="tvalue">' + uo.phone + '</div>';
                                    html += '</div>';

                                    html += '<div class="titem_host">';
                                    html += '<div class="ttitle">地址:</div>';
                                    html += '<div class="tvalue">' + uo.addr + '</div>';
                                    html += '</div>';

                                    html += '<div class="titem_host">';
                                    html += '<div class="ttitle">受理:</div>';
                                    html += '<div class="tvalue">' + uo.cretime + '</div>';
                                    html += '</div>';

                                    html += '<div class="titem_host">';
                                    html += '<div class="ttitle">预约:</div>';
                                    html += '<div class="tvalue">' + uo.booktime + '</div>';
                                    html += '</div>';

                                    html += '<div class="titem_host">';
                                    html += '<div class="ttitle">内容:</div>';
                                    html += '<div class="tvalue">' + uo.context + '</div>';
                                    html += '</div>';
                                    if (o.Imgs && o.Imgs.length > 0) {
                                        html += '<div  class="taskimg">';
                                        var z = 0;
                                        for (var j = 0; j < o.Imgs.length; j++) {
                                            if (z++ == 0) {
                                                html += '<div class="taskimg_box">';
                                            }
                                            html += '<img src="' + Pub.url(o.Imgs[j], "Images/default_bg.png") + '" onclick="lookImg(this,' + i + ',event)"/>';
                                            if (z == 4) {
                                                z = 0;
                                                html += "</div>";
                                            }
                                        }
                                        if (z != 0) {
                                            html += '</div>';
                                        }
                                        html += '</div>';
                                    }
                                    if (o.Task) {
                                        html += '<div class="taskPress">';
                                        var to = o.Task;
                                        var ts = [
                                            { t: "受理", v: to.accpeptime },
                                            { t: "派工", v: to.revctime },
                                            { t: "完工", v: to.finishtime },
                                            { t: "回访", v: to.echotime }
                                        ];
                                        html += setTaskPresBar(ts);
                                        html += '</div>';
                                    }
                                    html += '</div>';
                                }
                                if (html) {
                                    $('#order_host').html(html);
                                    return;
                                }
                            } catch (e) {
                                $('#order_host').html("加载异常" + e.message);
                                return;
                            }

                        } catch (e) {
                            $('#order_host').html("加载异常");
                            return;

                        }

                    }
                }
                load_fail("亲,您暂无投诉建议");
            },
            error: function (xhr, status, e) {
                load_fail("加载投诉建议失败");
            }
        });
    }
}


$(document.body).ready(getData);



function load_fail(msg) {
    Pub.noData("#order_host", msg, getData);
}


function goDetail(issue) {
    var u = Pub.getUser();
    if (issue) {
        window.location.href = Pub.url("Issue/Detail?issue=" + issue);
    }
}



function lookImg(obj, j, e) {
    e.stopPropagation();
    var imgs = issue_data[j].Imgs;
    var ms = [];
    if (!imgs || imgs.length <= 0) {
        imgs = ["Images/default_bg.png"];
    }
    if (imgs) {
        for (var i = 0; i < imgs.length; i++) {
            ms.push(Pub.fullUrl(imgs[i]));
        }
    }
    var img = Pub.fullUrl($(obj).attr('src'));
    PreviewImage({
        current: img,
        urls: ms
    });
}
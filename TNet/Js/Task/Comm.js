var __G_IMG_DATA_CACHE = null;
var __title_var = document.title;
//显示工人-派工
function showWorkHost(orderno) {
    var workHost = $(".work_host");
    if (workHost.length <= 0) {
        __title_var = document.title;
        var html = '<div class="work_host">';
        html += '<div class="work_List">';
        html += '<span class="loading_c">加载中...</span>';
        html += '</div>';
        html += '<div class="worker_op">';
        html += '<a id="worker_op_add" class="task" href="javascript:void(0)" onclick="dispTask(\'' + orderno + '\')">派单</a>';
        html += '</div>';
        html += '</div>';
        $(document.body).append(html);
        workHost = $(".work_host");
        getWorker();
    } else {
        workHost.toggle();
    }
    $("#OC").toggle();
    if (!workHost.is(":hidden")) {
        document.title = "派单";
        setTopMenuEvent(showWorkHost, "Top_Menu_Back");
    } else {
        document.title = __title_var;
        setTopMenuEvent();
    }

}


var g_worker_data = null;
//获取工人
function getWorker() {
    Pub.get({
        url: "Service/Mgr/Work/List",
        loadingMsg: "获取工人中...",
        success: function (data) {
            if (Pub.wsCheck(data)) {
                g_worker_data = data.Data;
                var html = "";
                for (var i = 0; i < data.Data.length; i++) {
                    var mg = data.Data[i];
                    html += '<label class="weui_cell weui_check_label" for="worker_' + i + '">';
                    html += '<div class="weui_cell_hd">';
                    html += '<input value="' + mg.mgcode + '" class="weui_check"  id="worker_' + i + '"  type="checkbox">';
                    html += '<i class="weui_icon_checked"></i>';
                    html += '</div>';
                    html += '<div class="weui_cell_bd weui_cell_primary">';
                    html += '<p>' + mg.mgname + '</p>';
                    html += '</div>';
                    html += '</label>';
                }
                if (html) {
                    html = '<div class="weui_cells weui_cells_checkbox">' + html + '</div>';
                    $(".work_List").html(html);
                    return;
                }
            }
            load_worker_fail("暂无工人");
        },
        error: function (xhr, status, e) {
            load_worker_fail("获取工人失败");
        }
    });

}


function load_worker_fail(msg) {
    Pub.noData(".work_List", msg, getWorker);
}




function setTaskPresBar(ts) {
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



//处理进度
function setPress(data) {
    var task = data.Data.task;
    var press = data.Data.press;
    var recver = data.Data.recver;
    var imgs = data.Data.imgs;
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
                        phtml += "<span class='p_oper'>" + ro.mname + "</span>";
                        break;
                    }
                }
            }
            phtml += "<span class='pcretime'>" + getTime(po.cretime) + "</span></div>";
            if (po.pdesc) {
                phtml += "<div class='p_item_desc'>" + po.pdesc + "</div>";
            }
            if (imgs) {
                var mh = "";

                for (var z = 0; z < imgs.length; z++) {
                    var mo = imgs[z];
                    if (mo.outkey == po.idpress) {
                        mh += '<a href="javascript:void(0)" onclick="lookImg(this)" ><img src="' + Pub.url(mo.path, "Images/default_bg.png") + '"/></a>';
                    }
                }
                if (mh) {
                    phtml += '<div class="taskImg">' + mh + '</div>';
                }
            }
            phtml += "</div></div>";
        }
    }
    if (phtml) {
        $(".taskDoPress").html(phtml);
    } else {
        Pub.noData(".taskDoPress", "暂无工单记录");
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



function lookImg(obj) {
    var imgs = __G_IMG_DATA_CACHE;
    var ms = [];
    if (!imgs || imgs.length <= 0) {
        imgs = ["Images/default_bg.png"];
    }
    if (imgs) {
        for (var i = 0; i < imgs.length; i++) {
            ms.push(Pub.fullUrl(imgs[i].path));
        }
    }
    var img = Pub.fullUrl($(obj).children().first().attr('src'));
    
    PreviewImage({
        current: img,
        urls: ms
    });
}
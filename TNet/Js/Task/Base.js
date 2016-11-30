Pub.checkUser(true);


function getOps(so, o) {
    var html = "";
    if (so.ops) {
        var op = so.ops.split("|");

        for (var i = 0; i < op.length; i++) {
            var p = op[i];
            if (p == "pause") {
                html += '<a class="pause" href="javascript:void(0)">暂停</a>';
            } else if (p == "finish") {
                html += '<a class="finish" href="' + Pub.url("Task/Finish?idtask=" + o.idtask) + '">完工</a>';
            }
        }
    }
    return html;
}


function getTimeNum(workTime, unit) {
    if (!unit) {
        unit = "分";
    }
    var tt = "";
    if (!workTime) {
        return tt;
    }
    if (workTime >= 60) {
        var wt = parseInt(workTime / 60);
        if (wt > 24) {
            tt = "超1天";
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
        tt = wt + "分";
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


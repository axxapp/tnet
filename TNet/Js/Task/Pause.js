
//保存
function Save() {
    var context = Pub.str($("#context").val());
    var _c = $.trim(context);
    if (!_c || _c == "") {
        alert("请输入工作描述");
        //$("#context").focus();
        return;
    }
    var u = Pub.getUser();
    var imgs = [];
    for (var i = 1; i <= 4; i++) {
        var mp = $.trim($("#idc_img" + i).attr("title"));
        if (mp) {
            imgs.push(mp);
        }
    }
    var idtask = Pub.urlParam("idtask");
    var idrecver = Pub.urlParam("idrecver");
    if (u != null && u.mu && idtask && idrecver) {
        var data = {
            idtask: idtask,
            idrecver: idrecver,
            mgcode: u.mu.code,
            context: context,
            imgs: imgs
        };
        Pub.post({
            url: "Service/Task/Pause",
            data: data,
            loadingMsg: "保存中...",
            success: function (data) {
                //alert(JSON.stringify(data));
                if (Pub.wsCheck(data)) {
                    alert("保存成功");
                    window.location.href = Pub.url("Task/Detail");
                    return;
                }
                //alert("保存失败,请稍后重试");
            },
            error: function (xhr, status, e) {
                alert("保存失败,请稍后重试");
            }
        });
    }
}

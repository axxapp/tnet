Pub.checkUser(true);

//保存
function Save() {
    var phone = $.trim($("#phone").val());
    if (!phone || phone == "") {
        alert("请输入电话号码");
        return;
    }
    var context = $.trim($("#context").val());
    if (!context || context == "") {
        alert("请输入内容");
        return;
    }
    var booktime = $.trim($("#booktime").val());
    var notes = $.trim($("#notes").val());
    var u = Pub.getUser();
    var imgs = [];
    for (var i = 1; i <= 6; i++) {
        var mp = $.trim($("#idc_img" + i).attr("title"));
        if (mp) {
            imgs.push(mp);
        }
    }
    if (u != null) {
        var data = {
            iduser: u.iduser,
            context: context,
            booktime: booktime,
            phone: phone,
            notes: notes,
            imgs: imgs
        };
        Pub.post({
            url: "Service/Issue/Create",
            data: JSON.stringify(data),
            loadingMsg: "保存中...",
            success: function (data) {
                //alert(JSON.stringify(data));
                if (Pub.wsCheck(data)) {
                    alert("受理成功,我们会尽快处理");
                    window.location.hash = Pub.url("Issue/List");
                    return;
                }
                alert("受理失败,请稍后重试");
            },
            error: function (xhr, status, e) {
                alert("受理失败,请稍后重试");
            }
        });
    }
}

Pub.checkUser(true);

//保存
function Save() {
    var context = Pub.str($("#context").val());
    var _c = $.trim(context);
    if (!_c || _c == "") {
        alert("请输入内容");
        //$("#context").focus();
        return;
    }
    var phone = Pub.str($("#phone").val(), true);
    if (!phone || phone == "") {
        alert("请输入电话号码");
        //$("#phone").focus();
        return;
    }
   
    var booktime = Pub.str($("#booktime").val(), true);
    var notes = Pub.str($("#notes").val());
    var _n = $.trim(notes);
    if (!_n || _n == "") {
        notes = "";
    }
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
                    window.location.href = Pub.url("Issue/List");
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

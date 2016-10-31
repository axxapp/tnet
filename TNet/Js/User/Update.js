Pub.checkUser(true);

function initParam() {
    var u = Pub.getUser();
    if (u != null) {
        //alert(JSON.stringify(u));
        //alert(u.avatar);
        $(".avater").css("background-image", "url(" + u.avatar + ")");
        $(".name").html(u.name);
        $("#alias").val(u.alias);
        $("#phone").val(u.phone);
        $("#comp").val(u.comp);
        //alert(u.sex)
        if (u.sex == 0) {
            $("#sex1").attr("checked", 'true');
            //$("#sex2").removeAttr("checked", 'false');
        } else {
            //$("#sex1").attr("checked", 'false');
            $("#sex2").attr("checked", 'true');
        }
        $("#notes").val(u.notes);

    }
}

//保存
function Save() {
    var u = Pub.getUser();
    if (u != null) {
        var phone = Pub.str($("#phone").val(), true);
        if (!phone || phone == "") {
            alert("请输入电话号码");
            //$("#phone").focus();
            return;
        }
        var alias = Pub.str($("#alias").val(), true);
        var comp = Pub.str($("#comp").val(), true);
        var sex = $("#sex1").is(":checked") ? 0 : 1;
        var notes = Pub.str($("#notes").val(), true);
        if (u.alias == alias && u.phone == phone && u.comp == comp
                                     && u.sex == sex && u.notes == notes) {
            alert("用户信息未发生改变");
            return false;
        }
        var data = {
            iduser: u.iduser,
            alias: alias,
            phone: phone,
            comp: comp,
            sex: sex,
            notes: notes
        };
        Pub.post({
            url: "Service/User/Update",
            data: JSON.stringify(data),
            loadingMsg: "保存中...",
            success: function (data) {
                //alert(JSON.stringify(data));
                if (Pub.wsCheck(data)) {
                    alert("保存成功");
                    Pub.goUser();
                    return;
                }
                alert("保存失败,请稍后重试");
            },
            error: function (xhr, status, e) {
                alert("保存失败,请稍后重试");
            }
        });
    } else {
        alert("用户信息有误");
    }
}

$(document.body).ready(initParam);
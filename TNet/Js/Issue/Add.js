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
    var contact = "";
    var addr = "";
    var phone = "";
    var ao = Pub.getCache("Addr");
    if (ao) {
        contact = ao.contact;
        addr = ao.province + ao.city + ao.district + ao.street;
        phone = ao.phone;
    }
    if (!addr) {
        alert("请选择联系人");
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
            contact: contact,
            phone: phone,
            addr: addr,
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

$(document.body).ready(function () {
    var now = new Date(),
         minDate = new Date(now.getFullYear(), now.getMonth(), now.getDate()),
         maxDate = new Date(now.getFullYear() + 10, 11, 31);

    $('#booktime').mobiscroll().date({
        theme: 'mobiscroll',
        lang: 'zh',
        display: 'bottom',
        minDate: minDate,
        maxDate: maxDate
    });
    loadAddr();
});

var _g_set_addr_tag = false;
function loadAddr() {
    _g_set_addr_tag = true;
    var ao = Pub.getCache("Addr");
    if (ao) {
        var real_addr = ao.province + ao.city + ao.district + ao.street;
        var html = '<i class="iconfont">&#xe615</i>';
        html += '<div class="addrInfo">';
        html += '<div class="npHost">';
        html += '<span class="contacts">' + ao.contact + '</span>';
        html += '<span class="phones">' + ao.phone + '</span>';
        html += '</div>';
        html += '<div class="realAddr">' + real_addr + '</div>';
        html += '</div>';
        html += '<span class="choice"></span>';
        $("#addr").html(html);
    } else {
        var html = '<i class="iconfont">&#xe615</i>';
        html += '<span>请选择地址...</span>';
        html += '<span class="choice"></span>';
        $("#addr").html(html);
    }
}

function selectAddr() {
    $("#OC").toggle();
    showAdrBox();
}
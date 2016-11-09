Pub.checkUser(true);
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



function checkIdc(idc) {
    if (!(/(^\d{18}$)|(^\d{17}([0-9]|[a-zA-Z])$)/.test(idc))) {
        alert('身份证号码格式不对');
        return false;
    }
    return true;
}

function initParam() {
    var href = window.location.href + "";
    
    if ((/(Manage\/MercList)/gi).test(href) || (/(Manage\/MercEdit)/gi).test(href)) {
        $("#MercList").addClass("select");
    } else if ((/(Manage\/MercTypeList)/gi).test(href) || (/(Manage\/MercTypeEdit)/gi).test(href)) {
        $("#MercTypeList").addClass("select");
    } else if ((/(Manage\/OrderList)/gi).test(href)) {
        $("#OrderList").addClass("select");
    } else if ((/(Manage\/BusinessList)/gi).test(href) || (/(Manage\/BusinessEdit)/gi).test(href)) {
        $("#BusinessList").addClass("select");
    }
    else if ((/(Manage\/Menu)/gi).test(href)) {
        $("#Menu").addClass("select");
    }
}

$(document.body).ready(initParam);

function ajax_del(delUrl,postData) {
    $.post(delUrl, postData, function (returndata) {
        returndata = eval("(" + returndata + ")");
        if (returndata.Code == "1") {
            alert(returndata.Message);
            window.location.href = window.location.href;
        }
        else {
            alert(returndata.Message);
        }
    });
}
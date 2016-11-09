
function initParam() {
    if (ru) {
        $(".Jump").show();
        window.location.href = ru;
    } else {

        $("#base").show();
        if (tn_u) {
            $(".avatar").css("background-image", "url(" + tn_u.avatar + ")");
            $(".name").html(tn_u.name);
            setV("phone", tn_u.phone);
            setV("comp", tn_u.comp);
            if (tn_u.sex != null && tn_u.sex != undefined && tn_u.sex != -1) {
                setV("sex", (tn_u.sex == 0) ? "男" : "女");
            }
            setV("cretime", tn_u.cretime);
            setV("notes", tn_u.notes);

            if (tn_u.mu) {
                $("#muser").show();

                setV("muphone", tn_u.mu.phone);
                setV("muname", tn_u.mu.name);

                if (tn_u.mu.recvOrder) {
                    $("#recv_order").attr("checked", 'true');
                }

                if (tn_u.mu.recvReview) {
                    $("#recv_review").attr("checked", 'true');
                }

                if (tn_u.mu.recvSetup) {
                    $("#recv_setup").attr("checked", 'true');
                }

                if (tn_u.mu.sendSetup) {
                    $("#send_setup").attr("checked", 'true');
                }




            }
        }

    }
}

function setV(id, v) {
    if (!v) {
        v = "...";
    }
    $("#" + id).html(v);
}
$(document.body).ready(initParam);


//错误
window.onerror = null;
function checkbox_select_all(all_id,item_name) {
    $("#" + all_id).click(function () {
        $("[name='" + item_name + "']").prop("checked", $(this).prop("checked"));
    });

    $("[name='" + item_name + "']").click(function () {
        if ($("[name='" + item_name + "']:checked").length == $("[name='" + item_name + "']").length) {
            $("#" + all_id).prop("checked", $(this).prop("checked"));
        }
        else {
            $("#" + all_id).prop("checked", false);
        }
    });
}

function checkbox_selected_del(name, delFun) {
    if (!confirm("确定要删除选中的记录吗？")) {
        return;
    }
    var selectedItems=$("[name='" + name + "']:checked");
    if (selectedItems.length == 0) {
        alert("您还未选中任何需要删除的记录。");
        return;
    }
    if (delFun) {
        var values = new Array();
        $(selectedItems).each(function (i, d) {
                values.push($(d).val());
        });
        delFun(values);
    }
}
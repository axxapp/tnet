

function dispTask(issue) {
    var u = Pub.getUser();
    if (u == null || !u.mu) {
        alert("您无权派单");
        return;
    }
    if (g_worker_data) {
        var ws = [];
        for (var i = 0; i < g_worker_data.length; i++) {
            if ($("#worker_" + i).is(":checked")) {
                ws.push(g_worker_data[i].mgcode);
            }
        }
        if (ws.length > 0) {
            Pub.post({
                url: "Service/Task/Dis/Issue",
                data: { issue: issue, mcode: u.mu.code, mname: u.mu.name, works: ws },
                loadingMsg: "派单中...",
                success: function (data) {
                    if (Pub.wsCheck(data)) {
                        alert("派单成功,工单号：" + data.Data);
                        window.location.href = window.location.href + "";// Pub.url("Iss/MyTask");
                    }
                },
                error: function (xhr, status, e) {
                    alert("派单失败");
                }
            });

            return;
        }
    }
    alert("请选择工人");
}
(function () {
    Pub = {
        ajax: _ajax_call,
        call: _ajax_call,
        post: _ajax_post,
        get: _ajax_get,
        jsonDate: jsonDate,
        rootUrl: rootUrl,
        url: url,
        fullUrl: fullUrl,
        urlParam: urlParam,
        wsCheck: wsCheck,//服务检查
        showLoading: showLoading,// 
        showMsg: showMsg,// 
        showError: showError,// 
        hieLoading: hieLoading,
        noData: noData,//无数据
        checkUser: checkUser,
        isHome: isHome,
        auth: auth,
        goUser: goUser,
        onWXLocation: onWXLocation,
        wxJsRead: false,
        cityReady: cityReady,
        doCityReadys: doCityReadys,
        ready: ready,
        doReadys: doReadys,
        onCity: onCity,
        curCity: curCity,
        getCitys: getCitys,
        setCache: setCache,
        getCache: getCache,
        hasCityListen: hasCityListen,
        delCache: delCache,
        setUser: function (v, e) {
            return setCache('tn_u', v, e);
        },
        getUser: function () {
            return getCache('tn_u');
        },
        delUser: function () {
            return delCache('tn_u');
        },
        str: getStr,
        isUserPage: false,
        v: {
            cache: 3
        }


    };
    var full_root_url = "http://app.i5shang.com/tnet/";
    var default_root_url = "";
    var showCount = 0;
    /*****进度条******/
    var msgTimeTag = 0, cur_toast_id = "";
    function showLoading(msg) {
        doShowMsg(msg, 'l', false);
    }
    function showMsg(msg) {
        doShowMsg(msg, 'f', true);
    }
    function showError(msg) {
        doShowMsg(msg, 'e', true);
    }

    function doShowMsg(msg, type, hid) {
        var ix = -1;
        if (type == 'e') {
            if (!msg) {
                msg = "失败了!";
            }
            ix = 1;
        } else if (type == 'l') {
            if (!msg) {
                msg = "处理中...";
            }
            ix = 2;
            showCount++;
        } else {
            ix = 0;
            if (!msg) {
                msg = "已完成";
            }
        }
        var tobj = "";
        var tids = ["#toast", "#toast_e", "#toast_l"]
        for (var i = 0; i < 3; i++) {
            if (i == ix) {
                if (i == 2) {
                    cur_toast_id = tids[i];
                    tobj = null;
                } else {
                    tobj = tids[i];
                }
                $(tids[i]).show();
                $(tids[i] + "_c").html(msg);
            } else {
                $(tids[i]).hide();
            }
        }
        if (hid) {
            clearMsgTime();
            msgTimeTag = window.setTimeout(function () {
                hieLoading(tobj + "");
            }, 1000 * 3);
        }
    }

    function hieLoading(tobj) {
        if (!tobj) {
            tobj = cur_toast_id;
        }
        if (tobj) {
            $(tobj).hide();
            if (tobj == cur_toast_id) {
                cur_toast_id = null;
            }
            showCount--;
            clearMsgTime();
        }
    }

    function clearMsgTime() {
        if (msgTimeTag) {
            window.clearTimeout(msgTimeTag);
            msgTimeTag = 0;
        }
    }



    //加载动画
    function _if_loading(msg) {
        if (showCount <= 0) {
            showLoading(msg);
        }
    }
    /***************获取跟路径******************/

    //获取服务基地址


    //获取服务基地址
    function rootUrl() {
        var root_url = $(document.body).attr("root");
        if (!root_url) {
            root_url = default_root_url;
        }
        root_url = decodeURIComponent(root_url);
        if ((root_url.charAt(root_url.length - 1) != '/')) {
            root_url += "/";
        }
        return root_url;
    }

    function fullUrl(url) {
        if (url) {
            var _url = url.toUpperCase();
            var ru = rootUrl().toUpperCase();
            if ((i = _url.indexOf("/")) == 0) {
                _url = _url.substring(i);
                url = url.substring(i);
            }
            if ((i = ru.indexOf("/")) == 0) {
                ru = ru.substring(i);
            }
            if ((i = _url.indexOf(ru)) == 0) {
                url = url.substring(ru.length);
            }
            return full_root_url + url;
        }
    }

    function url(rurl, durl) {
        if (rurl) {
            try {
                rurl = rurl.trimLeft();
            } catch (e) {

            }
            if (rurl) {
                var ru = rootUrl();
                if (rurl.indexOf(ru) == 0) {
                    return rurl;
                }
                if (rurl.indexOf("~/") == 0) {
                    rurl = ru + rurl.substring(2, rurl.length);
                    return rurl;
                }
                if (rurl.indexOf("/") == 0) {
                    rurl = ru + rurl.substring(1, rurl.length);
                    return rurl;
                }
                return ru + rurl;
            }
        }
        if (!durl) {
            durl = "";
        } else {
            durl = url(durl, "");
        }
        return durl;
    }







    //获取url参数
    function urlParam(key) {
        var reg = new RegExp('[?|&]' + key + '=([^&]+)');
        var match = window.location.search.match(reg);
        return match && match[1];
    }

    /**********Ajax请求***************/

    //ajax请求-跨域解决
    function _ajax_call(request) {
        var jasnObj = false;
        if (request.data && typeof (request.data) == 'object') {
            jasnObj = true;
            request.data = JSON.stringify(request.data);
        }
        request.url = url(request.url);
        var attHead = true;
        try {
            if (request.noAttHead !== undefined) {
                attHead = !request.noAttHead;
            }
        } catch (e) {

        }
        var isJson = false;
        if (attHead && request.headers == undefined) {
            request.headers = {
                accept: "application/json"
            };
        }
        if (attHead && request.headers && request.headers.accept == "application/json") {
            isJson = true;
        }
        if (jasnObj || (attHead && request.contentType == undefined)) {
            request.contentType = "application/json";
        }
        request.xhrFields = {
            withCredentials: true
        };
        request.crossDomain = true;
        request.cache = false;
        request.async = true;
        if (attHead && request.dataType == undefined) {
            request.dataType = "json";
        }
        request.timeout = 1000 * 60 * 3;
        var success = request.success;
        request.success = function (data) {
            hieLoading();
            if (success) {
                try {
                    data = jsonDate(data);
                } catch (e) {

                }
                success(data);
            }
        };
        var error = request.error;
        request.error = function (xhr, status, e) {
            hieLoading();
            if (error) {
                error(xhr, status, e);
            }
        };
        var msg = "";
        var ld = false;
        try {
            ld = request.noLoading;
        } catch (e) {
        }
        try {
            msg = request.loadingMsg;
        } catch (e) {
        }
        if (!ld) {
            _if_loading(msg);
        }
        $.ajax(request);
    }

    //ajax请求-POST-跨域解决
    function _ajax_post(request) {
        request.type = "POST";
        _ajax_call(request);
    }

    //ajax请求-POST-跨域解决
    function _ajax_get(request) {
        request.type = "GET";
        _ajax_call(request);
    }

    //处理Json Date 格式问题
    // data: 如果为 Json 对象会把 /Date(1472173921327+0800)/ to yyyy-MM-dd HH:mm:ss
    // data: 如果为 string 会把 yyyy-MM-dd HH:mm:ss to {jdate:/Date(1472173921327+0800)/,date:Date对象}
    //如：
    //var d = Pub.jsonDate("2016-08-26 09:12:01");
    //d = new Date();
    //alert(d.jdate + "==" + d.date.toLocaleString())
    function jsonDate(data) {
        if (data) {
            if (typeof (data) === 'object') {
                for (var o in data) {
                    var v = data[o];
                    var dr = /(\/Date)[\(]([\d]+)[\+](0800)[)][\/]/gi;
                    if (typeof (v) === 'string') {
                        if (v && v.length > 18 && dr.test(v)) {
                            v = v.substr(6, v.length - 6 - 7) - 0;
                            v = new Date(v);
                            var y = v.getYear() + 1900;
                            var M = v.getMonth() + 1;
                            var d = v.getDate();
                            var h = v.getHours();
                            var m = v.getMinutes();
                            var s = v.getSeconds();
                            M = M <= 9 ? "0" + M : M;
                            d = d <= 9 ? "0" + d : d;
                            if (h >= 0) {
                                h = " " + ((h <= 9) ? " 0" + h : h);
                            }
                            if (m >= 0) {
                                m = ":" + ((m <= 9) ? "0" + m : m);
                            }
                            if (s >= 0) {
                                s = ":" + ((s <= 9) ? "0" + s : s);
                            }
                            v = y + "-" + M + "-" + d + h + m + s;
                            data[o] = v;
                        }
                    } else if (typeof (v) === 'object') {
                        data[o] = jsonDate(v);
                    }
                }
            } else if (typeof (data) === 'string') {
                var dr = /[\d]{4}[-|\/][\d]{1,2}[-|\/][\d]{1,2}/gi;
                if (dr.test(data)) {
                    data = data.replace(/[-]/g, '/');
                    data = new Date(data);
                    if (data) {
                        data = {
                            jdate: "/Date(" + data.getTime() + "+0800)/",
                            date: data
                        };
                    }

                }

            }
        }
        return data;
    }
    /*********检查服务结果*********/


    //检查服务
    function wsCheck(data, showMsg) {
        if (data) {
            if (data.Code == -1000) {
                //_login();
            } else if (data.Code == 1) {
                return true;
            } else {
                if (data.Msg) {
                    alert(data.Msg);
                }
            }
        }
        return false;
    }

    //无数据统一处理
    function noData(id, msg, fun) {
        var obj = $(id);
        if (obj) {
            if (!msg) {
                msg = "暂无数据";
            }
            obj.html("<span class='load_error'>" + msg + "</span>");
            obj.children(":first").click(fun);
        }
    }

    function checkUser(go) {
        var updateUser = Pub.urlParam("updateUser");
        var tn_u = Pub.getUser();
        if (updateUser) {
            var updateUser_time = getCache("updateUser_time");
            if (!updateUser_time || ((new Date).getTime() - updateUser_time) > 1000 * 60 * 10) {
                tn_u = "";
                Pub.delUser();
            }
        }
        if (!tn_u || tn_u == "") {
            auth(go);
            return false;
        }
        return true;
    }
    function isHome() {
        var h = $(document.body).attr("__Is_Home_Tag");
        if (h != undefined) {
            return true;
        }
        return false;
    }

    function auth(go) {
        try {

            var tn_u = Pub.getUser();
            var ru = rootUrl();
            var realu = "";
            //if (!isHome()) {
            // realu = window.location.href + "";
            //}
            var u = "";
            var uurl = "";
            if (!tn_u) {
                return goUser(true, !go, true);
                //uurl = encodeURIComponent(full_root_url + "user?ru=" + encodeURIComponent(realu));
                //u = 'https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxc530ec3ce6a52233&redirect_uri=' + uurl + '&response_type=code&scope=snsapi_userinfo&state=1#wechat_redirect';
                //if (go) {
                //    if (window.navigator.userAgent.indexOf("MicroMesseng") > 0) {
                //        window.location.href = u;
                //    }
                //    // 
                //    return true;
                //}
            } else {
                u = full_root_url + "user" + "?idweixin=" + tn_u.idweixin;
            }
            $(".Top_User").attr("href", u);

            if (tn_u && tn_u.avatar) {
                var uo = $("#Top_User");
                uo.css("background-image", "url(" + tn_u.avatar + ")");
                uo.css("background-size", "1.5em");
            }
        } catch (e) {

        }
        return false;
    }

    function goUser(isReturn, noGo, isSetUserLink) {
        var realu = "";
        //if (!isHome()) {
        var up = false;
        if (isReturn && !Pub.isUserPage) {
            realu = window.location.href + "";

            //if (realu.indexOf("user/index"))
            //alert(realu);
            var reg = /[&]updateUser=1/;
            realu = realu.replace(reg, '');
            up = true;
            //alert(realu);
            //return;
        }
        uurl = encodeURIComponent(full_root_url + "user?ru=" + encodeURIComponent(realu));
        u = 'https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxc530ec3ce6a52233&redirect_uri=' + uurl + '&response_type=code&scope=snsapi_userinfo&state=1#wechat_redirect';
        if (isSetUserLink) {
            $(".Top_User").attr("href", u);
        }
        if (!noGo) {
            if (window.navigator.userAgent.indexOf("MicroMesseng") > 0) {
                if (up) {
                    Pub.setCache("updateUser_time", (new Date()).getTime());
                }
                window.location.href = u;
                return true;
            }
        }
        return false;
    }



    var callFunc = { "common": new Array(), "city": new Array() };
    function hasCityListen() {
        return callFunc.city.length;
    }
    function cityReady(call, param) {

        ready(call, param, "city");
    }

    function doCityReadys() {
        return doReadys("city");
    }


    function ready(callF, param, type) {
        if (!param) {
            param = null;
        }
        if (!type) {
            type = "common";
        }
        for (var i = 0; i < callFunc.length; i++) {
            if (callFunc[type][i].cf == callF) {
                return;
            }
        }
        callFunc[type].push({ cf: callF, p: param });

    }

    function doReadys(type) {
        try {
            for (var co in callFunc) {
                if (type) {
                    if (co != type) {
                        continue;
                    }
                }
                var cfs = callFunc[co];
                var i = 0, j = 0, lg = cfs.length;
                while (i++ < lg) {
                    var fo = cfs[j++];
                    if (type == "city") {
                        doOnCity(fo.cf);
                        j++;
                    } else {
                        //cfs.pop();
                        //fo.cf(fo.p);
                    }
                }
            }
        } catch (e) {

        }
    }
    var isCalling = false;
    function onCity(call) {
        //alert(Pub.wxJsRead);
        cityReady(call);
        if (!Pub.wxJsRead) {
            //onWXLocation();
            //alert("定位中...");
            Pub.showLoading("定位中...");
            window.setTimeout(function () {
                if (!Pub.wxJsRead) {
                    Pub.hieLoading();
                    Pub.wxJsRead = true;
                    doCityReadys();
                }
            }, 1000 * 3);
            //doOnCity(call);
        } else {
            onWXLocation();
        }
    }

    function onWXLocation() {
        try {
            //alert("定位中...");
            //alert("onWXLocation");
            Pub.showLoading("定位中...");
            wx.getLocation({
                type: 'wgs84', // 默认为wgs84的gps坐标，如果要返回直接给openLocation用的火星坐标，可传入'gcj02'
                success: function (res) {
                    Pub.hieLoading();
                    try {
                        var latitude = res.latitude; // 纬度，浮点数，范围为90 ~ -90
                        var longitude = res.longitude; // 经度，浮点数，范围为180 ~ -180。
                        //var speed = res.speed; // 速度，以米/每秒计
                        //var accuracy = res.accuracy; // 位置精度
                        getWXLocation(latitude, longitude);
                        //alert(latitude + "-wx.getLocation(" + longitude);
                        return;
                    } catch (e) {
                    }
                    doCityReadys();
                }, fail: function (res) {
                    Pub.hieLoading();
                    doCityReadys();
                }, cancel: function (res) {
                    Pub.hieLoading();
                    doCityReadys();
                }
            });
        } catch (e) {
            Pub.hieLoading();
            doCityReadys();
        }
    }

    function getWXLocation(latitude, longitude) {
        $.ajax({
            url: 'http://api.map.baidu.com/geocoder/v2/?ak=btsVVWf0TM1zUBEbzFz6QqWF&callback=renderReverse&location=' + latitude + ',' + longitude + '&output=json&pois=0',
            type: "get",
            dataType: "jsonp",
            jsonp: "callback",
            success: function (data) {
                //console.log(data);
                //var province = data.result.addressComponent.province;
                var city = (data.result.addressComponent.city);
                if (city) {
                    setCache("location_city", city);
                }
                doCityReadys();
                //var district = data.result.addressComponent.district;
                //var street = data.result.addressComponent.street;
                //var street_number = data.result.addressComponent.street_number;
                //var formatted_address = data.result.formatted_address;   
            }, error: function () {
                doCityReadys();
            }
        });
    }

    function doOnCity(call) {

        if (!isCalling) {
            isCalling = true;
            var city = curCity();
            if (!city || city == undefined) {
                getCityData(call);
            } else {
                call(city);
                isCalling = false;
            }
        }
    }

    function getCitys() {
        var city = Pub.getCache("city");
        var location_city = Pub.getCache("location_city")
        if (city) {
            if (location_city) {
                for (var i = 0; i < city.length; i++) {
                    if (city[i].city1.indexOf(location_city) >= 0 || location_city.indexOf(city[i].city1) >= 0) {
                        city[i].cur = 1;
                    }
                }

            }
        }
        return city;
    }

    function curCity() {
        var city = getCitys();
        //var location_city = Pub.getCache("location_city")
        if (city) {
            //if (location_city) {
            for (var i = 0; i < city.length; i++) {
                if (city[i] && city[i].cur) {
                    //city[i].cur = 1;
                    return city[i];
                }
            }

            // }
            city = city[0];
        }
        return city;
    }

    function getCityData(call) {
        Pub.get({
            url: "Service/City/List",
            loadingMsg: "加载中...",
            success: function (data) {
                if (Pub.wsCheck(data)) {
                    if (data.Data) {
                        //alert(JSON.stringify(data.Data))
                        Pub.setCache("city", data.Data);
                    }
                }
                var city = curCity();
                call(city);
                isCalling = false;

            },
            error: function (xhr, status, e) {
                call(null);
                isCalling = false;
            }
        });
    }


    //设置缓存/expires 分钟
    function setCache(key, value, expires) {
        if (window.localStorage) {
            if (value) {
                if (!expires || expires == undefined) {
                    expires = 1000 * 60 * 60 * 24;
                } else {
                    expires = expires * 1000 * 60;
                }
                var ex = new Date();
                ex = ex.getTime() + expires;
                var k = "tnet_app_" + key;
                window.localStorage[k] = JSON.stringify({ value: value, expires: ex, v: Pub.v.cache });
                return true;
            } else {
                delCache(key);
            }
        } else {
            //alert("不支持-localStorage");
        }
        return false;
    }


    //获取缓存
    function getCache(key) {
        if (window.localStorage) {
            var k = "tnet_app_" + key;
            var v = window.localStorage[k];
            if (v) {
                v = JSON.parse(v);
                if (v && (v.v != Pub.v.cache || v.expires <= (new Date().getTime()))) {
                    window.localStorage.removeItem(key);
                    v = null;
                    // alert("清空了");
                }
                //
                if (v) {
                    return v.value;
                }
            }
        } else {
            // alert("不支持-localStorage");
        }
        return null;
    }


    //清空缓存
    function delCache(key) {
        if (window.localStorage && key) {
            var k = "tnet_app_" + key;
            window.localStorage.removeItem(k);
            return true;
        } else {
            // alert("不支持-localStorage");
        }
        return false;
    }
    //处理ios半输入状态字乱码问题
    function getStr(str, removeSp) {
        if (str) {
            var s = String.fromCharCode(8198);
            var r = new RegExp("[" + s + "]", "gi");
            str = str.replace(r, "");
        }
        if (removeSp) {
            str = $.trim(str);
        }
        return str;
    }

})();


//错误
window.onerror = function (errorMessage, scriptURI, lineNumber, columnNumber, errorObj) {
    if (errorMessage) {
        //alert(errorMessage + "," + scriptURI + ",lineNumber=" + lineNumber);
    }
    return false;
};
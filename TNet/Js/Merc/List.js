
function initData() {

    var city = Pub.curCity();
    //alert(city);
    getMercList(city);
    getAd("merc", city);
    //Pub.onCity(function (city) {
    //    getMercList(city);
    //});
}




$(document.body).ready(initData);
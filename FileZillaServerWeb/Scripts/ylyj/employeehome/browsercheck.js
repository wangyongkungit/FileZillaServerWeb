
var checkIE = function () {
    var DEFAULT_VERSION = 8.0;
    var ua = navigator.userAgent.toLowerCase();
    var isIE = ua.indexOf("msie") > -1;
    var safariVersion;
    if (isIE) {
        safariVersion = ua.match(/msie ([\d.]+)/)[1];
    }
    if (safariVersion <= DEFAULT_VERSION) {
        return false;
    };
    return true;
}

if (!checkIE()) {
    //$(".total").css("display", "none");
    //$("body").css("background-color", "#777777");
    alert("浏览器版本过低，请使用内核版本 IE9 及以上的浏览器或者更换其他浏览器访问此页面！");
    if (window.stop)
        window.stop();
    else
        document.execCommand("Stop");
}


function SetQualityScore(parameters, employeeNo, type) {
    var currentWidth = $(top.window).width();
    var currentHeight = $(top.window).height();
    var dialogWidth = currentWidth - 200;
    var dialogHeight = currentHeight - 100;
    var _top = (currentHeight - dialogHeight) / 2;
    var _left = (currentWidth - dialogWidth) / 2;
    var dialog = jDialog.iframe("/SpecialtyQualityConfig.aspx?employeeID=" + parameters + "&type=" + type, {
        title: '我的技能设置',
        width: dialogWidth,
        height: dialogHeight,
        top: _top,
        left: _left
    });
};

function SetMyCertificate(employeeID) {
    var dialogWidth = $(top.window).width() - 50;
    var dialogHeight = $(top.window).height() - 50;
    var currentWidth = $(top.window).width();
    var currentHeight = $(top.window).height();
    var _top = (currentHeight - dialogHeight) / 2;
    var _left = (currentWidth - dialogWidth) / 2;
    var dialog = jDialog.iframe("/MaterialConfig.aspx?employeeID=" + employeeID, {
        title: '我的证件设置',
        width: dialogWidth,
        height: dialogHeight,
        top: _top,
        left: _left
    });
};

function TransferTask(prjID, employeeID, amount, taskno) {
    var dialogWidth = 600;
    var dialogHeight = 300;
    var currentWidth = $(top.window).width();
    var currentHeight = $(top.window).height();
    var _top = (currentHeight - dialogHeight) / 2;
    var _left = (currentWidth - dialogWidth) / 2;
    var dialog = jDialog.iframe("/TaskTransfer.aspx?prjID=" + prjID + "&parentEmployeeID=" + employeeID + "&amount=" + amount, {
        title: '任务转移 ' + taskno,
        width: dialogWidth,
        height: dialogHeight,
        top: _top,
        left: _left
    });
};

var ViewPrjFiles = function (prjID, taskNo) {
    vm.projectid = prjID;
    vm.taskno = taskNo;
    setTimeout('changeActive()', 1800);
    //setTimeout('bindClipEvent()', 1600);
};

$("#withdraw").bind("click", function () {
    var dialogWidth = $(top.window).width() - 50;
    var dialogHeight = $(top.window).height() - 50;
    var currentWidth = $(top.window).width();
    var currentHeight = $(top.window).height();
    var _top = (currentHeight - dialogHeight) / 2;
    var _left = (currentWidth - dialogWidth) / 2;
    var dialog = jDialog.iframe("/withdraw.aspx?employeeID=" + $("#hidEmployeeID").val() + "", {
        title: '提现',
        width: dialogWidth,
        height: dialogHeight,
        top: _top,
        left: _left
    })
});
$("#withdrawRecords").bind("click", function () {
    var dialogWidth = $(top.window).width() - 50;
    var dialogHeight = $(top.window).height() - 50;
    var currentWidth = $(top.window).width();
    var currentHeight = $(top.window).height();
    var _top = (currentHeight - dialogHeight) / 2;
    var _left = (currentWidth - dialogWidth) / 2;
    var dialog = jDialog.iframe("/withdrawapprove.aspx?employeeID=" + $("#hidEmployeeID").val() + "", {
        title: '提现记录',
        width: dialogWidth,
        height: dialogHeight,
        top: _top,
        left: _left
    })
});
$("#transactionRecords").bind("click", function () {
    var dialogWidth = $(top.window).width() - 50;
    var dialogHeight = $(top.window).height() - 50;
    var currentWidth = $(top.window).width();
    var currentHeight = $(top.window).height();
    var _top = (currentHeight - dialogHeight) / 2;
    var _left = (currentWidth - dialogWidth) / 2;
    var dialog = jDialog.iframe("/finance/transactionRecords.aspx?employeeID=" + $("#hidEmployeeID").val() + "", {
        title: '交易记录',
        width: dialogWidth,
        height: dialogHeight,
        top: _top,
        left: _left
    })
});

//window.onresize = adjust;
//function adjust() {
//    //console.log(2);
//    //console.log(document.body.clientWidth);
//    //LoadEcharts();
//}

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

var LoadEcharts = function () {
    if (!checkIE()) {
        //$(".total").css("display", "none");
        //$("body").css("background-color", "#777777");
        alert("浏览器版本过低，请使用内核版本 IE9 及以上的浏览器或者更换其他浏览器访问此页面！");
        return;
    }
    var myChart = echarts.init(document.getElementById("chart1"));//   document.getElementById("chart1"));
    var jsonResult;
    $.ajaxSetup({ cache: false });
    $.ajax({
        url: "/MyHandler.ashx",
        async: false,
        type: "post",
        data: { method: "GetEmployeeAccount", employeeID: $("#hidEmployeeID").val() },
        success: function (result) {
            jsonResult = result;
            //console.info(result);
            //for (var item in jsonResult) {
            //    console.info("---"+item);
            //    if (item.name === "剩余") {
            //        $("#lblCanWithdrawAmount").val(item.value);
            //    }
            //}
        },
        error: function (data) {
            console.log(data);
        }
    });
    //var s_data = ["已发", "余额", "转账", "借出"];
    //var myData01 = '[{ "value": "573", "name": "剩余" }, { "value": "402", "name": "已发" },{ "value": "87", "name": "奖罚" }, { "value": "24", "name": "其他" } ]';
    option = {
        tooltip: {
            trigger: "item",
            formatter: "{b}：￥{c}"
        },
        legend: {
            x: "center",
            y: "bottom",
            data: ["剩余", "已发", "奖罚", "其他"]
        },

        series: {
            type: "pie",
            center: ["50%", "40%"],
            radius: ['40%', '65%'],
            label: {
                normal: {
                    show: true,
                    position: "inner",
                    formatter: "{c}"
                }
            },
            selectedMode: "single",
            data: jsonResult,
            color: ['rgb(155,202,99)', 'rgb(216,160,86)', 'rgb(97,160,168)', 'rgb(228,167,164)']
        }
    };
    myChart.setOption(option);
    myChart.on('click', function (param) {
        var index = param.dataIndex;
        if (index === 1) {
            var dialogWidth = $(top.window).width() - 100;
            var dialogHeight = $(top.window).height() - 90;
            var currentWidth = $(top.window).width();
            var currentHeight = $(top.window).height();
            var _top = (currentHeight - dialogHeight) / 2;
            var _left = (currentWidth - dialogWidth) / 2;
            var dialog = jDialog.iframe("/finance/transactionRecords.aspx?employeeID=" + $("#hidEmployeeID").val() + "&type=yf", {
                title: '已发记录',
                width: dialogWidth,
                height: dialogHeight,
                top: _top,
                left: _left
            })
        }
        else if (index === 2) {
            var dialogWidth = $(top.window).width() - 100;
            var dialogHeight = $(top.window).height() - 90;
            var currentWidth = $(top.window).width();
            var currentHeight = $(top.window).height();
            var _top = (currentHeight - dialogHeight) / 2;
            var _left = (currentWidth - dialogWidth) / 2;
            var dialog = jDialog.iframe("/finance/transactionRecords.aspx?employeeID=" + $("#hidEmployeeID").val() + "&type=jf", {
                title: '奖罚记录',
                width: dialogWidth,
                height: dialogHeight,
                top: _top,
                left: _left
            })
        }
        else if (index === 3) {
            var dialogWidth = $(top.window).width() - 100;
            var dialogHeight = $(top.window).height() - 90;
            var currentWidth = $(top.window).width();
            var currentHeight = $(top.window).height();
            var _top = (currentHeight - dialogHeight) / 2;
            var _left = (currentWidth - dialogWidth) / 2;
            var dialog = jDialog.iframe("/finance/transactionRecords.aspx?employeeID=" + $("#hidEmployeeID").val() + "&type=qt", {
                title: '其他',
                width: dialogWidth,
                height: dialogHeight,
                top: _top,
                left: _left
            })
        }
    });
}

$().ready(function () {
    $("#lblCurrentYear").text(new Date().getFullYear());
    LoadEcharts();
});

//需要定义另个全局变量，index：行索引，oldColor：行本来的颜色
var oldColor;
var index;
//思路：在点击某一行时保存这一行本来的背景颜色，和索引，点击另一行时再得到保存的颜色和索引对上次点击行进行背景颜色赋值，再将本次点击行的行索引和背景颜色进行保存，重复此步骤-->
function changeIndex(obj) {
    //第一次点击的时候index为null，需要判断-->
    if (index != null) {
        //设置上次点击的行的原来的背景颜色-->
        document.getElementById("gvProject").rows[index].style.backgroundColor = oldColor;
    }
    //保存本次点击行的行索引和背景颜色-->
    index = obj.rowIndex;
    oldColor = obj.style.backgroundColor;
    //设置点击行的颜色-->
    obj.style.backgroundColor = "#eeee55";
}

$('#gvProject tr td input[id="btnViewPrjFiles"]').on('click', function () {
    var obj = this;
    //第一次点击的时候index为null，需要判断-->
    if (index != null) {
        //设置上次点击的行的原来的背景颜色-->
        document.getElementById("gvProject").rows[index].style.backgroundColor = oldColor;
    }
    //保存本次点击行的行索引和背景颜色-->
    index = obj.parentNode.parentNode.rowIndex;
    oldColor = obj.parentNode.parentNode.style.backgroundColor;
    //设置点击行的颜色-->
    obj.parentNode.parentNode.style.backgroundColor = "#ffffc6";
});

//var oldColor2;
//var index2;

//$("#divFileTabs button").each(function (i) {
//    console.log(i);
//    $("#divFileTabs button")[i].bind("click", function () {
//        $(this).css("color", "red");
//    });
//});

//给tab绑定单击改变背景色事件
var changeActive = function () {
    var list = document.getElementById('divFileTabs');
    var listChild = list.childNodes;
    for (var i = 0; i < listChild.length; i++) {
        listChild[i].addEventListener('click', function () {
            for (var j = 0; j < listChild.length; j++) {
                listChild[j].style.backgroundColor = '#007bff'; //所有tab颜色
            }
            this.style.backgroundColor = '#FF3333';             //选中的tab颜色
        }, false)
    }
};

var clip = null;
var bindClipEvent = function () {
    //clip = new ZeroClipboard.Client();
    //clip.setHandCursor(true);
    //clip.addEventListener("mouseOver", function (client) {
    //    clip.setText($("#aDownload").attr("href"));
    //});
    //clip.addEventListener("complete", function (client, text) {
    //    alert("链接已成功复制到剪贴板！");
    //});
    //clip.glue("copyHref", "clip_container");
};

var GetTrends = function () {
    var jsonTrend;
    $.ajax({
        url: "/MyHandler.ashx",
        async: false,
        type: "get",
        data: { method: "GetTrends", employeeID: $("#hidEmployeeID").val() },
        success: function (data) {
            jsonTrend = data;
        },
        error: function (data) {
            console.log(data);
        }
    });
    return jsonTrend;
};

var appForTrends = new Vue({
    el: "#taskTrendApp",
    data: {
        trends: GetTrends()
    },
    mounted: function () {
        var _this = this;
        this.timer = setInterval(function () {
            _this.trends = GetTrends();
        }, 1100000);
    },
    beforeDestroy: function () {
        if (this.timer) {
            clearInterval(this.timer);
        }
    }
});

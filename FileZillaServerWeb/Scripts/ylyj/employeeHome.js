function SetQualityScore(parameters, employeeNo, type) {
    var dialog = jDialog.iframe("SpecialtyQualityConfig.aspx?employeeID=" + parameters + "&type=" + type, {
        title: '我的技能设置',
        width: 1100,
        height: 610
    });
};

function SetMyCertificate(employeeID) {
    var dialog = jDialog.iframe("MaterialConfig.aspx?employeeID=" + employeeID, {
        title: '我的证件设置',
        width: 1100,
        height: 610
    });
};

function TransferTask(prjID, employeeID, amount) {
    var dialog = jDialog.iframe("TaskTransfer.aspx?prjID=" + prjID + "&parentEmployeeID=" + employeeID + "&amount=" + amount, {
        title: '任务转移',
        width: 800,
        height: 410
    });
};

var ViewPrjFiles = function (prjID) {
    vm.projectid = prjID;
};

$("#withdraw").bind("click", function () {
    var dialog = jDialog.iframe("withdraw.aspx?employeeID=" + $("#hidEmployeeID").val() + "", {
        title: '提现',
        width: 1200,
        height: 610
    })
});
$("#withdrawRecords").bind("click", function () {
    var dialog = jDialog.iframe("withdrawapprove.aspx?employeeID=" + $("#hidEmployeeID").val() + "", {
        title: '提现记录',
        width: 1200,
        height: 610
    })
});

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
        url: "MyHandler.ashx",
        async : false,
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
            data: jsonResult
            //    [
            //    { value: 573, name: "剩余" },
            //    { value: 402, name: "已发" },
            //    { value: 87, name: "奖罚" },
            //    { value: 24, name: "其他" }
            //]
        }

        //title: {
        //    text: '我的账本'
        //},
        //tooltip: {},
        //legend: {
        //    data: ['销量']
        //},
        //xAxis: {
        //    data: ["衬衫", "羊毛衫", "雪纺衫", "裤子", "高跟鞋", "袜子"]
        //},
        //yAxis: {},
        //series: [{
        //    name: '销量',
        //    type: 'bar',
        //    data: [5, 20, 36, 10, 10, 20]
        //}]
    };
    myChart.setOption(option);
    myChart.on('click', function (param) {
        var index = param.dataIndex;
        alert(index);
    });
}

$().ready(function () {
    LoadEcharts();
});
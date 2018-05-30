$(function () {
    $("#withdraw").bind("click", function () {
        var currentBalance = $("#lblCanWithdrawAmount").text();
        var withdrawAmount = $("#txtWithdrawAmount").val();
        console.log(typeof currentBalance);
        console.log(typeof withdrawAmount);
        if (parseFloat(currentBalance) < parseFloat(withdrawAmount)) {
            $("#cantWithdraw").dialog({
                resizable: false,
                height: 240,
                modal: true
            })
        }
        else if (withdrawAmount == 0) {
            $("#withdrawAmountValid").dialog({
                resizable: false,
                height: 240,
                modal: true
            })
        }
        else {
            $("#dialog-confirm").dialog({
                resizable: false,
                height: 240,
                modal: true,
                buttons: {
                    "确定": function () {
                        Withdraw();
                        $(this).dialog("close");
                    },
                    "取消": function () {
                        $(this).dialog("close");
                    }
                }
            })
        }
    });
});

var Withdraw = function () {
    $.ajax({
        url: "MyHandler.ashx",
        async: false,
        type: "post",
        data: { method: "Withdraw", employeeID: $("#hidEmployeeID").val(), withdrawAmount: $("#txtWithdrawAmount").val() },
        success: function (result) {
            console.log(result);
            if (result.result == 1) {
                alert("申请成功!");
            }
            else {
                alert("申请失败!");
            }
        },
        error: function (data) {
            console.log(data);
        }
    })
};
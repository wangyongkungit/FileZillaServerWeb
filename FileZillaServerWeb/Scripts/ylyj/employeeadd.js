function AlertDialog(msg, result) {
    var dialog = jDialog.alert(msg, {
        handler: function (button, dialog) {
            dialog.close();
            //为True时，自动跳转到登录页
            if (result == 'True') {
                //window.location.href = 'EmployeeAdd.aspx';
            }
        }
    }, {
        showShadow: true,// 对话框阴影
        wobbleEnable: true,
        events: {
            show: function (evt) {
                var dlg = evt.data.dialog;
            },
            close: function (evt) {
                var dlg = evt.data.dialog;
            },
            enterKey: function (evt) {
                alert('enter key pressed!');
            },
            escKey: function (evt) {
                alert('esc key pressed!');
                evt.data.dialog.close();
            }
        }
    });
}

//检查钉钉ID是否填写
function CheckInputDdId() {
    var val = $("#txtDingtalkUserId").val();
    if (val == "") {
        AlertDialog("请填写钉钉UserId！", null);
        //$("#txtDingtalkUserId").focus();
        return false;
    }
}
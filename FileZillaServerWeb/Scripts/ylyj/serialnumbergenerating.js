//$(document).ready(function(){
//    $("#bottom").removeClass("fixed");
//    var contentHeight=document.body.scrollHeight;
//    var winHeight=window.innerHeight;
//    if(winHeight<contentHeight){
//        $("#bottom").addClass("fixed");
//    }
//    else{
//        $("#bottom").removeClass("fixed");
//    }
//})

//function copyText(objId) {
//    try {
//        var obj = document.getElementById(objId);
//        var rng = document.body.createTextRange();
//        rng.moveToElementText(obj);
//        rng.scrollIntoView();
//        rng.select();
//        rng.execCommand("Copy");
//        rng.collapse(false);
//        alert("已经复制到粘贴板!你可以使用Ctrl+V 贴到需要的地方去了哦!");
//    } catch (e) {
//        alert("您的浏览器不支持此复制功能，请选中相应内容并使用Ctrl+C进行复制!");
//    }
//}

//$(document).ready(function () {
//    InitProvince("ddlProvince");
//});
////获取当前网址
//var path = document.URL;
////获取主机之后的目录
//var pathName = document.location.pathname;
//var position = path.indexOf(pathName);
////
//var localHostPath = path.substr(0, position);
////xml地址
//var xmlPath = localHostPath + "/Config/Area.xml";
//loadProvince(xmlPath);
//function loadProvince(path) {
//    var xmlDoc = null;
//    xmlDoc = jQuery.get(path, function (date) {
//        var $s1 = $("#ddlProvince");
//        var root = jQuery(date).find("address")[0];
//        $(root).children("province").each(function () {
//            appendOptionTo($s1, $(this).attr("name"), $(this).attr("code"));
//        });
//        function appendOptionTo($o, k, v, d) {
//            var $opt = $("<option>").text(k).val(v);
//            $opt.appendTo($o);
//        }
//    });
//}
//function catch_keydown(sel) {
//    switch (event.keyCode) {
//        case 13:
//            //Enter; 
//            sel.options[sel.length] = new Option("", "", false, true);
//            event.returnValue = false;
//            break;
//        case 27:
//            //Esc; 
//            alert("text:" + sel.options[sel.selectedIndex].text + ", value:" + sel.options[sel.selectedIndex].value + ";");
//            event.returnValue = false;
//            break;
//        case 46:
//            //Delete; 
//            if (confirm("删除当前选项!?")) {
//                sel.options[sel.selectedIndex] = null;
//                if (sel.length > 0) {
//                    sel.options[0].selected = true;
//                }
//            }
//            event.returnValue = false;
//            break;
//        case 8:
//            //Back Space; 
//            var s = sel.options[sel.selectedIndex].text;
//            sel.options[sel.selectedIndex].text = s.substr(0, s.length - 1);
//            event.returnValue = false;
//            break;
//    }

//}
//function catch_press(sel) {
//    sel.options[sel.selectedIndex].text = sel.options[sel.selectedIndex].text + String.fromCharCode(event.keyCode);
//    event.returnValue = false;
//}
//$(document).ready(function () {
//    $('#ddlValuateSoftware').editableSelect({
//        effects: 'slide'
//    });
//})
//清空文本框
function ClearAllTextBox() {
    var obj = window.document.forms[0];
    for (i = 0; i < obj.elements.length; i++) {
        var elem = obj.elements[i];
        if (elem) {
            if (elem.type == "text") {
                elem.value = "";
            }
            //select的type是select-one，而非select
            else if (elem.type == "select-one") {
                elem.options[0].selected = true;
            }
        }
    }
}
//压缩
function Compress(type, ID, i, finishedPerson) {
    //禁用按钮
    if (type == 0) {
        $("#btnProjectCompress").attr("disabled", true);
    }
    else if (type == 1) {
        //$("#btnCmprs" + i).attr("disabled", true);
    }
    $("#loadingProject").css("visibility", "visible");
    $.ajax({
        url: "/AttachmentHandler.ashx",
        type: "POST",
        data: { "RequestType": "AjaxRequest", "Method": "Compress", "Type": type, "ID": ID, "FinishedPerson": finishedPerson },
        success: function (data) {
            $("#loadingProject").css("visibility", "hidden");
            var dataJson = eval("(" + data + ")");
            if (dataJson.result == '2') {
                AlertDialog('人人为我，我为人人。他人已进行过压缩，可直接下载。', null);
            }
            else if (dataJson.result == '1') {
                AlertDialog('压缩完成！', null);
            }
            else if (dataJson.result == '0') {
                AlertDialog('压缩失败，未找到待压缩的目录！', null);
            }
            else {
                AlertDialog('压缩失败，原因未知。', null);
            }
        },
        complete: function (data) {
            if (type == "0") {
                $("#btnProjectDownload").attr("disabled", false);
                $("#btnProjectDownload").removeAttr("title");
            }
            else if (type == "1") {
                $("#btnModifyDownload" + i).attr("disabled", false);
                $("#btnModifyDownload" + i).removeAttr("title");
            }
        }
    });
}
////下载
//function Download(type, ID) {
//    $("#hidTaskType").val(type);
//    $("#hidProjectOrModifyID").val(ID);
//    $("#btnDownload").click();
//}
////删除附件
//function Delete(ID) {
//    $("#hidDeleteID").val(ID);
//    $("#btnModifyDelete").click();
//}
//验证是否选择了需要上传的文件
function ValidateUpload(fupId) {
    var val = $('#' + fupId).val();
    if (val == '') {
        AlertDialog('请选择文件！', null);
        return false;
    }
    //ToggleVisibility('progress', 'on')
}
//文件替换
function FileReplace(id, fileName) {
    $("#hidFileReplaceId").val(id);
    $("#hidFileReplaceName").val(fileName);
    document.getElementById("btnFileReplace").click();
}
function AlertDialog(msg, result, projectId) {
    if (result == 'InvokeCreateFolder') {
        $.ajax({
            url: "/HttpHandler/FileHandler.ashx",
            type: "POST",
            data: { "RequestType": "AjaxRequest", "FuncName": "AddFileCategory", "projectId": projectId, "categoryId": "1", "description": "任务书", "expiredate":"" },
            success: function (data) {
                vm.projectid = projectId;
                console.log("rws:" + data.Code);
            },
            complete: function (data) {

            }
        });
        // 2018-06-11 19-36-02 修改，默认不建完成稿
        //$.ajax({
        //    url: "/HttpHandler/FileHandler.ashx",
        //    type: "POST",
        //    data: { "RequestType": "AjaxRequest", "FuncName": "AddFileCategory", "projectId": projectId, "categoryId": "2", "description": "完成稿" },
        //    success: function (data) {
        //        vm.projectid = projectId;
        //        console.log("wcg:" + data.Code);
        //    },
        //    complete: function (data) {

        //    }
        //})
    }
    var dialog = jDialog.alert(msg, {
        handler: function (button, dialog) {
            dialog.close();
            //为True时，自动跳转到登录页
            if (result == 'True') {
                window.location.href = 'Login.aspx';
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
//订单时间错误设置获取焦点
function AlertDialogOrderErr(msg) {
    var dialog = jDialog.alert(msg, {
        handler: function (button, dialog) {
            dialog.close();
            $("#ContentPlaceHolder1_txtOrderDate").focus();
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
//删除任务时专用Confirm
function AlertConfirm() {
    var dialog = jDialog.confirm('确定要删除吗？', {
        handler: function (button, dialog) {
            dialog.close();
            $("#btnDelete").click();//调用服务器删除任务的button的click事件将任务删除
        }
    },
        {
            handler: function (button, dialog) {
                dialog.close();
            }
        })
}

function AlertConfirm2() {
    var dialog = jDialog.confirm('确定要删除吗？', {
        handler: function (button, dialog) {
            dialog.close();
            return true;
        }
    },
        {
            handler: function (button, dialog) {
                dialog.close();
                return false;
            }
        })
}

function Confirm() {
    if (confirm('确定要删除吗？')) {
        return true;
    }
    else {
        return false;
    }
}
//版权信息当前年份
$(document).ready(function () {
    //$("#lblCurrentYear").text(new Date().getFullYear());

    //判断projectID是否为空，对“同步生成目录”单选按钮的显示与隐藏进行控制
    if ($("#hidProjectID").val() != null && $("#hidProjectID").val() != "") {
        // ↓ 之前采用的方法，后来想到可以通过移除required属性实现
        //$("#rdbNoSync").attr("checked","checked");//默认要选一个，否则表单验证难以提交
        $("#rdbSync").removeAttr("required");
        $("#rdbNoSync").removeAttr("required");
        $("#divSync").css("visibility", "hidden");

        vm.projectid = $("#hidProjectID").val();
    };

    var btnDelete = document.getElementById("btnDeleteClient");
    if ($("#hidIsSuperAdmin").val() != "0") {
        btnDelete.style.display = "none";

        //$("#bottom").removeClass("fixed");
        //var contentHeight=document.body.scrollHeight;
        //var winHeight=window.innerHeight;
        //if(winHeight<contentHeight){
        //    $("#bottom").addClass("fixed");
        //}
        //else{
        //    $("#bottom").removeClass("fixed");
        //}
    };

    //改变退款金额的只读属性
    $("#ddlTransactionStatus").change(function () {
        if ($(this).val() == "5") {
            $("#txtRefund").removeAttr("readonly");
        }
        else {
            $("#txtRefund").attr("readonly", "true");
        }
    });

    $("#ContentPlaceHolder1_txtTimeNeeded").bind("focus", function () {
        // 通过options参数，控制tip对话框
        var dialog = jDialog.tip("【时间提示】<br/>请输入实际能完成的所需时间<br/>每人每天按8小时计算", { //详细请点击<a href='Login.aspx' target='_blank'>此处</a>
            target: $('#ContentPlaceHolder1_txtTimeNeeded'),
            position: 'bottom'
        }, {
                autoClose: 10000
            });
    });

    $("#ContentPlaceHolder1_txtTaskName").bind("focus", function () {
        $(this).select();
    });

    $(".fulltext").bind("click", function () {
        $(".hidediv").toggle();
        var img = $(this);
        if (img.attr("src") === "Images/listdown.jpg") {
            img.attr("src", "Images/listup.jpg");
            img.attr("title", "折叠");
        }
        else if (img.attr("src") === "Images/listup.jpg") {
            img.attr("src", "Images/listdown.jpg");
            img.attr("title", "展开");
        }
    });

    //
    //$("#ContentPlaceHolder1_txtOrderDate").click(function () {
    //    var dialog = jDialog.tip('如需选择日期，请点击右侧的日历图标', {
    //        target: $('#ContentPlaceHolder1_txtOrderDate'),
    //        position: 'left-top',
    //        //trianglePosFromStart: 0,
    //        showShadow: true,
    //        autoClose: true,
    //        offset: {
    //            top: -20,
    //            left: 10,
    //            right: 0,
    //            bottom: 0
    //        }
    //    });
    //});
    var getfilecode = $("#ContentPlaceHolder1_txtTaskName").val();
    var lastfourchar = getfilecode.substr(getfilecode.length - 4);
    getfilecode = lastfourchar.split("").reverse().join("");
    vm.taskno = getfilecode;
})

//切换上传进度DIV的显示与隐藏
function ToggleVisibility(id, type) {
    el = document.getElementById(id);
    if (el.style) {
        if (type == 'on') {
            el.style.display = 'block';
        }
        else {
            el.style.display = 'none';
        }
    }
    else {
        if (type == 'on') {
            el.display = 'block';
        }
        else {
            el.display = 'none';
        }
    }
}

//给tab绑定单击改变背景色事件
var changeActive = function () {
    var list = document.getElementById('divFileTabs');
    if (list) {
        var listChild = list.childNodes;
        if (listChild) {
            for (var i = 0; i < listChild.length; i++) {
                listChild[i].addEventListener('click', function () {
                    for (var j = 0; j < listChild.length; j++) {
                        listChild[j].style.backgroundColor = '#007bff'; //所有tab颜色
                    }
                    this.style.backgroundColor = '#FF3333';             //选中的tab颜色
                }, false)
            }
        }
    }

    //var copyHrefs = document.getElementsByClassName("copyhref");
    //for (var ii = 0; ii < copyHrefs.length; ii++) {
    //    var clipboard = new Clipboard("#" + copyHrefs[0].id);

    //    clipboard.on('success', function (e) {
    //        console.log("succ:" + e);
    //    });
    //    clipboard.on('error', function (e) {
    //        console.log("err:" + e);
    //    });
    //}
    //var clipboard = new Clipboard('#copyHref');

    //clipboard.on('success', function (e) {
    //    console.log("succ:"+e);
    //});
    //clipboard.on('error', function (e) {
    //    console.log("err:"+e);
    //});
}
setTimeout('changeActive()', 1200);

var clip = null;
var bindClipEvent = function () {
    //clip = new ZeroClipboard.Client();
    //clip.setHandCursor(true);
    //clip.addEventListener("mouseOver", function (client) {
    //    clip.setText("4444433333");
    //});
    //clip.addEventListener("complete", function (client, text) {
    //    alert("链接已成功复制到剪贴板！");
    //});
    //clip.glue("copyHref", "clip_container");
};

//var CopyUrl = function (copyText) {
//    bindClipEvent(copyText);
//};

//var ShareLink = function (fileHistoryId, getcode) {
//    var text = "链接：http://bimpan.iok.la:8/FileOperation/FileShare.aspx?fileHistoryId=" + fileHistoryId + " 提取码：" + getcode + "";
//    $("#divCopyText" + fileHistoryId).dialog({
//        resizable: false,
//        height: 240,
//        modal: true
//    })
//}
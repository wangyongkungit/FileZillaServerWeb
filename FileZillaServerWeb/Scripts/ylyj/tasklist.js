
$(document).ready(function () { //这个就是传说的ready  
    $(".tbl tr").mouseover(function () {//.not(':eq(0)')
        //如果鼠标移到class为stripe的表格的tr上时，执行函数
        $(this).addClass("over");
    }).mouseout(function () {
        //给这行添加class值为over，并且当鼠标一出该行时执行函数  
        $(this).removeClass("over");
    }) //移除该行的class  
    //$(".tbl1 tr:even").addClass("alt");
    //给class为stripe的表格的偶数行添加class值为alt
    //$(".tbl1 tr:odd").addClass("alt2");
});
/*
//获取当前网址
var path = document.URL;
//获取主机之后的目录
var pathName = document.location.pathname;
var position = path.indexOf(pathName);
//
var localHostPath = path.substr(0, position);
//xml地址
var xmlPath = localHostPath + "/Config/Area.xml";
loadProvince(xmlPath);
function loadProvince(path) {
    var xmlDoc = null;
    xmlDoc = jQuery.get(path, function (date) {
        var $s1 = $("#ddlProvince");
        var root = jQuery(date).find("address")[0];
        $(root).children("province").each(function () {
            appendOptionTo($s1, $(this).attr("name"), $(this).attr("code"));
        });
        function appendOptionTo($o, k, v, d) {
            var $opt = $("<option>").text(k).val(v);
            $opt.appendTo($o);
        }
    });
}
*/
//展开/折叠操作
function Expand(obj0, obj) {
    //var obj = document.getElementById("ttt");
    //obj.style.display = "none";
    //if (obj.nextSibling.style.display = "none") {
    //    obj.nextSibling.style.display = "block";
    //}
    //obj.next().hide();
    //alert( obj.nextSibling.className);
    //obj.nextSibling.css("display", "none");
    //$("#ttt").css("display", "none");
    // alert(obj);
    //  $("#obj").next(".test").css("display","block");
    // $("#obj").toggle();
    //alert($("ex"+obj).id);
    $("#" + obj).toggle();
}
//$(document).ready(function () {
//    $(".expand").click(function () {
//        //if ($(".expand").innerText() == "展开") {
//        //    $(".expand").innerText() == "隐藏";
//        //}
//        //else {
//        //    $(".expand").innerText() == "展开";
//        //}
//        if (document.getElementById("io").innerText == "展开") {
//            document.getElementById("io").innerText == "隐藏";
//        }
//        else {
//            document.getElementById("io").innerText == "展开";
//        }
//        $(".test").toggle();
//    });
//});
//全选
function CheckAll(e, itemname) {
    var item = document.getElementsByName(itemname);
    if (item == undefined) return;
    for (var i = 0; i < item.length; i++) item[i].checked = e.checked;
}
//版权信息设置当前年份
$(document).ready(function () {
    $("#lblCurrentYear").text(new Date().getFullYear());
    ////设置底部
    //$("#bottom").removeClass("fixed");
    //var contentHeight = document.body.scrollHeight;
    //var winHeight = window.innerHeight;
    //if (winHeight < contentHeight) {
    //    $("#bottom").addClass("fixed");
    //}
    //else {
    //    $("#bottom").removeClass("fixed");
    //}

})

//排序
function GetSort(field) {
    $("#sortTitle").val(field);
    var sortOrder = $("#ContentPlaceHolder1_sortOrder").val();
    if (sortOrder == " ASC") {
        $("#ContentPlaceHolder1_sortOrder").val(" DESC");
    }
    else {
        $("#ContentPlaceHolder1_sortOrder").val(" ASC");
    }
    __doPostBack("Sort", field);
}

function EditTransactionStatus(spanId, projectId) {
    $("#" + spanId).hide();
    $("#ddl_" + projectId).show();
    LoadTransactionStatus(spanId, "#ddl_" + projectId, projectId, $("#" + spanId).text());
}

function LoadTransactionStatus(spanId, selectId, projectId, currentStatus) {
    var str = "";
    $.ajaxSetup({ cache: false });
    $.ajax({
        url: "/MyHandler.ashx",
        async: false,
        type: "post",
        data: { method: "GetAllTransactionStatus" },
        success: function (result) {
            for (var index in result) {
                if (result[index].value === currentStatus) {
                    str += "<option value=\"" + result[index].key + "\" selected=\"selected\">" + result[index].value + "</option>";
                }
                else {
                    str += "<option value=\"" + result[index].key + "\" >" + result[index].value + "</option>";
                }
            }
            $(selectId).html("").append(str);

            $(selectId).on("change", function () {
                UpdateTransactionStatusByProjectId(projectId, selectId, $(selectId + ' option:selected').val());
                $(selectId).hide();
                $("#" + spanId).text($(selectId + ' option:selected').text()).show();
                $(selectId).unbind("change");
            });
        },
        error: function (data) {
            console.log(data);
        }
    });
}

function UpdateTransactionStatusByProjectId(projectId, selectId, newStatus) {
    $.ajax({
        url: "/MyHandler.ashx",
        async: false,
        type: "post",
        data: { method: "UpdateTransactionStatusByProjectId", projectId: projectId, newStatus: newStatus },
        success: function (result) {
            if (result.success === "true") {
                $(selectId + " option:selected").removeAttr("selected");
                $(selectId).find("option[value='" + newStatus + "']").attr("selected", "selected");
                alert("交易状态更新成功！");
            }
            else {
                console.log(result);
                alert("更新失败！");
            }
        },
        error: function (data) {
            console.log(data);
            alert("更新失败！");
        }
    });
}

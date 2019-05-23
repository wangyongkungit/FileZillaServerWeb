//import { error } from "util";

//Init Vue
function init() {
    //Note:this must be Vue
    // console.log(this);
    // this.projectid = "cdc19ae8-fa7b-49ef-9733-7666b3337e68";
    this.projectid = "";
    this.roleid = 1;
    this.newtab.categoryselected = -1;

    this.newtab.category = funcList["getCategoryList"]["func"]();
    this.showfile = true;
    this.showaddtab = this.showhistory = false;
}


function refreshProject(p1) {
    //funcList["getHistoryData"]["func"].call(this, p1);
    funcList["getFileTabs"]["func"].call(this, p1);
    funcList["getFiles"]["func"].call(this, p1);
    this.newtab.categoryselected = this.newtab.replytoselected = -1;
    this.showreply = false;
    this.projectfile.parentId = "";
}
//get filetabs 
function getFileTabsByProjectid(p1) {

    let _this = this;
    if (p1.projectid != "") {
        if (p1.projectid === undefined) {
            alert("invalid projectid");
            return;
        }
        let handlerurl = funcList["getFileTabs"]["interface"] + '&projectId=' + p1.projectid;;

        $.ajax({
            // type: "get",
            async: true,
            url: handlerurl,
            dataType: 'jsonp',
            crossDomain: true,
            beforeSend: function () {
                $("#loading").css("display", "block");
            },
            success: function (data) {
                if (data.Code === 0) {
                    _this.projectfile.filetabs = data.Result;
                } else {
                    alert("failed");
                }
            },
            complete: function () {
                $("#loading").css("display", "none");
            }
        });
    }
}

// 获取尚未创建完成稿的修改任务
function getExistFileNotFinishCategory(projectId) {
    var taskNotFinished = "";
    let prameters = "&projectId=" + projectId;
    let handlerurl = funcList["getExistFileNotFinishCategory"]["interface"] + prameters;

    $.ajax({
        // 改为同步
        async: false,
        url: handlerurl,
        dataType: 'jsonp',
        // 跨域不支持同步，因此注释
        //crossDomain: true,
        success: function (data) {
            if (data.Code === 0) {
                taskNotFinished = data.Result[0];
            } else {
                console.log("get not finished modify task failed");
            }
        }
    });
    return taskNotFinished;
}

//get files
function getFiles(p1) {
    let _this = this;
    let prameters = "&projectId=" + p1.projectid;
    let handlerurl = funcList["getFiles"]["interface"] + prameters;

    $.ajax({
        // type: "get",
        async: true,
        url: handlerurl,
        dataType: 'jsonp',
        crossDomain: true,
        success: function (data) {
            if (data.Code === 0) {
                //console.log("getFiles:", data.Result);
                _this.projectfile.files = data.Result;
            } else {
                alert("failed");
            }
        }
    });
}

/***
 * 
 * projecthistory
 */

function getHistoryData(p1) {
    let _this = this;
    let prameters = "&projectId=" + p1.projectid;
    let handlerurl = funcList["getHistoryData"]["interface"] + prameters;

    $.ajax({
        // type: "get",
        async: true,
        url: handlerurl,
        dataType: 'jsonp',
        crossDomain: true,
        beforeSend: function () {
            $("#loading").css("display", "block");
        },
        success: function (data) {
            if (data.Code === 0) {
                _this.projecthistory.data = data.Result;
            } else {
                alert("failed");
            }
        },
        complete: function () {
            $("#loading").css("display", "none");
        }
    });
}


/**
 * add new tab related
 * 
 */
//get fixed categoty list
function getCategoryList() {
    var categoryList = [];
    categoryList.push({
        "key": -1,
        "value": "请选择"
    });
    categoryList.push({
        "key": 2,
        "value": "完成稿"
    });
    categoryList.push({
        "key": 3,
        "value": "修改"
    });
    categoryList.push({
        "key": 4,
        "value": "修改完成稿"
    });
    categoryList.push({
        "key": 5,
        "value": "疑问"
    });
    categoryList.push({
        "key": 6,
        "value": "疑问回复"
    });
    return categoryList;
}

//get reply data
function getReplytodata(p1) {
    // let p1 = {
    //     "projectid": this.projectid,
    //     "categoryid": this.categoryselected
    // };
    let prameters = "&projectId=" + p1.projectid + "&categoryId=" + p1.categoryid;
    let handlerurl = funcList["addReplytoTab"]["interface"] + prameters;
    let _this = this;
    $.ajax({
        // type: "get",
        async: true,
        url: handlerurl,
        dataType: 'jsonp',
        crossDomain: true,
        success: function (data) {
            console.log(data);
            if (data.Code === 0) {
                //console.log("getReplytodata", "sucuess");
                _this.newtab.replyto = data.Result;
            }
            else {
                console.log("getReplytodata", "fail");

            }
        }
    });
}

//get new tab by category
function addNewTab(p1) {
    let _this = this;
    let prameters = "&projectId=" + p1.projectid + "&categoryId=" + p1.categoryid + "&description=" + p1.description + "&parentId=" + p1.parentid + "&expiredate=" + p1.expiredate;
    let handlerurl = funcList["addNewTab"]["interface"] + prameters;

    $.ajax({
        // type: "get",
        async: true,
        url: handlerurl,
        dataType: 'jsonp',
        crossDomain: true,
        success: function (data) {
            _this.newtab.returnmessage = data.Message;
            if (data.Code === 0) {
                //console.log("success");
                _this.newtab.categoryselected = -1;
                _this.newtab.desc = "";
                //refresh file tab data
                funcList["getFileTabs"]["func"].call(_this, p1);
                document.getElementById("fileTabTitle").click();
            } else {
                console.log("addNewTab", "failed");
            }
        }
    });
}

//add Reply Tab
function addReplytoTab(p1) {
    let _this = this;
    let prameters = "&projectId=" + p1.projectid + "&categoryId=" + p1.categoryid + "&description=" + p1.description + "&parentId=" + p1.parentid + "&expiredate=" + p1.expiredate;
    let handlerurl = funcList["addNewTab"]["interface"] + prameters;

    $.ajax({
        // type: "get",
        async: true,
        url: handlerurl,
        dataType: 'jsonp',
        crossDomain: true,
        success: function (data) {
            _this.newtab.returnmessage = data.Message;
            if (data.Code === 0) {
                funcList["getFileTabs"]["func"].call(_this, p1);
                funcList["getReplytodata"]["func"].call(_this, p1);
                _this.newtab.replytoselected = -1;
                _this.newtab.desc = "";
                document.getElementById("fileTabTitle").click();
            } else {
                console.log("addReplytoTab", "failed");
            }
        }
    });
}

// 删除文件
function deleteFile(p1) {
    if (!confirm("确认要删除吗？")) {
        return;
    }
    let _this = this;
    let prameters = "&fileHistoryId=" + p1.filehistoryid;
    let handlerurl = funcList["deleteFile"]["interface"] + prameters;

    $.ajax({
        // type: "get",
        async: true,
        url: handlerurl,
        dataType: 'jsonp',
        crossDomain: true,
        success: function (data) {
            if (data.Code === 0) {
                let p1 = {
                    projectid: _this.projectid
                };
                funcList["getFiles"]["func"].call(_this, p1);
                //funcList["getHistoryData"]["func"].call(_this, p1);
                alert("删除成功！");
            } else {
                alert("failed");
            }
        }
    });
}

// 下载文件
function downloadFile(p1) {
    let _this = this;
    let prameters = "&fileHistoryId=" + p1.filehistoryid;
    let handlerurl = funcList["downloadFile"]["interface"] + prameters;

    $.ajax({
        async: true,
        url: handlerurl,
        dataType: 'jsonp',
        crossDomain: true,
        success: function (data) {
            alert("successl d");
        }
    });
}

// 预览文件
function previewFile(p1) {
    let _this = this;
    let prameters = "&fileHistoryId=" + p1.filehistoryid;
    let handlerurl = funcList["previewFile"]["interface"] + prameters;
    switch (p1.fileExt.toLowerCase()) {
        case ".doc":
        case ".docx":
        case ".xls":
        case ".xlsx":
        //case ".rtf":
            PreviewOffice(p1.filehistoryid, p1.fileExt, handlerurl);
            break;
        case ".jpg":
        case ".png":
        case ".bmp":
        case ".jpeg":
        case ".gif":
        case ".txt":
        case ".pdf":
            PreviewOtherFile(p1.filehistoryid);
            break;
        case ".zip":
        case ".rar":
        case ".7z":
            PreviewFileOnline(p1.filehistoryid);
            break;
        default:
            alert("很抱歉，暂不支持此类型文件的预览！");
            break;
    }
}

// 预览 Office 文件
var PreviewOffice = function (fileHistoryId, ext, handlerurl) {
    $.ajax({
        async: true,
        url: handlerurl,
        dataType: 'jsonp',
        crossDomain: true,
        beforeSend: function (data) {
            $("#loadingimg" + fileHistoryId).css("display", "inline-block");
        },
        success: function (data) {
            if (data.Code === 0) {
                console.log(data.Code);
                console.log(data);
                $("#aPreview").attr("href", "/FilePreview/" + fileHistoryId + ".html");
                document.getElementById("aPreview").click();
            } else {
                alert("操作失败！");
            }
        },
        complete: function (result) {
            $("#loadingimg" + fileHistoryId).css("display", "none");
        }
    });
}

// 预览其他类型的文件
var PreviewOtherFile = function (fileHistoryId) {
    $("#aPreview").attr("href", "/HttpHandler/FilePreview.aspx?fileHistoryId=" + fileHistoryId);
    document.getElementById("aPreview").click();
}

// 使用在线预览服务
var PreviewFileOnline = function (fileHistoryId) {
    $("#aPreview").attr("href", "http://ow365.cn/?i=16255&furl=http://bimpan.iok.la:8/HttpHandler/FileHandler.ashx?FuncName=DownloadFile&fileHistoryId=" + fileHistoryId);
    document.getElementById("aPreview").click();
}

// 分享链接
var ShareLink = function (fileHistoryId) {
    $("#linkContent").bind("focus", function () {
        $(this).select();
    });
    console.log(fileHistoryId);
    $("#divCopyText" + fileHistoryId).dialog({
        resizable: true,
        width: 520,
        height: 440,
        modal: true
    });
    $(".ui-resizable").css("height", "340px");
    $(".ui-widget-content").css("height", "220px");
    var currentWidth = $(window).width();
    var currentHeight = $(window).height();
    $(".ui-resizable").css("left", ((currentWidth - 520) / 2) + "px").css("top", ((currentHeight - 440) / 2) + "px");
}

// 判断是否发送过提醒
function GetIsRemind(filehistoryid) {
    let _this = this;
    let prameters = "&categoryId=" + filehistoryid;
    let handlerurl = funcList["GetIsRemind"]["interface"] + prameters;

    if (!filehistoryid) {
        alert("请选中需要提醒的修改任务！");
        return;
    }

    $.ajax({
        async: true,
        url: handlerurl,
        dataType: 'jsonp',
        crossDomain: true,
        success: function (data) {
            if (data.Result[0].IsRemind === true) {
                if (confirm("已经发送过提醒，确认再次提醒吗？")) {
                    sendDingtalkMessage(filehistoryid, data.Result[0].Id);
                }
            }
            else {
                $("#myAlert").removeClass("hidealert");
                window.setTimeout(function () {
                    $("#myAlert").addClass("hidealert");
                }, 10000);
            }
        },
        error: function () {

        }
    });
}

// 再次发送提醒
function sendDingtalkMessage(filehistoryid, taskremindid) {
    let prameters = "&categoryId=" + filehistoryid + "&taskRemindId=" + taskremindid;
    let handlerurl = funcList["sendDingtalkMessage"]["interface"] + prameters;

    $.ajax({
        async: true,
        url: handlerurl,
        dataType: 'jsonp',
        crossDomain: true,
        success: function (data) {
            if (data.Code === 0) {
                $("#myAlert").removeClass("hidealert");
                window.setTimeout(function () {
                    $("#myAlert").addClass("hidealert");
                }, 10000);
            }
            else {
                alert('发送失败');
            }
        },
        error: function () {

        }
    });
}

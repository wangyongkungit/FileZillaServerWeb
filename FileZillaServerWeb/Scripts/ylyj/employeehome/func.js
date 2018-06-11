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
    funcList["getHistoryData"]["func"].call(this, p1);
    funcList["getFileTabs"]["func"].call(this, p1);
    funcList["getFiles"]["func"].call(this, p1);
    this.newtab.categoryselected = this.newtab.replytoselected = -1;
    this.showreply = false;
    this.projectfile.parentId = "";
}
//get filetabs 
function getFileTabsByProjectid(p1) {
    console.log(p1);

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
            success: function (data) {
                if (data.Code === 0) {
                    _this.projectfile.filetabs = data.Result;
                } else {
                    alert("failed");
                }
            }
        });
    }
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
                console.log("getFiles:", data.Result);
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
        success: function (data) {
            if (data.Code === 0) {
                _this.projecthistory.data = data.Result;
            } else {
                alert("failed");
            }
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
                console.log("getReplytodata", "sucuess");
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
                console.log("success");
                _this.newtab.categoryselected = -1;
                _this.newtab.desc = "";
                //refresh file tab data
                funcList["getFileTabs"]["func"].call(_this, p1);
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

            } else {
                console.log("addReplytoTab", "failed");
            }
        }
    });
}

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
                funcList["getHistoryData"]["func"].call(_this, p1);
                alert("删除成功！");
            } else {
                alert("failed");
            }
        }
    });
}

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
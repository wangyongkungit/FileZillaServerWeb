
//Init Vue
function init() {
    //Note:this must be Vue
    // console.log(this);
    // this.projectid = "cdc19ae8-fa7b-49ef-9733-7666b3337e68";
    this.projectid = "";
    this.roleid = 1;
    this.newtab.categoryselected = -1;
    // let p1 = {
    //     "projectid": this.projectid
    // };

    // funcList["getFileTabs"]["func"].call(this, p1);

    this.newtab.category = funcList["getCategoryList"]["func"]();

    // funcList["getHistoryData"]["func"].call(this, p1);
    //control which tab to show 
    this.showfile = true;
    this.showaddtab = this.showhistory = false;
    return;
    if (this.projectfile.filetabs.lenght > 0) {
        this.showfile = true;
        this.showaddtab = this.showhistory = false;
    } else {
        this.showaddtab = true;
        this.showfile = this.showhistory = false;
    }

}


function refreshProject(p1) {

    //    funcList["getFileTabs"]["func"].call(this, p1);
    funcList["getHistoryData"]["func"].call(this, {projectid : p1 });
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
    var test = "";
}

/***
 * 
 * projecthistory
 */

function getHistoryData(p1) {

    let _this = this;
    let prameters = "&projectId=" + p1.projectid;
    let handlerurl = funcList["getHistoryData"]["interface"] + prameters;
    console.log(handlerurl);

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
    // let p1 = {
    //     "projectid": this.projectid,
    //     "categoryid": this.newtab.categoryselected,
    //     "description": "test",
    //     "parentid": 0
    // };
    let _this = this;
    let prameters = "&projectId=" + p1.projectid + "&categoryId=" + p1.categoryid + "&description=" + p1.description + "&parentId=" + p1.parentid;
    let handlerurl = funcList["addNewTab"]["interface"] + prameters;

    $.ajax({
        // type: "get",
        async: true,
        url: handlerurl,
        dataType: 'jsonp',
        crossDomain: true,
        success: function (data) {
            console.log(data);
            console.log(data.Code);
            console.log(typeof data.Code);
            _this.newtab.returnmessage = data.Message;
            if (data.Code === 0) {
                console.log("success");
                _this.newtab.categoryselected = -1;
                _this.newtab.desc = "";
                //refresh file tab data
            } else {
                console.log("addNewTab", "failed");
            }
        }
    });
}

//add Reply Tab
function addReplytoTab(p1) {
    console.log(p1);

    let _this = this;
    let prameters = "&projectId=" + p1.projectid + "&categoryId=" + p1.categoryid + "&description=" + p1.description + "&parentId=" + p1.parentid;
    let handlerurl = funcList["addNewTab"]["interface"] + prameters;

    $.ajax({
        // type: "get",
        async: true,
        url: handlerurl,
        dataType: 'jsonp',
        crossDomain: true,
        success: function (data) {
            console.log(data);
            _this.newtab.returnmessage = data.Message;
            if (data.Code === 0) {
                console.log("addReplytoTab", "success");
                funcList["getReplytodata"]["func"].call(_this, p1);
                _this.newtab.replyto
                _this.newtab.replytoselected = -1;
                _this.newtab.desc = "";
            } else {
                console.log("addReplytoTab", "failed");
            }
        }
    });
}


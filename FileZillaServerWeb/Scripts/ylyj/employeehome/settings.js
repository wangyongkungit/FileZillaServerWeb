//global var
//var urlserver = "http://localhost:59649";
var urlserver = "";

var funcList = {};

//init releated
funcList["init"] = {
    func: init,
    interface: ""
};

//file
funcList["getFileTabs"] = {
    func: getFileTabsByProjectid,
    interface: urlserver + "/HttpHandler/FileHandler.ashx?FuncName=GetCategories"
};

funcList["getFiles"] = {
    func: "getFiles",
    interface: ""
};

funcList["refreshProject"] = {
    func: refreshProject,
    interface: ""
};


/**
 * history related
 * 
 */

funcList["getHistoryData"] = {
    func: getHistoryData,
    interface: urlserver + "/HttpHandler/FileHandler.ashx?FuncName=GetProjectOperationLogs"
};

/**
 * add new tab related
 * 
 */
funcList["getCategoryList"] = {
    func: getCategoryList,
    interface: urlserver + "/HttpHandler/FileHandler.ashx?FuncName=addCategory"
};

funcList["getReplytodata"] = {
    func: getReplytodata,
    interface: urlserver + "/HttpHandler/FileHandler.ashx?FuncName=AddFileCategory"
};

funcList["addNewTab"] = {
    func: addNewTab,
    interface: urlserver + "/HttpHandler/FileHandler.ashx?FuncName=AddFileCategory"
};

funcList["addReplytoTab"] = {
    func: addReplytoTab,
    interface: urlserver + "/HttpHandler/FileHandler.ashx?FuncName=GetReplyToTab"
};

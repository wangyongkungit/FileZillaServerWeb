var vm = new Vue({
    el: "#project",

    data: {
        projectid: "",
        taskno: "",
        roleid: 0,
        showhistory: false,
        showfile: false,
        showaddtab: false,
        showreply: false,

        projectfile: {
            filetabs: [
            ],
            files: [
            ],
            filedesc: "",
            parentId: ""
        },

        projecthistory: {
            data: [
            ],
        },
        newtab: {
            category: [],
            categoryselected: "",
            replyto: [],
            replytoselected: "",
            desc: "",
            expiredate: "",
            returnmessage: ""
        },
    },

    methods: {
        //category select change
        categoryChange: function (option) {
            (this.newtab.categoryselected === 4 || this.newtab.categoryselected === 6) ? this.showreply = true : this.showreply = false;
            if (this.showreply) {
                let p1 = {
                    "projectid": this.projectid,
                    "categoryid": this.newtab.categoryselected
                };
                funcList["getReplytodata"]["func"].call(this, p1);
            } else {
                this.newtab.replytoselected = -1;
            }
        },
        //when you click the add button
        addTab: function () {
            this.newtab.returnmessage = "";
            if (this.newtab.categoryselected < 0) {
                alert("请选择类型");
                return;
            }
            //add new tab
            if (this.newtab.categoryselected === 3 || this.newtab.categoryselected === 5 || this.newtab.categoryselected === 2) {
                // 新的修改任务
                if (this.newtab.categoryselected === 3) {
                    var taskNotFinished = getExistFileNotFinishCategory(this.projectid);
                    if (taskNotFinished) {
                        // 如果选择否，则直接 return 不再执行创建新 tab 的操作
                        if (!confirm("当前任务尚有 " + taskNotFinished + " 未完成，仍然要创建新的修改任务？")) {
                            return;
                        }
                    }
                }
                let p1 = {
                    "projectid": this.projectid,
                    "categoryid": this.newtab.categoryselected,
                    "description": this.newtab.desc,
                    "expiredate": $("#tabexpiredate").val(),
                    "parentid": 0
                };
                funcList["addNewTab"]["func"].call(this, p1);
            }
            //reply a tab
            else {
                if (this.newtab.replytoselected < 0 || this.newtab.replytoselected === "") {
                    alert("请选择回复");
                    return;
                }
                let p1 = {
                    "projectid": this.projectid,
                    "categoryid": this.newtab.categoryselected,
                    "description": this.newtab.desc,
                    "expiredate": $("#tabexpiredate").val(),
                    "parentid": this.newtab.replytoselected
                };
                funcList["addReplytoTab"]["func"].call(this, p1);
            }
        },
        //change Tab
        changeTab: function (showhistory, showfile, showaddtab) {
            let p1 = {
                projectid: this.projectid
            };
            if (showhistory && p1.projectid && !this.showhistory) {
                funcList["getHistoryData"]["func"].call(this, p1);
            }
            this.showhistory = showhistory;
            this.showfile = showfile;
            this.showaddtab = showaddtab;
        },
        //chang file list
        changeFilesTab: function (filetype) {
            this.projectfile.parentId = filetype;
            changeActive();
        },
        //delete file
        deleteFile: function (fileHistoryId) {
            let p1 = {
                "filehistoryid": fileHistoryId
            };
            funcList["deleteFile"]["func"].call(this, p1);
        },
        // download file
        downloadFile: function (fileHistoryId) {
            let p1 = {
                "filehistoryid": fileHistoryId
            };
            funcList["downloadFile"]["func"].call(this, p1);
        },
        // preview file
        previewFile: function (fileHistoryId, fileExt) {
            let p1 = {
                "filehistoryid": fileHistoryId,
                "fileExt": fileExt
            };
            funcList["previewFile"]["func"].call(this, p1);
        },
        // Share link
        ShareLink: function (fileHistoryId) {
            let p1 = {
                "filehistoryid": fileHistoryId
            };
            funcList["ShareLink"]["func"].call(this, fileHistoryId);
        },
        // 判断是否进行过提醒
        GetIsRemind: function (fileHistoryId) {
            let p1 = {
                "filehistoryid": fileHistoryId
            };
            funcList["GetIsRemind"]["func"].call(this, fileHistoryId);
        },
        // 发送提醒
        sendDingtalkMessage: function (fileHistoryId) {
            let p1 = {
                "filehistoryid": fileHistoryId
            };
            funcList["sendDingtalkMessage"]["func"].call(this, fileHistoryId);
        }
    },
    mounted: function () {
        init.call(this);
    }
});


Vue.filter('convTime', function (value) {
    var unixTimestamp = new Date(value * 1000);
    commonTime = unixTimestamp.toLocaleString();   //转换为24小时制的时间格式   //'chinese', { hour12: false }
    return commonTime;
});

vm.$watch("projectid", function (newVal, oldVal) {
    let p1 = {
        projectid: newVal
    };
    funcList["refreshProject"]["func"].call(this, p1);
    this.changeTab(false, true, false);
});




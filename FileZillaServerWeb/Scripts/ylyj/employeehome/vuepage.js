var vm = new Vue({
    el: "#project",

    data: {
        projectid: "",
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
            filedesc: ""
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
            if (this.newtab.categoryselected === 3 || this.newtab.categoryselected === 5) {
                let p1 = {
                    "projectid": this.projectid,
                    "categoryid": this.newtab.categoryselected,
                    "description": this.newtab.desc,
                    "parentid": 0
                };
                funcList["addNewTab"]["func"].call(this, p1);
            }
            //reply a tab
            else {
                if (this.newtab.replytoselected < 0 || this.newtab.replytoselected == "") {
                    alert("请选择回复");
                    return;
                }
                let p1 = {
                    "projectid": this.projectid,
                    "categoryid": this.newtab.categoryselected,
                    "description": this.newtab.desc,
                    "parentid": this.newtab.replytoselected
                };
                funcList["addReplytoTab"]["func"].call(this, p1);
                // addReplytoTab(p1);
            }
        },
        //change Tab
        changeTab: function (showhistory, showfile, showaddtab) {
            this.showhistory = showhistory;
            this.showfile = showfile;
            this.showaddtab = showaddtab;
        },
        //chang file list
        changeFiles: function (filetype) {

        },

    }
    ,
    mounted: function () {
        init.call(this);
        //funcList["init"]["func"].call(this);
        // this.projectid = "0007a46a-6914-4d4e-a6d5-f465f683204f";
        // this.roleid = 1;


    }
});


Vue.filter('convTime', function (value) {
    var unixTimestamp = new Date(value * 1000);
    commonTime = unixTimestamp.toLocaleString('chinese', { hour12: false });   //转换为24小时制的时间格式  
    return commonTime;
});

vm.$watch("projectid", function (newVal, oldVal) {

    funcList["refreshProject"]["func"].call(this, newVal);
});




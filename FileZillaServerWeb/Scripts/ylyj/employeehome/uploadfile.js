
//author: toby.yang
//date ： 20180602
///
$(function load() {
    myfunction("#file1", "#file1progressbar", "#file1progressbar");
});

//上次文件按钮：file，进度条:progessbar,进度条的父元素：progess
function myfunction(file, progressbar, progress) {
    let taskid = WebUploader.Base.guid(); // 产生文件唯一标识符task_id
    var uploader = WebUploader.create({
        swf: '../webuploader/Uploader.swf',
        server: '/HttpHandler/FileHandler.ashx',//note
        pick: file,//这个id值需要变化
        auto: true,
        chunked: true,
        chunkSize: 20 * 1024 * 1024,
        chunkRetry: 3,
        threads: 4,
        duplicate: true,
        formData: { // 上传分片的http请求中一同携带的数据
            taskid: taskid,
            FuncName: "UpadlodPart"
        },
        compress: false
    });
    uploader.on('beforeFileQueued', function (file) {
        var arr = ["exe", "lsp", "fas", "vlx", "gryphon"];
        if (!vm.projectfile.parentId) {
            alert("请选中一个标签后再上传！");
            return false;
        }
        if (arr.indexOf(file.ext) > -1) {
            alert("请上传合法扩展名的文件！");
            return false;
        }
    });
    uploader.on('startUpload', function () { // 开始上传时，调用该方法
        $(progress).show();
        $(progressbar).css('width', '0%');
        $(progressbar).text('0%');
        $(progressbar).removeClass('progress-bar-danger progress-bar-success');
        $(progressbar).addClass('active progress-bar-striped');
    });
    uploader.on('uploadProgress', function (file, percentage) { // 一个分片上传成功后，调用该方法
        $(progressbar).css('width', percentage * 100 - 1 + '%');
        $(progressbar).text(Math.floor(percentage * 100 - 1) + '%');
    });
    uploader.on('uploadSuccess', function (file) { // 整个文件的所有分片都上传成功后，调用该方法
        var data = {
            taskid: taskid,
            filename: file.source['name'],
            FuncName: 'UploadSuccess',
            //"parentId", "description", "taskid", "filename"
            description: vm.projectfile.filedesc,
            parentId: vm.projectfile.parentId
        };
        //$.get('/HttpHandler/FileHandler.ashx', data);      

        $.ajax({
            url: "/HttpHandler/FileHandler.ashx",
            type: "POST",
            data: data,
            success: function (data) {
                $(progressbar).css('width', '100%');
                $(progressbar).text('100%');
                $(progressbar).addClass('progress-bar-success');
                $(progressbar).text('上传完成');
                $("#pfile1").text(data.filename);
                funcList["getFiles"]["func"].call(vm, { projectid: vm.projectid });
                funcList["getHistoryData"]["func"].call(vm, { projectid: vm.projectid });
            },
            complete: function (data) {

            }
        })
    });
    uploader.on('uploadError', function (file) { // 上传过程中发生异常，调用该方法
        $(progressbar).css('width', '100%');
        $(progressbar).text('100%');
        $(progressbar).addClass('progress-bar-danger');
        $(progressbar).text('上传失败');
    });
    uploader.on('uploadComplete', function (file) { // 上传结束，无论文件最终是否上传成功，该方法都会被调用
        $(progressbar).removeClass('active progress-bar-striped');
    });
    $(progress).hide();
}

//author: toby.yang
//date ： 20180602
///
$(function load() {

    console.log("hello,jquery");


    //var taskid = WebUploader.Base.guid(); // 产生文件唯一标识符task_id
    //var uploader = WebUploader.create({
    //    swf: '../webuploader/Uploader.swf',
    //    server: 'Handler/FileUpload.ashx',
    //    pick: '#file1',//这个id值需要变化
    //    auto: true,
    //    chunked: true,
    //    chunkSize: 20* 1024 * 1024,
    //    chunkRetry: 3,
    //    threads: 10,
    //    duplicate: true,
    //    formData: { // 上传分片的http请求中一同携带的数据
    //        taskid: taskid,
    //        FuncName:"UpadlodPart"
    //    },
    //});
    //uploader.on('startUpload', function () { // 开始上传时，调用该方法
    //    $('#progress').show();
    //    $('#file1_progress').css('width', '0%');
    //    $('#file1_progress').text('0%');
    //    $('#file1_progress').removeClass('progress-bar-danger progress-bar-success');
    //    $('#file1_progress').addClass('active progress-bar-striped');
    //});
    //uploader.on('uploadProgress', function (file, percentage) { // 一个分片上传成功后，调用该方法
    //    $('#file1_progress').css('width', percentage * 100 - 1 + '%');
    //    $('#file1_progress').text(Math.floor(percentage * 100 - 1) + '%');
    //});
    //uploader.on('uploadSuccess', function (file) { // 整个文件的所有分片都上传成功后，调用该方法
    //    var data = { 'taskid': taskid, 'filename': file.source['name'], 'FuncName':'UploadSucess'};
    //    $.get('Handler/FileUpload.ashx', data);
    //    $('#file1_progress').css('width', '100%');
    //    $('#file1_progress').text('100%');
    //    $('#file1_progress').addClass('progress-bar-success');
    //    $('#file1_progress').text('上传完成');
    //});
    //uploader.on('uploadError', function (file) { // 上传过程中发生异常，调用该方法
    //    $('#file1_progress').css('width', '100%');
    //    $('#file1_progress').text('100%');
    //    $('#file1_progress').addClass('progress-bar-danger');
    //    $('#file1_progress').text('上传失败');
    //});
    //uploader.on('uploadComplete', function (file) { // 上传结束，无论文件最终是否上传成功，该方法都会被调用
    //    $('#file1_progress').removeClass('active progress-bar-striped');
    //});
    //$('#progress').hide();

    myfunction("#file1", "#file1progressbar", "#file1progressbar")
    // myfunction("#file2", "#file2progressbar", "#file2progressbar")

    $("#call").click(function () {
        //console.log(this.name + ",clicked");
        $.ajax({
            type: "GET",
            url: "Handler/TaskHandler.ashx",
            data: "FuncName=GenerateJson",
            success: function (data) {
                console.log(data);
            }
        });
    });

});

//上次文件按钮：file，进度条:progessbar,进度条的父元素：progess
function myfunction(file, progressbar, progress) {
    var taskid = WebUploader.Base.guid(); // 产生文件唯一标识符task_id
    var uploader = WebUploader.create({
        swf: '../webuploader/Uploader.swf',
        server: 'Handler/FileUpload.ashx',
        pick: file,//这个id值需要变化
        auto: true,
        chunked: true,
        chunkSize: 20 * 1024 * 1024,
        chunkRetry: 3,
        threads: 10,
        duplicate: true,
        formData: { // 上传分片的http请求中一同携带的数据
            taskid: taskid,
            FuncName: "UpadlodPart"
        },
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
        var data = { 'taskid': taskid, 'filename': file.source['name'], 'FuncName': 'UploadSucess' };
        $.get('Handler/FileUpload.ashx', data);
        $(progressbar).css('width', '100%');
        $(progressbar).text('100%');
        $(progressbar).addClass('progress-bar-success');
        $(progressbar).text('上传完成');
        $("#pfile1").text(data.filename);
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
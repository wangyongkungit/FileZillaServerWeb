<%@ Page Title="关于" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="FileZillaServerWeb.About" %>

<asp:Content runat="server" ID="titleContent" ContentPlaceHolderID="head">

</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <div>
        <input type="button" id="call" name="toby" title="tttt" value="Call" />
        <div style="margin: 50px 0 0 50px;">

            <div id="file1" style="float: left;">请选择</div>
            <span id="pfile1"></span>
            <div id="file1progress" class="progress" style="width: 500px; float: left; margin: 10px 0 0 20px;">
                <div id="file1progressbar" class="progress-bar progress-bar-striped active" role="progressbar" style="width: 0%;"></div>
            </div>
            <div style="clear: both;"></div>
        </div>

        <div style="margin: 50px 0 0 50px;">
            <div id="file2" style="float: left;">请选择</div>
            <div id="file2progress" class="progress" style="width: 500px; float: left; margin: 10px 0 0 20px;">
                <div id="file2progressbar" class="progress-bar progress-bar-striped active" role="progressbar" style="width: 0%;"></div>
            </div>
            <div style="clear: both;"></div>
        </div>

    </div>
    
    <script src="Scripts/UploadTest/jquery-3.3.1.min.js"></script>
    <script src="Scripts/UploadTest/bootstrap.min.js"></script>
    <link href="Scripts/UploadTest/webuploader/webuploader.css" rel="stylesheet" />
    <script src="Scripts/UploadTest/webuploader/webuploader.js"></script>
    <script src="Scripts/UploadTest/about.js"></script>
</asp:Content>
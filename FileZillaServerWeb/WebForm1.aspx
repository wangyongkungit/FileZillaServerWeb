<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="FileZillaServerWeb.WebForm1" %>

<%@ Register Assembly="Brettle.Web.NeatUpload" Namespace="Brettle.Web.NeatUpload" TagPrefix="Upload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="Scripts/jquery.min.js"></script>
    <script type="text/javascript">
//        $(function () {
//$(".parent").click(function () {
//    $(this).toggleClass("selected");
//    $(this).siblings().not(".parent").not(":first-child").hide();
//    $(this).next().show().next().show();
//});
//})
        function ToggleVisibility(id, type) {
            el = document.getElementById(id);
            if (el.style) {
                if (type == 'on') {
                    el.style.display = 'block';
                }
                else {
                    el.style.display = 'none';
                }
            }
            else {
                if (type == 'on') {
                    el.display = 'block';
                }
                else {
                    el.display = 'none';
                }
            }
            
        }
        //function SetStatus() {
        //    $("#lblStatus").text(window.status);
        //}
        //$(document).ready(function () {
        //    var t1 = window.setInterval(SetStatus, 1000);
        //})
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 291px;
        }
        .auto-style3 {
            width: 291px;
            height: 20px;
        }
        .auto-style4 {
            height: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
<%--          <Upload:InputFile ID="AttachFile" runat="server" />  
   <asp:Button ID="Upload" runat="server" Text="Upload"  
            OnClientClick="ToggleVisibility('ProgressBar','on')" onclick="Upload_Click" />  
       <div id="ProgressBar888" >  
       <Upload:ProgressBar ID="pbProgressBar" runat='server' Inline="true" Width="280px"  
               Height="50px">  
       </Upload:ProgressBar>--%>

        <div>
           <Upload:InputFile ID="AttachFile" runat="server" />
           <asp:Button ID="Upload" runat="server" Text="Upload" OnClientClick="ToggleVisibility('ProgressBar','on')" OnClick="Upload_Click1" /></div>
           <div id="ProgressBar">
               <Upload:ProgressBar ID="pbProgressBar" runat="server" Inline="true" Width="680px" Height="120px">
               </Upload:ProgressBar>

               <Upload:DetailsDiv ID="DetailsDiv1" runat="server">
               </Upload:DetailsDiv>
               <Upload:DetailsSpan ID="DetailsSpan1" runat="server">
               </Upload:DetailsSpan>

           </div>
        <div style="width:300px;height:20px;background-color:ActiveCaption;">
            <label id="lblStatus"></label>
        </div>
        <div>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="创建修改任务" />
            <br />
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">修改1</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">修改1完成</td>
                    <td>&nbsp;<asp:Button ID="Button4" runat="server" Text="压缩" />
                        <asp:Button ID="Button5" runat="server" Text="下载" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">修改2</td>
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style4">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">修改2完成</td>
                    <td class="auto-style4">
                        <asp:Button ID="Button6" runat="server" Text="压缩" />
                        <asp:Button ID="Button7" runat="server" Text="下载" />
                    </td>
                    <td class="auto-style4"></td>
                </tr>
                <tr>
                    <td class="auto-style2">修改3待审核</td>
                    <td>
                        <asp:Button ID="Button2" runat="server" Text="替换附件" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;
                        <asp:Button ID="btnSync" runat="server" Text="同步专业" OnClick="btnSync_Click" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>

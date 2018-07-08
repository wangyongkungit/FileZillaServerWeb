<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileShare.aspx.cs" Inherits="FileZillaServerWeb.FileOperation.FileShare" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>BIMPAN - 文件提取</title>
    <link href="../Content/themes/base/ylyj/fileShare.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="center">
                <div class="banner">
                    &nbsp;
                </div>
                <div class="centerheader">
                    <asp:Label runat="server" ID="lblShopName" CssClass="shopname"></asp:Label> 给你加密分享了文件
                </div>
                <div class="file">
                    <span>请输入提取密码：</span>
                    <br />
                    <br />
                    <asp:TextBox ID="txtGetPassword" CssClass="getpassword" runat="server" required="required"></asp:TextBox>
                    <asp:Button ID="btnGetFile" runat="server" CssClass="btngetfile" Text="提取文件" OnClick="btnGetFile_Click" />
                </div>
            </div>
            <div class="cloud-bg">
            </div>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopAddEdit.aspx.cs" Inherits="FileZillaServerWeb.ShopAddEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>店铺信息维护</title>
    <link href="Content/themes/base/ylyj/shopaddedit.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="container" class="container">
            <h1>店铺添加/编辑</h1>
            <div class="empadd">
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>店铺ID：</label>
                    </div>
                    <asp:TextBox ID="txtShopId" runat="server"></asp:TextBox>
                </div>                
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>店铺名称：</label>
                    </div>
                    <asp:TextBox ID="txtShopName" runat="server"></asp:TextBox>
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>Access Key：</label>
                    </div>
                    <asp:TextBox ID="txtAccessKey" runat="server"></asp:TextBox>
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>Secret Key：</label>
                    </div>
                    <asp:TextBox ID="txtSecretKey" runat="server"></asp:TextBox>
                </div>
                <div class="valGroup">
                    <div class="groupLeft">                        
                    </div>
                    <asp:Button ID="btnOk" runat="server" OnClick="btnOk_Click" Text="添加" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>

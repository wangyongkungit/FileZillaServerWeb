<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopList.aspx.cs" Inherits="FileZillaServerWeb.ShopList" %>

<%@ Register Src="~/UserControl/SiteMenu.ascx" TagPrefix="uc1" TagName="SiteMenu" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>店铺列表</title>
    <link href="/Content/themes/base/ylyj/shoplist.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 1200px; margin: 0 auto; text-align: center;">
                        <div style="width:200px; height: 30px; float:right; position:absolute; right:50px; top: 50px;">
                <a href="javascript:void()" onclick="location.href='/Pages/Admin/ShopList.aspx'">刷新</a>
            </div>
            <h1>店铺列表</h1>
            <asp:GridView ID="gvShop" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" CssClass="tbl" RowStyle-Height="28" OnRowCommand="gvShop_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="店铺ID" ItemStyle-Width="80" ItemStyle-Height="28">
                        <ItemTemplate>
                            <asp:Label ID="lblShopID" runat="server" Text='<%# Eval("ShopID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="店铺名称" ItemStyle-Height="28" ControlStyle-CssClass="rowleft">
                        <ItemTemplate>
                            <%# Eval("configValue") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态" ItemStyle-Width="80" ItemStyle-Height="28">
                        <ItemTemplate>
                            <asp:Label ID="lblTaskReadStatus" runat="server" Text='<%# Convert.ToBoolean(Eval("isActive")) ? "正常" : "禁用" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <a href='/Pages/Admin/ShopAddEdit.aspx?shopid=<%# Eval("shopid") %>' target="_blank" class="mya" style="display:inline-block;">编辑</a>
                            <asp:Button ID="btnDisable" runat="server" CommandName="Disable" CommandArgument='<%# Eval("ID") %>' Text='<%# Convert.ToBoolean(Eval("isActive")) ? "禁用" : "启用" %>' CssClass="mya" OnClientClick="return confirm('确定要禁用/启用该店铺吗？');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <span>暂无记录</span>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
        <uc1:SiteMenu runat="server" ID="SiteMenu" />
    </form>
</body>
</html>

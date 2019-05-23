<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerServiceTrends.aspx.cs" Inherits="FileZillaServerWeb.CustomerServiceTrends" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>任务动态</title>
    <link href="Content/themes/base/ylyj/customerServiceTrends.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 1200px; margin: 0 auto; text-align: center;">
            <div style="width:200px; height: 30px; float:right; position:absolute; right:50px; top: 50px;">
                <a href="javascript:void()" onclick="location.href='/CustomerServiceTrends.aspx'">刷新</a>
            </div>
            <h1 id="title">我的动态</h1>
            <asp:GridView ID="gvTrend" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" CssClass="tbl" RowStyle-Height="28" OnRowCommand="gvTrend_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="任务时间" ItemStyle-Width="80" ItemStyle-Height="28">
                        <ItemTemplate>
                            <asp:Label ID="lblTaskDate" runat="server" Text='<%# Eval("FriendlyDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="任务内容" ItemStyle-Height="28" ControlStyle-CssClass="rowleft">
                        <ItemTemplate>
                            <%--<asp:Label ID="lblTaskContent" runat="server" Text='<%# Eval("TrendContent") %>'></asp:Label>--%>
                            <%# Eval("TrendContent") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态" ItemStyle-Width="80" ItemStyle-Height="28">
                        <ItemTemplate>
                            <asp:Label ID="lblTaskReadStatus" runat="server" Text='<%# Convert.ToBoolean(Eval("ReadStatus")) ? "已读" : "未读" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <a href='SerialNumberGenerating.aspx?projectid=<%# Eval("projectid") %>' target="_blank" class="mya" style="display:inline;"></a>
                            <asp:Button ID="btnMarkRead" runat="server" CommandName="markRead" CommandArgument='<%# Eval("ID") %>' Text="标为已读" Visible='<%# !Convert.ToBoolean(Eval("ReadStatus")) %>' CssClass="mya" OnClientClick="return confirm('确定标记为已读吗？');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <span>暂无记录</span>
                </EmptyDataTemplate>
            </asp:GridView>
            <div>
                <div style="float: left;">
                    <label>共</label>
                    <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    <label>条记录</label>
                </div>
                <div class="aspNetPager">
                    <webdiyer:aspnetpager id="AspNetPager1" runat="server" onpagechanged="AspNetPager_PageChanged" height="30"
                        firstpagetext="首页" lastpagetext="尾页" nextpagetext="下一页" prevpagetext="上一页" showpageindexbox="Never"
                        alwaysshow="true" urlpaging="False" reverseurlpageindex="True" textbeforepageindexbox="跳到第" textafterpageindexbox="页"
                        cssclass="pagination" pagingbuttonlayouttype="UnorderedList" pagingbuttonspacing="0" currentpagebuttonclass="active">
                        </webdiyer:aspnetpager>
                    <div style="height: 30px; line-height: 30px; float: right;">
                        <label>跳转到</label><asp:TextBox ID="tb_pageindex" runat="server"></asp:TextBox><label>页</label>
                        <asp:Button ID="btnGoPage" runat="server" Text="转到" OnClick="btnGoPage_Click" ValidationGroup="pageGo" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必须输入页索引" ForeColor="Red" ControlToValidate="tb_pageindex" Display="Dynamic" ValidationGroup="pageGo" />
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tb_pageindex" Operator="DataTypeCheck" Type="Integer" ErrorMessage="页索引必须是整数" ForeColor="Red" Display="Dynamic" />
                        <asp:Label ID="lbl_error" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

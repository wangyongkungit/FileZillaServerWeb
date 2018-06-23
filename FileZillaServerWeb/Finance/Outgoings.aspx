<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Outgoings.aspx.cs" Inherits="FileZillaServerWeb.Finance.Outgoings" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>奖罚录入</title>
    <link href="../Content/themes/base/ylyj/outgoings.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 900px; margin: 0 auto; text-align: center;">
        <h1>奖罚录入</h1>
        <div style="width: 1000px; margin: 0 auto;">
            <label>员工：</label>
            <asp:DropDownList ID="ddlEmployeeName" runat="server" />
            <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" CssClass="mya" />
            <hr />
            <div class="row">
                <div class="left">
                    <div class="lbl">
                        <label>类型：</label>
                    </div>
                    <div class="txt">
                        <asp:DropDownList ID="ddlTransacType" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="left">
                    <div class="lbl">
                        <label>金额：</label>
                    </div>
                    <div class="txt">
                        <asp:TextBox ID="txtAmount" runat="server" placeholder="扣款需包含负号"></asp:TextBox>
                        元
                    </div>
                </div>
                <div class="right">
                    <div class="lbl">
                        <label>说明：</label>
                    </div>
                    <div class="txt">
                        <asp:TextBox ID="txtDescription" runat="server" Width="200"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">                
                <div class="left">
                    <div class="lbl">
                        <label>任务编号：</label>
                    </div>
                    <div class="txt">
                        <asp:TextBox ID="txtTaskNo" runat="server" Width="200"></asp:TextBox>
                    </div>
                </div>
                <div class="left">
                    <div class="lbl">
                        <label>时间：</label>
                    </div>
                    <div class="txt">
                        <asp:TextBox ID="txtTransacDate" runat="server" CssClass="Wdate" onFocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" Width="160"></asp:TextBox>
                    </div>
                </div>
                <div class="right">
                    <asp:Button ID="btnAdd" runat="server" Text="添加" OnClick="btnAdd_Click" OnClientClick="return confirmSubmit();" CssClass="mya" Width="180" />
                </div>
            </div>
            <asp:GridView ID="gvTransaction" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" OnRowCommand="gvTransaction_RowCommand" CssClass="tbl">
                <Columns>
                       <asp:TemplateField HeaderText="员工编号">
                           <ItemTemplate>
                               <asp:Label ID="lblEmployeeNo" runat="server" Text='<%# Eval("employeeNo") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="员工姓名">
                           <ItemTemplate>
                               <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="任务编号">
                           <ItemTemplate>
                               <asp:Label ID="lblTaskNo" runat="server" Text='<%# Eval("TaskNo") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="交易时间">
                           <ItemTemplate>
                               <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("TransactionDate") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="交易金额">
                           <ItemTemplate>
                               <asp:Label ID="lblTransactionAmount" runat="server" Text='<%# Eval("TransactionAmount") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="交易类型">
                           <ItemTemplate>
                               <asp:Label ID="lblTransactionType" runat="server" Text='<%# Eval("TransactionType") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="说明">
                           <ItemTemplate>
                               <asp:Label ID="lblTransactionDescription" runat="server" Text='<%# Eval("TransactionDescription") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="操作">
                           <ItemTemplate>
                               <asp:Button ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("ID") %>' Text="删除" CssClass="mya" OnClientClick="return confirm('确定要删除吗？');" />
                           </ItemTemplate>
                       </asp:TemplateField>
                   </Columns>
                <EmptyDataTemplate>
                    <span>暂无记录</span>
                </EmptyDataTemplate>
            </asp:GridView>
            <div class="aspNetPager">
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager_PageChanged" Height="30"
                    FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Never"
                    AlwaysShow="true" UrlPaging="False" ReverseUrlPageIndex="True" TextBeforePageIndexBox="跳到第" TextAfterPageIndexBox="页"
                    CssClass="pagination"  PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" >
                </webdiyer:AspNetPager>
                <div style="height:30px; line-height:30px; float:right;">
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
    <%--<script src='<% ResolveUrl("~/Scripts/My97DatePicker/WdatePicker.js"); %>'></script>--%>
    <script src="../Scripts/jquery-3.3.1.min.js"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        var confirmSubmit = function () {
            if (!document.getElementById("txtAmount").value || !document.getElementById("txtTransacDate").value) {
                alert("请补全金额和时间！");
                return false;
            }
            return true;
        }
    </script>
</body>
</html>

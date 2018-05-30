<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AttendanceList.aspx.cs" Inherits="FileZillaServerWeb.AttendanceList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="head1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/themes/base/ylyj/attendancelist.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () { //这个就是传说的ready  
            $(".tbl tr").not(':eq(0)').mouseover(function () {
                //如果鼠标移到class为stripe的表格的tr上时，执行函数
                $(this).addClass("over");
            }).mouseout(function () {
                //给这行添加class值为over，并且当鼠标一出该行时执行函数  
                $(this).removeClass("over");
            }) //移除该行的class  
            //$(".tbl1 tr:even").addClass("alt");
            //给class为stripe的表格的偶数行添加class值为alt
            $(".tbl tr:odd").addClass("alt2");
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <div class="divTitle">
                    <h1>考勤列表</h1>
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>姓名：</label>
                    </div>
                    <asp:TextBox ID="txtEmployeeName" runat="server" placeholder="员工姓名" />
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>编号：</label>
                    </div>
                    <asp:TextBox ID="txtEmployeeNo" runat="server" placeholder="员工编号" />
                </div>
                <div class="formSearchDate">
                    <div class="formSearchLeft">
                        <label>时间：</label>
                    </div>
                    <asp:TextBox ID="txtDateFrom" runat="server" ClientIDMode="Static" CssClass="Wdate" onFocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'txtDateTo\')}'})" placeholder="开始时间" />～
                    <asp:TextBox ID="txtDateTo" runat="server" ClientIDMode="Static" CssClass="Wdate" onFocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'txtDateFrom\')}'})" placeholder="结束时间" />
                </div>
                <div class="formSearch">
                </div>
                <div class="formSearch">
                </div>
                <div class="formSearch">
                </div>
                <div class="formSearch">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="查询" OnClick="btnSearch_Click" />
                </div>
            </div>
            <div id="content">
                <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="tbl" BorderWidth="0" GridLines="None" HeaderStyle-CssClass="tbHead" RowStyle-CssClass="tbl tr">
                    <Columns>
                        <asp:BoundField DataField="employeeno" HeaderText="员工编号" />
                        <asp:BoundField DataField="workDate" HeaderText="工作日" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="checkType" HeaderText="考勤类型" />
                        <asp:BoundField DataField="userCheckTime" HeaderText="打卡时间" DataFormatString="{0:HH:mm:ss}" />
                        <asp:BoundField DataField="timeResult" HeaderText="考勤结果" />
                        <asp:BoundField DataField="deviationMinutes" HeaderText="迟到/早退时长（分钟）" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="deductMoney" HeaderText="扣款金额" DataFormatString="&yen;{0}" />
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="lblEmptyTip" runat="server" Text="没有查询到符合条件的记录" />
                    </EmptyDataTemplate>
                </asp:GridView>
                <div class="aspNetPager">
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager_PageChanged"
                    FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Never"
                    AlwaysShow="true" UrlPaging="False" ReverseUrlPageIndex="True" TextBeforePageIndexBox="跳到第" TextAfterPageIndexBox="页">
                </webdiyer:AspNetPager>

                <label>跳转到</label><asp:TextBox ID="tb_pageindex" runat="server"></asp:TextBox><label>页</label>
                <asp:Button ID="btnGoPage" runat="server" Text="转到" OnClick="btnGoPage_Click" ValidationGroup="pageGo" />
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必须输入页索引" ForeColor="Red" ControlToValidate="tb_pageindex" Display="Dynamic" ValidationGroup="pageGo" />
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tb_pageindex" Operator="DataTypeCheck" Type="Integer" ErrorMessage="页索引必须是整数" ForeColor="Red" Display="Dynamic" />
                <asp:Label ID="lbl_error" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
            </div>
            </div>
        </div>
    </form>
</asp:Content>

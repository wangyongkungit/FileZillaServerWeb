<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogList.aspx.cs" Inherits="FileZillaServerWeb.LogList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="head1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/themes/base/ylyj/loglist.css?v=18426" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () { //这个就是传说的ready  
            /*$(".tbl tr").mouseover(function () {//.not(':eq(0)')
                //如果鼠标移到class为stripe的表格的tr上时，执行函数
                $(this).addClass("over");
            }).mouseout(function () {
                //给这行添加class值为over，并且当鼠标一出该行时执行函数  
                $(this).removeClass("over");
            }) //移除该行的class
            */
            //$(".tbl1 tr:even").addClass("alt");
            //给class为stripe的表格的偶数行添加class值为alt
            $(".tbl tr:odd").addClass("alt2");
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div id="container" class="container">
            <div id="header" class="header">
                <div class="divTitle">
                    <h1>日志查看</h1>
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>操作人：</label>
                    </div>
                    <asp:TextBox ID="txtEmployeeNo" runat="server" placeholder="员工编号" />
                </div>
                <div class="formSearch" style="width:500px;">
                    <div class="formSearchLeft">
                        <label>操作时间：</label>
                    </div>
                    <asp:TextBox ID="txtOperateDateStart" runat="server" ClientIDMode="Static" CssClass="Wdate" onFocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:00',maxDate:'#F{$dp.$D(\'txtOperateDateEnd\')}'})" placeholder="开始时间" />
                    <label>到</label>
                    <asp:TextBox ID="txtOperateDateEnd" runat="server" ClientIDMode="Static" CssClass="Wdate" onFocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:00',minDate:'#F{$dp.$D(\'txtOperateDateStart\')}'})" placeholder="结束时间" />
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>操作类型：</label>
                    </div>
                    <asp:DropDownList ID="ddlOperateType" runat="server">
                        <asp:ListItem Text="所有" Value=""></asp:ListItem>
                        <asp:ListItem Text="任务分配" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="formSearch"></div>
                <div class="formSearch"></div>
                <div class="formSearch"></div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                    </div>
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="button" OnClick="btnSearch_Click" />
                </div>
            </div>
            <div id="content">
                <asp:Repeater ID="rptData" runat="server">
                    <HeaderTemplate>
                        <table class="tbl">
                            <tr id="tbHead">
                                <td class="ygbh" style="text-align:center;">员工编号</td>
                                <td class="czsj" style="text-align:center;">操作时间</td>
                                <td class="ip" style="text-align:center;">IP地址</td>
                                <td class="cznr" style="text-align:center;">操作内容</td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="ygbh" style="text-align:center;"><%# Eval("EMPLOYEENO") %></td>
                            <td class="czsj" style="text-align:center;"><%# Eval("CREATEDATE") %></td>
                            <td class="ip" style="text-align:center;"><%# Eval("IPADDRESS") %></td>
                            <td class="cznr" style="text-align:center;"><%# Convert.ToString(Eval("OPERATECONTENT")).Replace("\r\n","<br/>") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <div style="text-align: right; float: right; padding-right: 100px;">
                    <webdiyer:aspnetpager ID="AspNetPager" runat="server" OnPageChanged="AspNetPager_PageChanged"
                        FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Never"
                        AlwaysShow="true" UrlPaging="False" ReverseUrlPageIndex="True">
                    </webdiyer:aspnetpager>
                </div>
            </div>
        </div>
        
        <div id="rightsead">
            <%--<ul>
                <li>
                    <a href="tencent://message/?uin=763687776&Site=bimpan.iok.la:8&Menu=yes" target="_blank" title="在线客服">
                        <img src="images/ll04.png" class="hides"/>
                        <img src="Images/l04.png" class="shows" />
                    </a>
                </li>
            </ul>--%>
            <a href="FileZillaLogs.aspx" title="查看FTP操作日志">
                <img src="Images/filezillalogo.png" alt="FileZilla日志查看" />
            </a>
        </div>
    </form>
</asp:Content>
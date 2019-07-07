<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeList.aspx.cs" Inherits="FileZillaServerWeb.EmployeeManage.EmployeeList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="head1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/themes/base/ylyj/employeelist.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div id="container" class="container">
        <div id="header" class="header">
            <div class="divTitle">
                <h1>员工列表</h1>
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label>员工编号：</label>
                </div>
                <asp:TextBox ID="txtEmployeeNo" runat="server" placeholder="员工编号" />
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label>员工姓名：</label>
                </div>
                <asp:TextBox ID="txtEmployeeName" runat="server" placeholder="员工姓名" />
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label>手机：</label>
                </div>
                <asp:TextBox ID="txtMobilePhone" runat="server" placeholder="手机" />
            </div>
            <div class="formSearch">
                <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="button" OnClick="btnSearch_Click" />
            </div>
        </div>

        <!--列表-->
        <div id="content">
            <asp:Repeater ID="rptData" runat="server">
                <HeaderTemplate>
                    <table class="tbl">
                        <tr id="tbHead">
                            <td>
                                编号
                            </td>
                            <td>
                                姓名
                            </td>
                            <td>
                                手机
                            </td>
                            <td>
                                操作
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Eval("EMPLOYEENO") %>
                        </td>
                        <td>
                            <%# Eval("NAME") %>
                        </td>
                        <td>
                            <%# Eval("MOBILEPHONE") %>
                        </td>
                        <td>
                            <a href="/Pages/Admin/EmployeeAdd.aspx?UserId=<%# Eval("ID") %>" target="_blank">详细</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    </form>
</asp:Content>

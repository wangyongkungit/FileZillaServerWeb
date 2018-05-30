<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeDemonation.aspx.cs" Inherits="FileZillaServerWeb.EmployeeDemonation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/themes/base/ylyj/employeeDemonation.css?v=18515" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <div class="divTitle">
                    <h1>员工权限配置</h1>
                </div>
                <div>
                    <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" CssClass="tbl">
                        <Columns>
                            <asp:TemplateField HeaderText="员工编号">
                           <ItemTemplate>
                               <asp:Label ID="lblEmployeeNo" runat="server" Text='<%# Eval("employeeNo") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="员工姓名">
                           <ItemTemplate>
                               <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("Name") %>'></asp:Label><br />
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="操作">
                           <ItemTemplate>
                               <a href="javascript:void(0);" onclick='SetEmployeeDemonation("<%# Eval("ID")%>", "<%# Eval("employeeNo")%>");' class="mya">设置</a>
                           </ItemTemplate>
                       </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
    <script src="Scripts/ylyj/employeeDemonation.js"></script>
</asp:Content>

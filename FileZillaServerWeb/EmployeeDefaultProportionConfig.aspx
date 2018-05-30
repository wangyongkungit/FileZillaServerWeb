<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeDefaultProportionConfig.aspx.cs" Inherits="FileZillaServerWeb.EmployeeDefaultProportionConfig"
    MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/themes/base/ylyj/employeeDefaultProportionConfig.css?v=18518" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form0001" runat="server">
        <div style="width:900px; margin:0 auto; text-align:center;">
        <h1>任务默认提成比例</h1>
        <div style="width:1000px; margin:0 auto;">
        <asp:GridView ID="gvEmployeeProportion" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvEmployeeProportion_RowDataBound" DataKeyNames="eID" CssClass="tbl"
             OnRowEditing="gvEmployeeProportion_RowEditing" OnRowUpdating="gvEmployeeProportion_RowUpdating" OnRowCancelingEdit="gvEmployeeProportion_RowCancelingEdit">
            <Columns>
                <asp:TemplateField HeaderText="编号">
                    <ItemTemplate>
                        <asp:Label ID="lblEmployeeNo" runat="server" Text='<%# Eval("employeeNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="提成比例" ItemStyle-CssClass="qualityScore_js">
                    <ItemTemplate>
                        <asp:Label ID="lblProportion" runat="server" Text='<%# Eval("Proportion") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtProportion" runat="server" Text='<%# Eval("Proportion") %>' Width="40" MaxLength="4" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="管理者">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlManager" runat="server" Enabled="false"></asp:DropDownList>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlManager" runat="server" Enabled="true"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ItemStyle-Width="200px">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" CausesValidation="true" Text="编辑" CssClass="mya"></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lbtnUpt" runat="server" CausesValidation="true" CommandName="Update" Text="更新" CssClass="mya"></asp:LinkButton>
                        <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="取消" CssClass="mya"></asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView></div>
    </form>
</asp:Content>

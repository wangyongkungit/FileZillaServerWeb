<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecialtyQualityConfig.aspx.cs" Inherits="FileZillaServerWeb.SpecialtyQualityConfig" %>

<%@ Register Src="~/UserControl/SiteMenu.ascx" TagPrefix="uc1" TagName="SiteMenu" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Content/themes/base/ylyj/specialtyqualityconfig.css?v=18426" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:90%; text-align:center;">
        <asp:GridView ID="gvSpcQlt" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvSpcQlt_RowDataBound" OnRowEditing="gvSpcQlt_RowEditing"
            OnRowUpdating="gvSpcQlt_RowUpdating" OnRowCancelingEdit="gvSpcQlt_RowCancelingEdit" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="编号">
                    <ItemTemplate>
                        <asp:Label ID="lblEmployeeNo" runat="server" Text='<%# Eval("employeeNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="专业">
                    <ItemTemplate>
                        <asp:HiddenField ID="hidSpecialty" runat="server" Value='<%# Eval("specialtyKey") %>' />
                        <asp:CheckBox ID="chkSpecialtyName" runat="server" Enabled="false" />
                        <asp:Label ID="lblSpecialtyName" runat="server" Text='<%# Eval("SpecialtyName") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField ID="hidSpecialty" runat="server" Value='<%# Eval("specialtyKey") %>' />
                        <asp:CheckBox ID="chkSpecialtyName" runat="server" />
                        <asp:Label ID="lblSpecialtyName" runat="server" Text='<%# Eval("SpecialtyName") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="质量分" ItemStyle-CssClass="qualityScore_js">
                    <ItemTemplate>
                        <asp:Label ID="lbl" runat="server" Text='<%# Eval("qualityScore") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtQualityScore" runat="server" Text='<%# Eval("qualityScore") %>' Width="40" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" MaxLength="3" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="工时倍数">
                    <ItemTemplate>
                        <asp:Label ID="lblTimeMultiple" runat="server" Text='<%# Eval("timeMultiple") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtTimeMultiple" runat="server" Text='<%# Eval("TimeMultiple") %>' Width="40" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" MaxLength="2" />
                    </EditItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="操作">
                    <EditItemTemplate>
                        <asp:LinkButton ID="lbtnUpt" runat="server" CausesValidation="true" CommandName="Update" Text="更新"></asp:LinkButton>
                        <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="取消"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" CausesValidation="true" Text="编辑"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <hr />
        <div id="cblst" runat="server" style="margin-top:20px; margin-left:50px;">
            <asp:CheckBoxList ID="cblstSpecialtyMinor" runat="server" OnDataBound="cblstSpecialtyMinor_DataBound" Width="300" RepeatDirection="Horizontal" RepeatLayout="Flow" Visible="false" ></asp:CheckBoxList>
            目标金额：
            <asp:TextBox ID="txtTargetAmount" runat="server" />
            <asp:Button ID="btnSaveTargetAmount" runat="server" MaxLength="7" OnClick="btnSaveTargetAmount_Click" Text="保存" CssClass="button" />
        </div>
        <div id="divTimeMultiple" runat="server">
            工时倍数：
            <asp:TextBox ID="txtTimeMultiple" runat="server" />
            <asp:Button ID="btnSaveTimeMultiple" runat="server" OnClick="btnSaveTimeMultiple_Click" Text="保存" CssClass="button" />
        </div>
    </div>
        <uc1:SiteMenu runat="server" ID="SiteMenu" />
    </form>
</body>
</html>

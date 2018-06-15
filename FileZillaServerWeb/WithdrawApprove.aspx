<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WithdrawApprove.aspx.cs" Inherits="FileZillaServerWeb.WithdrawApprove" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form0001" runat="server">
        <div style="width: 900px; margin: 0 auto; text-align: center;">
            <h1 id="hTitle" runat="server">提现申请审批</h1>
            <div style="width: 1000px; margin: 0 auto;">
                <asp:GridView ID="gvWithdrawApprove" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" CssClass="tbl" OnRowDataBound="gvWithdrawApprove_RowDataBound" OnRowCommand="gvWithdrawApprove_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="编号">
                            <ItemTemplate>
                                <asp:Label ID="lblEmployeeNo" runat="server" Text='<%# Eval("employeeNo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="提现金额" ItemStyle-CssClass="qualityScore_js">
                            <ItemTemplate>
                                <asp:Label ID="lblWithdraw" runat="server" Text='<%# Eval("WithdrawAmount") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="申请时间">
                            <ItemTemplate>
                                <asp:Label ID="lblApplyDate" runat="server" Text='<%# Eval("createDate") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="状态">
                            <ItemTemplate>
                                <asp:Label ID="lblApplyStatus" runat="server" Text='<%# Convert.ToBoolean(Eval("isConfirmed"))  ? "已确认" : "待确认" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作人">
                            <ItemTemplate>
                                <asp:Label ID="lblOperatePerson" runat="server" Text='<%# Eval("operatePerson") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作" ItemStyle-Width="200px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Confirm" CommandArgument='<%# Eval("ID").ToString() + "|" + Eval("employeeID").ToString() %>'
                                    Visible='<%# !Convert.ToBoolean(Eval("isConfirmed")) %>'  CausesValidation="true" Text="同意" CssClass="mya"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
        </div>
            </div>
    </form>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/themes/base/ylyj/withdrawapprove.css" rel="stylesheet" />
</asp:Content>
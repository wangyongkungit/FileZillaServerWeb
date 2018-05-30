﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalaryConfigManagement.aspx.cs" Inherits="FileZillaServerWeb.SalaryConfigManagement" %>
<asp:Content ID="head1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/themes/base/ylyj/salaryconfigmanage.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <div class="divTitle">
                    <h1>工资基本参数配置</h1>
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>姓名：</label>
                    </div>
                    <asp:TextBox ID="txtName" runat="server" />
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>编号：</label>
                    </div>
                    <asp:TextBox ID="txtEmployeeNo" runat="server" />
                </div>
                <div class="formSearch">
                </div>
                <div class="formSearch">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" CssClass="button" />
                </div>
            </div>
            <div>
                <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" OnRowDataBound="gvData_RowDataBound" OnRowEditing="gvData_RowEditing"
                    OnRowUpdating="gvData_RowUpdating" OnRowCancelingEdit="gvData_RowCancelingEdit" Width="100%" >
                    <Columns>
                        <asp:TemplateField HeaderText="编号">
                            <ItemTemplate>
                                <asp:Label ID="lblEmployeeNo" runat="server" Text='<%# Eval("employeeNo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="基本工资">
                            <ItemTemplate>
                                <asp:Label ID="lblBaseSalary" runat="server" Text='<%# Eval("BASESALARY") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBaseSalary" runat="server" Text='<%# Eval("BASESALARY") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="提成比例">
                            <ItemTemplate>
                                <asp:Label ID="lblCommission" runat="server" Text='<%# Eval("Commission") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCommission" runat="server" Text='<%# Eval("Commission") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="工龄工资">
                            <ItemTemplate>
                                <asp:Label ID="lblAgeWage" runat="server" Text='<%# Eval("AGEWAGE") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAgeWage" runat="server" Text='<%# Eval("AGEWAGE") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="住宿补贴">
                            <ItemTemplate>
                                <asp:Label ID="lblZsbt" runat="server" Text='<%# Eval("ACCOMMODATION_ALLOWANCE") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtZsbt" runat="server" Text='<%# Eval("ACCOMMODATION_ALLOWANCE") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="伙食补贴">
                            <ItemTemplate>
                                <asp:Label ID="lblHsbt" runat="server" Text='<%# Eval("MEAL_ALLOWANCE") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtHsbt" runat="server" Text='<%# Eval("MEAL_ALLOWANCE") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="其他收入">
                            <ItemTemplate>
                                <asp:Label ID="lblQtsr" runat="server" Text='<%# Eval("OTHERWAGE") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtQtsr" runat="server" Text='<%# Eval("OTHERWAGE") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="个人社保">
                            <ItemTemplate>
                                <asp:Label ID="lblGrsb" runat="server" Text='<%# Eval("SOCIALSECURITY_INDIVIDUAL") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtGrsb" runat="server" Text='<%# Eval("SOCIALSECURITY_INDIVIDUAL") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="企业社保">
                            <ItemTemplate>
                                <asp:Label ID="lblQysb" runat="server" Text='<%# Eval("SOCIALSECURITY_COMPANY") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtQysb" runat="server" Text='<%# Eval("SOCIALSECURITY_COMPANY") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="个人公积金">
                            <ItemTemplate>
                                <asp:Label ID="lblGrgjj" runat="server" Text='<%# Eval("HOUSINGPROVIDENTFUND_INDIVIDUAL") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtGrgjj" runat="server" Text='<%# Eval("HOUSINGPROVIDENTFUND_INDIVIDUAL") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="企业公积金">
                            <ItemTemplate>
                                <asp:Label ID="lblQygjj" runat="server" Text='<%# Eval("HOUSINGPROVIDENTFUND_COMPANY") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtQygjj" runat="server" Text='<%# Eval("HOUSINGPROVIDENTFUND_COMPANY") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbtnUpt" runat="server" CausesValidation="true" CommandName="Update" Text="更新"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="取消"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                            <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                Text="编辑" OnClientClick="return confirm(确认要编辑吗？);"></asp:LinkButton>
                        
                            <%--<asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="删除" OnClientClick="return confirm(确认要删除吗？);"></asp:LinkButton>--%>
                            <%--<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Select"
                                Text="选择"></asp:LinkButton>--%>
                        </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <RowStyle BackColor="White" ForeColor="#003399" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                </asp:GridView>
            </div>
        </div>
    </form>
</asp:Content>

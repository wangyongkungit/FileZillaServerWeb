<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rwmb.aspx.cs" Inherits="FileZillaServerWeb.rwmb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>rwmb</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" OnRowEditing="gvData_RowEditing"
            OnRowUpdating="gvData_RowUpdating" OnRowCancelingEdit="gvData_RowCancelingEdit" Width="500">
            <Columns>
                <asp:TemplateField HeaderText="员工编号">
                    <ItemTemplate>
                        <asp:Label ID="lblEmployeeNo" runat="server" Text='<%# Eval("EMPLOYEENO") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="目标值">
                    <ItemTemplate>
                        <asp:Label ID="lblObjectiveValue" runat="server" Text='<%# Eval("OBJECTIVEVALUE") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtObjectiveValue" runat="server" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" Text='<%# Eval("OBJECTIVEVALUE") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="误差值">
                    <ItemTemplate>
                        <asp:Label ID="lblD_value" runat="server" Text='<%# Eval("D_VALUE") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtD_value" runat="server" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" Text='<%# Eval("D_VALUE") %>' />
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
                        </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>

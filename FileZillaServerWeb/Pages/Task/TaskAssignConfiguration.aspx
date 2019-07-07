<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaskAssignConfiguration.aspx.cs" Inherits="FileZillaServerWeb.TaskAssignConfiguration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/themes/base/ylyj/taskassignconfiguration.css?v=18503" rel="stylesheet" />
    <script src="/Scripts/ylyj/taskassignconfiguration.js?v=18426"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <div class="divTitle">
                    <h1>任务分配参数配置</h1>
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>员工：</label>
                    </div>
                    <asp:DropDownList ID="ddlEmployeeName" runat="server" />
                </div>
                <div class="formSearch">
                    <asp:Button ID="btnAddToList" runat="server" Text="添加到任务分配列表" OnClick="btnAddToList_Click" CssClass="button" Width="200"/>
                </div>
            </div>
            <div style="width:100%; text-align:center;">
                <div>
                    <h4 style="display:inline-block; margin-right:20px;">已加入自动分配列表的员工</h4>
                    <asp:CheckBox ID="cbxShowVisible" runat="server" Text="显示全部" OnCheckedChanged="cbxShowVisible_CheckedChanged" AutoPostBack="true" />
                </div>
                <asp:GridView ID="gvEmpTaskAssignConfig" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" OnRowUpdating="gvEmpTaskAssignConfig_RowUpdating" Width="100%" CssClass="tbl"
                     OnRowDataBound="gvEmpTaskAssignConfig_RowDataBound" RowStyle-Height="50">
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
                       <asp:TemplateField HeaderText="">
                           <ItemTemplate>                               
                               <asp:HiddenField ID="hidEmpID" runat="server" Value='<%# Eval("employeeID")%>' />
                               <asp:Label ID="lblTip" runat="server" Font-Size="Smaller"></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="是否启用">
                           <ItemTemplate>
                               <asp:Label ID="lblAvailable" runat="server" Text='<%# Convert.ToString(Eval("Available")) == "1" ? "启用" : "禁用" %>' ForeColor='<%# Convert.ToString(Eval("Available")) == "1" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="操作">
                           <ItemTemplate>
                               <asp:LinkButton ID="lbtn" runat="server" Text='<%# Convert.ToString(Eval("Available")) == "1" ? "禁用" : "启用" %>' CommandName="Update" CommandArgument='<%# Eval("ID") %>' CssClass="mya" />
                               <a href="javascript:void(0);" onclick='SetQualityScore("<%# Eval("employeeID")%>", "<%# Eval("employeeNo")%>", "0");' class="mya">设置</a>
                           </ItemTemplate>
                       </asp:TemplateField>
                   </Columns>
                </asp:GridView>
            </div>
            <hr />
            <div style="width:100%; text-align:center; margin-top:18px;">
                <h4>达标降权配置</h4>
                <asp:GridView ID="gvRightdown" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" OnRowUpdating="gvRightdown_RowUpdating" OnRowEditing="gvRightdown_RowEditing" Width="100%"
                    OnRowCancelingEdit="gvRightdown_RowCancelingEdit" OnRowCommand="gvRightdown_RowCommand" CssClass="tbl">
                    <Columns>
                        <asp:TemplateField HeaderText="区间起始值">
                            <ItemTemplate>
                                <%--<asp:HiddenField ID="hidSpecialty" runat="server" Value='<%# Eval("specialtyKey") %>' />--%>
                                <asp:Label ID="lblFromvalue" runat="server" Text='<%# Eval("fromvalue") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <%--<asp:HiddenField ID="hidSpecialty" runat="server" Value='<%# Eval("specialtyKey") %>' />--%>
                                <asp:TextBox ID="txtFromvalue" runat="server" Text='<%# Eval("fromvalue") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="区间结束值">
                            <ItemTemplate>
                                <%--<asp:HiddenField ID="hidSpecialty" runat="server" Value='<%# Eval("specialtyKey") %>' />--%>
                                <asp:Label ID="lblTovalue" runat="server" Text='<%# Eval("Tovalue") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <%--<asp:HiddenField ID="hidSpecialty" runat="server" Value='<%# Eval("specialtyKey") %>' />--%>
                                <asp:TextBox ID="txtTovalue" runat="server" Text='<%# Eval("tovalue") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="降权比例">
                            <ItemTemplate>
                                <asp:Label ID="lblRightdownPercent" runat="server" Text='<%# Convert.ToDecimal(Eval("RIGHTDOWNPERCENT")).ToString("P") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRightdownPercent" runat="server" Text='<%# (Convert.ToDecimal(Eval("RIGHTDOWNPERCENT")) * 100).ToString("G0") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>                        
                        <asp:TemplateField HeaderText="操作" ItemStyle-Width="22%">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbtnUpt" runat="server" CausesValidation="true" CommandName="Update" Text="更新" CssClass="mya"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="取消" CssClass="mya"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Del" CausesValidation="true" OnClientClick="return confirm('确认删除？');" Text="删除" CssClass="mya"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" CausesValidation="true" Text="编辑" CssClass="mya"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <table class="tbl">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtFromvalue02" runat="server" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTovalue02" runat="server" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRightdownPercent02" runat="server" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')"></asp:TextBox>
                        </td>
                        <td style="width:22%;">
                            <asp:Button ID="btnSaveRightdown" runat="server" Text="添加" OnClick="btnSaveRightdown_Click" CssClass="mya" />
                        </td>
                    </tr>
                </table>
            </div>
            <hr />
            <div style="width:100%; text-align:center; margin-top:18px;">
                <h4>评分参数比例配置</h4>
                <asp:GridView ID="gvWeights" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" OnRowUpdating="gvWeights_RowUpdating" OnRowEditing="gvWeights_RowEditing" Width="100%"
                    OnRowCancelingEdit="gvWeights_RowCancelingEdit" OnRowCommand="gvWeights_RowCommand" CssClass="tbl">
                    <Columns>
                        <asp:TemplateField HeaderText="项目名称">
                            <ItemTemplate>
                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="项目值">
                            <ItemTemplate>
                                <asp:Label ID="lblItemValue" runat="server" Text='<%# Convert.ToDecimal(Eval("ItemValue")).ToString("P") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtItemValue" runat="server" Text='<%# (Convert.ToDecimal(Eval("ItemValue")) * 100).ToString("G0")  %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>      
                        <asp:TemplateField HeaderText="操作">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbtnUpt" runat="server" CausesValidation="true" CommandName="Update" Text="更新" CssClass="mya"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="取消" CssClass="mya"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" CausesValidation="true" Text="编辑" CssClass="mya"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</asp:Content>

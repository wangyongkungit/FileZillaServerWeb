<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaskProportionManagement.aspx.cs" Inherits="FileZillaServerWeb.TaskProportionManagement" %>

<asp:Content ID="head1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/themes/base/ylyj/taskproportion.css?v=18426" rel="stylesheet" />
    <script type="text/javascript">
        //检查输入比例
        function CheckInput() {
            var objTable = document.getElementById("gvData");
            var inputs = objTable.getElementsByTagName("input");
            var haveValueValue = 0;
            var noValueIndex = -1;
            for (var i = 0; i < inputs.length; i++) {
                if (!isNaN(inputs[i].value)) {
                    haveValueValue += parseInt(inputs[i].value);
                }
            }
            if (haveValueValue <= 100) {
                return true;
            }
            else {
                //alert(haveValueValue);
                alert('比例之和不能超过100%！');
                return false;
            }
        }
        //自动填充比例文本框的值
        function AutoFillData() {
            var objTable = document.getElementById("gvData");
            var inputs = $("input[type='text']"); //objTable.getElementsByTagName("input");
            var haveValueAmount = 0;//已经有值的文本框数量
            var haveValue = 0;//已经有值的文本框中的数值累加和
            var noValueIndex = -1;
            //for (var n = 0; n < inputs.length; n++) {
            //    if (inputs[n].type != "text") {
            //        //inputs.delete(n);
            //        inputs.remove(inputs[n]);
            //    }
            //}
            for (var i = 0; i < inputs.length; i++) {
                //if (inputs[i].type == "text") {
                    if (inputs[i].value) {
                        console.log(inputs[i].value);
                        haveValue += parseInt(inputs[i].value);
                        console.log(haveValue);
                        haveValueAmount++;//有值的文本框数量自增1
                    }
                        //如果文本框值为空，记录下来，便于在最后为其赋值
                    else {
                        noValueIndex = i;
                    }
                //}
            }
            //表格记录总行数
            var rows = parseInt('<%= gvData.Rows.Count %>');
            //如果剩余最后一个未输入值的文本框，则根据已输入的其他文本框的值自动赋值
            if (haveValueAmount + 1 == rows) {
                inputs[noValueIndex].value = 100 - haveValue;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div style="display: none;">
            <asp:HiddenField ID="hidNoValueText" runat="server" />
        </div>
        <div id="container" class="container">
            <div>
                <h1>任务分成管理</h1>
            </div>
            <div class="divGridView">
                <div style="width: 400px; height: 45px; line-height: 45px; font-size: large; text-align: center;">
                    <label style="font-size: 16px;">任务编号：</label>
                    <asp:Label ID="lblTaskNo" runat="server" Font-Bold="true" />
                </div>
                <asp:GridView ID="gvData" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" OnRowDataBound="gvData_RowDataBound" OnRowCommand="gvData_RowCommand" DataKeyNames="ID" Width="400">
                    <Columns>
                        <asp:TemplateField HeaderText="完成人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="160" HeaderStyle-BackColor="#cd1637" HeaderStyle-ForeColor="White" HeaderStyle-Height="40">
                            <ItemTemplate>
                                <asp:Label ID="lblFinishedPerson" runat="server" Text='<%# Eval("EMPLOYEENO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="分配比例" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#cd1637" HeaderStyle-ForeColor="White" HeaderStyle-Height="40">
                            <ItemTemplate>
                                <asp:TextBox ID="txtProportion" runat="server" ClientIDMode="Static" Text='<%# Eval("PROPORTION") %>' onblur="AutoFillData();" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" MaxLength="3" />
                                <%--<input type="text" runat="server" id="txtProportion" value='<%# Eval("PROPORTION") %>' onblur="AutoFillData();" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" length="3" />--%>
                                <asp:Label ID="lblPercent" runat="server" Text="&#37;" Font-Names="微软雅黑"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" HeaderStyle-BackColor="#cd1637" HeaderStyle-ForeColor="White" HeaderStyle-Height="40">
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" Text="删除" CommandArgument='<%# Eval("ID") %>' CommandName="del" OnClientClick="return confirm('确定要删除吗？')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="divBtn">
                <asp:Button ID="btnOk" runat="server" Text="确定" OnClientClick="return CheckInput();" OnClick="btnOk_Click" />
            </div>
        </div>
    </form>
</asp:Content>

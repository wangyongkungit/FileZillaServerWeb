<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeAdd.aspx.cs" Inherits="FileZillaServerWeb.EmployeeAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/themes/base/ylyj/employeeadd.css?v=18426" rel="stylesheet" />
    <script src="Scripts/ylyj/employeeadd.js?v=18426"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container" class="container">
        <form id="form1" runat="server">
            <h1>员工<asp:Label ID="lblNewOrEdit" runat="server" Text="添加"></asp:Label></h1>
            <div class="empadd">
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>用户类型：</label>
                    </div>
                    <asp:RadioButton ID="rdbGMO" runat="server" Text="总经办" GroupName="userType" Width="65" AutoPostBack="true" OnCheckedChanged="rdbUserType_CheckedChanged" />
                    <asp:RadioButton ID="rdbCustomerService" runat="server" Text="客服" GroupName="userType" Width="60" AutoPostBack="true" OnCheckedChanged="rdbUserType_CheckedChanged" required="required" />
                    <asp:RadioButton ID="rdbEmployee" runat="server" Text="造价员" GroupName="userType" Width="65" AutoPostBack="true" OnCheckedChanged="rdbUserType_CheckedChanged" required="required" />
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>编号：</label>
                    </div>
                    <asp:TextBox ID="txtEmployeeNo" runat="server" required="required" />
                    <span class="asterisk">*</span>
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>姓名：</label>
                    </div>
                    <asp:TextBox ID="txtName" runat="server" required="required" />
                    <span class="asterisk">*</span>
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>性别：</label>
                    </div>
                    <asp:RadioButton ID="rdbMale" runat="server" Text="男" GroupName="gender" Width="50" />
                    <asp:RadioButton ID="rdbFemale" runat="server" Text="女" GroupName="gender" Width="50" />
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>出生日期：</label>
                    </div>
                    <asp:TextBox ID="txtBirthDate" runat="server" CssClass="Wdate" onFocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:new Date()})"/>
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>手机号码：</label>
                    </div>
                    <asp:TextBox ID="txtMobilePhone" runat="server" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" MaxLength="11" required="required" />
                    <span class="asterisk">*</span>
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>电子邮箱：</label>
                    </div>
                    <asp:TextBox ID="txtEmail" runat="server" type="email" />
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>是否分部领导：</label>
                    </div>
                    <asp:RadioButtonList ID="rblIsBranchLeader" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Text="否" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="是"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>钉钉同步创建：</label>
                    </div>
                    <input id="rdbSync" type="radio" name="sync" value="rdbSync" required="required" checked="checked" /><label for="rdbSync">是</label>
                    <input id="rdbNoSync" type="radio" name="sync" value="rdbNoSync" required="required" /><label for="rdbNoSync">否</label>
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                    </div>
                    <asp:Button ID="btnOK" runat="server" Text="添加" OnClick="btnOK_Click" />
                </div>
                <div class="valGroup" style="border-top:solid 2px #ff6a00">
                    <div class="groupLeft">
                        <label>钉钉UserId：</label>
                    </div>
                    <asp:TextBox ID="txtDingtalkUserId" runat="server" ClientIDMode="Static" />
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                    </div>
                    <asp:Button ID="btnAddDingtalkUserId" runat="server" Text="关联钉钉UserId" OnClientClick="return CheckInputDdId();" OnClick="btnAddDingtalkUserId_Click" />
                    <asp:Button ID="btnInvokeWebService" runat="server" Text="Invoke" OnClick="btnInvokeWebService_Click" Visible="false" />
                </div>
            </div>
        </form>
    </div>
</asp:Content>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteMenu.ascx.cs" Inherits="FileZillaServerWeb.UserControl.SiteMenu" %>
<link href="/Content/themes/base/ylyj/site.css" rel="stylesheet" />

<div style="float:right; position:absolute; right:100px; top: 100px;">
    <ul class="list-menu"> 
        
	<li>我的菜单
		<ul> 
            <asp:Repeater ID="rptMenu" runat="server">
                <ItemTemplate>
                    <li>
                        <a href='<%# Eval("Path") %>' title='<%# Eval("remarks") %>' style="width:150px;"><%# Eval("Name") %></a>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
		</ul> 
	</li>
</ul>
</div>
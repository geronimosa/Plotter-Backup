<%@ Page Language="VB" MasterPageFile="~/Default.master" AutoEventWireup="false"  CodeFile="Users.aspx.vb" Inherits="Users" title="Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main"  Runat="Server">
<div align=center>
  <table border="0" cellpadding="0" cellspacing="0" style="width: 800; height: 500">
        <tr>
            <td style="width: 200px" valign="top">
                &nbsp;<asp:ListBox ID="UserList" runat="server" Height="304px" Width="200px" AutoPostBack="True"></asp:ListBox></td>
            <td valign="top">
            <table width=100% border=0>
                <tr>
                    <td style="width: 9px; height: 23px">
                    </td>
                    <td style="height: 23px">
                    </td>
                    <td style="height: 23px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td style="width: 9px">
                    </td>
                    <td>
                        User name:</td><td style="text-align: left">
                    <asp:TextBox ID="Username" runat="server" ReadOnly="True" CssClass="textbox" Width="152px"></asp:TextBox></td></tr>
                <tr>
                    <td style="width: 9px">
                    </td>
                    <td>Password:</td><td style="text-align: left">
                    <asp:TextBox ID="thePassword" runat="server" Width="152px" CssClass="textbox"></asp:TextBox></td></tr>
                <tr>
                    <td style="width: 9px">
                    </td>
                    <td>Email:</td><td style="text-align: left">
                    <asp:TextBox ID="Email" runat="server" Width="240px" CssClass="textbox"></asp:TextBox></td></tr>
                <tr>
                    <td style="width: 9px">
                    </td>
                    <td>Number:</td><td style="text-align: left">
                    <asp:TextBox ID="Number" runat="server" Width="152px" CssClass="textbox"></asp:TextBox></td></tr>
                <tr>
                    <td style="width: 9px">
                    </td>
                    <td>Region:</td><td style="text-align: left">
                    <asp:DropDownList ID="RegionList" runat="server" Width="160px">
                    </asp:DropDownList></td></tr>                                    
                <tr>
                    <td style="width: 9px">
                    </td>
                    <td>Role:</td><td style="text-align: left">
                    <asp:RadioButtonList ID="Role" runat="server" Width="112px">
                        <asp:ListItem Value="user">User</asp:ListItem>
                        <asp:ListItem Value="admin">Admin</asp:ListItem>
                        <asp:ListItem Value="super">SuperAdmin</asp:ListItem>
                    </asp:RadioButtonList></td></tr>
                <tr>
                    <td style="width: 9px">
                    </td>
                    <td>Authorised:</td><td style="text-align: left">
                    <asp:CheckBox ID="Authorised" runat="server" /></td></tr>
                <tr>
                    <td style="width: 9px">
                    </td>
                    <td>
                    </td>
                    <td style="text-align: left">
                    </td>
                </tr>
                <tr>
                    <td style="width: 9px">
                    </td>
                    <td>
                    </td>
                    <td style="text-align: left">
                        &nbsp;&nbsp;
                        <asp:ImageButton ID="SaveButton" runat="server" AlternateText="Save" CommandName="Save"
                            CssClass="button" SkinID="save" />
                        &nbsp;
                        <asp:ImageButton ID="DeleteButton" runat="server" AlternateText="Delete" CommandName="Delete"
                            CssClass="button" SkinID="delete" /></td>
                </tr>
            
            </table>
            
            </td>
        </tr>
    </table>
</div>
         
</asp:Content>


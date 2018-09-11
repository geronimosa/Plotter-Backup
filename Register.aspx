<%@ Page Language="VB" MasterPageFile="~/Default.master" Title="Register"
    CodeFile="Register.aspx.vb" Inherits="Register_aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" Runat="server">

    <div class="shim column"></div>

    <div class="page"  id="register">
		<div id="content">
            <h3>
                Register for access to view</h3>
            <p>
                <strong>Accounts will be activated pending the approval of the Administrator.</strong>
</p>
                            <table border="0">
                                <tr>
                                    <td align="center" colspan="2">
                                        <strong>Register here for access.</strong></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="UserName" runat="server"></asp:TextBox>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="Email" runat="server" Width="280px"></asp:TextBox>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question">Contact Number</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="Question" runat="server"></asp:TextBox>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Region:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="Answer" runat="server"></asp:TextBox>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" id="ResponseArea" runat="server">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        &nbsp;<asp:Button ID="RegisterUser" runat="server" Text="Register" /></td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="color: red">
                                        &nbsp;</td>
                                </tr>
                            </table>
        </div>
    </div>

</asp:Content>
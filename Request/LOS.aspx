<%@ Page Title="" Language="VB" MasterPageFile="~/Default.master" AutoEventWireup="false" CodeFile="LOS.aspx.vb" Inherits="Request_LOS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" Runat="Server">
    Line of Site<br />
    <br />
    <strong>Submit an address for a Line of Site Test</strong><br />
    <table align="center" style="width: 50%; border-style: solid; border-width: 1px">
        <tr>
            <td style="text-align: left; width: 172px">
                &nbsp;</td>
            <td style="text-align: left" id="StatusRow" runat=server colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left; width: 172px">
                Select Region</td>
            <td style="text-align: left" colspan="2">
                <asp:DropDownList runat=server ID="RegionList" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 172px">
                Exact Street address</td>
            <td style="text-align: left" colspan="2">
                <asp:TextBox  runat=server ID="StreetAddress" runat="server" Height="54px" 
                    TextMode="MultiLine" Width="230px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 172px">
                Longitude</td>
            <td style="text-align: left; width: 156px;">
                <asp:TextBox  runat=server ID="Longitude" runat="server">0° 0&#39; 0&quot;</asp:TextBox>
            &nbsp;</td>
            <td style="text-align: left" rowspan="2">
                Either leave the ##° ##&#39; ##&quot;
                <br />
                Or replace with decimal values</td>
        </tr>
        <tr>
            <td style="text-align: left; width: 172px">
                Latitude</td>
            <td style="text-align: left; width: 156px;">
                <asp:TextBox runat=server ID="Latitude" runat="server">0° 0&#39; 0&quot;</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 172px">
                Your full Name</td>
            <td style="text-align: left" colspan="2">
                <asp:TextBox runat=server ID="FullName" runat="server" Width="227px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 172px">
                Your Contact Number</td>
            <td style="text-align: left" colspan="2">
                <asp:TextBox runat=server ID="ContactNumber" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 172px">
                Your Email Address</td>
            <td style="text-align: left" colspan="2">
                <asp:TextBox ID="Email" runat="server" Width="315px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 172px" id="RandomText" runat=server>
                What is 5 plus 10</td>
            <td style="text-align: left" colspan="2">
                <asp:TextBox runat=server ID="Maths" runat="server"></asp:TextBox>
            &nbsp;For validating you are not a robot.</td>
        </tr>
        <tr>
            <td style="width: 172px">
                &nbsp;</td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 172px">
                <asp:Button runat=server ID="CancelButton" runat="server" Text="Cancel" />
            </td>
            <td style="text-align: left" colspan="2">
                <asp:Button  runat=server ID="SubmitButton" runat="server" Text="Submit" />
            </td>
        </tr>
        <tr>
            <td style="width: 172px">
                &nbsp;</td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 172px">
                &nbsp;</td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
    </table>
&nbsp;
</asp:Content>


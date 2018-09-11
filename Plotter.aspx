<%@ Page Language="VB" MasterPageFile="~/Default.master" AutoEventWireup="false" CodeFile="Plotter.aspx.vb" Inherits="Plotter"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" Runat="Server">
<script type="text/javascript" src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAxN36sT_1e_mQReV2XujZjxSxDUeKyisjqtbiHmPVZHVcIQE3pBTkJmY8e6yk8NOJfMLjtlhIIa_qiQ"></script>
<script type="text/javascript" src="http://www.google.com/uds/api?file=uds.js&v=1.0&key=ABQIAAAAxN36sT_1e_mQReV2XujZjxSxDUeKyisjqtbiHmPVZHVcIQE3pBTkJmY8e6yk8NOJfMLjtlhIIa_qiQ"></script>
<script type="text/javascript" src=gScript.js></script>

<script type="text/javascript">
function findaddress(searchingfor) {    
    getTextAddress(searchingfor,'TextBox1','ana_lat','ana_long','dec_lat','dec_long');
}
</script>
    
    <table border="0" cellpadding="1" cellspacing="1" class="ContentBG-Black" align=center style="width: 816px" >
        <tr>
            <td colspan="1" style="width: 186px" rowspan="19">
                &nbsp;
            </td>
            <td colspan="5" >
                <strong><span style="text-decoration: underline">V2.01</span></strong></td>
            <td colspan="1">
                List of towers within 120 KM's</td>
        </tr>
        <tr>
            <td colspan="5" style="border-right: #ffffcc thin outset; border-top: #ffffcc thin outset; border-left: #ffffcc thin outset; color: whitesmoke; border-bottom: #ffffcc thin outset; height: 30px; background-color: midnightblue">
                Enter the details of the location you want to connect <strong><span style="color: yellow">
                    from</span></strong>.</td>
            <td colspan="1" rowspan="19" valign="top" align="center">
                <div  id="details" runat="server" style="font-size: 10px; width: 328px; color: black; font-family: Arial; height: 328px; background-color: white; padding-right: 5px; padding-left: 5px; padding-bottom: 2px; overflow: auto; padding-top: 2px; vertical-align: top; text-align: left; border-top-width: 2px; border-left-width: 2px; border-left-color: #ffff99; border-bottom-width: 2px; border-bottom-color: #ffff99; border-top-color: #ffff99; border-right-width: 2px; border-right-color: #ffff99; left: 0px; top: 0px;" >
                
                </div>
                <br />
                <asp:HyperLink ID="PlotTen" runat="server" NavigateUrl="~/Simple2.aspx" Style="border-right: #cc3333 thin ridge;
                         padding-right: 5px; border-top: #cc3333 thin ridge; padding-left: 5px; padding-bottom: 5px;
                         border-left: #cc3333 thin ridge; cursor: hand; color: whitesmoke;
                         padding-top: 5px; border-bottom: #cc3333 thin ridge;  background-color: red"
                         Target="_new" Enabled="False">No Towers</asp:HyperLink>                
                
            </td>
        </tr>
        <tr>
            <td colspan="5" style="height: 25px">
                Search Address
                <input type=text id=searcher value='' style="width: 232px; font-size: 12px;" />
                <input type=button value='Search Google' onclick='findaddress(searcher.value);' style="font-size: 12px" id="Button1" /></td>
        </tr>
        <tr>
            <td >
                Name</td>
            <td align="left" colspan="4">
                <asp:TextBox ID="TextBox1" runat="server" Width="248px" Font-Size="Small"></asp:TextBox></td>
        </tr>
        <tr>
            <td  colspan="5" style="border-right: #ffffcc thin outset; border-top: #ffffcc thin outset; border-left: #ffffcc thin outset; color: whitesmoke; border-bottom: #ffffcc thin outset;  background-color: #333366">
            <table border=0 cellpadding=0 cellspacing=0 align=center>
<tr><td>
                UNC Format Required eg&nbsp; <span style="color: #ff6600">29°</span><span style="color: #ff3300">19'</span><span
                    style="color: #ff0000">12.05"</span><span style="color: #cc0000">E<span style="color: #666666">
                    </span></span>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td style="height: 26px"></td>
<td></td></tr>
</table>  
                </td>
        </tr>
        <tr>
            <td  align="right">
                <span style="color: #333399; background-color: #ffff33"><strong>&nbsp;1&nbsp;</strong></span></td>
            <td align="right">
                Lattitude</td>
            <td align="left" colspan="3">
                <asp:TextBox ID="ana_lat" runat="server" AutoPostBack="True" Font-Size="Small" Width="90px"></asp:TextBox>
                &nbsp;
                <asp:Button ID="ButCopy" runat="server" Text="Copy" BackColor="#FFC0C0" BorderStyle="Outset" BorderWidth="1px" Font-Size="12px" ToolTip="Copies Name and Latitude and Longitude details" Width="48px" /></td>
        </tr>
        <tr>
            <td  align="right" >
                <strong><span style="color: #000099; background-color: #ffff33">&nbsp;2&nbsp;</span></strong></td>
            <td align="right" >
                Longitude</td>
            <td align="left" colspan="3">
                <asp:TextBox ID="ana_long" runat="server" AutoPostBack="True" Font-Size="Small" Width="90px"></asp:TextBox>
                &nbsp;
                <asp:Button ID="ButPaste" runat="server" Text="Paste" BackColor="#FFC0C0" BorderStyle="Outset" BorderWidth="1px" Font-Size="12px" Enabled="False" Width="48px" /></td>
        </tr>
        <tr>
            <td align="right">
                <span style="background-color: #ffff00"><strong><span style="color: #330099">&nbsp;3&nbsp;</span></strong>
                </span>
            </td>
            <td align="right" >
                Height
            </td>
            <td align="left" colspan="3">
                <asp:TextBox ID="TowerHeight1" runat="server" Width="64px" Font-Size="Small">3</asp:TextBox>
                Meters above the ground</td>
        </tr>
        <tr>
            <td align="right" style="height: 21px" >
                <span style="background-color: #ffff00"><strong><span style="color: #330099">&nbsp;4&nbsp;</span></strong></span></td>
            <td align="right" style="height: 21px" >
                Frequency</td>
            <td align="left" colspan="3" style="height: 21px">
                <asp:DropDownList ID="Frequency" runat="server" Font-Size="Smaller">
                    <asp:ListItem Selected="True" Value="5.8">5.8 GHz</asp:ListItem>
                    <asp:ListItem Value="2.4">2.4 GHz</asp:ListItem>
                </asp:DropDownList>
                - Default 5.8GHz</td>
        </tr>
        
        <tr>
            <td align="right" >
                <strong><span style="color: #330099; background-color: #ffff00">&nbsp;5&nbsp;</span></strong></td>
            <td align="center" colspan="4">
                <strong>Get Nearest towers to you. &nbsp; &nbsp; </strong>
                <asp:Button ID="FindClosest" runat="server" Enabled="False" Text="Search"
                    Width="64px" Font-Bold="True" Font-Size="Smaller" /></td>
        </tr>
        <tr>
            <td align="center" colspan="5" style="border-right: #ffffcc thin outset; border-top: #ffffcc thin outset; border-left: #ffffcc thin outset; color: whitesmoke; border-bottom: #ffffcc thin outset; height: 30px; background-color: #333366" >
                <span>&nbsp; &nbsp; &nbsp; Select a Tower from the list below to connect <strong><span
                    style="color: yellow">to</span></strong>. &nbsp; &nbsp;</span></td>
        </tr>
        <tr>
            <td align="right" >
                <strong>
                Region</strong></td>
            <td align="right" >
                <asp:DropDownList ID="RegionList" runat="server" AutoPostBack="True" Font-Size="Smaller">
                </asp:DropDownList></td>
            <td align="left" colspan="3">
                <strong>Tower </strong>
                <asp:DropDownList ID="TowerList" runat="server" AutoPostBack="True" Width="168px" Font-Size="Smaller">
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                Distance</td>
            <td align="left" colspan="2">
                <asp:TextBox ID="TheDistance" runat="server" BorderStyle="None" ReadOnly="True" Font-Size="Smaller" Width="24px"></asp:TextBox>
                KM</td>
            <td rowspan="3" align="center" >
            <table cellpadding=0 cellspacing=0 border=0 style="height: 64px" align="center">
            <tr><td ><asp:Hyperlink ID="MapURL" runat="server" NavigateUrl="~/Maps.aspx"  style="border-right: #cc3333 thin ridge; padding-right: 5px; border-top: #cc3333 thin ridge; padding-left: 5px; padding-bottom: 5px; border-left: #cc3333 thin ridge; cursor: hand; color: whitesmoke; padding-top: 5px; border-bottom: #cc3333 thin ridge; background-color: red; width: 200px; height: 18px;">View Map</asp:Hyperlink></td></tr>
            <tr><td ><asp:Hyperlink ID="LinkToPlot" runat="server" NavigateUrl="~/Simple1.aspx"  style="border-right: #cc3333 thin ridge; padding-right: 5px; border-top: #cc3333 thin ridge; padding-left: 5px; padding-bottom: 5px; border-left: #cc3333 thin ridge; cursor: hand; color: whitesmoke; padding-top: 5px; border-bottom: #cc3333 thin ridge; background-color: red; width: 200px; height: 18px;" Enabled="False" Visible="False">View Plot</asp:Hyperlink></td></tr>
            
            </table>
                
                
                </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                Heading</td>
            <td align="left" colspan="2" nowrap="noWrap">
                <asp:TextBox ID="TheHeading" runat="server" BorderStyle="None" ReadOnly="True" Font-Size="Smaller" Width="24px"></asp:TextBox>
                ° True bearing</td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                Height above Ground</td>
            <td align="left" colspan="2">
                <asp:TextBox ID="TowerHeight2" runat="server" BorderStyle="None" ReadOnly="True"
                    Width="24px" Font-Size="Smaller">0</asp:TextBox>
                Meters</td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                Co-Ordinates :</td>
            <td align="left" colspan="3" >
                Lat :<asp:TextBox ID="lat2dec" runat="server" BorderStyle="None" Font-Size="Small" ReadOnly="True"
                    Width="96px"></asp:TextBox>
                Lon:<asp:TextBox ID="lon2dec" runat="server" BorderStyle="None" Font-Size="Small" ReadOnly="True"
                    Width="96px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="center" colspan="5" style="border-right: #ffffcc thin outset; border-top: #ffffcc thin outset; border-left: #ffffcc thin outset; color: whitesmoke; border-bottom: #ffffcc thin outset; background-color: #333366">
                <asp:TextBox ID="Status" runat="server" BorderStyle="None" Font-Bold="True" ForeColor="Red"
                    ReadOnly="True" Width="448px" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="center" colspan="5">
                <asp:TextBox ID="Reciprocal" runat="server" BorderStyle="None" ReadOnly="True" Width="32px" Visible="False">0</asp:TextBox>
                <asp:TextBox ID="Bearing" runat="server" BorderStyle="None" ReadOnly="True"
                    Width="16px" Visible="False">0</asp:TextBox>
                &nbsp;<asp:TextBox ID="lat2" runat="server" BorderStyle="None" Font-Size="Small" ReadOnly="True"
                    Width="16px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="lon2" runat="server" BorderStyle="None" Font-Size="Small" ReadOnly="True"
                    Width="16px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="dec_lat" runat="server" BorderStyle="None" Width="24px" Font-Size="Smaller" Visible="False"></asp:TextBox>
                <asp:TextBox ID="dec_long" runat="server" BorderStyle="None" Width="24px" Font-Size="Smaller" Visible="False"></asp:TextBox>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
    </table>


  
    
</asp:Content>


<%@ Page Language="VB" MasterPageFile="~/Default.master" AutoEventWireup="false" CodeFile="Towers.aspx.vb" Inherits="Towers" title="Towers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" Runat="Server">

<script type="text/javascript" src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAxN36sT_1e_mQReV2XujZjxSxDUeKyisjqtbiHmPVZHVcIQE3pBTkJmY8e6yk8NOJfMLjtlhIIa_qiQ"></script>
<script type="text/javascript" src="http://www.google.com/uds/api?file=uds.js&v=1.0&key=ABQIAAAAxN36sT_1e_mQReV2XujZjxSxDUeKyisjqtbiHmPVZHVcIQE3pBTkJmY8e6yk8NOJfMLjtlhIIa_qiQ"></script>
<script type="text/javascript" src=gScript.js></script>

<div  >
    <table align=center border=0 style="width: 944px" class="ContentBG-Black"  >
        <tr>
            <td valign=top>
                <div id="map_canvas" style="width: 600px; height: 500px"></div>    
            </td>
            <td valign=top style="width: 434px">
                <table border="0" cellpadding="1" cellspacing="0">
                    <caption style="color: #000000; height: 20px; background-color: #ffff66; text-align: center; font-weight: bold; vertical-align: middle;">
                        Edit tower.
                    </caption>
                        <tr>
                            <td nowrap="nowrap" rowspan="2" style="width: 7px">
                                &nbsp;</td>
                            <td style="height: 21px; width: 12px;" nowrap="noWrap">
                                </td>
                            <td colspan=2 style="height: 21px; width: 220px;" align="left">
                                <asp:DropDownList ID="TowerList" runat="server" AutoPostBack="True" Font-Size="Larger"
                                    Width="272px">
                                </asp:DropDownList></td>
                        </tr>
        
                        <tr>
                            <td style="width: 12px"></td>
                            <td nowrap="noWrap" colspan="3">
                                <table border=0 cellpadding=1 cellspacing=0 style="width: 312px">
                                    <tr><td align="left" colspan="2"></td></tr>
                                    <tr><td align="right">Decimal Lng</td><td align="left"><input runat="server" type=text id=rlon value='' class="textbox" style="border-right: #330000 thin outset; border-top: #330000 thin outset; font-size: 10pt; border-left: #330000 thin outset; color: #ccffff; border-bottom: #330000 thin outset; font-family: 'Courier New', Arial; background-color: #000000; text-align: right"  /></td></tr>
                                    <tr><td align="right">Decimal Lat</td><td align="left"><input runat="server"  readonly type=text id=rlat value='' class="textbox" style="border-right: #330000 thin outset; border-top: #330000 thin outset; font-size: 10pt; border-left: #330000 thin outset; color: #ccffff; border-bottom: #330000 thin outset; font-family: 'Courier New', Arial; background-color: #000000; text-align: right"  /></td></tr>
                                    <tr><td align="right">Longitude</td><td align="left"><input runat="server"  readonly type=text id=rlonu value='' class="textbox" style="border-right: #330000 thin outset; border-top: #330000 thin outset; font-size: 10pt; border-left: #330000 thin outset; color: #ccffff; border-bottom: #330000 thin outset; font-family: 'Courier New', Arial; background-color: #000000; text-align: right"  /></td></tr>
                                    <tr><td align="right">Lattitude</td><td align="left"><input runat="server"  readonly type=text id=rlatu value='' class="textbox" style="border-right: #330000 thin outset; border-top: #330000 thin outset; font-size: 10pt; border-left: #330000 thin outset; color: #ccffff; border-bottom: #330000 thin outset; font-family: 'Courier New', Arial; background-color: #000000; text-align: right" language="javascript" onclick="return rlatu_onclick()"  /></td></tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Name</td>
                                        <td align="left">
                                            <input onfocus="this.select();" type=text id=towername value='' runat="server" class="textbox" style="width: 216px" onclick="return towername_onclick()" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Height AGL.</td>
                                        <td align="left">
                                            <input type=text onfocus="this.select();"  id=AGL value='' style="width: 56px" runat="server" class="textbox" /></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 13px">
                                            Active</td>
                                        <td align="left" style="height: 13px">
                                            <asp:CheckBox ID="IsTower" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Owner</td>
                                        <td align="left">
                                            <input type=text onfocus="this.select();"  id=Owner value=''  runat="server" style="width: 192px" class="textbox" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Contact no</td>
                                        <td align="left">
                                            <input type=text onfocus="this.select();"  id=Contact value=''  runat="server" style="width: 192px" class="textbox" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Cost / Month</td>
                                        <td style="text-align: left">
                                            <input type=text onfocus="this.selecta();"  id=Monthly value='' style="width: 64px"  runat="server" class="textbox" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Region</td>
                                        <td align="left">
                                            <asp:DropDownList ID="RegionList" runat="server" Width="120px" CssClass="textbox">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 13px">
                                            Type</td>
                                        <td style="height: 13px" align="left">
                                            <asp:DropDownList ID="TypeList" runat="server" Width="152px" CssClass="textbox">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2" style="height: 13px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2" style="height: 13px">
                                            <asp:ImageButton ID="AddButton" runat="server" AlternateText="Save" CommandName="add"
                                                CssClass="button" SkinID="add" />&nbsp; <asp:ImageButton ID="SaveButton" runat="server"
                                                    AlternateText="Save" CommandName="Save" CssClass="button" SkinID="save" />&nbsp;
                                            <asp:ImageButton ID="DeleteButton" runat="server" AlternateText="Delete" CommandName="Delete"
                                                CssClass="button" SkinID="delete" />&nbsp;
                                            <asp:ImageButton ID="CancelButton" runat="server" AlternateText="Delete" CommandName="Delete"
                                                CssClass="button" SkinID="cancel" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <input type=text id=CurID value='' style="width: 56px" runat="server" class="textbox" readonly="readOnly" visible="false" /></td>
                                        <td>
                                        <input type=hidden id=rlocate value='' class="textbox"  /></td>
                                    </tr>
                                </table>
                            </td>        
                    </tr>
                </table>
                </td>
        </tr>
        
    </table>
    
    
    
</div>


<script type="text/javascript">
        window.onload=initializeedit;
function rlatu_onclick() {

}



</script>

</asp:Content>


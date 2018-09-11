<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ShowMap.aspx.vb" Inherits="ShowMap" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ShowMap</title>
</head>
<body>
    <script type="text/javascript" src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAxN36sT_1e_mQReV2XujZjxSxDUeKyisjqtbiHmPVZHVcIQE3pBTkJmY8e6yk8NOJfMLjtlhIIa_qiQ"></script>
<script type="text/javascript" src="http://www.google.com/uds/api?file=uds.js&v=1.0&key=ABQIAAAAxN36sT_1e_mQReV2XujZjxSxDUeKyisjqtbiHmPVZHVcIQE3pBTkJmY8e6yk8NOJfMLjtlhIIa_qiQ"></script>
<script type="text/javascript" src=gScript.js></script>
 <form id="form1" runat="server">
    <div>

  <div  >
      <table align=center >
      
      <tr><td colspan=3>
          Link<br />
      </td></tr>
      
      <tr>
    <td valign=top >
    
      
    <div id="map_canvas" style="width: 600px; height: 500px" ></div>
    
        
    
    </td>
    <td valign=top style="width: 434px">
    
    
    <table border="0" cellpadding="1" cellspacing="0">
    <caption style="padding-left: 20px; color: #ff0000; height: 20px; background-color: #ffff66; text-align: left">Plotting from <strong>Tower Point</strong> A </caption>
        
        
        <tr>
            <td bgcolor="#ffff66" nowrap="nowrap" rowspan="1" style="width: 18px">
            </td>
            <td align="left" colspan="3" style="height: 21px" nowrap="noWrap">
                &nbsp; &nbsp;&nbsp;
            </td>
        </tr>
    <tr>
            <td nowrap="nowrap" rowspan="1" bgcolor="#ffff66" style="width: 18px">
                &nbsp;<span style="color: #ff0000">A</span>&nbsp;
            </td>
            <td nowrap="noWrap" style="height: 15px" colspan="3">
        </td>
        
    </tr>
        <tr>
            <td align="center" colspan="4" style="height: 10px">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="1" rowspan="5" bgcolor="#ffff66" style="width: 18px">
                <span style="color: #ff0000">B</span></td>
            <td align="left" colspan="2" style="height: 20px" bgcolor="#ffff66">
                <span style="color: #cc0000"><strong>
                End Point</strong>
                Address Search Point B</span></td>
            <td align="center" colspan="1" style="height: 20px" bgcolor="#ffff66">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <input id=caddress value='' style="width: 248px" /></td>
            <td align="center" colspan="1">
            <input type=button onclick='processAddress();copydata();' value='search' id="Button1" /></td>
        </tr>
    <tr>
        <td align="left" colspan="2">
            </td>
        <td align="left">
            </td>
    </tr>
    <tr><td style="width: 47px"> Name</td><td align="left"><input id=cname value='Customer' onchange=";copydata();" onblur='processPoints();copydata();' style="width: 200px" /></td>
        <td align="left">
        <input id=cheight value='5' onblur='processPoints();copydata();' style="width: 16px" />
            Height</td>
    </tr>
    <tr><td colspan="2">
        LAT:
        <input readonly id=tlatu value='' style="width: 88px" />
        &nbsp;LON:<input readonly id=tlonu value='' style="width: 88px" />

    </td>
        <td align="left">
    <asp:Button ID="SaveSessions" runat="server" Text="Copy" style=" background-color: #FFC0C0; border-style:Outset; border-width:1px; font-size:12px;" Width="48px" PostBackUrl="~/Maps.aspx" /></td>
    </tr>
        <tr>
            <td align="center" colspan="4" style="height: 10px">
            </td>
        </tr>
        <tr>
            <td align="center" bgcolor="#ffff66" colspan="1" rowspan="5" style="width: 18px">
                <span style="color: #ff0000">C</span></td>
            <td align="left" colspan="2" style="height: 20px" bgcolor="#ffff66">
                <span style="color: #ff0000"><strong>Relay Point</strong> Calculation (optional) Point
                    C</span></td>
            <td align="center" colspan="1" style="height: 20px" bgcolor="#ffff66" nowrap="noWrap">
                <input type=button onclick='createrelay();' value='add' id="ActionButton" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <input id=raddress value='' style="width: 248px" /></td>
            <td align="center" colspan="1">
            <input type=button onclick='processAddress_r();copydata_r();' value='search' id="Button2" /></td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                </td>
            <td align="left">
                </td>
        </tr>
        <tr>
            <td align="center" style="height: 26px; width: 47px;">
                Name
            </td>
            <td align="left" style="height: 26px">
                <input id=rname value='Relay' onchange=";copydata_r();" onblur='processPoints();copydata_r();' style="width: 200px" /></td>
            <td align="left" style="height: 26px">
                <input id=rheight value='5' onblur='processPoints();copydata_r();' style="width: 16px" />
                Height</td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                LAT:
                <input readonly id=rlatu value='' style="width: 88px" />
                LON:
                <input readonly id=rlonu value='' style="width: 88px" /></td>
            <td align="left">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4" style="height: 10px">
            
            </td>
        </tr>
        <tr>
            <td id="explainit" align="left" colspan="4" style="height: 10px; border-right: #ccccff thin ridge; padding-right: 10px; border-top: #ccccff thin ridge; padding-left: 10px; padding-bottom: 10px; border-left: #ccccff thin ridge; color: #000099; padding-top: 10px; border-bottom: #ccccff thin ridge; background-color: #ffffff;">
                Select a tower from the list above.<br />
                Then find the point you need to connect to.<br />
                If you need to add a relay then click "add" and move the
                <br />
                relay marker (C) to the position of the relay.&nbsp;</td>
        </tr>
        <tr>
            <td id="plottit" align="left" colspan="4" style="height: 10px; padding-right: 10px; padding-left: 10px; padding-bottom: 1px; color: #cc0000; padding-top: 1px; background-color: #000000;">
            </td>
        </tr>
    
    </table>
    </td>
          <td valign="top">
              &nbsp;
          </td>
  </tr>
  <tr><td colspan=2 align=center>
      
  </td>
      <td align="center" colspan="1">
      </td>
  </tr>
  </table>
</div>
    


<table style="position:absolute;top:0;left:0; visibility:hidden">
                  
  <tr><td style="width: 67px">
      <asp:TextBox ID="copyname" runat="server" Width="32px"></asp:TextBox></td></tr>
  <tr><td style="width: 67px">
      <asp:TextBox ID="copyanalat" runat="server" Width="32px"></asp:TextBox></td></tr>
  <tr><td style="width: 67px">
      <asp:TextBox ID="copyanalon" runat="server" Width="32px"></asp:TextBox></td></tr>
  <tr><td style="width: 67px">
      <asp:TextBox ID="copydiglat" runat="server" Width="32px"></asp:TextBox></td></tr>
  <tr><td style="width: 67px">
      <asp:TextBox ID="copydiglon" runat="server" Width="32px"></asp:TextBox></td></tr>

  <tr><td style="width: 67px">
      <asp:TextBox ID="copyname_r" runat="server" Width="32px"></asp:TextBox></td></tr>
  <tr><td style="width: 67px">
      <asp:TextBox ID="copyanalat_r" runat="server" Width="32px"></asp:TextBox></td></tr>
  <tr><td style="width: 67px">
      <asp:TextBox ID="copyanalon_r" runat="server" Width="32px"></asp:TextBox></td></tr>
  <tr><td style="width: 67px">
      <asp:TextBox ID="copydiglat_r" runat="server" Width="32px"></asp:TextBox></td></tr>
  <tr><td style="width: 67px">
      <asp:TextBox ID="copydiglon_r" runat="server" Width="32px"></asp:TextBox></td></tr>




  <tr>
        <td style="width: 67px">
            <input type=hidden id=clocate value='Select on map' style="width: 24px" /></td>
    </tr>
  <tr>
        <td style="width: 67px">
            <input type=hidden id=rlocate value='Select on map' style="width: 24px" /></td>
    </tr>
   <tr>
        <td style="width: 67px">
            <input type=hidden id=flat value='' style="width: 32px" /></td>
    </tr>
    <tr>
        <td style="width: 67px">
            <input type=hidden id=flon value='' style="width: 32px" /></td>
    </tr>
  <tr><td style="width: 67px">
      <input type=hidden id=tlat value='' style="width: 40px" /></td></tr>
    <tr>
      <tr>
      <td style="width: 67px">
          <input type=hidden id=tlon value='' style="width: 32px" /></td>
  </tr>
  <tr><td style="width: 67px">
      <input type=hidden id=rlat value='' style="width: 40px" /></td></tr>
    <tr>
    </tr>
      <tr>
      <td style="width: 67px">
          <input type=hidden id=rlon value='' style="width: 32px" /></td>
  </tr>

<tr>
        <td style="width: 67px">
            <input  type=hidden  id=tlocate runat="server" style="width: 40px" /></td>
            </tr>
    <tr>
        <td style="width: 67px">
            <input type=hidden id=theight runat="server" style="width: 40px" /></td>
    </tr>
    <tr>
        <td style="width: 67px">
            <input type=hidden id=distance value='' style="width: 40px" /></td>
    </tr>
    <tr>
        <td style="width: 67px">
            <input type= hidden id=bearingto value='' style="width: 40px" /></td>
    </tr>
</table>
</div>
    </form>
<script type="text/javascript">
        window.onload=initialize();
        function copydata(){
            var myobj=SelectObject("copyname");myobj.value=document.getElementById("cname").value;
            var myobj=SelectObject("copyanalat");myobj.value=document.getElementById("tlatu").value;
            var myobj=SelectObject("copyanalon");myobj.value=document.getElementById("tlonu").value;
            var myobj=SelectObject("copydiglat");myobj.value=document.getElementById("tlat").value;
            var myobj=SelectObject("copydiglon");myobj.value=document.getElementById("tlon").value;
            
        }
        
        function copydata_r(){
            var myobj=SelectObject("copyname_r");myobj.value=document.getElementById("rname").value;
            var myobj=SelectObject("copyanalat_r");myobj.value=document.getElementById("rlatu").value;
            var myobj=SelectObject("copyanalon_r");myobj.value=document.getElementById("rlonu").value;
            var myobj=SelectObject("copydiglat_r");myobj.value=document.getElementById("rlat").value;
            var myobj=SelectObject("copydiglon_r");myobj.value=document.getElementById("rlon").value;
            
        }
    //window.close=GUnload();
    </script>
</body>
</html>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EmbeddedPlotter.aspx.vb" Inherits="EmbeddedPlotter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Vlocity.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color:White">
    <form id="form1" runat="server">
    <div>
    
<script type="text/javascript" src="http://maps.google.com/maps?file=api&amp;v=3&amp;key=ABQIAAAAxN36sT_1e_mQReV2XujZjxSxDUeKyisjqtbiHmPVZHVcIQE3pBTkJmY8e6yk8NOJfMLjtlhIIa_qiQ"></script>

<script type="text/javascript" src="http://www.google.com/uds/jsapi?key=ABQIAAAAxN36sT_1e_mQReV2XujZjxSxDUeKyisjqtbiHmPVZHVcIQE3pBTkJmY8e6yk8NOJfMLjtlhIIa_qiQ"></script>

<script type="text/javascript" src="ShortScript.js"></script>
  <div   >

<table  align=center >
      <tr><td>
      
         <table border="0" cellpadding="1" cellspacing="0" width="100%">
             <caption style="height:25px;">Create a plot using the physical address or location. Drag the icon to the 
                 correct position.<br />Locations prefixed with <strong>Sentech</strong> require special pricing.
                 </caption>

            <tr><td colspan=3>
                <table width=100%>
                    <tr>
                        <td >Street Address:</td>
                        <td width=80% >
                                <input id=caddress value='' style="width: 100%" class="myinput" /></td>
                    </tr>
                </table>
             </td></tr>

             <tr><td colspan=3>
                <table width=100%>

                <tr><td width=180px>
                    Latitude:<br />
                    <input readonly id=tlatu value='' class="borderless" style="width: 90%" />
                    </td><td width=180px>Longitude:<br />
                        <input readonly id=tlonu value='' class="borderless" style="width: 90%" />
                </td>
                    <td >
                        Height 
                        over ground:<br />
                        <input id=cheight value='5' onblur='processPoints();' style="width: 16px" 
                            class="myinput" />
            
                        </td>
                        <td>
                            <input type=button onclick='processAddress();' value='Search' id="Button1" class="mybutton"/>
                        </td>
                </tr>

</table></td></tr>

<tr><td colspan=3>
    <table width=100%>

        <tr><td width=10%>Name it</td><td width=70% align="left" ><input id=cname 
                value='Point' onblur='processPoints();' style="width: 100%" class="myinput" /></td>
        <td id="plottit" align="center" colspan="3" style="height:20px"  >
                &nbsp;</td>
        
    </tr>
</table></td></tr>




    </table>
      
      </td></tr>
      
      <tr>
    <td valign=top >
    
      
    <div id="map_canvas" style="width: 800px; height: 600px" ></div>
    
       
    
    </td>
  </tr>
  <tr><td colspan=3 id="explainit">&nbsp;</td></tr>
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
      <td style="width: 67px">
          <input type=hidden id=tlon value='' style="width: 32px" /></td>
  </tr>
  <tr><td style="width: 67px">
      <input type=hidden id=rlat value='' style="width: 40px" /></td></tr>
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

<script type="text/javascript">
    window.onload = initialize();
    
    //window.close=GUnload();
    </script>

    </div>
    </form>
</body>
</html>

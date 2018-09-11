<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ShowPoint.aspx.vb" Inherits="ShowMap" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ShowMap</title>
</head>
<body onload="initialize();">
    <script type="text/javascript" src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAxN36sT_1e_mQReV2XujZjxSxDUeKyisjqtbiHmPVZHVcIQE3pBTkJmY8e6yk8NOJfMLjtlhIIa_qiQ"></script>
<script type="text/javascript" src="http://www.google.com/uds/api?file=uds.js&v=1.0&key=ABQIAAAAxN36sT_1e_mQReV2XujZjxSxDUeKyisjqtbiHmPVZHVcIQE3pBTkJmY8e6yk8NOJfMLjtlhIIa_qiQ"></script>
<script type="text/javascript" src=scripts/PointScript.js></script>

 <form id="form1" runat="server">
      <table border=1  style="width:100%; height:100%" >      
      <tr>
    <td align=center>
    
    
    <table border="0" cellpadding="1" cellspacing="0">
    <caption style="padding-left: 20px; color: #ff0000; height: 20px; background-color: #ffff66; text-align: center">
        <strong>Point Found</strong></caption>
    <tr>
            <td nowrap style="border-right: black thin solid; padding-right: 5px; border-top: black thin solid; padding-left: 5px; padding-bottom: 5px; margin: 5px; border-left: black thin solid; padding-top: 5px; border-bottom: black thin solid">  Name
                <input id=cname value='Customer' onchange=";copydata();" onblur='processPoints();copydata();' style="width: 128px" /></td>
            <td nowrap style="width: 8px">
                &nbsp;&nbsp;
            </td>
            <td align="left" nowrap style="border-right: black thin inset; padding-right: 5px; border-top: black thin inset; padding-left: 5px; padding-bottom: 5px; margin: 5px; border-left: black thin inset; padding-top: 5px; border-bottom: black thin inset">
                Lattitude&nbsp;
        <input readonly id=lattitude value='' style="width: 88px" />
                &nbsp;&nbsp;
                <input readonly id=latdec value='' style="width: 112px" /></td>
            <td nowrap>
                &nbsp; &nbsp;
            </td>
            <td nowrap style="border-right: black thin inset; padding-right: 5px; border-top: black thin inset; padding-left: 5px; padding-bottom: 5px; margin: 5px; border-left: black thin inset; padding-top: 5px; border-bottom: black thin inset">
                &nbsp;Longitude
                <input readonly id=longitude value='' style="width: 88px" />
                &nbsp;
                <input readonly id=londec value='' style="width: 128px" /></td>
        </tr>
    </table>
    </td>
    </tr>
    
    <tr>
        <td valign=top>  
    
    
        
    
    </td>

  </tr>
  </table>
  <div id="map_canvas" style="width: 95%; height:600px " ></div>
    </form>
</body>
</html>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Coverage.aspx.vb" Inherits="Coverage" StylesheetTheme="Plain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Wireless Coverage</title>
    
    <link href="http://code.google.com/apis/maps/documentation/javascript/examples/standard.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://www.google.com/jsapi"></script> 
<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>

    
    
    <script type="text/javascript" src=scripts/Coverage.js></script>
</head>
<body>
    <form id="form1" runat="server">
      <div style="position: absolute; top:5px; left:30px; z-index:100 "  >
              Region
                <asp:DropDownlist ID="RegionList" AutoPostBack=True runat="server" LoadingMessage="" MarkFirstMatch="True" NoWrap="True" PostBackUrl="~/Coverage.aspx" Sort="Ascending" Width="104px">
                </asp:DropDownlist>
              &nbsp; Tower &nbsp;<asp:DropDownlist ID="TowerList" AutoPostBack=True runat="server" DropDownWidth="250px" ExpandEffect="Pixelate"  LoadingMessage="" MarkFirstMatch="True" NoWrap="True" PostBackUrl="~/Coverage.aspx" Sort="Ascending" Width="200px">
                </asp:DropDownlist>
              &nbsp; &nbsp;<input type=button onclick='showmarkers();' value='show all towers' id="Button3" /><br />
              <span >Click on Marker to see information on the tower.&nbsp;</span>
              
          </div>

    <div  id="map_canvas" style="position: absolute; top:50px; left:15px; width: 98%; height: 90%" ></div>


<script type="text/javascript">
        initialize();
        
    </script>
    </form>
</body>
</html>

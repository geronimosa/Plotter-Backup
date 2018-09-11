<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Lookup.aspx.vb" Inherits="Lookup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Map Lookup</title>
    
    <script src='http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAxN36sT_1e_mQReV2XujZjxSxDUeKyisjqtbiHmPVZHVcIQE3pBTkJmY8e6yk8NOJfMLjtlhIIa_qiQ' type='text/javascript'></script>
    <script src="http://www.google.com/uds/api?file=uds.js&v=1.0&key=ABQIAAAAxN36sT_1e_mQReV2XujZjxSxDUeKyisjqtbiHmPVZHVcIQE3pBTkJmY8e6yk8NOJfMLjtlhIIa_qiQ" type="text/javascript"></script>

 <script type='text/javascript'>
    var map="";
    var npoint="";
    var mcount=0;
    var lastmarker='';
    var curPolyline;
    var m1;
    var m2;   
    var degreesPerRadian = 180.0 / Math.PI;
    var radiansPerDegree = Math.PI / 180.0;
    var geocoder = new GClientGeocoder();
    // Create our "tiny" marker icon
    var blueIcon = new GIcon(G_DEFAULT_ICON);
    blueIcon.image = "http://www.google.com/intl/en_us/mapfiles/ms/micons/blue-dot.png";
    var tinyIcon = new GIcon();
    tinyIcon.image = "http://google-maps-icons.googlecode.com/files/sun.png";
    //tinyIcon.shadow = "http://labs.google.com/ridefinder/images/mm_20_shadow.png";
    tinyIcon.iconSize = new GSize(30, 20);
    tinyIcon.shadowSize = new GSize(32, 20);
    tinyIcon.iconAnchor = new GPoint(16, 20);
    tinyIcon.infoWindowAnchor = new GPoint(15, 1);
    
    var baseIcon = new GIcon(G_DEFAULT_ICON);
    //baseIcon.image="http://www.iconspedia.com/uploads/1352269352.png"
    baseIcon.image="http://plotter.vlocity.co.za/images/marker_v.png"
    baseIcon.shadow = "http://labs.google.com/ridefinder/images/mm_20_shadow.png";
   // baseIcon.iconSize = new GSize(40, 44);
    //baseIcon.shadowSize = new GSize(42, 44);
   // baseIcon.iconAnchor = new GPoint(9, 34);
   // baseIcon.infoWindowAnchor = new GPoint(9, 2);


                
    


    function processAddress(){
        var address=document.getElementById("caddress").value;
        showAddress(address);
    }
    
    function showAddress(address) {
      geocoder.getLatLng(
        address,
        function(point) {
          if (!point) {
            alert(address + " not found");
          } else {
            map.setCenter(point, 13);
            //mcount++;
            if (lastmarker.value>1){
               map.removeOverlay(lastmarker);
            }
            var marker = createMarker(point,2,address,true);
            
            document.getElementById("cname").value=address;
            GEvent.addListener(marker, "dragend", processPoints);
            map.addOverlay(marker);
            npoint=document.getElementById('clocate');
            npoint.value=point;
            lastmarker=marker;
            m2=marker;
            processPoints();
            //marker.openInfoWindowHtml(address);
            
          }
        }
      );
}
    function bearing( from, to ) {
       // Convert to radians.
       var lat1 = from.latRadians();
       var lon1 = from.lngRadians();
       var lat2 = to.latRadians();
       var lon2 = to.lngRadians();

       // display coordinates
       document.getElementById("flat").value = from.lat();
       document.getElementById("flon").value = from.lng();
       document.getElementById("tlat").value = to.lat();
       document.getElementById("tlon").value = to.lng();
       document.getElementById("tlatu").value = ConvertMetric(to.lat(),"LAT");
       document.getElementById("tlonu").value = ConvertMetric(to.lng(),"LON");


       // Compute the angle.
       var angle = - Math.atan2( Math.sin( lon1 - lon2 ) * Math.cos( lat2 ), Math.cos( lat1 ) * Math.sin( lat2 ) - Math.sin( lat1 ) * Math.cos( lat2 ) * Math.cos( lon1 - lon2 ) );
       if ( angle < 0.0 )
	        angle  += Math.PI * 2.0;

       // And convert result to degrees.
       angle = angle * degreesPerRadian;
       angle = angle.toFixed(1);

       return angle;
     }
    
    function processPoints()
    {
	    var p1 = m1.getPoint();
	    var p2 = m2.getPoint();
	    var rawDistance = p1.distanceFrom(p2);
	    var kilometers = rawDistance/1000;
	    var miles = kilometers *  0.621371192;
	    var plotstring="";

	    document.getElementById("distance").value = Math.round(kilometers*100)/100;
	    document.getElementById("bearingto").value = bearing(p1,p2);
	    //plotstring="http://plotter.vlocity.co.za/DrawPlot.aspx";
	    plotstring="http://plotter.vlocity.co.za/Simple1.aspx";
	    plotstring+="?NameFrom="+document.getElementById("tlocate").value;
	    plotstring+="&NameTo="+document.getElementById("cname").value;
	    plotstring+="&lon1="+p1.lng();
	    plotstring+="&lat1="+p1.lat();
	    plotstring+="&lon2="+p2.lng();
	    plotstring+="&lat2="+p2.lat();
	    plotstring+="&ht1="+document.getElementById("theight").value;
	    plotstring+="&ht2="+document.getElementById("cheight").value;
	    plotstring+="&Reciprocal="+bearing(p2,p1);
	    plotstring+="&Bearing="+bearing(p1,p2);
	    plotstring+="&Freq=5.8";
	    
	    if (Math.round(kilometers*100)/100 < 120){
	        document.getElementById("plottit").innerHTML="<a href='"+plotstring+"' target='_blank'>View Topology between points</a>";
	    } else {
	        document.getElementById("plottit").innerHTML="<p>Too far for a plot</P>";
	    }
	    map.removeOverlay(curPolyline);
	    curPolyline = new GPolyline([p1, p2], "#FFFFFF", 10);
	    map.addOverlay(curPolyline);
    }

    function createMarker(point, number, thisName, drag) {
          var markerOptions ;
          if (number==1){
            markerOptions= { icon:baseIcon , draggable: drag };
          } else {
           markerOptions= { draggable: drag };
          }
          var marker = new GMarker(point,markerOptions);
          marker.value = number;
          GEvent.addListener(marker, 'click', function() {
                var myHtml = '<b>' + thisName + '</b>' ;
                if (!drag){map.openInfoWindowHtml(point, myHtml);}
                npoint=document.getElementById('clocate');
                npoint.value=point;
            });
         return marker;
    }
   
    function initialize() { 
         if (GBrowserIsCompatible()) {
              map = new GMap2(document.getElementById('map_canvas'),{draggableCursor:'url(fcur.cur),default'});
              map.setMapType(G_HYBRID_MAP);
              map.addControl( new GLargeMapControl() );
              map.addControl( new GMapTypeControl()) ;
              //map.addControl(new GScaleControl());
              map.addControl( new GOverviewMapControl(new GSize(100,100)) );
              map.setCenter(new GLatLng(Lat,Lon), 11);
              map.enableScrollWheelZoom();
              
              
              var point = new GLatLng(Lat,Lon);
              var html = '<div style="height:20px; width:200px; zindex:100">';
              html += thename;
              html += '</div>';
              mcount++;
              var marker = createMarker(point,mcount,thename,false);
              m1=marker;
              map.addOverlay(marker);
              GEvent.addListener(map,'click', function(overlay, latlng) {
              if (latlng) { 
                        if (lastmarker.value>1){
                            map.removeOverlay(lastmarker);
                        }
                        var marker = createMarker(latlng,2,"Selected Point",true);
                        GEvent.addListener(marker, "dragend", processPoints);
                        map.addOverlay(marker);
                        npoint=document.getElementById('clocate');
                        npoint.value=latlng;
                        lastmarker=marker;
                        m2=marker;
                        processPoints();
                        
                    }
              });
         }
     }
     
     function ConvertMetric(MetricTime,latlong){
        var StringValue = "";
        var LatOrLong = "";
        var Factor = 1;
        var deg=0;
        var min=0;
        var sec=0;
        var WorkWith=0;
        var Letter = "";
        if (latlong=="LON"){
            if (MetricTime*1 < 0){Letter = "W";} else { Letter = "E";}
        } else {
            if (MetricTime*1 < 0){Letter = "S";} else { Letter = "N";}
        }
        if (MetricTime*1 < 0){
            WorkWith = MetricTime*-1;
            Factor = -1;
        } else {
            WorkWith = MetricTime*1;
        }
        deg = Math.round(WorkWith-0.5);
        min = (WorkWith - deg) * 60;
        sec = Math.round((min - Math.round(min-0.5)) * 60*100)/100;
        min=Math.round(min-0.5);
        StringValue = deg + "°" + min + "'" + sec + '"' + Letter;
        return StringValue;
    }
    
    
   // deg = Math.round(WorkWith);
   //     min = Math.round((WorkWith - Math.round(WorkWith)) * 60);
   //     sec = Math.round((((WorkWith - Math.round(WorkWith)) * 60) - Math.round((WorkWith - Math.round(WorkWith)) * 60)) * 60*100)/100;
        
   //         deg = Int(WorkWith)
   //         min = (WorkWith - deg) * 60
   //         sec = (min - Int(min)) * 60
   //         StringValue = String.Format("{00:n0}", deg) & "°" & String.Format("{00:n0}", Int(min)) & "'" & String.Format("{0:n2}", Math.Round(sec, 2, MidpointRounding.ToEven)) & Chr(34) & Letter

     
     
</script>

</head>
<body onload="initialize()" onunload="GUnload()" >
<form id="form1" runat="server">
  <div align=left>
      <table style="position:absolute; left:auto; top:90px"><tr>
    <td valign=top>
    <div id="map_canvas" align=left style="width: 600px; height: 400px" runat=server></div>
    
    </td>
    <td valign=top>
    <table>
    <caption>Plotting from point : <% Response.Write(Request.QueryString("name"))%><br /></caption>
        <tr>
            <td>
                Change Tower</td>
            <td>
                <asp:DropDownList ID="TowerList" runat="server" AutoPostBack="True" Font-Size="Smaller"
                    Width="168px">
                </asp:DropDownList></td>
        </tr>
    <tr><td>Tower Location  </td><td><%=Request.QueryString("name")%><input  type=hidden  id=tlocate value='<%=request.querystring("name") %>' /></td></tr>
    <tr><td>Tower Height AGL</td><td><%=request.querystring("ht1") %><input type=hidden id=theight value='<%=request.querystring("ht1") %>' onblur='processPoints()' /></td></tr>
        <tr>
            <td align="center" colspan="2" style="height: 26px">
                Address Search</td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <input id=caddress value='' style="width: 280px" /><input type=button onclick='processAddress()' value='search' /></td>
        </tr>
    
    <tr><td colspan=2 align=center style="height: 27px">
        Or plot a &nbsp;Point on the map</td></tr>
    <tr><td>Client Location</td><td>Select on map<input type=hidden id=clocate value='Select on map' /></td></tr>
    <tr><td>Client Name</td><td><input id=cname value='Point' onblur='processPoints()' /></td></tr>
    <tr><td>Client Height AGL</td><td><input id=cheight value='5' onblur='processPoints()' /></td></tr>
    <tr><td>Latitude</td><td><input readonly id=tlatu value='' /><input type=hidden id=tlat value='' /></td></tr>
    <tr><td>Longitude</td><td><input readonly id=tlonu value='' /><input type=hidden id=tlon value='' /></td></tr>
    <tr><td colspan=2 align=center>Stats.</td></tr>
    <tr><td>Distance</td><td><input readonly id=distance value='' />km's</td></tr>
    <tr><td>Bearing</td><td><input readonly id=bearingto value='' />degrees</td></tr>
        
    <tr><td></td><td><input type=hidden id=flat value='' /></td></tr>
    <tr><td></td><td><input type=hidden id=flon value='' /></td></tr>
    <tr><td colspan=2 id=plottit align=center>
        
    </td></tr>
    </table>
    </td>
  </tr>
  <tr><td colspan=2 align=center></td></tr>
  </table>
</div>
</form>
</body>
</html>

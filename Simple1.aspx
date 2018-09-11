<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Simple1.aspx.vb" Inherits="Simple1" %>
<%@ Register Src="googlegraph.ascx" TagName="googlegraph" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta name="viewport" content="initial-scale=1.0, user-scalable=no"/>
<meta http-equiv="content-type" content="text/html; charset=UTF-8"/>
<title>Point to Point Elevation</title>
<link href="http://code.google.com/apis/maps/documentation/javascript/examples/standard.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://www.google.com/jsapi"></script> 
<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>




<script type="text/javascript">
//NameFrom=Benoni Tower&NameTo=Customer&lon1=28.3116666666667&lat1=-26.1945&lon2=28.370819091796875&lat2=-26.157286956136396&Reciprocal=55.0&Bearing=235.0&ht1=30&ht2=5&Freq=5.8

  var elevator;
  var map;
  var chart;
  var infowindow = new google.maps.InfoWindow();
  var polyline;
  var distance;
  var linecolors=['red','orange','yellow','green','blue','indigo','violet','white','black','grey'];
  var colorcode=['ff0000','ff8000','ffff00','01DF01','0101DF','8A0886','FA58F4','ffffff','000000','848484'];
  var data;
  var towernames="";  
  var towerheight=0;
  var clientheight=0;
  var myactivediv="elevation_chart";
  google.load("visualization", "1", {packages: ["columnchart"]});

  function initialize() {
    var myOptions = {
      zoom: 12,
      center: posend,
      mapTypeId: 'hybrid'
    }
    map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);

    // Create an ElevationService.
    elevator = new google.maps.ElevationService();

    // Draw the path, using the Visualization API and the Elevation service.
    drawPath(posstart,posend,'elevation_chart',1,towerheight);
  }
function toRad(deg) {
     return deg * Math.PI/180;
}

function drawPath(mystart,myend,mydiv,chnum,theight) {
     // Calc Distance;
     document.getElementById('mystatus').innerHTML='<p>Calculating terrain to '+towernames+' and plotting targets.</p>';
     var R = 6371;
     var dLat = toRad(myend.lat()-mystart.lat());
     var dLon = toRad(myend.lng()-mystart.lng());
     var dLat1 = toRad(mystart.lat());
     var dLat2 = toRad(myend.lat());
     var a = Math.sin(dLat/2) * Math.sin(dLat/2) +
             Math.cos(dLat1) * Math.cos(dLat1) *
             Math.sin(dLon/2) * Math.sin(dLon/2);
     var c = 2 * Math.atan2( Math.sqrt(a), Math.sqrt(1-a));
     var d = R * c;
     distance=d;
     // end Calc Distance;
     var path = [mystart,myend];
     thisdiv=mydiv;
     thisheight=theight;
     samples=Math.round(((distance*1)*1000)/30);if(samples<=254){samples=254;}
     var pathRequest = {
         'path': path,
         'samples': 254
     }
     elevator.getElevationAlongPath(pathRequest, plotElevation);
}
 
function plotElevation(results, status) {
   if (status == google.maps.ElevationStatus.OK) {
     elevations = results;
     var elevationPath = [];
     for (var i = 0; i < results.length; i++) {
         elevationPath.push(elevations[i].location);
     }
     var pathOptions = {
         path: elevationPath,
         strokeColor:  linecolors[1],
         opacity: 0.4,
         weight: 1,
         map: map 
     }
     polyline = new google.maps.Polyline(pathOptions);
     var startline;var endline;
     var options={};
         options.width=800;
         options.height=200;
         options.legend='none';options.curveType='function';
         options.titleY='Elevation (m)';
         options.titleX='Distance ' + Math.round((distance*1)*100)/100 + ' km  '+samples.toString()+' height plots taken (every 30 Meters)';
         options.title=' '+towernames+' height '+thisheight+' m';
         options.pointSize=1;
         options.lineWidth=3;
         var mcolors = ['green','blue','brown','yellow','yellow'];
         options.colors = mcolors;
     data = new google.visualization.DataTable();
     data.addColumn('string', 'Sample');
     data.addColumn('number', 'Elevation');
     data.addColumn('number', 'Line');
     data.addColumn('number', 'Curve');
     data.addColumn('number', 'FREZ1');
     data.addColumn('number', 'FREZ2');
     var MaxCurve = ((distance * distance) / 68032) * 1000;
     var CurveFactor = MaxCurve / results.length;
     var MidPoint = results.length / 2;
     var xPOint = results.length;
     var yPoint=0;
     var CurveFraction=0;
     var CurvePerc=0;
     var MinValue = elevations[0].elevation ;
 // determine Min Value
     for (var i = 0; i < elevations.length; i++){
         if (elevations[i].elevation < MinValue){MinValue = elevations[i].elevation;}
     }
     for (var i = 0; i < results.length; i++) {
       // Earths Curve
         if (i <= MidPoint) {
             CurvePerc = (100 - (i / results.length) * 100) * 2;
             CurveFraction += CurveFactor;
             yPoint = (((xPOint) / 1) * CurveFraction) / (MidPoint);
             xPOint--;
         } else {
             CurvePerc = ((i / results.length) * 100) * 2;
             CurveFraction -= CurveFactor;
             yPoint = (((xPOint) / 1) * CurveFraction) / (MidPoint);
             xPOint++;
         }
       // End Earths Curve
             if (i==0){
                 data.addRow(['', elevations[i].elevation+towerheight+yPoint , elevations[i].elevation+towerheight+yPoint,MinValue + yPoint,elevations[i].elevation+towerheight+yPoint+6,elevations[i].elevation+towerheight+yPoint-6]);
             } else {
                 if (i==results.length-1){
                     data.addRow(['', elevations[i].elevation+clientheight+yPoint, elevations[i].elevation+clientheight+yPoint,MinValue + yPoint,elevations[i].elevation+clientheight+yPoint+6,elevations[i].elevation+clientheight+yPoint-6]);
                 } else {
                     data.addRow(['', elevations[i].elevation+yPoint,undefined,MinValue + yPoint,undefined,undefined]);
                 }
             }
     }
     //document.getElementById(thisdiv).style.display = 'block';
     //document.getElementById(thisdiv).style.border = 'solid 4px '+linecolors[1];
     chart = new google.visualization.LineChart(document.getElementById(myactivediv));
     chart.draw(data, options)
     document.getElementById('mystatus').innerHTML='<p>Plot completed to '+towernames+'.</p>';
      
    }else{
         if (status='OVER_QUERY_LIMIT'){document.getElementById(myactivediv).innerHTML='<p>Google numbers the number of queries per hour.</P><p> We have exceeded the limitation.</P><p> Please try again later</p>';}
         else{document.getElementById(myactivediv).innerHTML='<p>A Google error has occured : '+status+'</p>';
         }
         document.getElementById('mystatus').innerHTML='<p>Plot rejected by Google for '+towernames+ '.</p>';
    }
   
}


function new_drawPath(fromlat,fromlong,fromname,fromheight,tolat,tolong,toname,toheight,activediv) {
        var mystart=new google.maps.LatLng(fromlat,fromlong);
        var myend=new google.maps.LatLng(tolat,tolong); 
        myactivediv=activediv;       
        towername=fromname;
        towerheight=fromheight*1;
        drawPath(mystart,myend,activediv,1,fromheight);
        clientheight=toheight*1;
}
 

</script>
</head>
<body style="margin:5px; padding:5px;" >
<div align=center>
<form id=startup runat="server">
<div>Under Construction - Original Source of Plotting info no longer available to us, so we are redeveloping this tool.</div>
  <div align=center id="mystatus" style="width:100%;height:20px;color:yellow;"></div>
  <div align=center id="map_canvas" style="width: 800px; height: 450px; border: 1px solid black;"></div>
  <div align=center id="elevation_chart" style="width:800px; height:200px; "></div>
  </form>
</div>
</body>

</html>

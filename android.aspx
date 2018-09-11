<%@ Page Language="VB" AutoEventWireup="false" CodeFile="android.aspx.vb" Inherits="android" %>
<%@ Register Src="googlegraph.ascx" TagName="googlegraph" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
  
  var Elevation=[];
  var Line=[];
  var Curve=[];
  var FREZ1=[];
  var FREZ2=[];

  function initialize() {
    
    // Create an ElevationService.
    elevator = new google.maps.ElevationService();

    // Draw the path, using the Visualization API and the Elevation service.
    drawPath(posstart, posend, 'elevation_chart', 1, towerheight);
   // document.getElementById('description').innerHTML = "<p>Bearing: "&sentbearing&"</p>";

  }
function toRad(deg) {
     return deg * Math.PI/180;
}

function drawPath(mystart,myend,mydiv,chnum,theight) {
     // Calc Distance;
    // document.getElementById('mystatus').innerHTML='<p>Calculating terrain to '+towernames+' and plotting targets.</p>';
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
     var startline;var endline;
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
              Elevation[i]=elevations[i].elevation+towerheight+yPoint;
              Line[i]=elevations[i].elevation+towerheight+yPoint;
              Curve[i]=MinValue + yPoint;
              FREZ1[i]=elevations[i].elevation+towerheight+yPoint+6;
              FREZ2[i]=elevations[i].elevation+towerheight+yPoint-6;
             } else {
                 if (i==results.length-1){
                      Elevation[i]=elevations[i].elevation+clientheight+yPoint;
                      Line[i]=elevations[i].elevation+clientheight+yPoint;
                      Curve[i]=MinValue + yPoint;
                      FREZ1[i]=elevations[i].elevation+clientheight+yPoint+6;
                      FREZ2[i]=elevations[i].elevation+clientheight+yPoint-6;
                 } else {
                      Elevation[i]=elevations[i].elevation+yPoint;
                      Line[i]=undefined;
                      Curve[i]=MinValue + yPoint;
                      FREZ1[i]=null;
                      FREZ2[i]=null;
                 }
             }
     }
    //  draw graph
    var g = new Bluff.Line('plotting', '520x300');
    g.title = "<--"+towernames;
    g.tooltips = true;
    g.dot_radius=1;
    g.no_data_message="Invalid Plot";
    g.x_axis_label=" "+Math.round(distance*100)/100+" Km's";
    g.y_axis_label="height";
    g.hide_line_markers=false;
    g.line_width=1;
    g.top_margin=5;
    g.left_margin=5;
    g.bottom_margin=5;
    g.right_margin=5;
    g.title_font_size=18;
    g.legend_font_size=18;
    g.marker_font_size=18;

   // g.theme_37signals();
    g.set_theme({
    colors: ['green', 'red', 'brown', 'yellow',
             'orange'],
    marker_color: 'Wheat',
    font_color: 'black',
    background_colors: ['white', 'white']
  });
    g.data("Elevation", Elevation);
    g.data("Signal", Line);
    g.data("Earth", Curve);
    g.data("Upper", FREZ1);
    g.data("Lower", FREZ2);
    g.draw();
    }else{
         if (status='OVER_QUERY_LIMIT'){document.getElementById(myactivediv).innerHTML='<p>Google numbers the number of queries per hour.</P><p> We have exceeded the limitation.</P><p> Please try again later</p>';}
         else{document.getElementById(myactivediv).innerHTML='<p>A Google error has occured : '+status+'</p>';
         }
         //document.getElementById('mystatus').innerHTML='<p>Plot rejected by Google for '+towernames+ '.</p>';
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
<body   style="margin:0px; padding:0px; text-align:center" >
<script language="javascript" src="/scripts/js-class.js" type="text/javascript"></script>
<script language="javascript" src="/scripts/bluff-min.js" type="text/javascript"></script>
<!--[if IE]><script language="javascript" src="/scripts/excanvas.js" type="text/javascript"></script><![endif]-->

<form id=startup runat="server">

<div style="background-color:white;width:100%;height:500px" >
<center><canvas id="plotting" width="320" height="500"></canvas>
<div id="description"> </div></center>
</div>
  </form>

</body>

</html>
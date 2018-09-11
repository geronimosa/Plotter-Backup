<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Points.aspx.vb" Inherits="Points" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>


<script language="javascript" type="text/javascript"" src="https://www.google.com/jsapi"></script>
<script language="javascript" type="text/javascript"" src="https://maps.googleapis.com/maps/api/js?sensor=false&region=ZA"></script>
        
<script language='javascript'>

    var elevator;
    var map;
    var chart;
    var infowindow = new google.maps.InfoWindow();
    var polyline;
    // Load the Visualization API and the columnchart package.
    google.load("visualization", "1", { packages: ["columnchart"] });
    function detectBrowser() {
        var useragent = navigator.userAgent;
        var mapdiv = document.getElementById("map_canvas");

        if (useragent.indexOf('iPhone') != -1 || useragent.indexOf('Android') != -1) {
            mapdiv.style.width = '100%';
            mapdiv.style.height = '300px';
        } else {
            mapdiv.style.width = '600px';
            mapdiv.style.height = '800px';
        }
    }
    function setTextContent(element, text) {
       if (element.textContent)
            element.removeChild(element.firstChild);
        element.appendChild(document.createTextNode(text));
    }
    function initialize() {
        detectBrowser();
        if (mapactive == true) {
            var mapOptions = {
                zoom: 12,
                center: pointa,
                mapTypeId: 'satellite'
            }
            document.getElementById("map_canvas").style.display = "block";
            map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
        } else {
        document.getElementById("map_canvas").style.display = "none";
        document.getElementById("imager").style.display = "block";
        document.getElementById("imager").style.position = "absolute";
        document.getElementById("imager").style.top = "1";
        document.getElementById("imager").style.left = "1";
        
        
        
        }
        
        // Create an ElevationService.
        elevator = new google.maps.ElevationService();

        // Draw the path, using the Visualization API and the Elevation service.
        drawPath();
    }

    function drawPath() {

        // Create a new chart in the elevation_chart DIV.
        chart = new google.visualization.ColumnChart(document.getElementById('elevation_chart'));

        var path = [pointa,pointb];

        // Create a PathElevationRequest object using this array.
        // Ask for 256 samples along that path.
        var pathRequest = {
            'path': path,
            'samples': 256
        }

        // Initiate the path request.
        elevator.getElevationAlongPath(pathRequest, plotElevation);
    }

    // Takes an array of ElevationResult objects, draws the path on the map
    // and plots the elevation profile on a Visualization API ColumnChart.
    function plotElevation(results, status) {
        if (status == google.maps.ElevationStatus.OK) {
            elevations = results;

            // Extract the elevation samples from the returned results
            // and store them in an array of LatLngs.
            var elevationPath = [];
            for (var i = 0; i < results.length; i++) {
                elevationPath.push(elevations[i].location);
            }

            // Display a polyline of the elevation path.
            if (mapactive == true) {
                var pathOptions = {
                    path: elevationPath,
                    strokeColor: '#0000CC',
                    opacity: 0.4,
                    map: map
                }
                polyline = new google.maps.Polyline(pathOptions);
            }
            // Extract the data from which to populate the chart.
            // Because the samples are equidistant, the 'Sample'
            // column here does double duty as distance along the
            // X axis.
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Sample');
            data.addColumn('number', 'Elevation');
            
            for (var i = 0; i < results.length; i++) {
                data.addRow(['', elevations[i].elevation]);
            }
            var elev=new Array();
            for (var i = 0; i < results.length; i++) {
                elev.push(elevations[i].elevation);
            }
            elev[0] = elev[0] + clientheight;
            elev[elev.length - 1] = elev[elev.length - 1]+towerheight
            //setTextContent(document.getElementById("points"), elev);
            // Draw the chart using the data within its DIV.
            //document.getElementById('elevation_chart').style.display = 'block';
           // chart.draw(data, {
           //     width: 640,
           //     height: 200,
           //     legend: 'none',
           //     titleY: 'Elevation (m)'
           // });
            document.getElementById("imager").src = "http://plotter.vlocity.co.za/TestMASS.aspx?distance=" + distance + "&points=" + elev;
            
        }
    }
</script>

<body >
    <form id="form1" runat="server">
<div>
  <div id="map_canvas" style="position:relative;width:640px; height:400px; border: 1px solid black;"></div>
  
  <div id="iframearea" style="width:450; height:250px; ">
  <iframe id='imager' width=100% height=100% src=''></iframe>
  
  </div>
  <div id="elevation_chart" style="width:0px; height:0px; "></div>
</div>
    </form>
</body>
</html>

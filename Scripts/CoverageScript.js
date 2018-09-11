// JScript File

//	<script src='http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAxN36sT_1e_mQReV2XujZjxSxDUeKyisjqtbiHmPVZHVcIQE3pBTkJmY8e6yk8NOJfMLjtlhIIa_qiQ' type='text/javascript'></script>

//    <script src="http://www.google.com/uds/api?file=uds.js&v=1.0&key=ABQIAAAAxN36sT_1e_mQReV2XujZjxSxDUeKyisjqtbiHmPVZHVcIQE3pBTkJmY8e6yk8NOJfMLjtlhIIa_qiQ" type="text/javascript"></script>



    

    var match=false; 

    var map="";

    var npoint="";

    var mcount=0;

    var lastmarker='';

    var lastmarker_r='';

    

    var curPolyline;

    var m1=null;

    var m2=null;        

    var m3=null;   



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

    baseIcon.image="http://plotter.vlocity.co.za/images/red_MarkerV.png"

    baseIcon.shadow = "http://labs.google.com/ridefinder/images/mm_20_shadow.png";



    var baseIcon_a = new GIcon(G_DEFAULT_ICON);

    baseIcon_a.image="http://plotter.vlocity.co.za/images/red_MarkerA.png"

    baseIcon_a.shadow = "http://labs.google.com/ridefinder/images/mm_20_shadow.png";



    var baseIcon_b = new GIcon(G_DEFAULT_ICON);

    baseIcon_b.image="http://plotter.vlocity.co.za/images/red_MarkerB.png"

    baseIcon_b.shadow = "http://labs.google.com/ridefinder/images/mm_20_shadow.png";



    var baseIcon_c = new GIcon(G_DEFAULT_ICON);

    baseIcon_c.image="http://plotter.vlocity.co.za/images/red_MarkerC.png"

    baseIcon_c.shadow = "http://labs.google.com/ridefinder/images/mm_20_shadow.png";



    function make_icon(setcolor)

    {

        var myIcon = new GIcon();

        myIcon.image = 'http://www.vlocity.co.za/images/gapi/wave-' + setcolor + '/image.png';

        myIcon.shadow = 'http://www.vlocity.co.za/images/gapi/wave-' + setcolor + '/shadow.png';

        myIcon.iconSize = new GSize(34,33);

        myIcon.shadowSize = new GSize(51,33);

        myIcon.iconAnchor = new GPoint(17,33);

        myIcon.infoWindowAnchor = new GPoint(17,0);

        myIcon.printImage = 'http://www.vlocity.co.za/images/gapi/wave-' + setcolor + '/printImage.gif';

        myIcon.mozPrintImage = 'http://www.vlocity.co.za/images/gapi/wave-' + setcolor + '/mozPrintImage.gif';

        myIcon.printShadow = 'http://www.vlocity.co.za/images/gapi/wave-' + setcolor + '/printShadow.gif';

        myIcon.transparent = 'http://www.vlocity.co.za/images/gapi/wave-' + setcolor + '/transparent.png';

        myIcon.imageMap = [33,0,33,1,33,2,32,3,32,4,31,5,31,6,30,7,29,8,29,9,28,10,28,11,27,12,27,13,26,14,25,15,25,16,24,17,24,18,23,19,22,20,22,21,21,22,21,23,20,24,20,25,20,26,20,27,20,28,20,29,20,30,19,31,19,32,16,32,16,31,16,30,15,29,15,28,15,27,16,26,16,25,15,24,15,23,14,22,13,21,13,20,12,19,12,18,11,17,10,16,10,15,9,14,9,13,8,12,7,11,7,10,6,9,6,8,5,7,4,6,4,5,3,4,3,3,2,2,1,1,1,0];

        return myIcon;



    }



    function IncludeJavaScript(jsFile)

    {

      document.write('<script type="text/javascript" src="'

        + jsFile + '"></scr' + 'ipt>'); 

    }



    function processAddress(){

        var address=document.getElementById("caddress").value;

        showAddress(address);

    }

    

    function processAddress_r(){

        var address=document.getElementById("raddress").value;

        showAddress_r(address);

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

            var marker = createMarkerB(point,2,address,true);

            document.getElementById("cname").value='Plotted Point';

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



    function showAddress_r(address) {

      geocoder.getLatLng(

        address,

        function(point) {

          if (!point) {

            alert(address + " not found");

          } else {

            //map.setCenter(point, 13);

            //mcount++;

            if (lastmarker_r.value>1){

               map.removeOverlay(lastmarker_r);

            }

            var marker = createMarkerC(point,2,address,true);

            document.getElementById("rname").value='Plot Point';

            GEvent.addListener(marker, "dragend", processPoints);

            map.addOverlay(marker);

            npoint=document.getElementById('rlocate');

            npoint.value=point;

            lastmarker_r=marker;

            m3=marker;

            processPoints();

            //marker.openInfoWindowHtml(address);

            

          }

        }

      );

}



function showcordinate(point,textbox1,textbox2,textbox3,textbox4){

       var lat1 = point.latRadians();

       var lon1 = point.lngRadians();

       document.getElementById(textbox1).value = point.lat();

       document.getElementById(textbox2).value = point.lng();

       document.getElementById(textbox3).value = ConvertMetric(point.lat(),"LAT");

       document.getElementById(textbox4).value = ConvertMetric(point.lng(),"LON");

}



function displaycordinates(from, to, relay){

        

//        if (typeof from != "undefined"){}

        if (typeof to != "undefined"){showcordinate(to,"tlat","tlon","tlatu","tlonu");}

        if (typeof relay != "undefined"){showcordinate(relay,"rlat","rlon","rlatu","rlonu");}else{

            document.getElementById("rlatu").value="";document.getElementById("rlonu").value="";

            document.getElementById("rlat").value="";document.getElementById("rlon").value="";

        }

        

       var lat1 = from.latRadians();

       var lon1 = from.lngRadians();



}





    function bearing( p1,p2 ) {

       // Convert to radians.

       var lat1=0;

       var lon1=0;

       var lat2=0;

       var lon2=0;

       

       if (typeof m1 != "undefined"){if (m1 != null){

           lat1 = p1.latRadians();

           lon1 = p1.lngRadians();

       }}

       

       if (typeof m1 != "undefined"){if (m1 != null){

           lat2 = p2.latRadians();

           lon2 = p2.lngRadians();

       }}

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

        

        var explained="";

        var relayactive=false;

	    var p1 = m1.getPoint();

	    var dst_towertoclient=0;

	    var dst_towertorelay=0;

	    var dst_relaytoclient=0;

        explained+="From tower at Point A <br>";

	    if (typeof m3 != "undefined"){if (m3 != null){var p3 = m3.getPoint();explained+="Via realy at Point C <br>";relayactive=true;}}

	    if (typeof m2 != "undefined"){if (m2 != null){var p2 = m2.getPoint();explained+="To the endpoint at Point B <br>";}}

	    

	    if (typeof m3 != "undefined"){if (m3 != null) {

	        dst_towertorelay=p1.distanceFrom(p3);

	        if (typeof m2 != "undefined"){if (m2 != null){dst_relaytoclient=p2.distanceFrom(p3);}}

	    }}

	    if (typeof m2 != "undefined"){if (m2 != null) {

    	    dst_towertoclient=p1.distanceFrom(p2);

	    }}    	    

	    var rawDistance=0;

	    if (relayactive){	        

	        rawDistance=dst_relaytoclient+dst_towertorelay;	        

	    }else{

	        rawDistance=dst_towertoclient;	    

	    }

	    var kilometers = rawDistance/1000;

	    var miles = kilometers *  0.621371192;

	    explained+="";

	    if (relayactive){	        

            explained+="";


        }else{

        explained += "";         

        }



	    var plotstring1="";

	    var plotstring2="";

	    var plotstring3="";
	    var PlotTen1 = "";
	    var PlotTen1a = "";
	    var PlotTen2 = "";
	    var PlotTen2a = "";



	    document.getElementById("distance").value = Math.round(kilometers*100)/100;

	    document.getElementById("bearingto").value = bearing(p1,p2);

	    

	    //+document.getElementById("ctl00_Main_theight").value;

	    var TowerName=document.getElementById("ctl00_Main_tlocate").value;

        var TowerHeight=document.getElementById("ctl00_Main_theight").value;

        

	    var RelayName=document.getElementById("rname").value;

	    var RelayHeight=document.getElementById("rheight").value;

	    

	    var ClientName=document.getElementById("cname").value;

	    var ClientHeight=document.getElementById("cheight").value;

	    

	    if (typeof m3 != "undefined"){if (m3 != null) {

	        plotstring1="http://plotter.vlocity.co.za/Simple1.aspx";

	        plotstring1+="?NameFrom="+TowerName;

	        plotstring1+="&NameTo="+RelayName;



	        plotstring1+="&lon1="+p1.lng();

	        plotstring1+="&lat1="+p1.lat();

            plotstring1+="&lon2="+p3.lng();

            plotstring1+="&lat2="+p3.lat();

            plotstring1+="&Reciprocal="+bearing(p3,p1);

            plotstring1+="&Bearing="+bearing(p1,p3);

            plotstring1+="&ht1="+TowerHeight;

	        plotstring1+="&ht2="+RelayHeight;

	        plotstring1+="&Freq=5.8";
	        PlotTen2 = "Simple2.aspx?dec_lat=" + p3.lat() + "&dec_long=" + p3.lng() + "&namefrom=" + RelayName + "&towerheight=" + RelayHeight + "&frequency=5.8"
	        PlotTen2a = "TestMASS.aspx?dec_lat=" + p3.lat() + "&dec_long=" + p3.lng() + "&namefrom=" + RelayName + "&towerheight=" + RelayHeight + "&frequency=5.8"
	        

	        if (typeof m2 != "undefined"){if (m2 != null) {

                plotstring2="http://plotter.vlocity.co.za/Simple1.aspx";

                plotstring2+="?NameFrom="+RelayName;

                plotstring2+="&NameTo="+ClientName;

                plotstring2+="&lon1="+p3.lng();

                plotstring2+="&lat1="+p3.lat();

                plotstring2+="&lon2="+p2.lng();

                plotstring2+="&lat2="+p2.lat();

                plotstring2+="&Reciprocal="+bearing(p3,p2);

                plotstring2+="&Bearing="+bearing(p2,p3);

                plotstring2+="&ht1="+RelayHeight;

                plotstring2+="&ht2="+ClientHeight;

                plotstring2+="&Freq=5.8";
                PlotTen1 = "Simple2.aspx?dec_lat=" + p2.lat() + "&dec_long=" + p2.lng() + "&namefrom=" + ClientName + "&towerheight=" + ClientHeight + "&frequency=5.8"
                PlotTen1a = "TestMASS.aspx?dec_lat=" + p2.lat() + "&dec_long=" + p2.lng() + "&namefrom=" + ClientName + "&towerheight=" + ClientHeight + "&frequency=5.8"

            }}

        }} 	    

        if (typeof m2 != "undefined"){if (m2 != null) {

            plotstring3="http://plotter.vlocity.co.za/Simple1.aspx";

            plotstring3+="?NameFrom="+TowerName;

            plotstring3+="&NameTo="+document.getElementById("cname").value;

            plotstring3+="&lon1="+p1.lng();

            plotstring3+="&lat1="+p1.lat();

            plotstring3+="&lon2="+p2.lng();

            plotstring3+="&lat2="+p2.lat();

            plotstring3+="&Reciprocal="+bearing(p1,p2);

            plotstring3+="&Bearing="+bearing(p2,p1);

            plotstring3+="&ht1="+TowerHeight;

            plotstring3+="&ht2="+ClientHeight;

            plotstring3+="&Freq=5.8";
            PlotTen1 = "Simple2.aspx?dec_lat=" + p2.lat() + "&dec_long=" + p2.lng() + "&namefrom=" + ClientName + "&towerheight=" + ClientHeight + "&frequency=5.8"
            PlotTen1a = "TestMASS.aspx?dec_lat=" + p2.lat() + "&dec_long=" + p2.lng() + "&namefrom=" + ClientName + "&towerheight=" + ClientHeight + "&frequency=5.8"

        }}



	    displaycordinates(p1,p2,p3);



	    copydata();

        document.getElementById("explainit").innerHTML="<div>"+explained+"</div>";

	    var displayHL="";

	    

	    if (Math.round(kilometers*100)/100 < 120){

	        if (plotstring1!=""){displayHL+="<li><a href='"+plotstring1+"' target='Tower_Relay'>View Tower >> Relay</a></li>";}

	        if (plotstring2!=""){displayHL+="<li><a href='"+plotstring2+"' target='Relay_Client'>View Relay >> Client</a></li>";}

	        if (plotstring3!=""){

	            displayHL+="<li><a href='"+plotstring3+"' target='Client_Tower'>View Direct Client >> Tower</a></li>";	   

	            }

	    } else {

	        displayHL+="<li>Too far for a plot</li>";

	    }
	    displayHL += "<li><a href='" + PlotTen1 + "' target='Near10_Client'>View Nearest 10 - Google >> Client</a></li>";
	  //  displayHL += "<li><a href='" + PlotTen1a + "' target='Near10_Client'>View Nearest 10 - Deserthail >> Client</a></li>";

	    if (typeof m3 != "undefined"){if (m3 != null) {
	        displayHL += "<li><a href='" + PlotTen2 + "' target='Near10_Relay'>View Nearest 10 >> Relay - Google</a></li>";
	  //      displayHL += "<li><a href='" + PlotTen2a + "' target='Near10_Relay'>View Nearest 10 >> Relay - Deserthail</a></li>";

        }}            

	    document.getElementById("plottit").innerHTML="<ul>"+displayHL+"</ul>";	    

    }





    function createMarkerT(point, number, thisName, drag) {

          var markerOptions ;

          markerOptions= { icon:baseIcon_a , draggable: drag };

          var marker = new GMarker(point,markerOptions);

          marker.value = number;

          lastmarker=marker;

          m1=marker;

          processtower();

          GEvent.addListener(map,'click', function(overlay, latlng) {

          if (latlng){

            map.removeOverlay(lastmarker);

            var marker = createMarkerT(latlng,1,"Selected Point",true);

            GEvent.addListener(marker, 'dragend',function(){

                processtower();

            });

            map.addOverlay(marker);

            lastmarker=marker;

            m1=marker;

            }

          });

          return marker;

    }



    function processtower(){

        var p1 = m1.getPoint();

        document.getElementById('rlocate').value="Changed";

        document.getElementById('ctl00_Main_rlon').value=p1.lng();

        document.getElementById('ctl00_Main_rlat').value=p1.lat();

        document.getElementById('ctl00_Main_rlonu').value=ConvertMetric(p1.lng(),"LON");

        document.getElementById('ctl00_Main_rlatu').value=ConvertMetric(p1.lat(),"LAT");

        

    }

    function createMarkerA(point, number, thisName, drag) {

        var letter = "A";

        var letteredIcon = new GIcon(baseIcon);

        letteredIcon.image = "http://www.google.com/mapfiles/marker" + letter + ".png";

        

        var tinyIcon = new GIcon();

        tinyIcon.image = "http://google-maps-icons.googlecode.com/files/sun.png";

        tinyIcon.iconSize = new GSize(30, 20);

        tinyIcon.shadowSize = new GSize(32, 20);

        tinyIcon.iconAnchor = new GPoint(16, 20);

        tinyIcon.infoWindowAnchor = new GPoint(15, 1);

    

        

        

        var markerOptions ;

        markerOptions={};

        markerOptions.icon=tinyIcon;

        markerOptions.draggable=false;        

        var marker = new GMarker(point,markerOptions);

        marker.value = number;

        GEvent.addListener(marker, 'click', function() {

            marker.openInfoWindowHtml('<span style=\"color: #0077ff;\">' + thisName + '</span>');

        });

         return marker;

    }



    function createMarkerB(point, number, thisName, drag) {

        var letter = "B";

        var letteredIcon = new GIcon(baseIcon);

        letteredIcon.image = "http://www.google.com/mapfiles/marker" + letter + ".png";

        var markerOptions ;

        markerOptions={};

        markerOptions.icon=letteredIcon;

        markerOptions.draggable=true;        

          var marker = new GMarker(point,markerOptions);

          marker.value = number;

          GEvent.addListener(marker, 'click', function() {

                marker.openInfoWindowHtml('<span style=\"color: #0077ff;\"> Customer:<b>' + thisName + '</b></span>');

            });

          GEvent.addListener(marker, 'moveend', function() {

                npoint=document.getElementById('clocate');

                npoint.value=point;

            });

         return marker;

    }



    function createMarkerC(point, number, thisName, drag) {

        var letter = "C";

        var letteredIcon = new GIcon(baseIcon);

        letteredIcon.image = "http://www.google.com/mapfiles/marker" + letter + ".png";

        var markerOptions ;

        markerOptions={};

        markerOptions.icon=letteredIcon;

        markerOptions.draggable=true;        

        var marker = new GMarker(point,markerOptions);

        marker.value = number;

        GEvent.addListener(marker, 'click', function() {

            marker.openInfoWindowHtml('<span style=\"color: #0077ff;\"> Relay:<b>' + thisName + '</b></span>');

        });

        GEvent.addListener(marker, 'moveend', function() {

            npoint=document.getElementById('rlocate');

            npoint.value=point;

        });



        return marker;

    }

    

    function createMarkerN(point, number, thisName, drag, letter,thecolor) {

        var letteredIcon = new GIcon(baseIcon);

        //letteredIcon.image = "http://www.google.com/mapfiles/marker" + letter + ".png";

          letteredIcon=make_icon(thecolor);

          //letteredIcon = 'http://thydzik.com/thydzikGoogleMap/markerlink.php?text='+letter+'&color=5680FC';

          var markerOptions ;

          markerOptions={};

          markerOptions.icon=letteredIcon;

          markerOptions.draggable=drag;

          var marker = new GMarker(point,markerOptions);

          marker.value = number;

          marker.title=thisName;

          GEvent.addListener(marker, 'click', function() {

                marker.openInfoWindow('<span style=\"color: #0077ff;\"> ' + thisName + '</span>');

            });

         return marker;

    }



function createrelay(){

        if (lastmarker_r.value>1){ 

            map.removeOverlay(lastmarker_r);

        }

        var button=document.getElementById('ActionButton');

        if (button.value=="add"){

            var point = map.getCenter();

            var center = new GLatLng(point.lat(),point.lng());

            //center.Latitude+=5;

            //center.Longitude+=5;

            var marker = createMarkerC(center,2,"Relay Point",true);

            GEvent.addListener(marker, "dragend", processPoints);

            map.addOverlay(marker);

            npoint=document.getElementById('rlocate');

            npoint.value=center;

            lastmarker_r=marker;

            m3=marker;

            button.value="remove";

        }else{

            removerelay();

            button.value="add";            

        }

        processPoints();

}

function removerelay(){



        if (lastmarker_r.value>1){ 

            map.removeOverlay(lastmarker_r);

        }

        m3=null;

        document.getElementById("rlatu").value="";document.getElementById("rlonu").value="";

        document.getElementById("rlat").value="";document.getElementById("rlon").value="";

}

   

function initialize() { 

    if (GBrowserIsCompatible()){

      map = new GMap2(document.getElementById('map_canvas'),{draggableCursor:'url(fcur.cur),default'});

      map.setMapType(G_HYBRID_MAP);

      map.addControl( new GLargeMapControl() );

      map.addControl( new GMapTypeControl()) ;

      map.addControl(new GScaleControl());

      map.addControl( new GOverviewMapControl(new GSize(100,100)) );

      map.setCenter(new GLatLng(Lat,Lon), 9);

      map.enableScrollWheelZoom();



        othermarkers();

        showmarkers();



      var point = new GLatLng(Lat,Lon);

      var html = '';

      html += thename;

      html += '';

      mcount++;

      var marker = createMarkerN(point,mcount,thename,false,'V','blue');

      

      m1=marker;

      map.addOverlay(marker);

      marker.openInfoWindow('<span style=\"color: #0077ff;\"> ' + thename + '</span>');

      

//      GEvent.addListener(map,'click', function(overlay, latlng) {

//        if (latlng){

 //           if (lastmarker.value>1){ 

 //               map.removeOverlay(lastmarker);

  //          }

   //         var marker = createMarkerB(latlng,2,"Selected Point",true);

   //         GEvent.addListener(marker, "dragend", processPoints);

   //         map.addOverlay(marker);

   //         npoint=document.getElementById('clocate');

   //         npoint.value=latlng;

   //         lastmarker=marker;

   //         m2=marker;

   //         processPoints();

   //         }

    //    });

        

    }

}



function initializeedit() { 

    if (GBrowserIsCompatible()){

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

      var marker = createMarkerT(point,1,thename,true);

      GEvent.addListener(marker, 'dragend',function(){

          processtower();

      });

      m1=marker;

      map.addOverlay(marker);

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

    function getTextAddress(address,nameobj,latobj,lonobj,dlatobj,dlonobj) 

    {

      geocoder.getLatLng(

        address,

        function(point) {

          if (!point) {

            var obj=SelectObject(latobj);obj.value="";

            var obj=SelectObject(lonobj);obj.value="";

            var obj=SelectObject(dlatobj);obj.value="";

            var obj=SelectObject(dlonobj);obj.value="";

            var obj=SelectObject(nameobj);obj.value="Address not found";

            return false;

          } else {

            m1 = new GMarker(point);

            mypoint = m1.getPoint();

            var obj=SelectObject(latobj);obj.value=ConvertMetric(mypoint.lat(),"LAT");

            var obj=SelectObject(lonobj);obj.value=ConvertMetric(mypoint.lng(),"LON");

            setTimeout("__doPostBack('"+obj.name+"','')", 0);

            var obj=SelectObject(dlatobj);obj.value=mypoint.lat();

            var obj=SelectObject(dlonobj);obj.value=mypoint.lng();

            var obj=SelectObject(nameobj);obj.value=address; 

            return true;                       

          }

        }

      );

     }

     function SelectObject(PartName)

        {

            var str = '';

            var elem = document.getElementById('aspnetForm').elements;

            for(var i = 0; i < elem.length; i++)

            {

                if (elem[i].name.toLowerCase().indexOf(PartName.toLowerCase())>-1){

                    return elem[i];

                }

            }

            return -1;

        }




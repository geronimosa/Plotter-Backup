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

    function getelement(element){
        try {
            return document.getElementById(element).value;
        } catch(err){
            return "";
        }
    }
    function putelement(element,newvalue){
        try {
            document.getElementById(element).value=newvalue;
            
        } catch(err){
            
        }
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
            putelement("rlatu","");putelement("rlonu","");
            putelement("rlat","");putelement("rlon","");
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
        var p1 = m1.getPoint();
        putelement('lattitude',ConvertMetric(p1.lng(),"LON"));
        putelement('longitude',ConvertMetric(p1.lat(),"LAT"));
        putelement('londec',p1.lng());
        putelement('latdec',p1.lat());
        document.title=''+ConvertMetric(p1.lng(),"LON")+'|'+ConvertMetric(p1.lat(),"LAT")+'|'+p1.lng()+'|'+p1.lat()+'|';

    }

    function createMarkerA(point, number, thisName, drag) {
        var letter = "P";
        var letteredIcon = new GIcon(baseIcon);
        letteredIcon.image = "http://www.google.com/mapfiles/marker" + letter + ".png";
        var markerOptions ;
        markerOptions={};
        markerOptions.icon=letteredIcon;
        markerOptions.draggable=true;        
        var marker = new GMarker(point,markerOptions);
        marker.value = number;
        GEvent.addListener(marker, 'dragend',function(){
                processPoints();
            });
//        GEvent.addListener(marker, 'click', function() {
  //          marker.openInfoWindowHtml('<span style=\"color: #0077ff;\"> Tower:<b>' + thisName + '</b></span>');
    //    });
         return marker;
    }
   
function initialize() { 
    if (GBrowserIsCompatible()){
      map = new GMap2(document.getElementById('map_canvas'),{draggableCursor:'url(fcur.cur),default'});
      map.setMapType(G_HYBRID_MAP);
      map.addControl( new GLargeMapControl() );
      map.addControl( new GMapTypeControl()) ;
      map.addControl( new GOverviewMapControl(new GSize(100,100)) );
      map.setCenter(new GLatLng(Lat,Lon),16);
      map.enableScrollWheelZoom();
      var point = new GLatLng(Lat,Lon);
      var html = '<div style="height:20px; width:200px; zindex:100">';
      html += thename;
      html += '</div>';
      mcount++;
      var marker = createMarkerA(point,mcount,thename,true);
      m1=marker;
      map.addOverlay(marker);
      othermarkers();
      processPoints();
      
    }
}

function showAddress(address) {
      geocoder.getLatLng(
        address,
        function(point) {
          if (!point) {
            alert(address + " not found");
          } else {
            map.setCenter(point, 16);
            //mcount++;
            if (lastmarker.value>1){
               map.removeOverlay(lastmarker);
            }
            var marker = createMarkerA(point,2,address,true);
            m1=marker;
            map.addOverlay(marker);
            GEvent.addListener(marker, 'dragend',function(){
                processPoints();
            });            
          }
        }
      );
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
    var mcount=0;
    var map;
//   var baseIcon = new GIcon(G_DEFAULT_ICON);
  //  baseIcon.image="http://plotter.vlocity.co.za/images/red_MarkerV.png"
    //baseIcon.shadow = "http://labs.google.com/ridefinder/images/mm_20_shadow.png";

    
    function createMarker(themap, point, number, thisName, drag, letter, thecolor) {
        //var letteredIcon = new GIcon(baseIcon);
        var linecolors=['red'  ,'orange','yellow','green' ,'blue'  ,'indigo','violet','white' ,'black' ,'grey'];
        var colorcode=['ff0000','ff8000','ffff00','01DF01','0101DF','8A0886','FA58F4','ffffff','000000','848484'];
        if (letter=='T'){thecolor='01DF01'}
        if (letter=='H'){thecolor='848484'}
        if (letter=='P'){thecolor='ff0000'}
        if (letter=='B'){thecolor='ffffff'}
        
        var image = 'http://thydzik.com/thydzikGoogleMap/markerlink.php?text='+letter+'&color='+thecolor;
        var shape = {
             coord: [1, 1, 1, 20, 18, 20, 18 , 1], type:   'poly'};
             var beachMarker = new google.maps.Marker({
                 position: point,
                 map: themap,
                 icon: image,
                 shape: shape,
                 title: '" + Request.QueryString("namefrom") + "'
            });
            
            var infowindow = new google.maps.InfoWindow({
                 content: '<font style=""color:blue"">' + thisName + '</font>',
                 size: new google.maps.Size(50,50)
            });

            google.maps.event.addListener(beachMarker, 'click', function() {
                infowindow.open(map,beachMarker);
            });
            
            
         //return beachMarker;
    }
    
    

function initialize() { 
//    if (GBrowserIsCompatible()){
      var posstart=new google.maps.LatLng(Lat,Lon);
      var myOptions = {
            zoom: 10,
            center: posstart,
            mapTypeId:  'hybrid'
      };
      map = new google.maps.Map(document.getElementById('map_canvas'), myOptions);
      
     


        othermarkers();
        showmarkers();

      var point = new google.maps.LatLng(Lat,Lon);
      var html = '';
      html += thename;
      html += '';
      mcount++;
      //var marker = 
      createMarker(point,mcount,thename,false,'V','ff0000');
      
      //m1=marker;
//      map.addOverlay(marker);
  //    marker.openInfoWindow('<span style=\"color: #0077ff;\"> ' + thename + '</span>');
              
  //  }
}

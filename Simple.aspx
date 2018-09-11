<%@ Page  Language="VB" MasterPageFile="~/Default.master" AutoEventWireup="false" CodeFile="Simple.aspx.vb" Inherits="Simple" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" Runat="Server">
<div id="map_canvas" style="width:700; height:800"></div>

<script type="text/javascript"
    src="http://maps.google.com/maps/api/js?sensor=false">
</script>
<script type="text/javascript">
  function initialize() {
    alert("hello");
    var latlng = new google.maps.LatLng(-34.397, 150.644);
    var myOptions = {
      zoom: 8,
      center: latlng,
      mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("map_canvas"),
        myOptions);
  }
  this.onload="initialize()";

</script>

</asp:Content>


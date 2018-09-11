
Partial Class Simple1
    Inherits System.Web.UI.Page


    Private Sub WriteStartScript()
        Dim TheScript As String
        TheScript = "<script type='text/javascript'>"
        
        TheScript += "var posstart=new google.maps.LatLng(" + Request.QueryString("lat1") + "," + Request.QueryString("lon1") + ");"
        TheScript += "var posend=new google.maps.LatLng(" + Request.QueryString("lat2") + "," + Request.QueryString("lon2") + ");"
        TheScript += "var towernames= '" + Request.QueryString("NameFrom") + "';"
        TheScript += "var towerheight= " + Request.QueryString("ht1") + ";"
        TheScript += "var clientheight= " + Request.QueryString("ht2") + ";"

        TheScript += "initialize();"

        TheScript += "var image = 'http://code.google.com/apis/maps/documentation/javascript/examples/images/beachflag.png';"
        TheScript += "var shape = {"
        TheScript += "coord: [1, 1, 1, 20, 18, 20, 18 , 1],"
        TheScript += "type:   'poly'"
        TheScript += " };"

        TheScript += "var infowindow1 = new google.maps.InfoWindow({"
        TheScript += "content: '<p style=""color:blue"" >" + Request.QueryString("NameFrom") + "</p>'"
        TheScript += "});"
        TheScript += "var infowindow2 = new google.maps.InfoWindow({"
        TheScript += "content: '<p style=""color:blue"" >" + Request.QueryString("NameTo") + "</p>'"
        TheScript += "});"


        TheScript += "var beachMarker1 = new google.maps.Marker({"
        TheScript += "position: posstart,"
        TheScript += "map: map,"
        TheScript += "shape: shape,"
        'TheScript += "icon: image,"
        TheScript += "title: '" + Request.QueryString("NameFrom") + "'"
        TheScript += "});"

        
        TheScript += "var beachMarker2 = new google.maps.Marker({"
        TheScript += "position: posend,"
        TheScript += "map: map,"
        TheScript += "shape: shape,"
        ' TheScript += "icon: image,"
        TheScript += "title: '" + Request.QueryString("NameTo") + "'"
        TheScript += "});"

        TheScript += "google.maps.event.addListener(beachMarker1, 'click', function() {"
        TheScript += "infowindow1.open(map,beachMarker1);"
        TheScript += "});"
        TheScript += "google.maps.event.addListener(beachMarker2, 'click', function() {"
        TheScript += "infowindow2.open(map,beachMarker2);"
        TheScript += "});"


        'TheScript += "map.addOverlay(marker);"
        TheScript += "</script>"
        ClientScript.RegisterStartupScript(Request.GetType, "Startup", TheScript)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        WriteStartScript()
        'Dim MyPlot As New googlegraph(Request.QueryString("lat1"), Request.QueryString("lon1"), Request.QueryString("NameFrom"), Request.QueryString("ht1"), Request.QueryString("lat2"), Request.QueryString("lon2"), Request.QueryString("NameTo"), Request.QueryString("ht2"))
        'Page.Controls.Add(MyPlot)

    End Sub

    
End Class

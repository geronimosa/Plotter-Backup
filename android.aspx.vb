
Partial Class android
    Inherits System.Web.UI.Page


    Private Sub WriteStartScript()
        Dim TheScript As String
        TheScript = "<script type='text/javascript'>"

        TheScript += "var posstart=new google.maps.LatLng(" + Request.QueryString("lat1") + "," + Request.QueryString("lon1") + ");"
        TheScript += "var posend=new google.maps.LatLng(" + Request.QueryString("lat2") + "," + Request.QueryString("lon2") + ");"
        TheScript += "var towernames= '" + Request.QueryString("NameTo") + "';"
        TheScript += "var towerheight= " + Request.QueryString("ht1") + ";"
        TheScript += "var clientheight= " + Request.QueryString("ht2") + ";"
        TheScript += "var sentbearing= " + Request.QueryString("bearing") + ";"
        TheScript += "var sentdistance= " + Request.QueryString("distance") + ";"
        TheScript += "initialize();"
        TheScript += "</script>"
        ClientScript.RegisterStartupScript(Request.GetType, "Startup", TheScript)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        WriteStartScript()

    End Sub


End Class


Partial Class Points
    Inherits System.Web.UI.Page


    Private Sub WriteStartScript(ByVal Towerheight As Integer, ByVal Clientheight As Integer, ByVal Distance As String, ByVal Bearing As String, ByVal longa As String, ByVal lata As String, ByVal longb As String, ByVal latb As String, Optional ByVal DisplayMap As Boolean = False)
        Dim TheScript As String = ""
        TheScript += "<script language='javascript' type='text/javascript'>" & vbCrLf
        TheScript += "  var pointa = new google.maps.LatLng(" & longa & "," & lata & ");" & vbCrLf
        TheScript += "  var pointb = new google.maps.LatLng(" & longb & "," & latb & ");" & vbCrLf
        TheScript += "  var distance = " & Distance & ";" & vbCrLf
        TheScript += "  var bearing = " & Bearing & ";" & vbCrLf
        TheScript += "  var clientheight = " & Clientheight & ";" & vbCrLf
        TheScript += "  var towerheight = " & Towerheight & ";" & vbCrLf
        TheScript += "  var mapactive = " & DisplayMap.ToString.ToLower & ";" & vbCrLf
        TheScript += "  initialize();  " & vbCrLf
        TheScript += "</script>" & vbCrLf
        ClientScript.RegisterStartupScript(Request.GetType, "Startup", TheScript)
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            WriteStartScript(Request.QueryString("towerheight"), Request.QueryString("clientheight"), Request.QueryString("distance"), Request.QueryString("bearing"), Request.QueryString("lata"), Request.QueryString("longa"), Request.QueryString("latb"), Request.QueryString("longb"), Request.QueryString("mapactive"))
        Else

        End If
    End Sub
End Class


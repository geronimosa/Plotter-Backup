
Partial Class TestMASS
    Inherits System.Web.UI.Page
    Dim AClass As New MySqlDataClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
      
        Dim MyPlot As Plotting
        Dim Distance As Double
        Dim LinePlotStrings As String()
        Dim LinePlots As Double()

        Distance = Val(Request.QueryString("Distance"))
        LinePlotStrings = Split(Request.QueryString("points"), ",")
        LinePlots = Array.ConvertAll(LinePlotStrings, Function(input As String) Double.Parse(input))

        MyPlot = Me.LoadControl("~\Plotting.ascx")
        MyPlot.SetupNew(Distance, LinePlots)
        Me.Controls.Add(MyPlot)
    End Sub
End Class


Partial Class googlegraph
    Inherits System.Web.UI.UserControl

    Private Function MakeControl(ByVal controlname As String, ByVal controlvalue As String, ByVal controltype As System.Web.UI.HtmlTextWriterTag) As WebControl
        Dim newcontrol As New WebControl(controltype)
        With newcontrol
            .ID = controlname
            If controlvalue <> "" Then .Attributes("value") = controlvalue
        End With
        Return newcontrol
    End Function
    Public Sub New()

    End Sub
    Public Sub New(ByVal fromlat As String, ByVal fromlong As String, ByVal fromname As String, ByVal fromheight As String, ByVal tolat As String, ByVal tolong As String, ByVal toname As String, ByVal toheight As String)
        'action = MakeControl("fromlat", fromlat) : Me.Controls.Add(action)
        'action = MakeControl("fromlong", fromlong) : Me.Controls.Add(action)
        'action = MakeControl("fromname", fromname) : Me.Controls.Add(action)
        'action = MakeControl("fromheight", fromheight) : Me.Controls.Add(action)
        'action = MakeControl("tolat", tolat) : Me.Controls.Add(action)
        'action = MakeControl("tolong", tolong) : Me.Controls.Add(action)
        'action = MakeControl("toname", toname) : Me.Controls.Add(action)
        'action = MakeControl("toheight", toheight) : Me.Controls.Add(action)
        '
        Dim action As WebControl

        action = MakeControl("newdiv", "", HtmlTextWriterTag.Div)
        action.Style.Add("width", "800px")
        action.Style.Add("height", "200px")
        Me.Controls.Add(action)
        '
        Dim actionstring As String
        actionstring = "elevator = new google.maps.ElevationService();new_drawPath('" & fromlat & "','" & fromlong & "','" & fromname & "','" & fromheight & "','" & tolat & "','" & tolong & "','" & toname & "','" & toheight & ",""newdiv""');"
        action = MakeControl("action", "Plot", HtmlTextWriterTag.Button) : Me.Controls.Add(action)
        ' actionstring = "alert('you doos');"
        action.Attributes.Add("onclick", actionstring) : Me.Controls.Add(action)


        '<div align=center id="elevation_chart" style="width:800px; height:200px; "></div>
        'elevator = new google.maps.ElevationService();
        ' new_drawPath(posstart,posend,fromheight,toheight);
    End Sub
End Class

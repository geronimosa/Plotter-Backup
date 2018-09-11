
Partial Class ShowMap
    Inherits System.Web.UI.Page
    Dim AClass As New MySqlDataClass

    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                WriteScript()
            Catch ex As Exception
                ' RaiseEvent DataError(ex.Message)
            End Try
        End If
    End Sub

    Private Sub WriteScript()
        Dim TheScript As String
        TheScript = "<script type='text/javascript'>"
        If Request.QueryString("SetName") <> "" Then
            TheScript += "var thename='" & Request.QueryString("SetName") & "';"
            tlocate.Value = Request.QueryString("SetName")
        Else
            TheScript += "var thename='Undefined';"
        End If
        If Request.QueryString("FindLong") <> "" Then
            TheScript += "var Lon=" & Request.QueryString("FindLong") & ";"
        Else
            TheScript += "var Lon=18.5889444444444;"
        End If
        If Request.QueryString("FindLat") <> "" Then
            TheScript += "var Lat=" & Request.QueryString("FindLat") & ";"
        Else
            TheScript += "var Lat=-33.8546666666667;"
        End If
        If Request.QueryString("SetHeight") <> "" Then
            TheScript += "var TowerHeight=" & Request.QueryString("SetHeight") & ";"
            theight.Value = Request.QueryString("SetHeight")
        End If

        TheScript += "function showmarkers(){" & Session("OtherTowers") & "}"
        TheScript += "function pastedata(){alert('Nothing to paste!');}"

        'othermarkers
        If Request.QueryString("FindAddress") <> "" Then
            TheScript += "function othermarkers(){showAddress('" & Request.QueryString("FindAddress") & "');}"
        Else
            TheScript += "function othermarkers(){};"
           
        End If
        'End If
        TheScript += "</script>"
        ClientScript.RegisterClientScriptBlock(Request.GetType, "opening", TheScript)
    End Sub

    
End Class

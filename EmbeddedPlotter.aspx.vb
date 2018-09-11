Imports MySql.Data.MySqlClient

Partial Class EmbeddedPlotter

    Inherits System.Web.UI.Page

    Dim AClass As New MySqlDataClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            Dim a As Integer = 1
        Else
            Try
                SetupMaps()
            Catch ex As Exception
                ' RaiseEvent DataError(ex.Message)
            End Try
        End If
    End Sub

    Private Sub WriteScript(ByRef Lat As String, ByRef Lon As String, ByRef theName As String)
        Dim TheScript As String
        TheScript = "<script type='text/javascript'>"

        TheScript += "var Lat=" & Lat & ";"
        TheScript += "var Lon=" & Lon & ";"
        TheScript += "var thename='" & theName & "';"

        ' new section
        TheScript += "function plottower(vlat,vlon,vthename){"
        TheScript += "  Lat=vlat;"
        TheScript += "  Lon=vlon;"
        TheScript += "  thename=vthename;"
        TheScript += "}"

        'othermarkers
        TheScript += "function showmarkers(){" & Session("OtherTowers") & "}"
        TheScript += "function pastedata(){alert('Nothing to paste!');}"
        If Request.QueryString("FindAddress") <> "" Then
            TheScript += "function othermarkers(){showAddress('" & Request.QueryString("FindAddress") & "');"
            TheScript += "};"
        Else
            TheScript += "function othermarkers(){};"
        End If
        'End If
        TheScript += "</script>"
        ClientScript.RegisterClientScriptBlock(Request.GetType, "opening", TheScript)
    End Sub


    Private Sub SetupMaps()
        Dim Lat As String
        Dim Lon As String
        Dim theName As String
        Lat = "-33.8546666666667"
        Lon = "18.5889444444444"
        theName = "Tygerburg"

        Dim MyRst As MySql.Data.MySqlClient.MySqlDataReader
        Try
            MyRst = AClass.Recordset("select * from tower_pops where id=" & "1" & "")
            If MyRst.HasRows Then
                MyRst.Read()
                Lat = MyRst.Item("pop_lat_dec").ToString
                Lon = MyRst.Item("pop_long_dec").ToString
                theName = MyRst.Item("pop_name").ToString
                tlocate.Value = theName
                theight.Value = MyRst.Item("pop_height").ToString
            End If
        Catch
            Response.Write(Err.Description)
        End Try
        Session("copyname") = Me.copyname.Text
        Session("copyanalat") = Me.copyanalat.Text
        Session("copyanalon") = Me.copyanalon.Text
        Session("copydiglat") = Me.copydiglat.Text
        Session("copydiglon") = Me.copydiglon.Text
        WriteScript(Lat, Lon, theName)
    End Sub




End Class

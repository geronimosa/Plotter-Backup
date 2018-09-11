Imports MySql.Data.MySqlClient
Partial Class Lookup
    Inherits System.Web.UI.Page
    Dim AClass As New MySqlDataClass


    Private Sub FillCombo(ByVal tc As DropDownList, Optional ByVal FilterRegion As String = "")
        Try
            Dim MyRst As MySql.Data.MySqlClient.MySqlDataReader
            Dim SelectedItem As Int32 = -1
            Dim NewItem As ListItem
            If tc.Items.Count > 0 Then
                SelectedItem = tc.SelectedIndex
                tc.Items.Clear()
            End If
            Dim Where As String = ""
            If FilterRegion <> "" Then
                Where = " Where pop_region='" & FilterRegion & "' "
            End If
            NewItem = New ListItem("Select a Tower", 0) : tc.Items.Add(NewItem)
            MyRst = AClass.Recordset("select * from tower_pops " & Where & " order by pop_name")
            While MyRst.Read
                If MyRst.Item("pop_name").ToString.Trim <> "" Then
                    NewItem = New ListItem(MyRst.Item("pop_name").ToString, MyRst.Item("id"))
                    tc.Items.Add(NewItem)
                End If

            End While
            If SelectedItem > -1 Then
                tc.SelectedIndex = SelectedItem
            End If
        Catch ex As Exception
            'RaiseEvent DataError(ex.Message)
        End Try
    End Sub

    Private Sub FillRegions(ByVal tc As DropDownList)
        Try
            Dim MyRst As MySql.Data.MySqlClient.MySqlDataReader
            Dim SelectedItem As Int32 = -1
            Dim NewItem As ListItem
            If tc.Items.Count > 0 Then
                SelectedItem = tc.SelectedIndex
                tc.Items.Clear()
            End If
            MyRst = AClass.Recordset("select pop_region from tower_pops group by pop_region")
            While MyRst.Read
                If MyRst.Item("pop_region").ToString.Trim <> "" Then
                    NewItem = New ListItem(MyRst.Item("pop_region").ToString, MyRst.Item("pop_region").ToString)
                    tc.Items.Add(NewItem)
                End If
            End While
            If SelectedItem > -1 Then
                tc.SelectedIndex = SelectedItem
            End If
        Catch ex As Exception
            'RaiseEvent DataError(ex.Message)
        End Try
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim Lat As String = Request.QueryString("lat")
        Dim Lon As String = Request.QueryString("lon")
        Dim theName As String = Request.QueryString("name")
        Dim gStyle As String = Request.QueryString("style")
        If Lat = "" Then
            Lat = "-33.8546666666667"
            Lon = "18.5889444444444"
            theName = "Tygerburg"
            'gStyle = "G_PHYSICAL_MAP"
        End If
        Dim TheScript As String
        TheScript = "<script type='text/javascript'>"
        TheScript += "    var Lat=" & Lat & ";"
        TheScript += "    var Lon=" & Lon & ";"
        TheScript += "    var thename='" & theName & "';"
        TheScript += "</script>"
        FillCombo(TowerList)

        ClientScript.RegisterClientScriptBlock(Request.GetType, "opening", TheScript)
    End Sub


    Protected Sub TowerList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TowerList.SelectedIndexChanged
        Dim MyRst As MySql.Data.MySqlClient.MySqlDataReader

        Try

            MyRst = AClass.Recordset("select * from tower_pops where id=" & Me.TowerList.SelectedItem.Value & "")
            If MyRst.HasRows Then
                MyRst.Read()
                Response.Redirect("~/Maps.aspx?lat=" & MyRst.Item("pop_lat_dec").ToString & "&lon=" & MyRst.Item("pop_long_dec").ToString & "&name=" & TowerList.SelectedItem.Text & "&ht1=" & MyRst.Item("pop_height").ToString)
            End If

        Catch ex As Exception
            ' RaiseEvent DataError(ex.Message)
        End Try
    End Sub
End Class

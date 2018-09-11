Imports MySql.Data.MySqlClient
Partial Class Coverage
    Inherits System.Web.UI.Page
    Dim AClass As New MySqlDataClass
    Dim DefaultRegion As String = "GAU"

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
                    NewItem = New ListItem(MyRst.Item("pop_region").ToString.ToUpper, MyRst.Item("pop_region").ToString.ToUpper)
                    tc.Items.Add(NewItem)
                    If SelectedItem = -1 And NewItem.Value.ToUpper = DefaultRegion Then
                        SelectedItem = tc.Items.Count - 1
                    End If
                End If
            End While
            If SelectedItem > -1 Then
                tc.SelectedIndex = SelectedItem
            Else
                tc.SelectedIndex = 0
            End If
        Catch ex As Exception
            'RaiseEvent DataError(ex.Message)
        End Try
    End Sub
    Private Function FillCombo(ByVal tc As DropDownList, Optional ByVal FilterRegion As String = "") As Long
        Try
            Dim MyRst As MySql.Data.MySqlClient.MySqlDataReader
            Dim SelectedItem As Int32 = -1
            Dim NewItem As ListItem
            Dim FirstItem As Long = 0
            Dim OtherTowers As String = ""
            If tc.Items.Count > 0 Then
                SelectedItem = tc.SelectedIndex
                tc.Items.Clear()
            End If
            Dim Where As String = ""
            If FilterRegion <> "" Then
                Where = " Where pop_region='" & FilterRegion & "' "
            End If
            'NewItem = New ListItem("Select a Tower", 0) : tc.Items.Add(NewItem)
            MyRst = AClass.Recordset("select * from tower_pops " & Where & " order by pop_name")
            While MyRst.Read
                If FirstItem = 0 Then FirstItem = MyRst.Item("id")
                If MyRst.Item("pop_name").ToString.Trim <> "" Then
                    NewItem = New ListItem(StrConv(MyRst.Item("pop_name").ToString, VbStrConv.ProperCase), MyRst.Item("id"))
                    tc.Items.Add(NewItem)
                End If
                OtherTowers += "var point = new google.maps.LatLng(" & MyRst.Item("pop_lat_dec").ToString & "," & MyRst.Item("pop_long_dec").ToString & ");"
                'OtherTowers += "var html = '<div style=""height:20px; width:200px; zindex:100"">';"
                'OtherTowers += "html += '" & MyRst.Item("pop_name").ToString & "';"
                'OtherTowers += "html += '</div>';"
                Dim Html As String = "<b>" & Replace(MyRst.Item("pop_name").ToString, "'", "") & "</b><br>Type: " & MyRst.Item("pop_type").ToString & "<br>Active: " & IIf(MyRst.Item("pop_istower"), "Yes", "No")
                Html += "<br>Height: " & MyRst.Item("pop_height").ToString & " Meters AGL."
                Html += "<br>" & MyRst.Item("pop_long").ToString & " , " & MyRst.Item("pop_lat").ToString


                Dim Letter As String = Left(MyRst.Item("pop_type").ToString.ToUpper, 1)
                Dim defColor As String = "848484"

                If IIf(MyRst.Item("pop_istower"), "Yes", "No") = "Yes" Then
                    If MyRst.Item("pop_type").ToString.ToLower = "tower" Then
                        defColor = "848484"
                    Else
                        defColor = "01DF01"
                    End If
                Else
                    defColor = "ff0000"
                End If

                OtherTowers += "mcount++;"
                OtherTowers += "createMarker(map, point," & tc.Items.Count + 3 & ",'" & Html & "',false,'" & Letter & "','" & defColor & "');"
                'OtherTowers += "map.addOverlay(marker);"

            End While
            OtherTowers += " var MaxPoints=" & tc.Items.Count - 1 & ";"
            Session("OtherTowers") = OtherTowers
            If SelectedItem > -1 Then
                tc.SelectedIndex = SelectedItem
            Else
                tc.SelectedIndex = 0
            End If
            Return FirstItem
        Catch ex As Exception
            Return 0
            'RaiseEvent DataError(ex.Message)
        End Try
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DefaultRegion = Request.QueryString("ViewRegion").ToString.ToUpper.Trim
            If DefaultRegion.ToString.ToUpper = "" Then DefaultRegion = "GAU"
            Try
                FillRegions(RegionList)
                FillCombo(Me.TowerList, RegionList.SelectedValue)
                SetupMaps(TowerList.SelectedValue)
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
        'othermarkers
        TheScript += "function othermarkers(){}function showmarkers(){" & Session("OtherTowers") & "}"
        '        TheScript += "function pastedata(){alert('Nothing to paste!');}"
        'End If
        TheScript += "</script>"
        ClientScript.RegisterClientScriptBlock(Request.GetType, "opening", TheScript)
    End Sub


    Private Sub SetupMaps(ByVal TowerID As String)
        Dim Lat As String
        Dim Lon As String
        Dim theName As String
        Lat = "-33.8546666666667"
        Lon = "18.5889444444444"
        theName = "Tygerburg"

        Dim MyRst As MySql.Data.MySqlClient.MySqlDataReader
        Try
            MyRst = AClass.Recordset("select * from tower_pops where id=" & Me.TowerList.SelectedItem.Value & "")
            If MyRst.HasRows Then
                MyRst.Read()
                Lat = MyRst.Item("pop_lat_dec").ToString
                Lon = MyRst.Item("pop_long_dec").ToString
                theName = MyRst.Item("pop_name").ToString
                Dim Html As String = "<b>" & MyRst.Item("pop_name").ToString & "</b><br>Type: " & MyRst.Item("pop_type").ToString & "<br>Active: " & IIf(MyRst.Item("pop_istower"), "Yes", "No")
                Html += "<br>Height: " & MyRst.Item("pop_height").ToString & " Meters AGL."
                Html += "<br>" & MyRst.Item("pop_long").ToString & " , " & MyRst.Item("pop_lat").ToString
                theName = Html
            End If
        Catch
            Response.Write(Err.Description)
        End Try
        WriteScript(Lat, Lon, theName)
    End Sub

    

    


    Protected Sub RegionList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RegionList.SelectedIndexChanged
        FillCombo(Me.TowerList, RegionList.SelectedValue)
        TowerList.SelectedIndex = 0
        SetupMaps(Me.TowerList.SelectedValue)
    End Sub

    Protected Sub TowerList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TowerList.SelectedIndexChanged
        SetupMaps(TowerList.SelectedValue)
    End Sub
End Class

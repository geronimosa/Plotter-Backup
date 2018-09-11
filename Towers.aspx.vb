Imports MySql.Data.MySqlClient
Partial Class Towers
    Inherits System.Web.UI.Page

    Dim AClass As New MySqlDataClass


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If HttpContext.Current.User.IsInRole("super") Or HttpContext.Current.User.IsInRole("admin") Or HttpContext.Current.User.IsInRole("staff") Then
        Else
            Response.Redirect("default.aspx")
        End If

        If Not IsPostBack Then
            FillCombo(TowerList)
            ShowData(0)
        End If
    End Sub

    Private Sub FillLists()
        Dim MyRst As MySql.Data.MySqlClient.MySqlDataReader
        Dim MyItem As ListItem
        Try
            MyRst = AClass.Recordset("select pop_type from tower_pops group by pop_type")
            If MyRst.HasRows Then
                Me.TypeList.Items.Clear()
                While MyRst.Read()
                    MyItem = New ListItem(MyRst.Item("pop_type"))
                    Me.TypeList.Items.Add(MyItem)
                End While
            End If
            MyRst = AClass.Recordset("select pop_region from tower_pops group by pop_region")
            If MyRst.HasRows Then
                Me.RegionList.Items.Clear()
                While MyRst.Read()
                    MyItem = New ListItem(MyRst.Item("pop_region"))
                    Me.RegionList.Items.Add(MyItem)
                End While
            End If
        Catch ex As Exception
        End Try

    End Sub
    Private Sub FillCombo(ByVal tc As DropDownList, Optional ByVal FilterRegion As String = "")
        Dim MyRst As MySql.Data.MySqlClient.MySqlDataReader
        Dim SelectedItem As Int32 = -1
        Dim NewItem As ListItem
        Dim Where As String = ""
        Dim FirstTime As Boolean = True
        Try
            FillLists()
            tc.Items.Clear()
            If FilterRegion <> "" Then
                Where = " Where pop_region='" & FilterRegion & "' "
            End If
            MyRst = AClass.Recordset("select * from tower_pops " & Where & " order by pop_region,pop_name")
            While MyRst.Read()
                'If FirstTime Then
                '    FirstTime = False
                '    ShowData(MyRst.Item("id"))
                'End If
                If MyRst.Item("pop_name").ToString.Trim <> "" Then
                    NewItem = New ListItem(MyRst.Item("pop_region").ToString & "." & MyRst.Item("pop_name").ToString, MyRst.Item("id"))
                    tc.Items.Add(NewItem)
                End If
            End While
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub TowerList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TowerList.SelectedIndexChanged
        ShowData(Me.TowerList.SelectedItem.Value)

    End Sub

    Sub ShowData(ByVal ActID As Long)
        Dim MyRst As MySql.Data.MySqlClient.MySqlDataReader
        AddButton.Visible = True
        DeleteButton.Visible = True

        Dim TheScript As String = ""
        If ActID = 0 Then
            MyRst = AClass.Recordset("select * from tower_pops order by pop_name limit 0,1")
        Else
            MyRst = AClass.Recordset("select * from tower_pops where id=" & ActID & "")
        End If
        If MyRst.HasRows Then
            MyRst.Read()
            TheScript = "<script type='text/javascript'>"
            TheScript += "var Lat=" & MyRst.Item("pop_lat_dec").ToString & ";"
            TheScript += "var Lon=" & MyRst.Item("pop_long_dec").ToString & ";"
            TheScript += "var thename='" & MyRst.Item("pop_name").ToString & "';"
            TheScript += "var fuckup='" & ActID & "';"

            TheScript += "</script>"
            ClientScript.RegisterClientScriptBlock(Request.GetType, "opening", TheScript)
            Try
                Me.TowerList.SelectedValue = MyRst.Item("id")
            Catch ex As Exception
            End Try
            Me.CurID.Value = MyRst.Item("id").ToString
            Me.towername.Value = MyRst.Item("pop_name").ToString
            Me.AGL.Value = MyRst.Item("pop_height").ToString
            Me.IsTower.Checked = MyRst.Item("pop_istower")
            Me.Owner.Value = MyRst.Item("pop_owner").ToString
            Me.Contact.Value = MyRst.Item("pop_contact").ToString
            Me.Monthly.Value = MyRst.Item("pop_cost").ToString
            Me.RegionList.Text = MyRst.Item("pop_region").ToString
            Me.TypeList.Text = MyRst.Item("pop_type").ToString
        End If
    End Sub


    Protected Sub AddButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddButton.Click
        ShowData(Me.TowerList.SelectedItem.Value)
        AddButton.Visible = False
        DeleteButton.Visible = False
        CurID.Value = "New"
        towername.Value = "Type new name and select a point on map."
        AGL.Value = ""
        Owner.Value = ""
        Contact.Value = ""
        Monthly.Value = ""

    End Sub

    Protected Sub SaveButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles SaveButton.Click
        Dim SqlString As String
        Dim iId As Long
        If Me.CurID.Value = "New" Then
            SqlString = "Insert into tower_pops (pop_name,pop_long,pop_long_dec,pop_lat,pop_lat_dec,pop_height,pop_istower,pop_owner,pop_contact,pop_cost,pop_region,pop_type,pop_input_long,pop_input_lat) values (" & _
                "'" & Me.towername.Value & "','" & Replace(Me.rlonu.Value, "'", "`") & "'," & Me.rlon.Value & ",'" & Replace(Me.rlatu.Value, "'", "`") & "'," & _
                "" & Me.rlat.Value & "," & Me.AGL.Value & "," & IIf(Me.IsTower.Checked, 1, 0) & ",'" & Me.Owner.Value & "','" & Me.Owner.Value & "'," & Val(Me.Monthly.Value) & "," & _
                "'" & Me.RegionList.Text & "','" & Me.TypeList.Text & "','" & Replace(Me.rlonu.Value, "'", "`") & "','" & Replace(Me.rlatu.Value, "'", "`") & "') ; SELECT LAST_INSERT_ID() AS Current_Identity; "
            ' AClass.Execute(SqlString)
            Dim MyRst As MySql.Data.MySqlClient.MySqlDataReader
            '            MyRst = AClass.Recordset("SELECT IDENT_CURRENT ('tower_pops') AS Current_Identity")
            MyRst = AClass.Recordset(SqlString)
            MyRst.Read()
            iId = MyRst.Item("Current_Identity")
        Else
            iId = Val(Me.CurID.Value)
            SqlString = "Update tower_pops set " & _
                "pop_name='" & Me.towername.Value & "',pop_long='" & Replace(Me.rlonu.Value, "'", "`") & "',pop_long_dec=" & Me.rlon.Value & "," & _
                "pop_lat='" & Replace(Me.rlatu.Value, "'", "`") & "',pop_lat_dec=" & Me.rlat.Value & ",pop_height=" & Me.AGL.Value & "," & _
                "pop_istower=" & IIf(Me.IsTower.Checked, 1, 0) & ",pop_cost=" & Val(Me.Monthly.Value) & ",pop_contact='" & Me.Owner.Value & "'," & _
                "pop_region='" & Me.RegionList.Text & "',pop_type='" & Me.TypeList.Text & "',pop_input_long='" & Replace(Me.rlonu.Value, "'", "`") & "'," & _
                "pop_input_lat='" & Replace(Me.rlatu.Value, "'", "`") & "' where id = " & Me.CurID.Value
            AClass.Execute(SqlString)
        End If
        FillCombo(TowerList)
        ShowData(iId)
    End Sub



    Protected Sub DeleteButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles DeleteButton.Click
        Dim SqlString As String
        SqlString = "delete from tower_pops where id = " & Me.CurID.Value
        AClass.Execute(SqlString)
        FillCombo(TowerList)
        ShowData(0)
    End Sub

    Protected Sub CancelButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CancelButton.Click
        ShowData(Me.TowerList.SelectedItem.Value)
    End Sub
End Class

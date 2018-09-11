Imports MySql.Data.MySqlClient
Partial Class Users
    Inherits System.Web.UI.Page
    Dim AClass As New SqlDataClass
    Dim BClass As New MySqlDataClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not HttpContext.Current.User.IsInRole("super") Then
            Response.Redirect("default.aspx")
        End If
        If Not IsPostBack Then
            ' Users
            Dim MyRst As MySql.Data.MySqlClient.MySqlDataReader
            UserListUpdate()
            ' Regions active
            MyRst = BClass.Recordset("select pop_region from tower_pops group by pop_region")
            If MyRst.HasRows Then
                While MyRst.Read()
                    Dim MyItem As New ListItem
                    MyItem.Text = MyRst.Item("pop_region")
                    MyItem.Value = MyRst.Item("pop_region")
                    Me.RegionList.Items.Add(MyItem)
                End While
                Me.RegionList.SelectedIndex = 0
            End If

        End If

    End Sub

    Sub UserListUpdate()
        Dim MyRst As Data.SqlClient.SqlDataReader
        Dim FirstId As Long = 0
        MyRst = AClass.Recordset("select * from popuser order by username")
        UserList.Items.Clear()
        If MyRst.HasRows Then
            While MyRst.Read()
                Dim MyItem As New ListItem
                If FirstId = 0 Then FirstId = MyRst.Item("id")
                MyItem.Text = MyRst.Item("username")
                MyItem.Value = MyRst.Item("id")
                Me.UserList.Items.Add(MyItem)
            End While
            Me.UserList.SelectedIndex = 0
            ShowUser(FirstId)
        End If
    End Sub
    Sub ShowUser(ByVal id As Long)
        Dim MyRst As Data.SqlClient.SqlDataReader
        MyRst = AClass.Recordset("select * from popuser where id = " & id)
        If MyRst.HasRows Then
            MyRst.Read()
            Try
                Me.Username.Text = MyRst.Item("username")
                Me.thePassword.Text = MyRst.Item("password")
                Me.Email.Text = MyRst.Item("email")
                Me.Number.Text = MyRst.Item("partnercode")
                Me.RegionList.Text = MyRst.Item("regioncode")
                Me.Role.Text = MyRst.Item("role")
                Me.Authorised.Checked = MyRst.Item("authorised")
            Catch ex As Exception

            End Try

        End If
    End Sub


    Protected Sub UserList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles UserList.SelectedIndexChanged
        ShowUser(UserList.SelectedItem.Value)
    End Sub

    Protected Sub SaveButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles SaveButton.Click
        AClass.Execute("update popuser set password='" & thePassword.Text & "', email='" & Email.Text & "',partnercode='" & Number.Text & "', regioncode='" & RegionList.Text & "',authorised=" & IIf(Authorised.Checked, 1, 0) & ", role='" & Role.Text & "' where id=" & Me.UserList.SelectedValue)

        Dim checkvalue As String
        Dim RoleExists As Boolean = False
        Dim returnRoles As String()
        returnRoles = Roles.GetAllRoles()
        For Each checkvalue In returnRoles
            If UCase(checkvalue) = UCase(Role.Text) Then
                RoleExists = True
                Exit For
            End If
        Next
        If Not RoleExists Then
            Roles.CreateRole(Role.Text)
        End If
        returnRoles = Roles.GetRolesForUser(Username.Text)

        If returnRoles.Length = 0 Then
            Roles.AddUserToRole(Username.Text, Role.Text)
        Else
            If returnRoles(0) <> Role.Text Then
                Roles.RemoveUserFromRole(Username.Text, returnRoles(0))
                If Not Roles.IsUserInRole(Username.Text, Role.Text) Then
                    Roles.AddUserToRole(Username.Text, Role.Text)
                End If
            End If
        End If


    End Sub

    Protected Sub DeleteButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles DeleteButton.Click
        AClass.Execute("delete popuser  where id=" & Me.UserList.SelectedValue)
        UserListUpdate()
    End Sub

   
    Protected Sub Username_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Username.TextChanged

    End Sub
End Class

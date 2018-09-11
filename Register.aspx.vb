Imports MySql.Data.MySqlClient
Partial Class Register_aspx
    Inherits System.Web.UI.Page
    Dim AClass As New SqlDataClass

    Protected Sub CreatingUser()
        Dim Valid As Boolean = True
        Dim ErrStr As String = ""
        If Me.UserName.Text = "" Then ErrStr += "Username Required" & vbCrLf : Valid = False
        If Me.Password.Text = "" Then ErrStr += "Password Required" & vbCrLf : Valid = False
        If Me.Password.Text <> Me.ConfirmPassword.Text Then ErrStr += "Password Must match" & vbCrLf : Valid = False
        If Me.Email.Text = "" Then ErrStr += "Email Required" & vbCrLf : Valid = False
        Me.ResponseArea.InnerText = ErrStr
        If Valid Then
            Dim MyRst As Data.SqlClient.SqlDataReader
            MyRst = AClass.Recordset("select * from popuser where username='" & Me.UserName.Text & "' ")
            If MyRst.HasRows Then
                Me.ResponseArea.InnerText = "Failed to add new user as this user exists already."
            Else
                AClass.Execute("Insert into popuser (username,password,email,partnercode,regioncode,authorised,role) values ('" & Me.UserName.Text & "','" & Me.Password.Text & "','" & Me.Email.Text & "','" & Me.Question.Text & "','" & Me.Answer.Text & "',0,'user')")
                Me.ResponseArea.InnerText = "Created."
                ' Send email 
            End If
        End If
    End Sub


    Protected Sub RegisterUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RegisterUser.Click
        CreatingUser()
    End Sub
End Class
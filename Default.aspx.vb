Imports System.Net.nRadius.nRadius_Client


Partial Class Default_aspx
    Inherits System.Web.UI.Page

    Dim AClass As New SqlDataClass

    Public Event Authenticate As AuthenticateEventHandler
    Dim instance As Login
    Dim handler As AuthenticateEventHandler

    Sub OnAuthenticate(ByVal sender As Object, ByVal e As AuthenticateEventArgs)
        Dim Authenticated As Boolean
        instance = sender
        Authenticated = SiteSpecificAuthenticationMethod(instance.UserName, instance.Password)
        e.Authenticated = Authenticated

    End Sub

    Sub OnLogout()

    End Sub

    Sub WriteCookie(ByVal ThisUserName As String, ByVal UserRole As String)
        Dim ticket As FormsAuthenticationTicket = New FormsAuthenticationTicket(1, ThisUserName, DateTime.Now, DateTime.Now.AddMinutes(30), True, UserRole, FormsAuthentication.FormsCookiePath)
        Dim hash As String = FormsAuthentication.Encrypt(ticket)
        Dim cookie As HttpCookie = New HttpCookie(FormsAuthentication.FormsCookieName, hash)
        If ticket.IsPersistent Then cookie.Expires = ticket.Expiration
        Response.Cookies.Add(cookie)
    End Sub

    Function SiteSpecificAuthenticationMethod(ByVal UserName As String, ByVal Password As String) As Boolean

        Dim MyRst As Data.SqlClient.SqlDataReader
        MyRst = AClass.Recordset("select * from popuser where username='" & UserName & "' and password='" & Password & "'")
        If MyRst.HasRows Then
            MyRst.Read()
            If MyRst.Item("authorised") = True Then
                WriteCookie(UserName, MyRst.Item("role"))
                If Not Roles.IsUserInRole(UserName, MyRst.Item("role")) Then
                    Roles.AddUserToRole(UserName, MyRst.Item("role"))
                End If
                Headerh3.InnerText = MyRst.Item("role") & ":Authorised"
                Return True
            Else
                WriteCookie(UserName, "user")
                If Not Roles.IsUserInRole(UserName, "user") Then
                    Headerh3.InnerText = "Client: Not Authorised yet. Please authorise"
                End If
                Return True
            End If
        Else
            ' Ok So Lets try radius then
            Dim MyRadius As New System.Net.nRadius.nRadius_Client(System.Configuration.ConfigurationManager.AppSettings("radiusserver"), System.Configuration.ConfigurationManager.AppSettings("radiussecret"), UserName, Password)
            If MyRadius.Authenticate = 0 Then  ' Passed
                Headerh3.InnerText = "Radius Staff"
                WriteCookie(UserName, "user")
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Protected Sub LoginButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub LoginView1_ViewChanged(sender As Object, e As System.EventArgs) Handles LoginView1.ViewChanged

    End Sub
End Class
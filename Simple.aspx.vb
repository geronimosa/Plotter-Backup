
Partial Class Simple
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim CSM As ClientScriptManager = Page.ClientScript

        CSM.RegisterClientScriptBlock(Me.GetType, "Test", "alert('YES!');")
    End Sub
End Class

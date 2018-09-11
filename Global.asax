<%@ Application Language="VB" %>

<script runat="server">

	Sub Application_Start(ByVal sender As [Object], ByVal e As EventArgs)
		AddHandler SiteMap.SiteMapResolve, AddressOf Me.AppendQueryString
		If (Roles.RoleExists("admin") = False) Then
			Roles.CreateRole("admin")
		End If
		If (Roles.RoleExists("user") = False) Then
			Roles.CreateRole("user")
        End If
        If (Roles.RoleExists("staff") = False) Then
            Roles.CreateRole("staff")
        End If
		If (Roles.RoleExists("super") = False) Then
			Roles.CreateRole("super")
		End If
	End Sub

	Function AppendQueryString(ByVal o As [Object], ByVal e As SiteMapResolveEventArgs) As SiteMapNode
		If (Not (SiteMap.CurrentNode) Is Nothing) Then
			Dim temp As SiteMapNode
			temp = SiteMap.CurrentNode.Clone(True)
			Dim u As Uri = New Uri(e.Context.Request.Url.ToString)
			temp.Url = (temp.Url + u.Query)
			If (Not (temp.ParentNode) Is Nothing) Then
				temp.ParentNode.Url = (temp.ParentNode.Url + u.Query)
			End If
			Return temp
		Else
			Return Nothing
		End If
	End Function

</script>

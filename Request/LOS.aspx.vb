
Partial Class Request_LOS
    Inherits System.Web.UI.Page
    Private Math1, Math2 As Integer



    Protected Sub SubmitButton_Click(sender As Object, e As System.EventArgs) Handles SubmitButton.Click
        ' Validation Process
        Dim Msg(1) As String
        Msg(0) = ""
        Dim MsgCount As Integer = 0
        If Len(StreetAddress.Text) < 1 Then MsgCount += 1 : ReDim Msg(MsgCount) : Msg(MsgCount - 1) = "Street Address must be valid"
        '0° 0' 0"
        ' If Latitude.Text="" Then MsgCount += 1 : ReDim Preserve Msg(MsgCount) : Msg(MsgCount - 1) = "Full name must be valid"

        If Len(FullName.Text) < 1 Then MsgCount += 1 : ReDim Preserve Msg(MsgCount) : Msg(MsgCount - 1) = "Full name must be valid"
        If Len(ContactNumber.Text) < 10 Then MsgCount += 1 : ReDim Preserve Msg(MsgCount) : Msg(MsgCount - 1) = "Contact Number must be valid"
        If Len(Email.Text) < 2 Or InStr(Email.Text, "@") = 0 Then MsgCount += 1 : ReDim Preserve Msg(MsgCount) : Msg(MsgCount - 1) = "Email must be valid"
        If Val(Maths.Text) <> Math1 + Math2 Then MsgCount += 1 : ReDim Preserve Msg(MsgCount) : Msg(MsgCount - 1) = "Validation must be valid"
        Dim ErText As String = ""
        If MsgCount > 0 Then
            ErText = "Errors on Input:<br>"
            For i As Integer = 0 To MsgCount - 1
                ErText += Msg(i) + "<br>"
            Next
        Else
            'Save the stuff
        End If
        StatusRow.InnerHtml = ErText

    End Sub

    Protected Sub RegionList_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles RegionList.SelectedIndexChanged

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim a As Random = New Random(DateTime.Now.Millisecond)
            Math1 = a.Next(1, 10)
            Math2 = a.Next(1, 10)
            Session("Math1") = Math1
            Session("Math2") = Math2

            Dim MyItem(7) As ListItem
            MyItem(0) = New ListItem("Cape Town", "CPT")
            MyItem(1) = New ListItem("Gauteng", "GAU")
            MyItem(2) = New ListItem("Mphumalanga", "MPU")
            MyItem(3) = New ListItem("Eastern Cape", "ECP")
            MyItem(4) = New ListItem("Bloemfontein", "BFN")
            MyItem(5) = New ListItem("kwa Zulu Natal", "KZN")
            MyItem(6) = New ListItem("Other", "OTH")
            For i As Integer = 0 To UBound(MyItem) - 1
                RegionList.Items.Add(MyItem(i))
            Next
            RandomText.InnerText = String.Format("What is {0} plus {1}", Math1, Math2)
        Else
            Math1 = Session("Math1")
            Math2 = Session("Math2")

        End If
    End Sub

    Protected Sub CancelButton_Click(sender As Object, e As System.EventArgs) Handles CancelButton.Click
        RegionList.SelectedIndex = 0
        StreetAddress.Text = ""
        Email.Text = ""
        Longitude.Text = ""
        Latitude.Text = ""
        FullName.Text = ""
        ContactNumber.Text = ""
        Maths.Text = ""
    End Sub
End Class

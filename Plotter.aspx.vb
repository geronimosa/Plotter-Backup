Imports MySql.Data.MySqlClient
Partial Class Plotter
    Inherits System.Web.UI.Page
    Dim AClass As New MySqlDataClass



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then

            'If RegionList.SelectedItem.Text <> "All" Then
            '    FillCombo(TowerList, RegionList.SelectedItem.Text)
            'Else
            '    FillCombo(TowerList)
            'End If
        Else
            FillCombo(TowerList)
        End If
        If Session("copymade") = True Then
            Me.ButPaste.Enabled = True
        End If


    End Sub

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
            FillRegions(RegionList)

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
            NewItem = New ListItem("All", 0) : tc.Items.Add(NewItem)
            While MyRst.Read
                NewItem = New ListItem(MyRst.Item("pop_region").ToString)
                tc.Items.Add(NewItem)
                'MyRst.MoveNext()
            End While
            If SelectedItem > -1 Then
                tc.SelectedIndex = SelectedItem
            End If
        Catch ex As Exception
            ' RaiseEvent DataError(ex.Message)
        End Try
    End Sub


    Protected Sub RegionList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RegionList.SelectedIndexChanged
        If RegionList.SelectedItem.Text <> "All" Then
            FillCombo(TowerList, RegionList.SelectedItem.Text)
        Else
            FillCombo(TowerList)
        End If
    End Sub

    Protected Sub ana_lat_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ana_lat.TextChanged
        dec_lat.Text = ConvertGoogle(ana_lat.Text.Trim)
        CalculateStuff()
    End Sub

    Function ConvertGoogle(ByVal GoogleTime As String) As String
        Dim StringValue As String = ""
        Dim Factor, hrpos, minpos, secpos, hr, min As Int16
        Dim sec As Double
        Dim LatOrLong As String = ""

        '26°1’22.66”S 28°15’36.39”E
        Try
            If LTrim(GoogleTime) = "" Then Return "" : Exit Function
            GoogleTime = Replace(GoogleTime, "`", "'")
            GoogleTime = Replace(GoogleTime, "’", "'")
            GoogleTime = Replace(GoogleTime, Chr(148), Chr(34))
            GoogleTime = Replace(GoogleTime, Chr(248), Chr(176))
            ' 26°1’22.66”S 28°15’36.39”E
            ' degrees = Chr(248) or 176

            hrpos = InStr(GoogleTime, "°")
            minpos = InStr(GoogleTime, "'")
            secpos = InStr(GoogleTime, Chr(34))
            hr = Val(Mid(GoogleTime, 1, hrpos - 1))
            min = Val(Mid(GoogleTime, hrpos + 1, minpos - hrpos - 1))
            sec = Val(Mid(GoogleTime, minpos + 1, secpos - minpos - 1))
            If InStr(GoogleTime, "E") Then Factor = 1
            If InStr(GoogleTime, "W") Then Factor = -1
            If InStr(GoogleTime, "N") Then Factor = 1
            If InStr(GoogleTime, "S") Then Factor = -1
            StringValue = (hr + (min / 60) + (sec / 3600)) * Factor
        Catch ex As Exception
            ' RaiseEvent DataError(ex.Message)
        End Try
        Return StringValue
    End Function

    Private Sub CalculateStuff()
        Dim MyGeo As New GeoCalculator(Me.dec_lat.Text, Me.dec_long.Text, Me.lat2.Text, Me.lon2.Text)
        If MyGeo.Valid Then
            Me.TheDistance.Text = Int(MyGeo.Distance)
            Me.TheHeading.Text = Int(MyGeo.Direction) & " "
            If Me.dec_long.Text.Trim <> "" And Me.dec_lat.Text.Trim <> "" Then
                Me.FindClosest.Enabled = True
            Else
                Me.FindClosest.Enabled = False
            End If
            If Val(MyGeo.Distance) <= 120 And Val(MyGeo.Distance) > 0 Then
                Me.LinkToPlot.Enabled = True
                Me.LinkToPlot.Visible = True
                Status.Text = "Ready to Plot"
                Me.Bearing.Text = MyGeo.Direction
                Me.Reciprocal.Text = MyGeo.Reciprocal
                Me.LinkToPlot.Target = "_blank"
                Me.LinkToPlot.NavigateUrl = "Simple1.aspx?Freq=" & Me.Frequency.SelectedValue & "&lon1=" & Me.dec_long.Text & "&lon2=" & Me.lon2.Text & "&lat1=" & Me.dec_lat.Text & "&lat2=" & Me.lat2.Text & "&NameFrom=" & Me.TextBox1.Text & "&NameTo=" & Me.TowerList.SelectedItem.Text & "&h1=" & Me.TowerHeight1.Text & "&ht2=" & Me.TowerHeight2.Text & "&bearing=" & Me.Bearing.Text & "&reciprocal=" & Me.Reciprocal.Text & ""

            Else
                Me.LinkToPlot.Visible = False
                Status.Text = "Distance to the selected tower is invalid or > 120KM. Plot unavailable."
            End If
        Else
            Me.TheHeading.Text = MyGeo.Errors
            Me.LinkToPlot.Enabled = False
            Me.LinkToPlot.Visible = False
            Status.Text = "Invalid Details"
        End If
        'Session("GeoData") = MyGeo

    End Sub

    Protected Sub ana_long_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ana_long.TextChanged
        dec_long.Text = ConvertGoogle(ana_long.Text.Trim)
        CalculateStuff()
    End Sub

    Protected Sub TowerList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TowerList.SelectedIndexChanged
        Dim MyRst As MySql.Data.MySqlClient.MySqlDataReader

        Try

            MyRst = AClass.Recordset("select * from tower_pops where id=" & Me.TowerList.SelectedItem.Value & "")
            If MyRst.HasRows Then
                MyRst.Read()
                'Me.Regions.Text = MyRst.Item("pop_region").ToString.Trim
                'Me.Towertype.Text = MyRst.Item("pop_type").ToString.Trim

                lon2.Text = MyRst.Item("pop_long_dec").ToString
                lat2.Text = MyRst.Item("pop_lat_dec").ToString
                lon2dec.Text = Replace(MyRst.Item("pop_long").ToString, "`", "'")
                lat2dec.Text = Replace(MyRst.Item("pop_lat").ToString, "`", "'")

                TowerHeight2.Text = MyRst.Item("pop_height").ToString
                CalculateStuff()
                SetHyperLink()
            End If

        Catch ex As Exception
            ' RaiseEvent DataError(ex.Message)
        End Try



    End Sub

    Private Sub SetHyperLink()
        MapURL.NavigateUrl = "~/Maps.aspx?lat=" & Me.lat2.Text & "&lon=" & Me.lon2.Text & "&name=" & TowerList.SelectedItem.Text & "&ht1=" & Me.TowerHeight1.Text '& "&style=" & Me.GoogleStyle.Text

    End Sub


    Protected Sub FindClosest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FindClosest.Click
        Dim MyRst As MySql.Data.MySqlClient.MySqlDataReader
        Dim Where As String = ""
        Dim MyGeo As New GeoCalculator("", "", "", "")
        Dim TName As String = ""
        Dim Buffer As String = ""
        Dim FoundOne As Boolean = False
        Dim HRef As String = ""
        Dim MyArray(7, 0)
        MyRst = AClass.Recordset("select * from tower_pops " & Where & " order by pop_name")

        While MyRst.Read
            If MyRst.Item("pop_name").ToString.Trim <> "" Then
                MyGeo.DoCalculation(Me.dec_lat.Text, Me.dec_long.Text, MyRst.Item("pop_lat_dec"), MyRst.Item("pop_long_dec"))
                TName = MyRst.Item("pop_name").ToString.Trim
                If MyGeo.Distance <= 120 And MyGeo.Distance >= 0 Then
                    ReDim Preserve MyArray(7, UBound(MyArray, 2) + 1)
                    MyArray(0, UBound(MyArray, 2) - 1) = MyRst.Item("id")
                    MyArray(1, UBound(MyArray, 2) - 1) = MyGeo.Distance
                    MyArray(2, UBound(MyArray, 2) - 1) = TName.ToString
                    MyArray(3, UBound(MyArray, 2) - 1) = MyGeo.CalcDeclination(MyGeo.Direction)
                    MyArray(4, UBound(MyArray, 2) - 1) = MyRst.Item("pop_type").ToString
                    Buffer = ""
                    Buffer += "<A target='_blank' Href='Simple1.aspx?NameFrom=" & TextBox1.Text
                    Buffer += "&NameTo=" & MyRst.Item("pop_name")
                    Buffer += "&lon1=" & Me.dec_long.Text
                    Buffer += "&lat1=" & Me.dec_lat.Text
                    Buffer += "&lon2=" & MyRst.Item("pop_long_dec")
                    Buffer += "&lat2=" & MyRst.Item("pop_lat_dec")
                    Buffer += "&ht1=" & Me.TowerHeight1.Text
                    Buffer += "&ht2=" & MyRst.Item("pop_height")
                    Buffer += "&Reciprocal=" & MyGeo.CalcDeclination(MyGeo.Direction)
                    Buffer += "&Bearing=" & MyGeo.Direction
                    Buffer += "&Freq=" & Me.Frequency.SelectedValue

                    Buffer += "'>>>..</a>"
                    MyArray(5, UBound(MyArray, 2) - 1) = Buffer
                    MyArray(6, UBound(MyArray, 2) - 1) = MyRst.Item("pop_istower")

                    'If MyRst.Item("pop_istower") = True Then


                    FoundOne = True

                End If
            End If
        End While

        If FoundOne = False Then
            Buffer += "No towers in your immediate location"
            PlotTen.Text = "No towers"
            PlotTen.Enabled = False
        Else
            PlotTen.Enabled = True
            PlotTen.Text = "View closest 10"
            PlotTen.NavigateUrl = "~/Simple2.aspx?dec_lat=" & dec_lat.Text.Trim & "&dec_long=" & dec_long.Text.Trim & "&namefrom=" & TextBox1.Text & "&towerheight=" & Me.TowerHeight1.Text & "&frequency=" & Me.Frequency.Text
            PlotTen.Visible = True
            MyGeo.arraysort(MyArray, 1, "d")
            Dim I As Int16
            '
            Buffer = "<Table style='border-right: #ffff99 1px solid; border-top: #ffff99 1px solid; font-size: 10px; border-left: #ffff99 1px solid; width: 192px; color: gray; border-bottom: #ffff99 1px solid; font-family: Arial;  background-color: white' >"
            Buffer += "<Caption ><font style='COLOR: red'> Red points are under construction.</font></caption>"
            For I = 0 To UBound(MyArray, 2)
                If MyArray(2, I) <> "" Then
                    Buffer += "<tr>"
                    If MyArray(4, I).ToString.ToUpper = "PENDING" Then
                        Buffer += "<td align=top style='COLOR: red' nowrap>"
                    Else
                        Buffer += "<td align=top style='COLOR: blue' nowrap>"
                    End If
                    Buffer += MyArray(5, I).ToString


                    Buffer += "<b>" & String.Format("{0:n2}", MyArray(1, I)) & " km  </b>" & Replace(MyArray(2, I), "Tower", "") & "   " & Chr(177) & "<B><i>" & String.Format("{0:n0}", Val(MyArray(3, I))) & "° </i></b>"
                    If MyArray(6, I) Then
                        Buffer += " " & MyArray(4, I) & " Active"
                    Else
                        Buffer += " " & MyArray(4, I) & " Future"
                    End If

                    Buffer += "</td></tr>"

                End If

            Next
            Buffer += "</table>"
            'Buffer = mTable. '

        End If

        Me.details.InnerHtml = Buffer
    End Sub

    Protected Sub ButCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButCopy.Click
        If Me.dec_lat.Text.Trim = "" Or Me.dec_long.Text.Trim = "" Then
            Session("copymade") = False
        Else
            Session("copyname") = Me.TextBox1.Text
            Session("copyanalat") = Me.ana_lat.Text
            Session("copyanalon") = Me.ana_long.Text
            Session("copydiglat") = Me.dec_lat.Text
            Session("copydiglon") = Me.dec_long.Text
            Session("copymade") = True
        End If
        
    End Sub

    Protected Sub ButPaste_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButPaste.Click
        Me.TextBox1.Text = Session("copyname")
        Me.ana_lat.Text = Session("copyanalat")
        Me.ana_long.Text = Session("copyanalon")
        Me.dec_lat.Text = Session("copydiglat")
        Me.dec_long.Text = Session("copydiglon")
        CalculateStuff()


    End Sub

   
End Class

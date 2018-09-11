Imports ZedGraph
Imports ZedGraph.Web
Imports System.Drawing

Partial Class Admin_DrawPlot
    Inherits System.Web.UI.Page
    Dim Freq As Double = 5.8
    Dim lat1, lon1, lat2, lon2, thedistance, ht1, ht2 As Double

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        fromto.InnerHtml = "<b> From " & Request.QueryString("NameFrom") & " to " & Request.QueryString("NameTo") & "."
        Me.Title = Request.QueryString("NameFrom") & ">>" & Request.QueryString("NameTo")

    End Sub


    Public Function GetFresnelZone(ByVal DistFromStartMeters As Double, ByVal DistFromEndMeters As Double)
        Dim Wv, D1, D2, Rad As Double
        Dim SOL As Long = 299792458
        If Val(Request.QueryString("Freq")) <> 0 Then Freq = Val(Request.QueryString("Freq"))
        D1 = DistFromStartMeters * 1000
        D2 = DistFromEndMeters * 1000
        Wv = SOL / (Freq * 1000 * 1000 * 1000) ' WaveLength Freq as 5.8 or 2.4
        Rad = Math.Sqrt((Wv * D1 * D2) / (D1 + D2))
        '  wavelength is 299,792,458 m/s (Speed of light) / f - Frequency 5800,000,000
        Return Rad ' Returns Radius of the first Freznel Zone
    End Function

    'Protected Sub ZedGraphWeb1_RenderGraph(ByVal webObject As ZedGraph.Web.ZedGraphWeb, ByVal g As System.Drawing.Graphics, ByVal pane As ZedGraph.MasterPane) Handles NSPlot1.RenderGraph

    '    Dim myPane As GraphPane = pane(0)
    '    Dim Mydesert As New com.deserthail.services.ServiceMaster
    '    Dim ElevationData As New com.deserthail.services.GetElevationLineResponseObject
    '    Dim Distance As New com.deserthail.services.GetLineDistanceResponseObject
    '    Dim Response As String = ""
    '    Dim PointValue As Int32 = 0
    '    Dim list As PointPairList = New PointPairList()
    '    Dim curve = New ZedGraph.PointPairList()
    '    Dim signal As PointPairList = New PointPairList()
    '    Dim signal_neg As PointPairList = New ZedGraph.PointPairList()
    '    Dim signal_pos As PointPairList = New ZedGraph.PointPairList()
    '    Dim MinValue As Double = 0
    '    Dim MaxCurve As Double = 0
    '    Dim CurveFactor As Double = 0
    '    Dim MidPoint As Long
    '    Dim EarthRadius As Long = 6371

    '    Dim x As Double
    '    Dim text1 As New ZedGraph.TextObj
    '    Dim text2 As New ZedGraph.TextObj
    '    Dim text3 As New ZedGraph.TextObj

    '    lat1 = Val(Request.QueryString("lat1"))
    '    lat2 = Val(Request.QueryString("lat2"))
    '    lon1 = Val(Request.QueryString("lon1"))
    '    lon2 = Val(Request.QueryString("lon2"))
    '    ht1 = Val(Request.QueryString("ht1"))
    '    ht2 = Val(Request.QueryString("ht2"))


    '    Try
    '        ElevationData = Mydesert.GetElevationLine(lon1, lat1, lon2, lat2)
    '        If ElevationData.ResponseStatus = "SUCCESS" Then
    '            Dim LinePoints As Int32()
    '            LinePoints = ElevationData.ElevationLineArray
    '            MinValue = LinePoints(0)
    '            For x = 0 To UBound(LinePoints)
    '                If LinePoints(x) < MinValue Then MinValue = LinePoints(x)
    '            Next
    '            Distance = Mydesert.GetLineDistance(lon1, lat1, lon2, lat2)
    '            MaxCurve = ((Distance.LineDistance * Distance.LineDistance) / 68032) * 1000
    '            CurveFactor = MaxCurve / UBound(LinePoints)
    '            MidPoint = UBound(LinePoints) / 2

    '            thedistance = Distance.LineDistance
    '            Dim DistMarker As Double = Distance.LineDistance / UBound(LinePoints)
    '            Dim addht1, addht2 As Int32
    '            addht1 = ht1 + 25
    '            addht2 = ht2 + 25
    '            Dim EndPoint As Double = LinePoints(UBound(LinePoints)) + ht2
    '            Dim StartPoint As Double = LinePoints(0) + ht1
    '            signal.Add(0, StartPoint)
    '            signal.Add(thedistance, LinePoints(UBound(LinePoints)) + ht2)
    '            Dim Fractions As Double = (EndPoint - StartPoint) / UBound(LinePoints)
    '            Dim CurveFraction As Double = 0
    '            Dim CurvePerc As Double
    '            Dim xPOint As Double = UBound(LinePoints)
    '            Dim yPoint As Double
    '            For x = 0 To UBound(LinePoints)
    '                If x <= MidPoint Then
    '                    CurvePerc = (100 - (x / UBound(LinePoints)) * 100) * 2
    '                    CurveFraction = CurveFraction + CurveFactor
    '                    yPoint = (((xPOint) / 1) * CurveFraction) / (MidPoint)
    '                    xPOint = xPOint - 1
    '                Else
    '                    CurvePerc = ((x / UBound(LinePoints)) * 100) * 2
    '                    CurveFraction = CurveFraction - CurveFactor '- (CurveFactor * CurvePerc / 100)
    '                    yPoint = (((xPOint) / 1) * CurveFraction) / (MidPoint)
    '                    xPOint = xPOint + 1
    '                End If
    '                list.Add(x * DistMarker, LinePoints(x) + yPoint)
    '                curve.Add(x * DistMarker, MinValue + yPoint)
    '                Dim Rad As Double = GetFresnelZone(x * DistMarker, (UBound(LinePoints) - x) * DistMarker)
    '                Dim CurrentY As Double = StartPoint + (x * Fractions)
    '                signal_neg.Add(x * DistMarker, CurrentY + Rad)
    '                signal_pos.Add(x * DistMarker, CurrentY - Rad)
    '            Next x
    '            For Each PointValue In LinePoints
    '                Response += PointValue.ToString & vbCrLf
    '            Next
    '            text1 = New ZedGraph.TextObj(Request.QueryString("NameFrom"), 0.2, 0.84F, ZedGraph.CoordType.PaneFraction)
    '            text2 = New ZedGraph.TextObj(Request.QueryString("NameTo"), 0.8F, 0.84F, ZedGraph.CoordType.PaneFraction)
    '            text1.FontSpec.Size = 9
    '            text2.FontSpec.Size = 9
    '            myPane.GraphObjList.Add(text1)  ' from
    '            myPane.GraphObjList.Add(text2)  ' to
    '            Dim mySignal As ZedGraph.LineItem = myPane.AddCurve("Signal", signal, Color.Red, ZedGraph.SymbolType.Star)
    '            Dim mySignal_neg As ZedGraph.LineItem = myPane.AddCurve("Fr-", signal_neg, Color.Yellow, ZedGraph.SymbolType.None)
    '            Dim mySignal_pos As ZedGraph.LineItem = myPane.AddCurve("Fr+", signal_pos, Color.Yellow, ZedGraph.SymbolType.None)
    '            Dim earthCurve As ZedGraph.LineItem = myPane.AddCurve("EarthCurve", curve, Color.Khaki, ZedGraph.SymbolType.None)
    '            earthCurve.Line.Fill = New ZedGraph.Fill(Color.White, Color.Brown, 45.0F)
    '            Dim myCurve As ZedGraph.LineItem = myPane.AddCurve("Surface", list, Color.Blue, ZedGraph.SymbolType.None)
    '            myCurve.Line.Fill = New ZedGraph.Fill(Color.White, Color.Green, 45.0F)
    '            myCurve.Symbol.Fill = New ZedGraph.Fill(Color.White)
    '            myPane.Chart.Fill = New ZedGraph.Fill(Color.White, Color.LightGoldenrodYellow, 45.0F)
    '            myPane.Fill = New ZedGraph.Fill(Color.White, Color.FromArgb(220, 220, 255), 45.0F)
    '            pane.AxisChange(g)
    '            Specifics.InnerHtml = "<Center><b><u>Installation Data: Approx.</u></b></center><br>" & _
    '                    "Magnetic Bearing  <b> " & Format(Val(Request.QueryString("Reciprocal")), "##0") & "° </b>" & "<br>" & _
    '            "True Bearing  <b> " & Format(Val(Request.QueryString("Bearing")), "##0") & "° </b>" & "<br>" & _
    '            "A distance of exactly  <b> " & thedistance & " Km's </b>" & "<br>" & _
    '            "From " & Request.QueryString("NameFrom") & " to " & Request.QueryString("NameTo") & "<BR>" & _
    '            "Fresnal Zone calculated on " & Freq & "GHz."

    '        Else
    '        End If
    '        text3 = New ZedGraph.TextObj("NS-Wireless", 0.9F, 0.1F, ZedGraph.CoordType.PaneFraction) '
    '        text3.Location.AlignH = ZedGraph.AlignH.Right
    '        text3.Location.AlignV = ZedGraph.AlignV.Top
    '        text3.FontSpec.Border.IsVisible = True
    '        text3.FontSpec.Fill = New ZedGraph.Fill(Color.White, Color.DarkSlateBlue, 45.0F)
    '        text3.FontSpec.StringAlignment = StringAlignment.Center
    '        With myPane
    '            .Title.Text = "NS-Wireless Potential Terrain Interference Analysis"
    '            .YAxis.Title.Text = "Height (Meters)"
    '            .XAxis.Title.Text = "Distance (KM's)"
    '            .GraphObjList.Add(text3)
    '        End With
    '        Dim copyright As New ZedGraph.TextObj
    '        copyright = New ZedGraph.TextObj("Plotter - CopyRight NS-Wireless", 0.17F, 0.94F, ZedGraph.CoordType.PaneFraction)
    '        copyright.Location.AlignH = ZedGraph.AlignH.Center
    '        copyright.Location.AlignV = ZedGraph.AlignV.Top
    '        copyright.FontSpec.Border.IsVisible = True
    '        copyright.FontSpec.FontColor = Color.Black
    '        copyright.FontSpec.IsBold = True
    '        copyright.FontSpec.Fill = New ZedGraph.Fill(Color.White, Color.White, 45.0F)
    '        copyright.FontSpec.StringAlignment = StringAlignment.Center
    '        copyright.FontSpec.Size = 8
    '        copyright.FontSpec.Border.IsVisible = False
    '        myPane.GraphObjList.Add(copyright)
    '        myPane.YAxis.CrossAuto = True
    '        myPane.YAxis.AxisGap = 10
    '    Catch ex As Exception
    '    End Try

    '    Mydesert = Nothing
    '    ElevationData = Nothing

    'End Sub

    Protected Sub ZedGraphWeb2_RenderGraph(ByVal webObject As ZedGraph.Web.ZedGraphWeb, ByVal g As System.Drawing.Graphics, ByVal pane As ZedGraph.MasterPane) Handles NSCompass.RenderGraph

        Dim myPane As GraphPane = pane(0)
        Dim noOfScaleCircles As Int32 = 3
        Dim scaleDiam As Double = 30

        myPane.Fill = New ZedGraph.Fill(Color.White, Color.Goldenrod, 45.0F)
        myPane.Title.Text = "Antenna Installation Orientation"
        myPane.XAxis.Title.Text = ""
        myPane.YAxis.Title.Text = ""
        myPane.XAxis.Cross = 0
        myPane.YAxis.Cross = 0
        myPane.XAxis.MajorTic.IsAllTics = True
        myPane.XAxis.MinorTic.IsAllTics = True
        myPane.YAxis.MajorTic.IsAllTics = True
        myPane.YAxis.MinorTic.IsAllTics = True
        myPane.XAxis.Scale.IsVisible = False
        myPane.YAxis.Scale.IsVisible = False
        myPane.XAxis.Scale.Min = -35
        myPane.XAxis.Scale.Max = 35
        myPane.YAxis.Scale.Max = 35
        myPane.YAxis.Scale.Min = -35
        Dim scaleCircleList(noOfScaleCircles) As ZedGraph.RadarPointList
        Dim scaleCircle(noOfScaleCircles) As ZedGraph.LineItem
        '= New ZedGraph.LineItem(noOfScaleCircles)
        Dim delta As Double = scaleDiam / noOfScaleCircles
        Dim i, j As Int32
        For j = 0 To noOfScaleCircles
            scaleCircleList(j) = New ZedGraph.RadarPointList
            'scaleCircle(j) = New ZedGraph.LineItem()

            For i = 0 To 360
                scaleCircleList(j).Add(scaleDiam, 1)
            Next
            scaleCircle(j) = myPane.AddCurve("", scaleCircleList(j), Color.Gray, ZedGraph.SymbolType.None)
            scaleCircle(j).Line.IsSmooth = True
            scaleCircle(j).Line.SmoothTension = 0.6F
            scaleCircle(j).Line.Style = Drawing2D.DashStyle.Custom
            scaleCircle(j).Line.DashOff = 2
            scaleCircle(j).Line.DashOn = 4
            scaleDiam = scaleDiam - delta
        Next
        For j = 0 To 360
            Dim line As ZedGraph.LineObj
            Select Case j
                Case Is = Val(Request.QueryString("Bearing"))
                    line = New ZedGraph.ArrowObj(Color.Red, 0, 0, 0, scaleCircleList(0)(j).X, scaleCircleList(0)(j).Y)
                    line.Line.Style = Drawing2D.DashStyle.Solid
                    line.Line.Width = 4
                    AddInText(j, myPane, scaleCircleList(0)(j).X, scaleCircleList(0)(j).Y)
                Case Is = Val(Request.QueryString("Reciprocal"))
                    line = New ZedGraph.ArrowObj(Color.Green, 0, 0, 0, scaleCircleList(0)(j).X, scaleCircleList(0)(j).Y)
                    line.Line.Style = Drawing2D.DashStyle.Solid
                    line.Line.Width = 4
                    AddInText(j, myPane, scaleCircleList(0)(j).X, scaleCircleList(0)(j).Y)

                Case Is = 0, 90, 180, 270
                    line = New ZedGraph.ArrowObj(Color.Blue, 0, 0, 0, scaleCircleList(0)(j).X, scaleCircleList(0)(j).Y)
                    line.Line.Style = Drawing2D.DashStyle.Custom
                    line.Line.DashOn = 1
                    line.Line.DashOff = 4
                    Dim ST As String = ""
                    If j = 0 Then ST = "N"
                    If j = 90 Then ST = "E"
                    If j = 180 Then ST = "S"
                    If j = 270 Then ST = "W"
                    AddInText(ST, myPane, scaleCircleList(0)(j).X, scaleCircleList(0)(j).Y)
                Case Else
                    line = New ZedGraph.ArrowObj(Color.Transparent, 0, 0, 0, scaleCircleList(0)(j).X, scaleCircleList(0)(j).Y)
                    line.IsVisible = False
            End Select

            Dim text3 As New ZedGraph.TextObj
            text3 = New ZedGraph.TextObj("RED - True bearing", 0.25F, 0.01F, ZedGraph.CoordType.PaneFraction)
            text3.Location.AlignH = ZedGraph.AlignH.Right
            text3.Location.AlignV = ZedGraph.AlignV.Top
            text3.FontSpec.Border.IsVisible = True
            text3.FontSpec.FontColor = Color.Red
            text3.FontSpec.IsBold = True
            text3.FontSpec.Fill = New ZedGraph.Fill(Color.White, Color.LightGray, 45.0F)
            text3.FontSpec.StringAlignment = StringAlignment.Center
            myPane.GraphObjList.Add(text3)
            Dim text4 As New ZedGraph.TextObj
            text4 = New ZedGraph.TextObj("GREEN - Magnetic", 0.76F, 0.01F, ZedGraph.CoordType.PaneFraction)
            text4.Location.AlignH = ZedGraph.AlignH.Left
            text4.Location.AlignV = ZedGraph.AlignV.Top
            text4.FontSpec.Border.IsVisible = True
            text4.FontSpec.FontColor = Color.Green
            text4.FontSpec.IsBold = True
            text4.FontSpec.Fill = New ZedGraph.Fill(Color.White, Color.LightGray, 45.0F)
            text4.FontSpec.StringAlignment = StringAlignment.Center
            myPane.GraphObjList.Add(text4)
            Dim copyright As New ZedGraph.TextObj
            copyright = New ZedGraph.TextObj("Compass - CopyRight NS-Wireless", 0.75F, 0.92F, ZedGraph.CoordType.PaneFraction)
            copyright.Location.AlignH = ZedGraph.AlignH.Center
            copyright.Location.AlignV = ZedGraph.AlignV.Top
            copyright.FontSpec.Border.IsVisible = True
            copyright.FontSpec.FontColor = Color.Black
            copyright.FontSpec.IsBold = True
            copyright.FontSpec.Fill = New ZedGraph.Fill(Color.White, Color.White, 45.0F)
            copyright.FontSpec.StringAlignment = StringAlignment.Center
            myPane.GraphObjList.Add(copyright)

            myPane.GraphObjList.Add(line)
        Next

    End Sub

    Private Sub AddInText(ByVal TextString As String, ByVal myPane As ZedGraph.GraphPane, ByVal x As Int32, ByVal y As Int32)
        Dim text As ZedGraph.TextObj
        text = New ZedGraph.TextObj(" " & TextString & " ", x, y, ZedGraph.CoordType.AxisXYScale)
        text.FontSpec.Border.IsVisible = False
        text.FontSpec.Size = 16
        myPane.GraphObjList.Add(text)
    End Sub
End Class

Imports ZedGraph
Imports ZedGraph.Web
Imports System.Drawing

Partial Class Plotting
    Inherits System.Web.UI.UserControl
    Dim ElevationLineArray As Double()
    Dim Distance As Double
    Dim MaxCurve As Double = 0

    Protected Sub ZedGraphWeb1_RenderGraph(ByVal webObject As ZedGraph.Web.ZedGraphWeb, ByVal g As System.Drawing.Graphics, ByVal pane As ZedGraph.MasterPane) Handles NSPlot1.RenderGraph

        Dim myPane As GraphPane = pane(0)
        Dim Response As String = ""
        Dim PointValue As Int32 = 0
        Dim list As PointPairList = New PointPairList()
        Dim curve = New ZedGraph.PointPairList()

        Dim signal As PointPairList = New PointPairList()
        Dim signal_neg As PointPairList = New ZedGraph.PointPairList()
        Dim signal_pos As PointPairList = New ZedGraph.PointPairList()

        Dim x As Double
        Dim text1 As New ZedGraph.TextObj
        Dim text2 As New ZedGraph.TextObj
        Dim text3 As New ZedGraph.TextObj


        Dim CurveFactor As Double = 0
        Dim MidPoint As Long
        Dim EarthRadius As Long = 6371
        Dim MinValue As Double = 0
        MaxCurve = ((Distance * Distance) / 68032) * 1000
        Try
            Dim LinePoints As Double()
            LinePoints = ElevationLineArray
            MinValue = LinePoints(0)
            For x = 0 To UBound(LinePoints)
                If LinePoints(x) < MinValue Then MinValue = LinePoints(x)
            Next
            CurveFactor = MaxCurve / UBound(LinePoints)

            MidPoint = UBound(LinePoints) / 2


            Dim DistMarker As Double = Distance / UBound(LinePoints)
            '            Dim addht1, addht2 As Int32
            Dim EndPoint As Double = LinePoints(UBound(LinePoints))
            Dim StartPoint As Double = LinePoints(0)
            signal.Add(0, StartPoint)
            signal.Add(Distance, LinePoints(UBound(LinePoints)))
            Dim Fractions As Double = (EndPoint - StartPoint) / UBound(LinePoints)

            Dim xPOint As Double = UBound(LinePoints)
            Dim yPoint As Double
            Dim CurveFraction As Double = 0
            Dim CurvePerc As Double


            For x = 0 To UBound(LinePoints)
                If x <= MidPoint Then
                    CurvePerc = (100 - (x / UBound(LinePoints)) * 100) * 2
                    CurveFraction = CurveFraction + CurveFactor
                    yPoint = (((xPOint) / 1) * CurveFraction) / (MidPoint)
                    xPOint = xPOint - 1
                Else
                    CurvePerc = ((x / UBound(LinePoints)) * 100) * 2
                    CurveFraction = CurveFraction - CurveFactor '- (CurveFactor * CurvePerc / 100)
                    yPoint = (((xPOint) / 1) * CurveFraction) / (MidPoint)
                    xPOint = xPOint + 1
                End If
                list.Add(x * DistMarker, LinePoints(x) + yPoint)
                curve.Add(x * DistMarker, MinValue + yPoint)

            Next x







            For Each PointValue In LinePoints
                Response += PointValue.ToString & vbCrLf
            Next
            text1 = New ZedGraph.TextObj("From", 0.2, 0.84F, ZedGraph.CoordType.PaneFraction)
            text2 = New ZedGraph.TextObj("To", 0.8F, 0.84F, ZedGraph.CoordType.PaneFraction)
            text1.FontSpec.Size = 9
            text2.FontSpec.Size = 9
            myPane.GraphObjList.Add(text1)  ' from
            myPane.GraphObjList.Add(text2)  ' to
            Dim mySignal As ZedGraph.LineItem = myPane.AddCurve("Signal", signal, Color.Red, ZedGraph.SymbolType.Star)
            Dim mySignal_neg As ZedGraph.LineItem = myPane.AddCurve("Fr-", signal_neg, Color.Yellow, ZedGraph.SymbolType.None)
            Dim mySignal_pos As ZedGraph.LineItem = myPane.AddCurve("Fr+", signal_pos, Color.Yellow, ZedGraph.SymbolType.None)
            Dim earthCurve As ZedGraph.LineItem = myPane.AddCurve("EarthCurve", curve, Color.Khaki, ZedGraph.SymbolType.None)
            earthCurve.Line.Fill = New ZedGraph.Fill(Color.White, Color.Brown, 45.0F)
            Dim myCurve As ZedGraph.LineItem = myPane.AddCurve("Surface", list, Color.Blue, ZedGraph.SymbolType.None)
            myCurve.Line.Fill = New ZedGraph.Fill(Color.White, Color.Green, 45.0F)
            myCurve.Symbol.Fill = New ZedGraph.Fill(Color.White)
            myPane.Chart.Fill = New ZedGraph.Fill(Color.White, Color.LightGoldenrodYellow, 45.0F)
            myPane.Fill = New ZedGraph.Fill(Color.White, Color.FromArgb(220, 220, 255), 45.0F)

            text3 = New ZedGraph.TextObj("Vlocity Communications (Pty) Ltd", 0.9F, 0.1F, ZedGraph.CoordType.PaneFraction) '
            text3.Location.AlignH = ZedGraph.AlignH.Right
            text3.Location.AlignV = ZedGraph.AlignV.Top
            text3.FontSpec.Border.IsVisible = True
            text3.FontSpec.Fill = New ZedGraph.Fill(Color.White, Color.DarkSlateBlue, 45.0F)
            text3.FontSpec.StringAlignment = StringAlignment.Center
            With myPane
                .Title.Text = "Copyright Vlocity (2012)"
                .YAxis.Title.Text = "Height (Meters)"
                .XAxis.Title.Text = "Distance (KM's)"
                .GraphObjList.Add(text3)
            End With
            Dim copyright As New ZedGraph.TextObj
            copyright = New ZedGraph.TextObj("Plotter - CopyRight Vlocity Communications (Pty) Ltd", 0.17F, 0.94F, ZedGraph.CoordType.PaneFraction)
            copyright.Location.AlignH = ZedGraph.AlignH.Center
            copyright.Location.AlignV = ZedGraph.AlignV.Top
            copyright.FontSpec.Border.IsVisible = True
            copyright.FontSpec.FontColor = Color.Black
            copyright.FontSpec.IsBold = True
            copyright.FontSpec.Fill = New ZedGraph.Fill(Color.White, Color.White, 45.0F)
            copyright.FontSpec.StringAlignment = StringAlignment.Center
            copyright.FontSpec.Size = 8
            copyright.FontSpec.Border.IsVisible = False
            myPane.GraphObjList.Add(copyright)
            myPane.YAxis.CrossAuto = True
            myPane.YAxis.AxisGap = 10
        Catch ex As Exception
        End Try


    End Sub

    Private Sub AddInText(ByVal TextString As String, ByVal myPane As ZedGraph.GraphPane, ByVal x As Int32, ByVal y As Int32)
        Dim text As ZedGraph.TextObj
        text = New ZedGraph.TextObj(" " & TextString & " ", x, y, ZedGraph.CoordType.AxisXYScale)
        text.FontSpec.Border.IsVisible = False
        text.FontSpec.Size = 16
        myPane.GraphObjList.Add(text)
    End Sub
    Public Sub New()

    End Sub
    Public Sub SetupNew(ByVal theDistance As Double, ByVal LineArray As Double())
        ElevationLineArray = LineArray
        Distance = theDistance

    End Sub
End Class



Imports Microsoft.VisualBasic
Imports System

Public Class GeoCalculator
    Dim lt1, ln1, lt2, ln2, Bearing, TotalDistance As Double
    Dim isValid As Boolean = False
    Dim varError As String
    Dim AverageDeclination As Int32 = 20

    Public ReadOnly Property Distance() As String
        Get
            Return Format(TotalDistance, "0.00")
        End Get
    End Property

    Public ReadOnly Property Direction() As String
        Get
            Dim ReturnValue As Double = (Bearing)
            If ReturnValue = 0 Then ReturnValue = 360
            Return ReturnValue
        End Get
    End Property

    Public ReadOnly Property Errors() As String
        Get
            Return varError
        End Get
    End Property

    Public ReadOnly Property Valid() As Boolean
        Get
            Return isValid
        End Get

    End Property

    Public ReadOnly Property Reciprocal() As String
        Get
            Dim ReturnValue As Double
            If Bearing > 180 Then
                ReturnValue = (Bearing - 180)
            ElseIf Bearing < 0 Then
                ReturnValue = -1
            Else
                ReturnValue = (Bearing + 180)
            End If
            If ReturnValue = 0 Then ReturnValue = 360
            Return ReturnValue
        End Get
    End Property


    Private Function ToRad(ByVal Value As Double) As Double
        Dim fact As Integer
        If Value < 0 Then fact = -1 Else fact = 1
        Return Value * System.Math.PI / 180 * fact
    End Function
    Private Function ToDeg(ByVal Value As Double) As Double
        Return Value * 180 / Math.PI
    End Function
    Private Function toBrng(ByVal Value As Double) As Double
        Return (ToDeg(Value) + 360) ' % 360
    End Function

    Public Sub New(ByVal varLat1 As String, ByVal varLong1 As String, ByVal varLat2 As String, ByVal varLong2 As String)
        ''Dim R As Int32 = 6371 '; // km
        DoCalculation(varLat1, varLong1, varLat2, varLong2)

    End Sub
    Public Sub DoCalculation(ByVal varLat1 As String, ByVal varLong1 As String, ByVal varLat2 As String, ByVal varLong2 As String)

        lt1 = ToRad(Val(varLat1))
        lt2 = ToRad(Val(varLat2))
        ln1 = ToRad(Val(varLong1))
        ln2 = ToRad(Val(varLong2))
        isValid = True
        Try

            Dim brng, dbrng As Double
            Dim d As Single = Math.Sin(lt1) * Math.Sin(lt2) + Math.Cos(lt1) * Math.Cos(lt2) * Math.Cos(ln1 - ln2)
            Dim d1 As Single = Math.Acos(d)
            Dim d2 As Single = d1 * 180 * 60 / Math.PI
            TotalDistance = d2 * 1.852
            If d2 * 1.852 <= 200 And d2 * 1.852 > 0.5 Then
                'Button1.Enabled = True
            Else
                ' Button1.Enabled = False
                '  TextBox2.Text = "Distance greater than 200 KM or less than 1KM - No plot available - Wireless not an option"
            End If
            Dim sin_d1 As Double = Math.Sin(d1)
            Dim sin_lat1 As Double = Math.Sin(lt1)
            Dim cos_lat1 As Double = Math.Cos(lt1)
            Dim cos_d1 As Double = Math.Cos(d1)
            Dim sin_lat2 As Double = Math.Sin(lt2)
            Dim sin_lon1_lon2 As Double = Math.Sin(ln2 - ln1)
            Dim Sumall As Double = (sin_lat2 - sin_lat1 * cos_d1) / (sin_d1 * cos_lat1)
            Dim acos_total As Double = Math.Acos(Sumall)
            Dim pi As Double = Math.PI

            If sin_lon1_lon2 < 0 Then
                brng = acos_total
            Else
                brng = 2 * pi - acos_total
            End If
            If d2 * 1.852 <= 200 And d2 * 1.852 > 0.5 Then

            End If

            dbrng = ToDeg(brng)
            If Bearing >= 0 Then
            Else
                brng = 0
                Bearing = -1
            End If
            If dbrng > 180 Then
                dbrng = dbrng - 180
            ElseIf dbrng < 0 Then
                dbrng = -1
            Else
                dbrng = dbrng + 180
            End If
            Bearing = dbrng
            'DrawCompass(zg2, Bearing, Me.Reciprocal)

        Catch ex As Exception
            isValid = False
            varError = ex.Message
        End Try



    End Sub

    Public Function CalcDeclination(ByVal TheBearing As Double) As Double
        Dim Decl As Double
        'http://www.ngdc.noaa.gov/geomagmodels/struts/calcDeclination?minLatStr=-33.937852&minLatHemisphere=S&minLonStr=18.8941&minLonHemisphere=E&MinYear=2009&MinMonth=6&MinDay=1
        'If Bearing >= 0 And Bearing <= 180 Then
        '    Decl = TheBearing + AverageDeclination
        'Else
        '    Decl = TheBearing - AverageDeclination
        'End If
        Decl = TheBearing + AverageDeclination
        If Decl > 360 Then
            Decl = Decl - 360
        End If
        Return Decl
    End Function

    Public Function arraysort(ByVal values As Object, ByVal intSortCol As Int32, ByVal strDirection As String) As Object
        Dim i
        Dim j
        Dim value
        Dim value_j
        Dim min
        Dim max
        Dim temp
        Dim datatype
        Dim intComp
        Dim intA
        Dim intCheckIndex

        min = LBound(values, 2)
        max = UBound(values, 2)

        ' check to see what direction you want to sort.
        If LCase(strDirection) = "d" Then
            intComp = -1
        Else
            intComp = 1
        End If

        If intSortCol < 0 Or intSortCol > UBound(values, 1) Then
            arraysort = values
            Exit Function
        End If
        ' find the first item which has valid data in it to sort
        intCheckIndex = min
        While Len(Trim(values(intSortCol, intCheckIndex))) = 0 And intCheckIndex < UBound(values, 2)
            intCheckIndex = intCheckIndex + 1
        End While
        If IsDate(Trim(values(intSortCol, intCheckIndex))) Then
            datatype = 2
        Else
            If IsNumeric(Trim(values(intSortCol, intCheckIndex))) Then
                datatype = 2
            Else
                datatype = 0
            End If
        End If
        For i = min To max - 1
            value = values(intSortCol, i)
            value_j = i
            For j = i + 1 To max
                Select Case datatype
                    Case 0
                        ' See if values(j) is smaller. works with strings now.
                        If StrComp(values(intSortCol, j), value, vbTextCompare) = intComp Then
                            ' Save the new smallest value.
                            value = values(intSortCol, j)
                            value_j = j
                        End If
                    Case 1
                        If intComp = -1 Then
                            If DateDiff("s", values(intSortCol, j), value) > 0 Then
                                ' Save the new smallest value.
                                value = values(intSortCol, j)
                                value_j = j
                            End If
                        Else
                            If DateDiff("s", values(intSortCol, j), value) < 0 Then
                                ' Save the new smallest value.
                                value = values(intSortCol, j)
                                value_j = j
                            End If
                        End If
                    Case 2
                        If intComp = -1 Then
                            If CDbl(values(intSortCol, j)) < CDbl(value) Then
                                ' Save the new smallest value.
                                value = values(intSortCol, j)
                                value_j = j
                            End If
                        Else
                            If CDbl(values(intSortCol, j)) > CDbl(value) Then
                                ' Save the new smallest value.
                                value = values(intSortCol, j)
                                value_j = j
                            End If
                        End If
                End Select
            Next 'j
            If value_j <> i Then
                ' Swap items i and value_j.
                For intA = 0 To UBound(values, 1)
                    temp = values(intA, value_j)
                    values(intA, value_j) = values(intA, i)
                    values(intA, i) = temp
                Next 'intA
            End If
        Next 'i
        arraysort = values
    End Function
End Class

Imports MySql.Data.MySqlClient
Partial Class Simple2
    Inherits System.Web.UI.Page
    Dim AClass As New MySqlDataClass
    Dim MyArray(7, 0)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim TheScript As String
        Dim TheEndScript As String = ""
        Dim MyStatus As New HtmlGenericControl("div")
        Dim MyCanvas As New HtmlGenericControl("div")
        Dim MyDiv As New HtmlGenericControl("div")
        Dim MyDivPart As New HtmlGenericControl("div")

        Dim CustHeight As Int16 = Request.QueryString("towerheight")
        Dim FrezMax As Double ' = GetFresnelZone(10000, 10000)
        FrezMax = 6
        With MyStatus
            .ID = "mystatus"
            .Style.Item("width") = "100%"
            .Style.Item("height") = "20px"
            .Style.Item("color") = "yellow"
            .Attributes("align") = "center"
        End With
        MyCanvas.ID = "map_canvas"
        MyCanvas.Style.Item("width") = "800px"
        MyCanvas.Style.Item("height") = "700px"
        MyCanvas.Attributes("align") = "center"

        Me.startup.Controls.Add(MyStatus)
        Me.startup.Controls.Add(MyCanvas)
        ' Me.startup.Controls.Add(MyDiv)

        TheScript = "<script type='text/javascript'>"

        TheScript += vbCrLf & "var elevator;"
        TheScript += vbCrLf & "var thisheight;var samples;"
        TheScript += vbCrLf & "     var mlats ;"
        TheScript += vbCrLf & "     var mlons ;"
        TheScript += vbCrLf & "     var mlate;"
        TheScript += vbCrLf & "     var mlone ;"

        TheScript += vbCrLf & "var map;var timeperiod=1400;"
        TheScript += vbCrLf & "var chart=[];var readyfornext=true;"
        TheScript += vbCrLf & "var linecolors=['red','orange','yellow','green','blue','indigo','violet','white','black','grey'];"
        TheScript += vbCrLf & "var colorcode=['ff0000','ff8000','ffff00','01DF01','0101DF','8A0886','FA58F4','ffffff','000000','848484'];"
        'FF0000

        TheScript += vbCrLf & "var distance=[];"
        TheScript += vbCrLf & "var data=[];"
        TheScript += vbCrLf & "var divs=[];var towernames=[];"
        TheScript += vbCrLf & "var chartnumber=1;"
        TheScript += vbCrLf & "var infowindow = new google.maps.InfoWindow();"
        TheScript += vbCrLf & "var polyline;"
        TheScript += vbCrLf & "var thisdiv;"
        TheScript += vbCrLf & " document.getElementById('mystatus').innerHTML='<p>Contacting Google..</P><p>Please be patient - we have to delay 2 seconds between requests or we get blocked by them</p>';"

        TheScript += vbCrLf & "google.load('visualization', '1', {packages: ['columnchart']});"

        TheScript += vbCrLf & "function toRad(deg) {"
        TheScript += vbCrLf & "     return deg * Math.PI/180;"
        TheScript += vbCrLf & "}"

        TheScript += vbCrLf & "function drawPath(mystart,myend,mydiv,chnum,theight) {"
        TheScript += vbCrLf & "     // Calc Distance;"
        TheScript += vbCrLf & "     document.getElementById('mystatus').innerHTML='<p>Calculating terrain to '+towernames[chnum]+' and plotting targets.</p>';"
        TheScript += vbCrLf & "     var R = 6371;"
        TheScript += vbCrLf & "    var dLat = toRad(myend.lat()-mystart.lat());"
        TheScript += vbCrLf & "    var dLon = toRad(myend.lng()-mystart.lng());"
        TheScript += vbCrLf & "    var dLat1 = toRad(mystart.lat());"
        TheScript += vbCrLf & "    var dLat2 = toRad(myend.lat());"
        TheScript += vbCrLf & "    mlats = mystart.lat();"
        TheScript += vbCrLf & "    mlons = mystart.lng();"
        TheScript += vbCrLf & "    mlate = myend.lat();"
        TheScript += vbCrLf & "    mlone = myend.lng();"

        TheScript += vbCrLf & "     var a = Math.sin(dLat/2) * Math.sin(dLat/2) +"
        TheScript += vbCrLf & "             Math.cos(dLat1) * Math.cos(dLat1) *"
        TheScript += vbCrLf & "             Math.sin(dLon/2) * Math.sin(dLon/2);"
        TheScript += vbCrLf & "     var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));"
        TheScript += vbCrLf & "     var d = R * c;"
        TheScript += vbCrLf & "     distance[chnum]=d;"
        TheScript += vbCrLf & "     // end Calc Distance;"

        TheScript += vbCrLf & "     var path = [mystart,myend];"
        TheScript += vbCrLf & "     thisdiv=mydiv;"
        TheScript += vbCrLf & "     thisheight=theight;"
        TheScript += vbCrLf & "     samples=Math.round(((distance[chnum]*1)*1000)/30);if(samples<=254){samples=254;}"
        TheScript += vbCrLf & "     var pathRequest = {"
        TheScript += vbCrLf & "         'path': path,"
        TheScript += vbCrLf & "         'samples': 200"
        TheScript += vbCrLf & "     }"
        TheScript += vbCrLf & "     elevator.getElevationAlongPath(pathRequest, plotElevation);"
        TheScript += vbCrLf & "}"
        TheScript += vbCrLf & " "
        TheScript += vbCrLf & "function plotElevation(results, status) {"
        TheScript += vbCrLf & "   if (status == google.maps.ElevationStatus.OK) {"
        TheScript += vbCrLf & "     elevations = results;"
        TheScript += vbCrLf & "     var elevationPath = [];"
        TheScript += vbCrLf & "     for (var i = 0; i < results.length; i++) {"
        TheScript += vbCrLf & "         elevationPath.push(elevations[i].location);"
        TheScript += vbCrLf & "     }"

        TheScript += vbCrLf & "     var pathOptions = {"
        TheScript += vbCrLf & "         path: elevationPath,"
        TheScript += vbCrLf & "         strokeColor:  linecolors[chartnumber-1],"
        TheScript += vbCrLf & "         opacity: 0.4,"
        TheScript += vbCrLf & "         weight: 1,"
        TheScript += vbCrLf & "         map: map "
        TheScript += vbCrLf & "     }"
        TheScript += vbCrLf & "     polyline = new google.maps.Polyline(pathOptions);"

        TheScript += vbCrLf & "     var startline;var endline;"
        TheScript += vbCrLf & "     var options={};"
        TheScript += vbCrLf & "         options.width=800;"
        TheScript += vbCrLf & "         options.height=200;"
        TheScript += vbCrLf & "         options.legend='none';options.curveType='function';"
        TheScript += vbCrLf & "         options.titleY='Elevation (m)';"
        TheScript += vbCrLf & "         options.titleX='Distance ' + Math.round((distance[chartnumber]*1)*100)/100 + ' km  '+samples.toString()+' height plots taken (every 30 Meters)';"
        TheScript += vbCrLf & "         options.title=chartnumber+') '+towernames[chartnumber]+' height '+thisheight+' m';"
        TheScript += vbCrLf & "         options.pointSize=1;"
        TheScript += vbCrLf & "         options.lineWidth=3;"

        TheScript += vbCrLf & "         var mcolors = ['green','blue','brown','yellow','yellow'];"
        TheScript += vbCrLf & "         options.colors = mcolors;"
        TheScript += vbCrLf & "     data[chartnumber] = new google.visualization.DataTable();"
        TheScript += vbCrLf & "     data[chartnumber].addColumn('string', 'Sample');"
        TheScript += vbCrLf & "     data[chartnumber].addColumn('number', 'Elevation');"
        TheScript += vbCrLf & "     data[chartnumber].addColumn('number', 'Line');"
        TheScript += vbCrLf & "     data[chartnumber].addColumn('number', 'Curve');"
        TheScript += vbCrLf & "     data[chartnumber].addColumn('number', 'FREZ1');"
        TheScript += vbCrLf & "     data[chartnumber].addColumn('number', 'FREZ2');"
        TheScript += vbCrLf & "     var MaxCurve = ((distance[chartnumber] * distance[chartnumber]) / 68032) * 1000;"
        TheScript += vbCrLf & "     var CurveFactor = MaxCurve / results.length;"
        TheScript += vbCrLf & "     var MidPoint = results.length / 2;"
        TheScript += vbCrLf & "     var xPOint = results.length;"
        TheScript += vbCrLf & "     var yPoint=0;"
        TheScript += vbCrLf & "     var CurveFraction=0;"
        TheScript += vbCrLf & "     var CurvePerc=0;"
        TheScript += vbCrLf & "     var MinValue = elevations[0].elevation ;"

        TheScript += vbCrLf & " // determine Min Value"
        TheScript += vbCrLf & "     for (var i = 0; i < elevations.length; i++){"
        TheScript += vbCrLf & "         if (elevations[i].elevation < MinValue){MinValue = elevations[i].elevation;}"
        TheScript += vbCrLf & "     }"
        '

        TheScript += vbCrLf & "     for (var i = 0; i < results.length; i++) {"

        TheScript += vbCrLf & "       // Earths Curve"
        TheScript += vbCrLf & "         if (i <= MidPoint) {"
        TheScript += vbCrLf & "             CurvePerc = (100 - (i / results.length) * 100) * 2;"
        TheScript += vbCrLf & "             CurveFraction += CurveFactor;"
        TheScript += vbCrLf & "             yPoint = (((xPOint) / 1) * CurveFraction) / (MidPoint);"
        TheScript += vbCrLf & "             xPOint--;"
        TheScript += vbCrLf & "         } else {"
        TheScript += vbCrLf & "             CurvePerc = ((i / results.length) * 100) * 2;"
        TheScript += vbCrLf & "             CurveFraction -= CurveFactor;"
        TheScript += vbCrLf & "             yPoint = (((xPOint) / 1) * CurveFraction) / (MidPoint);"
        TheScript += vbCrLf & "             xPOint++;"
        TheScript += vbCrLf & "         }"
        TheScript += vbCrLf & "       // End Earths Curve"

        TheScript += vbCrLf & "             if (i==0){"
        TheScript += vbCrLf & "                 data[chartnumber].addRow(['', elevations[i].elevation+" & CustHeight & "+yPoint , elevations[i].elevation+" & CustHeight & "+yPoint,MinValue + yPoint,elevations[i].elevation+" & CustHeight & "+yPoint+" & FrezMax & ",elevations[i].elevation+" & CustHeight & "+yPoint-" & FrezMax & "]);"
        TheScript += vbCrLf & "             } else {"
        TheScript += vbCrLf & "                 if (i==results.length-1){"
        TheScript += vbCrLf & "                     data[chartnumber].addRow(['', elevations[i].elevation+thisheight+yPoint, elevations[i].elevation+thisheight+yPoint,MinValue + yPoint,elevations[i].elevation+thisheight+yPoint+" & FrezMax & ",elevations[i].elevation+thisheight+yPoint-" & FrezMax & "]);"
        TheScript += vbCrLf & "                 } else {"
        TheScript += vbCrLf & "                     data[chartnumber].addRow(['', elevations[i].elevation+yPoint,undefined,MinValue + yPoint,undefined,undefined]);"
        TheScript += vbCrLf & "                 }"
        TheScript += vbCrLf & "             }"
        TheScript += vbCrLf & "     }"

        TheScript += vbCrLf & "     document.getElementById(thisdiv).style.display = 'block';"
        TheScript += vbCrLf & "     document.getElementById(thisdiv).style.border = 'solid 4px '+linecolors[chartnumber-1];"
        TheScript += vbCrLf & "     chart[chartnumber] = new google.visualization.LineChart(document.getElementById(divs[chartnumber]));"
        TheScript += vbCrLf & "     chart[chartnumber].draw(data[chartnumber], options)"
        TheScript += vbCrLf & "     document.getElementById('mystatus').innerHTML='<p>Plot completed to '+towernames[chartnumber]+'. Sending '+towernames[chartnumber+1]+'</p>';"
        TheScript += vbCrLf & "      "
        TheScript += vbCrLf & "    }else{"
        TheScript += vbCrLf & "         if (status='OVER_QUERY_LIMIT'){document.getElementById(divs[chartnumber]).innerHTML='<p>Google counts the number of queries per hour.</P><p> We have exceeded the limitation.</P><p> Please try again later</p>';}"
        TheScript += vbCrLf & "         else{document.getElementById(divs[chartnumber]).innerHTML='<p>A Google error has occured : '+status+'</p>';"
        TheScript += vbCrLf & "         }"
        TheScript += vbCrLf & "         document.getElementById('mystatus').innerHTML='<p>Plot rejected by Google for '+towernames[chartnumber]+ '.</p>';"
        TheScript += vbCrLf & "         document.getElementById(thisdiv).innerHTML='<p><a href=""Simple1.aspx?NameFrom='+towernames[chartnumber]+'&NameTo=Customer&lon1='+mlons+'&lat1='+mlats+'&lon2='+mlone+'&lat2='+mlate+'&Reciprocal=125.0&Bearing=305.0&ht1='+thisheight+'&ht2=" & CustHeight & "&Freq=5.8"" target=""blank"">Try this alternative Link</a></p>';"
        TheScript += vbCrLf & "    }"
        '        TheScript += vbCrLf & "         document.getElementById(""status""+divs[chartnumber]).innerHTML='<p><a href=""Simple1.aspx?NameFrom='+towernames[chartnumber]+'&NameTo=Customer&lon1='+mlons+'&lat1='+mlats+'&lon2='+mlone+'&lat2='+mlate+'&Reciprocal=125.0&Bearing=305.0&ht1='+thisheight+'&ht2=" & CustHeight & "&Freq=5.8"" target=""blank"">Try this alternative Link</a></p>';"

        TheScript += vbCrLf & "   chartnumber++;"
        TheScript += vbCrLf & "}"

        TheScript += vbCrLf & "function initialize() {"
        TheScript += vbCrLf & "  var myOptions = {"
        TheScript += vbCrLf & "      zoom: 11,"
        TheScript += vbCrLf & "      center: posstart,"
        TheScript += vbCrLf & "      mapTypeId:  'hybrid'"
        TheScript += vbCrLf & "   };"
        TheScript += vbCrLf & "   map = new google.maps.Map(document.getElementById('map_canvas'), myOptions);"
        TheScript += vbCrLf & "   elevator = new google.maps.ElevationService();"
        TheScript += vbCrLf & "}" & vbCrLf

        ' Position 1 - Clients - posstart
        TheScript += vbCrLf & "var posstart=new google.maps.LatLng(" + Request.QueryString("dec_lat") + "," + Request.QueryString("dec_long") + ");"
        TheScript += vbCrLf & "initialize();"

        TheScript += vbCrLf & "var infowindow = new google.maps.InfoWindow({"
        TheScript += vbCrLf & "     content: '<font style=""color:blue"">" & Request.QueryString("namefrom") + "</font>',"
        TheScript += vbCrLf & "     size: new google.maps.Size(50,50)"
        TheScript += vbCrLf & "});"
        TheScript += vbCrLf & "var image = 'http://thydzik.com/thydzikGoogleMap/markerlink.php?text=T&color=5680FC';"
        TheScript += vbCrLf & "var shape = {"
        TheScript += vbCrLf & "     coord: [1, 1, 1, 20, 18, 20, 18 , 1],"
        TheScript += vbCrLf & "     type:   'poly'"
        TheScript += vbCrLf & " };"
        TheScript += vbCrLf & "var beachMarker = new google.maps.Marker({"
        TheScript += vbCrLf & "     position: posstart,"
        TheScript += vbCrLf & "     map: map,"
        TheScript += vbCrLf & "     icon: image,"
        TheScript += vbCrLf & "     shape: shape,"
        TheScript += vbCrLf & "     title: '" + Request.QueryString("namefrom") + "'"
        TheScript += vbCrLf & "});"
        TheScript += vbCrLf & "google.maps.event.addListener(beachMarker, 'click', function() {"
        TheScript += vbCrLf & "    infowindow.open(map,beachMarker);"
        TheScript += vbCrLf & "  });"


        'TheScript += vbcrlf &  "var posend=new google.maps.LatLng(" + Request.QueryString("lat2") + "," + Request.QueryString("lon2") + ");"
        TheScript += vbCrLf & "var arstart = new Array();"
        TheScript += vbCrLf & "var arend = new Array();"
        TheScript += vbCrLf & "function drawstuff(){;"
        Dim TimerPeriod As Long = 3000
        Dim TimerInterval As Long = 5000
        If NearestTen() = True Then
            Dim I As Int16
            Dim CounterI As Int16 = 1

            For I = 0 To UBound(MyArray, 2)
                If I < 11 Then
                    If MyArray(2, I) <> "" Then
                        TheScript += vbCrLf & "arstart[" & CounterI & "]=posstart;"
                        '                        TheScript += vbCrLf & "arend[" & (I + 1) & "]=posend;"
                        TheScript += vbCrLf & "arend[" & CounterI & "]=new google.maps.LatLng(" & MyArray(4, I) & "," & MyArray(5, I) & ");"
                        'TheScript += vbcrlf &  "var infowindow1 = new google.maps.InfoWindow({"
                        'TheScript += vbcrlf &  "content: '<p style=""color:blue"" >" & MyArray(2, I) & "</p>'"
                        'TheScript += vbcrlf &  "});"
                        'TheScript += vbCrLf & "var image = 'http://www.google.com/intl/en_us/mapfiles/ms/micons/blue-dot.png';"
                        TheScript += vbCrLf & "var shape = {"
                        TheScript += vbCrLf & "     coord: [1, 1, 1, 20, 18, 20, 18 , 1],"
                        TheScript += vbCrLf & "     type:   'poly'"
                        TheScript += vbCrLf & " };"
                        TheScript += vbCrLf & "towernames[" & CounterI & "]='" & MyArray(2, I) & "';"

                        TheScript += vbCrLf & "var infowindow" & (CounterI) & " = new google.maps.InfoWindow({"
                        TheScript += vbCrLf & "     content: '<font style=""color:black"">Tower is " + MyArray(2, I) + "</font>',"
                        TheScript += vbCrLf & "     size: new google.maps.Size(50,50)"
                        TheScript += vbCrLf & "});"

                        TheScript += vbCrLf & "var image = 'http://thydzik.com/thydzikGoogleMap/markerlink.php?text=" & (CounterI) & "&color='+colorcode[" & (CounterI - 1) & "];"
                        TheScript += vbCrLf & "var beachMarker" & (CounterI) & " = new google.maps.Marker({"
                        TheScript += vbCrLf & "     position: " & "arend[" & CounterI & "],"
                        TheScript += vbCrLf & "     map: map,"
                        TheScript += vbCrLf & "     icon: image,"
                        TheScript += vbCrLf & "     shape: shape,"
                        TheScript += vbCrLf & "     title: '" & MyArray(2, I) & "'"
                        TheScript += vbCrLf & "});"

                        TheScript += vbCrLf & "google.maps.event.addListener(beachMarker" & (CounterI) & ", 'click', function() {"
                        TheScript += vbCrLf & "    infowindow" & (CounterI) & ".open(map,beachMarker" & (CounterI) & ");"
                        TheScript += vbCrLf & "  });"


                        TheScript += vbCrLf & "divs[" & (CounterI) & "]='" & "customer" & CounterI & "';"
                        'TheScript += vbCrLf & "drawPath(posstart,arend[" & (CounterI) & "],'" & "customer" & CounterI & "'," & CounterI & "," & MyArray(6, I) & ")"
                        TheEndScript += vbCrLf & "setTimeout(""drawPath(posstart,arend[" & (CounterI) & "],'" & "customer" & CounterI & "'," & CounterI & "," & MyArray(6, I) & ")""," & TimerPeriod & ");"
                        'TheEndScript += vbCrLf & "if (readyfornext){readyfornext=drawPath(arstart[" & (CounterI) & "],arend[" & (CounterI) & "],'" & "customer" & CounterI & "'," & CounterI & "," & MyArray(6, I) & ")};"
                        TimerPeriod = TimerPeriod + TimerInterval
                        TheScript += vbCrLf & "//end " & (CounterI) & " ********************;"
                        TheScript += vbCrLf
                        MyDiv = New HtmlGenericControl("div")
                        MyDiv.ID = "customer" & CounterI
                        MyDiv.Style.Item("width") = "800px"
                        MyDiv.Style.Item("height") = "200px"
                        MyDiv.Style.Item("margin") = "10px"
                        MyDiv.InnerText = CounterI & ") " & MyArray(2, I) & ". If this message remains then there has been a timeout error to google. Please wait for the page to completely download, then refresh."
                        MyDivPart = New HtmlGenericControl("div")
                        With MyDivPart
                            .ID = "statuscustomer" & CounterI
                            .Style.Item("width") = "800px"
                            .Style.Item("height") = "10px"
                            .Style.Item("margin") = "10px"
                        End With

                        Me.startup.Controls.Add(MyDiv)
                        CounterI = CounterI + 1
                    End If
                End If

            Next
            TheScript += vbCrLf & "}"
            TheScript += vbCrLf & "drawstuff();" 'function docharts{var readyfornext=true;"
            TheScript += vbCrLf & TheEndScript
            TheScript += vbCrLf & "</script>"
            ClientScript.RegisterStartupScript(Request.GetType, "Startup", TheScript)

        End If


        'WriteStartScript()

        ' http://plotter.vlocity.co.za/Simple2.aspx?dec_lat=-26.154205294151907&dec_long=28.357086181640625&namefrom=Customer&towerheight=5&frequency=5.8


    End Sub

    Private Function NearestTen() As Boolean
        Dim MyRst As MySql.Data.MySqlClient.MySqlDataReader
        Dim Where As String = ""
        Dim MyGeo As New GeoCalculator("", "", "", "")
        Dim TName As String = ""
        Dim Buffer As String = ""
        Dim FoundOne As Boolean = False
        Dim HRef As String = ""
        Dim dec_lat As Double = Request.QueryString("dec_lat")
        Dim dec_long As Double = Request.QueryString("dec_long")
        Dim towerheight As Double = Request.QueryString("towerheight")
        Dim Frequency As Double = Request.QueryString("frequency")
        Dim NameFrom As String = Request.QueryString("namefrom")
        Me.Title = "Plot:" & NameFrom

        MyRst = AClass.Recordset("select * from tower_pops " & Where & " order by pop_name")
        While MyRst.Read
            If MyRst.Item("pop_name").ToString.Trim <> "" Then
                MyGeo.DoCalculation(dec_lat, dec_long, MyRst.Item("pop_lat_dec"), MyRst.Item("pop_long_dec"))
                TName = MyRst.Item("pop_name").ToString.Trim
                If MyGeo.Distance <= 100 And MyGeo.Distance >= 0 Then
                    ReDim Preserve MyArray(7, UBound(MyArray, 2) + 1)
                    MyArray(0, UBound(MyArray, 2) - 1) = MyRst.Item("id")
                    MyArray(1, UBound(MyArray, 2) - 1) = MyGeo.Distance
                    MyArray(2, UBound(MyArray, 2) - 1) = TName.ToString
                    MyArray(3, UBound(MyArray, 2) - 1) = MyGeo.CalcDeclination(MyGeo.Direction)
                    MyArray(4, UBound(MyArray, 2) - 1) = MyRst.Item("pop_lat_dec")
                    MyArray(5, UBound(MyArray, 2) - 1) = MyRst.Item("pop_long_dec")
                    MyArray(6, UBound(MyArray, 2) - 1) = MyRst.Item("pop_height")
                    FoundOne = True
                End If
            End If
        End While

        If FoundOne = False Then
            Return False
        Else
            MyGeo.arraysort(MyArray, 1, "d")
            Return True
        End If
    End Function

    'Public Function GetFresnelZone(ByVal DistFromStartMeters As Double, ByVal DistFromEndMeters As Double)
    '    Dim Wv, D1, D2, Rad As Double
    '    Dim SOL As Long = 299792458
    '    'If Val(Request.QueryString("Freq")) <> 0 Then Freq = Val(Request.QueryString("Freq"))
    '    D1 = DistFromStartMeters * 1000
    '    D2 = DistFromEndMeters * 1000
    '    Wv = SOL / (5800 * 1000 * 1000 * 1000) ' WaveLength Freq as 5.8 or 2.4
    '    Rad = Math.Sqrt((Wv * D1 * D2) / (D1 + D2))
    '    '  wavelength is 299,792,458 m/s (Speed of light) / f - Frequency 5800,000,000
    '    Return Rad ' Returns Radius of the first Freznel Zone
    'End Function


End Class

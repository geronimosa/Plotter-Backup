Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports System.Configuration
Imports System

Public Class MySqlDataClass
    Dim ConnectionOpen As Boolean = False
    Dim varLastErrorMessage As String


    Public Function CreateConnectionString() As String

        Return ConfigurationManager.ConnectionStrings("MySqlDBPLotter").ConnectionString


    End Function

    Public Function Recordset(ByVal SqlString As String, Optional ByRef LastError As String = "") As MySqlDataReader ' Data.SqlClient.SqlDataReader
        Dim sqlConn As New MySql.Data.MySqlClient.MySqlConnection(CreateConnectionString)
        sqlConn.Open()
        Dim sqlComm As New MySql.Data.MySqlClient.MySqlCommand(SqlString, sqlConn)
        Dim r As MySql.Data.MySqlClient.MySqlDataReader = sqlComm.ExecuteReader()
        Return r
    End Function

    Public Function Execute(ByVal SqlString As String) As Boolean
        Dim sqlConn As New MySql.Data.MySqlClient.MySqlConnection(CreateConnectionString)
        sqlConn.Open()
        Dim sqlComm As New MySql.Data.MySqlClient.MySqlCommand(SqlString, sqlConn)
        sqlComm.ExecuteNonQuery()
        Return True
    End Function



    Public Class ValueDescriptionPair
        Public Value As Object
        Public Description As String

        Public Sub New(ByVal NewValue As Object, ByVal NewDescription As String)
            Value = NewValue
            Description = NewDescription
        End Sub

        Public Overrides Function ToString() As String
            Return Description
        End Function
    End Class



End Class

Public Class SqlDataClass
    Dim ConnectionOpen As Boolean = False
    Dim varLastErrorMessage As String


    Public Function CreateConnectionString() As String

        Return ConfigurationManager.ConnectionStrings("VlocitySqlServer").ConnectionString

    End Function

    Public Function Recordset(ByVal SqlString As String, Optional ByRef LastError As String = "") As Data.SqlClient.SqlDataReader ' Data.SqlClient.SqlDataReader
        Dim sqlConn As New Data.SqlClient.SqlConnection(CreateConnectionString)
        sqlConn.Open()
        Dim sqlComm As New Data.SqlClient.SqlCommand(SqlString, sqlConn)
        Dim r As Data.SqlClient.SqlDataReader = sqlComm.ExecuteReader()
        Return r
    End Function

    Public Function Execute(ByVal SqlString As String) As Boolean
        Dim sqlConn As New Data.SqlClient.SqlConnection(CreateConnectionString)
        sqlConn.Open()
        Dim sqlComm As New Data.SqlClient.SqlCommand(SqlString, sqlConn)
        sqlComm.ExecuteNonQuery()
        Return True
    End Function

    Public Class ValueDescriptionPair
        Public Value As Object
        Public Description As String

        Public Sub New(ByVal NewValue As Object, ByVal NewDescription As String)
            Value = NewValue
            Description = NewDescription
        End Sub

        Public Overrides Function ToString() As String
            Return Description
        End Function
    End Class



End Class

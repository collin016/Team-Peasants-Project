Imports System.Data.OleDb

'Module Name: mdlDbutilities
'Purpose: A contrainer to hold commonly methods for database connection
'Change Log: A. Morgan 10/11/14


Module mdlDbutilities

    'Method Name: ConnectToDb
    'Purpose: To connect to a database
    'Parameter: None
    'Return: Database Connection - OleDbConnection Object
    'Change Log: A. Morgan 10//1/14
    Public strFolderPath As String = "R:\CIT\Cita450"
    Public strFileName As String = "ho1046r - 1"

    Public Function GetCsvData(ByVal strFolderPath As String, ByVal strFileName As String) As DataTable
        Dim strConnString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strFolderPath & ";Extended Properties=Text;"
        Dim conn As New OleDbConnection(strConnString)

        Try
            conn.Open()
            Dim cmd As New OleDbCommand("SELECT * FROM [" & strFileName & "]", conn)
            Dim da As New OleDbDataAdapter()
            da.SelectCommand = cmd
            Dim ds As New DataSet()
            da.Fill(ds)
            da.Dispose()
            Return ds.Tables(0)
        Catch ex As Exception

            Return Nothing
            MessageBox.Show("Error occured in Modules:mdlDbutilities. Method: GetCsvData(). Error:" _
                            & ex.Message)
        Finally
            conn.Close()
        End Try

    End Function

    'Method Name: CloseDb
    'Purpose: To disconnect from database
    'Parameter: None
    'Return: None
    'Change Log: A.Morgan 10/23/13

    Public Sub CloseDb(ByVal aConnection As OleDbConnection)
        'Error Handling
        Try
            aConnection.Close()
        Catch ex As Exception
            MessageBox.Show("Error occured in Modules:mdlDbutilities. Method: CloseDb(Object). Error:" _
                           & ex.Message)
        End Try
    End Sub
End Module

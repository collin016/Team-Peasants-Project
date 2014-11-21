Imports System.Data.OleDb
Imports System.IO

'Module Name: mdlDbutilities
'Purpose: A contrainer to hold commonly methods for database connection
'Change Log: A. Morgan 10/11/14


Module mdlDbutilities

    'Method Name: ConnectToDb
    'Purpose: To connect to a database
    'Parameter: None
    'Return: Database Connection - OleDbConnection Object
    'Change Log: A. Morgan 10//1/14
    Public strFolderPath As String = "R:\CIT\Cita450\ho1046r - 1"
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

    'Public Sub CloseDb(ByVal aConnection As OleDbConnection)
    '    'Error Handling
    '    Try
    '        aConnection.Close()
    '    Catch ex As Exception
    '        MessageBox.Show("Error occured in Modules:mdlDbutilities. Method: CloseDb(Object). Error:" _
    '                       & ex.Message)
    '    End Try
    'End Sub

    Public Function ReadCSVFile(ByVal strFilePath As String, ByVal tablename As String, ByVal delimiter As String)

        Dim ds As New DataSet

        Dim sr As New StreamReader(strFilePath)

        Dim columns As String() = sr.ReadLine().Split(delimiter.ToCharArray())

        'Add the new datatable to the dataset
        ds.Tables.Add(tablename)

        For Each col As String In columns
            Dim added As Boolean = False
            Dim _next As String = ""
            Dim i As Integer = 0

            While Not added
                'Build the column name and remove any unwanted Characters
                Dim Columnname As String = col + _next
                Columnname = Columnname.Replace("#", "")
                Columnname = Columnname.Replace("'", "")
                Columnname = Columnname.Replace("&", "")
                Columnname = Columnname.Replace("""", "")

                'See if the column already exsists 
                If Not ds.Tables(tablename).Columns.Contains(Columnname) Then
                    ds.Tables(tablename).Columns.Add(Columnname)
                    added = True
                Else
                    'If it did exsist then we increment the sequencer and try again
                    i = i + 1
                    _next = "_" + i.ToString()
                End If


            End While
        Next
        ' Read the rest of the data in the file
        Dim allData As String = sr.ReadToEnd()
        ' Split off each row at the Carriage Return / Line Feed
        ' Default line ending in most windows exports
        ' You may have to edit this to match your particular file
        ' This will work for Excel, Access etc default exports.
        Dim rows() As String = allData.Split(vbCr.ToCharArray)

        'Add each row to the dataset
        For Each rowValue As String In rows

            'Remove quotation field markers
            Dim row As String = rowValue.ToString().Replace("""", "")

            'Splut the row at the middle
            Dim items As String() = row.Split(delimiter.ToCharArray())

            'Add the item to the dataset
            ds.Tables(tablename).Rows.Add(items)

        Next
        'Clean up - Release StreamReader Resources
        sr.Close()
        sr.Dispose()

        Return ds

    End Function
End Module

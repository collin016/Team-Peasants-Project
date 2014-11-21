Imports System.Data.OleDb

'Class Name: clsStudentDA
'Purpose: A Container that holds methods and provides access 
'Change Log: 10/1/14 - A.Morgan "Update this when class changed Please" 
Public Class clsStudentDA

    Shared Function GetRecords() As DataSet
        'error Handing
        Dim dbConnection As OleDbConnection = Nothing

        Try
            'Connect to db
            dbConnection = ReadCSVFile("C:\Users\Alfred\Documents\GitHub\Team-Peasants-Project\PeasantsProjectSolution\PeasantsProject\bin\Debug\work", "Student", "ID")

            If dbConnection Is Nothing Then
                MessageBox.Show("Connection Failed!")
                Return Nothing
            End If

            Dim strQuery As String
            strQuery = "SELECT * FROM STUDENT"

            Dim dbDataAdapter As New OleDbDataAdapter

            Dim dbCommand As New OleDbCommand

            dbCommand.CommandText = strQuery
            dbCommand.Connection = dbConnection
            dbDataAdapter.SelectCommand = dbCommand

            Dim ds As New DataSet
            dbDataAdapter.Fill(ds, "STUDENT")

            'CloseDb(dbConnection)
            Return ds

        Catch ex As Exception
            MessageBox.Show("Error Occured in Class: clsStudentDA. Method: GetRecods(). Error:", ex.Message)
            Return Nothing
        Finally
            'CloseDb(dbConnection)
        End Try
    End Function
End Class

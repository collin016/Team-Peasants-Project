Imports System.Data.OleDb

Public Class Main


    Dim dt As New DataTable
    Dim filename As String = "C:\TEMP\Work.csv"
    Dim rowcount As Integer


    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(filename)
            MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
            MyReader.Delimiters = New String() {","}
            Dim firstrow As String()
            Dim currentRow As String()

            If Not MyReader.EndOfData Then
                firstrow = MyReader.ReadFields()
                For i As Integer = 0 To UBound(firstrow)
                    dt.Columns.Add(firstrow(i))
                Next
            End If
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    dt.Rows.Add(currentRow)

                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MessageBox.Show(ex.Message & vbCrLf & "Skipping")

                End Try
            End While
        End Using
        dgvStudents.DataSource = dt
        rowcount = dt.Rows.Count

    End Sub

   
   
End Class

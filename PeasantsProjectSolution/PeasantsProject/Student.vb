Imports System.Data.OleDb

Public Class Main

    Dim dtStudents As New DataTable
    Dim dtPacakges As New DataTable
    Dim csvStudents As String = "\\CISNTS1\BUSINESS\CIT\Cita450\ho1046r-1.csv"
    Dim rowcount As Integer
    Dim csvPackages As String = My.Application.Info.DirectoryPath & "\Packages.csv"

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load student table
        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(csvStudents)
            MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
            MyReader.Delimiters = New String() {","}
            Dim firstrow As String()
            Dim currentRow As String()
            If Not MyReader.EndOfData Then
                firstrow = MyReader.ReadFields()
                For i As Integer = 0 To UBound(firstrow)
                    dtStudents.Columns.Add(firstrow(i))
                Next
                dtStudents.Columns.Add("# Package(s)")
            End If
            dtStudents.Columns("# Package(s)").DefaultValue = 0

            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    dtStudents.Rows.Add(currentRow)
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MessageBox.Show(ex.Message & vbCrLf & "Skipping")
                End Try
            End While
        End Using

        ' Load package table
        Using MyReader2 As New Microsoft.VisualBasic.FileIO.TextFieldParser(csvPackages)
            MyReader2.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
            MyReader2.Delimiters = New String() {","}
            Dim firstrow As String()
            Dim currentRow As String()
            If Not MyReader2.EndOfData Then
                firstrow = MyReader2.ReadFields()
                For i As Integer = 0 To UBound(firstrow)
                    dtPacakges.Columns.Add(firstrow(i))
                Next
            End If
            dtPacakges.Columns(1).AutoIncrement = True
            dtPacakges.Columns(1).Unique = True
            dtPacakges.Columns(2).DataType = Type.GetType("System.Boolean")
            dtPacakges.Columns(3).DataType = Type.GetType("System.DateTime")

            While Not MyReader2.EndOfData
                Try
                    currentRow = MyReader2.ReadFields()
                    dtPacakges.Rows.Add(currentRow)
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MessageBox.Show(ex.Message & vbCrLf & "Skipping")
                End Try
            End While
        End Using

        dgvStudents.DataSource = dtStudents
        dgvPackage.DataSource = dtPacakges
        rowcount = dtStudents.Rows.Count
        dgvStudents.Rows(0).Selected = True

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            SaveGridDataInFile(csvPackages, dgvPackage)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dgvStudents_SelectionChanged(sender As Object, e As EventArgs) Handles dgvStudents.SelectionChanged
        If dgvStudents.SelectedRows.Count < 1 Then Return

        Dim studentIDvalue = dgvStudents.SelectedRows(0).Cells(0).Value

        dgvPackage.Rows(dgvPackage.Rows.Count - 1).Cells(0).Value = studentIDvalue
        dgvPackage.Rows(dgvPackage.Rows.Count - 1).Cells(1).Value = dgvPackage.RowCount
    End Sub

    Private Sub SaveGridDataInFile(ByVal fName As String, ByVal dgvA As DataGridView)
        ''create a new stream(file) writer based on supplied path
        'Dim fw As New IO.StreamWriter(fName, True)

        ''row controller
        'For x As Short = 0 To dgvA.Rows.Count - 1
        '    'for each cell, write the cellvalue followed by a comma
        '    For y As Short = 0 To dgvA.Columns.Count
        '        fw.Write(dgvA.Rows.Item(x).Cells(y).Value + ",")
        '    Next
        '    'ends the line
        '    fw.WriteLine()
        'Next
        ''close the file writer
        'fw.Close()

        Dim rows = From row As DataGridViewRow In dgvA.Rows.Cast(Of DataGridViewRow)() _
                   Where Not row.IsNewRow _
                   Select Array.ConvertAll(row.Cells.Cast(Of DataGridViewCell).ToArray, _
                                           Function(c) If(c.Value IsNot Nothing, c.Value.ToString, ""))

        Using sw As New IO.StreamWriter(fName, True)
            For Each r In rows
                sw.WriteLine(String.Join(",", r))
            Next
        End Using
        Process.Start(fName)
    End Sub
End Class
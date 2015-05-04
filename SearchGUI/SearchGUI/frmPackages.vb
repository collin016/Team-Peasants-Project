Public Class frmPackages

    Dim dtPackages As New DataTable
    Dim csvPackages As String = "C:\Users\morgan294\Desktop\Packages.csv"
    Dim searchedPackageID As String = ""

    Private Sub LoadPackageDGV()
        dtPackages = New DataTable
        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(csvPackages)
            MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
            MyReader.Delimiters = New String() {","}
            Dim firstrow As String()
            Dim currentRow As String()
            If Not MyReader.EndOfData Then
                firstrow = MyReader.ReadFields()
                For i As Integer = 0 To UBound(firstrow)
                    ' Create row for column headers from first row of CSV
                    dtPackages.Columns.Add(firstrow(i))
                Next
            End If
            dtPackages.Columns(0).ReadOnly = True

            ' Overweight? properties
            dtPackages.Columns(2).DataType = Type.GetType("System.Boolean")
            dtPackages.Columns(2).DefaultValue = False

            ' Arrival Time properties
            dtPackages.Columns(3).DataType = Type.GetType("System.DateTime")

            ' Is Picked Up? properties
            dtPackages.Columns(5).DataType = Type.GetType("System.Boolean")
            dtPackages.Columns(5).DefaultValue = False


            ' Add each row from CSV into dgvPackages
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()

                    If (txtSearch.Text <> "M") Then
                        ' Adds current row if part of textbox matches record
                        Dim lastName As String = txtSearch.Text & "*"
                        If (currentRow(0) Like lastName) Then
                            dtPackages.Rows.Add(currentRow)
                        End If
                    Else
                        dtPackages.Rows.Add(currentRow)
                    End If

                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MessageBox.Show(ex.Message & vbCrLf & "Skipping")
                End Try
            End While
        End Using
        dgvPackage.DataSource = dtPackages
    End Sub

    Private Sub frmPackages_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ManualM()
        LoadPackageDGV()

    End Sub

    'key restrctions to numbers and backspacing
    Private Sub TxtPStof_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
        Handles txtSearch.KeyPress
        'if character
        If e.KeyChar <> ControlChars.Back Then
            'EDIT: Limit string length (MUCH BETTER NOW, EH?!)
            If txtSearch.Text.Length > 8 Then
                txtSearch.Text = txtSearch.Text.Remove(8)
            End If
            e.Handled = Not (Char.IsDigit(e.KeyChar))
        End If
    End Sub

    'adds an M to the text box
    Private Sub ManualM()
        'if there isnt't anything in the textbox...
        If txtSearch.Text = "" Then
            'ADD MMMMMMMM!
            'CORRECTION: "ADD TTTTTTTT!"
            txtSearch.Text = "M"
        End If

        'focus to textbox
        txtSearch.Focus()
        'unhighlight and go to the end of the text
        txtSearch.SelectionStart = txtSearch.Text.Length
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        'trigger add m code
        ManualM()
        LoadPackageDGV()
    End Sub

    'btn search
    Private Sub btnSearch_Click(sender As Object, e As EventArgs)
        'SEARCH!!!
        showSearchResults()
        LoadPackageDGV()
    End Sub

    Private Sub showSearchResults()
        'Collect all the records
        Dim records() As String = System.IO.File.ReadAllLines(csvPackages)
        Dim found As Boolean = False
        searchedPackageID = ""
        'Iterate a for loop statement to extract "T" numbers of students...
        For index As Integer = 0 To (records.Length - 1)
            Dim record() As String = records(index).Split(",")

            'Validate whether the record matches the search criterion
            If txtSearch.Text = record(0) Then
                found = True
                'MessageBox.Show("ID: " + record(0) + vbNewLine + _
                '                "Name: " + record(2).Trim().TrimEnd("""") + " " + _
                '                           record(1).Trim().TrimStart("""") + vbNewLine + _
                '                "Dorm: " + record(3) + vbNewLine + _
                '                "Room: " + record(4) + vbNewLine + _
                '                "Phone: " + phone + vbNewLine + _
                '                "Email: " + record(6), "Record Found!")
                searchedPackageID = record(0)
                Exit For
            End If
        Next

        'If nothing has been found
        If found = False Then
            MessageBox.Show("No package with the ID, " + txtSearch.Text + ", has been found!")
        End If
    End Sub
End Class
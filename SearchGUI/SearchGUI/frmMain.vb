﻿Public Class frmMain

    'private attributes
    Private maxLength As Integer = 8

    'globals
    Dim dtStudents As New DataTable
    Dim dtPackages As New DataTable
    Dim csvStudents As String = "\\CISNTS1\BUSINESS\CIT\Cita450\ho1046r-1.csv"
    Dim rowcount As Integer
    Dim csvPackages As String = My.Application.Info.DirectoryPath & "\Packages.csv"
    Dim searchedStudentID As String = ""

    'help & instructions with example
    Private Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        MessageBox.Show("To manage student packages please enter that students M# and press the search button." & vbNewLine & vbNewLine &
                        "Example: ""M13462752""", "Search Help")
        Focusing()
    End Sub

    'key restrctions to numbers and backspacing
    Private Sub TxtPStof_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
        Handles txtSearch.KeyPress
        'if character
        If e.KeyChar <> ControlChars.Back Then
            'EDIT: Limit string length (MUCH BETTER NOW, EH?!)
            If txtSearch.Text.Length > maxLength Then
                txtSearch.Text = txtSearch.Text.Remove(maxLength)
            End If
            e.Handled = Not (Char.IsDigit(e.KeyChar))
        End If
    End Sub

    'close program click
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'close the program
        Me.Close()
    End Sub

    'load frm
    Private Sub frmSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'enter m
        ManualM()
        LoadPackageDGV()
        LoadStudentDGV()
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
        Focusing()
    End Sub

    Private Sub Focusing()
        'focus to textbox
        txtSearch.Focus()
        'unhighlight and go to the end of the text
        txtSearch.SelectionStart = txtSearch.Text.Length
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        'trigger add m code
        ManualM()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        'clear
        txtSearch.Text = ""
        txtLastName.Text = ""
        'it auto triggers manualM() because of txtchange
        'focus to textbox
        searchedStudentID = ""
        Focusing()
        LoadStudentDGV()
    End Sub

    'btn search
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        'focus to textbox
        Focusing()
        'SEARCH!!!
        showSearchResults()
        LoadStudentDGV()
    End Sub

    Private Sub showSearchResults()
        'Collect all the records
        Dim records() As String = System.IO.File.ReadAllLines(csvStudents)
        Dim found As Boolean = False
        searchedStudentID = ""
        'Iterate a for loop statement to extract "T" numbers of students...
        For index As Integer = 0 To (records.Length - 1)
            Dim record() As String = records(index).Split(",")

            'Validate whether phone info is available
            Dim phone As String = "Not listed"
            If Not record(5) = "" Then
                phone = record(5)
            End If

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
                searchedStudentID = record(0)
                Exit For
            End If
        Next

        'If nothing has been found
        If found = False Then
            MessageBox.Show("No student with the ID, " + txtSearch.Text + ", has been found!")
        End If
    End Sub

    Private Sub LoadStudentDGV()
        dtStudents = New DataTable
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

                    ' Checks if student ID has been searched
                    If (searchedStudentID <> "") Then
                        ' Adds current row if the ID fields match
                        If (currentRow(0) = searchedStudentID) Then
                            dtStudents.Rows.Add(currentRow)
                        End If

                        ' Checks if text has been entered into Last Name textbox
                    ElseIf (txtLastName.Text <> "") Then
                        ' Adds current row if part of textbox matches record
                        Dim lastName As String = txtLastName.Text & "*"
                        If (currentRow(1).ToLower Like lastName.ToLower) Then
                            dtStudents.Rows.Add(currentRow)
                        End If

                        ' Display all students
                    Else
                        dtStudents.Rows.Add(currentRow)
                    End If
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MessageBox.Show(ex.Message & vbCrLf & "Skipping")
                End Try
            End While
        End Using

        dgvStudents.DataSource = dtStudents
        rowcount = dtStudents.Rows.Count
        If dgvStudents.Rows.Count > 0 Then
            dgvStudents.Rows(0).Selected = True
        End If
    End Sub

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
            'dtPacakges.Columns(0).ReadOnly = True

            ' Overweight? properties
            dtPackages.Columns(2).DataType = Type.GetType("System.Boolean")
            dtPackages.Columns(2).DefaultValue = False

            ' Arrival Time properties
            dtPackages.Columns(3).DataType = Type.GetType("System.DateTime")

            ' Is Picked Up? properties
            dtPackages.Columns(5).DataType = Type.GetType("System.Boolean")
            dtPackages.Columns(5).DefaultValue = False

            ' Add each row from CSV into dgvPackagers
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    ' Display only if student has not picked up package
                    If (currentRow(5) = False) Then
                        ' Ensure students is selected
                        If (dgvStudents.SelectedRows.Count >= 1) Then
                            ' Only add to dgvPackages if ID fields match (only add packages for selected student)
                            If (currentRow(0) = dgvStudents.SelectedRows(0).Cells(0).Value) Then
                                dtPackages.Rows.Add(currentRow)
                            End If
                        Else
                            dtPackages.Rows.Add(currentRow)
                        End If
                    End If
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MessageBox.Show(ex.Message & vbCrLf & "Skipping")
                End Try
            End While
        End Using
        dgvPackage.DataSource = dtPackages
        'dgvPackage.Columns(0).Visible = False

        'Dim studentIDvalue = dgvStudents.SelectedRows(0).Cells(0).Value
        'dgvPackage.Rows(dtPacakges.Rows.Count - 1).Cells(0).Value = studentIDvalue
        'dgvPackage.Rows(dtPacakges.Rows.Count - 1).Cells(1).Value = dtPacakges.Rows.Count
    End Sub

    Private Sub SaveGridDataInFile(ByVal fName As String, ByVal dgvA As DataGridView)
        Try
            ' Store elements of selected row in an array
            Dim rows = From row As DataGridViewRow In dgvA.Rows.Cast(Of DataGridViewRow)() _
                   Where row.Selected = True _
                   Select Array.ConvertAll(row.Cells.Cast(Of DataGridViewCell).ToArray, _
                                           Function(c) If(c.Value IsNot Nothing, c.Value.ToString, ""))

            ' Open StreamWriter to edit csv("fName")
            Using sw As New IO.StreamWriter(fName, True)
                For Each r In rows
                    ' Add array to csv as new line using ", " as seperator
                    sw.WriteLine(String.Join(", ", r))
                Next
            End Using
            ' Open csv in notepad for debugging purposes
            Process.Start(fName)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub RemoveGridDataInFile(ByVal fName As String, ByVal dgvA As DataGridView)
        Try
            ' Store elements of selected row in an array
            Dim rows = From row As DataGridViewRow In dgvA.Rows.Cast(Of DataGridViewRow)() _
                   Where row.Selected = True _
                   Select Array.ConvertAll(row.Cells.Cast(Of DataGridViewCell).ToArray, _
                                           Function(c) If(c.Value IsNot Nothing, c.Value.ToString, ""))

            ' Open StreamWriter to edit csv("fName")
            Using sw As New IO.StreamWriter(fName, True)
                For Each r In rows
                    ' If "Package Picked Up?" is "False"
                    If r(5).Contains("False") Then
                        ' Create Yes/No validation message box
                        Dim result As Integer = MessageBox.Show("Are you sure you want to delete package #" & r(1) & "?", _
                                                                "Delete Package", MessageBoxButtons.YesNo)

                        If result = DialogResult.Yes Then
                            ' Change "Picked Up Package?" valuse to "True" to make not visible in dgvPackages
                            r(5) = r(5).Replace("False", "True")
                            ' Open csv in notepad for debugging purposes
                            Process.Start(fName)
                        End If

                        If result = DialogResult.No Then

                        End If
                    End If
                Next
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub dgvStudents_SelectionChanged(sender As Object, e As EventArgs) Handles dgvStudents.SelectionChanged
        If dgvStudents.SelectedRows.Count < 1 Then Return

        ' Refresh dgvPackage if row is selected in dgvStudents
        LoadPackageDGV()
        dgvPackage.Rows(dgvPackage.Rows.Count - 1).Cells(0).Value = dgvStudents.SelectedRows(0).Cells(0).Value
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If (dgvPackage.SelectedRows.Count >= 1) Then
                ' Add selected package to csv
                SaveGridDataInFile(csvPackages, dgvPackage)
                ' Give feedback
                MessageBox.Show("Package #" & dgvPackage.SelectedRows(0).Cells(1).Value & _
                    " has been added to " & dgvStudents.SelectedRows(0).Cells(1).Value)
            Else
                ' Give feedback if no package selected
                MessageBox.Show("Please select a package.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    ' Dynamically update Student DGV
    Private Sub txtLastName_TextChanged(sender As Object, e As EventArgs) Handles txtLastName.TextChanged
        LoadStudentDGV()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If (dgvPackage.SelectedRows.Count >= 1) Then
                ' Remove package if row is selected
                RemoveGridDataInFile(csvPackages, dgvPackage)
            Else
                ' Give feedback if no package selected
                MessageBox.Show("Please select a package.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class

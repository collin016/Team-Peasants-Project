Public Class frmSearch

    'private attributes
    Private maxLength As Integer = 8

    'help & instructions with example
    Private Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        MessageBox.Show("To manage student packages please enter that students M# and press the search button." & vbNewLine & vbNewLine &
                        "Example: ""M13462752""", "Search Help")
        Focusing()
    End Sub

    'key restrctions to numbers and backspacing
    Private Sub TxtPStof_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
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
    End Sub

    'adds an M to the text box
    Private Sub ManualM()
        'if there isnt't anything in the textbox...
        If txtSearch.Text = "" Then
            'ADD MMMMMMMM!
            'CORRECTION: "ADD TTTTTTTT!"
            txtSearch.Text = "T"
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
        'it auto triggers manualM() because of txtchange
        'focus to textbox
        Focusing()
    End Sub

    'btn search
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        'focus to textbox
        Focusing()
        'SEARCH!!!
        showSearchResults()
    End Sub

    Private Sub showSearchResults()
        'Collect all the records
        Dim records() As String = System.IO.File.ReadAllLines("students.csv")
        Dim found As Boolean = False
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
                MessageBox.Show("ID: " + record(0) + vbNewLine + _
                                "Name: " + record(2).Trim().TrimEnd("""") + " " + _
                                           record(1).Trim().TrimStart("""") + vbNewLine + _
                                "Dorm: " + record(3) + vbNewLine + _
                                "Room: " + record(4) + vbNewLine + _
                                "Phone: " + phone + vbNewLine + _
                                "Email: " + record(6), "Record Found!")
                Exit For
            End If
        Next

        'If nothing has been found
        If found = False Then
            MessageBox.Show("No student with the ID, " + txtSearch.Text + ", has been found!")
        End If
    End Sub
End Class

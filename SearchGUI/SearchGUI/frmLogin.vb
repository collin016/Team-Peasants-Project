Public Class frmLogin

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'mailBox.BackColor = Color.Transparent
    End Sub


    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Login()
    End Sub


    'check to see if enter key is pressed
    Private Sub txtUserName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUserName.KeyDown
        'login check on enter key pressed
        If e.KeyCode = Keys.Enter Then
            Login()
        End If

    End Sub
    Private Sub txtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyDown
        'login check on enter key pressed
        If e.KeyCode = Keys.Enter Then
            Login()
        End If
    End Sub


    Private Sub Login()

        'if username is not null value
        If txtUserName.Text IsNot "" Then
            'if password is not null
            If txtPassword.Text IsNot "" Then
                If ValidationCheck() Then
                    GoToForm()
                End If
            Else 'else for null value
                ErrorProvider1.SetError(Me.txtPassword, "Please provide a password.")
                statusStrip.Text = "Please provide a password."
            End If 'If txtPassword.Text IsNot "" Then

        Else 'else for null value
        ErrorProvider1.SetError(Me.txtUserName, "Please provide a username.")
        statusStrip.Text = "Please provide a username."
        End If 'txtUserName.Text IsNot "" Then

    End Sub

    Private Function ValidationCheck() As Boolean
        'if password is correct
        If txtPassword.Text = "Password1" And _
            txtUserName.Text.ToLower Like "admin" Then
            'txtUserName.Text.IndexOf("Admin", 0, StringComparison.CurrentCultureIgnoreCase) > -1
            'clear the error
            statusStrip.Text = ""
            Return True
        Else
            statusStrip.Text = "Incorrect username or password."
            Return False
        End If
    End Function

    Private Sub GoToForm()
        'hide current form
        Me.Hide()
        frmMain.Show()
        'new form show
        'MsgBox("Login was successful. THIS MESSAGE BOX MUST BE REPLACED FOR SHOWING THE NEW CONNECTING FORM. SEE GOTOFORM() in frmLogin")
    End Sub

End Class

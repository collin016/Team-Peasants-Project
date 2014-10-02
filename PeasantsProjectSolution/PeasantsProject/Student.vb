Imports System.Data.OleDb

Public Class Main

    Private dsStudent As New DataSet



    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'You will need to change the file desitination to read from the right place
        For Each line As String In System.IO.File.ReadAllLines("C:\Users\Alfred\Documents\GitHub\Team-Peasants-Project\PeasantsProjectSolution\PeasantsProject\bin\Debug\work.csv")
            dgvStudents.Rows.Add(line.Split(","))
        Next
    End Sub

    Public Sub DisplayRecord(ByVal aIndex As Integer)
        
    End Sub

End Class

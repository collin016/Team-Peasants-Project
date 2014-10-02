<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.dgvStudents = New System.Windows.Forms.DataGridView()
        Me.Col1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvStudents, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvStudents
        '
        Me.dgvStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvStudents.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col1, Me.col2})
        Me.dgvStudents.Location = New System.Drawing.Point(12, 42)
        Me.dgvStudents.Name = "dgvStudents"
        Me.dgvStudents.RowTemplate.Height = 24
        Me.dgvStudents.Size = New System.Drawing.Size(381, 150)
        Me.dgvStudents.TabIndex = 0
        '
        'Col1
        '
        Me.Col1.HeaderText = "ID"
        Me.Col1.Name = "Col1"
        '
        'col2
        '
        Me.col2.HeaderText = "Name"
        Me.col2.Name = "col2"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(455, 253)
        Me.Controls.Add(Me.dgvStudents)
        Me.Name = "Main"
        Me.Text = "Form1"
        CType(Me.dgvStudents, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvStudents As System.Windows.Forms.DataGridView
    Friend WithEvents Col1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2 As System.Windows.Forms.DataGridViewTextBoxColumn

End Class

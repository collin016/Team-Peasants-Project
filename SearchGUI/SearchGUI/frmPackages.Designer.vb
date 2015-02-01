<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPackages
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
        Me.dgvPackage = New System.Windows.Forms.DataGridView()
        Me.lblMNumber = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        CType(Me.dgvPackage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvPackage
        '
        Me.dgvPackage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPackage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPackage.Location = New System.Drawing.Point(12, 100)
        Me.dgvPackage.Name = "dgvPackage"
        Me.dgvPackage.ReadOnly = True
        Me.dgvPackage.RowTemplate.Height = 24
        Me.dgvPackage.Size = New System.Drawing.Size(822, 342)
        Me.dgvPackage.TabIndex = 0
        '
        'lblMNumber
        '
        Me.lblMNumber.AutoSize = True
        Me.lblMNumber.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.lblMNumber.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMNumber.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMNumber.Location = New System.Drawing.Point(331, 37)
        Me.lblMNumber.Name = "lblMNumber"
        Me.lblMNumber.Size = New System.Drawing.Size(46, 29)
        Me.lblMNumber.TabIndex = 2
        Me.lblMNumber.Text = "M#"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(384, 40)
        Me.txtSearch.MaxLength = 320
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(131, 22)
        Me.txtSearch.TabIndex = 3
        '
        'frmPackages
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(846, 454)
        Me.Controls.Add(Me.lblMNumber)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.dgvPackage)
        Me.Name = "frmPackages"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Packages"
        CType(Me.dgvPackage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvPackage As System.Windows.Forms.DataGridView
    Friend WithEvents lblMNumber As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
End Class

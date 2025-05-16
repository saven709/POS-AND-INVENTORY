<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBackUp
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
        Me.components = New System.ComponentModel.Container()
        Me.GunaElipse1 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.BtnBackup = New Bunifu.Framework.UI.BunifuFlatButton()
        Me.BtnRestore = New Bunifu.Framework.UI.BunifuFlatButton()
        Me.GunaElipse2 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GunaElipse3 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.GunaElipse4 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GunaElipse1
        '
        Me.GunaElipse1.Radius = 15
        Me.GunaElipse1.TargetControl = Me
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel3.Controls.Add(Me.BtnBackup)
        Me.Panel3.Controls.Add(Me.BtnRestore)
        Me.Panel3.Location = New System.Drawing.Point(12, 28)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1170, 750)
        Me.Panel3.TabIndex = 30
        '
        'BtnBackup
        '
        Me.BtnBackup.Activecolor = System.Drawing.Color.FromArgb(CType(CType(167, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(119, Byte), Integer))
        Me.BtnBackup.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(199, Byte), Integer), CType(CType(183, Byte), Integer))
        Me.BtnBackup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnBackup.BorderRadius = 0
        Me.BtnBackup.ButtonText = "BACKUP"
        Me.BtnBackup.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnBackup.DisabledColor = System.Drawing.Color.Gray
        Me.BtnBackup.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnBackup.Iconcolor = System.Drawing.Color.Transparent
        Me.BtnBackup.Iconimage = Global.POSINVENTORY.My.Resources.Resources.folder
        Me.BtnBackup.Iconimage_right = Nothing
        Me.BtnBackup.Iconimage_right_Selected = Nothing
        Me.BtnBackup.Iconimage_Selected = Nothing
        Me.BtnBackup.IconMarginLeft = 0
        Me.BtnBackup.IconMarginRight = 0
        Me.BtnBackup.IconRightVisible = True
        Me.BtnBackup.IconRightZoom = 0R
        Me.BtnBackup.IconVisible = True
        Me.BtnBackup.IconZoom = 70.0R
        Me.BtnBackup.IsTab = False
        Me.BtnBackup.Location = New System.Drawing.Point(345, 183)
        Me.BtnBackup.Margin = New System.Windows.Forms.Padding(14, 13, 14, 13)
        Me.BtnBackup.Name = "BtnBackup"
        Me.BtnBackup.Normalcolor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(199, Byte), Integer), CType(CType(183, Byte), Integer))
        Me.BtnBackup.OnHovercolor = System.Drawing.Color.FromArgb(CType(CType(167, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(119, Byte), Integer))
        Me.BtnBackup.OnHoverTextColor = System.Drawing.Color.White
        Me.BtnBackup.selected = False
        Me.BtnBackup.Size = New System.Drawing.Size(463, 147)
        Me.BtnBackup.TabIndex = 33
        Me.BtnBackup.Text = "BACKUP"
        Me.BtnBackup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.BtnBackup.Textcolor = System.Drawing.SystemColors.ControlText
        Me.BtnBackup.TextFont = New System.Drawing.Font("Poppins", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'BtnRestore
        '
        Me.BtnRestore.Activecolor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.BtnRestore.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(161, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.BtnRestore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnRestore.BorderRadius = 0
        Me.BtnRestore.ButtonText = "RESTORE"
        Me.BtnRestore.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnRestore.DisabledColor = System.Drawing.Color.Gray
        Me.BtnRestore.Font = New System.Drawing.Font("Poppins", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnRestore.Iconcolor = System.Drawing.Color.Transparent
        Me.BtnRestore.Iconimage = Global.POSINVENTORY.My.Resources.Resources.history
        Me.BtnRestore.Iconimage_right = Nothing
        Me.BtnRestore.Iconimage_right_Selected = Nothing
        Me.BtnRestore.Iconimage_Selected = Nothing
        Me.BtnRestore.IconMarginLeft = 0
        Me.BtnRestore.IconMarginRight = 0
        Me.BtnRestore.IconRightVisible = True
        Me.BtnRestore.IconRightZoom = 0R
        Me.BtnRestore.IconVisible = True
        Me.BtnRestore.IconZoom = 70.0R
        Me.BtnRestore.IsTab = False
        Me.BtnRestore.Location = New System.Drawing.Point(345, 428)
        Me.BtnRestore.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnRestore.Name = "BtnRestore"
        Me.BtnRestore.Normalcolor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(161, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.BtnRestore.OnHovercolor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.BtnRestore.OnHoverTextColor = System.Drawing.Color.White
        Me.BtnRestore.selected = False
        Me.BtnRestore.Size = New System.Drawing.Size(463, 147)
        Me.BtnRestore.TabIndex = 32
        Me.BtnRestore.Text = "RESTORE"
        Me.BtnRestore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.BtnRestore.Textcolor = System.Drawing.SystemColors.ControlText
        Me.BtnRestore.TextFont = New System.Drawing.Font("Poppins", 26.25!, System.Drawing.FontStyle.Bold)
        '
        'GunaElipse2
        '
        Me.GunaElipse2.Radius = 15
        Me.GunaElipse2.TargetControl = Me.Panel3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Poppins Medium", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(21, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(193, 19)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "BACKUP/RESTORE DATABASE"
        '
        'GunaElipse3
        '
        Me.GunaElipse3.Radius = 15
        Me.GunaElipse3.TargetControl = Me.BtnBackup
        '
        'GunaElipse4
        '
        Me.GunaElipse4.Radius = 15
        Me.GunaElipse4.TargetControl = Me.BtnRestore
        '
        'frmBackUp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(201, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1200, 790)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmBackUp"
        Me.Text = "frmBackUp"
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GunaElipse1 As Guna.UI.WinForms.GunaElipse
    Friend WithEvents Panel3 As Panel
    Friend WithEvents BtnRestore As Bunifu.Framework.UI.BunifuFlatButton
    Friend WithEvents GunaElipse2 As Guna.UI.WinForms.GunaElipse
    Private WithEvents Label3 As Label
    Friend WithEvents BtnBackup As Bunifu.Framework.UI.BunifuFlatButton
    Friend WithEvents GunaElipse3 As Guna.UI.WinForms.GunaElipse
    Friend WithEvents GunaElipse4 As Guna.UI.WinForms.GunaElipse
End Class

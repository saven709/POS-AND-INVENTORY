<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCashierChangepassword
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btn_save = New System.Windows.Forms.Button()
        Me.GunaControlBox1 = New Guna.UI.WinForms.GunaControlBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_Newpassword = New System.Windows.Forms.TextBox()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_Currentpassword = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'GunaElipse1
        '
        Me.GunaElipse1.Radius = 20
        Me.GunaElipse1.TargetControl = Me
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(79, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(-9, -1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(34, 362)
        Me.Panel1.TabIndex = 72
        '
        'btn_save
        '
        Me.btn_save.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.btn_save.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_save.FlatAppearance.BorderSize = 0
        Me.btn_save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue
        Me.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_save.Font = New System.Drawing.Font("Poppins", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_save.ForeColor = System.Drawing.Color.White
        Me.btn_save.Location = New System.Drawing.Point(171, 302)
        Me.btn_save.Name = "btn_save"
        Me.btn_save.Size = New System.Drawing.Size(97, 30)
        Me.btn_save.TabIndex = 71
        Me.btn_save.Text = "SAVE"
        Me.btn_save.UseVisualStyleBackColor = False
        '
        'GunaControlBox1
        '
        Me.GunaControlBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GunaControlBox1.AnimationHoverSpeed = 0.07!
        Me.GunaControlBox1.AnimationSpeed = 0.03!
        Me.GunaControlBox1.ControlBoxTheme = Guna.UI.WinForms.FormControlBoxTheme.Custom
        Me.GunaControlBox1.IconColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GunaControlBox1.IconSize = 15.0!
        Me.GunaControlBox1.Location = New System.Drawing.Point(419, 3)
        Me.GunaControlBox1.Name = "GunaControlBox1"
        Me.GunaControlBox1.OnHoverBackColor = System.Drawing.Color.Maroon
        Me.GunaControlBox1.OnHoverIconColor = System.Drawing.Color.White
        Me.GunaControlBox1.OnPressedColor = System.Drawing.Color.Black
        Me.GunaControlBox1.Size = New System.Drawing.Size(38, 24)
        Me.GunaControlBox1.TabIndex = 70
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(77, 200)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(140, 28)
        Me.Label4.TabIndex = 69
        Me.Label4.Text = "New Password :"
        '
        'txt_Newpassword
        '
        Me.txt_Newpassword.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(201, Byte), Integer))
        Me.txt_Newpassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_Newpassword.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Newpassword.ForeColor = System.Drawing.Color.White
        Me.txt_Newpassword.Location = New System.Drawing.Point(82, 234)
        Me.txt_Newpassword.Multiline = True
        Me.txt_Newpassword.Name = "txt_Newpassword"
        Me.txt_Newpassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txt_Newpassword.Size = New System.Drawing.Size(296, 32)
        Me.txt_Newpassword.TabIndex = 68
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsername.ForeColor = System.Drawing.Color.Black
        Me.lblUsername.Location = New System.Drawing.Point(77, 96)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(94, 28)
        Me.lblUsername.TabIndex = 67
        Me.lblUsername.Text = "Username"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(77, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 28)
        Me.Label1.TabIndex = 66
        Me.Label1.Text = "Username "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Poppins", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(131, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(210, 34)
        Me.Label3.TabIndex = 65
        Me.Label3.Text = "CHANGE PASSWORD"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(77, 131)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(168, 28)
        Me.Label2.TabIndex = 74
        Me.Label2.Text = "Current Password :"
        '
        'txt_Currentpassword
        '
        Me.txt_Currentpassword.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(201, Byte), Integer))
        Me.txt_Currentpassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_Currentpassword.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Currentpassword.ForeColor = System.Drawing.Color.White
        Me.txt_Currentpassword.Location = New System.Drawing.Point(82, 165)
        Me.txt_Currentpassword.Multiline = True
        Me.txt_Currentpassword.Name = "txt_Currentpassword"
        Me.txt_Currentpassword.Size = New System.Drawing.Size(296, 32)
        Me.txt_Currentpassword.TabIndex = 73
        '
        'frmCashierChangepassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(456, 354)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_Currentpassword)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btn_save)
        Me.Controls.Add(Me.GunaControlBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txt_Newpassword)
        Me.Controls.Add(Me.lblUsername)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmCashierChangepassword"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmCashierChangepassword"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GunaElipse1 As Guna.UI.WinForms.GunaElipse
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btn_save As Button
    Friend WithEvents GunaControlBox1 As Guna.UI.WinForms.GunaControlBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txt_Newpassword As TextBox
    Friend WithEvents lblUsername As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_Currentpassword As TextBox
End Class

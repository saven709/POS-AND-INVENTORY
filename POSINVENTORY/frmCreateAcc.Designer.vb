<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreateAcc
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_fullname = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_Username = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_Password = New System.Windows.Forms.TextBox()
        Me.btn_save = New System.Windows.Forms.Button()
        Me.GunaControlBox1 = New Guna.UI.WinForms.GunaControlBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New Guna.UI.WinForms.GunaGroupBox()
        Me.rbProductManager = New System.Windows.Forms.RadioButton()
        Me.rbInventoryManager = New System.Windows.Forms.RadioButton()
        Me.rbCashier = New System.Windows.Forms.RadioButton()
        Me.rbAdmin = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GunaElipse1 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Poppins SemiBold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(34, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 26)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Fullname"
        '
        'txt_fullname
        '
        Me.txt_fullname.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(201, Byte), Integer))
        Me.txt_fullname.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_fullname.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_fullname.ForeColor = System.Drawing.Color.Black
        Me.txt_fullname.Location = New System.Drawing.Point(34, 76)
        Me.txt_fullname.Multiline = True
        Me.txt_fullname.Name = "txt_fullname"
        Me.txt_fullname.Size = New System.Drawing.Size(235, 32)
        Me.txt_fullname.TabIndex = 14
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Poppins", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(140, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(185, 34)
        Me.Label3.TabIndex = 51
        Me.Label3.Text = "CREATE ACCOUNT"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Poppins SemiBold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(34, 125)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 26)
        Me.Label1.TabIndex = 53
        Me.Label1.Text = "Username"
        '
        'txt_Username
        '
        Me.txt_Username.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(201, Byte), Integer))
        Me.txt_Username.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_Username.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Username.ForeColor = System.Drawing.Color.Black
        Me.txt_Username.Location = New System.Drawing.Point(34, 145)
        Me.txt_Username.Multiline = True
        Me.txt_Username.Name = "txt_Username"
        Me.txt_Username.Size = New System.Drawing.Size(235, 32)
        Me.txt_Username.TabIndex = 52
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Poppins SemiBold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(34, 194)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 26)
        Me.Label4.TabIndex = 55
        Me.Label4.Text = "Password"
        '
        'txt_Password
        '
        Me.txt_Password.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(201, Byte), Integer))
        Me.txt_Password.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_Password.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Password.ForeColor = System.Drawing.Color.Black
        Me.txt_Password.Location = New System.Drawing.Point(34, 213)
        Me.txt_Password.Multiline = True
        Me.txt_Password.Name = "txt_Password"
        Me.txt_Password.Size = New System.Drawing.Size(235, 32)
        Me.txt_Password.TabIndex = 54
        '
        'btn_save
        '
        Me.btn_save.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.btn_save.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_save.FlatAppearance.BorderSize = 0
        Me.btn_save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue
        Me.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_save.Font = New System.Drawing.Font("Poppins SemiBold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_save.ForeColor = System.Drawing.Color.Transparent
        Me.btn_save.Location = New System.Drawing.Point(191, 271)
        Me.btn_save.Name = "btn_save"
        Me.btn_save.Size = New System.Drawing.Size(97, 30)
        Me.btn_save.TabIndex = 60
        Me.btn_save.Text = "SAVE"
        Me.btn_save.UseVisualStyleBackColor = False
        '
        'GunaControlBox1
        '
        Me.GunaControlBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GunaControlBox1.AnimationHoverSpeed = 0.07!
        Me.GunaControlBox1.AnimationSpeed = 0.03!
        Me.GunaControlBox1.ControlBoxTheme = Guna.UI.WinForms.FormControlBoxTheme.Custom
        Me.GunaControlBox1.IconColor = System.Drawing.Color.Black
        Me.GunaControlBox1.IconSize = 15.0!
        Me.GunaControlBox1.Location = New System.Drawing.Point(442, 0)
        Me.GunaControlBox1.Name = "GunaControlBox1"
        Me.GunaControlBox1.OnHoverBackColor = System.Drawing.Color.Maroon
        Me.GunaControlBox1.OnHoverIconColor = System.Drawing.Color.White
        Me.GunaControlBox1.OnPressedColor = System.Drawing.Color.Black
        Me.GunaControlBox1.Size = New System.Drawing.Size(38, 24)
        Me.GunaControlBox1.TabIndex = 61
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Poppins", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(3, 5)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 19)
        Me.Label5.TabIndex = 57
        Me.Label5.Text = "Role"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.BaseColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.GroupBox1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(31, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.rbProductManager)
        Me.GroupBox1.Controls.Add(Me.rbInventoryManager)
        Me.GroupBox1.Controls.Add(Me.rbCashier)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.rbAdmin)
        Me.GroupBox1.LineColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(31, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(292, 55)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(169, 190)
        Me.GroupBox1.TabIndex = 59
        Me.GroupBox1.TextLocation = New System.Drawing.Point(10, 8)
        '
        'rbProductManager
        '
        Me.rbProductManager.AutoSize = True
        Me.rbProductManager.Font = New System.Drawing.Font("Poppins", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbProductManager.ForeColor = System.Drawing.Color.Black
        Me.rbProductManager.Location = New System.Drawing.Point(11, 146)
        Me.rbProductManager.Name = "rbProductManager"
        Me.rbProductManager.Size = New System.Drawing.Size(161, 30)
        Me.rbProductManager.TabIndex = 61
        Me.rbProductManager.TabStop = True
        Me.rbProductManager.Text = "Product Manager"
        Me.rbProductManager.UseVisualStyleBackColor = True
        Me.rbProductManager.Visible = False
        '
        'rbInventoryManager
        '
        Me.rbInventoryManager.AutoSize = True
        Me.rbInventoryManager.Font = New System.Drawing.Font("Poppins", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbInventoryManager.ForeColor = System.Drawing.Color.Black
        Me.rbInventoryManager.Location = New System.Drawing.Point(11, 113)
        Me.rbInventoryManager.Name = "rbInventoryManager"
        Me.rbInventoryManager.Size = New System.Drawing.Size(172, 30)
        Me.rbInventoryManager.TabIndex = 60
        Me.rbInventoryManager.TabStop = True
        Me.rbInventoryManager.Text = "Inventory Manager"
        Me.rbInventoryManager.UseVisualStyleBackColor = True
        Me.rbInventoryManager.Visible = False
        '
        'rbCashier
        '
        Me.rbCashier.AutoSize = True
        Me.rbCashier.Font = New System.Drawing.Font("Poppins", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbCashier.ForeColor = System.Drawing.Color.Black
        Me.rbCashier.Location = New System.Drawing.Point(11, 77)
        Me.rbCashier.Name = "rbCashier"
        Me.rbCashier.Size = New System.Drawing.Size(89, 30)
        Me.rbCashier.TabIndex = 59
        Me.rbCashier.TabStop = True
        Me.rbCashier.Text = "Cashier"
        Me.rbCashier.UseVisualStyleBackColor = True
        '
        'rbAdmin
        '
        Me.rbAdmin.AutoSize = True
        Me.rbAdmin.Font = New System.Drawing.Font("Poppins", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbAdmin.ForeColor = System.Drawing.Color.Black
        Me.rbAdmin.Location = New System.Drawing.Point(11, 44)
        Me.rbAdmin.Name = "rbAdmin"
        Me.rbAdmin.Size = New System.Drawing.Size(79, 30)
        Me.rbAdmin.TabIndex = 58
        Me.rbAdmin.TabStop = True
        Me.rbAdmin.Text = "Admin"
        Me.rbAdmin.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(98, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(-9, -4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(34, 340)
        Me.Panel1.TabIndex = 62
        '
        'GunaElipse1
        '
        Me.GunaElipse1.Radius = 15
        Me.GunaElipse1.TargetControl = Me
        '
        'frmCreateAcc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(477, 320)
        Me.Controls.Add(Me.txt_Username)
        Me.Controls.Add(Me.txt_fullname)
        Me.Controls.Add(Me.txt_Password)
        Me.Controls.Add(Me.GunaControlBox1)
        Me.Controls.Add(Me.btn_save)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmCreateAcc"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmCreateAcc"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label2 As Label
    Friend WithEvents txt_fullname As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_Username As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txt_Password As TextBox
    Friend WithEvents btn_save As Button
    Friend WithEvents GunaControlBox1 As Guna.UI.WinForms.GunaControlBox
    Friend WithEvents Label5 As Label
    Friend WithEvents GroupBox1 As Guna.UI.WinForms.GunaGroupBox
    Friend WithEvents rbProductManager As RadioButton
    Friend WithEvents rbInventoryManager As RadioButton
    Friend WithEvents rbCashier As RadioButton
    Friend WithEvents rbAdmin As RadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents GunaElipse1 As Guna.UI.WinForms.GunaElipse
End Class

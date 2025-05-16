<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UserAccount
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GunaElipse1 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.username = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnRemove = New Bunifu.Framework.UI.BunifuFlatButton()
        Me.BtnActivateDeact = New Bunifu.Framework.UI.BunifuFlatButton()
        Me.BtnChangePass = New Bunifu.Framework.UI.BunifuFlatButton()
        Me.BtnCreateAcc = New Bunifu.Framework.UI.BunifuFlatButton()
        Me.GunaElipse2 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.GunaElipse3 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.GunaElipse4 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.GunaElipse5 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbl_date1 = New System.Windows.Forms.Label()
        Me.lbl_time = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel3.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GunaElipse1
        '
        Me.GunaElipse1.Radius = 15
        Me.GunaElipse1.TargetControl = Me
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(98, Byte), Integer))
        Me.Panel3.Controls.Add(Me.DataGridView1)
        Me.Panel3.Controls.Add(Me.btnRemove)
        Me.Panel3.Controls.Add(Me.BtnActivateDeact)
        Me.Panel3.Controls.Add(Me.BtnChangePass)
        Me.Panel3.Controls.Add(Me.BtnCreateAcc)
        Me.Panel3.Location = New System.Drawing.Point(12, 28)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1170, 750)
        Me.Panel3.TabIndex = 29
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Poppins", 8.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.ColumnHeadersHeight = 30
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id, Me.Column2, Me.username, Me.Column4, Me.status, Me.Column6, Me.Column7})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(31, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView1.GridColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer))
        Me.DataGridView1.Location = New System.Drawing.Point(16, 78)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(1141, 593)
        Me.DataGridView1.TabIndex = 33
        '
        'id
        '
        Me.id.HeaderText = "No"
        Me.id.Name = "id"
        Me.id.ReadOnly = True
        Me.id.Visible = False
        '
        'Column2
        '
        Me.Column2.HeaderText = "FULLNAME"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'username
        '
        Me.username.HeaderText = "USERNAME"
        Me.username.Name = "username"
        Me.username.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.HeaderText = "ROLE"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'status
        '
        Me.status.HeaderText = "STATUS"
        Me.status.Name = "status"
        Me.status.ReadOnly = True
        '
        'Column6
        '
        Me.Column6.HeaderText = "CREATED"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'Column7
        '
        Me.Column7.HeaderText = "UPDATED"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        '
        'btnRemove
        '
        Me.btnRemove.Activecolor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnRemove.BackColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemove.BorderRadius = 0
        Me.btnRemove.ButtonText = "REMOVE"
        Me.btnRemove.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRemove.DisabledColor = System.Drawing.Color.Gray
        Me.btnRemove.Font = New System.Drawing.Font("Poppins", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemove.Iconcolor = System.Drawing.Color.Transparent
        Me.btnRemove.Iconimage = Nothing
        Me.btnRemove.Iconimage_right = Nothing
        Me.btnRemove.Iconimage_right_Selected = Nothing
        Me.btnRemove.Iconimage_Selected = Nothing
        Me.btnRemove.IconMarginLeft = 0
        Me.btnRemove.IconMarginRight = 0
        Me.btnRemove.IconRightVisible = True
        Me.btnRemove.IconRightZoom = 0R
        Me.btnRemove.IconVisible = True
        Me.btnRemove.IconZoom = 60.0R
        Me.btnRemove.IsTab = False
        Me.btnRemove.Location = New System.Drawing.Point(982, 687)
        Me.btnRemove.Margin = New System.Windows.Forms.Padding(4)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Normalcolor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.btnRemove.OnHovercolor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnRemove.OnHoverTextColor = System.Drawing.Color.White
        Me.btnRemove.selected = False
        Me.btnRemove.Size = New System.Drawing.Size(175, 44)
        Me.btnRemove.TabIndex = 32
        Me.btnRemove.Text = "REMOVE"
        Me.btnRemove.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnRemove.Textcolor = System.Drawing.SystemColors.ControlText
        Me.btnRemove.TextFont = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'BtnActivateDeact
        '
        Me.BtnActivateDeact.Activecolor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.BtnActivateDeact.BackColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.BtnActivateDeact.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnActivateDeact.BorderRadius = 0
        Me.BtnActivateDeact.ButtonText = "ACTIVATE/DEACTIVATE ACCOUNT"
        Me.BtnActivateDeact.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnActivateDeact.DisabledColor = System.Drawing.Color.Gray
        Me.BtnActivateDeact.Font = New System.Drawing.Font("Poppins", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnActivateDeact.Iconcolor = System.Drawing.Color.Transparent
        Me.BtnActivateDeact.Iconimage = Nothing
        Me.BtnActivateDeact.Iconimage_right = Nothing
        Me.BtnActivateDeact.Iconimage_right_Selected = Nothing
        Me.BtnActivateDeact.Iconimage_Selected = Nothing
        Me.BtnActivateDeact.IconMarginLeft = 0
        Me.BtnActivateDeact.IconMarginRight = 0
        Me.BtnActivateDeact.IconRightVisible = True
        Me.BtnActivateDeact.IconRightZoom = 0R
        Me.BtnActivateDeact.IconVisible = True
        Me.BtnActivateDeact.IconZoom = 60.0R
        Me.BtnActivateDeact.IsTab = False
        Me.BtnActivateDeact.Location = New System.Drawing.Point(945, 17)
        Me.BtnActivateDeact.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnActivateDeact.Name = "BtnActivateDeact"
        Me.BtnActivateDeact.Normalcolor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.BtnActivateDeact.OnHovercolor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(129, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.BtnActivateDeact.OnHoverTextColor = System.Drawing.Color.White
        Me.BtnActivateDeact.selected = False
        Me.BtnActivateDeact.Size = New System.Drawing.Size(212, 54)
        Me.BtnActivateDeact.TabIndex = 31
        Me.BtnActivateDeact.Text = "ACTIVATE/DEACTIVATE ACCOUNT"
        Me.BtnActivateDeact.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.BtnActivateDeact.Textcolor = System.Drawing.SystemColors.ControlText
        Me.BtnActivateDeact.TextFont = New System.Drawing.Font("Poppins", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'BtnChangePass
        '
        Me.BtnChangePass.Activecolor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.BtnChangePass.BackColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.BtnChangePass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnChangePass.BorderRadius = 0
        Me.BtnChangePass.ButtonText = "CHANGE PASSWORD"
        Me.BtnChangePass.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnChangePass.DisabledColor = System.Drawing.Color.Gray
        Me.BtnChangePass.Font = New System.Drawing.Font("Poppins", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnChangePass.Iconcolor = System.Drawing.Color.Transparent
        Me.BtnChangePass.Iconimage = Nothing
        Me.BtnChangePass.Iconimage_right = Nothing
        Me.BtnChangePass.Iconimage_right_Selected = Nothing
        Me.BtnChangePass.Iconimage_Selected = Nothing
        Me.BtnChangePass.IconMarginLeft = 0
        Me.BtnChangePass.IconMarginRight = 0
        Me.BtnChangePass.IconRightVisible = True
        Me.BtnChangePass.IconRightZoom = 0R
        Me.BtnChangePass.IconVisible = True
        Me.BtnChangePass.IconZoom = 60.0R
        Me.BtnChangePass.IsTab = False
        Me.BtnChangePass.Location = New System.Drawing.Point(202, 17)
        Me.BtnChangePass.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnChangePass.Name = "BtnChangePass"
        Me.BtnChangePass.Normalcolor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.BtnChangePass.OnHovercolor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(129, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.BtnChangePass.OnHoverTextColor = System.Drawing.Color.White
        Me.BtnChangePass.selected = False
        Me.BtnChangePass.Size = New System.Drawing.Size(168, 54)
        Me.BtnChangePass.TabIndex = 30
        Me.BtnChangePass.Text = "CHANGE PASSWORD"
        Me.BtnChangePass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.BtnChangePass.Textcolor = System.Drawing.SystemColors.ControlText
        Me.BtnChangePass.TextFont = New System.Drawing.Font("Poppins", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'BtnCreateAcc
        '
        Me.BtnCreateAcc.Activecolor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.BtnCreateAcc.BackColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.BtnCreateAcc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnCreateAcc.BorderRadius = 0
        Me.BtnCreateAcc.ButtonText = "CREATE ACCOUNT"
        Me.BtnCreateAcc.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnCreateAcc.DisabledColor = System.Drawing.Color.Gray
        Me.BtnCreateAcc.Font = New System.Drawing.Font("Poppins", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCreateAcc.Iconcolor = System.Drawing.Color.Transparent
        Me.BtnCreateAcc.Iconimage = Nothing
        Me.BtnCreateAcc.Iconimage_right = Nothing
        Me.BtnCreateAcc.Iconimage_right_Selected = Nothing
        Me.BtnCreateAcc.Iconimage_Selected = Nothing
        Me.BtnCreateAcc.IconMarginLeft = 0
        Me.BtnCreateAcc.IconMarginRight = 0
        Me.BtnCreateAcc.IconRightVisible = True
        Me.BtnCreateAcc.IconRightZoom = 0R
        Me.BtnCreateAcc.IconVisible = True
        Me.BtnCreateAcc.IconZoom = 60.0R
        Me.BtnCreateAcc.IsTab = False
        Me.BtnCreateAcc.Location = New System.Drawing.Point(16, 17)
        Me.BtnCreateAcc.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnCreateAcc.Name = "BtnCreateAcc"
        Me.BtnCreateAcc.Normalcolor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.BtnCreateAcc.OnHovercolor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(129, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.BtnCreateAcc.OnHoverTextColor = System.Drawing.Color.White
        Me.BtnCreateAcc.selected = False
        Me.BtnCreateAcc.Size = New System.Drawing.Size(168, 54)
        Me.BtnCreateAcc.TabIndex = 29
        Me.BtnCreateAcc.Text = "CREATE ACCOUNT"
        Me.BtnCreateAcc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.BtnCreateAcc.Textcolor = System.Drawing.SystemColors.ControlText
        Me.BtnCreateAcc.TextFont = New System.Drawing.Font("Poppins", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'GunaElipse2
        '
        Me.GunaElipse2.Radius = 15
        Me.GunaElipse2.TargetControl = Me.BtnActivateDeact
        '
        'GunaElipse3
        '
        Me.GunaElipse3.Radius = 15
        Me.GunaElipse3.TargetControl = Me.BtnChangePass
        '
        'GunaElipse4
        '
        Me.GunaElipse4.Radius = 15
        Me.GunaElipse4.TargetControl = Me.BtnCreateAcc
        '
        'GunaElipse5
        '
        Me.GunaElipse5.Radius = 15
        Me.GunaElipse5.TargetControl = Me.Panel3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Poppins Medium", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(21, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(134, 19)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "USER MANAGEMENT"
        '
        'lbl_date1
        '
        Me.lbl_date1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_date1.BackColor = System.Drawing.Color.Transparent
        Me.lbl_date1.Font = New System.Drawing.Font("Poppins", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_date1.ForeColor = System.Drawing.Color.Black
        Me.lbl_date1.Location = New System.Drawing.Point(847, 3)
        Me.lbl_date1.Name = "lbl_date1"
        Me.lbl_date1.Size = New System.Drawing.Size(251, 25)
        Me.lbl_date1.TabIndex = 35
        Me.lbl_date1.Text = "00.00"
        Me.lbl_date1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_time
        '
        Me.lbl_time.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_time.Font = New System.Drawing.Font("Poppins", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_time.ForeColor = System.Drawing.Color.Black
        Me.lbl_time.Location = New System.Drawing.Point(1075, 3)
        Me.lbl_time.Name = "lbl_time"
        Me.lbl_time.Size = New System.Drawing.Size(107, 25)
        Me.lbl_time.TabIndex = 34
        Me.lbl_time.Text = "00.00"
        Me.lbl_time.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'UserAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(201, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1200, 790)
        Me.Controls.Add(Me.lbl_date1)
        Me.Controls.Add(Me.lbl_time)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "UserAccount"
        Me.Text = "UserAccount"
        Me.Panel3.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GunaElipse1 As Guna.UI.WinForms.GunaElipse
    Friend WithEvents Panel3 As Panel
    Friend WithEvents BtnActivateDeact As Bunifu.Framework.UI.BunifuFlatButton
    Friend WithEvents BtnChangePass As Bunifu.Framework.UI.BunifuFlatButton
    Friend WithEvents BtnCreateAcc As Bunifu.Framework.UI.BunifuFlatButton
    Friend WithEvents GunaElipse2 As Guna.UI.WinForms.GunaElipse
    Friend WithEvents GunaElipse3 As Guna.UI.WinForms.GunaElipse
    Friend WithEvents GunaElipse4 As Guna.UI.WinForms.GunaElipse
    Friend WithEvents GunaElipse5 As Guna.UI.WinForms.GunaElipse
    Friend WithEvents btnRemove As Bunifu.Framework.UI.BunifuFlatButton
    Private WithEvents Label3 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents lbl_date1 As Label
    Friend WithEvents lbl_time As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents id As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents username As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents status As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
End Class

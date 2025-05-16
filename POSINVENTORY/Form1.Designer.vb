<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.PanelMain = New Guna.UI.WinForms.GunaPanel()
        Me.PanelSlide = New System.Windows.Forms.Panel()
        Me.PanelSetting = New System.Windows.Forms.Panel()
        Me.btnBackup = New Guna.UI.WinForms.GunaButton()
        Me.GunaButton4 = New Guna.UI.WinForms.GunaButton()
        Me.btnSetting = New Guna.UI.WinForms.GunaButton()
        Me.PanelRecord = New System.Windows.Forms.Panel()
        Me.GunaButton9 = New Guna.UI.WinForms.GunaButton()
        Me.btnRecord = New Guna.UI.WinForms.GunaButton()
        Me.PanelStock = New System.Windows.Forms.Panel()
        Me.GunaButton6 = New Guna.UI.WinForms.GunaButton()
        Me.GunaButton7 = New Guna.UI.WinForms.GunaButton()
        Me.btnStock = New Guna.UI.WinForms.GunaButton()
        Me.PanelProduct = New System.Windows.Forms.Panel()
        Me.GunaButton3 = New Guna.UI.WinForms.GunaButton()
        Me.btnList = New Guna.UI.WinForms.GunaButton()
        Me.btnProduct = New Guna.UI.WinForms.GunaButton()
        Me.btnDashboard = New Guna.UI.WinForms.GunaButton()
        Me.PanelLogo = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.lblRole = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GunaElipse1 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.GunaControlBox2 = New Guna.UI.WinForms.GunaControlBox()
        Me.GunaElipse2 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.GunaElipse3 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.GunaElipse4 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.btnLogout = New Guna.UI.WinForms.GunaButton()
        Me.GunaElipse5 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.GunaButton1 = New Guna.UI.WinForms.GunaButton()
        Me.GunaControlBox1 = New Guna.UI.WinForms.GunaControlBox()
        Me.transitionTimer = New System.Windows.Forms.Timer(Me.components)
        Me.PanelSlide.SuspendLayout()
        Me.PanelSetting.SuspendLayout()
        Me.PanelRecord.SuspendLayout()
        Me.PanelStock.SuspendLayout()
        Me.PanelProduct.SuspendLayout()
        Me.PanelLogo.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelMain
        '
        Me.PanelMain.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PanelMain.Location = New System.Drawing.Point(266, 39)
        Me.PanelMain.Name = "PanelMain"
        Me.PanelMain.Size = New System.Drawing.Size(1200, 790)
        Me.PanelMain.TabIndex = 5
        '
        'PanelSlide
        '
        Me.PanelSlide.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PanelSlide.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.PanelSlide.Controls.Add(Me.PanelSetting)
        Me.PanelSlide.Controls.Add(Me.btnSetting)
        Me.PanelSlide.Controls.Add(Me.PanelRecord)
        Me.PanelSlide.Controls.Add(Me.btnRecord)
        Me.PanelSlide.Controls.Add(Me.PanelStock)
        Me.PanelSlide.Controls.Add(Me.btnStock)
        Me.PanelSlide.Controls.Add(Me.PanelProduct)
        Me.PanelSlide.Controls.Add(Me.btnProduct)
        Me.PanelSlide.Controls.Add(Me.btnDashboard)
        Me.PanelSlide.Location = New System.Drawing.Point(21, 265)
        Me.PanelSlide.Name = "PanelSlide"
        Me.PanelSlide.Size = New System.Drawing.Size(217, 486)
        Me.PanelSlide.TabIndex = 3
        '
        'PanelSetting
        '
        Me.PanelSetting.Controls.Add(Me.btnBackup)
        Me.PanelSetting.Controls.Add(Me.GunaButton4)
        Me.PanelSetting.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelSetting.Location = New System.Drawing.Point(0, 431)
        Me.PanelSetting.Name = "PanelSetting"
        Me.PanelSetting.Size = New System.Drawing.Size(217, 72)
        Me.PanelSetting.TabIndex = 18
        Me.PanelSetting.Visible = False
        '
        'btnBackup
        '
        Me.btnBackup.AnimationHoverSpeed = 0.07!
        Me.btnBackup.AnimationSpeed = 0.03!
        Me.btnBackup.BackColor = System.Drawing.Color.Transparent
        Me.btnBackup.BaseColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.btnBackup.BorderColor = System.Drawing.Color.Black
        Me.btnBackup.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnBackup.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnBackup.FocusedColor = System.Drawing.Color.Empty
        Me.btnBackup.Font = New System.Drawing.Font("Poppins", 10.0!)
        Me.btnBackup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.btnBackup.Image = Nothing
        Me.btnBackup.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnBackup.Location = New System.Drawing.Point(0, 34)
        Me.btnBackup.Name = "btnBackup"
        Me.btnBackup.OnHoverBaseColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.btnBackup.OnHoverBorderColor = System.Drawing.Color.Black
        Me.btnBackup.OnHoverForeColor = System.Drawing.Color.White
        Me.btnBackup.OnHoverImage = Nothing
        Me.btnBackup.OnPressedColor = System.Drawing.Color.Black
        Me.btnBackup.Size = New System.Drawing.Size(217, 34)
        Me.btnBackup.TabIndex = 13
        Me.btnBackup.Text = "Backup/Restore Database"
        Me.btnBackup.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GunaButton4
        '
        Me.GunaButton4.AnimationHoverSpeed = 0.07!
        Me.GunaButton4.AnimationSpeed = 0.03!
        Me.GunaButton4.BackColor = System.Drawing.Color.Transparent
        Me.GunaButton4.BaseColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.GunaButton4.BorderColor = System.Drawing.Color.Black
        Me.GunaButton4.DialogResult = System.Windows.Forms.DialogResult.None
        Me.GunaButton4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GunaButton4.FocusedColor = System.Drawing.Color.Empty
        Me.GunaButton4.Font = New System.Drawing.Font("Poppins", 10.0!)
        Me.GunaButton4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.GunaButton4.Image = Nothing
        Me.GunaButton4.ImageSize = New System.Drawing.Size(20, 20)
        Me.GunaButton4.Location = New System.Drawing.Point(0, 0)
        Me.GunaButton4.Name = "GunaButton4"
        Me.GunaButton4.OnHoverBaseColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.GunaButton4.OnHoverBorderColor = System.Drawing.Color.Black
        Me.GunaButton4.OnHoverForeColor = System.Drawing.Color.White
        Me.GunaButton4.OnHoverImage = Nothing
        Me.GunaButton4.OnPressedColor = System.Drawing.Color.Black
        Me.GunaButton4.Size = New System.Drawing.Size(217, 34)
        Me.GunaButton4.TabIndex = 12
        Me.GunaButton4.Text = "User Management"
        Me.GunaButton4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnSetting
        '
        Me.btnSetting.AnimationHoverSpeed = 0.07!
        Me.btnSetting.AnimationSpeed = 0.03!
        Me.btnSetting.BackColor = System.Drawing.Color.FromArgb(CType(CType(73, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.btnSetting.BaseColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.btnSetting.BorderColor = System.Drawing.Color.Black
        Me.btnSetting.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnSetting.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnSetting.FocusedColor = System.Drawing.Color.Empty
        Me.btnSetting.Font = New System.Drawing.Font("Poppins", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnSetting.ForeColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.btnSetting.Image = Global.POSINVENTORY.My.Resources.Resources._21
        Me.btnSetting.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnSetting.Location = New System.Drawing.Point(0, 381)
        Me.btnSetting.Name = "btnSetting"
        Me.btnSetting.OnHoverBaseColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.btnSetting.OnHoverBorderColor = System.Drawing.Color.Black
        Me.btnSetting.OnHoverForeColor = System.Drawing.Color.White
        Me.btnSetting.OnHoverImage = Nothing
        Me.btnSetting.OnPressedColor = System.Drawing.Color.Black
        Me.btnSetting.Size = New System.Drawing.Size(217, 50)
        Me.btnSetting.TabIndex = 17
        Me.btnSetting.Text = "SETTING"
        '
        'PanelRecord
        '
        Me.PanelRecord.Controls.Add(Me.GunaButton9)
        Me.PanelRecord.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelRecord.Location = New System.Drawing.Point(0, 344)
        Me.PanelRecord.Name = "PanelRecord"
        Me.PanelRecord.Size = New System.Drawing.Size(217, 37)
        Me.PanelRecord.TabIndex = 16
        Me.PanelRecord.Visible = False
        '
        'GunaButton9
        '
        Me.GunaButton9.AnimationHoverSpeed = 0.07!
        Me.GunaButton9.AnimationSpeed = 0.03!
        Me.GunaButton9.BackColor = System.Drawing.Color.Transparent
        Me.GunaButton9.BaseColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.GunaButton9.BorderColor = System.Drawing.Color.Black
        Me.GunaButton9.DialogResult = System.Windows.Forms.DialogResult.None
        Me.GunaButton9.Dock = System.Windows.Forms.DockStyle.Top
        Me.GunaButton9.FocusedColor = System.Drawing.Color.Empty
        Me.GunaButton9.Font = New System.Drawing.Font("Poppins", 10.0!)
        Me.GunaButton9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.GunaButton9.Image = Nothing
        Me.GunaButton9.ImageSize = New System.Drawing.Size(20, 20)
        Me.GunaButton9.Location = New System.Drawing.Point(0, 0)
        Me.GunaButton9.Name = "GunaButton9"
        Me.GunaButton9.OnHoverBaseColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.GunaButton9.OnHoverBorderColor = System.Drawing.Color.Black
        Me.GunaButton9.OnHoverForeColor = System.Drawing.Color.White
        Me.GunaButton9.OnHoverImage = Nothing
        Me.GunaButton9.OnPressedColor = System.Drawing.Color.Black
        Me.GunaButton9.Size = New System.Drawing.Size(217, 34)
        Me.GunaButton9.TabIndex = 12
        Me.GunaButton9.Text = "Sale History"
        Me.GunaButton9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnRecord
        '
        Me.btnRecord.AnimationHoverSpeed = 0.07!
        Me.btnRecord.AnimationSpeed = 0.03!
        Me.btnRecord.BackColor = System.Drawing.Color.FromArgb(CType(CType(73, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.btnRecord.BaseColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.btnRecord.BorderColor = System.Drawing.Color.Black
        Me.btnRecord.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnRecord.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnRecord.FocusedColor = System.Drawing.Color.Empty
        Me.btnRecord.Font = New System.Drawing.Font("Poppins", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnRecord.ForeColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.btnRecord.Image = Global.POSINVENTORY.My.Resources.Resources._19
        Me.btnRecord.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnRecord.Location = New System.Drawing.Point(0, 294)
        Me.btnRecord.Name = "btnRecord"
        Me.btnRecord.OnHoverBaseColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.btnRecord.OnHoverBorderColor = System.Drawing.Color.Black
        Me.btnRecord.OnHoverForeColor = System.Drawing.Color.White
        Me.btnRecord.OnHoverImage = Nothing
        Me.btnRecord.OnPressedColor = System.Drawing.Color.Black
        Me.btnRecord.Size = New System.Drawing.Size(217, 50)
        Me.btnRecord.TabIndex = 15
        Me.btnRecord.Text = "RECORD"
        '
        'PanelStock
        '
        Me.PanelStock.Controls.Add(Me.GunaButton6)
        Me.PanelStock.Controls.Add(Me.GunaButton7)
        Me.PanelStock.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelStock.Location = New System.Drawing.Point(0, 222)
        Me.PanelStock.Name = "PanelStock"
        Me.PanelStock.Size = New System.Drawing.Size(217, 72)
        Me.PanelStock.TabIndex = 14
        Me.PanelStock.Visible = False
        '
        'GunaButton6
        '
        Me.GunaButton6.AnimationHoverSpeed = 0.07!
        Me.GunaButton6.AnimationSpeed = 0.03!
        Me.GunaButton6.BackColor = System.Drawing.Color.Transparent
        Me.GunaButton6.BaseColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.GunaButton6.BorderColor = System.Drawing.Color.Black
        Me.GunaButton6.DialogResult = System.Windows.Forms.DialogResult.None
        Me.GunaButton6.Dock = System.Windows.Forms.DockStyle.Top
        Me.GunaButton6.FocusedColor = System.Drawing.Color.Empty
        Me.GunaButton6.Font = New System.Drawing.Font("Poppins", 10.0!)
        Me.GunaButton6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.GunaButton6.Image = Nothing
        Me.GunaButton6.ImageSize = New System.Drawing.Size(20, 20)
        Me.GunaButton6.Location = New System.Drawing.Point(0, 34)
        Me.GunaButton6.Name = "GunaButton6"
        Me.GunaButton6.OnHoverBaseColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.GunaButton6.OnHoverBorderColor = System.Drawing.Color.Black
        Me.GunaButton6.OnHoverForeColor = System.Drawing.Color.White
        Me.GunaButton6.OnHoverImage = Nothing
        Me.GunaButton6.OnPressedColor = System.Drawing.Color.Black
        Me.GunaButton6.Size = New System.Drawing.Size(217, 35)
        Me.GunaButton6.TabIndex = 13
        Me.GunaButton6.Text = "Adjustment"
        Me.GunaButton6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GunaButton7
        '
        Me.GunaButton7.AnimationHoverSpeed = 0.07!
        Me.GunaButton7.AnimationSpeed = 0.03!
        Me.GunaButton7.BackColor = System.Drawing.Color.Transparent
        Me.GunaButton7.BaseColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.GunaButton7.BorderColor = System.Drawing.Color.Black
        Me.GunaButton7.DialogResult = System.Windows.Forms.DialogResult.None
        Me.GunaButton7.Dock = System.Windows.Forms.DockStyle.Top
        Me.GunaButton7.FocusedColor = System.Drawing.Color.Empty
        Me.GunaButton7.Font = New System.Drawing.Font("Poppins", 10.0!)
        Me.GunaButton7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.GunaButton7.Image = Nothing
        Me.GunaButton7.ImageSize = New System.Drawing.Size(20, 20)
        Me.GunaButton7.Location = New System.Drawing.Point(0, 0)
        Me.GunaButton7.Name = "GunaButton7"
        Me.GunaButton7.OnHoverBaseColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.GunaButton7.OnHoverBorderColor = System.Drawing.Color.Black
        Me.GunaButton7.OnHoverForeColor = System.Drawing.Color.White
        Me.GunaButton7.OnHoverImage = Nothing
        Me.GunaButton7.OnPressedColor = System.Drawing.Color.Black
        Me.GunaButton7.Size = New System.Drawing.Size(217, 34)
        Me.GunaButton7.TabIndex = 12
        Me.GunaButton7.Text = "Entry"
        Me.GunaButton7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnStock
        '
        Me.btnStock.AnimationHoverSpeed = 0.07!
        Me.btnStock.AnimationSpeed = 0.03!
        Me.btnStock.BackColor = System.Drawing.Color.FromArgb(CType(CType(73, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.btnStock.BaseColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.btnStock.BorderColor = System.Drawing.Color.BlanchedAlmond
        Me.btnStock.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnStock.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnStock.FocusedColor = System.Drawing.Color.Empty
        Me.btnStock.Font = New System.Drawing.Font("Poppins", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnStock.ForeColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.btnStock.Image = Global.POSINVENTORY.My.Resources.Resources._18
        Me.btnStock.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnStock.Location = New System.Drawing.Point(0, 172)
        Me.btnStock.Name = "btnStock"
        Me.btnStock.OnHoverBaseColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.btnStock.OnHoverBorderColor = System.Drawing.Color.Black
        Me.btnStock.OnHoverForeColor = System.Drawing.Color.White
        Me.btnStock.OnHoverImage = Nothing
        Me.btnStock.OnPressedColor = System.Drawing.Color.Black
        Me.btnStock.Size = New System.Drawing.Size(217, 50)
        Me.btnStock.TabIndex = 13
        Me.btnStock.Text = "INVENTORY"
        '
        'PanelProduct
        '
        Me.PanelProduct.Controls.Add(Me.GunaButton3)
        Me.PanelProduct.Controls.Add(Me.btnList)
        Me.PanelProduct.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelProduct.Location = New System.Drawing.Point(0, 100)
        Me.PanelProduct.Name = "PanelProduct"
        Me.PanelProduct.Size = New System.Drawing.Size(217, 72)
        Me.PanelProduct.TabIndex = 12
        Me.PanelProduct.Visible = False
        '
        'GunaButton3
        '
        Me.GunaButton3.AnimationHoverSpeed = 0.07!
        Me.GunaButton3.AnimationSpeed = 0.03!
        Me.GunaButton3.BackColor = System.Drawing.Color.Transparent
        Me.GunaButton3.BaseColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.GunaButton3.BorderColor = System.Drawing.Color.Black
        Me.GunaButton3.DialogResult = System.Windows.Forms.DialogResult.None
        Me.GunaButton3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GunaButton3.FocusedColor = System.Drawing.Color.Empty
        Me.GunaButton3.Font = New System.Drawing.Font("Poppins", 10.0!)
        Me.GunaButton3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.GunaButton3.Image = Nothing
        Me.GunaButton3.ImageSize = New System.Drawing.Size(20, 20)
        Me.GunaButton3.Location = New System.Drawing.Point(0, 34)
        Me.GunaButton3.Name = "GunaButton3"
        Me.GunaButton3.OnHoverBaseColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.GunaButton3.OnHoverBorderColor = System.Drawing.Color.Black
        Me.GunaButton3.OnHoverForeColor = System.Drawing.Color.White
        Me.GunaButton3.OnHoverImage = Nothing
        Me.GunaButton3.OnPressedColor = System.Drawing.Color.Black
        Me.GunaButton3.Size = New System.Drawing.Size(217, 35)
        Me.GunaButton3.TabIndex = 13
        Me.GunaButton3.Text = "Category"
        Me.GunaButton3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.GunaButton3.Visible = False
        '
        'btnList
        '
        Me.btnList.AnimationHoverSpeed = 0.07!
        Me.btnList.AnimationSpeed = 0.03!
        Me.btnList.BackColor = System.Drawing.Color.Transparent
        Me.btnList.BaseColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.btnList.BorderColor = System.Drawing.Color.Black
        Me.btnList.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnList.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnList.FocusedColor = System.Drawing.Color.Empty
        Me.btnList.Font = New System.Drawing.Font("Poppins", 10.0!)
        Me.btnList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.btnList.Image = Nothing
        Me.btnList.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnList.Location = New System.Drawing.Point(0, 0)
        Me.btnList.Name = "btnList"
        Me.btnList.OnHoverBaseColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.btnList.OnHoverBorderColor = System.Drawing.Color.Black
        Me.btnList.OnHoverForeColor = System.Drawing.Color.White
        Me.btnList.OnHoverImage = Nothing
        Me.btnList.OnPressedColor = System.Drawing.Color.Black
        Me.btnList.Size = New System.Drawing.Size(217, 34)
        Me.btnList.TabIndex = 12
        Me.btnList.Text = "Manage Product"
        Me.btnList.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnProduct
        '
        Me.btnProduct.AnimationHoverSpeed = 0.07!
        Me.btnProduct.AnimationSpeed = 0.03!
        Me.btnProduct.BackColor = System.Drawing.Color.FromArgb(CType(CType(73, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.btnProduct.BaseColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.btnProduct.BorderColor = System.Drawing.Color.Black
        Me.btnProduct.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnProduct.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnProduct.FocusedColor = System.Drawing.Color.Empty
        Me.btnProduct.Font = New System.Drawing.Font("Poppins", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnProduct.ForeColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.btnProduct.Image = Global.POSINVENTORY.My.Resources.Resources._17
        Me.btnProduct.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnProduct.Location = New System.Drawing.Point(0, 50)
        Me.btnProduct.Name = "btnProduct"
        Me.btnProduct.OnHoverBaseColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.btnProduct.OnHoverBorderColor = System.Drawing.Color.Black
        Me.btnProduct.OnHoverForeColor = System.Drawing.Color.White
        Me.btnProduct.OnHoverImage = Nothing
        Me.btnProduct.OnPressedColor = System.Drawing.Color.Black
        Me.btnProduct.Size = New System.Drawing.Size(217, 50)
        Me.btnProduct.TabIndex = 11
        Me.btnProduct.Text = "MANAGE PRODUCT"
        '
        'btnDashboard
        '
        Me.btnDashboard.AnimationHoverSpeed = 0.07!
        Me.btnDashboard.AnimationSpeed = 0.03!
        Me.btnDashboard.BackColor = System.Drawing.Color.FromArgb(CType(CType(73, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.btnDashboard.BaseColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.btnDashboard.BorderColor = System.Drawing.Color.Black
        Me.btnDashboard.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnDashboard.FocusedColor = System.Drawing.Color.Empty
        Me.btnDashboard.Font = New System.Drawing.Font("Poppins", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnDashboard.ForeColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.btnDashboard.Image = Global.POSINVENTORY.My.Resources.Resources._16
        Me.btnDashboard.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnDashboard.Location = New System.Drawing.Point(0, 0)
        Me.btnDashboard.Name = "btnDashboard"
        Me.btnDashboard.OnHoverBaseColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.btnDashboard.OnHoverBorderColor = System.Drawing.Color.Black
        Me.btnDashboard.OnHoverForeColor = System.Drawing.Color.White
        Me.btnDashboard.OnHoverImage = Nothing
        Me.btnDashboard.OnPressedColor = System.Drawing.Color.Black
        Me.btnDashboard.Size = New System.Drawing.Size(217, 50)
        Me.btnDashboard.TabIndex = 0
        Me.btnDashboard.Text = "DASHBOARD"
        '
        'PanelLogo
        '
        Me.PanelLogo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PanelLogo.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.PanelLogo.Controls.Add(Me.Panel1)
        Me.PanelLogo.Controls.Add(Me.PictureBox1)
        Me.PanelLogo.Location = New System.Drawing.Point(21, 39)
        Me.PanelLogo.Name = "PanelLogo"
        Me.PanelLogo.Size = New System.Drawing.Size(217, 207)
        Me.PanelLogo.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.Panel1.Controls.Add(Me.lblUsername)
        Me.Panel1.Controls.Add(Me.lblRole)
        Me.Panel1.Location = New System.Drawing.Point(16, 124)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(187, 49)
        Me.Panel1.TabIndex = 1
        '
        'lblUsername
        '
        Me.lblUsername.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblUsername.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblUsername.ForeColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.lblUsername.Location = New System.Drawing.Point(0, 11)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(187, 19)
        Me.lblUsername.TabIndex = 1
        Me.lblUsername.Text = "admin"
        Me.lblUsername.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'lblRole
        '
        Me.lblRole.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblRole.Font = New System.Drawing.Font("Poppins", 9.25!, System.Drawing.FontStyle.Bold)
        Me.lblRole.ForeColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.lblRole.Location = New System.Drawing.Point(0, 30)
        Me.lblRole.Name = "lblRole"
        Me.lblRole.Size = New System.Drawing.Size(187, 19)
        Me.lblRole.TabIndex = 0
        Me.lblRole.Text = "Administrator"
        Me.lblRole.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.POSINVENTORY.My.Resources.Resources.User2
        Me.PictureBox1.InitialImage = CType(resources.GetObject("PictureBox1.InitialImage"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(58, 18)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(100, 100)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'GunaElipse1
        '
        Me.GunaElipse1.Radius = 15
        Me.GunaElipse1.TargetControl = Me
        '
        'GunaControlBox2
        '
        Me.GunaControlBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GunaControlBox2.AnimationHoverSpeed = 0.07!
        Me.GunaControlBox2.AnimationSpeed = 0.03!
        Me.GunaControlBox2.ControlBoxTheme = Guna.UI.WinForms.FormControlBoxTheme.Custom
        Me.GunaControlBox2.ControlBoxType = Guna.UI.WinForms.FormControlBoxType.MaximizeBox
        Me.GunaControlBox2.IconColor = System.Drawing.SystemColors.ButtonFace
        Me.GunaControlBox2.IconSize = 15.0!
        Me.GunaControlBox2.Location = New System.Drawing.Point(1404, 1)
        Me.GunaControlBox2.Name = "GunaControlBox2"
        Me.GunaControlBox2.OnHoverBackColor = System.Drawing.Color.Maroon
        Me.GunaControlBox2.OnHoverIconColor = System.Drawing.Color.White
        Me.GunaControlBox2.OnPressedColor = System.Drawing.Color.Black
        Me.GunaControlBox2.Size = New System.Drawing.Size(38, 24)
        Me.GunaControlBox2.TabIndex = 21
        '
        'GunaElipse2
        '
        Me.GunaElipse2.Radius = 10
        Me.GunaElipse2.TargetControl = Me.PanelSlide
        '
        'GunaElipse3
        '
        Me.GunaElipse3.Radius = 10
        Me.GunaElipse3.TargetControl = Me.PanelLogo
        '
        'GunaElipse4
        '
        Me.GunaElipse4.Radius = 10
        Me.GunaElipse4.TargetControl = Me.btnLogout
        '
        'btnLogout
        '
        Me.btnLogout.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnLogout.AnimationHoverSpeed = 0.07!
        Me.btnLogout.AnimationSpeed = 0.03!
        Me.btnLogout.BackColor = System.Drawing.Color.FromArgb(CType(CType(73, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.btnLogout.BaseColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(178, Byte), Integer))
        Me.btnLogout.BorderColor = System.Drawing.Color.Black
        Me.btnLogout.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnLogout.FocusedColor = System.Drawing.Color.Empty
        Me.btnLogout.Font = New System.Drawing.Font("Poppins", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnLogout.ForeColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.btnLogout.Image = Global.POSINVENTORY.My.Resources.Resources._8
        Me.btnLogout.ImageSize = New System.Drawing.Size(20, 20)
        Me.btnLogout.Location = New System.Drawing.Point(21, 779)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.OnHoverBaseColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.btnLogout.OnHoverBorderColor = System.Drawing.Color.Black
        Me.btnLogout.OnHoverForeColor = System.Drawing.Color.White
        Me.btnLogout.OnHoverImage = Nothing
        Me.btnLogout.OnPressedColor = System.Drawing.Color.Black
        Me.btnLogout.Size = New System.Drawing.Size(217, 50)
        Me.btnLogout.TabIndex = 8
        Me.btnLogout.Text = "LOG OUT"
        '
        'GunaElipse5
        '
        Me.GunaElipse5.Radius = 10
        Me.GunaElipse5.TargetControl = Me.PanelMain
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'GunaButton1
        '
        Me.GunaButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GunaButton1.AnimationHoverSpeed = 0.07!
        Me.GunaButton1.AnimationSpeed = 0.03!
        Me.GunaButton1.BaseColor = System.Drawing.Color.Transparent
        Me.GunaButton1.BorderColor = System.Drawing.Color.Black
        Me.GunaButton1.DialogResult = System.Windows.Forms.DialogResult.None
        Me.GunaButton1.FocusedColor = System.Drawing.Color.Empty
        Me.GunaButton1.Font = New System.Drawing.Font("Poppins", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GunaButton1.ForeColor = System.Drawing.Color.White
        Me.GunaButton1.Image = Nothing
        Me.GunaButton1.ImageSize = New System.Drawing.Size(20, 20)
        Me.GunaButton1.Location = New System.Drawing.Point(1439, 1)
        Me.GunaButton1.Name = "GunaButton1"
        Me.GunaButton1.OnHoverBaseColor = System.Drawing.Color.Maroon
        Me.GunaButton1.OnHoverBorderColor = System.Drawing.Color.Black
        Me.GunaButton1.OnHoverForeColor = System.Drawing.Color.White
        Me.GunaButton1.OnHoverImage = Nothing
        Me.GunaButton1.OnPressedColor = System.Drawing.Color.Black
        Me.GunaButton1.Size = New System.Drawing.Size(47, 24)
        Me.GunaButton1.TabIndex = 43
        Me.GunaButton1.Text = "X"
        Me.GunaButton1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GunaControlBox1
        '
        Me.GunaControlBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GunaControlBox1.AnimationHoverSpeed = 0.07!
        Me.GunaControlBox1.AnimationSpeed = 0.03!
        Me.GunaControlBox1.ControlBoxTheme = Guna.UI.WinForms.FormControlBoxTheme.Custom
        Me.GunaControlBox1.ControlBoxType = Guna.UI.WinForms.FormControlBoxType.MinimizeBox
        Me.GunaControlBox1.IconColor = System.Drawing.SystemColors.ButtonFace
        Me.GunaControlBox1.IconSize = 15.0!
        Me.GunaControlBox1.Location = New System.Drawing.Point(1360, 1)
        Me.GunaControlBox1.Name = "GunaControlBox1"
        Me.GunaControlBox1.OnHoverBackColor = System.Drawing.Color.Maroon
        Me.GunaControlBox1.OnHoverIconColor = System.Drawing.Color.White
        Me.GunaControlBox1.OnPressedColor = System.Drawing.Color.Black
        Me.GunaControlBox1.Size = New System.Drawing.Size(38, 24)
        Me.GunaControlBox1.TabIndex = 44
        '
        'transitionTimer
        '
        Me.transitionTimer.Interval = 10
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1489, 850)
        Me.Controls.Add(Me.btnLogout)
        Me.Controls.Add(Me.GunaControlBox1)
        Me.Controls.Add(Me.GunaButton1)
        Me.Controls.Add(Me.PanelSlide)
        Me.Controls.Add(Me.PanelLogo)
        Me.Controls.Add(Me.GunaControlBox2)
        Me.Controls.Add(Me.PanelMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.PanelSlide.ResumeLayout(False)
        Me.PanelSetting.ResumeLayout(False)
        Me.PanelRecord.ResumeLayout(False)
        Me.PanelStock.ResumeLayout(False)
        Me.PanelProduct.ResumeLayout(False)
        Me.PanelLogo.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelMain As Guna.UI.WinForms.GunaPanel
    Friend WithEvents PanelSlide As Panel
    Friend WithEvents btnLogout As Guna.UI.WinForms.GunaButton
    Friend WithEvents btnDashboard As Guna.UI.WinForms.GunaButton
    Friend WithEvents PanelLogo As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lblUsername As Label
    Friend WithEvents GunaElipse1 As Guna.UI.WinForms.GunaElipse
    Friend WithEvents btnProduct As Guna.UI.WinForms.GunaButton
    Friend WithEvents btnRecord As Guna.UI.WinForms.GunaButton
    Friend WithEvents btnStock As Guna.UI.WinForms.GunaButton
    Friend WithEvents btnSetting As Guna.UI.WinForms.GunaButton
    Friend WithEvents PanelSetting As Panel
    Friend WithEvents GunaButton4 As Guna.UI.WinForms.GunaButton
    Friend WithEvents PanelRecord As Panel
    Friend WithEvents GunaButton9 As Guna.UI.WinForms.GunaButton
    Friend WithEvents PanelStock As Panel
    Friend WithEvents GunaButton6 As Guna.UI.WinForms.GunaButton
    Friend WithEvents GunaButton7 As Guna.UI.WinForms.GunaButton
    Friend WithEvents PanelProduct As Panel
    Friend WithEvents GunaButton3 As Guna.UI.WinForms.GunaButton
    Friend WithEvents btnList As Guna.UI.WinForms.GunaButton
    Friend WithEvents lblRole As Label
    Friend WithEvents GunaControlBox2 As Guna.UI.WinForms.GunaControlBox
    Friend WithEvents GunaElipse2 As Guna.UI.WinForms.GunaElipse
    Friend WithEvents GunaElipse3 As Guna.UI.WinForms.GunaElipse
    Friend WithEvents GunaElipse4 As Guna.UI.WinForms.GunaElipse
    Friend WithEvents GunaElipse5 As Guna.UI.WinForms.GunaElipse
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents GunaButton1 As Guna.UI.WinForms.GunaButton
    Friend WithEvents GunaControlBox1 As Guna.UI.WinForms.GunaControlBox
    Friend WithEvents transitionTimer As Timer
    Friend WithEvents btnBackup As Guna.UI.WinForms.GunaButton
End Class

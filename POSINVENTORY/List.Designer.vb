<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class List
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(List))
        Dim StateProperties1 As Bunifu.UI.WinForms.BunifuTextBox.StateProperties = New Bunifu.UI.WinForms.BunifuTextBox.StateProperties()
        Dim StateProperties2 As Bunifu.UI.WinForms.BunifuTextBox.StateProperties = New Bunifu.UI.WinForms.BunifuTextBox.StateProperties()
        Dim StateProperties3 As Bunifu.UI.WinForms.BunifuTextBox.StateProperties = New Bunifu.UI.WinForms.BunifuTextBox.StateProperties()
        Dim StateProperties4 As Bunifu.UI.WinForms.BunifuTextBox.StateProperties = New Bunifu.UI.WinForms.BunifuTextBox.StateProperties()
        Me.GunaElipse1 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.PanelTitle = New System.Windows.Forms.Panel()
        Me.btn_ManageAddons = New Bunifu.Framework.UI.BunifuFlatButton()
        Me.categorybtn = New System.Windows.Forms.Button()
        Me.txt_search = New Bunifu.UI.WinForms.BunifuTextBox()
        Me.btn_ManageFoods = New Bunifu.Framework.UI.BunifuFlatButton()
        Me.GunaElipse2 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.GunaElipse3 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.GunaElipse4 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.GunaElipse7 = New Guna.UI.WinForms.GunaElipse(Me.components)
        Me.PanelTitle.SuspendLayout()
        Me.SuspendLayout()
        '
        'GunaElipse1
        '
        Me.GunaElipse1.Radius = 15
        Me.GunaElipse1.TargetControl = Me
        '
        'PanelTitle
        '
        Me.PanelTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(201, Byte), Integer))
        Me.PanelTitle.Controls.Add(Me.btn_ManageAddons)
        Me.PanelTitle.Controls.Add(Me.categorybtn)
        Me.PanelTitle.Controls.Add(Me.txt_search)
        Me.PanelTitle.Controls.Add(Me.btn_ManageFoods)
        Me.PanelTitle.Location = New System.Drawing.Point(12, 27)
        Me.PanelTitle.Name = "PanelTitle"
        Me.PanelTitle.Size = New System.Drawing.Size(1162, 124)
        Me.PanelTitle.TabIndex = 5
        '
        'btn_ManageAddons
        '
        Me.btn_ManageAddons.Activecolor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.btn_ManageAddons.BackColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.btn_ManageAddons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_ManageAddons.BorderRadius = 0
        Me.btn_ManageAddons.ButtonText = "ADD ONS"
        Me.btn_ManageAddons.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_ManageAddons.DisabledColor = System.Drawing.Color.Gray
        Me.btn_ManageAddons.Font = New System.Drawing.Font("Poppins", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ManageAddons.Iconcolor = System.Drawing.Color.Transparent
        Me.btn_ManageAddons.Iconimage = Global.POSINVENTORY.My.Resources.Resources.Add_New1
        Me.btn_ManageAddons.Iconimage_right = Nothing
        Me.btn_ManageAddons.Iconimage_right_Selected = Nothing
        Me.btn_ManageAddons.Iconimage_Selected = Nothing
        Me.btn_ManageAddons.IconMarginLeft = 0
        Me.btn_ManageAddons.IconMarginRight = 0
        Me.btn_ManageAddons.IconRightVisible = True
        Me.btn_ManageAddons.IconRightZoom = 0R
        Me.btn_ManageAddons.IconVisible = True
        Me.btn_ManageAddons.IconZoom = 60.0R
        Me.btn_ManageAddons.IsTab = False
        Me.btn_ManageAddons.Location = New System.Drawing.Point(919, 72)
        Me.btn_ManageAddons.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_ManageAddons.Name = "btn_ManageAddons"
        Me.btn_ManageAddons.Normalcolor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.btn_ManageAddons.OnHovercolor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(129, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.btn_ManageAddons.OnHoverTextColor = System.Drawing.Color.White
        Me.btn_ManageAddons.selected = False
        Me.btn_ManageAddons.Size = New System.Drawing.Size(115, 37)
        Me.btn_ManageAddons.TabIndex = 42
        Me.btn_ManageAddons.Text = "ADD ONS"
        Me.btn_ManageAddons.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btn_ManageAddons.Textcolor = System.Drawing.SystemColors.ControlText
        Me.btn_ManageAddons.TextFont = New System.Drawing.Font("Poppins", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'categorybtn
        '
        Me.categorybtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(84, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(81, Byte), Integer))
        Me.categorybtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.categorybtn.FlatAppearance.BorderSize = 0
        Me.categorybtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue
        Me.categorybtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.categorybtn.Font = New System.Drawing.Font("Poppins", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.categorybtn.ForeColor = System.Drawing.Color.Transparent
        Me.categorybtn.Location = New System.Drawing.Point(1049, 72)
        Me.categorybtn.Name = "categorybtn"
        Me.categorybtn.Size = New System.Drawing.Size(95, 37)
        Me.categorybtn.TabIndex = 41
        Me.categorybtn.Text = "Category"
        Me.categorybtn.UseVisualStyleBackColor = False
        '
        'txt_search
        '
        Me.txt_search.AcceptsReturn = False
        Me.txt_search.AcceptsTab = False
        Me.txt_search.AnimationSpeed = 200
        Me.txt_search.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txt_search.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txt_search.AutoSize = True
        Me.txt_search.BackColor = System.Drawing.Color.White
        Me.txt_search.BackgroundImage = CType(resources.GetObject("txt_search.BackgroundImage"), System.Drawing.Image)
        Me.txt_search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.txt_search.BorderColorActive = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(26, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.txt_search.BorderColorDisabled = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(26, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.txt_search.BorderColorHover = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(26, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.txt_search.BorderColorIdle = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(26, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.txt_search.BorderRadius = 1
        Me.txt_search.BorderThickness = 1
        Me.txt_search.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txt_search.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txt_search.DefaultFont = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_search.DefaultText = ""
        Me.txt_search.FillColor = System.Drawing.Color.White
        Me.txt_search.ForeColor = System.Drawing.Color.Black
        Me.txt_search.HideSelection = True
        Me.txt_search.IconLeft = Global.POSINVENTORY.My.Resources.Resources.Search1
        Me.txt_search.IconLeftCursor = System.Windows.Forms.Cursors.IBeam
        Me.txt_search.IconPadding = 10
        Me.txt_search.IconRight = Nothing
        Me.txt_search.IconRightCursor = System.Windows.Forms.Cursors.IBeam
        Me.txt_search.Lines = New String(-1) {}
        Me.txt_search.Location = New System.Drawing.Point(21, 18)
        Me.txt_search.MaxLength = 32767
        Me.txt_search.MinimumSize = New System.Drawing.Size(1, 1)
        Me.txt_search.Modified = False
        Me.txt_search.Multiline = False
        Me.txt_search.Name = "txt_search"
        StateProperties1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(26, Byte), Integer), CType(CType(26, Byte), Integer))
        StateProperties1.FillColor = System.Drawing.Color.Empty
        StateProperties1.ForeColor = System.Drawing.Color.Empty
        StateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty
        Me.txt_search.OnActiveState = StateProperties1
        StateProperties2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(26, Byte), Integer), CType(CType(26, Byte), Integer))
        StateProperties2.FillColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(26, Byte), Integer), CType(CType(26, Byte), Integer))
        StateProperties2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(26, Byte), Integer), CType(CType(26, Byte), Integer))
        StateProperties2.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(26, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.txt_search.OnDisabledState = StateProperties2
        StateProperties3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(26, Byte), Integer), CType(CType(26, Byte), Integer))
        StateProperties3.FillColor = System.Drawing.Color.Empty
        StateProperties3.ForeColor = System.Drawing.Color.Empty
        StateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty
        Me.txt_search.OnHoverState = StateProperties3
        StateProperties4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(26, Byte), Integer), CType(CType(26, Byte), Integer))
        StateProperties4.FillColor = System.Drawing.Color.White
        StateProperties4.ForeColor = System.Drawing.Color.Black
        StateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty
        Me.txt_search.OnIdleState = StateProperties4
        Me.txt_search.Padding = New System.Windows.Forms.Padding(3)
        Me.txt_search.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txt_search.PlaceholderForeColor = System.Drawing.Color.White
        Me.txt_search.PlaceholderText = ""
        Me.txt_search.ReadOnly = False
        Me.txt_search.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txt_search.SelectedText = ""
        Me.txt_search.SelectionLength = 0
        Me.txt_search.SelectionStart = 0
        Me.txt_search.ShortcutsEnabled = True
        Me.txt_search.Size = New System.Drawing.Size(1124, 37)
        Me.txt_search.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Material
        Me.txt_search.TabIndex = 26
        Me.txt_search.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txt_search.TextMarginBottom = 0
        Me.txt_search.TextMarginLeft = 3
        Me.txt_search.TextMarginTop = 0
        Me.txt_search.TextPlaceholder = ""
        Me.txt_search.UseSystemPasswordChar = False
        Me.txt_search.WordWrap = True
        '
        'btn_ManageFoods
        '
        Me.btn_ManageFoods.Activecolor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.btn_ManageFoods.BackColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.btn_ManageFoods.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_ManageFoods.BorderRadius = 0
        Me.btn_ManageFoods.ButtonText = "ADD PRODUCT"
        Me.btn_ManageFoods.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_ManageFoods.DisabledColor = System.Drawing.Color.Gray
        Me.btn_ManageFoods.Font = New System.Drawing.Font("Poppins", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ManageFoods.Iconcolor = System.Drawing.Color.Transparent
        Me.btn_ManageFoods.Iconimage = Global.POSINVENTORY.My.Resources.Resources.Add_New1
        Me.btn_ManageFoods.Iconimage_right = Nothing
        Me.btn_ManageFoods.Iconimage_right_Selected = Nothing
        Me.btn_ManageFoods.Iconimage_Selected = Nothing
        Me.btn_ManageFoods.IconMarginLeft = 0
        Me.btn_ManageFoods.IconMarginRight = 0
        Me.btn_ManageFoods.IconRightVisible = True
        Me.btn_ManageFoods.IconRightZoom = 0R
        Me.btn_ManageFoods.IconVisible = True
        Me.btn_ManageFoods.IconZoom = 60.0R
        Me.btn_ManageFoods.IsTab = False
        Me.btn_ManageFoods.Location = New System.Drawing.Point(736, 72)
        Me.btn_ManageFoods.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_ManageFoods.Name = "btn_ManageFoods"
        Me.btn_ManageFoods.Normalcolor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.btn_ManageFoods.OnHovercolor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(129, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.btn_ManageFoods.OnHoverTextColor = System.Drawing.Color.White
        Me.btn_ManageFoods.selected = False
        Me.btn_ManageFoods.Size = New System.Drawing.Size(174, 37)
        Me.btn_ManageFoods.TabIndex = 8
        Me.btn_ManageFoods.Text = "ADD PRODUCT"
        Me.btn_ManageFoods.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btn_ManageFoods.Textcolor = System.Drawing.SystemColors.ControlText
        Me.btn_ManageFoods.TextFont = New System.Drawing.Font("Poppins", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'GunaElipse2
        '
        Me.GunaElipse2.Radius = 15
        Me.GunaElipse2.TargetControl = Me.PanelTitle
        '
        'GunaElipse3
        '
        Me.GunaElipse3.Radius = 10
        Me.GunaElipse3.TargetControl = Me.btn_ManageFoods
        '
        'GunaElipse4
        '
        Me.GunaElipse4.Radius = 15
        Me.GunaElipse4.TargetControl = Me.btn_ManageAddons
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Poppins Medium", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(21, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(129, 19)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "MANAGE PRODUCT"
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.BackColor = System.Drawing.Color.White
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(12, 170)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Padding = New System.Windows.Forms.Padding(20)
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(1162, 594)
        Me.FlowLayoutPanel1.TabIndex = 10
        '
        'GunaElipse7
        '
        Me.GunaElipse7.Radius = 12
        Me.GunaElipse7.TargetControl = Me.txt_search
        '
        'List
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1200, 790)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.PanelTitle)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "List"
        Me.Text = "List"
        Me.PanelTitle.ResumeLayout(False)
        Me.PanelTitle.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GunaElipse1 As Guna.UI.WinForms.GunaElipse
    Friend WithEvents PanelTitle As Panel
    Friend WithEvents GunaElipse2 As Guna.UI.WinForms.GunaElipse
    Friend WithEvents GunaElipse3 As Guna.UI.WinForms.GunaElipse
    Friend WithEvents GunaElipse4 As Guna.UI.WinForms.GunaElipse
    Friend WithEvents txt_search As Bunifu.UI.WinForms.BunifuTextBox
    Friend WithEvents btn_ManageFoods As Bunifu.Framework.UI.BunifuFlatButton
    Private WithEvents Label3 As Label
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents categorybtn As Button
    Friend WithEvents GunaElipse7 As Guna.UI.WinForms.GunaElipse
    Friend WithEvents btn_ManageAddons As Bunifu.Framework.UI.BunifuFlatButton
End Class

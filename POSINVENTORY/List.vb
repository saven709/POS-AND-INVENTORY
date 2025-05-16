Imports System.IO
Imports MySql.Data.MySqlClient
Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class List
    Private WithEvents pan As Panel
    Private WithEvents pan_top As Panel
    Private WithEvents foodname As Label
    Private WithEvents img As CirclePicturBox
    Private WithEvents btnEdit As Button
    Private isHovering As Boolean = False


    ' Add categories list and current index
    Private categories As List(Of String) = New List(Of String)
    Private currentCategoryIndex As Integer = -1

    Private Sub btn_ManageFoods_Click(sender As Object, e As EventArgs) Handles btn_ManageFoods.Click
        frm_ManageFoods.ShowDialog()
    End Sub

    Private Sub List_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbconn()
        LoadCategories()
        categorybtn.Text = categories(0)

        Dim tooltip As New ToolTip()
        tooltip.SetToolTip(categorybtn, "Click to switch category")

        SetupHoverTimer() ' Add this line
        Load_Foods()
    End Sub
    Public Class CirclePicturBox
        Inherits PictureBox

        Private customImage As Image
        Private isHovering As Boolean = False
        Private originalSize As Size
        Private originalLocation As Point

        Public Sub New()
            Me.BackColor = Color.Transparent
            Me.Size = New Size(120, 120)
            ' Set the image layout mode to zoom to help with centering
            Me.SizeMode = PictureBoxSizeMode.Zoom
        End Sub
        Public Property CircleImage As Image
            Get
                Return customImage
            End Get
            Set(value As Image)
                customImage = value
                Me.Invalidate() ' Redraw the control
            End Set
        End Property


        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)

            Dim g As Graphics = e.Graphics
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

            Dim circleRect As New Rectangle(0, 0, Me.Width - 1, Me.Height - 1)

            ' Draw the product image if available
            If customImage IsNot Nothing Then
                Using gp As New Drawing2D.GraphicsPath()
                    gp.AddEllipse(circleRect)
                    g.SetClip(gp)

                    g.DrawImage(customImage, circleRect)

                    g.ResetClip()
                End Using
            Else
                Using brush As New SolidBrush(Color.Gray)
                    g.FillEllipse(brush, circleRect)
                End Using
            End If

            ' Draw border based on hover state
            Dim borderColor As Color = If(isHovering, Color.White, Color.Black)
            Using pen As New Pen(borderColor, 3)
                g.DrawEllipse(pen, circleRect)
            End Using
        End Sub



        Protected Overrides Sub OnMouseEnter(e As EventArgs)
            MyBase.OnMouseEnter(e)

            Try
                ' Store original size and location
                originalSize = Me.Size
                originalLocation = Me.Location

                ' Calculate new size (7% larger)
                Dim growFactor As Double = 1.07
                Dim newWidth As Integer = CInt(Me.Width * growFactor)
                Dim newHeight As Integer = CInt(Me.Height * growFactor)

                ' Calculate position adjustment to keep image centered
                Dim xOffset As Integer = (newWidth - Me.Width) \ 2
                Dim yOffset As Integer = (newHeight - Me.Height) \ 2

                ' Set new size and position
                Me.SuspendLayout()
                Me.Size = New Size(newWidth, newHeight)
                Me.Location = New Point(Me.Location.X - xOffset, Me.Location.Y - yOffset)
                Me.ResumeLayout()

                isHovering = True
                Me.Invalidate()  ' Redraw with hover effect
            Catch ex As Exception
                ' If resizing fails, just show the hover effect without size change
                isHovering = True
                Me.Invalidate()
            End Try
        End Sub

        Protected Overrides Sub OnMouseLeave(e As EventArgs)
            MyBase.OnMouseLeave(e)

            Try
                ' Reset to original size and position if we have stored values
                If Not originalSize.IsEmpty Then
                    Me.SuspendLayout()
                    Me.Size = originalSize
                    Me.Location = originalLocation
                    Me.ResumeLayout()
                End If

                isHovering = False
                Me.Invalidate()  ' Redraw without hover effect
            Catch ex As Exception
                ' If resizing fails, just remove the hover effect
                isHovering = False
                Me.Invalidate()
            End Try
        End Sub

        ' Override the Region property to prevent graphics issues
        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            ' Create a circular region
            Using path As New GraphicsPath()
                path.AddEllipse(0, 0, Me.Width, Me.Height)
                Me.Region = New Region(path)
            End Using
        End Sub
    End Class

    Public Sub LoadCategories()
        categories.Clear()
        categories.Add("All")  ' Always add "All" as the first option

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            cmd = New MySqlCommand("SELECT DISTINCT category FROM tbl_food ORDER BY category", conn)
            dr = cmd.ExecuteReader

            While dr.Read
                If Not dr.IsDBNull(0) Then  ' Check if category is not null
                    categories.Add(dr("category").ToString())
                End If
            End While

        Catch ex As Exception
            MsgBox("Error loading categories: " & ex.Message)
        Finally
            dr.Close()
            If conn.State = ConnectionState.Open Then conn.Close()

        End Try
    End Sub

    Private Sub categorybtn_Click(sender As Object, e As EventArgs) Handles categorybtn.Click
        ' Move to next category
        currentCategoryIndex = (currentCategoryIndex + 1) Mod categories.Count

        ' Update button text to show current category
        categorybtn.Text = categories(currentCategoryIndex)

        ' Filter items based on selected category
        If currentCategoryIndex = 0 Then  ' "All" category
            Load_Foods()
        Else
            FilterByCategory(categories(currentCategoryIndex))
        End If

        UpdateCategoryButtonTooltip()
    End Sub

    Private Sub FilterByCategory(categoryName As String)
        FlowLayoutPanel1.Controls.Clear()
        FlowLayoutPanel1.AutoScroll = True
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            cmd = New MySqlCommand("SELECT `img`, `foodcode`, `foodname` FROM `tbl_food` WHERE category = @category", conn)
            cmd.Parameters.AddWithValue("@category", categoryName)
            dr = cmd.ExecuteReader
            While dr.Read
                Load_controls()
            End While
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            dr.Close()
            If conn.State = ConnectionState.Open Then conn.Close()

        End Try
    End Sub

    Private Sub UpdateCategoryButtonTooltip()
        Dim nextIndex = (currentCategoryIndex + 1) Mod categories.Count
        Dim tooltip As New ToolTip()
        tooltip.SetToolTip(categorybtn, $"Next: {categories(nextIndex)}")
    End Sub

    Public Sub Load_Foods()
        FlowLayoutPanel1.Controls.Clear()
        FlowLayoutPanel1.AutoScroll = True
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            cmd = New MySqlCommand("SELECT img, foodcode, foodname FROM tbl_food", conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Load_controls()
            End While
        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()

        End Try
    End Sub
    Private hoverTimer As New Timer()
    Private currentPanel As Panel = Nothing
    Private originalPanelColor As Color

    Private Sub SetupHoverTimer()
        hoverTimer.Interval = 50
        AddHandler hoverTimer.Tick, AddressOf HoverTimer_Tick
    End Sub

    Private Sub HoverTimer_Tick(sender As Object, e As EventArgs)
        If currentPanel IsNot Nothing Then
            ' Create a pulsing glow effect
            Dim alpha As Integer = 20 + CInt(Math.Abs(Math.Sin(DateTime.Now.Millisecond / 500.0) * 40))
            currentPanel.BackColor = Color.FromArgb(alpha + 40, alpha + 40, alpha + 40)
        End If
    End Sub

    Private Sub StartHoverEffect(panel As Panel)
        currentPanel = panel
        originalPanelColor = panel.BackColor
        hoverTimer.Start()
    End Sub

    Private Sub StopHoverEffect()
        If currentPanel IsNot Nothing Then
            currentPanel.BackColor = originalPanelColor
        End If
        currentPanel = Nothing
        hoverTimer.Stop()
    End Sub

    Sub Load_controls()
        Dim len As Long = dr.GetBytes(0, 0, Nothing, 0, 0)
        Dim array(CInt(len)) As Byte
        dr.GetBytes(0, 0, array, 0, CInt(len))
        Dim foodCode As String = dr.Item("foodcode").ToString

        ' Panel
        pan = New Panel
        With pan
            FlowLayoutPanel1.Padding = New Padding(5)
            .Width = 156
            .Height = 150
            .BackColor = Color.FromArgb(40, 40, 40)
            .Tag = foodCode
            ' Add hover effects to the panel
            AddHandler .MouseEnter, AddressOf Panel_MouseEnter
            AddHandler .MouseLeave, AddressOf Panel_MouseLeave
        End With

        ' Food Image
        img = New CirclePicturBox
        With img
            .Height = 120
            .BackgroundImageLayout = ImageLayout.Stretch
            .Dock = DockStyle.Top
            .Tag = foodCode
            .Name = "img" ' Add name for reference
        End With

        ' Food Name
        foodname = New Label
        With foodname
            .ForeColor = Color.White
            .Font = New Font("Segoe UI", 8, FontStyle.Bold)
            .TextAlign = ContentAlignment.MiddleCenter
            .Dock = DockStyle.Top
            .Tag = foodCode
            .Text = dr.Item("foodname").ToString
            .Name = "foodname" ' Add name for reference
        End With

        ' Assigning food image
        Dim ms As New System.IO.MemoryStream(array)
        Dim bitmap As New System.Drawing.Bitmap(ms)
        img.CircleImage = bitmap

        ' Adding controls to panel in correct order
        pan.Controls.Add(btnEdit)
        pan.Controls.Add(foodname)
        pan.Controls.Add(img)

        FlowLayoutPanel1.Controls.Add(pan)

        ' Adding click events
        AddHandler foodname.Click, AddressOf Selectimg_Click
        AddHandler img.Click, AddressOf ButtonEdit_Click
        AddHandler pan.Click, AddressOf Selectimg_Click
    End Sub
    Private Sub Panel_MouseEnter(sender As Object, e As EventArgs)
        Try
            Dim panel As Panel = DirectCast(sender, Panel)
            panel.BackColor = Color.FromArgb(60, 60, 60)  ' Slightly lighter on hover

            ' Find the button and change its appearance
            For Each ctrl As Control In panel.Controls
                If TypeOf ctrl Is Button AndAlso ctrl.Name = "btnEdit" Then
                    Dim btn As Button = DirectCast(ctrl, Button)
                    btn.BackColor = Color.FromArgb(217, 207, 199)  ' Highlight color
                    Exit For
                End If
            Next
        Catch ex As Exception
            ' Silently handle any errors
        End Try
    End Sub

    Private Sub Panel_MouseLeave(sender As Object, e As EventArgs)
        Try
            Dim panel As Panel = DirectCast(sender, Panel)
            panel.BackColor = Color.FromArgb(40, 40, 40)  ' Original color

            ' Restore the button appearance
            For Each ctrl As Control In panel.Controls
                If TypeOf ctrl Is Button AndAlso ctrl.Name = "btnEdit" Then
                    Dim btn As Button = DirectCast(ctrl, Button)
                    btn.BackColor = Color.FromArgb(197, 187, 179)  ' Original button color
                    Exit For
                End If
            Next
        Catch ex As Exception
            ' Silently handle any errors
        End Try
    End Sub

    Private Sub ButtonEdit_Click(sender As Object, e As EventArgs)
        Dim foodCode As String = sender.Tag.ToString

        Dim editForm As New EditForm()
        editForm.FoodCode = foodCode
        If editForm.ShowDialog() = DialogResult.OK Then
            Load_Foods()
        End If
    End Sub

    Private Sub Selectimg_Click(sender As Object, e As EventArgs)
        ' Handle image or panel click events if necessary.
    End Sub

    Private Sub txt_search_TextChanged(sender As Object, e As EventArgs) Handles txt_search.TextChanged
        FlowLayoutPanel1.Controls.Clear()
        FlowLayoutPanel1.AutoScroll = True
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            cmd = New MySqlCommand("SELECT img, foodcode, foodname FROM tbl_food WHERE foodcode LIKE @search OR foodname LIKE @search", conn)
            cmd.Parameters.AddWithValue("@search", "%" & txt_search.Text & "%")
            dr = cmd.ExecuteReader
            While dr.Read
                Load_controls()
            End While
        Catch ex As Exception
            ' Handle exception silently
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()

        End Try
    End Sub

    Private Sub btn_ManageAddons_Click(sender As Object, e As EventArgs) Handles btn_ManageAddons.Click
        frm_ManageAddons.ShowDialog()
    End Sub
End Class
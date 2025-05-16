Imports System.IO
Imports MySql.Data.MySqlClient
Imports System.Drawing
Imports System.Drawing.Imaging

Public Class Cashier

    Dim connString As String = "server=localhost;port=3307;user=root;password=;database=brewtopia_db"
    Private WithEvents pan As Panel
    Private WithEvents pan_top As Panel
    Private WithEvents foodcode As Label
    Private WithEvents foodname As Label
    Private WithEvents img As CirclePicturBox
    Private CriticalStockTimer As Timer



    Public Sub UpdateBalanceAmount()
        Try
            Dim grandtotal As Decimal = 0
            For i As Integer = 0 To a.Rows.Count() - 1
                grandtotal += CDec(a.Rows(i).Cells(6).Value)
            Next

            If Not String.IsNullOrEmpty(txt_receivedAmount.Text) AndAlso Decimal.TryParse(txt_receivedAmount.Text, Nothing) Then
                txt_BalanceAmount.Text = Format(CDec(txt_receivedAmount.Text) - grandtotal, "#,##0.00")
                lbl_tot.Text = Format(grandtotal, "#,##0.00")
            End If
        Catch ex As Exception
            ' Handle any errors that might occur during calculation
        End Try
    End Sub
    Private Sub txt_receivedAmount_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_receivedAmount.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevents the beep sound
            btnRecord.PerformClick() ' Triggers the button click
        End If
    End Sub
    Private Sub CheckCriticalStockForCashier(Optional forcePlaySound As Boolean = False)
        If Me.IsDisposed OrElse Me.Disposing Then Exit Sub
        If Panelnotif Is Nothing OrElse Panelnotif.IsDisposed OrElse Panelnotif.Disposing Then Exit Sub

        Try
            ' Clear the panel first
            Panelnotif.Controls.Clear()

            Dim connectionString As String = "server=localhost;port=3307;user=root;password=;database=brewtopia_db"
            Using conn As New MySql.Data.MySqlClient.MySqlConnection(connectionString)
                conn.Open()

                Dim cmd As New MySql.Data.MySqlClient.MySqlCommand("SELECT name, quantity FROM tbl_inventoryad WHERE quantity < 11", conn)
                Dim reader As MySql.Data.MySqlClient.MySqlDataReader = cmd.ExecuteReader()

                Dim hasCriticalItems As Boolean = False

                While reader.Read()
                    hasCriticalItems = True
                    Exit While
                End While

                If hasCriticalItems Then
                    Dim popup As New PosPopup()
                    popup.TopLevel = False
                    popup.FormBorderStyle = FormBorderStyle.None
                    popup.Dock = DockStyle.Fill

                    Panelnotif.Controls.Add(popup)

                    Dim wasVisible As Boolean = Panelnotif.Visible
                    Panelnotif.Visible = True
                    popup.Show()

                    If Not wasVisible Or forcePlaySound Then
                        System.Media.SystemSounds.Exclamation.Play()
                    End If
                Else
                    Panelnotif.Visible = False
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error checking critical stock: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_receivedAmount_TextChanged(sender As Object, e As EventArgs) Handles txt_receivedAmount.TextChanged
        Try
            ' Limit to 5 digits or 99999
            If txt_receivedAmount.Text.Length > 0 Then
                Dim value As Decimal
                If Decimal.TryParse(txt_receivedAmount.Text, value) Then
                    ' Check if the value exceeds 99999
                    If value > 99999 Then
                        txt_receivedAmount.Text = "99999"
                        txt_receivedAmount.SelectionStart = txt_receivedAmount.Text.Length ' Keep cursor at the end
                    End If

                    ' Remove leading zeros
                    If value > 0 AndAlso txt_receivedAmount.Text.StartsWith("0") Then
                        value = CDec(txt_receivedAmount.Text)
                        txt_receivedAmount.Text = value.ToString()
                        txt_receivedAmount.SelectionStart = txt_receivedAmount.Text.Length
                    End If
                Else
                    txt_receivedAmount.Text = ""
                End If
            End If

            ' Update grand total and balance amount
            Dim grandtotal As Decimal = 0
            For i As Integer = 0 To a.Rows.Count() - 1
                grandtotal += CDec(a.Rows(i).Cells(6).Value) ' Match subtotal column
            Next

            If Not String.IsNullOrEmpty(txt_receivedAmount.Text) AndAlso Decimal.TryParse(txt_receivedAmount.Text, Nothing) Then
                ' Format correctly to avoid truncation issues
                Dim balance As Decimal = CDec(txt_receivedAmount.Text) - grandtotal
                txt_BalanceAmount.Text = Format(balance, "#,##0.00")
                lbl_tot.Text = Format(grandtotal, "#,##0.00")
            Else
                txt_BalanceAmount.Text = ""
                lbl_tot.Text = Format(grandtotal, "#,##0.00")
            End If
        Catch ex As Exception
            ' Log the error
            Debug.WriteLine("Error in txt_receivedAmount_TextChanged: " & ex.Message)
        End Try
    End Sub



    Private Sub txt_receivedAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_receivedAmount.KeyPress
        ' Allow numbers, one decimal point, and control keys
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." Then
            e.Handled = True
        End If

        ' Allow only one decimal point
        If e.KeyChar = "." AndAlso txt_receivedAmount.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub Get_grandTotal()
        Try
            Dim grandtotal As Double = 0
            For i As Double = 0 To a.Rows.Count() - 1 Step +1
                grandtotal = grandtotal + a.Rows(i).Cells(6).Value  ' Changed from Cells(5) to Cells(6) to match subtotal column
            Next
            lbl_overallTotal.Text = Format(CDec(grandtotal), "PHP #,##0.00")
            lbl_GrandTotal.Text = Format(CDec(grandtotal), "PHP #,##0.00")
            lbl_tot.Text = Format(grandtotal, "#,##0.00")
        Catch ex As Exception
            ' Log the error or show a message if needed
        End Try
    End Sub

    Private Sub Get_pricedata()
        Try
            Dim totalQuantity As Double = 0
            For i As Double = 0 To a.Rows.Count() - 1 Step +1
                totalQuantity = totalQuantity + CDbl(a.Rows(i).Cells(5).Value)  ' Changed from Cells(4) to Cells(5) to match quantity column
            Next
            lbl_noOfProducts.Text = totalQuantity.ToString()
        Catch ex As Exception
            ' Log the error or show a message if needed
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Get_grandTotal()
        Get_pricedata()
        lbl_date1.Text = Date.Now.ToString("ddd, dd-MM-yyyy")
        lbl_time.Text = Date.Now.ToString("hh:mm:ss tt")
    End Sub

    Private Sub Cashier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbconn()
        lbl_time1.Text = Date.Now.ToString("HH:mm:ss")
        lbl_date.Text = Date.Now.ToString("yyyy-MM-dd")
        InitializeDataGridView()
        UpdateButtonVisibility()
        Load_Foods()
        auto_Transno()

        Timer1 = New Timer()
        Timer1.Interval = 1000
        Timer1.Start()

        txt_transno.ReadOnly = True
        txt_transno.TabStop = False
        txt_transno.BackColor = SystemColors.Control

        LoadCategories()
        categorybtn.Text = categories(0)

        Dim tooltip As New ToolTip()
        tooltip.SetToolTip(categorybtn, "Click to switch category")

        ' --- FIXED HERE ---
        CriticalStockTimer = New Timer()
        CriticalStockTimer.Interval = 60000
        AddHandler CriticalStockTimer.Tick, AddressOf CriticalStockTimer_Tick
        CriticalStockTimer.Start()

        ' Initial check
        CheckCriticalStockForCashier()
    End Sub

    Private Sub CriticalStockTimer_Tick(sender As Object, e As EventArgs)
        ' Periodically check for critical stock
        CheckCriticalStockForCashier()
    End Sub
    Private Sub CashierForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If CriticalStockTimer IsNot Nothing Then
            CriticalStockTimer.Stop()
            CriticalStockTimer.Dispose()
        End If
    End Sub




    Private Sub InitializeDataGridView()
        a.RowTemplate.Height = 30
        a.Columns.Clear()
        a.Columns.Add("No", "No")
        a.Columns.Add("FoodCode", "Food Code")
        a.Columns.Add("FoodName", "Food Name")
        a.Columns.Add("Size", "Size")
        a.Columns.Add("Price", "Price")
        a.Columns.Add("Quantity", "Quantity")
        a.Columns.Add("totalprice", "Subtotal")

        ' Set column properties
        a.Columns("Price").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        a.Columns("totalprice").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        a.Columns("No").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        a.Columns("FoodCode").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        a.Columns("FoodName").Width = 150
        a.Columns("Size").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        a.Columns("Price").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        a.Columns("Quantity").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        a.Columns("totalprice").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        ' Center text in the "Quantity" column
        a.Columns("Quantity").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        ' Hide the FoodCode column
        a.Columns("FoodCode").Visible = False

        ' Enable text wrapping for the "FoodName" and "Size" columns
        Dim wrapStyle As New DataGridViewCellStyle()
        wrapStyle.WrapMode = DataGridViewTriState.True
        wrapStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        a.Columns("FoodName").DefaultCellStyle = wrapStyle
        a.Columns("Size").DefaultCellStyle = wrapStyle

        ' Adjust the row height to accommodate multiline text
        a.RowTemplate.Height = 50
        a.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

        ' Set DataGridView properties
        a.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        a.MultiSelect = False
        a.AllowUserToAddRows = False
        a.AllowUserToDeleteRows = False
        a.ReadOnly = True
    End Sub

    Private Sub auto_Transno()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            cmd = New MySqlCommand("SELECT * FROM `tbl_pos` ORDER BY id DESC", conn)
            dr = cmd.ExecuteReader
            dr.Read()

            If dr.HasRows Then
                txt_transno.Text = (CInt(dr.Item("transno")) + 1).ToString()
            Else
                txt_transno.Text = Date.Now.ToString("yyyyMM") & "001"
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            dr.Close()
            If conn.State = ConnectionState.Open Then conn.Close()

        End Try
    End Sub

    Private Sub Load_Foods()
        FlowLayoutPanel1.Controls.Clear()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            cmd = New MySqlCommand("SELECT `img`, `foodcode`, `foodname` FROM `tbl_food`", conn)
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

    Private Function HasSufficientIngredients(foodCode As String) As Boolean
        Dim checkConnection As New MySqlConnection(connString)  ' Create a new connection
        Try
            checkConnection.Open()

            ' First, get all sizes and their ingredients for this food
            Dim checkIngredientsCmd As New MySqlCommand(
            "SELECT i.size_name, i.itemcode, i.quantity, inv.quantity as inventory_qty " &
            "FROM tbl_ingredients i " &
            "INNER JOIN tbl_inventory inv ON i.itemcode = inv.itemcode " &
            "WHERE i.foodcode = @foodcode", checkConnection)

            checkIngredientsCmd.Parameters.AddWithValue("@foodcode", foodCode)

            ' Create a dictionary to store size availability
            Dim sizeAvailability As New Dictionary(Of String, Boolean)

            Using reader As MySqlDataReader = checkIngredientsCmd.ExecuteReader()
                While reader.Read()
                    Dim sizeName As String = reader("size_name").ToString()
                    Dim requiredQty As Decimal = Decimal.Parse(reader("quantity").ToString())
                    Dim inventoryQty As Decimal = Decimal.Parse(reader("inventory_qty").ToString())

                    ' If this size hasn't been checked yet, assume it's available
                    If Not sizeAvailability.ContainsKey(sizeName) Then
                        sizeAvailability(sizeName) = True
                    End If

                    ' If any ingredient is insufficient, mark the size as unavailable
                    If requiredQty > inventoryQty Then
                        sizeAvailability(sizeName) = False
                    End If
                End While
            End Using

            ' If all sizes are unavailable, return false
            Return sizeAvailability.Values.Any(Function(available) available = True)

        Catch ex As Exception
            MessageBox.Show("Error checking ingredients: " & ex.Message)
            Return False
        Finally
            If checkConnection IsNot Nothing AndAlso checkConnection.State = ConnectionState.Open Then
                checkConnection.Close()
                checkConnection.Dispose()
            End If
        End Try
    End Function

    Private Sub Load_controls()
        Try
            ' Retrieve image data
            Dim len As Long = dr.GetBytes(0, 0, Nothing, 0, 0)
            Dim array(CInt(len)) As Byte
            dr.GetBytes(0, 0, array, 0, CInt(len))
            Dim foodCode As String = dr.Item("foodcode").ToString()
            Dim foodNameStr As String = dr.Item("foodname").ToString()

            ' Store the image data in memory
            Dim ms As New MemoryStream(array)
            Dim originalImage As New Bitmap(ms)

            ' Check availability using the separate connection
            Dim isAvailable As Boolean = HasSufficientIngredients(foodCode)

            ' Create panel with consistent properties
            pan = New Panel With {
            .Width = 140,
            .Height = 140,
            .BackColor = Color.Black,
            .Tag = foodCode,
            .Enabled = True,
            .Padding = New Padding(0)  ' Default padding
        }

            ' Create image control
            img = New CirclePicturBox With {
            .Height = 100,
            .BackgroundImageLayout = ImageLayout.Stretch,
            .Dock = DockStyle.Top,
            .Tag = foodCode
        }

            ' Handle image availability
            If Not isAvailable Then
                ' Create grayscale version with proper alpha handling
                Dim grayImage As New Bitmap(originalImage.Width, originalImage.Height)
                Using g As Graphics = Graphics.FromImage(grayImage)
                    Dim colorMatrix As New ColorMatrix(
                New Single()() {
                    New Single() {0.3F, 0.3F, 0.3F, 0, 0},
                    New Single() {0.3F, 0.3F, 0.3F, 0, 0},
                    New Single() {0.3F, 0.3F, 0.3F, 0, 0},
                    New Single() {0, 0, 0, 0.5F, 0},  ' Reduced opacity to 50%
                    New Single() {0, 0, 0, 0, 1}
                })
                    Dim attributes As New ImageAttributes()
                    attributes.SetColorMatrix(colorMatrix)

                    g.DrawImage(originalImage,
                New Rectangle(0, 0, originalImage.Width, originalImage.Height),
                0, 0, originalImage.Width, originalImage.Height,
                GraphicsUnit.Pixel, attributes)
                End Using
                img.BackgroundImage = grayImage
            Else
                img.BackgroundImage = originalImage
            End If

            ' Create label with proper color handling
            foodname = New Label With {
            .ForeColor = If(isAvailable, Color.White, Color.Gray),  ' Changed unavailable color to Gray
            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
            .TextAlign = ContentAlignment.MiddleCenter,
            .Dock = DockStyle.Top,
            .Text = foodNameStr,
            .Tag = foodCode
        }

            ' Add controls to panel
            pan.Controls.Add(img)
            pan.Controls.Add(foodname)
            FlowLayoutPanel1.Controls.Add(pan)

            ' Add event handlers for available items
            If isAvailable Then
                AddHandler pan.Click, AddressOf Selectimg_Click
                AddHandler img.Click, AddressOf Selectimg_Click
                AddHandler foodname.Click, AddressOf Selectimg_Click

                ' Add hover effect event handlers
                AddHandler pan.MouseEnter, AddressOf Panel_MouseEnter
                AddHandler pan.MouseLeave, AddressOf Panel_MouseLeave

                ' Add hover effects to child controls too
                AddHandler img.MouseEnter, AddressOf Control_MouseEnter
                AddHandler img.MouseLeave, AddressOf Control_MouseLeave
                AddHandler foodname.MouseEnter, AddressOf Control_MouseEnter
                AddHandler foodname.MouseLeave, AddressOf Control_MouseLeave
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading controls: " & ex.Message)
        End Try
    End Sub

    ' Handles mouse enter event for panel
    Private Sub Panel_MouseEnter(sender As Object, e As EventArgs)
        Try
            ' Apply white border effect with smooth transition
            Dim panel As Panel = DirectCast(sender, Panel)
            panel.BackColor = Color.FromArgb(40, 40, 40)  ' Slightly lighter background
            panel.Padding = New Padding(2)  ' Add padding to create border effect
            panel.BorderStyle = BorderStyle.FixedSingle  ' Add border
            panel.Cursor = Cursors.Hand  ' Change cursor to hand
        Catch ex As Exception
            ' Handle any errors silently
        End Try
    End Sub

    ' Handles mouse leave event for panel
    Private Sub Panel_MouseLeave(sender As Object, e As EventArgs)
        Try
            ' Restore original appearance
            Dim panel As Panel = DirectCast(sender, Panel)
            panel.BackColor = Color.Black  ' Restore original background
            panel.Padding = New Padding(0)  ' Remove padding
            panel.BorderStyle = BorderStyle.None  ' Remove border
            panel.Cursor = Cursors.Default  ' Restore default cursor
        Catch ex As Exception
            ' Handle any errors silently
        End Try
    End Sub

    ' Handles mouse enter for child controls (bubble up to parent panel)
    Private Sub Control_MouseEnter(sender As Object, e As EventArgs)
        Try
            ' Get the parent panel and trigger its MouseEnter event
            Dim control As Control = DirectCast(sender, Control)
            Dim parentPanel As Panel = DirectCast(control.Parent, Panel)
            Panel_MouseEnter(parentPanel, e)
        Catch ex As Exception
            ' Handle any errors silently
        End Try
    End Sub

    ' Handles mouse leave for child controls (bubble up to parent panel)
    Private Sub Control_MouseLeave(sender As Object, e As EventArgs)
        Try
            ' Get the parent panel and trigger its MouseLeave event
            Dim control As Control = DirectCast(sender, Control)
            Dim parentPanel As Panel = DirectCast(control.Parent, Panel)

            ' Only restore if mouse is not over the panel or any of its children
            If Not IsMouseOverControl(parentPanel) Then
                Panel_MouseLeave(parentPanel, e)
            End If
        Catch ex As Exception
            ' Handle any errors silently
        End Try
    End Sub

    ' Helper function to check if mouse is over a control or any of its children
    Private Function IsMouseOverControl(ctrl As Control) As Boolean
        Dim mousePos As Point = ctrl.PointToClient(Cursor.Position)
        If ctrl.ClientRectangle.Contains(mousePos) Then
            Return True
        End If

        ' Check all child controls
        For Each child As Control In ctrl.Controls
            mousePos = child.PointToClient(Cursor.Position)
            If child.ClientRectangle.Contains(mousePos) Then
                Return True
            End If
        Next

        Return False
    End Function

    Public Sub Selectimg_Click(sender As Object, e As EventArgs)
        Dim foodCode As String = sender.Tag.ToString()

        ' Get the food name from the related control
        Dim foodName As String = ""
        If TypeOf sender Is Label Then
            foodName = DirectCast(sender, Label).Text
        Else
            ' If clicked on panel or image, find the label in the panel
            Dim panel As Panel = If(TypeOf sender Is Panel,
              DirectCast(sender, Panel),
              DirectCast(sender, CirclePicturBox).Parent)
            For Each ctrl As Control In panel.Controls
                If TypeOf ctrl Is Label Then
                    foodName = DirectCast(ctrl, Label).Text
                    Exit For
                End If
            Next
        End If

        ' Check inventory availability before even showing the size selection
        If HasSufficientIngredients(foodCode) Then
            ' Pass both foodCode and foodName to the formsizes form
            Dim sizeForm As New formsizes(foodCode, foodName, Me)  ' Pass 'Me' to reference the current Cashier form
            sizeForm.ShowDialog()

            ' Get the selected size and price
            If sizeForm.SelectedSize IsNot Nothing Then
                Dim selectedSize = sizeForm.SelectedSize
                Dim selectedPrice = sizeForm.SelectedPrice
                Dim sugarQty = sizeForm.SelectedSugarQuantity  ' Get sugar quantity

                ' Double-check if we have enough inventory for the selected size with sugar
                If HasSufficientInventoryForOrder(foodCode, selectedSize, 1, sugarQty) Then
                    AddItemToCart(foodCode, selectedSize, selectedPrice, foodName, sizeForm.SelectedAddons, sugarQty)
                Else
                    MessageBox.Show("Not enough inventory available for this item with the selected size and sugar level.", "Insufficient Inventory", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        Else
            MessageBox.Show("This item is currently unavailable due to insufficient inventory.", "Item Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub AddItemToCart(foodCode As String, sizeName As String, price As Decimal, foodName As String, addons As List(Of formsizes.AddonItem), sugarQuantity As Integer)
        Dim exist As Boolean = False
        Dim numRow As Integer = 0

        ' Create addon description
        Dim addonDesc As String = ""
        If addons IsNot Nothing AndAlso addons.Count > 0 Then
            addonDesc = " with " & String.Join(", ", addons.Select(Function(a) a.AddonName))
        End If

        ' Check if item with same addons exists
        For Each itm As DataGridViewRow In a.Rows
            If itm.Cells(1).Value.ToString() = foodCode AndAlso
               itm.Cells(3).Value.ToString() = sizeName AndAlso
               itm.Cells(2).Value.ToString() = foodName & addonDesc Then
                exist = True
                numRow = itm.Index
                Exit For
            End If
        Next

        If Not exist Then
            ' Add new row with addons
            Dim subtotal As Decimal = price
            a.Rows.Add(a.Rows.Count + 1, foodCode, foodName & addonDesc, sizeName, price, 1, subtotal)
            ' Store sugar quantity in the Tag property of the row
            If a.Rows.Count > 0 Then
                a.Rows(a.Rows.Count - 1).Tag = sugarQuantity
            End If
        Else
            ' Update existing row
            a.Rows(numRow).Cells(5).Value = CInt(a.Rows(numRow).Cells(5).Value) + 1
            a.Rows(numRow).Cells(6).Value = CDec(a.Rows(numRow).Cells(4).Value) * CInt(a.Rows(numRow).Cells(5).Value)
        End If



        CalculateTotal()
        UpdateNoOfProducts()
        UpdateButtonVisibility()
        UpdateBalanceAmount()
    End Sub

    Private Sub CalculateTotal()
        Try
            Dim total As Decimal = 0
            For Each row As DataGridViewRow In a.Rows
                If row.Cells(6).Value IsNot Nothing Then
                    total += CDec(row.Cells(6).Value)
                End If
            Next
            lbl_GrandTotal.Text = total.ToString("N2")
        Catch ex As Exception
            MessageBox.Show("Error calculating total: " & ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles a.CellClick
        Try
            If e.RowIndex >= 0 Then  ' Make sure we didn't click a header
                Dim row = a.Rows(e.RowIndex)
                ' Store the current row index for use with quantity buttons
                a.CurrentCell = a.Rows(e.RowIndex).Cells(0)
                UpdateNoOfProducts() ' Update the total number of items when quantity changes
            End If
        Catch ex As Exception
            MessageBox.Show("Error selecting item: " & ex.Message)
        End Try
    End Sub

    Private Sub txtquantity_TextChanged(sender As Object, e As EventArgs) Handles txtquantity.TextChanged
        Try
            If a.CurrentRow IsNot Nothing Then
                Dim row = a.CurrentRow
                Dim price As Decimal = CDec(row.Cells(4).Value)
                Dim foodCode = row.Cells(1).Value.ToString()
                Dim sizeName = row.Cells(3).Value.ToString()
                Dim currentQty = CInt(row.Cells(5).Value)
                Dim newQty As Integer

                ' Get sugar quantity from row's Tag property
                Dim sugarQty As Integer = 0
                If row.Tag IsNot Nothing AndAlso TypeOf row.Tag Is Integer Then
                    sugarQty = CInt(row.Tag)
                End If

                ' Skip validation if the textbox is empty
                If String.IsNullOrWhiteSpace(txtquantity.Text) Then
                    Return
                End If

                ' Remove leading zeros
                If txtquantity.Text.StartsWith("0") AndAlso txtquantity.Text.Length > 1 Then
                    Dim cleanValue As Integer
                    If Integer.TryParse(txtquantity.Text, cleanValue) Then
                        txtquantity.Text = cleanValue.ToString()
                        txtquantity.SelectionStart = txtquantity.Text.Length
                    End If
                End If

                ' Validate the input in txtquantity
                If Integer.TryParse(txtquantity.Text, newQty) Then
                    If newQty > 0 Then
                        ' Temporarily set quantity to 0 to exclude current item from cart calculation
                        row.Cells(5).Value = 0

                        ' Check if we have enough inventory for the new quantity with sugar
                        If HasSufficientInventoryForOrder(foodCode, sizeName, newQty, sugarQty) Then
                            ' Update quantity and subtotal
                            row.Cells(5).Value = newQty
                            row.Cells(6).Value = price * newQty
                            CalculateTotal()
                            UpdateNoOfProducts()
                            UpdateBalanceAmount()
                        Else
                            ' Restore original quantity
                            row.Cells(5).Value = currentQty
                            MessageBox.Show("Cannot set this quantity. Not enough inventory available considering items already in cart.", "Insufficient Inventory", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            txtquantity.Text = currentQty.ToString()
                        End If
                    Else
                        ' Restore original quantity
                        row.Cells(5).Value = currentQty
                        If txtquantity.Text.Trim() <> "" Then
                            MessageBox.Show("Please enter a valid quantity (greater than 0).", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            txtquantity.Text = currentQty.ToString()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error updating quantity: " & ex.Message)
        End Try
    End Sub

    ' Add this handler for the Enter key
    Private Sub txtquantity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtquantity.KeyPress
        ' If Enter key is pressed
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            ' Prevent the Enter key from triggering another TextChanged event
            e.Handled = True
            ' Move focus to another control or process the entry
            SendKeys.Send("{TAB}")
        End If
        ' Allow only numeric characters and control keys (e.g., Backspace)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True ' Suppress the invalid keypress
        End If
    End Sub

    Private Sub btnPlus_Click(sender As Object, e As EventArgs) Handles btnPlus.Click
        Try
            If a.CurrentRow IsNot Nothing Then
                Dim row = a.CurrentRow
                Dim foodCode = row.Cells(1).Value.ToString()
                Dim sizeName = row.Cells(3).Value.ToString()
                Dim currentQty = CInt(row.Cells(5).Value)
                Dim price = CDec(row.Cells(4).Value)

                ' Get sugar quantity from row's Tag property
                Dim sugarQty As Integer = 0
                If row.Tag IsNot Nothing AndAlso TypeOf row.Tag Is Integer Then
                    sugarQty = CInt(row.Tag)
                End If

                ' Temporarily reduce the current item's quantity by 1 in the DataGridView
                row.Cells(5).Value = currentQty - 1

                ' Check if we have enough inventory for the increased quantity
                If HasSufficientInventoryForOrder(foodCode, sizeName, 1, sugarQty) Then
                    ' Restore and increment the quantity
                    currentQty += 1
                    row.Cells(5).Value = currentQty

                    ' Update subtotal
                    row.Cells(6).Value = price * currentQty

                    CalculateTotal()
                    UpdateNoOfProducts()
                    UpdateBalanceAmount()
                Else
                    ' Restore the original quantity
                    row.Cells(5).Value = currentQty
                    MessageBox.Show("Cannot increase quantity. Not enough inventory available considering items already in cart.", "Insufficient Inventory", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error updating quantity: " & ex.Message)
        End Try
    End Sub

    Private Sub btnMinus_Click(sender As Object, e As EventArgs) Handles btnminus.Click
        Try
            If a.CurrentRow IsNot Nothing Then
                Dim row = a.CurrentRow
                Dim currentQty = CInt(row.Cells(5).Value)
                Dim price = CDec(row.Cells(4).Value)

                If currentQty > 1 Then
                    ' Update quantity
                    currentQty -= 1
                    row.Cells(5).Value = currentQty

                    ' Update subtotal
                    row.Cells(6).Value = price * currentQty

                    CalculateTotal()
                    UpdateNoOfProducts()
                    UpdateBalanceAmount() ' Add this line to update balance
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error updating quantity: " & ex.Message)
        End Try
    End Sub


    ' Handle Remove button
    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Try
            If a.CurrentRow IsNot Nothing Then
                If MessageBox.Show("Are you sure you want to remove this item?", "Confirm Remove",
                             MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    a.Rows.Remove(a.CurrentRow)
                    RenumberRows()
                    CalculateTotal()
                    UpdateNoOfProducts()
                    UpdateBalanceAmount()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error removing item: " & ex.Message)
        End Try
        UpdateButtonVisibility()
    End Sub


    Private Sub RenumberRows()
        For i As Integer = 0 To a.Rows.Count - 1
            a.Rows(i).Cells(0).Value = i + 1
        Next
    End Sub

    Private Sub btnNewOrder_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
        Try
            If a.Rows.Count > 0 Then
                If MessageBox.Show("Are you sure you want to start a new order?", "Confirm New Order",
                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    a.Rows.Clear()
                    lbl_GrandTotal.Text = "0.00"
                    auto_Transno()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error creating new order: " & ex.Message)
        End Try
    End Sub

    Private Sub btnRecord_Click_1(sender As Object, e As EventArgs) Handles btnRecord.Click
        If MsgBox("Are You Sure Order Confirm?", vbQuestion + vbYesNo) = vbYes Then
            If txt_receivedAmount.Text = String.Empty Then
                MsgBox("Please Enter Received Amount!", vbExclamation)
                Return
            ElseIf CDec(txt_BalanceAmount.Text) < 0 Then
                MsgBox("Insufficient Payment!" & vbNewLine & "Amount needed: PHP " & Format(Math.Abs(CDec(txt_BalanceAmount.Text)) + CDec(txt_receivedAmount.Text), "#,##0.00"), MsgBoxStyle.Exclamation)
                Return
            Else
                Try
                    If conn.State = ConnectionState.Closed Then conn.Open()

                    ' Start transaction
                    Dim transaction As MySqlTransaction = conn.BeginTransaction()
                    Try
                        ' Insert transaction details into tbl_pos
                        cmd = New MySqlCommand("INSERT INTO `tbl_pos`(`transno`, `transdate`, `cashiername`, `foodcode`, `foodname`, `price`, `qty`, `totalprice`, `grandtotal`, `nooffoods`, `amountreceived`, `changes`) VALUES (@transno,@transdate,@cashiername,@foodcode,@foodname,@price,@qty,@totalprice,@grandtotal,@nooffoods,@amountreceived,@changes)", conn, transaction)

                        Dim transDateTime As DateTime = DateTime.ParseExact(lbl_date.Text & " " & lbl_time1.Text, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)

                        For j As Integer = 0 To a.Rows.Count - 1
                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@transno", txt_transno.Text)
                            cmd.Parameters.AddWithValue("@transdate", transDateTime)
                            cmd.Parameters.AddWithValue("@cashiername", lblUsername.Text)
                            cmd.Parameters.AddWithValue("@foodcode", a.Rows(j).Cells(1).Value)

                            Dim foodname As String = a.Rows(j).Cells(2).Value.ToString()
                            cmd.Parameters.AddWithValue("@foodname", foodname)

                            cmd.Parameters.AddWithValue("@price", a.Rows(j).Cells(4).Value)
                            cmd.Parameters.AddWithValue("@qty", a.Rows(j).Cells(5).Value)
                            cmd.Parameters.AddWithValue("@totalprice", a.Rows(j).Cells(6).Value)
                            cmd.Parameters.AddWithValue("@grandtotal", Format(CDec(lbl_tot.Text), "0.00"))
                            cmd.Parameters.AddWithValue("@nooffoods", CInt(lbl_noOfProducts.Text))
                            cmd.Parameters.AddWithValue("@amountreceived", Format(CDec(txt_receivedAmount.Text), "0.00"))
                            cmd.Parameters.AddWithValue("@changes", Format(CDec(txt_BalanceAmount.Text), "0.00"))
                            cmd.ExecuteNonQuery()

                            ' Inside btnRecord_Click_1
                            ' Extract addon information from the foodname (if any)
                            Dim addons As New List(Of formsizes.AddonItem)
                            If foodname.Contains(" with ") Then
                                Dim addonNames = foodname.Split(New String() {" with "}, StringSplitOptions.None)(1).Split(",")
                                For Each addonName In addonNames
                                    Dim trimmedAddonName = addonName.Trim()
                                    If Not String.IsNullOrEmpty(trimmedAddonName) Then
                                        ' Get addon information from the database
                                        Dim addonCmd As New MySqlCommand(
                                            "SELECT a.addoncode, a.name, s.itemcode, s.quantity " &
                                            "FROM tbl_addons a " &
                                            "INNER JOIN tbl_addon_supplies s ON a.addoncode = s.addoncode " &
                                            "WHERE a.name = @name", conn, transaction)
                                        addonCmd.Parameters.AddWithValue("@name", trimmedAddonName)

                                        Using addonReader As MySqlDataReader = addonCmd.ExecuteReader()
                                            If addonReader.Read() Then
                                                addons.Add(New formsizes.AddonItem With {
                                                    .AddonCode = addonReader("addoncode").ToString(),
                                                    .AddonName = addonReader("name").ToString(),
                                                    .ItemCode = addonReader("itemcode").ToString(),
                                                    .Quantity = CDec(addonReader("quantity"))
                                                })
                                            End If
                                        End Using
                                    Else
                                        Debug.WriteLine($"Addon name '{addonName}' was empty or invalid.")
                                    End If
                                Next
                            Else
                                Debug.WriteLine("No addons found in foodname.")
                            End If

                            ' Log addons found
                            Debug.WriteLine($"Number of addons: {addons.Count}")
                            For Each addon In addons
                                Debug.WriteLine($"Addon found: {addon.AddonName}, Code: {addon.AddonCode}, ItemCode: {addon.ItemCode}, Quantity: {addon.Quantity}")
                            Next

                            ' Deduct inventory for the current foodcode and size with addons
                            ' Get sugar quantity from row Tag (if available)
                            Dim sugarQty As Integer = 0
                            If a.Rows(j).Tag IsNot Nothing AndAlso TypeOf a.Rows(j).Tag Is Integer Then
                                sugarQty = CInt(a.Rows(j).Tag)
                            End If

                            DeductInventory(a.Rows(j).Cells(1).Value.ToString(), a.Rows(j).Cells(3).Value.ToString(), CInt(a.Rows(j).Cells(5).Value), addons, transaction, sugarQty)

                        Next

                        transaction.Commit()
                        MsgBox("Transaction completed successfully!", vbInformation)

                        If MsgBox("Print Bill?", vbQuestion + vbYesNo) = vbYes Then
                            Try
                                System.Threading.Thread.Sleep(100)
                                Using printForm As New frm_BillPrint(txt_transno.Text)
                                    printForm.ShowDialog()
                                End Using
                            Catch ex As Exception
                                MessageBox.Show("Error showing bill: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End Try
                        End If
                        new_order()
                        CheckCriticalStockForCashier()

                    Catch ex As Exception
                        transaction.Rollback()
                        MsgBox("Error processing transaction: " & ex.Message, vbExclamation)
                    End Try
                Catch ex As Exception
                    MsgBox("Error: " & ex.Message, vbExclamation)
                Finally
                    If conn.State = ConnectionState.Open Then conn.Close()
                End Try
            End If
        End If

        UpdateButtonVisibility()
    End Sub

    Private Sub DeductInventory(foodCode As String, sizeName As String, quantity As Integer, addons As List(Of formsizes.AddonItem), transaction As MySqlTransaction, Optional sugarQuantity As Integer = 0)
        Try
            Debug.WriteLine($"Starting DeductInventory for foodCode: {foodCode}, size: {sizeName}, quantity: {quantity}")
            Debug.WriteLine($"Sugar quantity: {sugarQuantity}")
            Debug.WriteLine($"Number of addons: {If(addons Is Nothing, "0", addons.Count.ToString())}")

            Dim itemsToDeduct As New List(Of (itemCode As String, quantity As Decimal))

            ' Get ingredients for the food item
            Dim fetchIngredientsCmd As New MySqlCommand(
            "SELECT i.itemcode, i.quantity * @orderQuantity AS totalQty " &
            "FROM tbl_ingredients i " &
            "WHERE i.foodcode = @foodcode AND i.size_name = @size_name", conn, transaction)
            fetchIngredientsCmd.Parameters.AddWithValue("@foodcode", foodCode)
            fetchIngredientsCmd.Parameters.AddWithValue("@size_name", sizeName)
            fetchIngredientsCmd.Parameters.AddWithValue("@orderQuantity", quantity)

            Using reader As MySqlDataReader = fetchIngredientsCmd.ExecuteReader()
                While reader.Read()
                    itemsToDeduct.Add((reader("itemcode").ToString(), CDec(reader("totalQty"))))
                End While
            End Using

            ' Add addon supplies to the deduction list
            If addons IsNot Nothing AndAlso addons.Count > 0 Then
                For Each addon In addons
                    Dim addonQty = addon.Quantity * quantity
                    itemsToDeduct.Add((addon.ItemCode, addonQty))
                    Debug.WriteLine($"Added addon to deduction: AddonName={addon.AddonName}, ItemCode={addon.ItemCode}, Quantity={addonQty}")
                Next
            End If

            ' Add sugar deduction if quantity > 0
            If sugarQuantity > 0 Then
                Dim sugarItemCode As String = "SUG001" ' Sugar inventory item code
                Dim totalSugarQty As Decimal = sugarQuantity * quantity

                ' Check if sugar is already in the items to deduct (from addons list)
                Dim existingSugarItem = itemsToDeduct.Find(Function(item) item.itemCode = sugarItemCode)

                If existingSugarItem.itemCode Is Nothing Then
                    ' Sugar not already in list, add it
                    itemsToDeduct.Add((sugarItemCode, totalSugarQty))
                    Debug.WriteLine($"Added sugar to deduction: ItemCode={sugarItemCode}, Quantity={totalSugarQty}")
                Else
                    ' Sugar already in list (maybe from addon), remove it and add with combined quantity
                    itemsToDeduct.Remove(existingSugarItem)
                    itemsToDeduct.Add((sugarItemCode, existingSugarItem.quantity + totalSugarQty))
                    Debug.WriteLine($"Updated sugar in deduction: ItemCode={sugarItemCode}, Quantity={existingSugarItem.quantity + totalSugarQty}")
                End If
            End If

            ' Deduct inventory with separate handling for quantity and lastquantity
            For Each item In itemsToDeduct
                ' First, get current quantity from inventoryad
                Dim getCurrentQtyCmd As New MySqlCommand(
                "SELECT quantity FROM tbl_inventoryad WHERE itemcode = @itemcode",
                conn, transaction)
                getCurrentQtyCmd.Parameters.AddWithValue("@itemcode", item.itemCode)
                Dim currentQty As Decimal = CDec(getCurrentQtyCmd.ExecuteScalar())

                ' Calculate new quantity
                Dim newQty As Decimal = currentQty - item.quantity
                Debug.WriteLine($"Deducting {item.itemCode}: Current={currentQty}, Deduct={item.quantity}, New={newQty}")

                ' Add inventory check
                If newQty < 0 Then
                    Throw New Exception($"Insufficient inventory for item {item.itemCode}. Required: {item.quantity}, Available: {currentQty}")
                End If

                ' Update tbl_inventory
                Dim deductCmd As New MySqlCommand(
                "UPDATE tbl_inventory " &
                "SET quantity = quantity - @quantity " &
                "WHERE itemcode = @itemcode", conn, transaction)
                deductCmd.Parameters.AddWithValue("@quantity", item.quantity)
                deductCmd.Parameters.AddWithValue("@itemcode", item.itemCode)
                deductCmd.ExecuteNonQuery()

                ' Update tbl_inventoryad
                ' Here we store the current quantity as lastquantity before updating the new quantity
                Dim deductAdCmd As New MySqlCommand(
                "UPDATE tbl_inventoryad " &
                "SET lastquantity = quantity, " & ' Store current quantity as lastquantity
                "    quantity = @newQty " &      ' Update to new quantity
                "WHERE itemcode = @itemcode", conn, transaction)
                deductAdCmd.Parameters.AddWithValue("@newQty", newQty)
                deductAdCmd.Parameters.AddWithValue("@itemcode", item.itemCode)
                deductAdCmd.ExecuteNonQuery()
            Next

            Debug.WriteLine("DeductInventory completed successfully.")
        Catch ex As Exception
            Debug.WriteLine($"Error in DeductInventory: {ex.Message}")
            Throw New Exception("Error deducting inventory: " & ex.Message)
        End Try
    End Sub


    ' Helper method to deduct from both inventory tables
    Private Sub DeductInventoryItem(itemCode As String, quantity As Decimal, transaction As MySqlTransaction)
        ' Deduct from tbl_inventory
        Dim deductInventoryCmd As New MySqlCommand(
        "UPDATE tbl_inventory SET quantity = quantity - @quantity WHERE itemcode = @itemcode",
        conn, transaction)
        deductInventoryCmd.Parameters.AddWithValue("@quantity", quantity)
        deductInventoryCmd.Parameters.AddWithValue("@itemcode", itemCode)
        deductInventoryCmd.ExecuteNonQuery()

        ' Deduct from tbl_inventoryad
        Dim deductInventoryAdCmd As New MySqlCommand(
        "UPDATE tbl_inventoryad SET quantity = quantity - @quantity WHERE itemcode = @itemcode",
        conn, transaction)
        deductInventoryAdCmd.Parameters.AddWithValue("@quantity", quantity)
        deductInventoryAdCmd.Parameters.AddWithValue("@itemcode", itemCode)
        deductInventoryAdCmd.ExecuteNonQuery()
    End Sub

    Sub new_order()
        Load_Foods()
        a.Rows.Clear()
        lbl_date.Text = Date.Now.ToString("yyyy-MM-dd")
        auto_Transno()
        txt_BalanceAmount.Clear()
        txt_receivedAmount.Clear()
        UpdateButtonVisibility()
    End Sub
    Private Sub UpdateNoOfProducts()
        Dim totalItems As Integer = 0
        For Each row As DataGridViewRow In a.Rows
            If row.Cells(5).Value IsNot Nothing Then
                totalItems += CInt(row.Cells(5).Value) ' Add the quantity of each item
            End If
        Next
        lbl_noOfProducts.Text = totalItems.ToString() ' Update the label
    End Sub

    Private Sub btnSetting_Click(sender As Object, e As EventArgs)
        Load_Foods()
        a.Rows.Clear()
        lbl_date.Text = Date.Now.ToString("yyyy-MM-dd")
        auto_Transno()
        txt_BalanceAmount.Clear()
        txt_receivedAmount.Clear()
        UpdateButtonVisibility()
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        ' Show a confirmation dialog
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        ' Check user's decision
        If result = DialogResult.Yes Then
            ' Show the login form
            Login.Show()
            ' Close the current form
            Me.Close()
        End If
    End Sub
    Private Sub txt_search_TextChanged(sender As Object, e As EventArgs) Handles txt_search.TextChanged
        FlowLayoutPanel1.Controls.Clear()
        FlowLayoutPanel1.AutoScroll = True
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            cmd = New MySqlCommand("SELECT `img`, `foodcode`, `foodname` FROM `tbl_food` WHERE foodcode like '%" & txt_search.Text & "%' or foodname like '%" & txt_search.Text & "%'", conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Load_controls()
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If conn.State = ConnectionState.Open Then conn.Close()
    End Sub
    Private categories As List(Of String) = New List(Of String)
    Private currentCategoryIndex As Integer = -1

    Private Sub LoadCategories()
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
    Private Sub UpdateButtonVisibility()

        txtquantity.Enabled = a.Rows.Count > 0
        txt_receivedAmount.Enabled = a.Rows.Count > 0
        btnRecord.Enabled = a.Rows.Count > 0
        btnRemove.Enabled = a.Rows.Count > 0
        btnPlus.Enabled = a.Rows.Count > 0
        btnminus.Enabled = a.Rows.Count > 0
        txt_transno.Enabled = a.Rows.Count > 0
        If a.Rows.Count = 0 Then
            txt_receivedAmount.Text = String.Empty
            txt_BalanceAmount.Text = String.Empty
        End If
        If Not txt_transno.Enabled Then
            txt_transno.ForeColor = Color.Red ' or any color you prefer
        End If
    End Sub
    Private Function HasSufficientInventoryForOrder(foodCode As String, sizeName As String, orderQuantity As Integer, Optional sugarQuantity As Integer = 0) As Boolean
        Dim checkConnection As New MySqlConnection(connString)
        Try
            checkConnection.Open()

            ' Get current cart ingredient usage
            Dim cartUsage As Dictionary(Of String, Decimal) = GetCartIngredientUsage()

            ' Add sugar usage to cart usage if needed
            If sugarQuantity > 0 Then
                Dim sugarItemCode As String = "SUG001"
                Dim totalSugarNeeded As Decimal = sugarQuantity * orderQuantity

                If cartUsage.ContainsKey(sugarItemCode) Then
                    cartUsage(sugarItemCode) += totalSugarNeeded
                Else
                    cartUsage.Add(sugarItemCode, totalSugarNeeded)
                End If
            End If

            ' Query to check if we have enough inventory for all ingredients
            Dim checkCmd As New MySqlCommand(
            "SELECT i.itemcode, i.quantity * @orderQuantity as required_qty, inv.quantity as available_qty " &
            "FROM tbl_ingredients i " &
            "INNER JOIN tbl_inventory inv ON i.itemcode = inv.itemcode " &
            "WHERE i.foodcode = @foodcode AND i.size_name = @size_name", checkConnection)

            checkCmd.Parameters.AddWithValue("@foodcode", foodCode)
            checkCmd.Parameters.AddWithValue("@size_name", sizeName)
            checkCmd.Parameters.AddWithValue("@orderQuantity", orderQuantity)

            Using reader As MySqlDataReader = checkCmd.ExecuteReader()
                While reader.Read()
                    Dim itemCode As String = reader("itemcode").ToString()
                    Dim requiredQty As Decimal = Decimal.Parse(reader("required_qty").ToString())
                    Dim availableQty As Decimal = Decimal.Parse(reader("available_qty").ToString())

                    ' Add existing cart usage for this ingredient
                    If cartUsage.ContainsKey(itemCode) Then
                        requiredQty += cartUsage(itemCode)
                    End If

                    ' If total required quantity exceeds available, return false
                    If requiredQty > availableQty Then
                        Return False
                    End If
                End While
            End Using

            ' Also check sugar specifically if needed
            If sugarQuantity > 0 Then
                Dim sugarItemCode As String = "SUG001"

                ' Skip if we already checked this item from ingredients
                If Not cartUsage.ContainsKey(sugarItemCode) Then
                    Dim checkSugarCmd As New MySqlCommand(
                    "SELECT quantity FROM tbl_inventory WHERE itemcode = @itemcode", checkConnection)
                    checkSugarCmd.Parameters.AddWithValue("@itemcode", sugarItemCode)

                    Dim availableSugar As Decimal = CDec(checkSugarCmd.ExecuteScalar())
                    Dim requiredSugar As Decimal = sugarQuantity * orderQuantity

                    If requiredSugar > availableSugar Then
                        Return False
                    End If
                End If
            End If

            Return True

        Catch ex As Exception
            MessageBox.Show("Error checking inventory: " & ex.Message)
            Return False
        Finally
            If checkConnection IsNot Nothing Then
                checkConnection.Close()
                checkConnection.Dispose()
            End If
        End Try
    End Function
    Private Function GetCartIngredientUsage() As Dictionary(Of String, Decimal)
        Dim ingredientUsage As New Dictionary(Of String, Decimal)
        Dim checkConnection As New MySqlConnection(connString)

        Try
            checkConnection.Open()

            ' For each item in the cart
            For Each row As DataGridViewRow In a.Rows
                Dim foodCode As String = row.Cells(1).Value.ToString()
                Dim sizeName As String = row.Cells(3).Value.ToString()
                Dim quantity As Integer = CInt(row.Cells(5).Value)

                ' Get ingredients for this item
                Dim cmd As New MySqlCommand(
                "SELECT itemcode, quantity * @orderQuantity as total_qty " &
                "FROM tbl_ingredients " &
                "WHERE foodcode = @foodcode AND size_name = @size_name", checkConnection)

                cmd.Parameters.AddWithValue("@foodcode", foodCode)
                cmd.Parameters.AddWithValue("@size_name", sizeName)
                cmd.Parameters.AddWithValue("@orderQuantity", quantity)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim itemCode As String = reader("itemcode").ToString()
                        Dim totalQty As Decimal = Decimal.Parse(reader("total_qty").ToString())

                        ' Add to dictionary
                        If ingredientUsage.ContainsKey(itemCode) Then
                            ingredientUsage(itemCode) += totalQty
                        Else
                            ingredientUsage.Add(itemCode, totalQty)
                        End If
                    End While
                End Using

                ' Add sugar usage from the row's Tag if present
                If row.Tag IsNot Nothing AndAlso TypeOf row.Tag Is Integer Then
                    Dim sugarQty As Integer = CInt(row.Tag)
                    If sugarQty > 0 Then
                        Dim sugarItemCode As String = "SUG001"
                        Dim totalSugarQty As Decimal = sugarQty * quantity

                        If ingredientUsage.ContainsKey(sugarItemCode) Then
                            ingredientUsage(sugarItemCode) += totalSugarQty
                        Else
                            ingredientUsage.Add(sugarItemCode, totalSugarQty)
                        End If
                    End If
                End If
            Next

            Return ingredientUsage

        Catch ex As Exception
            MessageBox.Show("Error calculating cart ingredients: " & ex.Message)
            Return New Dictionary(Of String, Decimal)
        Finally
            If checkConnection IsNot Nothing Then
                checkConnection.Close()
                checkConnection.Dispose()
            End If
        End Try
    End Function

    Private Sub GunaButton3_Click(sender As Object, e As EventArgs) Handles GunaButton3.Click
        ' Show a confirmation dialog
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to exit the application?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        ' Check user's decision
        If result = DialogResult.Yes Then
            ' Close the application
            Application.Exit()
        End If
    End Sub
    Private Sub txt_transno_Enter(sender As Object, e As EventArgs) Handles txt_transno.Enter
        ' Remove focus from the TextBox to hide the blinking cursor
        txt_transno.Parent.Focus() ' Remove focus by setting focus to its parent control
    End Sub

    Private Sub GunaButton2_Click(sender As Object, e As EventArgs)
        Dim changePasswordForm As New frmCashierChangepassword()
        changePasswordForm.lblUsername.Text = lblUseraccname.Text ' Pass the username
        changePasswordForm.ShowDialog()
    End Sub

End Class
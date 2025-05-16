Imports MySql.Data.MySqlClient

Public Class formsizes
    Dim connString As String = "server=localhost;port=3307;user=root;password=;database=brewtopia_db"
    Public SelectedAddons As New List(Of AddonItem)
    ' Add reference to parent Cashier form
    Private parentCashierForm As Cashier

    ' Create a class to store addon information
    Public Class AddonItem
        Public Property AddonName As String
        Public Property Price As Decimal
        Public Property AddonCode As String
        Public Property ItemCode As String
        Public Property Quantity As Decimal
    End Class
    Private foodCode As String
    Private foodName As String
    Public SelectedSize As String = Nothing
    Public SelectedPrice As Decimal = 0

    Public Sub New(code As String, name As String, cashierForm As Cashier)
        InitializeComponent()
        foodCode = code
        foodName = name
        parentCashierForm = cashierForm
    End Sub

    Private Sub formsizes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Existing code...
        foodnamelbl.Text = foodName
        LoadSizes()
        LoadAddons()

        ' Add this line to load sugar levels
        LoadSugarLevels()
    End Sub

    Private Sub LoadSizes()
        FlowLayoutPanel1.Controls.Clear()
        Try
            dbconn()
            If conn.State = ConnectionState.Closed Then conn.Open()

            cmd = New MySqlCommand("SELECT `size_name`, `price` FROM `tbl_food_sizes` WHERE `foodcode` = @foodcode", conn)
            cmd.Parameters.AddWithValue("@foodcode", foodCode)
            dr = cmd.ExecuteReader()

            While dr.Read()
                Dim sizeName = dr("size_name").ToString()
                Dim price = Decimal.Parse(dr("price").ToString())

                ' Create button for each size
                Dim sizeButton As New Button With {
                    .Text = $"{sizeName} - PHP {price:N2}",
                    .Width = 200,
                    .Height = 50,
                    .Font = New Font("Segoe UI", 9, FontStyle.Bold),
                    .BackColor = Color.FromArgb(70, 70, 70),
                    .ForeColor = Color.White,
                    .Tag = New With {.SizeName = sizeName, .Price = price},
                    .FlatStyle = FlatStyle.Flat
                }

                ' Customize FlatAppearance properties
                sizeButton.FlatAppearance.BorderSize = 0 ' Removes border
                sizeButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 90, 90) ' Change color on hover
                sizeButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(50, 50, 50) ' Change color on click

                ' Add hover effects
                AddHandler sizeButton.MouseEnter, Sub(sender, e)
                                                      sizeButton.BackColor = Color.FromArgb(90, 90, 90)
                                                  End Sub

                AddHandler sizeButton.MouseLeave, Sub(sender, e)
                                                      sizeButton.BackColor = Color.FromArgb(70, 70, 70)
                                                  End Sub

                AddHandler sizeButton.Click, AddressOf SizeButton_Click
                FlowLayoutPanel1.Controls.Add(sizeButton)
            End While

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            dr.Close()
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub GunaControlBox1_Click(sender As Object, e As EventArgs) Handles GunaControlBox1.Click
        Me.Close()
    End Sub
    Private Sub LoadAddons()
        FlowLayoutPanel2.Controls.Clear()
        Dim addonsList As New List(Of (name As String, price As Decimal, addonCode As String,
                              itemCode As String, requiredQty As Decimal, availableQty As Decimal))

        Try
            dbconn()
            If conn.State = ConnectionState.Closed Then conn.Open()

            ' Get current cart addon usage
            Dim cartAddonUsage As Dictionary(Of String, Decimal) = GetCartAddonUsage()

            ' First, get all the addon data
            cmd = New MySqlCommand(
        "SELECT a.name, a.price, a.addoncode, s.itemcode, s.quantity as required_qty, " &
        "LEAST(i.quantity, ia.quantity) as available_qty " &
        "FROM tbl_addons a " &
        "INNER JOIN tbl_addon_supplies s ON a.addoncode = s.addoncode " &
        "INNER JOIN tbl_inventory i ON s.itemcode = i.itemcode " &
        "INNER JOIN tbl_inventoryad ia ON s.itemcode = ia.itemcode", conn)

            Using reader As MySqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    addonsList.Add((
                reader("name").ToString(),
                Decimal.Parse(reader("price").ToString()),
                reader("addoncode").ToString(),
                reader("itemcode").ToString(),
                Decimal.Parse(reader("required_qty").ToString()),
                Decimal.Parse(reader("available_qty").ToString())
            ))
                End While
            End Using

            ' Now create controls for each addon
            For Each addon In addonsList
                Dim addonCheckBox As New CheckBox With {
            .Text = $"{addon.name} - PHP {addon.price:N2}",
            .AutoSize = True,
            .Tag = New AddonItem With {
                .AddonName = addon.name,
                .Price = addon.price,
                .AddonCode = addon.addonCode,
                .ItemCode = addon.itemCode,
                .Quantity = addon.requiredQty
            }
        }

                ' Calculate remaining inventory after considering cart usage
                Dim remainingQty As Decimal = addon.availableQty
                If cartAddonUsage.ContainsKey(addon.itemCode) Then
                    remainingQty -= cartAddonUsage(addon.itemCode)
                End If

                ' Check if there's enough inventory considering cart usage
                Dim isAvailable = (remainingQty >= addon.requiredQty)
                addonCheckBox.Enabled = isAvailable
                addonCheckBox.ForeColor = If(isAvailable, Color.Black, Color.Gray)

                ' Add tooltip to show remaining quantity
                Dim tooltip As New ToolTip()
                tooltip.SetToolTip(addonCheckBox, $"Available: {remainingQty}")

                ' Add event handler for checkbox state change
                AddHandler addonCheckBox.CheckedChanged, AddressOf AddonCheckBox_CheckedChanged

                FlowLayoutPanel2.Controls.Add(addonCheckBox)
            Next

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Function GetCartAddonUsage() As Dictionary(Of String, Decimal)
        Dim addonUsage As New Dictionary(Of String, Decimal)

        ' Go through each row in the cart
        For Each row As DataGridViewRow In parentCashierForm.a.Rows
            Dim foodName As String = row.Cells(2).Value.ToString()
            Dim quantity As Integer = CInt(row.Cells(5).Value)

            ' Check if the item has addons
            If foodName.Contains(" with ") Then
                Dim addons = foodName.Split(New String() {" with "}, StringSplitOptions.None)(1).Split(",")
                For Each addon In addons
                    Dim trimmedAddon = addon.Trim()
                    If Not String.IsNullOrEmpty(trimmedAddon) Then
                        ' Get addon details from database
                        Using conn As New MySqlConnection(connString)
                            conn.Open()
                            Dim cmd As New MySqlCommand(
                            "SELECT s.itemcode, s.quantity " &
                            "FROM tbl_addons a " &
                            "INNER JOIN tbl_addon_supplies s ON a.addoncode = s.addoncode " &
                            "WHERE a.name = @name", conn)
                            cmd.Parameters.AddWithValue("@name", trimmedAddon)

                            Using reader As MySqlDataReader = cmd.ExecuteReader()
                                If reader.Read() Then
                                    Dim itemCode As String = reader("itemcode").ToString()
                                    Dim addonQty As Decimal = CDec(reader("quantity")) * quantity

                                    If addonUsage.ContainsKey(itemCode) Then
                                        addonUsage(itemCode) += addonQty
                                    Else
                                        addonUsage.Add(itemCode, addonQty)
                                    End If
                                End If
                            End Using
                        End Using
                    End If
                Next
            End If
        Next

        Return addonUsage
    End Function

    Private Sub AddonCheckBox_CheckedChanged(sender As Object, e As EventArgs)
        Dim checkBox As CheckBox = DirectCast(sender, CheckBox)
        Dim addonItem As AddonItem = DirectCast(checkBox.Tag, AddonItem)

        If checkBox.Checked Then
            ' Verify inventory one more time before adding
            If HasSufficientAddonInventory(addonItem.ItemCode, addonItem.Quantity) Then
                ' Add to selected addons if checked
                SelectedAddons.Add(addonItem)
            Else
                ' Uncheck the box if there's not enough inventory
                checkBox.Checked = False
                MessageBox.Show("This addon is no longer available due to insufficient inventory.", "Insufficient Inventory", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            ' Remove from selected addons if unchecked
            SelectedAddons.RemoveAll(Function(x) x.AddonCode = addonItem.AddonCode)
        End If
    End Sub

    Private Function HasSufficientAddonInventory(itemCode As String, requiredQty As Decimal) As Boolean
        Try
            ' Get current cart addon usage
            Dim cartAddonUsage As Dictionary(Of String, Decimal) = GetCartAddonUsage()

            dbconn()
            If conn.State = ConnectionState.Closed Then conn.Open()

            ' Check both inventory tables
            cmd = New MySqlCommand(
            "SELECT LEAST(i.quantity, ia.quantity) as available_qty " &
            "FROM tbl_inventory i " &
            "INNER JOIN tbl_inventoryad ia ON i.itemcode = ia.itemcode " &
            "WHERE i.itemcode = @itemcode", conn)

            cmd.Parameters.AddWithValue("@itemcode", itemCode)

            Dim availableQty As Decimal = CDec(cmd.ExecuteScalar())

            ' Subtract current cart usage
            If cartAddonUsage.ContainsKey(itemCode) Then
                availableQty -= cartAddonUsage(itemCode)
            End If

            Return availableQty >= requiredQty

        Catch ex As Exception
            MsgBox("Error checking addon inventory: " & ex.Message)
            Return False
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Function

    Private Sub SizeButton_Click(sender As Object, e As EventArgs)
        Dim btn = DirectCast(sender, Button)
        Dim buttonData = btn.Tag
        SelectedSize = buttonData.SizeName
        SelectedPrice = buttonData.Price

        ' Add selected addons prices to total
        For Each addon In SelectedAddons
            SelectedPrice += addon.Price
        Next

        Dim selectedSugarQuantity As Integer = 0

        ' Find the selected radio button in the nested panel structure
        For Each mainPanel As Control In FlowLayoutPanel3.Controls
            If TypeOf mainPanel Is Panel Then
                For Each innerPanel As Control In mainPanel.Controls
                    If TypeOf innerPanel Is GroupBox Then  ' Changed from Panel to GroupBox to match your implementation
                        For Each radioBtn As Control In innerPanel.Controls
                            If TypeOf radioBtn Is RadioButton AndAlso DirectCast(radioBtn, RadioButton).Checked Then
                                selectedSugarQuantity = CInt(radioBtn.Tag)
                                Exit For
                            End If
                        Next
                    End If
                Next
            End If
        Next

        ' Set the selected sugar quantity property
        Me.SelectedSugarQuantity = selectedSugarQuantity

        ' Important: Don't add sugar as an addon - we handle it separately
        ' Because we're using the row's Tag property to store the sugar quantity

        Me.Close()
    End Sub


    ' Add this property to your form class
    Public Property SelectedSugarQuantity As Integer = 0
    Private Sub LoadSugarLevels()
        FlowLayoutPanel3.Controls.Clear()

        ' Set properties for FlowLayoutPanel3 to remove scrollbars
        FlowLayoutPanel3.AutoScroll = False
        FlowLayoutPanel3.HorizontalScroll.Visible = False
        FlowLayoutPanel3.VerticalScroll.Visible = False

        ' Define sugar levels and corresponding quantities
        Dim sugarLevels As New Dictionary(Of String, Integer) From {
        {"0%", 0},
        {"50%", 13},
        {"75%", 19},
        {"100%", 26}
    }

        ' Create a panel to hold the 2x2 grid of radio buttons
        Dim sugarPanel As New Panel With {
        .Width = 251,
        .Height = 112,
        .BackColor = Color.Transparent
    }

        ' Set button size
        Dim buttonWidth As Integer = 100
        Dim buttonHeight As Integer = 35

        ' Calculate positions for centered layout
        Dim leftX As Integer = (251 - (buttonWidth * 2) - 10) \ 2  ' Horizontally center (10px gap)
        Dim topY As Integer = (112 - (buttonHeight * 2) - 10) \ 2  ' Vertically center (10px gap)

        Dim positions As New List(Of Point) From {
        New Point(leftX, topY),                    ' Top-left (0%)
        New Point(leftX + buttonWidth + 10, topY), ' Top-right (50%)
        New Point(leftX, topY + buttonHeight + 10), ' Bottom-left (75%)
        New Point(leftX + buttonWidth + 10, topY + buttonHeight + 10) ' Bottom-right (100%)
    }

        ' Create a single RadioButton group
        Dim sugarGroup As New GroupBox With {
        .Location = New Point(0, 0),
        .Size = New Size(251, 112),
        .Text = "",
        .BackColor = Color.FromArgb(196, 186, 179),
        .FlatStyle = FlatStyle.Flat
    }

        ' Remove the border of the GroupBox
        sugarGroup.FlatStyle = FlatStyle.Flat

        ' Create and add radio buttons in a 2x2 grid
        Dim index As Integer = 0

        For Each level In sugarLevels
            ' Create the radio button
            Dim sugarRB As New RadioButton With {
            .Text = level.Key,
            .Size = New Size(buttonWidth, buttonHeight),
            .Location = positions(index),
            .Font = New Font("Segoe UI", 9, FontStyle.Regular),
            .ForeColor = Color.Black,
            .BackColor = Color.White,
            .Tag = level.Value, ' Store sugar quantity in tag
            .Checked = (level.Key = "50%") ' Set 50% as default
        }

            ' Add event handler for radio button
            AddHandler sugarRB.CheckedChanged, AddressOf SugarRadioButton_CheckedChanged

            ' Add radio button directly to the group
            sugarGroup.Controls.Add(sugarRB)

            ' Update index for next position
            index += 1
        Next

        ' Add the group to panel
        sugarPanel.Controls.Add(sugarGroup)

        ' Add panel to FlowLayoutPanel3
        FlowLayoutPanel3.Controls.Add(sugarPanel)
    End Sub

    Private Sub SugarRadioButton_CheckedChanged(sender As Object, e As EventArgs)
        Dim radioButton As RadioButton = DirectCast(sender, RadioButton)

        ' Only process when a button is checked (not unchecked)
        If radioButton.Checked Then
            ' Store the selected sugar level quantity for later use when adding to cart
            ' The quantity value is stored in the Tag property
            Dim sugarQuantity As Integer = CInt(radioButton.Tag)

            ' Here you can add code to check if there's enough sugar in inventory
            If sugarQuantity > 0 Then
                If Not HasSufficientSugarInventory(sugarQuantity) Then
                    MessageBox.Show($"Not enough sugar for {radioButton.Text} level.", "Insufficient Inventory",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    ' Find and select the 0% option if current selection isn't available
                    For Each panelCtrl As Control In FlowLayoutPanel3.Controls
                        If TypeOf panelCtrl Is Panel Then
                            For Each innerPanel As Control In panelCtrl.Controls
                                If TypeOf innerPanel Is Panel Then
                                    For Each ctrl As Control In innerPanel.Controls
                                        If TypeOf ctrl Is RadioButton AndAlso CInt(DirectCast(ctrl, RadioButton).Tag) = 0 Then
                                            DirectCast(ctrl, RadioButton).Checked = True
                                            Exit For
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Private Function HasSufficientSugarInventory(requiredQuantity As Integer) As Boolean
        Dim checkConnection As New MySqlConnection(connString)
        Try
            checkConnection.Open()

            ' Get current cart sugar usage
            Dim cartSugarUsage As Decimal = GetCartSugarUsage()

            ' Get sugar inventory level - assuming sugar has a specific itemcode
            Dim cmd As New MySqlCommand("SELECT quantity FROM tbl_inventory WHERE itemcode = 'SUG001'", checkConnection)
            Dim availableSugar As Decimal = CDec(cmd.ExecuteScalar())

            ' Check if there's enough sugar for this order
            Return (requiredQuantity + cartSugarUsage) <= availableSugar

        Catch ex As Exception
            MessageBox.Show("Error checking sugar inventory: " & ex.Message)
            Return False
        Finally
            If checkConnection IsNot Nothing Then
                checkConnection.Close()
                checkConnection.Dispose()
            End If
        End Try
    End Function

    Private Function GetCartSugarUsage() As Decimal
        ' Calculate sugar usage in current cart
        ' This is a simplified implementation - you'll need to adjust it based on your data structure
        Dim totalSugarUsage As Decimal = 0

        ' If you have an actual way to track sugar in the cart, implement it here
        ' For now, returning 0 assumes no sugar is being used in the cart yet

        Return totalSugarUsage
    End Function
End Class
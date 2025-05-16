Imports System.IO
Imports MySql.Data.MySqlClient

Public Class frm_ManageAddons
    Dim ConnectionString As String = "server=localhost;port=3307;user=root;password=;database=brewtopia_db"
    Private AddonCode As String ' To store the current addon code

    Private Sub frm_ManageAddons_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateAddons()
        Auto_AddonCode()
        LoadInventoryItems() ' Load items into combobox
        GenerateSupplyCode() ' Generate initial supply code

    End Sub

    ' Existing methods remain the same...
    Private Sub Auto_AddonCode()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            Dim query As String = "SELECT addoncode FROM tbl_addons ORDER BY ID DESC LIMIT 1"
            Using cmd As New MySqlCommand(query, conn)
                Dim dr As MySqlDataReader = cmd.ExecuteReader()
                If dr.Read() Then
                    Dim lastAddonCode As String = dr("addoncode").ToString()
                    Dim nextCode As Integer = Integer.Parse(lastAddonCode) + 1
                    txt_addoncode.Text = nextCode.ToString("D6")
                Else
                    txt_addoncode.Text = "000001"
                End If
            End Using
        Catch ex As Exception
            MsgBox("Error generating addon code: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' Modified to also load addon supplies
    Private Sub PopulateAddons()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            Dim query As String = "SELECT addoncode, name, price FROM tbl_addons"
            Dim dt As New DataTable()
            Using cmd As New MySqlCommand(query, conn)
                Dim da As New MySqlDataAdapter(cmd)
                da.Fill(dt)
            End Using

            DataGridView1.DataSource = dt
            DataGridView1.Columns("addoncode").HeaderText = "ADDON CODE"
            DataGridView1.Columns("name").HeaderText = "ADDON NAME"
            DataGridView1.Columns("price").HeaderText = "PRICE"
            DataGridView1.Columns("price").DefaultCellStyle.Format = "C2"

            DataGridView1.Columns("addoncode").Visible = False

            ' When an addon is selected, load its supplies
            If DataGridView1.SelectedRows.Count > 0 Then
                AddonCode = DataGridView1.SelectedRows(0).Cells("addoncode").Value.ToString()
                LoadAddonsIngredients()
            End If
        Catch ex As Exception
            MsgBox("Error loading addons: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' New method to load addon supplies
    Private Sub LoadAddonsIngredients()
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Using cmd As New MySqlCommand("SELECT s.id, s.supplycode, s.itemcode, inv.name, s.quantity, inv.measurementname " &
                                          "FROM tbl_addon_supplies s " &
                                          "INNER JOIN tbl_inventory inv ON s.itemcode = inv.itemcode " &
                                          "WHERE s.addoncode = @addoncode", conn)
                    cmd.Parameters.AddWithValue("@addoncode", AddonCode)

                    DataGridView2.Rows.Clear() ' Assuming you have a second DataGridView for supplies
                    Using dr As MySqlDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            DataGridView2.Rows.Add(
                                dr("id").ToString(),
                                dr("supplycode").ToString(),
                                dr("itemcode").ToString(),
                                dr("name").ToString(),
                                dr("quantity").ToString(),
                                dr("measurementname").ToString()
                            )
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error loading addon supplies: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtquantity_KeyDown(sender As Object, e As KeyEventArgs) Handles txtquantity.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Optional: Prevents the "ding" sound
            btnAddSupply.PerformClick()
        End If
    End Sub

    ' New method to add supplies to addon
    Private Sub btnAddSupply_Click(sender As Object, e As EventArgs) Handles btnAddSupply.Click
        Try
            ' Validation checks
            If DataGridView1.SelectedRows.Count = 0 Then
                MsgBox("Please select an addon first!", MsgBoxStyle.Exclamation)
                Return
            End If

            If cbbName.SelectedIndex = -1 Then
                MsgBox("Please select an item from the list!", MsgBoxStyle.Exclamation)
                Return
            End If

            If String.IsNullOrWhiteSpace(txt_supplycode.Text) Then
                MsgBox("Supply code cannot be empty!", MsgBoxStyle.Critical)
                Return
            End If

            ' Validate quantity
            If String.IsNullOrWhiteSpace(txtquantity.Text) Then
                MsgBox("Please enter a quantity!", MsgBoxStyle.Exclamation)
                txtquantity.Focus()
                Return
            End If

            Dim quantity As Decimal
            If Not Decimal.TryParse(txtquantity.Text, quantity) OrElse quantity <= 0 Then
                MsgBox("Please enter a valid numeric quantity greater than 0!", MsgBoxStyle.Exclamation)
                txtquantity.Focus()
                Return
            End If

            If conn.State = ConnectionState.Closed Then conn.Open()

            ' Get itemcode for selected inventory item
            Dim cmdGetItemCode As New MySqlCommand("SELECT itemcode FROM tbl_inventory WHERE name = @name", conn)
            cmdGetItemCode.Parameters.AddWithValue("@name", cbbName.Text)
            Dim itemcode As String = cmdGetItemCode.ExecuteScalar().ToString()

            ' Get addon_name from the selected row in DataGridView1
            Dim addon_name As String = DataGridView1.SelectedRows(0).Cells("name").Value.ToString()

            ' Check if supply already exists for this addon
            Dim checkCmd As New MySqlCommand("SELECT COUNT(*) FROM tbl_addon_supplies WHERE addoncode = @addoncode AND itemcode = @itemcode", conn)
            checkCmd.Parameters.AddWithValue("@addoncode", AddonCode)
            checkCmd.Parameters.AddWithValue("@itemcode", itemcode)

            If Convert.ToInt32(checkCmd.ExecuteScalar()) > 0 Then
                MsgBox("This supply already exists for this addon!", MsgBoxStyle.Exclamation)
                LoadInventoryItems()
                Return
            End If

            ' Add the supply with addon_name
            Dim cmd As New MySqlCommand("INSERT INTO tbl_addon_supplies (addoncode, itemcode, addon_name, quantity, supplycode) VALUES (@addoncode, @itemcode, @addon_name, @quantity, @supplycode)", conn)
            cmd.Parameters.AddWithValue("@addoncode", AddonCode)
            cmd.Parameters.AddWithValue("@itemcode", itemcode)
            cmd.Parameters.AddWithValue("@addon_name", addon_name)
            cmd.Parameters.AddWithValue("@quantity", quantity)
            cmd.Parameters.AddWithValue("@supplycode", txt_supplycode.Text)

            cmd.ExecuteNonQuery()
            MsgBox("Supply added successfully!", MsgBoxStyle.Information)

            LoadInventoryItems()
            LoadAddonsIngredients()
            txtquantity.Clear()
            GenerateSupplyCode()
        Catch ex As Exception
            MsgBox("Error adding supply: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' New method to delete supplies from addon
    Private Sub btn_deletesupply_Click(sender As Object, e As EventArgs) Handles btn_deletesupply.Click
        Try
            If DataGridView2.SelectedRows.Count = 0 Then
                MsgBox("Please select a supply to delete!", MsgBoxStyle.Exclamation)
                Return
            End If

            Dim confirmDelete As DialogResult = MessageBox.Show("Are you sure you want to delete the selected supply?",
                                                              "Confirm Delete",
                                                              MessageBoxButtons.YesNo,
                                                              MessageBoxIcon.Warning)
            If confirmDelete = DialogResult.No Then Return

            Dim selectedId As String = DataGridView2.SelectedRows(0).Cells("id").Value.ToString()

            If conn.State = ConnectionState.Closed Then conn.Open()

            Dim deleteCmd As New MySqlCommand("DELETE FROM tbl_addon_supplies WHERE id = @id", conn)
            deleteCmd.Parameters.AddWithValue("@id", selectedId)
            deleteCmd.ExecuteNonQuery()

            MsgBox("Supply deleted successfully!", MsgBoxStyle.Information)
            LoadAddonsIngredients()
        Catch ex As Exception
            MsgBox("Error deleting supply: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub LoadInventoryItems()
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Using cmd As New MySqlCommand("SELECT name FROM tbl_inventory ORDER BY name", conn)
                    Using dr As MySqlDataReader = cmd.ExecuteReader()
                        cbbName.Items.Clear()
                        While dr.Read()
                            cbbName.Items.Add(dr("name").ToString())
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error loading inventory items: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub GenerateSupplyCode()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            Dim datePrefix As String = Date.Now.ToString("yyyyMM")
            Dim cmd As New MySqlCommand("SELECT MAX(supplycode) FROM tbl_addon_supplies WHERE supplycode LIKE @prefix", conn)
            cmd.Parameters.AddWithValue("@prefix", datePrefix & "%")

            Dim latestCode As Object = cmd.ExecuteScalar()
            If latestCode Is DBNull.Value OrElse latestCode Is Nothing Then
                txt_supplycode.Text = datePrefix & "001"
            Else
                Dim sequence As Integer = Integer.Parse(latestCode.ToString().Substring(6)) + 1
                txt_supplycode.Text = datePrefix & sequence.ToString("D3")
            End If
        Catch ex As Exception
            txt_supplycode.Text = Date.Now.ToString("yyyyMM") & "001"
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' Event handler for addon selection change
    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If DataGridView1.SelectedRows.Count > 0 Then
            AddonCode = DataGridView1.SelectedRows(0).Cells("addoncode").Value.ToString()
            LoadAddonsIngredients()
        End If
    End Sub

    Private Sub GunaButton3_Click(sender As Object, e As EventArgs) Handles GunaButton3.Click
        Me.Close()
    End Sub
    Private Sub txt_addonname_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_addonname.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevent beep sound
            btnAddAddons.PerformClick()
        End If
    End Sub

    Private Sub txt_price_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_price.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnAddAddons.PerformClick()
        End If
    End Sub

    Private Sub btnAddAddons_Click(sender As Object, e As EventArgs) Handles btnAddAddons.Click
        Try
            ' Validate inputs
            If String.IsNullOrWhiteSpace(txt_addonname.Text) OrElse String.IsNullOrWhiteSpace(txt_price.Text) Then
                MsgBox("Please enter both size name and price!", MsgBoxStyle.Exclamation, "Validation Error")
                Return
            End If

            If conn.State = ConnectionState.Closed Then conn.Open()

            Dim query As String = "INSERT INTO tbl_addons (addoncode, name, price) VALUES (@addoncode, @name, @price)"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@addoncode", txt_addoncode.Text)
                cmd.Parameters.AddWithValue("@name", txt_addonname.Text)
                cmd.Parameters.AddWithValue("@price", Decimal.Parse(txt_price.Text))

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                If rowsAffected > 0 Then
                    MsgBox("Addon saved successfully!", MsgBoxStyle.Information, "Success")
                    PopulateAddons() ' Refresh the DataGridView
                    ClearFields()
                Else
                    MsgBox("Failed to save addon.", MsgBoxStyle.Critical, "Error")
                End If
            End Using
        Catch ex As Exception
            MsgBox("Error saving addon: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub btn_deleteaddon_Click(sender As Object, e As EventArgs) Handles btn_deleteaddon.Click
        Try
            If DataGridView1.SelectedRows.Count = 0 Then
                MsgBox("Please select an addon to delete.", MsgBoxStyle.Exclamation, "Validation Error")
                Return
            End If

            ' Get selected addon name
            Dim selectedAddonName As String = DataGridView1.SelectedRows(0).Cells("name").Value.ToString()

            Dim result = MsgBox($"Are you sure you want to delete the addon '{selectedAddonName}'?", MsgBoxStyle.YesNo, "Confirmation")
            If result = MsgBoxResult.No Then Return

            If conn.State = ConnectionState.Closed Then conn.Open()

            Dim query As String = "DELETE FROM tbl_addons WHERE name = @name"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@name", selectedAddonName)

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                If rowsAffected > 0 Then
                    MsgBox("Addon deleted successfully!", MsgBoxStyle.Information, "Success")
                    PopulateAddons() ' Refresh the DataGridView
                Else
                    MsgBox("Failed to delete addon.", MsgBoxStyle.Critical, "Error")
                End If
            End Using
        Catch ex As Exception
            MsgBox("Error deleting addon: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub
    ' Clear input fields
    Private Sub ClearFields()
        txt_addoncode.Clear()
        txt_addonname.Clear()
        txt_price.Clear()
        Auto_AddonCode() ' Regenerate addon code
    End Sub

    Private Sub txt_price_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_price.KeyPress
        ' Allow only numeric characters and control keys (e.g., Backspace)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True ' Suppress the invalid keypress
        End If
    End Sub

    Private Sub txtquantity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtquantity.KeyPress
        ' Allow only numeric characters and control keys (e.g., Backspace)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True ' Suppress the invalid keypress
        End If
    End Sub
End Class
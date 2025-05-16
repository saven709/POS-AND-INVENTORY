Imports MySql.Data.MySqlClient

Public Class formingredients
    'Private _foodCode As String
    Dim ConnectionString As String = "server=localhost;port=3307;user=root;password=;database=brewtopia_db"

    Public Property FoodCode As String
    Private CurrentSupplyCode As String ' Stores the current supplycode for the form lifecycle

    Private Sub formingredients_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadFormData()

    End Sub

    Public Sub LoadFormData()
        LoadSizes()
        LoadInventoryItems()
        LoadIngredients()

        ' Only generate supplycode if it's not already set
        If String.IsNullOrEmpty(txt_supplycode.Text) Then
            GenerateSupplyCode()
        End If
    End Sub

    Private Sub GenerateSupplyCode()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            ' Get the latest supply code for today
            Dim datePrefix As String = Date.Now.ToString("yyyyMM")
            Dim cmd As New MySqlCommand("SELECT MAX(supplycode) FROM tbl_ingredients WHERE supplycode LIKE @prefix", conn)
            cmd.Parameters.AddWithValue("@prefix", datePrefix & "%")

            Dim latestCode As Object = cmd.ExecuteScalar()

            If latestCode Is DBNull.Value OrElse latestCode Is Nothing Then
                ' No codes exist for today, start with 001
                txt_supplycode.Text = datePrefix & "001"
            Else
                ' Increment the last sequence number
                Dim sequence As Integer = Integer.Parse(latestCode.ToString().Substring(6)) + 1
                txt_supplycode.Text = datePrefix & sequence.ToString("D3")
            End If
        Catch ex As Exception
            ' If any error occurs, fall back to the basic format
            txt_supplycode.Text = Date.Now.ToString("yyyyMM") & "001"
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub LoadSizes()
        Try
            Using conn As New MySqlConnection(ConnectionString)  ' Create new connection
                conn.Open()

                ' Query to load sizes for the selected food item
                Using cmd As New MySqlCommand("SELECT size_name, price FROM tbl_food_sizes WHERE foodcode = @foodcode", conn)
                    cmd.Parameters.AddWithValue("@foodcode", FoodCode)

                    ' Clear existing rows in the grid before adding new ones
                    GunaDataGridView1.Rows.Clear()

                    Using dr As MySqlDataReader = cmd.ExecuteReader()
                        Dim rowCount As Integer = 0 ' To track number of rows added
                        While dr.Read()
                            ' Populate the grid with size_name and price
                            GunaDataGridView1.Rows.Add(dr("size_name").ToString(), dr("price").ToString())
                            rowCount += 1
                        End While
                    End Using ' DataReader is automatically closed here

                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error loading sizes: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub LoadInventoryItems()
        Try
            Using conn As New MySqlConnection(ConnectionString)  ' Create new connection
                conn.Open()

                Using cmd As New MySqlCommand("SELECT name, itemcode FROM tbl_inventory ORDER BY name", conn)
                    Using dr As MySqlDataReader = cmd.ExecuteReader()
                        cbbName.Items.Clear()
                        While dr.Read()
                            ' Skip the item with itemcode "SUG001"
                            If dr("itemcode").ToString() <> "SUG001" Then
                                cbbName.Items.Add(dr("name").ToString())
                            End If
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error loading inventory items: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub


    Private Sub LoadIngredients()
        Try
            Using conn As New MySqlConnection(ConnectionString)  ' Create new connection
                conn.Open()

                ' Check if sizes exist for the selected food item
                Dim sizeCount As Integer
                Using sizeCheckCmd As New MySqlCommand("SELECT COUNT(*) FROM tbl_food_sizes WHERE foodcode = @foodcode", conn)
                    sizeCheckCmd.Parameters.AddWithValue("@foodcode", FoodCode)
                    sizeCount = Convert.ToInt32(sizeCheckCmd.ExecuteScalar())
                End Using

                If sizeCount = 0 Then
                    MsgBox("No sizes found for this Product item. Please add a size first.", MsgBoxStyle.Information)
                    DataGridView1.Rows.Clear()
                    Return
                End If

                ' If there are sizes, then check if one is selected
                If GunaDataGridView1.SelectedRows.Count > 0 Then
                    ' Get the selected size_name from GunaDataGridView1
                    Dim selectedSizeName As String = GunaDataGridView1.SelectedRows(0).Cells(0).Value.ToString()

                    ' Query to load ingredients based on the selected size
                    Using cmd As New MySqlCommand("SELECT i.id, i.supplycode, i.itemcode, inv.name, i.quantity, inv.measurementname " &
                                              "FROM tbl_ingredients i " &
                                              "INNER JOIN tbl_inventory inv ON i.itemcode = inv.itemcode " &
                                              "WHERE i.foodcode = @foodcode AND i.size_name = @size_name", conn)
                        cmd.Parameters.AddWithValue("@foodcode", FoodCode)
                        cmd.Parameters.AddWithValue("@size_name", selectedSizeName)

                        ' Clear existing ingredients before adding new ones
                        DataGridView1.Rows.Clear()

                        Using dr As MySqlDataReader = cmd.ExecuteReader()
                            While dr.Read()
                                DataGridView1.Rows.Add(
                                dr("id").ToString(),
                                dr("supplycode").ToString(),
                                dr("itemcode").ToString(),
                                dr("name").ToString(),
                                dr("quantity").ToString(),
                                dr("measurementname").ToString() ' Add measurement name
                            )
                            End While
                        End Using
                    End Using
                Else
                    ' If no size is selected in GunaDataGridView1, clear the ingredient list
                    DataGridView1.Rows.Clear()
                End If
            End Using
        Catch ex As Exception
            MsgBox("Error loading ingredients: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtnamesize_KeyDown(sender As Object, e As KeyEventArgs) Handles txtnamesize.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevents the "ding" sound
            btnAddSize.PerformClick()
        End If
    End Sub

    Private Sub txtPrice_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPrice.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnAddSize.PerformClick()
        End If
    End Sub

    Private Sub btnAddSize_Click(sender As Object, e As EventArgs) Handles btnAddSize.Click
        Try
            If String.IsNullOrWhiteSpace(txtnamesize.Text) OrElse String.IsNullOrWhiteSpace(txtPrice.Text) Then
                MsgBox("Please enter both size name and price!", MsgBoxStyle.Exclamation)
                Return
            End If

            If conn.State = ConnectionState.Closed Then conn.Open()

            Dim checkCmd As New MySqlCommand("SELECT COUNT(*) FROM tbl_food_sizes WHERE foodcode = @foodcode AND size_name = @size_name", conn)
            checkCmd.Parameters.AddWithValue("@foodcode", FoodCode)
            checkCmd.Parameters.AddWithValue("@size_name", txtnamesize.Text)

            If Convert.ToInt32(checkCmd.ExecuteScalar()) > 0 Then
                MsgBox("This size already exists for this Product item!", MsgBoxStyle.Exclamation)
                Return
            End If

            ' INSERT SUPPLYCODE ALSO
            Dim cmd As New MySqlCommand("INSERT INTO tbl_food_sizes (foodcode, size_name, price, supplycode) VALUES (@foodcode, @size_name, @price, @supplycode)", conn)
            cmd.Parameters.AddWithValue("@foodcode", FoodCode)
            cmd.Parameters.AddWithValue("@size_name", txtnamesize.Text)
            cmd.Parameters.AddWithValue("@price", Decimal.Parse(txtPrice.Text))
            cmd.Parameters.AddWithValue("@supplycode", txt_supplycode.Text)

            cmd.ExecuteNonQuery()

            LoadSizes()
            Dim EditForm As EditForm = Application.OpenForms.OfType(Of EditForm)().FirstOrDefault()
            EditForm.LoadSizes()
            MsgBox("Size added successfully!", MsgBoxStyle.Information)

            txtnamesize.Clear()
            txtPrice.Clear()
        Catch ex As Exception
            MsgBox("Error adding size: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub txtquantity_KeyDown(sender As Object, e As KeyEventArgs) Handles txtquantity.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevents beep sound
            btnAddSupply.PerformClick()
        End If
    End Sub

    Private Sub btnAddSupply_Click(sender As Object, e As EventArgs) Handles btnAddSupply.Click
        Try
            ' Check if the user has selected a size from GunaDataGridView1
            If GunaDataGridView1.SelectedRows.Count = 0 Then
                MsgBox("Please select a size from the list!", MsgBoxStyle.Exclamation)
                Return
            End If

            ' Get the size_name from the selected row in the grid
            Dim selectedSizeName As String = GunaDataGridView1.SelectedRows(0).Cells(0).Value.ToString()

            ' Check if a valid item is selected
            If cbbName.SelectedIndex = -1 Then
                MsgBox("Please select an item from the list!", MsgBoxStyle.Exclamation)
                Return
            End If

            ' Ensure supply code is not empty
            If String.IsNullOrWhiteSpace(txt_supplycode.Text) Then
                MsgBox("Supply code cannot be null or empty!", MsgBoxStyle.Critical)
                Return
            End If

            ' Ensure quantity is provided and valid
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

            ' Check if this ingredient already exists for this food and size
            Dim checkCmd As New MySqlCommand("SELECT COUNT(*) FROM tbl_ingredients WHERE foodcode = @foodcode AND itemcode = @itemcode AND size_name = @size_name", conn)
            checkCmd.Parameters.AddWithValue("@foodcode", FoodCode)
            checkCmd.Parameters.AddWithValue("@itemcode", itemcode)
            checkCmd.Parameters.AddWithValue("@size_name", selectedSizeName)

            If Convert.ToInt32(checkCmd.ExecuteScalar()) > 0 Then
                MsgBox("This ingredient already exists for this size!", MsgBoxStyle.Exclamation)
                LoadInventoryItems()
                Return
            End If

            ' Add the ingredient to the tbl_ingredients
            Dim cmd As New MySqlCommand("INSERT INTO tbl_ingredients (foodcode, itemcode, size_name, quantity, supplycode) VALUES (@foodcode, @itemcode, @size_name, @quantity, @supplycode)", conn)
            cmd.Parameters.AddWithValue("@foodcode", FoodCode)
            cmd.Parameters.AddWithValue("@itemcode", itemcode)
            cmd.Parameters.AddWithValue("@size_name", selectedSizeName)
            cmd.Parameters.AddWithValue("@quantity", quantity)
            cmd.Parameters.AddWithValue("@supplycode", txt_supplycode.Text)

            cmd.ExecuteNonQuery()

            MsgBox("Ingredient added successfully!", MsgBoxStyle.Information)
            LoadInventoryItems()
            LoadIngredients()
            txtquantity.Clear()
        Catch ex As Exception
            MsgBox("Error adding ingredient: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub btn_deletesize_Click(sender As Object, e As EventArgs) Handles btn_deletesize.Click
        Try
            ' Check if a size is selected in GunaDataGridView1
            If GunaDataGridView1.SelectedRows.Count = 0 Then
                MsgBox("Please select a size to delete!", MsgBoxStyle.Exclamation)
                Return
            End If

            ' Confirm deletion
            Dim confirmDelete As DialogResult = MessageBox.Show("Are you sure you want to delete the selected size and its ingredients?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If confirmDelete = DialogResult.No Then Return

            ' Get the selected size name
            Dim selectedSize As String = GunaDataGridView1.SelectedRows(0).Cells(0).Value.ToString()

            If conn.State = ConnectionState.Closed Then conn.Open()

            ' Delete entries from tbl_ingredients related to the size
            Dim deleteIngredientsCmd As New MySqlCommand("DELETE FROM tbl_ingredients WHERE foodcode = @foodcode AND EXISTS (SELECT 1 FROM tbl_food_sizes WHERE foodcode = tbl_ingredients.foodcode AND size_name = @size_name)", conn)
            deleteIngredientsCmd.Parameters.AddWithValue("@foodcode", FoodCode)
            deleteIngredientsCmd.Parameters.AddWithValue("@size_name", selectedSize)
            deleteIngredientsCmd.ExecuteNonQuery()

            ' Delete the size from tbl_food_sizes
            Dim deleteSizeCmd As New MySqlCommand("DELETE FROM tbl_food_sizes WHERE foodcode = @foodcode AND size_name = @size_name", conn)
            deleteSizeCmd.Parameters.AddWithValue("@foodcode", FoodCode)
            deleteSizeCmd.Parameters.AddWithValue("@size_name", selectedSize)
            deleteSizeCmd.ExecuteNonQuery()

            Dim EditForm As EditForm = Application.OpenForms.OfType(Of EditForm)().FirstOrDefault()
            EditForm.LoadSizes()
            MsgBox("Size and its ingredients deleted successfully!", MsgBoxStyle.Information)
            LoadSizes()
            LoadIngredients()
        Catch ex As Exception
            MsgBox("Error deleting size: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub btn_deletesupply_Click(sender As Object, e As EventArgs) Handles btn_deletesupply.Click
        Try
            ' Check if an ingredient is selected in DataGridView1
            If DataGridView1.SelectedRows.Count = 0 Then
                MsgBox("Please select an ingredient to delete!", MsgBoxStyle.Exclamation)
                Return
            End If

            ' Confirm deletion
            Dim confirmDelete As DialogResult = MessageBox.Show("Are you sure you want to delete the selected ingredient?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If confirmDelete = DialogResult.No Then Return

            ' Get the selected id
            Dim selectedId As String = DataGridView1.SelectedRows(0).Cells("id").Value.ToString()

            If conn.State = ConnectionState.Closed Then conn.Open()

            ' Delete the ingredient from tbl_ingredients
            Dim deleteCmd As New MySqlCommand("DELETE FROM tbl_ingredients WHERE id = @id", conn)
            deleteCmd.Parameters.AddWithValue("@id", selectedId)
            deleteCmd.ExecuteNonQuery()

            MsgBox("Ingredient deleted successfully!", MsgBoxStyle.Information)
            LoadIngredients()
        Catch ex As Exception
            MsgBox("Error deleting ingredient: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub GunaDataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles GunaDataGridView1.SelectionChanged
        ' Check if a row is selected
        If GunaDataGridView1.SelectedRows.Count > 0 Then
            ' Load the ingredients based on the selected size
            LoadIngredients()
        End If
    End Sub

    Private Sub GunaButton3_Click(sender As Object, e As EventArgs) Handles GunaButton3.Click
        Me.Close()
    End Sub

    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrice.KeyPress
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

    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click

    End Sub
End Class

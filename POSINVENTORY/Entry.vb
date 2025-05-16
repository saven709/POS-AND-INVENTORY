Imports System.IO
Imports MySql.Data.MySqlClient
Public Class Entry

    Private Sub btn_ManageFoods_Click(sender As Object, e As EventArgs) Handles btn_ManageFoods.Click
        frmitem.ShowDialog()
    End Sub
    Private Sub InitializeDataGridView()
        ' Configure DataGridView layout and columns
        DataGridView1.RowTemplate.Height = 30
        DataGridView1.Columns.Clear()

        ' Define the columns in the DataGridView
        DataGridView1.Columns.Add("No", "No")
        DataGridView1.Columns.Add("ItemCode", "Item Code")
        DataGridView1.Columns.Add("Name", "Item Name")
        DataGridView1.Columns.Add("MeasurementName", "Measurement")
        DataGridView1.Columns.Add("Category", "Category")
        DataGridView1.Columns.Add("Quantity", "Quantity")
        DataGridView1.Columns.Add("Date", "Date")
        DataGridView1.Columns.Add("Time", "Time")
        DataGridView1.Columns.Add("Username", "Added By")

        DataGridView1.Columns("No").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridView1.Columns("ItemCode").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridView1.Columns("MeasurementName").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridView1.Columns("Category").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridView1.Columns("Name").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridView1.Columns("Quantity").Width = 55
        DataGridView1.Columns("Date").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridView1.Columns("Time").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridView1.Columns("Username").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        ' Center text in the "Quantity" column
        DataGridView1.Columns("Quantity").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        ' Hide the ItemCode column if necessary
        'DataGridView1.Columns("ItemCode").Visible = False
        DataGridView1.Columns("No").Visible = False

        ' Set DataGridView properties
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.MultiSelect = False
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.ReadOnly = True
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        ' Enable cell formatting
        AddHandler DataGridView1.CellFormatting, AddressOf DataGridView1_CellFormatting
    End Sub

    Public Sub LoadInventory()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            ' Include measurementname in the SELECT query
            Dim cmd As New MySqlCommand("SELECT `name`, `itemcode`, `measurementname`, `category`, `quantity`, `stockdate`, `stocktime`, `stockby` FROM `tbl_inventory`", conn)
            Dim dr As MySqlDataReader = cmd.ExecuteReader()

            DataGridView1.Rows.Clear()
            Dim rowIndex As Integer = 1

            While dr.Read()
                ' Add measurementname to the DataGridView row data
                Dim newRow As DataGridViewRow = DataGridView1.Rows(DataGridView1.Rows.Add(
                rowIndex,
                dr("itemcode").ToString(),
                dr("name").ToString(),
                dr("measurementname").ToString(),
                dr("category").ToString(),
                dr("quantity").ToString(),
                dr("stockdate").ToString(),
                dr("stocktime").ToString(),
                dr("stockby").ToString()
            ))

                ' Apply color coding directly here too (in addition to the CellFormatting event)
                Dim quantity As Integer
                If Integer.TryParse(dr("quantity").ToString(), quantity) Then
                    If quantity = 0 Then
                        ' No stock - bright red
                        newRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 80, 80)
                        newRow.DefaultCellStyle.ForeColor = Color.White
                    ElseIf quantity < 11 Then
                        ' Low stock - lighter red or orange
                        newRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 150, 100)
                        newRow.DefaultCellStyle.ForeColor = Color.Black
                    End If
                End If

                rowIndex += 1
            End While

            dr.Close()
        Catch ex As Exception
            MsgBox("Error loading inventory: " & ex.Message, vbCritical, "Error")
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub Entry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbconn()
        ' Initialize DataGridView structure
        InitializeDataGridView()

        ' Load data into DataGridView on form load
        LoadInventory()
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Try
            ' Check if a row is selected
            If DataGridView1.SelectedRows.Count > 0 Then
                ' Get the itemcode and item name of the selected row
                Dim itemcode As String = DataGridView1.SelectedRows(0).Cells("ItemCode").Value.ToString()
                Dim itemName As String = DataGridView1.SelectedRows(0).Cells("Name").Value.ToString()

                ' Confirm deletion
                If MessageBox.Show("Are you sure you want to delete the item: " & itemName & "?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                    ' Open database connection
                    If conn.State = ConnectionState.Closed Then conn.Open()

                    ' Begin transaction
                    Dim transaction As MySqlTransaction = conn.BeginTransaction()

                    Try
                        Dim success As Boolean = True

                        ' First check if the item exists in tbl_inventory
                        Dim checkCmd1 As New MySqlCommand("SELECT COUNT(*) FROM `tbl_inventory` WHERE `itemcode` = @itemcode", conn, transaction)
                        checkCmd1.Parameters.AddWithValue("@itemcode", itemcode)
                        Dim inventoryExists As Integer = Convert.ToInt32(checkCmd1.ExecuteScalar())

                        ' Check if the item exists in tbl_inventoryad
                        Dim checkCmd2 As New MySqlCommand("SELECT COUNT(*) FROM `tbl_inventoryad` WHERE `itemcode` = @itemcode", conn, transaction)
                        checkCmd2.Parameters.AddWithValue("@itemcode", itemcode)
                        Dim inventoryAdExists As Integer = Convert.ToInt32(checkCmd2.ExecuteScalar())

                        ' Delete from tbl_inventory if it exists
                        If inventoryExists > 0 Then
                            Dim cmd1 As New MySqlCommand("DELETE FROM `tbl_inventory` WHERE `itemcode` = @itemcode", conn, transaction)
                            cmd1.Parameters.AddWithValue("@itemcode", itemcode)
                            Dim result1 As Integer = cmd1.ExecuteNonQuery()
                            If result1 <= 0 Then success = False
                        End If

                        ' Delete from tbl_inventoryad if it exists
                        If inventoryAdExists > 0 Then
                            Dim cmd2 As New MySqlCommand("DELETE FROM `tbl_inventoryad` WHERE `itemcode` = @itemcode", conn, transaction)
                            cmd2.Parameters.AddWithValue("@itemcode", itemcode)
                            Dim result2 As Integer = cmd2.ExecuteNonQuery()
                            If result2 <= 0 Then success = False
                        End If

                        ' If both exist but one delete fails, or if only one exists and that delete fails
                        If Not success Then
                            transaction.Rollback()
                            MsgBox("Failed to delete item from one or both tables.", vbCritical, "Error")
                        Else
                            ' If everything succeeded, commit the transaction
                            transaction.Commit()
                            MsgBox("Item deleted successfully!", vbInformation, "Deletion Successful")
                            ' Refresh the DataGridView
                            LoadInventory()
                        End If
                    Catch ex As Exception
                        ' If an error occurred, rollback the transaction
                        transaction.Rollback()
                        MsgBox("Error occurred while deleting item: " & ex.Message, vbCritical, "Error")
                    End Try

                    ' Close the connection
                    If conn.State = ConnectionState.Open Then conn.Close()
                End If
            Else
                MsgBox("Please select a row to delete.", vbExclamation, "No Selection")
            End If
        Catch ex As Exception
            MsgBox("Error deleting item: " & ex.Message, vbCritical, "Error")
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lbl_date1.Text = Date.Now.ToString("ddd, dd-MM-yyyy")
        lbl_time.Text = Date.Now.ToString("hh:mm:ss tt")
    End Sub

    Private Sub txt_search_TextChanged(sender As Object, e As EventArgs) Handles txt_search.TextChanged
        ' Clear existing rows in DataGridView1
        DataGridView1.Rows.Clear()

        ' Ensure the DataGridView columns are initialized
        If DataGridView1.Columns.Count = 0 Then
            InitializeDataGridView()  ' Initialize columns if not done already
        End If

        ' Define the connection string
        Dim ConnectionString As String = "server=localhost;port=3307;user=root;password=;database=brewtopia_db"

        ' Use the connection string inside a Using statement
        Using conn As New MySqlConnection(ConnectionString)
            Try
                ' Open the connection if it's not already open
                If conn.State = ConnectionState.Closed Then conn.Open()

                ' Query to search for items by name, itemcode, measurementname, or category
                Dim cmd As New MySqlCommand("SELECT `name`, `itemcode`, `measurementname`, `category`, `quantity`, `stockdate`, `stocktime`, `stockby` FROM `tbl_inventory` WHERE name LIKE @search OR itemcode LIKE @search OR measurementname LIKE @search OR category LIKE @search", conn)
                cmd.Parameters.AddWithValue("@search", "%" & txt_search.Text & "%")

                ' Execute the command and fetch the data
                Dim dr As MySqlDataReader = cmd.ExecuteReader()

                ' Loop through the records and populate the DataGridView
                While dr.Read()
                    ' Add each record to the DataGridView
                    Dim newRow As DataGridViewRow = DataGridView1.Rows(DataGridView1.Rows.Add(
                    DataGridView1.Rows.Count + 1,  ' Auto-increment No column
                    dr("itemcode").ToString(),
                    dr("name").ToString(),
                    dr("measurementname").ToString(),
                    dr("category").ToString(),
                    dr("quantity").ToString(),
                    dr("stockdate").ToString(),
                    dr("stocktime").ToString(),
                    dr("stockby").ToString()
                ))

                    ' Apply color coding based on quantity
                    Dim quantity As Integer
                    If Integer.TryParse(dr("quantity").ToString(), quantity) Then
                        If quantity = 0 Then
                            ' No stock - bright red
                            newRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 80, 80)
                            newRow.DefaultCellStyle.ForeColor = Color.White
                        ElseIf quantity < 11 Then
                            ' Low stock - lighter red or orange
                            newRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 150, 100)
                            newRow.DefaultCellStyle.ForeColor = Color.Black
                        End If
                    End If
                End While
            Catch ex As Exception
                ' Handle exception if needed
                MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
            End Try
        End Using
    End Sub
    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        ' Check if this is a valid row (not the new row) and we're not in design mode
        If e.RowIndex >= 0 AndAlso Not DesignMode Then
            Dim quantity As Integer
            ' Try to parse the quantity value from the "Quantity" column
            If Integer.TryParse(DataGridView1.Rows(e.RowIndex).Cells("Quantity").Value.ToString(), quantity) Then
                ' If quantity is 0, set the row's background color to bright red
                If quantity = 0 Then
                    DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.FromArgb(255, 80, 80)
                    DataGridView1.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.White
                    ' If quantity is less than 11, set the row's background color to orange
                ElseIf quantity < 11 Then
                    DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.FromArgb(255, 150, 100)
                    DataGridView1.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Black
                End If
            End If
        End If
    End Sub
    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex >= 0 Then ' Ensure a valid row is selected
            ' Get the selected row's data
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            ' Create an instance of EntryEdit form
            Dim editForm As New EntryEdit()

            ' Pass data to the EntryEdit form
            editForm.txt_itemcode.Text = selectedRow.Cells("ItemCode").Value.ToString()
            editForm.txt_name.Text = selectedRow.Cells("Name").Value.ToString()
            editForm.txt_measurementname.Text = selectedRow.Cells("MeasurementName").Value.ToString()
            editForm.txt_category.Text = selectedRow.Cells("Category").Value.ToString()
            editForm.txt_quantity.Text = selectedRow.Cells("Quantity").Value.ToString()

            ' Set original values for comparison if needed
            editForm.Tag = New Dictionary(Of String, String) From {
            {"original_name", selectedRow.Cells("Name").Value.ToString()},
            {"original_quantity", selectedRow.Cells("Quantity").Value.ToString()},
            {"original_measurementname", selectedRow.Cells("MeasurementName").Value.ToString()},
            {"original_category", selectedRow.Cells("Category").Value.ToString()}
        }

            ' Show the EntryEdit form as a dialog
            editForm.ShowDialog()

            ' Refresh data after the dialog is closed
            LoadInventory()
        End If
    End Sub


End Class
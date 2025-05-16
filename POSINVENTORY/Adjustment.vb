Imports MySql.Data.MySqlClient

Public Class Adjustment

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex >= 0 Then ' Ensure a valid row is selected
            ' Get the selected row's data
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            ' Create an instance of editadjustment form
            Dim editForm As New editadjustment()

            ' Pass data to the editadjustment form
            editForm.txt_itemcode.Text = selectedRow.Cells("ItemCode").Value.ToString()
            editForm.txt_name.Text = selectedRow.Cells("Name").Value.ToString()
            editForm.txt_quantity.Text = selectedRow.Cells("Quantity").Value.ToString()
            editForm.txt_desc.Text = selectedRow.Cells("Desc").Value.ToString()

            ' Also pass the last quantity value to the label
            editForm.lbl_lastquantity.Text = selectedRow.Cells("Quantity").Value.ToString()

            ' Show the editadjustment form as a dialog
            editForm.ShowDialog()
        End If
    End Sub

    Private Sub InitializeDataGridView()
        ' Configure DataGridView layout and columns
        DataGridView1.RowTemplate.Height = 30
        DataGridView1.Columns.Clear()
        DataGridView1.Columns.Add("No", "No")
        DataGridView1.Columns.Add("Name", "Item Name")
        DataGridView1.Columns.Add("ItemCode", "Item Code")
        DataGridView1.Columns.Add("LastQuantity", "Last QTY")
        DataGridView1.Columns.Add("Quantity", "Quantity")
        DataGridView1.Columns.Add("Waste", "Waste")  ' New Waste column
        DataGridView1.Columns.Add("Desc", "Description")
        DataGridView1.Columns.Add("AdjustBy", "Adjusted By")
        DataGridView1.Columns.Add("AdjustDate", "Adjust Date")
        DataGridView1.Columns.Add("AdjustTime", "Adjust Time")

        ' Set column properties
        DataGridView1.Columns("No").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridView1.Columns("Name").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridView1.Columns("ItemCode").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridView1.Columns("LastQuantity").Width = 65
        DataGridView1.Columns("Quantity").Width = 60
        DataGridView1.Columns("Waste").Width = 50
        DataGridView1.Columns("Desc").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridView1.Columns("AdjustBy").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridView1.Columns("AdjustDate").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridView1.Columns("AdjustTime").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        ' Center text in the "Quantity" column
        DataGridView1.Columns("LastQuantity").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView1.Columns("Quantity").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView1.Columns("Waste").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        ' Hide the No and ItemCode columns if necessary
        DataGridView1.Columns("ItemCode").Visible = False
        DataGridView1.Columns("No").Visible = False

        ' Set DataGridView properties
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.MultiSelect = False
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.ReadOnly = True
        'DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Public Sub LoadAdjustments()
        Try
            ' First, update any descriptions that need to be changed
            UpdateDescriptionsBasedOnStock()

            ' Then proceed with loading data as before
            If conn.State = ConnectionState.Closed Then conn.Open()

            ' Query data from tbl_inventoryad
            Dim cmd As New MySqlCommand("SELECT `itemcode`, `name`, `lastquantity`, `quantity`, `waste`, `desc`, `adjustby`, `adjustdate`, `adjusttime` FROM `tbl_inventoryad`", conn)
            Dim dr As MySqlDataReader = cmd.ExecuteReader()

            DataGridView1.Rows.Clear()
            Dim rowIndex As Integer = 1

            While dr.Read()
                ' Add data from tbl_inventoryad to the DataGridView
                Dim newRow As DataGridViewRow = DataGridView1.Rows(DataGridView1.Rows.Add(
                rowIndex,
                dr("name").ToString(),
                dr("itemcode").ToString(),
                dr("lastquantity").ToString(),
                dr("quantity").ToString(),
                dr("waste").ToString(),
                dr("desc").ToString(),
                dr("adjustby").ToString(),
                dr("adjustdate").ToString(),
                dr("adjusttime").ToString()
            ))

                ' Check if quantity is less than 11 and color the row red
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
            MsgBox("Error loading adjustments: " & ex.Message, vbCritical, "Error")
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub Adjustment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbconn()
        ' Initialize DataGridView structure
        InitializeDataGridView()

        ' Update descriptions based on current stock levels
        UpdateDescriptionsBasedOnStock()

        ' Load data into DataGridView
        LoadAdjustments()

        'SetupStockCheckTimer()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lbl_date1.Text = Date.Now.ToString("ddd, dd-MM-yyyy")
        lbl_time.Text = Date.Now.ToString("hh:mm:ss tt")
    End Sub

    Private Sub txt_search_TextChanged(sender As Object, e As EventArgs) Handles txt_search.TextChanged
        ' First, update descriptions
        UpdateDescriptionsBasedOnStock()

        ' Now proceed with search
        DataGridView1.Rows.Clear()

        If DataGridView1.Columns.Count = 0 Then
            InitializeDataGridView()
        End If

        Dim ConnectionString As String = "server=localhost;port=3307;user=root;password=;database=brewtopia_db"

        Using conn As New MySqlConnection(ConnectionString)
            Try
                If conn.State = ConnectionState.Closed Then conn.Open()

                Dim cmd As New MySqlCommand("SELECT `itemcode`, `name`, `lastquantity`, `quantity`, `waste`, `desc`, `adjustby`, `adjustdate`, `adjusttime` FROM `tbl_inventoryad` WHERE `name` LIKE @search OR `itemcode` LIKE @search OR `desc` LIKE @search", conn)
                cmd.Parameters.AddWithValue("@search", "%" & txt_search.Text & "%")

                Dim dr As MySqlDataReader = cmd.ExecuteReader()

                Dim rowIndex As Integer = 1
                While dr.Read()
                    Dim newRow As DataGridViewRow = DataGridView1.Rows(DataGridView1.Rows.Add(
                    rowIndex,
                    dr("name").ToString(),
                    dr("itemcode").ToString(),
                    dr("lastquantity").ToString(),
                    dr("quantity").ToString(),
                    dr("waste").ToString(),
                    dr("desc").ToString(),
                    dr("adjustby").ToString(),
                    dr("adjustdate").ToString(),
                    dr("adjusttime").ToString()
                ))

                    ' Check if quantity is zero or less than 11 and color the row appropriately
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
            Catch ex As Exception
                MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
            End Try
        End Using
    End Sub
    Private Sub UpdateDescriptionsBasedOnStock()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            ' Get items that need description updates (quantity <= 10)
            Dim cmd As New MySqlCommand("SELECT `itemcode`, `name`, `quantity`, `desc` FROM `tbl_inventoryad` WHERE `quantity` <= 10", conn)
            Dim dr As MySqlDataReader = cmd.ExecuteReader()

            ' Store items to update (need to close reader before executing update queries)
            Dim itemsToUpdate As New List(Of Tuple(Of String, String, Integer, String))

            While dr.Read()
                ' Store itemcode, name, quantity, current desc
                itemsToUpdate.Add(New Tuple(Of String, String, Integer, String)(
                    dr("itemcode").ToString(),
                    dr("name").ToString(),
                    Convert.ToInt32(dr("quantity")),
                    dr("desc").ToString()
                ))
            End While

            dr.Close()

            ' Process each item that needs updating
            For Each item In itemsToUpdate
                Dim itemcode As String = item.Item1
                Dim name As String = item.Item2
                Dim quantity As Integer = item.Item3
                Dim currentDesc As String = item.Item4

                ' Only update system descriptions or blank descriptions
                Dim isSystemDesc As Boolean = String.IsNullOrWhiteSpace(currentDesc) OrElse
                                              currentDesc.Contains("Low Stock") OrElse
                                              currentDesc.Contains("No Stock") OrElse
                                              currentDesc = "Inventory Updated"

                If isSystemDesc Then
                    Dim newDesc As String = ""

                    ' Set appropriate description based on quantity
                    If quantity = 0 Then
                        newDesc = "No Stock Available"
                    ElseIf quantity < 11 Then
                        newDesc = "Low Stock - Action Required"
                    End If

                    If Not String.IsNullOrEmpty(newDesc) AndAlso newDesc <> currentDesc Then
                        ' Update the description in the database
                        Dim updateCmd As New MySqlCommand("UPDATE `tbl_inventoryad` SET `desc`=@desc WHERE `itemcode`=@itemcode", conn)
                        updateCmd.Parameters.AddWithValue("@itemcode", itemcode)
                        updateCmd.Parameters.AddWithValue("@desc", newDesc)
                        updateCmd.ExecuteNonQuery()
                    End If
                End If
            Next
        Catch ex As Exception
            MsgBox("Error updating descriptions: " & ex.Message, vbCritical, "Error")
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub
    ' Add a timer to periodically check and update descriptions
    Private WithEvents StockCheckTimer As New Timer()

    ' Set up the timer in the form load
    Private Sub SetupStockCheckTimer()
        StockCheckTimer.Interval = 5 * 60 * 1000  ' Check every 5 minutes (adjust as needed)
        StockCheckTimer.Enabled = True
        AddHandler StockCheckTimer.Tick, AddressOf StockCheckTimer_Tick
    End Sub

    Private Sub StockCheckTimer_Tick(sender As Object, e As EventArgs)
        ' Update descriptions based on current stock and reload the grid
        UpdateDescriptionsBasedOnStock()
        LoadAdjustments()
    End Sub

End Class
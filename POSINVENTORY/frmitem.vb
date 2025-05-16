Imports MySql.Data.MySqlClient

Public Class frmitem
    Sub auto_itemcode()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            cmd = New MySqlCommand("SELECT * FROM `tbl_inventory` order by id desc", conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows = True Then
                txt_itemcode.Text = dr.Item("itemcode").ToString + 1

            Else
                txt_itemcode.Text = Date.Now.ToString("yyyyMM") & "001"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If conn.State = ConnectionState.Open Then conn.Close()

    End Sub
    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        Try
            ' Field validation
            If String.IsNullOrWhiteSpace(txt_name.Text) OrElse String.IsNullOrWhiteSpace(txt_quantity.Text) OrElse
           String.IsNullOrWhiteSpace(txt_measurementname.Text) OrElse String.IsNullOrWhiteSpace(txt_category.Text) Then
                MsgBox("Please fill out all fields!", MsgBoxStyle.Exclamation)
                Return
            End If

            ' Ensure quantity is a valid number
            Dim quantity As Integer
            If Not Integer.TryParse(txt_quantity.Text, quantity) Then
                MsgBox("Quantity must be a valid number!", MsgBoxStyle.Exclamation)
                Return
            End If

            If conn.State = ConnectionState.Closed Then conn.Open()

            ' Start a transaction to ensure both inserts succeed or fail together
            Dim transaction As MySqlTransaction = conn.BeginTransaction()

            Try
                ' Insert data into tbl_inventory
                Dim cmd As New MySqlCommand("INSERT INTO `tbl_inventory`(`itemcode`, `name`, `measurementname`, `quantity`, `category`, `stockdate`, `stocktime`, `stockby`) " &
                                        "VALUES (@itemcode, @name, @measurementname, @quantity, @category, @date, @time, @username)", conn, transaction)
                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("@itemcode", txt_itemcode.Text)
                cmd.Parameters.AddWithValue("@name", txt_name.Text)
                cmd.Parameters.AddWithValue("@measurementname", txt_measurementname.Text)
                cmd.Parameters.AddWithValue("@quantity", txt_quantity.Text)
                cmd.Parameters.AddWithValue("@category", txt_category.Text)
                cmd.Parameters.AddWithValue("@date", lbl_date1.Text)
                cmd.Parameters.AddWithValue("@time", lbl_time.Text)
                cmd.Parameters.AddWithValue("@username", Form1.lblUsername.Text)

                Dim i As Integer = cmd.ExecuteNonQuery()

                ' Insert into tbl_inventoryad with all required fields
                Dim cmd2 As New MySqlCommand("INSERT INTO `tbl_inventoryad`(`itemcode`, `name`, `quantity`, `lastquantity`, `waste`, `desc`, `adjustby`, `adjustdate`, `adjusttime`) " &
                                         "VALUES (@itemcode, @name, @quantity, @quantity, 0, 'Initial entry', @username, @date, @time)", conn, transaction)
                cmd2.Parameters.Clear()
                cmd2.Parameters.AddWithValue("@itemcode", txt_itemcode.Text)
                cmd2.Parameters.AddWithValue("@name", txt_name.Text)
                cmd2.Parameters.AddWithValue("@quantity", txt_quantity.Text)
                cmd2.Parameters.AddWithValue("@username", Form1.lblUsername.Text)
                cmd2.Parameters.AddWithValue("@date", lbl_date1.Text)
                cmd2.Parameters.AddWithValue("@time", lbl_time.Text)

                Dim j As Integer = cmd2.ExecuteNonQuery()

                ' If both inserts succeed, commit the transaction
                If i > 0 And j > 0 Then
                    transaction.Commit()
                    Dim Entry As Entry = Application.OpenForms.OfType(Of Entry)().FirstOrDefault()
                    If Entry IsNot Nothing Then
                        Entry.LoadInventory() ' Refresh the inventory list
                    End If
                    MsgBox("Inventory item saved successfully in both tables!", vbInformation, "INVENTORY")

                    ' Clear the textboxes after saving
                    txt_itemcode.Clear()
                    txt_name.Clear()
                    txt_measurementname.Clear()
                    txt_category.Clear()
                    txt_quantity.Clear()
                    Me.Close()
                Else
                    ' If either insert failed, roll back
                    transaction.Rollback()
                    MsgBox("Failed to save inventory item!", vbCritical, "INVENTORY")
                End If
            Catch ex As Exception
                ' Roll back on exception
                transaction.Rollback()
                MsgBox("Error: " & ex.Message, vbCritical, "Error saving inventory")
            End Try
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, vbCritical, "Error")
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub frmitem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        auto_itemcode()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lbl_date1.Text = Date.Now.ToString("ddd, dd-MM-yyyy")
        lbl_time.Text = Date.Now.ToString("hh:mm:ss tt")
    End Sub

    Private Sub GunaButton3_Click(sender As Object, e As EventArgs) Handles GunaButton3.Click
        Dim entryForm As Entry = Application.OpenForms.OfType(Of Entry)().FirstOrDefault()
        If entryForm IsNot Nothing Then
            entryForm.LoadInventory() ' Refresh the inventory list
        End If
        Me.Close()
    End Sub

    Private Sub txt_quantity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_quantity.KeyPress
        ' Allow only numeric characters and control keys (e.g., Backspace)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True ' Suppress the invalid keypress
        End If
    End Sub
    Private Sub txt_name_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_name.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevents the beep sound
            btn_save.PerformClick() ' Triggers the button click
        End If
    End Sub

    Private Sub txt_quantity_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_quantity.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevents the beep sound
            btn_save.PerformClick() ' Triggers the button click
        End If
    End Sub
    Private Sub txt_measurementname_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_measurementname.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevents the beep sound
            btn_save.PerformClick() ' Triggers the button click
        End If
    End Sub

    Private Sub txt_category_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_category.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevents the beep sound
            btn_save.PerformClick() ' Triggers the button click
        End If
    End Sub

End Class
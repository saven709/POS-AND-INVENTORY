Imports MySql.Data.MySqlClient

Public Class EntryEdit
    Private Sub btn_update_Click(sender As Object, e As EventArgs) Handles btn_update.Click
        Try
            ' Validate input fields
            If String.IsNullOrWhiteSpace(txt_name.Text) OrElse
           String.IsNullOrWhiteSpace(txt_quantity.Text) OrElse
           String.IsNullOrWhiteSpace(txt_measurementname.Text) OrElse
           String.IsNullOrWhiteSpace(txt_category.Text) Then
                MsgBox("Please fill out all required fields!", MsgBoxStyle.Exclamation)
                Return
            End If

            ' Ensure quantity is a valid number
            Dim newQuantity As Integer
            If Not Integer.TryParse(txt_quantity.Text, newQuantity) Then
                MsgBox("Quantity must be a valid number!", MsgBoxStyle.Exclamation)
                Return
            End If

            ' Open connection
            If conn.State = ConnectionState.Closed Then conn.Open()

            ' Start transaction
            Dim transaction As MySqlTransaction = conn.BeginTransaction()

            Try
                ' Get original values from Tag
                Dim originalValues As Dictionary(Of String, String) = DirectCast(Me.Tag, Dictionary(Of String, String))
                Dim originalQuantity As Integer = Integer.Parse(originalValues("original_quantity"))

                ' Calculate waste if quantity is being reduced
                Dim waste As Integer = If(originalQuantity > newQuantity, originalQuantity - newQuantity, 0)

                ' Auto-generate description for stock level
                Dim description As String = ""
                If newQuantity = 0 Then
                    description = "No Stock Available"
                ElseIf newQuantity < 11 Then
                    description = "Low Stock - Action Required"
                Else
                    description = "Inventory Updated"
                End If

                ' Update tbl_inventory
                Dim cmd1 As New MySqlCommand("UPDATE `tbl_inventory` SET `name`=@name, `measurementname`=@measurementname, " &
                                         "`quantity`=@quantity, `category`=@category WHERE `itemcode`=@itemcode", conn, transaction)
                cmd1.Parameters.Clear()
                cmd1.Parameters.AddWithValue("@itemcode", txt_itemcode.Text)
                cmd1.Parameters.AddWithValue("@name", txt_name.Text)
                cmd1.Parameters.AddWithValue("@measurementname", txt_measurementname.Text)
                cmd1.Parameters.AddWithValue("@quantity", txt_quantity.Text)
                cmd1.Parameters.AddWithValue("@category", txt_category.Text)

                Dim result1 As Integer = cmd1.ExecuteNonQuery()

                ' Update tbl_inventoryad
                Dim cmd2 As New MySqlCommand("UPDATE `tbl_inventoryad` SET `name`=@name, `lastquantity`=@lastquantity, " &
                                         "`quantity`=@quantity, `waste`=@waste, `desc`=@desc, `adjustby`=@adjustby, " &
                                         "`adjustdate`=@adjustdate, `adjusttime`=@adjusttime WHERE `itemcode`=@itemcode", conn, transaction)
                cmd2.Parameters.Clear()
                cmd2.Parameters.AddWithValue("@itemcode", txt_itemcode.Text)
                cmd2.Parameters.AddWithValue("@name", txt_name.Text)
                cmd2.Parameters.AddWithValue("@lastquantity", originalQuantity)
                cmd2.Parameters.AddWithValue("@quantity", txt_quantity.Text)
                cmd2.Parameters.AddWithValue("@waste", waste)
                cmd2.Parameters.AddWithValue("@desc", description)
                cmd2.Parameters.AddWithValue("@adjustby", Form1.lblUsername.Text)
                cmd2.Parameters.AddWithValue("@adjustdate", DateTime.Now.ToString("ddd, dd-MM-yyyy"))
                cmd2.Parameters.AddWithValue("@adjusttime", DateTime.Now.ToString("hh:mm:ss tt"))

                Dim result2 As Integer = cmd2.ExecuteNonQuery()

                ' Check if both updates were successful
                If result1 > 0 AndAlso result2 > 0 Then
                    transaction.Commit()
                    MsgBox("Inventory item updated successfully!", vbInformation, "Success")

                    ' Close the form
                    Me.Close()
                Else
                    transaction.Rollback()
                    MsgBox("Failed to update one or both tables!", vbCritical, "Error")
                End If
            Catch ex As Exception
                transaction.Rollback()
                MsgBox("Error occurred: " & ex.Message, vbCritical, "Error")
            End Try
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, vbCritical, "Error")
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub GunaButton3_Click(sender As Object, e As EventArgs) Handles GunaButton3.Click
        Me.Close()
    End Sub
End Class
Imports MySql.Data.MySqlClient

Public Class editadjustment
    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        Try
            ' VALIDATE INPUT FIELDS
            If String.IsNullOrWhiteSpace(txt_quantity.Text) Then
                MsgBox("PLEASE FILL OUT ALL REQUIRED FIELDS!", MsgBoxStyle.Exclamation)
                Return
            End If

            ' ENSURE QUANTITY IS A VALID NUMBER
            Dim inputQuantity As Integer
            If Not Integer.TryParse(txt_quantity.Text, inputQuantity) Then
                MsgBox("QUANTITY MUST BE A VALID NUMBER!", MsgBoxStyle.Exclamation)
                Return
            End If

            ' GET LAST QUANTITY FROM DATABASE LABEL
            Dim lastQty As Integer = Integer.Parse(lbl_lastquantity.Text)

            ' CALCULATE WASTE
            Dim waste As Integer = If(lastQty > inputQuantity, lastQty - inputQuantity, 0)

            ' AUTO-GENERATE DESCRIPTION IF NEEDED
            Dim originalDesc As String = txt_desc.Text
            Dim isSystemDesc As Boolean = originalDesc.Contains("LOW STOCK") OrElse originalDesc.Contains("NO STOCK")

            If inputQuantity = 0 Then
                If String.IsNullOrWhiteSpace(originalDesc) OrElse isSystemDesc Then
                    txt_desc.Text = "NO STOCK AVAILABLE"
                End If
            ElseIf inputQuantity < 11 Then
                If String.IsNullOrWhiteSpace(originalDesc) OrElse isSystemDesc Then
                    txt_desc.Text = "LOW STOCK - ACTION REQUIRED"
                End If
            End If

            ' OPEN CONNECTION
            If conn.State = ConnectionState.Closed Then conn.Open()

            ' START TRANSACTION
            Dim transaction As MySqlTransaction = conn.BeginTransaction()

            Try
                ' UPDATE tbl_inventoryad
                Dim cmd1 As New MySqlCommand("UPDATE `tbl_inventoryad` 
                                          SET `name`=@name, 
                                              `quantity`=@quantity, 
                                              `desc`=@desc, 
                                              `adjustby`=@adjustby, 
                                              `adjustdate`=@adjustdate, 
                                              `adjusttime`=@adjusttime, 
                                              `lastquantity`=@lastquantity, 
                                              `waste`=@waste 
                                          WHERE `itemcode`=@itemcode", conn, transaction)
                cmd1.Parameters.Clear()
                cmd1.Parameters.AddWithValue("@itemcode", txt_itemcode.Text)
                cmd1.Parameters.AddWithValue("@name", txt_name.Text)
                cmd1.Parameters.AddWithValue("@quantity", inputQuantity)
                cmd1.Parameters.AddWithValue("@desc", txt_desc.Text)
                cmd1.Parameters.AddWithValue("@adjustby", Form1.lblUsername.Text)
                cmd1.Parameters.AddWithValue("@adjustdate", DateTime.Now.ToString("ddd, dd-MM-yyyy"))
                cmd1.Parameters.AddWithValue("@adjusttime", DateTime.Now.ToString("hh:mm:ss tt"))
                cmd1.Parameters.AddWithValue("@lastquantity", lbl_lastquantity.Text)
                cmd1.Parameters.AddWithValue("@waste", waste)

                Dim result1 As Integer = cmd1.ExecuteNonQuery()

                ' UPDATE tbl_inventory
                Dim cmd2 As New MySqlCommand("UPDATE `tbl_inventory` 
                                          SET `name`=@name, 
                                              `quantity`=@quantity 
                                          WHERE `itemcode`=@itemcode", conn, transaction)
                cmd2.Parameters.Clear()
                cmd2.Parameters.AddWithValue("@itemcode", txt_itemcode.Text)
                cmd2.Parameters.AddWithValue("@name", txt_name.Text)
                cmd2.Parameters.AddWithValue("@quantity", inputQuantity)

                Dim result2 As Integer = cmd2.ExecuteNonQuery()

                ' CHECK IF BOTH UPDATES WERE SUCCESSFUL
                If result1 > 0 AndAlso result2 > 0 Then
                    transaction.Commit()
                    MsgBox("INVENTORY ADJUSTMENT SUCCESSFULLY SAVED!", vbInformation, "SUCCESS")

                    ' REFRESH THE ADJUSTMENT FORM
                    Dim adjustmentForm As Adjustment = Application.OpenForms.OfType(Of Adjustment)().FirstOrDefault()
                    If adjustmentForm IsNot Nothing Then
                        adjustmentForm.LoadAdjustments()
                    End If

                    ' CLOSE THE FORM
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    transaction.Rollback()
                    MsgBox("FAILED TO UPDATE ONE OR BOTH TABLES!", vbCritical, "ERROR")
                End If
            Catch ex As Exception
                transaction.Rollback()
                MsgBox("ERROR OCCURRED DURING TRANSACTION: " & ex.Message, vbCritical, "TRANSACTION ERROR")
            End Try
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Message, vbCritical, "ERROR")
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lbl_date1.Text = Date.Now.ToString("ddd, dd-MM-yyyy")
        lbl_time.Text = Date.Now.ToString("hh:mm:ss tt")
    End Sub

    Private Sub GunaButton3_Click(sender As Object, e As EventArgs) Handles GunaButton3.Click
        Me.Close()
    End Sub

    Private Sub txt_quantity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_quantity.KeyPress
        ' Allow only numeric characters and control keys (e.g., Backspace)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True ' Suppress the invalid keypress
        End If
    End Sub

    Private Sub txt_wastequantity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_wastequantity.KeyPress
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
    Private Sub txt_desc_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_desc.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevents the beep sound
            btn_save.PerformClick() ' Triggers the button click
        End If
    End Sub

    Private Sub txt_wastequantity_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_wastequantity.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevents the beep sound
            btn_save.PerformClick() ' Triggers the button click
        End If
    End Sub
End Class
Imports MySql.Data.MySqlClient

Public Class frmCashierChangepassword
    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        ' Validate that the current and new passwords are entered
        If String.IsNullOrWhiteSpace(txt_Currentpassword.Text) Then
            MsgBox("Please enter your current password!", MsgBoxStyle.Exclamation, "Validation Error")
            Return
        End If

        If String.IsNullOrWhiteSpace(txt_Newpassword.Text) Then
            MsgBox("Please enter a new password!", MsgBoxStyle.Exclamation, "Validation Error")
            Return
        End If

        Try
            ' Open the database connection
            If conn.State = ConnectionState.Closed Then conn.Open()

            ' Verify the current password
            Dim currentPassword As String = ""
            Dim checkQuery As String = "SELECT password FROM tbl_users WHERE username = @username"
            Using cmdCheck As New MySqlCommand(checkQuery, conn)
                cmdCheck.Parameters.AddWithValue("@username", lblUsername.Text)
                Using dr As MySqlDataReader = cmdCheck.ExecuteReader()
                    If dr.Read() Then
                        currentPassword = dr("password").ToString()
                    Else
                        MsgBox("User not found!", MsgBoxStyle.Critical, "Error")
                        Return
                    End If
                End Using
            End Using

            ' Compare the current password
            If txt_Currentpassword.Text.Trim() <> currentPassword Then
                MsgBox("The current password is incorrect!", MsgBoxStyle.Critical, "Validation Error")
                txt_Currentpassword.Clear()
                Return
            End If

            ' Ensure the new password is not the same as the current password
            If txt_Newpassword.Text.Trim() = currentPassword Then
                MsgBox("The new password cannot be the same as the current password!", MsgBoxStyle.Exclamation, "Validation Error")
                txt_Newpassword.Clear()
                Return
            End If

            ' Update the password
            Dim query As String = "UPDATE tbl_users SET password = @newpassword WHERE username = @username"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@newpassword", txt_Newpassword.Text.Trim()) ' Use hashing in production
                cmd.Parameters.AddWithValue("@username", lblUsername.Text)

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    MsgBox("Password updated successfully!", MsgBoxStyle.Information, "Success")
                Else
                    MsgBox("Failed to update password. Please try again.", MsgBoxStyle.Critical, "Error")
                End If
            End Using
        Catch ex As Exception
            MsgBox("Error updating password: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try

        ' Clear the textboxes and close the form
        txt_Currentpassword.Clear()
        txt_Newpassword.Clear()
        Me.Close()
    End Sub

End Class
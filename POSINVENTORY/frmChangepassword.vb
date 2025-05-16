Imports MySql.Data.MySqlClient
Imports BCrypt.Net

Public Class frmChangepassword
    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        ' Validate that a new password is entered
        If String.IsNullOrWhiteSpace(txt_Newpassword.Text) Then
            MsgBox("Please enter a new password!", MsgBoxStyle.Exclamation, "Validation Error")
            Return
        End If

        Try
            ' Open the database connection
            If conn.State = ConnectionState.Closed Then conn.Open()

            ' Get the current hashed password from the database
            Dim storedHashedPassword As String = ""
            Dim checkQuery As String = "SELECT password FROM tbl_users WHERE username = @username"
            Using cmdCheck As New MySqlCommand(checkQuery, conn)
                cmdCheck.Parameters.AddWithValue("@username", lblUsername.Text)
                Using dr As MySqlDataReader = cmdCheck.ExecuteReader()
                    If dr.Read() Then
                        storedHashedPassword = dr("password").ToString()
                    End If
                End Using
            End Using

            ' Verify if the new password matches the current password
            If BCrypt.Net.BCrypt.Verify(txt_Newpassword.Text.Trim(), storedHashedPassword) Then
                MsgBox("The new password cannot be the same as the current password!", MsgBoxStyle.Exclamation, "Validation Error")
                txt_Newpassword.Clear()
                Return
            End If

            ' Hash the new password before saving
            Dim hashedNewPassword As String = BCrypt.Net.BCrypt.HashPassword(txt_Newpassword.Text.Trim(), BCrypt.Net.BCrypt.GenerateSalt(12))

            ' Update the password in the database
            Dim query As String = "UPDATE tbl_users SET password = @newpassword WHERE username = @username"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@newpassword", hashedNewPassword)
                cmd.Parameters.AddWithValue("@username", lblUsername.Text)

                ' Execute the update query
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                ' Check if the update was successful
                If rowsAffected > 0 Then
                    Dim UserAccount As UserAccount = Application.OpenForms.OfType(Of UserAccount)().FirstOrDefault()
                    UserAccount.LoadUsers()
                    MsgBox("Password updated successfully!", MsgBoxStyle.Information, "Success")
                Else
                    MsgBox("Failed to update password. User not found!", MsgBoxStyle.Critical, "Error")
                End If
            End Using
        Catch ex As Exception
            MsgBox("Error updating password: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        Finally
            ' Close the database connection
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try

        ' Clear password field and close the form
        txt_Newpassword.Clear()
        Me.Close()
    End Sub
End Class

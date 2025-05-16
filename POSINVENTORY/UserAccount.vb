Imports MySql.Data.MySqlClient

Public Class UserAccount
    Private Sub BtnCreateAcc_Click(sender As Object, e As EventArgs) Handles BtnCreateAcc.Click
        frmCreateAcc.ShowDialog()
    End Sub

    Public Sub LoadUsers()
        Try
            ' Open the database connection
            If conn.State = ConnectionState.Closed Then conn.Open()

            ' Query to select user account data
            Dim query As String = "SELECT id, fullname, username, role, status, created_at, updated_at FROM tbl_users"

            Using cmd As New MySqlCommand(query, conn)
                Using dr As MySqlDataReader = cmd.ExecuteReader()
                    ' Clear the DataGridView before loading new data
                    DataGridView1.Rows.Clear()

                    ' Loop through the data and add to DataGridView
                    While dr.Read()
                        DataGridView1.Rows.Add(
                            dr("id").ToString(),
                            dr("fullname").ToString(),
                            dr("username").ToString(),
                            dr("role").ToString(),
                            dr("status").ToString(),
                            dr("created_at").ToString(),
                            dr("updated_at").ToString()
                        )
                    End While
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error loading user accounts: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub UserAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbconn()
        LoadUsers()

    End Sub


    Private Sub BtnChangePass_Click(sender As Object, e As EventArgs) Handles BtnChangePass.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MsgBox("Please select a user to change the password!", MsgBoxStyle.Exclamation, "No Selection")
            Return
        End If

        Dim selectedUsername As String = DataGridView1.SelectedRows(0).Cells("username").Value.ToString()

        frmChangepassword.lblUsername.Text = selectedUsername
        frmChangepassword.ShowDialog()
    End Sub

    Private Sub BtnActivateDeact_Click(sender As Object, e As EventArgs) Handles BtnActivateDeact.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MsgBox("Please select a user to activate or deactivate!", MsgBoxStyle.Exclamation, "No Selection")
            Return
        End If

        Dim selectedUsername As String = DataGridView1.SelectedRows(0).Cells("username").Value.ToString()
        Dim selectedUserId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("id").Value)
        Dim currentStatus As String = DataGridView1.SelectedRows(0).Cells("status").Value.ToString()

        ' CHECK IF THE SELECTED USER IS "admin"
        If selectedUsername.ToLower() = "admin" Then
            MsgBox("The 'admin' account cannot be deactivated!", MsgBoxStyle.Exclamation, "Action Denied")
            Return
        End If

        Dim newStatus As String = If(currentStatus = "Active", "Inactive", "Active")

        If MsgBox($"Are you sure you want to change the status to '{newStatus}'?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Confirm Action") = MsgBoxResult.No Then
            Return
        End If

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            Dim query As String = "UPDATE tbl_users SET status = @newstatus WHERE id = @id"

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@newstatus", newStatus)
                cmd.Parameters.AddWithValue("@id", selectedUserId)

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    MsgBox($"Status changed to '{newStatus}' successfully!", MsgBoxStyle.Information, "Success")
                    LoadUsers()
                Else
                    MsgBox("Failed to change status. User not found!", MsgBoxStyle.Critical, "Error")
                End If
            End Using
        Catch ex As Exception
            MsgBox("Error updating status: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MsgBox("Please select a user to remove!", MsgBoxStyle.Exclamation, "No Selection")
            Return
        End If

        Dim selectedUsername As String = DataGridView1.SelectedRows(0).Cells("username").Value.ToString()

        ' PREVENT REMOVING ADMIN ACCOUNT
        If selectedUsername.ToLower() = "admin" Then
            MsgBox("The 'admin' account cannot be removed!", MsgBoxStyle.Exclamation, "Action Denied")
            Return
        End If

        Dim selectedUserId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("id").Value)

        If MsgBox("Are you sure you want to remove this user?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Confirm Removal") = MsgBoxResult.No Then
            Return
        End If

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            Dim query As String = "DELETE FROM tbl_users WHERE id = @id"

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", selectedUserId)

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    MsgBox("User removed successfully!", MsgBoxStyle.Information, "Success")
                    LoadUsers()
                Else
                    MsgBox("Failed to remove user. User not found!", MsgBoxStyle.Critical, "Error")
                End If
            End Using
        Catch ex As Exception
            MsgBox("Error removing user: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lbl_date1.Text = Date.Now.ToString("ddd, dd-MM-yyyy")
        lbl_time.Text = Date.Now.ToString("hh:mm:ss tt")
    End Sub
End Class

Imports MySql.Data.MySqlClient
Imports BCrypt.Net

Public Class frmCreateAcc
    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        ' Validate the inputs
        If String.IsNullOrWhiteSpace(txt_fullname.Text) OrElse String.IsNullOrWhiteSpace(txt_Username.Text) OrElse String.IsNullOrWhiteSpace(txt_Password.Text) Then
            MsgBox("Please fill out all fields!", MsgBoxStyle.Exclamation, "Validation Error")
            Return
        End If

        ' Determine the selected role
        Dim selectedRole As String = ""
        If rbAdmin.Checked Then
            selectedRole = "Admin"
        ElseIf rbCashier.Checked Then
            selectedRole = "Cashier"
        ElseIf rbInventoryManager.Checked Then
            selectedRole = "Inventory Manager"
        ElseIf rbProductManager.Checked Then
            selectedRole = "Product Manager"
        End If

        If String.IsNullOrEmpty(selectedRole) Then
            MsgBox("Please select a role!", MsgBoxStyle.Exclamation, "Validation Error")
            Return
        End If

        Try
            ' Open database connection
            If conn.State = ConnectionState.Closed Then conn.Open()

            ' Insert query
            Dim query As String = "INSERT INTO tbl_users (fullname, username, password, role, status) VALUES (@fullname, @username, @password, @role, @status)"

            ' Create command
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@fullname", txt_fullname.Text.Trim())
                cmd.Parameters.AddWithValue("@username", txt_Username.Text.Trim())

                ' Hash password with BCrypt
                Dim hashedPassword As String = BCrypt.Net.BCrypt.HashPassword(txt_Password.Text.Trim(), BCrypt.Net.BCrypt.GenerateSalt(12))
                cmd.Parameters.AddWithValue("@password", hashedPassword)

                cmd.Parameters.AddWithValue("@role", selectedRole)
                cmd.Parameters.AddWithValue("@status", "Active") ' Default status is Active

                ' Execute query
                cmd.ExecuteNonQuery()
            End Using

            ' Refresh user list if the UserAccount form is open
            Dim UserAccount As UserAccount = Application.OpenForms.OfType(Of UserAccount)().FirstOrDefault()
            If UserAccount IsNot Nothing Then
                UserAccount.LoadUsers()
            End If

            MsgBox("Account created successfully!", MsgBoxStyle.Information, "Success")

            ' Clear the fields for new entry
            txt_fullname.Clear()
            txt_Username.Clear()
            txt_Password.Clear()
            rbAdmin.Checked = False
            rbCashier.Checked = False
            rbInventoryManager.Checked = False
            rbProductManager.Checked = False

        Catch ex As Exception
            MsgBox("Error saving account: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        Finally
            ' Close database connection
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub frmCreateAcc_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbconn()
    End Sub
End Class

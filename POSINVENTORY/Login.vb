Imports MySql.Data.MySqlClient
Imports BCrypt.Net
Imports System.Threading
Imports System.IO

Public Class Login
    Dim ConnectionString As String = "Server=localhost;Port=3307;Database=brewtopia_db;Uid=root;Pwd=;"

    ' Variables for tracking attempts
    Private loginAttempts As Integer = 0
    Private cooldownTime As Integer = 0 ' Time in seconds
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckCooldownOnStart()
    End Sub
    Private Function FormatTime(seconds As Integer) As String
        If seconds < 60 Then Return $"{seconds} seconds"
        If seconds < 3600 Then Return $"{seconds \ 60} minutes"
        Return $"{seconds \ 3600} hours"
    End Function
    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        StopMariaDB()
    End Sub

    Private Sub StopMariaDB()
        Try
            Dim appPath As String = Path.GetDirectoryName(Application.ExecutablePath)
            Dim startInfo As New ProcessStartInfo()
            startInfo.FileName = "cmd.exe"
            startInfo.Arguments = "/c " & Chr(34) & appPath & "\Database\stop_mariadb.bat" & Chr(34)
            startInfo.WindowStyle = ProcessWindowStyle.Hidden
            Process.Start(startInfo)

            ' Give some time for MariaDB to shut down properly
            System.Threading.Thread.Sleep(2000)
        Catch ex As Exception
            ' Log error but don't show message as application is closing
            Debug.WriteLine("Error stopping database: " & ex.Message)
        End Try
    End Sub


    Private Sub CheckCooldownOnStart()
        If IO.File.Exists("cooldown.txt") Then
            Dim lines = IO.File.ReadAllLines("cooldown.txt")
            Dim cooldownEndStr = lines.FirstOrDefault(Function(l) l.StartsWith("CooldownEnd="))?.Split("="c)(1)
            Dim storedAttemptsStr = lines.FirstOrDefault(Function(l) l.StartsWith("LoginAttempts="))?.Split("="c)(1)

            ' Restore loginAttempts
            If Integer.TryParse(storedAttemptsStr, loginAttempts) Then
                ' loginAttempts restored
            End If

            If DateTime.TryParse(cooldownEndStr, Nothing) Then
                Dim endTime As DateTime = DateTime.Parse(cooldownEndStr)
                If DateTime.Now < endTime Then
                    cooldownTime = CInt((endTime - DateTime.Now).TotalSeconds)
                    txtUsername.Enabled = False
                    txtPassword.Enabled = False
                    SigninBtn.Enabled = False

                    Dim cooldownThread As New Thread(AddressOf CooldownTimer)
                    cooldownThread.Start()
                Else
                    IO.File.Delete("cooldown.txt")
                    loginAttempts = 0 ' Reset attempts if cooldown expired
                End If
            End If
        End If
    End Sub



    Private Sub SigninBtn_Click(sender As Object, e As EventArgs) Handles SigninBtn.Click
        If cooldownTime > 0 Then
            MsgBox("Please wait " & cooldownTime & " seconds before trying again.", MsgBoxStyle.Exclamation, "Cooldown Active")
            Return
        End If

        If String.IsNullOrWhiteSpace(txtUsername.Text) OrElse String.IsNullOrWhiteSpace(txtPassword.Text) Then
            MsgBox("Please enter both username and password!", MsgBoxStyle.Exclamation, "Validation Error")
            Return
        End If

        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()

        Try
            Using conn As New MySqlConnection(ConnectionString)
                If conn.State = ConnectionState.Closed Then conn.Open()

                Dim query As String = "SELECT id, fullname, username, password, role, status FROM tbl_users WHERE username = @username"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@username", username)
                    Using dr As MySqlDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            Dim storedHash As String = dr("password").ToString()

                            ' Verify the hashed password
                            If BCrypt.Net.BCrypt.Verify(password, storedHash) Then
                                If dr("status").ToString() = "Active" Then
                                    ' ✅ Reset everything on successful login
                                    loginAttempts = 0
                                    cooldownTime = 0
                                    If IO.File.Exists("cooldown.txt") Then IO.File.Delete("cooldown.txt")

                                    Dim useraccname As String = dr("username").ToString()
                                    Dim userFullname As String = dr("fullname").ToString()
                                    Dim userRole As String = dr("role").ToString()

                                    ' Reset attempts on successful login
                                    loginAttempts = 0
                                    'lblAttempt.Text = "Attempts Left: 3"
                                    cooldownTime = 0

                                    Me.Hide()

                                    If userRole = "Cashier" Then
                                        Dim cashierForm As New Cashier()
                                        cashierForm.lblUsername.Text = userFullname
                                        cashierForm.lblUseraccname.Text = useraccname
                                        txtUsername.Clear()
                                        txtPassword.Clear()
                                        cashierForm.Show()
                                    ElseIf userRole = "Admin" Then
                                        Dim adminForm As New Form1()
                                        adminForm.lblUsername.Text = userFullname
                                        txtUsername.Clear()
                                        txtPassword.Clear()
                                        adminForm.Show()

                                        Dim dashboardForm As Dashboard = Application.OpenForms.OfType(Of Dashboard)().FirstOrDefault()
                                        If dashboardForm IsNot Nothing Then
                                            dashboardForm.lblUsername2.Text = userFullname
                                        End If
                                    End If
                                Else
                                    MsgBox("Your account is inactive. Please contact the administrator.", MsgBoxStyle.Critical, "Inactive Account")
                                    txtPassword.Clear()
                                End If
                            Else
                                HandleFailedLogin()
                            End If
                        Else
                            HandleFailedLogin()
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error logging in: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        End Try
    End Sub

    ' Handle failed login attempts
    Private Sub HandleFailedLogin()
        loginAttempts += 1
        Dim remainingAttempts As Integer = 3 - loginAttempts
        'lblAttempt.Text = "Attempts Left: " & Math.Max(0, remainingAttempts)

        If loginAttempts >= 3 Then
            ApplyCooldown()
        Else
            MsgBox("Incorrect username or password! You have " & remainingAttempts & " attempts left.", MsgBoxStyle.Critical, "Login Failed")
        End If


        txtPassword.Clear()
    End Sub
    Private Function GetCooldownIndex() As Integer
        Dim waitTimes As Integer() = {30, 60, 300, 1800, 3600}
        Dim cooldownIndex As Integer = Math.Max(0, loginAttempts - 3)

        If IO.File.Exists("cooldown.txt") Then
            Dim lines = IO.File.ReadAllLines("cooldown.txt")
            Dim lastFailedStr = lines.FirstOrDefault(Function(l) l.StartsWith("LastFailed="))?.Split("="c)(1)

            If DateTime.TryParse(lastFailedStr, Nothing) Then
                Dim lastFailed As DateTime = DateTime.Parse(lastFailedStr)
                If (DateTime.Now - lastFailed).TotalSeconds > 3600 Then ' Reset after 1 hour
                    Return 0
                End If
            End If
        End If

        Return Math.Min(cooldownIndex, waitTimes.Length - 1)
    End Function


    ' Apply cooldown based on failed attempts
    Private Sub ApplyCooldown()
        Dim waitTimes As Integer() = {30, 60, 300, 1800, 3600}
        Dim cooldownIndex As Integer = GetCooldownIndex()

        cooldownTime = waitTimes(cooldownIndex)
        Dim cooldownEndTime As DateTime = DateTime.Now.AddSeconds(cooldownTime)
        Dim lastFailedTime As DateTime = DateTime.Now

        ' Save to file
        Dim data As String =
        $"CooldownEnd={cooldownEndTime:o}" & Environment.NewLine &
        $"CooldownIndex={cooldownIndex}" & Environment.NewLine &
        $"LastFailed={lastFailedTime:o}" & Environment.NewLine &
        $"LoginAttempts={loginAttempts}"
        IO.File.WriteAllText("cooldown.txt", data)


        MsgBox("Too many failed attempts. Please wait " & FormatTime(cooldownTime) & ".", MsgBoxStyle.Critical, "Cooldown Active")

        txtUsername.Enabled = False
        txtPassword.Enabled = False
        SigninBtn.Enabled = False

        Dim cooldownThread As New Thread(AddressOf CooldownTimer)
        cooldownThread.Start()
    End Sub


    ' Cooldown countdown timer
    Private Sub CooldownTimer()
        While cooldownTime > 0
            Thread.Sleep(1000) ' Wait for 1 second
            cooldownTime -= 1

            ' Prevent updating UI if the form is closing or disposed
            If Me.IsDisposed OrElse Not Me.IsHandleCreated Then
                Exit Sub
            End If

            ' Update UI safely
            Me.Invoke(Sub()
                          If Not Me.IsDisposed Then
                              If cooldownTime > 0 Then
                                  lblAttempt.Text = "Wait " & cooldownTime & "s before retrying"
                              Else
                                  lblAttempt.Text = "" ' Clear the text when cooldown reaches 0
                              End If
                          End If
                      End Sub)
        End While

        ' Enable inputs after cooldown, clear textboxes
        If Not Me.IsDisposed AndAlso Me.IsHandleCreated Then
            Me.Invoke(Sub()
                          txtUsername.Enabled = True
                          txtPassword.Enabled = True
                          SigninBtn.Enabled = True
                          txtUsername.Clear()
                          txtPassword.Clear()
                      End Sub)
            IO.File.Delete("cooldown.txt") ' ✅ remove the file
        End If

    End Sub

    Private Sub txtUsername_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUsername.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevents the beep sound
            SigninBtn.PerformClick() ' Triggers the button click
        End If
    End Sub

    Private Sub txtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevents the beep sound
            SigninBtn.PerformClick() ' Triggers the button click
        End If
    End Sub
End Class

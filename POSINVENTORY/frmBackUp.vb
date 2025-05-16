Imports System.Diagnostics
Imports System.IO

Public Class frmBackUp
    ' Updated to use the correct path for your MariaDB installation
    Private Function GetMariaDBPath() As String
        Return Path.Combine(Application.StartupPath, "Database", "mysql", "bin")
    End Function

    Private Sub frmBackUp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' You can add initialization code here if needed
    End Sub

    Public Sub BackupDatabase()
        Try
            Using saveFileDialog As New SaveFileDialog()
                saveFileDialog.Filter = "SQL Files|*.sql"
                saveFileDialog.DefaultExt = "sql"
                saveFileDialog.FileName = "database_backup_" & DateTime.Now.ToString("yyyyMMdd_HHmmss")

                If saveFileDialog.ShowDialog() = DialogResult.OK Then
                    Dim backupFilePath As String = saveFileDialog.FileName
                    Cursor = Cursors.WaitCursor

                    ' === CONFIG - Dynamic paths based on current installation ===
                    Dim mysqldumpPath As String = Path.Combine(GetMariaDBPath(), "mysqldump.exe")
                    Dim dbName As String = "brewtopia_db"
                    Dim user As String = "root"
                    Dim password As String = ""
                    Dim host As String = "127.0.0.1"
                    Dim port As String = "3307"

                    ' Verify mysqldump exists
                    If Not File.Exists(mysqldumpPath) Then
                        Throw New FileNotFoundException("Could not find mysqldump.exe at: " & mysqldumpPath)
                    End If

                    ' === COMMAND ===
                    ' Create temporary batch file with proper quoting and error handling
                    Dim tempBat As String = Path.Combine(Path.GetTempPath(), "mysql_backup_" & Guid.NewGuid().ToString() & ".bat")

                    Using writer As New StreamWriter(tempBat, False)
                        writer.WriteLine("@echo off")
                        writer.WriteLine("echo Starting database backup...")
                        ' Add double quotes around paths to handle spaces
                        writer.WriteLine("""" & mysqldumpPath & """ --host=" & host & " --port=" & port & " --user=" & user & IIf(String.IsNullOrEmpty(password), "", " --password=" & password).ToString() & " " & dbName & " > """ & backupFilePath & """")
                        writer.WriteLine("if %ERRORLEVEL% NEQ 0 (")
                        writer.WriteLine("  echo Backup failed with error code %ERRORLEVEL%")
                        writer.WriteLine("  exit /b %ERRORLEVEL%")
                        writer.WriteLine(")")
                    End Using

                    ' === RUN THE BATCH FILE ===
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = tempBat
                    psi.WindowStyle = ProcessWindowStyle.Hidden
                    psi.CreateNoWindow = True
                    psi.UseShellExecute = False
                    psi.RedirectStandardError = True
                    psi.RedirectStandardOutput = True

                    Dim proc As Process = Process.Start(psi)
                    Dim errorOutput As String = proc.StandardError.ReadToEnd()
                    proc.WaitForExit()

                    ' Clean up the batch file
                    Try
                        File.Delete(tempBat)
                    Catch ex As Exception
                        ' Ignore cleanup errors
                    End Try

                    ' === VERIFY IF FILE WAS CREATED AND HAS CONTENT ===
                    If File.Exists(backupFilePath) AndAlso New FileInfo(backupFilePath).Length > 0 Then
                        MsgBox("DATABASE BACKUP SUCCESSFUL!" & vbCrLf & "Saved at: " & backupFilePath, MsgBoxStyle.Information)
                    Else
                        If Not String.IsNullOrEmpty(errorOutput) Then
                            MsgBox("BACKUP FAILED: " & errorOutput, MsgBoxStyle.Critical)
                        Else
                            MsgBox("BACKUP FAILED - FILE WAS NOT CREATED OR IS EMPTY.", MsgBoxStyle.Critical)
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception
            MsgBox("Error during backup: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnBackup_Click(sender As Object, e As EventArgs) Handles BtnBackup.Click
        BackupDatabase()
    End Sub

    Public Sub RestoreDatabase()
        Try
            Using openFileDialog As New OpenFileDialog()
                openFileDialog.Filter = "SQL Files|*.sql"
                openFileDialog.Title = "Select SQL Backup File to Restore"

                If openFileDialog.ShowDialog() = DialogResult.OK Then
                    Dim result As DialogResult = MsgBox("WARNING: This will overwrite your current database." & vbCrLf &
                                                   "Make sure you have a backup before proceeding." & vbCrLf & vbCrLf &
                                                   "Do you want to continue?",
                                                   MsgBoxStyle.YesNo Or MsgBoxStyle.Exclamation)

                    If result = DialogResult.No Then
                        Return
                    End If

                    Dim sqlFilePath As String = openFileDialog.FileName
                    Cursor = Cursors.WaitCursor

                    ' === CONFIGURATION - Dynamic paths based on current installation ===
                    Dim mysqlPath As String = Path.Combine(GetMariaDBPath(), "mysql.exe")
                    Dim dbName As String = "brewtopia_db"
                    Dim user As String = "root"
                    Dim password As String = ""
                    Dim host As String = "127.0.0.1"
                    Dim port As String = "3307"

                    ' Verify mysql client exists
                    If Not File.Exists(mysqlPath) Then
                        Throw New FileNotFoundException("Could not find mysql.exe at: " & mysqlPath)
                    End If

                    ' === BATCH FILE TO EXECUTE RESTORE ===
                    Dim tempBat As String = Path.Combine(Path.GetTempPath(), "mysql_restore_" & Guid.NewGuid().ToString() & ".bat")
                    Using writer As New StreamWriter(tempBat, False)
                        writer.WriteLine("@echo off")
                        writer.WriteLine("echo Starting database restore...")
                        ' Add double quotes around paths to handle spaces
                        writer.WriteLine("""" & mysqlPath & """ --host=" & host & " --port=" & port & " --user=" & user & IIf(String.IsNullOrEmpty(password), "", " --password=" & password).ToString() & " " & dbName & " < """ & sqlFilePath & """")
                        writer.WriteLine("if %ERRORLEVEL% NEQ 0 (")
                        writer.WriteLine("  echo Restore failed with error code %ERRORLEVEL%")
                        writer.WriteLine("  exit /b %ERRORLEVEL%")
                        writer.WriteLine(")")
                    End Using

                    ' === EXECUTE THE RESTORE ===
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = tempBat
                    psi.UseShellExecute = False
                    psi.CreateNoWindow = True
                    psi.WindowStyle = ProcessWindowStyle.Hidden
                    psi.RedirectStandardError = True
                    psi.RedirectStandardOutput = True

                    Dim proc As Process = Process.Start(psi)
                    Dim errorOutput As String = proc.StandardError.ReadToEnd()
                    proc.WaitForExit()

                    ' Clean up the batch file
                    Try
                        File.Delete(tempBat)
                    Catch ex As Exception
                        ' Ignore cleanup errors
                    End Try

                    If proc.ExitCode = 0 Then
                        MsgBox("DATABASE RESTORE SUCCESSFUL!", MsgBoxStyle.Information)
                    Else
                        If Not String.IsNullOrEmpty(errorOutput) Then
                            MsgBox("ERROR DURING RESTORE: " & errorOutput, MsgBoxStyle.Critical)
                        Else
                            MsgBox("ERROR DURING RESTORE. Exit code: " & proc.ExitCode, MsgBoxStyle.Critical)
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception
            MsgBox("ERROR DURING RESTORE: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnRestore_Click(sender As Object, e As EventArgs) Handles BtnRestore.Click
        RestoreDatabase()
    End Sub
End Class
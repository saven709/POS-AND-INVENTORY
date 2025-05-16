Imports System.Diagnostics
Imports System.IO

Public Class FlashScreen
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ProgressBar1.Increment(2)
        If ProgressBar1.Value = 100 Then
            Timer1.Stop()
            Login.Show()
            Me.Close()
        End If
    End Sub

    Private Sub FlashScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StartMariaDB()
        Timer1.Start()
    End Sub
    Private Sub StartMariaDB()
        Try
            Dim appPath As String = Path.GetDirectoryName(Application.ExecutablePath)
            Dim startInfo As New ProcessStartInfo()
            startInfo.FileName = "cmd.exe"
            startInfo.Arguments = "/c " & Chr(34) & appPath & "\Database\start_mariadb.bat" & Chr(34)
            startInfo.WindowStyle = ProcessWindowStyle.Hidden
            Process.Start(startInfo)

            ' Wait a moment for MariaDB to start
            System.Threading.Thread.Sleep(5000)
        Catch ex As Exception
            MessageBox.Show("Error starting database: " & ex.Message)
        End Try
    End Sub
End Class

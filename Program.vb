Imports System
Imports System.Net.Mime.MediaTypeNames
Imports System.Windows.Forms

<STAThread>
Sub Main()
    Application.EnableVisualStyles()
    Application.SetCompatibleTextRenderingDefault(False)

    ' Show Splash Screen first
    Dim splashScreen As New SplashScreen() ' Replace with your splash screen form name
    splashScreen.Show()

    ' Delay for a few seconds (optional, simulating loading)
    System.Threading.Thread.Sleep(3000)

    ' Close Splash Screen and show the Main Form
    splashScreen.Close()
    Application.Run(New MainForm()) ' Replace with your main form name
End Sub

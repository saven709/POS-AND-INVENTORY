Imports System.Media

Public Class frmPopup
    Private WithEvents slideTimer As New Timer()
    Private slideSpeed As Integer = 10
    Private targetTop As Integer
    Private startingTop As Integer

    ' CLOSE POPUP WHEN CLICKED (EITHER FORM OR BUTTON)
    Private Sub ClosePopup(sender As Object, e As EventArgs) Handles MyBase.Click, BtnClose.Click
        Me.Close()
    End Sub

    Private Sub frmPopup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' PLAY A BUILT-IN SYSTEM SOUND (LIKE MESSAGEBOX)
        SystemSounds.Beep.Play()

        PictureBox2.Image = My.Resources.Untitled_design
        MakePictureBoxCircular(PictureBox2)

        ' SET START POSITION ABOVE THE FORM (JUST ABOVE THE OWNER'S TOP)
        startingTop = Me.Owner.Top - Me.Height ' Start above the owner form
        targetTop = Me.Owner.Top + (Me.Owner.Height - Me.Height) \ 2 ' Centered vertically in the owner's form

        Me.StartPosition = FormStartPosition.Manual
        Me.Left = Me.Owner.Left + (Me.Owner.Width - Me.Width) \ 2
        Me.Top = startingTop ' Position above the screen initially

        ' START SLIDE TIMER
        slideTimer.Interval = 10 ' SPEED OF SLIDE
        slideTimer.Start()
    End Sub

    Private Sub MakePictureBoxCircular(pb As PictureBox)
        Dim path As New Drawing2D.GraphicsPath()
        path.AddEllipse(0, 0, pb.Width, pb.Height)
        pb.Region = New Region(path)
    End Sub

    ' TIMER TICK HANDLER - ANIMATION OF SLIDING DOWN
    Private Sub slideTimer_Tick(sender As Object, e As EventArgs) Handles slideTimer.Tick
        ' SLIDE DOWN UNTIL IT REACHES TARGET TOP
        If Me.Top < targetTop Then
            Me.Top += slideSpeed
            ' STOP TIMER IF WE'RE AT OR PAST THE TARGET POSITION
            If Me.Top >= targetTop Then
                Me.Top = targetTop
                slideTimer.Stop()
            End If
        Else
            slideTimer.Stop()
        End If
    End Sub
End Class

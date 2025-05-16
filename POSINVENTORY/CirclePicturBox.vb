Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging

Class CirclePicturBox
    Inherits PictureBox

    Protected Overrides Sub OnPaint(pe As PaintEventArgs)
        If Me.Image IsNot Nothing Then
            ' Use a GraphicsPath to define a rectangle region
            Dim graph As GraphicsPath = New GraphicsPath
            graph.AddRectangle(New Rectangle(0, 0, ClientSize.Width, ClientSize.Height))
            Me.Region = New Region(graph)

            ' Enable high-quality rendering for better image quality
            Dim g As Graphics = pe.Graphics
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            g.PixelOffsetMode = PixelOffsetMode.HighQuality

            ' Preserve transparency by drawing the image manually
            g.Clear(Me.BackColor) ' Use the control's background color
            g.DrawImage(Me.Image, New Rectangle(0, 0, Me.Width, Me.Height))
        Else
            ' Call the base paint method if no image is present
            MyBase.OnPaint(pe)
        End If
    End Sub
End Class

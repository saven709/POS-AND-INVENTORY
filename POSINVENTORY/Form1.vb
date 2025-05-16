Public Class Form1
    Private currentForm As Form = Nothing
    Private nextForm As Form = Nothing
    Private slideStep As Integer = 20
    Private slideDirection As String = "none"
    Private popupTimer As New Timer()
    Private transitionDuration As Integer = 300 ' Total milliseconds for the transition
    Private transitionStartTime As DateTime
    Private isLowPerformanceMode As Boolean = False ' Set to True for extremely low-end systems

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        OpeninPanel(New Dashboard)

        ' Set timer interval to target ~60fps for smooth animation
        transitionTimer.Interval = 16 ' ~60fps (1000ms / 60 = 16.67ms)

        ' SETUP THE POPUP TIMER
        popupTimer.Interval = 500 ' HALF A SECOND AFTER FORM SHOWN
        AddHandler popupTimer.Tick, AddressOf popupTimer_Tick
        popupTimer.Start()

        ' Detect if running on a low-end system and adjust settings accordingly
        DetectSystemPerformance()
    End Sub
    ' Add this method to auto-detect system capabilities
    Private Sub DetectSystemPerformance()
        Try
            ' Check available memory
            Dim computerInfo As New Microsoft.VisualBasic.Devices.ComputerInfo()
            Dim availableMemory As Long = computerInfo.AvailablePhysicalMemory

            ' Check if running on a terminal server (often indicates low resources)
            Dim isTerminalServer As Boolean = SystemInformation.TerminalServerSession

            ' Very low-end system (under 512MB free RAM or terminal server)
            If availableMemory < 536870912 OrElse isTerminalServer Then
                isLowPerformanceMode = True
                transitionTimer.Interval = 33     ' Lower to ~30fps
                transitionDuration = 150          ' Even faster transitions
                slideStep = 10                    ' Smaller steps
                ' Low-end system (under 1GB free RAM)
            ElseIf availableMemory < 1073741824 Then
                isLowPerformanceMode = True
                transitionTimer.Interval = 20     ' ~50fps
                transitionDuration = 200          ' Faster transitions
                slideStep = 15                    ' Medium steps
            Else
                ' Normal system - use standard settings
                isLowPerformanceMode = False
                transitionTimer.Interval = 16     ' ~60fps
                transitionDuration = 300          ' Standard transitions
                slideStep = 20                    ' Normal steps
            End If
        Catch ex As Exception
            ' If we can't check, assume normal mode
            isLowPerformanceMode = False
        End Try
    End Sub
    ' Method to check if the form is currently transitioning
    Public Function IsFormTransitioning() As Boolean
        Return slideDirection <> "none" And nextForm IsNot Nothing
    End Function
    Private Sub popupTimer_Tick(sender As Object, e As EventArgs)
        popupTimer.Stop() ' STOP TIMER AFTER IT FIRES ONCE
        CheckCriticalStockEnhanced() ' NOW CALL THE CHECK
    End Sub
    Public Sub CheckCriticalStockEnhanced()
        Try
            Dim connectionString As String = "server=localhost;port=3307;user=root;password=;database=brewtopia_db"
            Using conn As New MySql.Data.MySqlClient.MySqlConnection(connectionString)
                conn.Open()

                Dim cmd As New MySql.Data.MySqlClient.MySqlCommand("SELECT name, quantity FROM tbl_inventoryad WHERE quantity < 11", conn)
                Dim reader As MySql.Data.MySqlClient.MySqlDataReader = cmd.ExecuteReader()

                Dim hasCriticalItems As Boolean = False

                While reader.Read()
                    hasCriticalItems = True
                    Exit While ' ONLY NEED TO KNOW IF THERE'S AT LEAST ONE
                End While

                If hasCriticalItems Then
                    Dim popup As New frmPopup()
                    popup.Owner = Me
                    popup.StartPosition = FormStartPosition.Manual
                    popup.TopMost = True

                    popup.Show()

                    ' CENTER THE POPUP
                    popup.Location = New Point(
                    Me.Left + (Me.Width - popup.Width) \ 2,
                    Me.Top + (Me.Height - popup.Height) \ 2
                )
                End If

            End Using
        Catch ex As Exception
            MessageBox.Show("Error checking critical stock: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClosePopup(sender As Object, e As EventArgs)
        Dim control As Control = DirectCast(sender, Control)
        Dim popup As Form = control.FindForm()

        If popup IsNot Nothing Then
            popup.Close()
        End If
    End Sub
    Private Sub OpeninPanel(ByVal newForm As Form)
        ' First check if the new form is the same type as the current form
        If currentForm IsNot Nothing AndAlso newForm.GetType() Is currentForm.GetType() Then
            ' If it's the same form type, just return without doing anything
            newForm.Dispose() ' Clean up the new instance we don't need
            Return
        End If

        ' Reduce memory usage before transition
        GC.Collect(0, GCCollectionMode.Optimized)

        ' If a transition is already in progress, don't start another one
        If IsFormTransitioning() Then
            Return
        End If

        ' Special case for extremely low-end systems - skip animation entirely
        If isLowPerformanceMode AndAlso SystemInformation.TerminalServerSession Then
            ' Skip animation completely for remote sessions or very low-end systems
            If currentForm IsNot Nothing Then
                PanelMain.Controls.Remove(currentForm)
                currentForm.Dispose()
            End If

            newForm.TopLevel = False
            newForm.FormBorderStyle = FormBorderStyle.None
            newForm.Dock = DockStyle.Fill
            PanelMain.Controls.Add(newForm)
            PanelMain.Tag = newForm
            newForm.Show()
            currentForm = newForm

            ' If it's a dashboard, we need to call TransitionComplete
            If TypeOf currentForm Is Dashboard Then
                DirectCast(currentForm, Dashboard).TransitionComplete()
            End If
            Return
        End If

        ' If there's no form currently loaded, just load without animation
        If currentForm Is Nothing Then
            newForm.TopLevel = False
            newForm.FormBorderStyle = FormBorderStyle.None
            newForm.Dock = DockStyle.Fill
            PanelMain.Controls.Add(newForm)
            PanelMain.Tag = newForm
            newForm.Show()
            currentForm = newForm

            ' If it's a dashboard, we need to call TransitionComplete to force chart rendering
            If TypeOf currentForm Is Dashboard Then
                DirectCast(currentForm, Dashboard).TransitionComplete()
            End If
            Return
        End If

        ' Use SetStyle to optimize the drawing of forms
        SetDoubleBufferingForControls(PanelMain, True)

        ' Prepare the current form for animation
        currentForm.Dock = DockStyle.None
        currentForm.Location = New Point(0, 0)
        currentForm.BringToFront()

        ' Prepare the new form off-screen (right side)
        nextForm = newForm
        nextForm.TopLevel = False
        nextForm.FormBorderStyle = FormBorderStyle.None
        nextForm.Dock = DockStyle.None
        nextForm.Size = PanelMain.Size
        nextForm.Location = New Point(PanelMain.Width, 0)
        PanelMain.Controls.Add(nextForm)
        nextForm.BringToFront()
        nextForm.Show()

        ' Record the start time for smooth animation
        transitionStartTime = DateTime.Now

        ' Start sliding animation
        slideDirection = "left"
        transitionTimer.Start()
    End Sub
    ' Add this method to enable/disable double buffering for smoother drawing
    Private Sub SetDoubleBufferingForControls(ByVal ctrl As Control, ByVal enable As Boolean)
        ' Use reflection to access the internal SetStyle method
        Try
            Dim method As Reflection.MethodInfo = ctrl.GetType().GetMethod("SetStyle",
            Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)

            If method IsNot Nothing Then
                Dim param As Object() = {ControlStyles.OptimizedDoubleBuffer, enable}
                method.Invoke(ctrl, param)
            End If

            ' Apply to all child controls recursively
            For Each child As Control In ctrl.Controls
                SetDoubleBufferingForControls(child, enable)
            Next
        Catch ex As Exception
            ' Ignore errors if this fails
        End Try
    End Sub

    Private Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
        ' Open the Dashboard form first
        Dim dashboardForm As New Dashboard()
        OpeninPanel(dashboardForm)

        ' Transfer username to dashboardForm - improved technique
        ' The form might not be fully loaded, so we check if it's available
        If dashboardForm IsNot Nothing Then
            dashboardForm.UsernameLabel2.Text = lblUsername.Text
        End If
    End Sub

    Private Sub transitionTimer_Tick(sender As Object, e As EventArgs) Handles transitionTimer.Tick
        If slideDirection = "left" Then
            ' Calculate elapsed percentage of the transition
            Dim elapsedTime As Double = (DateTime.Now - transitionStartTime).TotalMilliseconds
            Dim progress As Double = Math.Min(1, elapsedTime / transitionDuration)

            ' For extremely low-end systems, use linear animation instead of easing
            Dim easedProgress As Double
            If isLowPerformanceMode Then
                easedProgress = progress ' Linear movement - less CPU intensive
            Else
                ' Apply easing function for smoother acceleration/deceleration
                easedProgress = 1 - ((1 - progress) * (1 - progress))
            End If

            ' Calculate positions
            Dim panelWidth As Integer = PanelMain.Width
            Dim currentLeft As Integer = -CInt(easedProgress * panelWidth)
            Dim nextLeft As Integer = panelWidth - CInt(easedProgress * panelWidth)

            ' Update positions
            currentForm.Left = currentLeft
            nextForm.Left = nextLeft

            ' If animation is complete
            If progress >= 1.0 Then
                ' Snap to final position
                nextForm.Left = 0
                transitionTimer.Stop()

                ' Clean up old form
                PanelMain.Controls.Remove(currentForm)
                currentForm.Dispose()
                currentForm = nextForm
                nextForm = Nothing
                slideDirection = "none"

                ' Re-enable docking
                currentForm.Dock = DockStyle.Fill

                ' Restore normal double buffering settings
                SetDoubleBufferingForControls(PanelMain, False)

                ' Run garbage collection to free memory
                GC.Collect(0, GCCollectionMode.Optimized)

                ' Notify the form that transition is complete
                If TypeOf currentForm Is Dashboard Then
                    DirectCast(currentForm, Dashboard).TransitionComplete()
                End If
            End If
        End If
    End Sub


    Private Sub btnProduct_Click(sender As Object, e As EventArgs) Handles btnProduct.Click
        'PanelProduct.Visible = Not PanelProduct.Visible
        OpeninPanel(New List)
    End Sub

    Private Sub btnStock_Click(sender As Object, e As EventArgs) Handles btnStock.Click
        ' Hide all panels first
        PanelSetting.Visible = False
        PanelRecord.Visible = False

        ' Toggle visibility of PanelStock
        PanelStock.Visible = Not PanelStock.Visible
    End Sub

    Private Sub btnRecord_Click(sender As Object, e As EventArgs) Handles btnRecord.Click
        ' Hide all panels first
        PanelStock.Visible = False
        PanelSetting.Visible = False

        ' Toggle visibility of PanelRecord
        PanelRecord.Visible = Not PanelRecord.Visible
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        ' Show a confirmation dialog
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        ' Check user's decision
        If result = DialogResult.Yes Then
            ' Show the login form
            Login.Show()
            ' Close the current form
            Me.Close()
        End If
    End Sub
    Private Sub btnList_Click(sender As Object, e As EventArgs) Handles btnList.Click
        OpeninPanel(New List)
    End Sub

    Private Sub GunaButton3_Click(sender As Object, e As EventArgs) Handles GunaButton3.Click
        OpeninPanel(New Category)
    End Sub

    Private Sub GunaButton7_Click(sender As Object, e As EventArgs) Handles GunaButton7.Click
        OpeninPanel(New Entry)
    End Sub

    Private Sub GunaButton6_Click(sender As Object, e As EventArgs) Handles GunaButton6.Click
        OpeninPanel(New Adjustment)
    End Sub

    Private Sub btnSetting_Click(sender As Object, e As EventArgs) Handles btnSetting.Click
        ' Hide all panels first
        PanelStock.Visible = False
        PanelRecord.Visible = False

        ' Toggle visibility of PanelSetting
        PanelSetting.Visible = Not PanelSetting.Visible
    End Sub

    Private Sub GunaButton4_Click(sender As Object, e As EventArgs) Handles GunaButton4.Click
        OpeninPanel(New UserAccount)
    End Sub

    Private Sub GunaButton9_Click(sender As Object, e As EventArgs) Handles GunaButton9.Click
        OpeninPanel(New frm_report)
    End Sub

    Private Sub GunaButton1_Click(sender As Object, e As EventArgs) Handles GunaButton1.Click
        ' Show a confirmation dialog
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to exit the application?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        ' Check user's decision
        If result = DialogResult.Yes Then
            ' Close the application
            Application.Exit()
        End If
    End Sub

    Private Sub btnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        OpeninPanel(New frmBackUp)
    End Sub

End Class
Imports System.IO
Imports MySql.Data.MySqlClient
Imports System.Drawing

Public Class frm_report
    Private Sub frm_report_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbconn()
        DataGridView1.RowTemplate.Height = 30

        ' Configure DataGridView properties for better text display
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        ' Configure specific column properties
        For Each col As DataGridViewColumn In DataGridView1.Columns
            Select Case col.Name.ToLower()
                Case "foodname"
                    col.MinimumWidth = 200  ' Adjust this value based on your needs
                Case "transno", "foodcode"
                    col.MinimumWidth = 100
                Case "price", "qty", "totalprice", "grandtotal"
                    col.MinimumWidth = 80
            End Select
        Next

        Load_report()
        Get_Dashboard()  ' Load initial dashboard data
        Timer1.Start()   ' Start the dashboard update timer
    End Sub

    Private Sub btn_close_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Sub Load_report()
        DataGridView1.Rows.Clear()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            cmd = New MySqlCommand("SELECT `transno`, `transdate`, `cashiername`, `foodcode`, `foodname`, `price`, `qty`, `totalprice`, `grandtotal` FROM `tbl_pos`", conn)
            dr = cmd.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(DataGridView1.Rows.Count + 1, dr.Item("transdate"), dr.Item("transno"), dr.Item("cashiername"), dr.Item("foodcode"), dr.Item("foodname"), dr.Item("price"), dr.Item("qty"), dr.Item("totalprice"), dr.Item("grandtotal"))
            End While

            ' Auto-size columns after loading data
            DataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)

            ' Ensure minimum widths are maintained
            For Each col As DataGridViewColumn In DataGridView1.Columns
                If col.Width < col.MinimumWidth Then
                    col.Width = col.MinimumWidth
                End If
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()

        End Try
    End Sub

    Private Sub txt_search_TextChanged(sender As Object, e As EventArgs) Handles txt_search.TextChanged
        DataGridView1.Rows.Clear()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            cmd = New MySqlCommand("SELECT `transno`, `transdate`, `cashiername`, `foodcode`, `foodname`, `price`, `qty`, `totalprice`, `grandtotal` FROM `tbl_pos` WHERE transno like @search OR foodcode like @search OR foodname like @search", conn)
            cmd.Parameters.AddWithValue("@search", "%" & txt_search.Text & "%")
            dr = cmd.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(DataGridView1.Rows.Count + 1, dr.Item("transdate"), dr.Item("transno"), dr.Item("cashiername"), dr.Item("foodcode"), dr.Item("foodname"), dr.Item("price"), dr.Item("qty"), dr.Item("totalprice"), dr.Item("grandtotal"))
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()

        End Try
    End Sub

    Private Sub btn_Filter_Click(sender As Object, e As EventArgs) Handles btn_Filter.Click
        ' Set the dates to include the full day range
        Dim date1 As String = DateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00")
        Dim date2 As String = DateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59")

        DataGridView1.Rows.Clear()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            cmd = New MySqlCommand("SELECT `transno`, `transdate`, `cashiername`, `foodcode`, `foodname`, `price`, `qty`, `totalprice`, `grandtotal` " &
                             "FROM `tbl_pos` " &
                             "WHERE transdate BETWEEN @date1 AND @date2 " &
                             "ORDER BY transdate", conn)
            cmd.Parameters.AddWithValue("@date1", date1)
            cmd.Parameters.AddWithValue("@date2", date2)
            dr = cmd.ExecuteReader

            While dr.Read
                ' Display the full datetime value
                Dim transDateTime As DateTime = Convert.ToDateTime(dr.Item("transdate"))
                DataGridView1.Rows.Add(
                DataGridView1.Rows.Count + 1,
                transDateTime.ToString("yyyy-MM-dd HH:mm:ss"),  ' Show full datetime
                dr.Item("transno"),
                dr.Item("cashiername"),
                dr.Item("foodcode"),
                dr.Item("foodname"),
                dr.Item("price"),
                dr.Item("qty"),
                dr.Item("totalprice"),
                dr.Item("grandtotal")
            )
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()

        End Try
    End Sub

    Private Sub btn_Excel_Click(sender As Object, e As EventArgs) Handles btn_Excel.Click
        Try
            ' Show progress indicator
            Cursor = Cursors.WaitCursor
            Dim progressLabel As New Label()
            progressLabel.Text = "Exporting data to Excel..."
            progressLabel.AutoSize = True
            progressLabel.Location = New Point(Me.Width / 2 - 75, Me.Height / 2)
            progressLabel.BackColor = Color.LightYellow
            progressLabel.Padding = New Padding(10)
            progressLabel.BorderStyle = BorderStyle.FixedSingle
            Me.Controls.Add(progressLabel)
            progressLabel.BringToFront()
            Application.DoEvents()

            ' Set up Excel objects
            Dim xlApp As Microsoft.Office.Interop.Excel.Application = Nothing
            Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook = Nothing
            Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet = Nothing

            Try
                xlApp = New Microsoft.Office.Interop.Excel.Application()
                xlWorkBook = xlApp.Workbooks.Add()
                xlWorkSheet = CType(xlWorkBook.Sheets(1), Microsoft.Office.Interop.Excel.Worksheet)

                ' Disable Excel features that slow down performance
                xlApp.ScreenUpdating = False
                xlApp.DisplayAlerts = False
                xlApp.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationManual

                ' Create a 2D array to hold all data (much faster than cell-by-cell)
                Dim rowCount As Integer = DataGridView1.RowCount
                Dim colCount As Integer = DataGridView1.ColumnCount
                Dim excelData(rowCount, colCount - 1) As Object

                ' Add headers to the array
                For j As Integer = 0 To colCount - 1
                    excelData(0, j) = DataGridView1.Columns(j).HeaderText
                Next

                ' Add data to the array in chunks (batch processing)
                For i As Integer = 0 To rowCount - 1
                    For j As Integer = 0 To colCount - 1
                        If DataGridView1(j, i).Value IsNot Nothing Then
                            excelData(i + 1, j) = DataGridView1(j, i).Value.ToString()
                        Else
                            excelData(i + 1, j) = ""
                        End If
                    Next

                    ' Update UI periodically to show progress
                    If i Mod 100 = 0 Then
                        progressLabel.Text = $"Exporting data to Excel... ({Math.Round((i / rowCount) * 100)}%)"
                        Application.DoEvents()
                    End If
                Next

                ' Write the entire array to Excel at once (major performance improvement)
                Dim dataRange As Microsoft.Office.Interop.Excel.Range = xlWorkSheet.Range(
                xlWorkSheet.Cells(1, 1),
                xlWorkSheet.Cells(rowCount + 1, colCount))
                dataRange.Value2 = excelData

                ' Apply formatting after data is loaded
                progressLabel.Text = "Formatting Excel spreadsheet..."
                Application.DoEvents()

                ' Format header row
                Dim headerRange As Microsoft.Office.Interop.Excel.Range = xlWorkSheet.Range(
                xlWorkSheet.Cells(1, 1),
                xlWorkSheet.Cells(1, colCount))
                headerRange.Font.Bold = True
                headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)

                ' Auto-fit columns after all data is in place
                xlWorkSheet.UsedRange.Columns.AutoFit()

                ' Save the Excel file
                progressLabel.Text = "Saving Excel file..."
                Application.DoEvents()

                Dim fName As String = "BREWTOPIA SALES REPORT"
                Using sfd As New SaveFileDialog
                    sfd.Title = "Save As"
                    sfd.OverwritePrompt = True
                    sfd.FileName = fName
                    sfd.DefaultExt = ".xlsx"
                    sfd.Filter = "Excel Workbook(*.xlsx)|*.xlsx"
                    sfd.AddExtension = True

                    If sfd.ShowDialog() = DialogResult.OK Then
                        xlWorkSheet.SaveAs(sfd.FileName)
                        MsgBox("Data Export Success!", MsgBoxStyle.Information, "BREWTOPIA")
                    End If
                End Using

            Finally
                ' Re-enable Excel features
                If xlApp IsNot Nothing Then
                    xlApp.ScreenUpdating = True
                    xlApp.DisplayAlerts = True
                    xlApp.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationAutomatic

                    ' Close workbook and quit Excel
                    If xlWorkBook IsNot Nothing Then xlWorkBook.Close(False)
                    xlApp.Quit()

                    ' Release COM objects
                    ReleaseObject(xlWorkSheet)
                    ReleaseObject(xlWorkBook)
                    ReleaseObject(xlApp)
                End If

                ' Remove progress indicator
                Me.Controls.Remove(progressLabel)
                progressLabel.Dispose()
                Cursor = Cursors.Default
            End Try

        Catch ex As Exception
            MsgBox("Export failed: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ReleaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Sub Get_Dashboard()
        Try
            ' Open connection if not already open
            If conn.State = ConnectionState.Closed Then conn.Open()

            ' Fetch today's sales - using date range for the full day
            Dim todayStart As String = Date.Now.ToString("yyyy-MM-dd 00:00:00")
            Dim todayEnd As String = Date.Now.ToString("yyyy-MM-dd 23:59:59")

            Dim cmd As New MySqlCommand("SELECT IFNULL(SUM(`totalprice`), 0) FROM `tbl_pos` WHERE `transdate` BETWEEN @todayStart AND @todayEnd", conn)
            cmd.Parameters.AddWithValue("@todayStart", todayStart)
            cmd.Parameters.AddWithValue("@todayEnd", todayEnd)
            lbl_todaySale.Text = FormatNumber(cmd.ExecuteScalar(), 2)

            ' Get current month's date range
            Dim currentMonth As DateTime = Date.Now
            Dim monthStart As String = New DateTime(currentMonth.Year, currentMonth.Month, 1).ToString("yyyy-MM-dd 00:00:00")
            Dim monthEnd As String = New DateTime(currentMonth.Year, currentMonth.Month, DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month)).ToString("yyyy-MM-dd 23:59:59")

            ' Query for current month's total sales
            cmd = New MySqlCommand("SELECT IFNULL(SUM(`totalprice`), 0) FROM `tbl_pos` WHERE `transdate` BETWEEN @monthStart AND @monthEnd", conn)
            cmd.Parameters.AddWithValue("@monthStart", monthStart)
            cmd.Parameters.AddWithValue("@monthEnd", monthEnd)

            ' Add debug prints to verify the date ranges
            Debug.Print($"Current Month Start: {monthStart}")
            Debug.Print($"Current Month End: {monthEnd}")

            Dim monthTotal As Decimal = Convert.ToDecimal(cmd.ExecuteScalar())
            lbl_lastmonth.Text = FormatNumber(monthTotal, 2)  ' Note: You might want to rename this label to lbl_currentMonth

        Catch ex As Exception
            MsgBox("Error fetching dashboard data: " & ex.Message, vbCritical, "Error")
        Finally
            ' Close the connection only if it was opened here
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Get_Dashboard()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        lbl_date1.Text = Date.Now.ToString("ddd, dd-MM-yyyy")
        lbl_time.Text = Date.Now.ToString("hh:mm:ss tt")
    End Sub

End Class
Imports System.IO
Imports MySql.Data.MySqlClient
Imports Microsoft.Reporting.WinForms
Imports System.Diagnostics

Public Class frm_BillPrint
    Private _transNo As String
    Private ConnectionString As String = "server=localhost;port=3307;user=root;password=;database=brewtopia_db"

    Public Sub New(transNo As String)
        InitializeComponent()
        _transNo = transNo
        ' Set KeyPreview to True so the form can capture key events
        Me.KeyPreview = True
    End Sub

    Private Sub frm_BillPrint_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        load_reportPrint()
    End Sub

    ' Add this event handler for the form's KeyDown event
    Private Sub frm_BillPrint_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        ' Check if Enter key was pressed
        If e.KeyCode = Keys.Enter Then
            ' Print the receipt
            PrintReceipt()
            e.Handled = True ' Mark the event as handled
        End If
    End Sub

    Private Sub load_reportPrint()
        Using conn As New MySqlConnection(ConnectionString)
            Dim rptDS As ReportDataSource
            Try
                conn.Open()
                ReportViewer1.LocalReport.ReportPath = Application.StartupPath & "\Report\Report1.rdlc"
                ReportViewer1.LocalReport.DataSources.Clear()
                ' Create dataset and adapter
                Dim ds As New DataSet1
                Using da As New MySqlDataAdapter()
                    ' Ensure we're using parameters to prevent SQL injection
                    da.SelectCommand = New MySqlCommand("SELECT * FROM `tbl_pos` WHERE transno = @transno", conn)
                    da.SelectCommand.Parameters.AddWithValue("@transno", _transNo)
                    ' Add a small delay to ensure transaction is committed
                    System.Threading.Thread.Sleep(100)
                    ' Fill dataset
                    da.Fill(ds.Tables("DataTable1"))
                    ' Check if we got any data
                    If ds.Tables("DataTable1").Rows.Count = 0 Then
                        MessageBox.Show("No data found for transaction " & _transNo, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                End Using

                ' Set up report
                rptDS = New ReportDataSource("DataSet1", ds.Tables("DataTable1"))
                ReportViewer1.LocalReport.DataSources.Add(rptDS)
                ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
                ReportViewer1.ZoomMode = ZoomMode.Percent
                ReportViewer1.ZoomPercent = 100

                ' Add handler for RenderingComplete event
                AddHandler ReportViewer1.RenderingComplete, AddressOf ReportViewer1_RenderingComplete

                ' Refresh to trigger the rendering
                ReportViewer1.RefreshReport()
            Catch ex As Exception
                MessageBox.Show("Error loading report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub ReportViewer1_RenderingComplete(sender As Object, e As RenderingCompleteEventArgs)
        ' Remove the handler to avoid multiple prints
        RemoveHandler ReportViewer1.RenderingComplete, AddressOf ReportViewer1_RenderingComplete

        ' If you want auto-print on load, uncomment the next line:
        ' PrintReceipt()
    End Sub

    ' Method to handle the printing process
    Private Sub PrintReceipt()
        Try
            ' Method 1: Using the PrintDialog from ReportViewer
            ' This will show the print dialog automatically
            ReportViewer1.PrintDialog()

            ' Method 2: For direct printing without dialog, use this instead:
            'DirectPrint()

            ' Optionally, close the form after printing
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error printing: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Alternative method for direct printing without dialog
    Private Sub DirectPrint()
        Try
            ' Access the internal report viewer to trigger printing directly
            Dim serverReport As LocalReport = ReportViewer1.LocalReport

            ' Export the report to PDF format in memory
            Dim warnings As Warning() = Nothing
            Dim streamids As String() = Nothing
            Dim mimeType As String = Nothing
            Dim encoding As String = Nothing
            Dim extension As String = Nothing
            Dim deviceInfo As String = "<DeviceInfo>" &
                                      "<OutputFormat>PDF</OutputFormat>" &
                                      "<PageWidth>8.5in</PageWidth>" &
                                      "<PageHeight>11in</PageHeight>" &
                                      "<MarginTop>0.5in</MarginTop>" &
                                      "<MarginLeft>0.5in</MarginLeft>" &
                                      "<MarginRight>0.5in</MarginRight>" &
                                      "<MarginBottom>0.5in</MarginBottom>" &
                                      "</DeviceInfo>"

            ' Render the report
            Dim bytes As Byte() = serverReport.Render("PDF", deviceInfo, mimeType, encoding, extension, streamids, warnings)

            ' Create a temporary PDF file
            Dim tempFile As String = Path.GetTempFileName() & ".pdf"
            File.WriteAllBytes(tempFile, bytes)

            ' Print the PDF using the default system print process
            ' Use the specific printer from your ReportViewer (POS-58C)
            Process.Start("rundll32.exe", String.Format("printui.dll,PrintUIEntry /k /n ""{0}"" /o /f ""{1}""",
                         "POS-58C", tempFile))

            ' Or use default printer:
            ' Process.Start("rundll32.exe", String.Format("printui.dll,PrintUIEntry /k /n ""{0}"" /o /f ""{1}""", _
            '              GetDefaultPrinterName(), tempFile))
        Catch ex As Exception
            MessageBox.Show("Error direct printing: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Helper function to get the default printer name
    Private Function GetDefaultPrinterName() As String
        Dim settings As New System.Drawing.Printing.PrinterSettings()
        Return settings.PrinterName
    End Function

End Class
Imports MySql.Data.MySqlClient
Imports LiveCharts
Imports LiveCharts.Wpf
Imports CartesianChart = LiveCharts.WinForms.CartesianChart
Imports PieChart = LiveCharts.WinForms.PieChart
Imports System.Drawing
Imports System.Threading
Imports System.ComponentModel

Public Class Dashboard
    Dim ConnectionString As String = "server=localhost;port=3307;user=root;password=;database=brewtopia_db"

    ' Declare LiveCharts controls
    Private cartesianChart1 As New CartesianChart()
    Private cartesianChart2 As New CartesianChart()
    Private pieChart1 As New PieChart()  ' Pie chart for category distribution

    ' Animation timer and variables
    ' Fix: Use System.Windows.Forms.Timer instead of System.Threading.Timer
    Private WithEvents chartAnimationTimer As New System.Windows.Forms.Timer()
    Private chartAnimationStep As Integer = 0
    Private chartAnimationsComplete As Boolean = False

    ' Background worker for loading chart data
    Private chartDataWorker As New BackgroundWorker()

    ' Track chart data status
    Private chartDataReady As Boolean = False
    Private dailySalesData As DataSet = Nothing
    Private topProductsData As DataSet = Nothing
    Private categoryData As DataSet = Nothing

    ' Low performance mode tracking
    Private isLowPerformanceMode As Boolean = False
    ' Add this code in Dashboard.vb in the class-level declarations section
    Private disableAllAnimations As Boolean = False  ' Set to True for extremely low-end systems

    ' Public property to access the username label from Form1
    Public ReadOnly Property UsernameLabel2 As Label
        Get
            Return lblUsername2
        End Get
    End Property

    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Check system performance first
        DetectSystemPerformance()

        ' Always load static information immediately
        DisplayDailySales()
        DisplayTotalProducts()
        LoadCriticalItems()
        LoadTotalProductsSold()

        ' Setup chart controls but make them invisible initially
        SetupChartControls()

        ' Configure background worker for chart data
        chartDataWorker.WorkerSupportsCancellation = True
        chartDataWorker.WorkerReportsProgress = False
        AddHandler chartDataWorker.DoWork, AddressOf ChartDataWorker_DoWork
        AddHandler chartDataWorker.RunWorkerCompleted, AddressOf ChartDataWorker_RunWorkerCompleted

        ' Start loading chart data in background
        If Not chartDataWorker.IsBusy Then
            chartDataWorker.RunWorkerAsync()
        End If
    End Sub

    ' Detect if system has low performance
    Private Sub DetectSystemPerformance()
        Try
            ' Check available memory
            Dim computerInfo As New Microsoft.VisualBasic.Devices.ComputerInfo()
            Dim availableMemory As Long = computerInfo.AvailablePhysicalMemory

            ' If less than 512MB RAM available, disable all animations
            If availableMemory < 536870912 Then
                isLowPerformanceMode = True
                disableAllAnimations = True
                ' If less than 1GB RAM available, use low performance mode
            ElseIf availableMemory < 1073741824 Then
                isLowPerformanceMode = True
                disableAllAnimations = False
            Else
                ' Regular performance mode
                isLowPerformanceMode = False
                disableAllAnimations = False
            End If
        Catch ex As Exception
            ' If we can't check, assume normal mode
            isLowPerformanceMode = False
            disableAllAnimations = False
        End Try
    End Sub

    ' Background worker to load chart data
    Private Sub ChartDataWorker_DoWork(sender As Object, e As DoWorkEventArgs)
        Try
            ' Load Daily Sales Trend Data
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT DATE(transdate) AS sale_date, SUM(totalprice) AS daily_sales " &
                                  "FROM tbl_pos " &
                                  "WHERE transdate >= CURDATE() - INTERVAL 7 DAY " &
                                  "GROUP BY sale_date " &
                                  "ORDER BY sale_date;"
                Dim cmd As New MySqlCommand(query, conn)
                Dim adapter As New MySqlDataAdapter(cmd)
                dailySalesData = New DataSet()
                adapter.Fill(dailySalesData, "DailySales")
            End Using

            ' Load Top Selling Products Data
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT foodname, SUM(qty) AS total_quantity " &
                              "FROM tbl_pos " &
                              "GROUP BY foodname " &
                              "ORDER BY total_quantity DESC " &
                              "LIMIT 10;"
                Dim cmd As New MySqlCommand(query, conn)
                Dim adapter As New MySqlDataAdapter(cmd)
                topProductsData = New DataSet()
                adapter.Fill(topProductsData, "TopProducts")
            End Using

            ' Load Product Category Distribution Data
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                Dim query As String = "SELECT category, COUNT(*) as category_count " &
                              "FROM tbl_food " &
                              "GROUP BY category " &
                              "ORDER BY category_count DESC;"
                Dim cmd As New MySqlCommand(query, conn)
                Dim adapter As New MySqlDataAdapter(cmd)
                categoryData = New DataSet()
                adapter.Fill(categoryData, "Categories")
            End Using

            ' Sleep a tiny bit to prevent UI freezing if data loads too fast
            Thread.Sleep(10)

        Catch ex As Exception
            ' Log error but don't crash
            Debug.WriteLine("Error loading chart data: " & ex.Message)
        End Try
    End Sub

    ' When chart data loading is complete
    Private Sub ChartDataWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        chartDataReady = True

        ' If not in middle of transition, show charts now
        If Not IsInsideTransition() Then
            ShowCharts()
        End If
    End Sub

    ' Show and populate charts when ready
    Private Sub ShowCharts()
        If Not chartsInitialized Then
            ConfigureChartAppearance()
            PopulateCharts()
            StartChartAnimation()
            chartsInitialized = True
        End If
    End Sub

    Public Sub TransitionComplete()
        ' Now that the transition is complete, initialize and show charts if data is ready
        If chartDataReady Then
            ShowCharts()
        End If
    End Sub

    ' Helper method to check if this form is inside a transition
    Private Function IsInsideTransition() As Boolean
        ' Check if this form is inside a transition by looking at its parent
        If Me.Parent IsNot Nothing AndAlso TypeOf Me.Parent Is Panel Then
            Dim parentForm As Form = DirectCast(Me.Parent, Panel).FindForm()
            If TypeOf parentForm Is Form1 Then
                Return DirectCast(parentForm, Form1).IsFormTransitioning()
            End If
        End If
        Return False
    End Function

    ' Add this field to your class
    Private chartsInitialized As Boolean = False

    Private Sub SetupChartControls()
        ' Setup daily sales chart
        cartesianChart1.Size = Chart1.Size
        cartesianChart1.Location = Chart1.Location
        cartesianChart1.BackColor = Color.Transparent
        cartesianChart1.Visible = False  ' Start invisible

        ' Configure animations based on performance mode
        If disableAllAnimations Then
            cartesianChart1.DisableAnimations = True
        ElseIf isLowPerformanceMode Then
            cartesianChart1.AnimationsSpeed = TimeSpan.FromMilliseconds(100)
        End If

        Me.Controls.Add(cartesianChart1)
        Chart1.Visible = False

        ' Setup top selling products chart
        cartesianChart2.Size = Chart2.Size
        cartesianChart2.Location = Chart2.Location
        cartesianChart2.BackColor = Color.Transparent
        cartesianChart2.Visible = False  ' Start invisible

        ' Configure animations based on performance mode
        If disableAllAnimations Then
            cartesianChart2.DisableAnimations = True
        ElseIf isLowPerformanceMode Then
            cartesianChart2.AnimationsSpeed = TimeSpan.FromMilliseconds(100)
        End If

        Me.Controls.Add(cartesianChart2)
        Chart2.Visible = False

        ' Setup pie chart for category distribution
        pieChart1.Size = Chart3.Size
        pieChart1.Location = Chart3.Location
        pieChart1.BackColor = Color.Transparent
        pieChart1.Visible = False  ' Start invisible

        ' Configure animations based on performance mode
        If disableAllAnimations Then
            pieChart1.DisableAnimations = True
        ElseIf isLowPerformanceMode Then
            pieChart1.AnimationsSpeed = TimeSpan.FromMilliseconds(100)
        End If

        Me.Controls.Add(pieChart1)
        Chart3.Visible = False
    End Sub

    ' Start animated display of charts with optimized timing
    Private Sub StartChartAnimation()
        chartAnimationStep = 0
        chartAnimationsComplete = False

        ' Skip animation completely if disabled
        If disableAllAnimations Then
            ' Show all charts at once
            cartesianChart1.Visible = True
            cartesianChart2.Visible = True
            pieChart1.Visible = True
            chartAnimationsComplete = True
            Return
        End If

        ' Use faster animation on low-end systems
        If isLowPerformanceMode Then
            chartAnimationTimer.Interval = 80  ' Faster display
        Else
            chartAnimationTimer.Interval = 150  ' Standard timing
        End If

        chartAnimationTimer.Start()
    End Sub

    ' Handle chart animation timing - reveal each chart one by one
    Private Sub ChartAnimationTimer_Tick(sender As Object, e As EventArgs) Handles chartAnimationTimer.Tick
        Select Case chartAnimationStep
            Case 0  ' Reveal first chart
                cartesianChart1.Visible = True
                chartAnimationStep += 1

            Case 1  ' Reveal second chart
                cartesianChart2.Visible = True
                chartAnimationStep += 1

            Case 2  ' Reveal third chart
                pieChart1.Visible = True
                chartAnimationStep += 1

            Case Else  ' All animations complete
                chartAnimationTimer.Stop()
                chartAnimationsComplete = True
        End Select
    End Sub

    Private Sub ConfigureChartAppearance()
        ' Configure daily sales chart appearance
        cartesianChart1.AxisX.Add(New Axis With {
            .Title = "Date",
            .Labels = New List(Of String)(),
            .Separator = New Separator With {
                .StrokeThickness = 1,
                .Stroke = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 220, 220))
            },
            .Foreground = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)) ' Black text
        })

        cartesianChart1.AxisY.Add(New Axis With {
            .Title = "Total Sales (₱)",
            .LabelFormatter = Function(value) value.ToString("N0"),
            .Separator = New Separator With {
                .StrokeThickness = 1,
                .Stroke = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 220, 220))
            },
            .Foreground = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)) ' Black text
        })

        ' Configure top selling products chart appearance
        cartesianChart2.AxisY.Add(New Axis With {
            .Title = "Products",
            .Labels = New List(Of String)(),
            .Separator = New Separator With {
                .StrokeThickness = 0
            },
            .Foreground = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)) ' Black text
        })

        cartesianChart2.AxisX.Add(New Axis With {
            .Title = "Quantity Sold",
            .Separator = New Separator With {
                .StrokeThickness = 1,
                .Stroke = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 220, 220))
            },
            .Foreground = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)) ' Black text
        })
    End Sub

    ' Populate all charts from dataset
    Private Sub PopulateCharts()
        Try
            ' Populate daily sales trend chart
            If dailySalesData IsNot Nothing AndAlso dailySalesData.Tables.Contains("DailySales") AndAlso dailySalesData.Tables("DailySales").Rows.Count > 0 Then
                ' Create collections for chart data
                Dim values As New ChartValues(Of Decimal)()
                Dim labels As New List(Of String)()

                ' Populate the data
                For Each row As DataRow In dailySalesData.Tables("DailySales").Rows
                    Dim saleDate As Date = Convert.ToDateTime(row("sale_date"))
                    Dim dailySales As Decimal = Convert.ToDecimal(row("daily_sales"))
                    values.Add(dailySales)
                    labels.Add(saleDate.ToString("MM/dd"))
                Next

                ' Create column series for daily sales with reduced points for performance
                Dim series = New ColumnSeries With {
                    .Title = "Daily Sales",
                    .Values = values,
                    .DataLabels = True,
                    .Fill = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(143, 134, 128)),
                    .Stroke = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(104, 95, 89)),
                    .StrokeThickness = 1,
                    .Foreground = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)) ' Black text for data labels
}

                ' Update chart
                cartesianChart1.Series = New SeriesCollection From {series}

                ' Set the labels
                cartesianChart1.AxisX(0).Labels = labels
                cartesianChart1.LegendLocation = LegendLocation.None
            End If

            ' Populate top selling products chart
            If topProductsData IsNot Nothing AndAlso topProductsData.Tables.Contains("TopProducts") AndAlso topProductsData.Tables("TopProducts").Rows.Count > 0 Then
                ' Create collections for chart data
                Dim values As New ChartValues(Of Integer)()
                Dim labels As New List(Of String)()

                ' Limit to top 5 products for better performance
                Dim rowCount As Integer = Math.Min(5, topProductsData.Tables("TopProducts").Rows.Count)

                ' Populate the data 
                For i As Integer = 0 To rowCount - 1
                    Dim row As DataRow = topProductsData.Tables("TopProducts").Rows(i)
                    Dim foodName As String = row("foodname").ToString()
                    Dim totalQuantity As Integer = Convert.ToInt32(row("total_quantity"))
                    values.Add(totalQuantity)
                    labels.Add(foodName)
                Next

                ' Create horizontal bar series
                Dim series = New RowSeries With {
                    .Title = "Products Sold",
                    .Values = values,
                    .DataLabels = True,
                    .Fill = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(143, 134, 128)),
                    .Stroke = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(104, 95, 89)),
                    .StrokeThickness = 1,
                    .Foreground = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)) ' Black text for data labels
}

                ' Update chart
                cartesianChart2.Series = New SeriesCollection From {series}
                cartesianChart2.AxisY(0).Labels = labels
                cartesianChart2.LegendLocation = LegendLocation.None
            End If

            ' Populate category distribution pie chart
            If categoryData IsNot Nothing AndAlso categoryData.Tables.Contains("Categories") AndAlso categoryData.Tables("Categories").Rows.Count > 0 Then
                ' Create new pie series
                Dim seriesCollection As New SeriesCollection()

                ' Set of colors for the pie chart
                Dim colors As New List(Of System.Windows.Media.Color) From {
                    System.Windows.Media.Color.FromRgb(143, 134, 128),  ' Base color from your UI
                    System.Windows.Media.Color.FromRgb(163, 154, 148),  ' Lighter version
                    System.Windows.Media.Color.FromRgb(123, 114, 108),  ' Darker version
                    System.Windows.Media.Color.FromRgb(183, 174, 168),  ' Even lighter
                    System.Windows.Media.Color.FromRgb(103, 94, 88)     ' Even darker
                }

                Dim colorIndex As Integer = 0

                ' Limit to top 5 categories for performance
                Dim maxCategories As Integer = Math.Min(5, categoryData.Tables("Categories").Rows.Count)

                For i As Integer = 0 To maxCategories - 1
                    Dim row As DataRow = categoryData.Tables("Categories").Rows(i)
                    Dim category As String = row("category").ToString()
                    Dim count As Integer = Convert.ToInt32(row("category_count"))

                    ' Create pie slice with simpler rendering
                    Dim pieSeries = New PieSeries With {
                        .Title = category,
                        .Values = New ChartValues(Of Integer) From {count},
                        .DataLabels = True,
                        .LabelPoint = Function(chartPoint) String.Format("{0}", chartPoint.SeriesView.Title),
                        .Fill = New System.Windows.Media.SolidColorBrush(colors(colorIndex Mod colors.Count)),
                        .Foreground = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)) ' Black text for labels
}

                    ' Add to collection
                    seriesCollection.Add(pieSeries)

                    colorIndex += 1
                Next

                ' Update pie chart
                pieChart1.Series = seriesCollection
                pieChart1.LegendLocation = LegendLocation.Right

                ' Configure legend with simpler rendering
                pieChart1.DefaultLegend = New DefaultLegend With {
                    .Foreground = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0))
                }
            End If
        Catch ex As Exception
            Debug.WriteLine("Error populating charts: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadCriticalItems()
        ' Connection string for the database
        Dim ConnectionString As String = "server=localhost;port=3307;user=root;password=;database=brewtopia_db"

        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                ' SQL query to count critical items
                Dim query As String = "SELECT COUNT(*) AS critical_count FROM tbl_inventoryad WHERE quantity < 11;"
                Dim cmd As New MySqlCommand(query, conn)

                ' Execute the query and fetch the count
                Dim criticalCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' Display the count in lblcriticalitems
                lblcriticalitems.Text = criticalCount.ToString()
            End Using
        Catch ex As Exception
            ' Silent error for critical items - less important than other data
            lblcriticalitems.Text = "0"
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lbl_date1.Text = Date.Now.ToString("ddd, dd-MM-yyyy")
        lbl_time.Text = Date.Now.ToString("hh:mm:ss tt")
    End Sub

    Private Sub DisplayTotalProducts()
        Using conn As New MySqlConnection(ConnectionString)
            Try
                ' Open the database connection
                If conn.State = ConnectionState.Closed Then conn.Open()

                ' SQL query to count the total number of rows in tbl_food
                Dim cmd As New MySqlCommand("SELECT COUNT(`ID`) FROM `tbl_food`", conn)

                ' Execute the query and get the total number of products
                Dim totalProducts As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' Display the total number of products in the label
                lbltotalproducts.Text = totalProducts.ToString()

            Catch ex As Exception
                ' Handle any errors
                lbltotalproducts.Text = "0"
            End Try
        End Using
    End Sub

    Private Sub LoadTotalProductsSold()
        Dim totalSold As Integer = 0

        Try
            ' Create new connection using the ConnectionString
            Using conn As New MySqlConnection(ConnectionString)
                If conn.State = ConnectionState.Closed Then conn.Open()

                ' SQL query to get total products sold today, using transdate format
                Dim cmd As New MySqlCommand("SELECT SUM(qty) AS total_sold FROM tbl_pos WHERE DATE_FORMAT(transdate, '%Y-%m-%d') = CURDATE()", conn)

                ' Execute the query and retrieve the result
                Dim result As Object = cmd.ExecuteScalar()

                ' If the result is not null, assign it to totalSold
                If result IsNot DBNull.Value Then
                    totalSold = Convert.ToInt32(result)
                End If

                ' Display the result in lblTotalProductSold
                lbltotalproductsold.Text = totalSold.ToString()
            End Using

        Catch ex As Exception
            lbltotalproductsold.Text = "0"
        End Try
    End Sub

    ' Clean up resources when form closes
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        ' Cancel any background operations
        If chartDataWorker IsNot Nothing AndAlso chartDataWorker.IsBusy Then
            chartDataWorker.CancelAsync()
        End If

        If chartAnimationTimer IsNot Nothing Then
            chartAnimationTimer.Stop()
        End If

        ' Clean up chart resources
        If cartesianChart1 IsNot Nothing Then
            cartesianChart1.Series = Nothing
            cartesianChart1.AxisX.Clear()
            cartesianChart1.AxisY.Clear()
        End If

        If cartesianChart2 IsNot Nothing Then
            cartesianChart2.Series = Nothing
            cartesianChart2.AxisX.Clear()
            cartesianChart2.AxisY.Clear()
        End If

        If pieChart1 IsNot Nothing Then
            pieChart1.Series = Nothing
        End If

        ' Force garbage collection before disposal
        GC.Collect(0, GCCollectionMode.Optimized)

        MyBase.OnFormClosing(e)
    End Sub

    ' Existing methods remain unchanged
    Private Sub LoadTopSellingProducts()
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                ' SQL Query to get the top-selling products
                Dim query As String = "SELECT foodname, SUM(qty) AS total_quantity " &
                                  "FROM tbl_pos " &
                                  "GROUP BY foodname " &
                                  "ORDER BY total_quantity DESC " &
                                  "LIMIT 10;"
                Dim cmd As New MySqlCommand(query, conn)

                ' Execute the query and load the data
                Dim reader As MySqlDataReader = cmd.ExecuteReader()

                ' Create collections for chart data
                Dim values As New ChartValues(Of Integer)()
                Dim labels As New List(Of String)()

                ' Populate the data from query
                While reader.Read()
                    Dim foodName As String = reader("foodname").ToString()
                    Dim totalQuantity As Integer = Convert.ToInt32(reader("total_quantity"))
                    values.Add(totalQuantity)
                    labels.Add(foodName)
                End While

                reader.Close()

                ' Create horizontal bar series
                Dim series = New RowSeries With {
                    .Title = "Products Sold",
                    .Values = values,
                    .DataLabels = True,
                    .Fill = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(143, 134, 128)),
                    .Stroke = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(104, 95, 89)),
                    .StrokeThickness = 1,
                    .Foreground = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)) ' Black text for data labels
                }

                ' Update chart
                cartesianChart2.Series = New SeriesCollection From {series}

                ' Set the labels
                cartesianChart2.AxisY(0).Labels = labels

                ' Add title with black text
                cartesianChart2.LegendLocation = LegendLocation.None
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading top-selling products: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadDailySalesTrend()
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                Dim query As String = "SELECT DATE(transdate) AS sale_date, SUM(totalprice) AS daily_sales " &
                                      "FROM tbl_pos " &
                                      "WHERE transdate >= CURDATE() - INTERVAL 7 DAY " &
                                      "GROUP BY sale_date " &
                                      "ORDER BY sale_date;"

                Dim cmd As New MySqlCommand(query, conn)
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim ds As New DataSet()
                adapter.Fill(ds, "DailySales")

                ' Check if there is data to display
                If ds.Tables("DailySales").Rows.Count > 0 Then
                    ' Create collections for chart data
                    Dim values As New ChartValues(Of Decimal)()
                    Dim labels As New List(Of String)()

                    ' Populate the data
                    For Each row As DataRow In ds.Tables("DailySales").Rows
                        Dim saleDate As Date = Convert.ToDateTime(row("sale_date"))
                        Dim dailySales As Decimal = Convert.ToDecimal(row("daily_sales"))
                        values.Add(dailySales)
                        labels.Add(saleDate.ToString("MM/dd"))
                    Next

                    ' Create column series for daily sales
                    Dim series = New ColumnSeries With {
                        .Title = "Daily Sales",
                        .Values = values,
                        .DataLabels = True,
                        .Fill = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(143, 134, 128)),
                        .Stroke = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(104, 95, 89)),
                        .StrokeThickness = 1,
                        .Foreground = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)) ' Black text for data labels
                    }

                    ' Update chart
                    cartesianChart1.Series = New SeriesCollection From {series}

                    ' Set the labels
                    cartesianChart1.AxisX(0).Labels = labels

                    ' Add title with black text
                    cartesianChart1.LegendLocation = LegendLocation.None
                Else
                    ' If no data, clear the chart
                    cartesianChart1.Series = New SeriesCollection()
                    MsgBox("No sales data available for the last 7 days.", MsgBoxStyle.Information, "No Data")
                End If
            End Using
        Catch ex As Exception
            MsgBox("Error loading daily sales trend: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        End Try
    End Sub

    Private Sub LoadProductCategoryDistribution()
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                ' SQL Query to get product categories distribution
                Dim query As String = "SELECT category, COUNT(*) as category_count " &
                                      "FROM tbl_food " &
                                      "GROUP BY category " &
                                      "ORDER BY category_count DESC;"

                Dim cmd As New MySqlCommand(query, conn)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()

                ' Create new pie series
                Dim seriesCollection As New SeriesCollection()

                ' Set of colors for the pie chart
                Dim colors As New List(Of System.Windows.Media.Color) From {
                    System.Windows.Media.Color.FromRgb(143, 134, 128),  ' Base color from your UI
                    System.Windows.Media.Color.FromRgb(163, 154, 148),  ' Lighter version
                    System.Windows.Media.Color.FromRgb(123, 114, 108),  ' Darker version
                    System.Windows.Media.Color.FromRgb(183, 174, 168),  ' Even lighter
                    System.Windows.Media.Color.FromRgb(103, 94, 88),    ' Even darker
                    System.Windows.Media.Color.FromRgb(193, 184, 178),
                    System.Windows.Media.Color.FromRgb(93, 84, 78),
                    System.Windows.Media.Color.FromRgb(203, 194, 188),
                    System.Windows.Media.Color.FromRgb(83, 74, 68),
                    System.Windows.Media.Color.FromRgb(213, 204, 198)
                }

                Dim colorIndex As Integer = 0

                While reader.Read()
                    Dim category As String = reader("category").ToString()
                    Dim count As Integer = Convert.ToInt32(reader("category_count"))

                    ' Add a new pie slice
                    seriesCollection.Add(New PieSeries With {
                        .Title = category,
                        .Values = New ChartValues(Of Integer) From {count},
                        .DataLabels = True,
                        .LabelPoint = Function(chartPoint) String.Format("{0}: {1}", chartPoint.SeriesView.Title, chartPoint.Y),
                        .Fill = New System.Windows.Media.SolidColorBrush(colors(colorIndex Mod colors.Count)),
                        .Foreground = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)) ' Black text for labels
                    })

                    colorIndex += 1
                End While

                reader.Close()

                ' Update pie chart
                pieChart1.Series = seriesCollection
                pieChart1.LegendLocation = LegendLocation.Right

                ' Configure legend
                pieChart1.DefaultLegend = New DefaultLegend With {
                    .Foreground = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0))
                }
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading product categories: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub DisplayDailySales()
        Using conn As New MySqlConnection(ConnectionString)
            Try
                ' Open the database connection
                If conn.State = ConnectionState.Closed Then conn.Open()

                ' Define the start and end of the current day
                Dim todayStart As String = Date.Now.ToString("yyyy-MM-dd 00:00:00")
                Dim todayEnd As String = Date.Now.ToString("yyyy-MM-dd 23:59:59")

                ' SQL query to calculate the total daily sales
                Dim cmd As New MySqlCommand("SELECT IFNULL(SUM(`totalprice`), 0) FROM `tbl_pos` WHERE `transdate` BETWEEN @todayStart AND @todayEnd", conn)
                cmd.Parameters.AddWithValue("@todayStart", todayStart)
                cmd.Parameters.AddWithValue("@todayEnd", todayEnd)

                ' Execute the query and get the total daily sales
                Dim dailyTotal As Decimal = Convert.ToDecimal(cmd.ExecuteScalar())

                ' Format the total and display it in the label
                lbldailysales.Text = "₱" & FormatNumber(dailyTotal, 2)

            Catch ex As Exception
                ' Handle any errors
                lbldailysales.Text = "₱0.00"
            End Try
        End Using
    End Sub

End Class
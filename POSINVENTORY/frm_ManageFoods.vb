Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class frm_ManageFoods
    Private Sub GunaCirclePictureBox1_Click(sender As Object, e As EventArgs) Handles GunaCirclePictureBox1.Click
        Dim ofd As New OpenFileDialog
        ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"

        If ofd.ShowDialog = DialogResult.OK Then
            TextBox3.Text = ofd.FileName
            GunaCirclePictureBox1.Image = Image.FromFile(TextBox3.Text)

            ' Hide placeholder when an image is selected
            GunaCirclePictureBox2.Visible = False
        End If
    End Sub



    Private Sub frm_ManageFoods_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        auto_foodcode()
        GunaCirclePictureBox2.Visible = True ' Show placeholder
        GunaCirclePictureBox2.Enabled = False ' Make it unclickable (clicks pass through)
    End Sub


    Sub auto_foodcode()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            cmd = New MySqlCommand("SELECT * FROM `tbl_food` order by id desc", conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows = True Then
                txt_foodcode.Text = dr.Item("foodcode").ToString + 1

            Else
                txt_foodcode.Text = Date.Now.ToString("yyyyMM") & "001"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If conn.State = ConnectionState.Open Then conn.Close()

    End Sub
    Sub clear()
        txt_foodname.Clear()
        txt_category.Clear()
        GunaCirclePictureBox1.Image = Nothing

        ' Show placeholder again
        GunaCirclePictureBox2.Visible = True
    End Sub

    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        ' Check if txt_foodname or txt_category is empty
        If String.IsNullOrWhiteSpace(txt_foodname.Text) OrElse String.IsNullOrWhiteSpace(txt_category.Text) Then
            MsgBox("Please fill in all required fields!", MsgBoxStyle.Exclamation, "Validation Error")
            Return ' Stop execution
        End If

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            Dim cmd As New MySqlCommand("INSERT INTO `tbl_food`(`foodcode`, `foodname`, `img`, `category`) VALUES (@foodcode, @foodname, @img, @category)", conn)
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@foodcode", txt_foodcode.Text)
            cmd.Parameters.AddWithValue("@foodname", txt_foodname.Text)
            cmd.Parameters.AddWithValue("@category", txt_category.Text) ' Add category parameter

            ' Check if an image is selected in GunaCirclePictureBox1
            If GunaCirclePictureBox1.Image IsNot Nothing Then
                ' Use the selected image
                Dim mstream As New System.IO.MemoryStream
                GunaCirclePictureBox1.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
                Dim picture() As Byte = mstream.GetBuffer
                mstream.Close()
                cmd.Parameters.AddWithValue("@img", picture)
            Else
                ' Use a default placeholder image if no image is provided
                Dim defaultImagePath As String = Application.StartupPath & "\placeholder.jpg" ' Path to your default image
                Dim defaultImage As Image = Image.FromFile(defaultImagePath)
                Dim mstream As New System.IO.MemoryStream
                defaultImage.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
                Dim picture() As Byte = mstream.GetBuffer
                mstream.Close()
                cmd.Parameters.AddWithValue("@img", picture)
            End If

            Dim i As Integer = cmd.ExecuteNonQuery()
            If i > 0 Then
                Dim list As List = Application.OpenForms.OfType(Of List)().FirstOrDefault()
                list.Load_Foods() ' Refresh the inventory list
                list.LoadCategories()

                MsgBox("New Product Saved Successfully!", vbInformation, "BREWTOPIA")
            Else
                MsgBox("Warning: Product Save Failed!", vbCritical, "BREWTOPIA")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try

        clear()
        Me.Close()
    End Sub

    Private Sub btn_find_Click(sender As Object, e As EventArgs) Handles btn_find.Click
        clear()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            cmd = New MySqlCommand("SELECT * FROM `tbl_food` WHERE `foodcode`=@foodcode", conn)
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@foodcode", txt_found.Text)
            dr = cmd.ExecuteReader
            While dr.Read
                Dim foodcode As String = dr.Item("foodcode")
                Dim foodname As String = dr.Item("foodname")
                'Dim price As Decimal = dr.Item("price")

                txt_foodcode.Text = foodcode
                txt_foodname.Text = foodname
                'txt_price.Text = price
                Dim bytes As [Byte]() = dr.Item("img")
                Dim ms As New MemoryStream(bytes)
                GunaCirclePictureBox1.Image = Image.FromStream(ms)

            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If conn.State = ConnectionState.Open Then conn.Close()

    End Sub
    Private Sub GunaButton3_Click(sender As Object, e As EventArgs) Handles GunaButton3.Click
        clear()

        'Dim list As List = Application.OpenForms.OfType(Of List)().FirstOrDefault()
        'If list IsNot Nothing Then
        '    list.Load_Foods() ' Refresh the inventory list
        'End If

        Me.Close()
    End Sub
    Private Sub txt_foodname_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_foodname.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevents the beep sound
            btn_save.PerformClick() ' Triggers the button click
        End If
    End Sub

    Private Sub txt_category_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_category.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevents the beep sound
            btn_save.PerformClick() ' Triggers the button click
        End If
    End Sub

End Class
Imports MySql.Data.MySqlClient

Public Class EditForm
    Public Property FoodCode As String

    Private Sub EditForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadFoodDetails()
        LoadSizes()

    End Sub
    Private Sub GunaCirclePictureBox1_Click(sender As Object, e As EventArgs) Handles GunaCirclePictureBox1.Click
        Dim ofd As New OpenFileDialog
        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
            GunaLabel1.Text = ofd.FileName
            GunaCirclePictureBox1.Image = Image.FromFile(GunaLabel1.Text)
        End If
    End Sub

    ' Load Food details (foodname) based on the foodcode
    Private Sub LoadFoodDetails()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            ' Fetch the food details (foodname, img, and category) from tbl_food
            Dim cmd As New MySqlCommand("SELECT foodcode, foodname, img, category FROM tbl_food WHERE foodcode = @foodcode", conn)
            cmd.Parameters.AddWithValue("@foodcode", FoodCode)
            Dim dr As MySqlDataReader = cmd.ExecuteReader()

            If dr.Read() Then
                ' Load foodcode
                TextBox3.Text = dr("foodcode").ToString()

                ' Load foodname
                txtFoodName.Text = dr("foodname").ToString()

                ' Load category
                If dr("category") IsNot DBNull.Value Then
                    txt_category.Text = dr("category").ToString()
                Else
                    txt_category.Text = String.Empty
                End If

                ' Load image
                If dr("img") IsNot DBNull.Value Then
                    Dim imgData As Byte() = CType(dr("img"), Byte())
                    Dim ms As New System.IO.MemoryStream(imgData)
                    GunaCirclePictureBox1.Image = Image.FromStream(ms)
                Else
                    GunaCirclePictureBox1.Image = Nothing
                End If
            End If
        Catch ex As Exception
            MsgBox("Error loading food details: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()

        End Try
    End Sub



    ' Load sizes and prices into DataGridView
    Public Sub LoadSizes()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            Dim cmd As New MySqlCommand("SELECT size_name, price FROM tbl_food_sizes WHERE foodcode = @foodcode", conn)
            cmd.Parameters.AddWithValue("@foodcode", FoodCode)
            Dim dr As MySqlDataReader = cmd.ExecuteReader()

            ' Clear the existing rows
            DataGridView1.Rows.Clear()

            ' Add rows to DataGridView
            While dr.Read()
                DataGridView1.Rows.Add(dr("size_name").ToString(), dr("price").ToString())
            End While
        Catch ex As Exception
            'MsgBox("Error loading sizes: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()

        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        Try
            ' Update the food details (foodname, img, and category) in tbl_food
            If conn.State = ConnectionState.Closed Then conn.Open()

            Dim cmd As New MySqlCommand("UPDATE `tbl_food` SET `foodname`=@foodname, `img`=@img, `category`=@category WHERE `foodcode`=@foodcode", conn)
            cmd.Parameters.Clear()

            cmd.Parameters.AddWithValue("@foodname", txtFoodName.Text)
            cmd.Parameters.AddWithValue("@category", txt_category.Text) ' Add the category to the query

            Dim FileSize As New UInt32
            Dim mstream As New System.IO.MemoryStream
            GunaCirclePictureBox1.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim picture() As Byte = mstream.GetBuffer
            FileSize = mstream.Length
            mstream.Close()
            cmd.Parameters.AddWithValue("@img", picture)

            cmd.Parameters.AddWithValue("@foodcode", FoodCode)  ' Ensure foodcode is passed for the update

            Dim i As Integer = cmd.ExecuteNonQuery()
            If i > 0 Then
                ' Call list from the list form
                Dim list As List = Application.OpenForms.OfType(Of List)().FirstOrDefault()
                list.Load_Foods() ' Refresh the inventory list
                list.LoadCategories()

                MsgBox("Product Edit Successfully!", vbInformation, "BREWTOPIA")
            Else
                MsgBox("Warning: Product Edit Failed!", vbCritical, "BREWTOPIA")
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, vbCritical, "Error")
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub
    Private Sub btnDeletion_Click(sender As Object, e As EventArgs) Handles btndeletion.Click
        ' Ensure that the user has selected a food item to delete
        If String.IsNullOrEmpty(FoodCode) Then
            MsgBox("No product item selected to delete!", MsgBoxStyle.Exclamation, "BREWTOPIA")
            Return
        End If

        ' Confirm the deletion with the user
        If MsgBox("Are you sure you want to delete this product item and all associated sizes and ingredients?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question) = MsgBoxResult.Yes Then
            Try
                If conn.State = ConnectionState.Closed Then conn.Open()

                ' Delete from tbl_ingredients (ingredients associated with the food item)
                Dim cmdIngredients As New MySqlCommand("DELETE FROM tbl_ingredients WHERE foodcode = @foodcode", conn)
                cmdIngredients.Parameters.AddWithValue("@foodcode", FoodCode)
                cmdIngredients.ExecuteNonQuery()

                ' Delete from tbl_food_sizes (sizes associated with the food item)
                Dim cmdSizes As New MySqlCommand("DELETE FROM tbl_food_sizes WHERE foodcode = @foodcode", conn)
                cmdSizes.Parameters.AddWithValue("@foodcode", FoodCode)
                cmdSizes.ExecuteNonQuery()

                ' Delete from tbl_food (the food item itself)
                Dim cmdFood As New MySqlCommand("DELETE FROM tbl_food WHERE foodcode = @foodcode", conn)
                cmdFood.Parameters.AddWithValue("@foodcode", FoodCode)
                cmdFood.ExecuteNonQuery()

                ' Refresh the UI or reload the list of foods
                ' Call list from the list form
                Dim list As List = Application.OpenForms.OfType(Of List)().FirstOrDefault()
                list.Load_Foods() ' Refresh the inventory list
                list.LoadCategories()

                MsgBox("Product item, associated sizes, and ingredients deleted successfully!", vbInformation, "BREWTOPIA")

            Catch ex As Exception
                MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "BREWTOPIA")
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()

            End Try
            Me.Close()
        End If
    End Sub

    Private Sub btnSupplies_Click(sender As Object, e As EventArgs) Handles btnSupplies.Click
        Dim frmIngredients As New formingredients()
        frmIngredients.FoodCode = Me.FoodCode  ' Pass the FoodCode to the ingredients form
        frmIngredients.LoadFormData()  ' Initialize the form with data
        frmIngredients.Show()

        ' Here we pass the FoodCode and set the supplycode as FoodCode initially
        frmIngredients.txt_supplycode.Text = Me.FoodCode
    End Sub

    Private Sub GunaButton3_Click(sender As Object, e As EventArgs) Handles GunaButton3.Click
        Dim formingredients As formingredients = Application.OpenForms.OfType(Of formingredients)().FirstOrDefault()
        If formingredients IsNot Nothing Then
            formingredients.Close() ' Refresh the inventory list
        End If
        Me.Close()
    End Sub

End Class

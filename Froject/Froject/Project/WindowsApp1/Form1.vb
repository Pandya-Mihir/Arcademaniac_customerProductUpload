Imports System.Data.SqlClient

Public Class form1
    'Private connectionString As String = "Data Source=THOUSANDSUNNY;Initial Catalog=crudop;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"
    'Private connectionString As String = "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"

    'Dim builder As New SqlConnectionStringBuilder()
    Private ConnectionString As String = "Data Source=THOUSANDSUNNY;Initial Catalog=crudop;Integrated Security=True"

    Private parsedCustomerID As Integer




    Private Sub ClearForm()
        txtCustomerName.Clear()
        txtCustomerNumber.Clear()
        txtProductCategory.Clear()
        txtProductName.Clear()
        txtPrice.Clear()
        picProductImage.Image = My.Resources.images
    End Sub

    Private ReadOnly Property IsCustomerNameValid As Boolean
        Get
            If txtCustomerName.Text = "" Then
                MessageBox.Show("Please enter a name.")
                Return False
            Else
                Return True
            End If
        End Get
    End Property

    Private Function IsOrderDataValid() As Boolean
        If txtCustomerNumber.Text = "" Then
            MessageBox.Show("Please enter a customer number.")
            Return False
        ElseIf txtProductCategory.Text = "" Then
            MessageBox.Show("Please select a product category.")
            Return False
        ElseIf txtProductName.Text = "" Then
            MessageBox.Show("Please enter a product name.")
            Return False
        ElseIf txtPrice.Text = "" Then
            MessageBox.Show("Please enter a price.")
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ProductDetailsForm.Show()

    End Sub




    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        If ofdProductImage.ShowDialog() = DialogResult.OK Then
            picProductImage.Image = Image.FromFile(ofdProductImage.FileName)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        picProductImage.Image = My.Resources.images
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If IsCustomerNameValid And IsOrderDataValid() Then
            Dim productImage As Byte() = Nothing
            If picProductImage.Image IsNot Nothing Then
                Using ms As New IO.MemoryStream()
                    picProductImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                    productImage = ms.ToArray()
                End Using
            End If

            Using connection As New SqlConnection(ConnectionString)
                Dim query As String = "INSERT INTO ProductDetails (CustomerName, CustomerNumber, ProductCategory, ProductName, Price, ProductImage) VALUES (@CustomerName, @CustomerNumber, @ProductCategory, @ProductName, @Price, @ProductImage)"
                Dim command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text)
                'command.Parameters.AddWithValue("@CustomerNumber", Integer.Parse(txtCustomerNumber.Text))
                command.Parameters.AddWithValue("@CustomerNumber", txtCustomerNumber.Text)
                command.Parameters.AddWithValue("@ProductCategory", txtProductCategory.Text)
                command.Parameters.AddWithValue("@ProductName", txtProductName.Text)
                command.Parameters.AddWithValue("@Price", Decimal.Parse(txtPrice.Text))
                command.Parameters.AddWithValue("@ProductImage", productImage)
                connection.Open()
                command.ExecuteNonQuery()
            End Using

            MessageBox.Show("Product details uploaded successfully.")
            ClearForm()
            ProductDetailsForm.ShowDialog()

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        txtCustomerName.Text = ""
        txtCustomerNumber.Text = ""
        txtProductCategory.Text = ""
        txtProductName.Text = ""
        txtPrice.Text = ""
        Button4.PerformClick()
    End Sub





    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Using connection As New SqlConnection(ConnectionString)
    '        Dim query As String = "SELECT * FROM ProductDetails"
    '        Dim adapter As New SqlDataAdapter(query, connection)
    '        Dim table As New DataTable()
    '        adapter.Fill(table)

    '        ProductDetailsForm.DataGridView1.DataSource = table
    '        ProductDetailsForm.ShowDialog()
    '    End Using
    'End Sub
End Class
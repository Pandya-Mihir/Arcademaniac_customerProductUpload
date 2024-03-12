Imports System.Data.SqlClient
Imports System.Drawing.Bitmap
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Public Class ProductDetailsForm
    Private ConnectionString As String = "Data Source=THOUSANDSUNNY;Initial Catalog=crudop;Integrated Security=True"
    'Private Sub ProductDetailsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '    ' LoadProductDetails()


    'End Sub
    Private Sub ProductDetailsForm_load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connectionString As String = "Data Source=THOUSANDSUNNY;Initial Catalog=crudop;Integrated Security=True"
        Dim query As String = "SELECT ID,CustomerName,CustomerNumber,ProductName,Price, ProductImage FROM ProductDetails WHERE ID = (SELECT MAX(ID) FROM ProductDetails)"

        Using connection As New SqlConnection(connectionString)
            Dim command As New SqlCommand(query, connection)
            connection.Open()
            Dim reader As SqlDataReader = command.ExecuteReader()

            If reader.Read() Then
                txtId.Text = reader("ID")
                txtCustomerName.Text = reader("CustomerName").ToString()
                txtContactNo.Text = reader("CustomerNumber").ToString()
                txtProductName.Text = reader("ProductName").ToString()
                txtPrice.Text = reader("Price").ToString()

                Dim imageBytes As Byte() = DirectCast(reader("ProductImage"), Byte())
                If imageBytes IsNot Nothing Then
                    Using ms As New MemoryStream(imageBytes)
                        PictureBox1.Image = Image.FromStream(ms)
                    End Using
                End If
            End If
        End Using
    End Sub

    'Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
    '    If e.ColumnIndex = DataGridView1.Columns("ProductImage").Index Then
    '        Dim imageBytes As Byte() = CType(e.Value, Byte())
    '        If imageBytes IsNot Nothing Then
    '            Using ms As New IO.MemoryStream(imageBytes)
    '                e.Value = Image.FromStream(ms)
    '            End Using
    '        End If
    '    End If
    'End Sub
    Private Sub LoadProductDetails()
        Using connection As New SqlConnection(ConnectionString)
            Dim query As String = "SELECT * FROM ProductDetails "
            Dim adapter As New SqlDataAdapter(query, connection)
            Dim table As New DataTable()
            adapter.Fill(table)

            'DataGridView1.DataSource = table
        End Using
    End Sub



    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtCustomerName.TextChanged

    End Sub
    'Private Sub DataGridView1CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
    '    If e.ColumnIndex = DataGridView1.Columns("ProductImage").Index Then
    '        Dim image As Image = CType(e.Value, Image)
    '        If image IsNot Nothing Then
    '            Dim imageColumn As DataGridViewImageColumn = DirectCast(DataGridView1.Columns("ProductImage"), DataGridViewImageColumn)
    '            imageColumn.ImageLayout = DataGridViewImageCellLayout.Normal ' or DataGridViewImageCellLayout.Stretch
    '            e.Value = image
    '        End If
    '    End If
    'End Sub
    'Private Sub DataGridView1CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
    '    If e.ColumnIndex = DataGridView1.Columns("ProductImage").Index Then
    '        Dim imageBytes As Byte() = CType(e.Value, Byte())
    '        If imageBytes IsNot Nothing Then
    '            Using ms As New IO.MemoryStream(imageBytes)
    '                e.Value = Image.FromStream(ms)
    '            End Using
    '        End If
    '    End If
    'End Sub

End Class
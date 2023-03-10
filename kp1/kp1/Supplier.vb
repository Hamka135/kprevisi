Imports MySql.Data.MySqlClient

Public Class Supplier
    Private Sub Supplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Tampil_data()
        Button2.Enabled = False
        Button1.Enabled = False
    End Sub

    'menampilkan data dari mysql ke dalam datagrid
    Sub Tampil_data()
        koneksi()
        da = New MySqlDataAdapter("SELECT * FROM supplier order by IdSupplier", con)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "supplier")
        Me.DataGridView1.DataSource = (ds.Tables("supplier"))

        nomor()
    End Sub
    'pemberian id
    Sub nomor()
        koneksi()
        cmd = New MySqlCommand("Select * from SUPPLIER where IdSupplier in (Select max(IdSupplier) from supplier)", con)
        Dim UrutanKode As String
        Dim Hitung As Long
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            UrutanKode = "SPL" + "001"
        Else
            Hitung = Microsoft.VisualBasic.Right(rd.GetString(0), 3) + 1
            UrutanKode = "SPL" + Microsoft.VisualBasic.Right("000" & Hitung, 3)
        End If
        TextBox1.Text = UrutanKode
    End Sub

    'input data ke database
    Sub simpan()
        koneksi()

        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Then
            MsgBox("isi data dengan lengkap")
        Else
            Dim simpan As String
            MsgBox("data anda akan disimpan")
            simpan = "INSERT INTO supplier (IdSupplier,NamaSupplier,Kontak,Alamat,Kota) VALUES('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "')"
            cmd = New MySqlCommand(simpan, con)
            cmd.ExecuteNonQuery()
        End If

    End Sub
    'hapus data
    Sub hapus()
        koneksi()
        Dim hapus As String

        hapus = "DELETE FROM supplier WHERE IdSupplier = '" & TextBox1.Text & "'"
        cmd = New MySqlCommand(hapus, con)
        cmd.ExecuteNonQuery()
        Tampil_data()
    End Sub
    Sub edit()
        koneksi()

        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Then
            MsgBox("Data tidak ada yang diupdate")
        Else
            MsgBox(" Data akan diupdate")
            Dim edit As String

            edit = "UPDATE supplier SET IdSupplier ='" & TextBox1.Text & "' , NamaSupplier = '" & TextBox2.Text & "', Kontak = '" & TextBox3.Text & "', Alamat = '" & TextBox4.Text & "', Kota = '" & TextBox5.Text & "'WHERE IdSupplier = '" & TextBox1.Text & "'"
            cmd = New MySqlCommand(edit, con)
            cmd.ExecuteNonQuery()
            Me.Show()
            Tampil_data()
        End If
    End Sub
    'mengklik data yang dipilih
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim pilihdata

        On Error Resume Next
        pilihdata = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        TextBox1.Text = pilihdata
        TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        TextBox4.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        TextBox5.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value

        Button3.Enabled = False
        Button2.Enabled = True
        Button1.Enabled = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        hapus()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        edit()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        simpan()
        TextBox2.Focus()
    End Sub
    'validasi angka textbox
    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then
            e.Handled = True
        End If
    End Sub
End Class
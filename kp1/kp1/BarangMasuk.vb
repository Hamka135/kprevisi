Imports MySql.Data.MySqlClient
Public Class BarangMasuk
    Private Sub BarangMasuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Tampil_data()
        tampilCB()
        tampilCB2()
        TextBox4.Text = Today
        Button1.Enabled = False
        Button2.Enabled = False
    End Sub
    'menampilkan data dari mysql ke dalam datagrid
    Sub Tampil_data()
        koneksi()
        da = New MySqlDataAdapter("SELECT * FROM brg_masuk order by IdBMasuk", con)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "brg_masuk")
        Me.DataGridView1.DataSource = (ds.Tables("brg_masuk"))

        nomor()
    End Sub
    'pemberian id
    Sub nomor()
        koneksi()
        cmd = New MySqlCommand("Select * from brg_masuk where IdBMasuk in (Select max(IdBMasuk) from brg_masuk)", con)
        Dim UrutanKode As String
        Dim Hitung As Long
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            UrutanKode = "BM" + "001"
        Else
            Hitung = Microsoft.VisualBasic.Right(rd.GetString(0), 3) + 1
            UrutanKode = "BM" + Microsoft.VisualBasic.Right("000" & Hitung, 3)
        End If
        TextBox1.Text = UrutanKode
    End Sub

    'input data ke database
    Sub simpan()
        koneksi()

        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox5.Text = "" Or ComboBox1.Text = "" Or ComboBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
            MsgBox("isi data dengan lengkap")
        Else
            Dim simpan As String
            MsgBox("data anda akan disimpan")
            simpan = "INSERT INTO brg_masuk (IdBMasuk,IdBrg,IdSupplier,NamaBarang,NamaSupplier,Jumlah,Tanggal) VALUES('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox5.Text & "','" & ComboBox1.Text & "', '" & ComboBox2.Text & "', '" & TextBox3.Text & "', '" & TextBox4.Text & "')"
            cmd = New MySqlCommand(simpan, con)
            cmd.ExecuteNonQuery()
        End If
        bersih()
    End Sub
    'hapus data
    Sub hapus()
        koneksi()
        Dim hapus As String

        hapus = "DELETE FROM brg_masuk WHERE IdBMasuk = '" & TextBox1.Text & "'"
        cmd = New MySqlCommand(hapus, con)
        cmd.ExecuteNonQuery()
        bersih()
        Tampil_data()
    End Sub
    'edit data
    Sub edit()
        koneksi()

        If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Data tidak ada yang diupdate")
        Else
            MsgBox(" Data akan diupdate")
            Dim edit As String

            edit = "UPDATE brg_masuk SET IdBMasuk ='" & TextBox1.Text & "' , IdBrg = '" & TextBox2.Text & "', NamaBarang = '" & ComboBox1.Text & "', NamaSupplier = '" & ComboBox2.Text & "', Jumlah = '" & TextBox3.Text & "' ,Tanggal = '" & TextBox4.Text & "'WHERE IdBMasuk = '" & TextBox1.Text & "'"
            cmd = New MySqlCommand(edit, con)
            cmd.ExecuteNonQuery()
            Me.bersih()
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
        TextBox5.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        ComboBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        ComboBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
        TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value
        TextBox4.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value

        Button3.Enabled = False
        Button1.Enabled = True
        Button2.Enabled = True
    End Sub
    'Refresh form
    Sub bersih()
        TextBox3.Text = ""
        nomor()
        Tampil_data()
    End Sub

    'button hapus, edit, dan simpan
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        hapus()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        edit()
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        simpan()
    End Sub
    'memasukan nama barang dari database ke combobox
    Sub tampilCB()
        koneksi()
        cmd = New MySqlCommand("select NamaBarang from barang order by IdBrg", con)
        rd = cmd.ExecuteReader
        While rd.Read
            ComboBox1.Items.Add(rd("NamaBarang"))
        End While
        On Error Resume Next

    End Sub

    'memasukkan nama supplier ke combobox
    Sub tampilCB2()
        koneksi()
        cmd = New MySqlCommand("select NamaSupplier from supplier order by IdSupplier", con)
        rd = cmd.ExecuteReader
        While rd.Read
            ComboBox2.Items.Add(rd("NamaSupplier"))
        End While
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        koneksi()
        cmd = New MySqlCommand("Select IdBrg from barang where NamaBarang ='" & ComboBox1.Text & "'", con)
        rd = cmd.ExecuteReader
        rd.Read()

        If Not rd.HasRows Then
            MsgBox("Pilih Nama Barang terlebih Dahulu")
        Else
            TextBox2.Text = rd!IdBrg
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        bersih()
        Button3.Enabled = True
        Button1.Enabled = False
        Button2.Enabled = False
    End Sub

    'validasi angka textbox
    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        koneksi()
        cmd = New MySqlCommand("Select IdSupplier from supplier where Namasupplier ='" & ComboBox2.Text & "'", con)
        rd = cmd.ExecuteReader
        rd.Read()

        If Not rd.HasRows Then
            MsgBox("Pilih Nama Barang terlebih Dahulu")
        Else
            TextBox5.Text = rd!IdSupplier
        End If
    End Sub

    'cetak
    Dim con1 As MySqlConnection
    Dim cmd1 As MySqlCommand
    Dim da1 As MySqlDataAdapter
    Dim brg_masuk As New DataTable
    Sub cetak()
        cmd1 = New MySqlCommand("select * from brg_masuk where Tanggal = '" & DateTimePicker1.Text & "' ", con1)
        da1 = New MySqlDataAdapter(cmd1)
        da1.Fill(brg_masuk)

        con1.Close()
        con1.Dispose()

    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Cursor = Cursors.WaitCursor
        brg_masuk.Clear()
        Try
            con1 = New MySqlConnection("Server=localhost;user id=root;password=;database= data_barang")
            Dim RPT As New LapMasuk

            cetak()

            RPT.Database.Tables("brg_masuk").SetDataSource(brg_masuk)
            laporan.CrystalReportViewer1.ReportSource = Nothing
            laporan.CrystalReportViewer1.ReportSource = RPT
            laporan.ShowDialog()
            RPT.Clone()
            RPT.Dispose()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click
        PictureBox1.Show()
    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click
        PictureBox1.Show()
    End Sub
End Class
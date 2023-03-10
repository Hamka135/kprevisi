Imports MySql.Data.MySqlClient
Public Class DaftarBarang
    Dim con1 As MySqlConnection
    Dim cmd1 As MySqlCommand
    Dim adp1 As MySqlDataAdapter
    Dim barang As New DataTable
    Private Sub produk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Tampil_data()
        Button1.Enabled = False
        Button2.Enabled = False
    End Sub


    'menampilkan data dari mysql ke dalam datagrid
    Sub Tampil_data()
        koneksi()
        da = New MySqlDataAdapter("SELECT * FROM barang order by IdBrg", con)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "barang")
        Me.DataGridView1.DataSource = (ds.Tables("barang"))

        nomor()
    End Sub
    'pemberian id'
    Sub nomor()
        koneksi()
        cmd = New MySqlCommand("Select * from barang where IdBrg in (Select max(IdBrg) from barang)", con)
        Dim UrutanKode As String
        Dim Hitung As Long
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            UrutanKode = "BRG" + "001"
        Else
            Hitung = Microsoft.VisualBasic.Right(rd.GetString(0), 3) + 1
            UrutanKode = "BRG" + Microsoft.VisualBasic.Right("000" & Hitung, 3)
        End If
        TextBox1.Text = UrutanKode
    End Sub

    'input data ke database
    Sub simpan()
        koneksi()

        If Me.TextBox1.Text = "" Or Me.TextBox2.Text = "" Or Me.TextBox3.Text = "" Then
            MsgBox("isi data dengan lengkap")
        Else
            Dim simpan As String
            MsgBox("data anda akan disimpan")
            simpan = "INSERT INTO barang (IdBrg,Namabarang,Stok) VALUES('" & Me.TextBox1.Text & "','" & Me.TextBox2.Text & "','" & Me.TextBox3.Text & "')"
            cmd = New MySqlCommand(simpan, con)
            cmd.ExecuteNonQuery()

        End If
    End Sub


    'hapus data
    Sub hapus()
        koneksi()
        Dim hapus As String

        hapus = "DELETE FROM barang WHERE IdBrg = '" & TextBox1.Text & "'"
        cmd = New MySqlCommand(hapus, con)
        cmd.ExecuteNonQuery()
        Tampil_data()
    End Sub

    Sub edit()
        koneksi()

        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Data tidak ada yang diupdate")
        Else
            MsgBox(" Data akan diupdate")
            Dim edit As String

            edit = "UPDATE barang SET IdBrg ='" & TextBox1.Text & "' , NamaBarang = '" & TextBox2.Text & "', Stok = '" & TextBox3.Text & "'WHERE IdBrg = '" & TextBox1.Text & "'"
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

        Button3.Enabled = False
        Button2.Enabled = True
        Button1.Enabled = True
    End Sub
    'button edit, hapus dan simpan
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        hapus()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        simpan()
        TextBox2.Focus()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        edit()
    End Sub

    'validasi angka textbox
    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Cursor = Cursors.WaitCursor
        barang.Clear()
        Try
            con1 = New MySqlConnection("Server=localhost;user id=root;password=;database=data_barang")
            Dim rpt As New rptbarang

            cetaksemua()

            rpt.Database.Tables("barang").SetDataSource(barang)
            laporan.CrystalReportViewer1.ReportSource = Nothing
            laporan.CrystalReportViewer1.ReportSource = rpt
            laporan.ShowDialog()
            rpt.Clone()
            rpt.Dispose()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Sub cetaksemua()
        cmd1 = New MySqlCommand("select * from barang", con1)
        adp1 = New MySqlDataAdapter(cmd1)
        adp1.Fill(barang)

        con1.Close()
        con1.Dispose()
    End Sub
End Class
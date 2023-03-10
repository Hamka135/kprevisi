Imports MySql.Data.MySqlClient
Public Class DataUser
    Private Sub DataUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Tampil_data()
        With Me.ComboBox1
            .Items.Add("Admin")
            .Items.Add("Karyawan")
        End With
        Button10.Enabled = False
        Button11.Enabled = False
    End Sub
    'menampilkan data dari mysql ke dalam datagrid
    Sub Tampil_data()
        koneksi()
        da = New MySqlDataAdapter("SELECT * FROM login order by UserId", con)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "login")
        Me.DataGridView1.DataSource = (ds.Tables("login"))

        nomor()
    End Sub
    'pemberian id
    Sub nomor()
        koneksi()
        cmd = New MySqlCommand("Select * from login where UserId in (Select max(UserId) from login)", con)
        Dim UrutanKode As String
        Dim Hitung As Long
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            UrutanKode = "USR" + "001"
        Else
            Hitung = Microsoft.VisualBasic.Right(rd.GetString(0), 3) + 1
            UrutanKode = "USR" + Microsoft.VisualBasic.Right("000" & Hitung, 3)
        End If
        TextBox1.Text = UrutanKode
    End Sub



    'input data ke database
    Sub simpan()
        koneksi()

        If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Or TextBox3.Text = "" Then
            MsgBox("isi data dengan lengkap")
        Else
            Dim simpan As String
            MsgBox("data anda akan disimpan")
            simpan = "INSERT INTO login (UserId,Username,Level,Password) VALUES('" & TextBox1.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "', '" & TextBox3.Text & "')"
            cmd = New MySqlCommand(simpan, con)
            cmd.ExecuteNonQuery()
        End If
    End Sub



    'hapus data
    Sub hapus()
        koneksi()
        Dim hapus As String

        hapus = "DELETE FROM login WHERE UserId = '" & TextBox1.Text & "'"
        cmd = New MySqlCommand(hapus, con)
        cmd.ExecuteNonQuery()
        Tampil_data()
    End Sub

    Sub edit()
        koneksi()

        If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Data tidak ada yang diupdate")
        Else
            MsgBox(" Data akan diupdate")
            Dim edit As String

            edit = "UPDATE login SET UserId ='" & TextBox1.Text & "' , Username = '" & TextBox2.Text & "', Level = '" & ComboBox1.Text & "', Password = '" & TextBox3.Text & "'WHERE UserId = '" & TextBox1.Text & "'"
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
        ComboBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value

        Button8.Enabled = False
        Button10.Enabled = True
        Button11.Enabled = True

    End Sub
    'button hapus, edit, dan simpan
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        hapus()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        edit()
    End Sub
    Private Sub Button8_Click_1(sender As Object, e As EventArgs) Handles Button8.Click
        simpan()
    End Sub

End Class
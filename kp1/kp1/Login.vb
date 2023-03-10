Imports MySql.Data.MySqlClient

Public Class Login
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox1.Text = "" And TextBox2.Text = "" Then
            MsgBox("Data tidak boleh kosong")
        Else
            Call koneksi()
            cmd = New MySqlCommand("select * from login where username='" & TextBox1.Text & "' and password='" & TextBox2.Text & "'", con)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                If rd("Level").ToString = "Administrator" Then
                    Form1.Show()
                    Form1.Label6.Text = TextBox1.Text
                    Form1.Label10.Text = TextBox1.Text
                    Form1.Label7.Text = "Administrator"
                    MsgBox("Selamat Datang " + rd!username)
                    Me.Hide()

                Else
                    Me.Hide()
                    Form1.Show()
                    Form1.Label6.Text = TextBox1.Text
                    Form1.Label10.Text = TextBox1.Text
                    Form1.Label7.Text = "Karyawan Umum"
                    Form1.Button9.Visible = False
                    Form1.Button10.Visible = False
                    Form1.Button8.Visible = False
                End If

            Else
                MsgBox("Username atau password salah")
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        End
    End Sub
End Class
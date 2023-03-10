Public Class Form1
    Private PindahForm As Form = Nothing

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        End
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Public Sub BukaForm(Bukaform As Form)
        If PindahForm IsNot Nothing Then PindahForm.Close()
        PindahForm = Bukaform
        PindahForm.TopLevel = False
        PindahForm.FormBorderStyle = FormBorderStyle.None
        PindahForm.Dock = DockStyle.Fill
        Panel3.Controls.Add(Bukaform)
        Bukaform.BringToFront()
        Bukaform.Show()
    End Sub
    Private Sub Button2_Click_2(sender As Object, e As EventArgs) Handles Button2.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal

        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Label1.Visible = False
        Label2.Visible = False
        Label3.Visible = False
        Label4.Visible = True
        Label12.Visible = False
        Label13.Visible = False
        Label14.Visible = False
        If MsgBox("Apakah Anda Ingin Keluar? ", vbYesNo) = vbYes Then
            Me.Close()
            Login.Show()
        End If
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        BukaForm(New DaftarBarang)
        Label1.Visible = True
        Label2.Visible = False
        Label3.Visible = False
        Label4.Visible = False
        Label12.Visible = False
        Label13.Visible = False
        Label14.Visible = False

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        BukaForm(New BarangMasuk)
        Label1.Visible = False
        Label2.Visible = True
        Label3.Visible = False
        Label4.Visible = False
        Label12.Visible = False
        Label13.Visible = False
        Label14.Visible = False

    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        BukaForm(New BarangKeluar)
        Label1.Visible = False
        Label2.Visible = False
        Label3.Visible = True
        Label4.Visible = False
        Label12.Visible = False
        Label13.Visible = False
        Label14.Visible = False

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        BukaForm(New DataUser)
        Label1.Visible = False
        Label2.Visible = False
        Label3.Visible = False
        Label4.Visible = False
        Label12.Visible = False
        Label13.Visible = True
        Label14.Visible = False
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        BukaForm(New Supplier)
        Label1.Visible = False
        Label2.Visible = False
        Label3.Visible = False
        Label4.Visible = False
        Label12.Visible = False
        Label13.Visible = False
        Label14.Visible = True
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        BukaForm(New Divisi)
        Label1.Visible = False
        Label2.Visible = False
        Label3.Visible = False
        Label4.Visible = False
        Label12.Visible = True
        Label13.Visible = False
        Label14.Visible = False
    End Sub
End Class
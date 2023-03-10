Imports MySql.Data.MySqlClient

Module Utama
    Public con As MySqlConnection
    Public cmd As MySqlCommand
    Public ds As DataSet
    Public da As MySqlDataAdapter
    Public rd As MySqlDataReader
    Public db As String

    Public Sub koneksi()

        db = "Server=localhost;user id=root;password=;database=data_barang"
        con = New MySqlConnection(db)
        Try
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
        Catch ex As Exception
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
End Module


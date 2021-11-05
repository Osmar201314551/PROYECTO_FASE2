Public Class Reportes
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Hide()
        Administrador.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim datos = BD.reporte1()
        Dim tamaño As Integer = datos.Tables("Datos").Rows.Count
        DataGridView1.DataSource = datos.Tables("Datos")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim datos = BD.reporte2()
        Dim tamaño As Integer = datos.Tables("Datos").Rows.Count
        DataGridView1.DataSource = datos.Tables("Datos")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim datos = BD.reporte3()
        Dim tamaño As Integer = datos.Tables("Datos").Rows.Count
        DataGridView1.DataSource = datos.Tables("Datos")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim datos = BD.reporte4()
        Dim tamaño As Integer = datos.Tables("Datos").Rows.Count
        DataGridView1.DataSource = datos.Tables("Datos")
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim datos = BD.reporte5()
        Dim tamaño As Integer = datos.Tables("Datos").Rows.Count
        DataGridView1.DataSource = datos.Tables("Datos")
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim datos = BD.reporte6()
        Dim tamaño As Integer = datos.Tables("Datos").Rows.Count
        DataGridView1.DataSource = datos.Tables("Datos")
    End Sub
End Class
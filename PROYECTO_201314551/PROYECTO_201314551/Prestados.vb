Public Class Prestados
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Cliente.Show()
    End Sub

    Private Sub Prestados_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        Dim datos = BD.verLibrosPrestados()
        Dim tamaño As Integer = datos.Tables("Datos").Rows.Count
        DataGridView1.DataSource = datos.Tables("Datos")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim i, id As Integer
        i = DataGridView1.CurrentRow.Index
        id = DataGridView1.Item(0, i).Value
        If MsgBox("¿Desea devolver este libro?", MsgBoxStyle.Information + vbYesNo) = vbYes Then
            Dim valor As Boolean
            valor = BD.devolverLibro(id)
            If valor = True Then
                MsgBox("Se devolvio el libro exitosamente")
                BD.GuardarBitacora("Se devolvio un libro.")
                Dim datos = BD.verLibrosPrestados()
                Dim tamaño As Integer = datos.Tables("Datos").Rows.Count
                DataGridView1.DataSource = datos.Tables("Datos")
            End If
        Else
            MsgBox("Se cancelo la operacion")
        End If
    End Sub
End Class
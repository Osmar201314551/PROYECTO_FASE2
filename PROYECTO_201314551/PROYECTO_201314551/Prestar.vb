Public Class Prestar
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Cliente.Show()

    End Sub

    Private Sub Prestar_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        Dim generos = BD.verGeneros()
        Dim tam1 As Integer = generos.Tables("Datos").Rows.Count
        ComboBox1.Items.Clear()
        If tam1 > 0 Then
            For i As Integer = 0 To tam1 - 1
                Dim texto = generos.Tables("Datos").Rows(i).Item(0) & "." & generos.Tables("Datos").Rows(i).Item(1)
                ComboBox1.Items.Add(texto)
            Next
        End If

        Dim datos = BD.verLibrosParaPrestar()
        Dim tamaño As Integer = datos.Tables("Datos").Rows.Count
        DataGridView1.DataSource = datos.Tables("Datos")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim nombre As String
        nombre = TextBox1.Text
        Dim datos = BD.verLibrosParaPrestarNombre(nombre)
        Dim tamaño As Integer = datos.Tables("Datos").Rows.Count
        DataGridView1.DataSource = datos.Tables("Datos")
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim id As String
        id = ComboBox1.Text.Split(".")(0)
        Dim datos = BD.verLibrosParaPrestarGenero(id)
        Dim tamaño As Integer = datos.Tables("Datos").Rows.Count
        DataGridView1.DataSource = datos.Tables("Datos")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim i, id As Integer
        i = DataGridView1.CurrentRow.Index
        id = DataGridView1.Item(0, i).Value
        If MsgBox("¿Desea prestar este libro?", MsgBoxStyle.Information + vbYesNo) = vbYes Then
            Dim valor As Boolean
            valor = BD.prestarLibro(id)
            If valor = True Then
                MsgBox("Se presto el libro exitosamente")
                BD.GuardarBitacora("Se presto un libro.")
                Dim datos = BD.verLibrosParaPrestar()
                Dim tamaño As Integer = datos.Tables("Datos").Rows.Count
                DataGridView1.DataSource = datos.Tables("Datos")
            End If
        Else
            MsgBox("Se cancelo la operacion")
        End If
    End Sub
End Class
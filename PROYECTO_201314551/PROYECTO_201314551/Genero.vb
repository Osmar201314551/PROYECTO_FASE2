Public Class Genero
    Dim seleccionado As Integer = 0
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()
        Administrador.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If seleccionado = 0 Then
            Dim nombre, telefono, direccion As String
            nombre = TextBox1.Text
            If nombre IsNot "" Then
                Dim valor As Boolean
                valor = BD.registrarGenero(nombre)
                If valor = True Then
                    ObtenerDatos()
                    LimpiarCampos()
                    MsgBox("Se registro el genero exitosamente")
                    BD.GuardarBitacora("Se creo un nuevo genero.")
                End If
            Else
                MsgBox("No se pueden dejar campos vacios.")
            End If
        Else
            MsgBox("Hay un dato seleccionado, no se puede crear")
        End If
    End Sub

    Sub LimpiarCampos()
        TextBox1.Text = ""
    End Sub

    Sub ObtenerDatos()
        Dim datos = BD.verGeneros()
        Dim tamaño As Integer = datos.Tables("Datos").Rows.Count
        DataGridView1.DataSource = datos.Tables("Datos")

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim i, id As Integer
        i = DataGridView1.CurrentRow.Index
        id = DataGridView1.Item(0, i).Value
        If MsgBox("¿Desea seleccionar este campo?", MsgBoxStyle.Information + vbYesNo) = vbYes Then
            TextBox1.Text = DataGridView1.Item(1, i).Value
            seleccionado = id
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If seleccionado > 0 Then
            Dim nombre As String
            nombre = TextBox1.Text
            If nombre IsNot "" Then
                Dim valor As Boolean
                valor = BD.modificarGenero(seleccionado, nombre)
                If valor = True Then
                    ObtenerDatos()
                    LimpiarCampos()
                    MsgBox("Se modifico el genero exitosamente")
                    BD.GuardarBitacora("Se modifico un genero de libros")
                    seleccionado = 0
                End If
            Else
                MsgBox("No se pueden dejar campos vacios.")
            End If

        Else
            MsgBox("No se ha seleccionado un dato.")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If seleccionado > 0 Then
            Dim valor As Boolean
            If MsgBox("¿Desea eliminar el genero seleccionado?", MsgBoxStyle.Information + vbYesNo) = vbYes Then
                valor = BD.eliminarGenero(seleccionado)
                If valor = True Then
                    ObtenerDatos()
                    LimpiarCampos()
                    MsgBox("Se elimino el genero exitosamente")
                    BD.GuardarBitacora("Se elimino un genero de libros")
                    seleccionado = 0
                End If
            Else
                MsgBox("Se cancelo la operacion")
                ObtenerDatos()
                LimpiarCampos()
                seleccionado = 0
            End If
        Else
            MsgBox("No se ha seleccionado un dato.")

        End If

    End Sub

    Private Sub Genero_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        ObtenerDatos()
        LimpiarCampos()
    End Sub
End Class
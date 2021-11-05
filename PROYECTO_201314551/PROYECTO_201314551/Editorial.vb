Public Class Editorial
    Dim seleccionado As Integer = 0
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()
        Administrador.Show()
    End Sub

    Private Sub Editorial_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        ObtenerDatos()
        LimpiarCampos()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If seleccionado = 0 Then
            Dim nombre, telefono, direccion As String
            nombre = TextBox1.Text
            direccion = TextBox2.Text
            telefono = TextBox3.Text
            If nombre IsNot "" And telefono IsNot "" And direccion IsNot "" Then
                Dim valor As Boolean
                valor = BD.registrarEditorial(nombre, direccion, telefono)
                If valor = True Then
                    ObtenerDatos()
                    LimpiarCampos()
                    MsgBox("Se registro la editorial exitosamente")
                    BD.GuardarBitacora("Se creo una nueva editorial.")
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
        TextBox2.Text = ""
        TextBox3.Text = ""
    End Sub

    Sub ObtenerDatos()
        Dim datos = BD.verEditoriales()
        Dim tamaño As Integer = datos.Tables("Datos").Rows.Count
        DataGridView1.DataSource = datos.Tables("Datos")

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim i, id As Integer
        i = DataGridView1.CurrentRow.Index
        id = DataGridView1.Item(0, i).Value
        If MsgBox("¿Desea seleccionar este campo?", MsgBoxStyle.Information + vbYesNo) = vbYes Then
            TextBox1.Text = DataGridView1.Item(1, i).Value
            TextBox2.Text = DataGridView1.Item(2, i).Value
            TextBox3.Text = DataGridView1.Item(3, i).Value
            seleccionado = id
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If seleccionado > 0 Then
            Dim nombre, telefono, direccion As String
            nombre = TextBox1.Text
            direccion = TextBox2.Text
            telefono = TextBox3.Text
            If nombre IsNot "" And telefono IsNot "" And direccion IsNot "" Then
                Dim valor As Boolean
                valor = BD.modificarEditorial(seleccionado, nombre, direccion, telefono)
                If valor = True Then
                    ObtenerDatos()
                    LimpiarCampos()
                    MsgBox("Se modifico la editorial exitosamente")
                    BD.GuardarBitacora("Se modifico una editorial")
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
            If MsgBox("¿Desea eliminar la editorial seleccionada?", MsgBoxStyle.Information + vbYesNo) = vbYes Then
                valor = BD.eliminarEditorial(seleccionado)
                If valor = True Then
                    ObtenerDatos()
                    LimpiarCampos()
                    MsgBox("Se elimino la editorial exitosamente")
                    BD.GuardarBitacora("Se elimino una editorial")
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
End Class
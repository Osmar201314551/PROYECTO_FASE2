Public Class Perfil
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        Cliente.Show()
    End Sub

    Private Sub Perfil_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        ObtenerDatos()
    End Sub

    Sub ObtenerDatos()
        Dim datos = BD.obtenerUsuario(idUser)
        Dim tam1 As Integer = datos.Tables("Datos").Rows.Count
        If tam1 > 0 Then
            TextBox1.Text = datos.Tables("Datos").Rows(0).Item(1)
            TextBox2.Text = datos.Tables("Datos").Rows(0).Item(2)
            TextBox3.Text = datos.Tables("Datos").Rows(0).Item(3)
            TextBox4.Text = datos.Tables("Datos").Rows(0).Item(5)
            DateTimePicker1.Value = datos.Tables("Datos").Rows(0).Item(4)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim nombre, apellido, user, pass, fecha As String
        nombre = TextBox1.Text
        apellido = TextBox2.Text
        user = TextBox3.Text
        pass = TextBox4.Text
        fecha = DateTimePicker1.Value.ToString("yyyy-MM-dd")
        If nombre IsNot "" And apellido IsNot "" And user IsNot "" And pass IsNot "" And fecha IsNot "" Then
            Dim valor As Boolean
            valor = BD.editarPerfil(idUser, nombre, apellido, user, pass, fecha)
            If valor = True Then
                logUser = user
                MsgBox("Se guardo la información del cliente")
                BD.GuardarBitacora("Se actualizo la información del cliente.")
                Me.Hide()
                Cliente.Show()
            End If
        Else
            MsgBox("No pueden ir campos vacios")
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MsgBox("¿Desea eliminar su cuenta?", MsgBoxStyle.Information + vbYesNo) = vbYes Then
            Dim valor As Boolean
            valor = BD.borrarCuenta()
            If valor = True Then
                MsgBox("Se elimino la cuenta exitosamente")
                BD.GuardarBitacora("Se elimino la cuenta.")
                LimpiarCampos()
                Me.Hide()
                Form1.Show()
                idUser = 0
                tipoUser = 0
                imagenUser = 0
                logUser = ""
            End If
        Else
            MsgBox("Se cancelo la operacion")
        End If
    End Sub

    Sub LimpiarCampos()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""

    End Sub
End Class
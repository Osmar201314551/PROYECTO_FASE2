﻿Public Class Administrador
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Editorial.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        Genero.Show()
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Libros.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Me.Hide()
        Reportes.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If MsgBox("¿Desea cerrar su sesión actual?", MsgBoxStyle.Information + vbYesNo) = vbYes Then
            Me.Hide()
            Form1.Show()
            idUser = 0
            tipoUser = 0
            imagenUser = 0
            logUser = ""
        Else
            MsgBox("Se cancelo la operacion")
        End If
    End Sub

End Class
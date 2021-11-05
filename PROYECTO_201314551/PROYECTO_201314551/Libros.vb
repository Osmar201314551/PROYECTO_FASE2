Public Class Libros
    Dim seleccionado As Integer = 0
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()
        Administrador.Show()
    End Sub

    Private Sub Libros_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        Dim generos = BD.verGeneros()
        Dim editorial = BD.verEditoriales()
        Dim tam1 As Integer = generos.Tables("Datos").Rows.Count
        Dim tam2 As Integer = editorial.Tables("Datos").Rows.Count
        ComboBox1.Items.Clear()
        ComboBox2.Items.Clear()

        If tam1 > 0 Then
            For i As Integer = 0 To tam1 - 1
                Dim texto = generos.Tables("Datos").Rows(i).Item(0) & "." & generos.Tables("Datos").Rows(i).Item(1)
                ComboBox1.Items.Add(texto)
            Next
        End If
        If tam2 > 0 Then
            For i As Integer = 0 To tam2 - 1
                Dim texto = editorial.Tables("Datos").Rows(i).Item(0) & "." & editorial.Tables("Datos").Rows(i).Item(1)
                ComboBox2.Items.Add(texto)
            Next
        End If
        ObtenerDatos()
        LimpiarCampos()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If seleccionado = 0 Then
            Dim titulo, autor, genero, editorial As String
            Dim cantidad As Integer
            titulo = TextBox1.Text
            autor = TextBox2.Text
            genero = ComboBox1.Text.Split(".")(0)
            editorial = ComboBox2.Text.Split(".")(0)
            cantidad = Val(TextBox3.Text)
            If titulo IsNot "" And autor IsNot "" And genero IsNot "" And editorial IsNot "" Then
                If cantidad > 0 Then
                    Dim valor As Boolean
                    valor = BD.registrarLibro(titulo, autor, genero, editorial, cantidad)
                    If valor = True Then
                        ObtenerDatos()
                        LimpiarCampos()
                        MsgBox("Se registro el libro exitosamente")
                        BD.GuardarBitacora("Se creo un nuevo libro.")
                    End If
                Else
                    MsgBox("La cantidad debe de ser mayor a 0")
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
        ComboBox1.Text = ""
        ComboBox2.Text = ""
    End Sub

    Sub ObtenerDatos()
        Dim datos = BD.verLibros()
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
            TextBox3.Text = DataGridView1.Item(5, i).Value
            ComboBox1.Text = ""
            ComboBox2.Text = ""
            seleccionado = id
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If seleccionado > 0 Then
            Dim titulo, autor, genero, editorial As String
            Dim cantidad As Integer
            titulo = TextBox1.Text
            autor = TextBox2.Text
            genero = ComboBox1.Text.Split(".")(0)
            editorial = ComboBox2.Text.Split(".")(0)
            cantidad = Val(TextBox3.Text)
            If titulo IsNot "" And autor IsNot "" And genero IsNot "" And editorial IsNot "" Then
                If cantidad > 0 Then
                    Dim valor As Boolean
                    valor = BD.modificarLibro(seleccionado, titulo, autor, genero, editorial, cantidad)
                    If valor = True Then
                        ObtenerDatos()
                        LimpiarCampos()
                        MsgBox("Se modifico el libro exitosamente")
                        BD.GuardarBitacora("Se modifico un libro")
                        seleccionado = 0
                    End If
                Else
                    MsgBox("La cantidad de libros debe ser mayor a 0.")
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
            If MsgBox("¿Desea eliminar el libro seleccionado?", MsgBoxStyle.Information + vbYesNo) = vbYes Then
                valor = BD.eliminarLibro(seleccionado)
                If valor = True Then
                    ObtenerDatos()
                    LimpiarCampos()
                    MsgBox("Se elimino el libro exitosamente")
                    BD.GuardarBitacora("Se elimino un libro")
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
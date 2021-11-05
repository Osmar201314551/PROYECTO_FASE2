Imports System.Data.SqlClient

Module BD
    Public Server = "LAPTOP-MQTS1TI4\SQLEXPRESS"
    Public Database = "BD_201314551"
    Public Cadena As String = "Data Source='" & Server & "'; Initial Catalog='" & Database & "'; Integrated Security=True;"
    Public Conexion As New SqlConnection
    Public estadoBD As Boolean

    Public logUser As String
    Public tipoUser, imagenUser, idUser As Integer

    Sub ConectarBD()
        If estadoBD = False Then
            Try
                Conexion.ConnectionString = Cadena
                Conexion.Open()
                estadoBD = True
                MsgBox("Base de datos conectada exitosamente")
            Catch ex As Exception
                MsgBox("No se pudo conectar a la base de datos" & vbCrLf & ex.ToString())
            End Try
        Else
            MsgBox("La base de datos ya fue conectada previamente")
        End If
    End Sub

    Sub GuardarBitacora(accion As String)
        If estadoBD = True Then
            Try
                Dim strHostName As String = Net.Dns.GetHostName()
                Dim strIPAddress As String = Net.Dns.GetHostByName(strHostName).AddressList(0).ToString()
                Dim sql = "INSERT INTO Bitacora (usuario, direccion_ip, accion) VALUES ('" & logUser & "', '" & strIPAddress & "', '" & accion & "');"
                Dim Comando As New SqlCommand(sql, Conexion)
                Comando.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
        End If
    End Sub

    Function IniciarSesion(username As String, password As String) As Boolean
        If estadoBD = True Then
            Try
                Dim sql = "SELECT * FROM Usuario WHERE usuario='" & username & "' AND DECRYPTBYPASSPHRASE('password', contrasenia)='" & password & "';"
                Dim ds As New DataSet
                Dim Comando As New SqlDataAdapter(sql, Conexion)
                Comando.Fill(ds, "Datos")
                Dim tamaño = ds.Tables("Datos").Rows.Count
                If tamaño > 0 Then
                    idUser = ds.Tables("Datos").Rows(0).Item(0)
                    logUser = ds.Tables("Datos").Rows(0).Item(3)
                    tipoUser = ds.Tables("Datos").Rows(0).Item(4)
                    imagenUser = ds.Tables("Datos").Rows(0).Item(7)
                    Return True
                Else
                    MsgBox("No se encontro un usuario con las credenciales indicadas.")
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
            Return False
        End If
    End Function


    Friend Function Autenticar(v As Integer) As Boolean
        If v = imagenUser Then
            Return True
        Else
            Return False
        End If
    End Function

    Function registrar(nombre As String, apellido As String, user As String, pass As String, fecha As String, imagen As Object) As Boolean
        If estadoBD = True Then
            Try
                Dim sql = "INSERT INTO Usuario (nombre, apellido, usuario, tipo_usr, nacimiento, contrasenia, imagen) " & "VALUES ('" & nombre & "', '" & apellido & "', '" & user & " ', 2, '" & fecha & "', ENCRYPTBYPASSPHRASE('password', '" & pass & "'), " & imagen & ");"
                Dim Comando As New SqlCommand(sql, Conexion)
                Comando.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
            Return False
        End If
    End Function

    Function verEditoriales() As DataSet
        If estadoBD = True Then
            Try
                Dim sql = "SELECT id_editorial as ID, nombre as Nombre, direccion as Direccion, telefono as Telefono FROM Editorial;"
                Dim Comando As New SqlDataAdapter(sql, Conexion)
                Dim ds As New DataSet
                Comando.Fill(ds, "Datos")
                Return ds
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
        End If
    End Function


    Function registrarEditorial(nombre As String, direccion As String, telefono As String) As Boolean
        If estadoBD = True Then
            Try
                Dim sql = "INSERT INTO Editorial (nombre, direccion, telefono) " & "VALUES ('" & nombre & "', '" & direccion & "', '" & telefono & "');"
                Dim Comando As New SqlCommand(sql, Conexion)
                Comando.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
            Return False
        End If
    End Function

    Function modificarEditorial(id As String, nombre As String, direccion As String, telefono As String) As Boolean
        If estadoBD = True Then
            Try
                Dim sql = "UPDATE Editorial SET nombre = '" & nombre & "', direccion = '" & direccion & "', telefono = '" & telefono & "' WHERE id_editorial = " & id & ";"
                Dim Comando As New SqlCommand(sql, Conexion)
                Comando.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
            Return False
        End If
    End Function

    Function eliminarEditorial(id As String) As Boolean
        If estadoBD = True Then
            Try
                Dim sql = "DELETE Editorial WHERE id_editorial = " & id & ";"
                Dim Comando As New SqlCommand(sql, Conexion)
                Comando.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
            Return False
        End If
    End Function

    Function verGeneros() As DataSet
        If estadoBD = True Then
            Try
                Dim sql = "SELECT id_genero as ID, nombre as Nombre from Genero;"
                Dim Comando As New SqlDataAdapter(sql, Conexion)
                Dim ds As New DataSet
                Comando.Fill(ds, "Datos")
                Return ds
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
        End If
    End Function

    Function registrarGenero(nombre As String) As Boolean
        If estadoBD = True Then
            Try
                Dim sql = "INSERT INTO Genero(nombre) VALUES ('" & nombre & "');"
                Dim Comando As New SqlCommand(sql, Conexion)
                Comando.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
            Return False
        End If
    End Function

    Function modificarGenero(id As String, nombre As String) As Boolean
        If estadoBD = True Then
            Try
                Dim sql = "UPDATE Genero SET nombre = '" & nombre & "' WHERE id_genero = " & id & ";"
                Dim Comando As New SqlCommand(sql, Conexion)
                Comando.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
            Return False
        End If
    End Function

    Function eliminarGenero(id As String) As Boolean
        If estadoBD = True Then
            Try
                Dim sql = "DELETE Genero WHERE id_genero = " & id & ";"
                Dim Comando As New SqlCommand(sql, Conexion)
                Comando.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
            Return False
        End If
    End Function

    Function verLibros() As DataSet
        If estadoBD = True Then
            Try
                Dim sql = "SELECT id_libro as ID, titulo as Titulo, autor as Autor, E.nombre as Editorial, G.nombre as Genero, cantidad as Cantidad FROM Libro L INNER JOIN Editorial E ON E.id_editorial = L.id_editorial INNER JOIN Genero G ON G.id_genero = L.genero;"
                Dim Comando As New SqlDataAdapter(sql, Conexion)
                Dim ds As New DataSet
                Comando.Fill(ds, "Datos")
                Return ds
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
        End If
    End Function

    Function registrarLibro(nombre As String, autor As String, genero As String, editorial As String, cantidad As Integer) As Boolean
        If estadoBD = True Then
            Try
                Dim sql = "INSERT INTO Libro(id_editorial, genero, titulo, autor, cantidad) VALUES (" & editorial & "," & genero & ", '" & nombre & "', '" & autor & "', " & cantidad & ");"
                Dim Comando As New SqlCommand(sql, Conexion)
                Comando.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
            Return False
        End If
    End Function

    Friend Function modificarLibro(seleccionado As Integer, titulo As String, autor As String, genero As String, editorial As String, cantidad As Integer) As Boolean
        If estadoBD = True Then
            Try
                Dim sql = "UPDATE Libro SET id_editorial = " & editorial & ", genero = " & genero & ", titulo = '" & titulo & "', autor = '" & autor & "', cantidad = " & cantidad & " WHERE id_libro = " & seleccionado & " ;"
                Dim Comando As New SqlCommand(sql, Conexion)
                Comando.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
            Return False
        End If
    End Function

    Function eliminarLibro(id As String) As Boolean
        If estadoBD = True Then
            Try
                Dim sql = "DELETE Libro WHERE id_libro = " & id & ";"
                Dim Comando As New SqlCommand(sql, Conexion)
                Comando.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
            Return False
        End If
    End Function

    Function obtenerUsuario(id As String) As DataSet
        If estadoBD = True Then
            Try
                Dim sql = "SELECT id_usuario, nombre, apellido, usuario, nacimiento, CAST(DECRYPTBYPASSPHRASE('password', contrasenia) AS VARCHAR(8000)) FROM Usuario WHERE id_usuario = " & id & ";"
                Dim Comando As New SqlDataAdapter(sql, Conexion)
                Dim ds As New DataSet
                Comando.Fill(ds, "Datos")
                Return ds
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
        End If
    End Function

    Function editarPerfil(isUser As String, nombre As String, apellido As String, user As String, pass As String, fecha As String) As Boolean
        If estadoBD = True Then
            Try
                Dim sql = "UPDATE Usuario SET nombre = '" & nombre & "', apellido = '" & apellido & "', usuario = '" & user & "', nacimiento = '" & fecha & "', contrasenia = ENCRYPTBYPASSPHRASE('password', '" & pass & "') WHERE id_usuario = " & isUser & ";"
                Dim Comando As New SqlCommand(sql, Conexion)
                Comando.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
            Return False
        End If

    End Function

    Function verLibrosParaPrestar() As DataSet
        If estadoBD = True Then
            Try
                Dim sql = "SELECT id_libro as ID, titulo as Titulo, autor as Autor, E.nombre as Editorial, G.nombre as Genero, cantidad as Cantidad FROM Libro L INNER JOIN Editorial E ON E.id_editorial = L.id_editorial INNER JOIN Genero G ON G.id_genero = L.genero WHERE cantidad > 0;"
                Dim Comando As New SqlDataAdapter(sql, Conexion)
                Dim ds As New DataSet
                Comando.Fill(ds, "Datos")
                Return ds
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
        End If
    End Function

    Function verLibrosParaPrestarNombre(nombre As String) As DataSet
        If estadoBD = True Then
            Try
                Dim sql = "SELECT id_libro as ID, titulo as Titulo, autor as Autor, E.nombre as Editorial, G.nombre as Genero, cantidad as Cantidad FROM Libro L INNER JOIN Editorial E ON E.id_editorial = L.id_editorial INNER JOIN Genero G ON G.id_genero = L.genero WHERE cantidad > 0 and titulo = '" & nombre & "';"
                MsgBox(sql)
                Dim Comando As New SqlDataAdapter(sql, Conexion)
                Dim ds As New DataSet
                Comando.Fill(ds, "Datos")
                Return ds
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
        End If
    End Function

    Function verLibrosParaPrestarGenero(id As String) As DataSet
        If estadoBD = True Then
            Try
                Dim sql = "SELECT id_libro as ID, titulo as Titulo, autor as Autor, E.nombre as Editorial, G.nombre as Genero, cantidad as Cantidad FROM Libro L INNER JOIN Editorial E ON E.id_editorial = L.id_editorial INNER JOIN Genero G ON G.id_genero = L.genero WHERE cantidad > 0 and L.genero = " & id & ";"
                Dim Comando As New SqlDataAdapter(sql, Conexion)
                Dim ds As New DataSet
                Comando.Fill(ds, "Datos")
                Return ds
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
        End If
    End Function

    Friend Function prestarLibro(id As Integer) As Boolean
        If estadoBD = True Then
            Try
                Dim sql = "INSERT INTO Prestamo (id_usuario, id_libro, estatus) VALUES (" & idUser & ", " & id & ", 1)"
                Dim Comando As New SqlCommand(sql, Conexion)
                Comando.ExecuteNonQuery()
                sql = "UPDATE Libro SET cantidad = cantidad - 1 WHERE id_libro = " & id & ";"
                Dim Comando2 As New SqlCommand(sql, Conexion)
                Comando2.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
            Return False
        End If
    End Function

    Function verLibrosPrestados() As DataSet
        If estadoBD = True Then
            Try
                Dim sql = "SELECT P.id_prestamo as ID, titulo as Titulo, autor as Autor, E.nombre as Editorial, G.nombre as Genero FROM Prestamo P INNER JOIN Libro L ON P.id_libro = L.id_libro INNER JOIN Editorial E ON E.id_editorial = L.id_editorial INNER JOIN Genero G ON G.id_genero = L.genero WHERE id_usuario = " & idUser & " AND estatus = 1;"
                Dim Comando As New SqlDataAdapter(sql, Conexion)
                Dim ds As New DataSet
                Comando.Fill(ds, "Datos")
                Return ds
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
        End If
    End Function

    Friend Function devolverLibro(id As Integer) As Boolean
        If estadoBD = True Then
            Try
                Dim sql = "UPDATE Prestamo SET estatus = 0 WHERE id_prestamo = " & id & ";"
                Dim Comando As New SqlCommand(sql, Conexion)
                Comando.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
            Return False
        End If
    End Function

    Friend Function reporte1() As DataSet
        If estadoBD = True Then
            Try
                Dim sql = "SELECT E.nombre as Editorial, E.direccion  as Direccion, SUM(cantidad) as Cantidad FROM Libro L INNER JOIN Editorial E ON E.id_editorial = L.id_editorial GROUP BY E.nombre, direccion;"
                Dim Comando As New SqlDataAdapter(sql, Conexion)
                Dim ds As New DataSet
                Comando.Fill(ds, "Datos")
                Return ds
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
        End If
    End Function
    Friend Function reporte2() As DataSet
        If estadoBD = True Then
            Try
                Dim sql = "SELECT T1.C as Disponibles, T2.E as Prestados FROM (SELECT SUM(cantidad) as C FROM Libro) T1, (SELECT COUNT(estatus) as E FROM Prestamo WHERE estatus != 0) T2;"
                Dim Comando As New SqlDataAdapter(sql, Conexion)
                Dim ds As New DataSet
                Comando.Fill(ds, "Datos")
                Return ds
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
        End If

    End Function
    Friend Function reporte3() As DataSet
        If estadoBD = True Then
            Try
                Dim sql = "SELECT L.titulo as Titulo, L.autor as Autor, COUNT(*) as Cantidad FROM Prestamo P INNER JOIN Libro L ON P.id_libro = L.id_libro GROUP BY L.titulo, L.autor;"
                Dim Comando As New SqlDataAdapter(sql, Conexion)
                Dim ds As New DataSet
                Comando.Fill(ds, "Datos")
                Return ds
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
        End If

    End Function
    Friend Function reporte4() As DataSet
        If estadoBD = True Then
            Try
                Dim sql = "SELECT nombre as Nombre, apellido as Apellido, nacimiento as 'Fecha de Nacimiento', COUNT(*) as Cantidad FROM Prestamo P INNER JOIN Usuario U ON P.id_usuario= U.id_usuario GROUP BY nombre, apellido, nacimiento;"
                Dim Comando As New SqlDataAdapter(sql, Conexion)
                Dim ds As New DataSet
                Comando.Fill(ds, "Datos")
                Return ds
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
        End If

    End Function
    Friend Function reporte5() As DataSet
        If estadoBD = True Then
            Try
                Dim sql = "SELECT TOP 3 L.titulo as Titulo, L.autor as Autor, COUNT(*) as Cantidad FROM Prestamo P INNER JOIN Libro L ON P.id_libro = L.id_libro GROUP BY L.titulo, L.autor ORDER BY Cantidad DESC;"
                Dim Comando As New SqlDataAdapter(sql, Conexion)
                Dim ds As New DataSet
                Comando.Fill(ds, "Datos")
                Return ds
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
        End If

    End Function
    Friend Function reporte6() As DataSet
        If estadoBD = True Then
            Try
                Dim sql = "SELECT TOP 5 nombre as Nombre, apellido as Apellido, nacimiento as 'Fecha de Nacimiento', COUNT(*) as Cantidad FROM Prestamo P INNER JOIN Usuario U ON P.id_usuario= U.id_usuario GROUP BY nombre, apellido, nacimiento ORDER BY Cantidad DESC;"
                Dim Comando As New SqlDataAdapter(sql, Conexion)
                Dim ds As New DataSet
                Comando.Fill(ds, "Datos")
                Return ds
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
        End If

    End Function

    Function borrarCuenta() As Boolean
        If estadoBD = True Then
            Try
                Dim sql = "DELETE Usuario WHERE id_usuario = " & idUser & ";"
                Dim Comando As New SqlCommand(sql, Conexion)
                Comando.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        Else
            MsgBox("No esta conectado a la base de datos.")
            Return False
        End If
    End Function

End Module

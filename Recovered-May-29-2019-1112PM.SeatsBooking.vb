Imports System.Data.OleDb

Public Class SeatsBooking

    'define 3 profile icons

    Dim grayprofile As New System.Drawing.Bitmap(My.Resources.profilegray1)
    Dim greenprofile As New System.Drawing.Bitmap(My.Resources.profilegreen1)
    Dim redprofile As New System.Drawing.Bitmap(My.Resources.profilered1)






    Private Sub SeatsBooking_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        'place the gray icons into the pictureboxes

        Dim c As Control
        For Each c In Me.Controls

            If TypeOf (c) Is PictureBox AndAlso c IsNot PictureBox98 Then
                CType(c, PictureBox).Image = grayprofile
                'add the click event of the picturebox1 to all the otherones
                AddHandler c.Click, AddressOf PictureBox1_MouseClick
            End If
        Next

        'connect to the database
        Dim stSQL As String
        stSQL = "SELECT BookingID,Name,Lastname,Seat from tblBookings"

        Dim stConString As String
        stConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\I.C.T\source\repos\MovieTicketBookingSolution\Database3.accdb"

        Dim conBookings As System.Data.OleDb.OleDbConnection

        conBookings = New System.Data.OleDb.OleDbConnection
        conBookings.ConnectionString = stConString
        conBookings.Open()

        Dim cmdSelectBookings As OleDbCommand
        cmdSelectBookings = New OleDbCommand
        cmdSelectBookings.CommandText = stSQL
        cmdSelectBookings.Connection = conBookings

        'datase and data adaptor
        Dim dsBookings As New DataSet
        Dim daBookings As New OleDbDataAdapter(cmdSelectBookings)
        daBookings.Fill(dsBookings, "tblBookings")
        conBookings.Close()

        Dim stOut As String
        Dim t1 As DataTable = dsBookings.Tables("tblBookings")
        Dim row As DataRow

        For Each row In t1.Rows
            stOut = stOut & row(0) & " " & row(1) & " " & row(2) & " " & row(3) & vbNewLine
            CType(Controls("PictureBox" & row(3)), PictureBox).Image = redprofile
        Next
        'MsgBox(stOut)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        mtbform.Show()
    End Sub





    Private Sub PictureBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseClick

        If CType(sender, PictureBox).Image Is grayprofile Then
            CType(sender, PictureBox).Image = greenprofile
        ElseIf CType(sender, PictureBox).Image Is greenprofile Then
            CType(sender, PictureBox).Image = grayprofile
        End If

    End Sub




    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim c As Control
        Dim bselected As Boolean

        For Each c In Me.Controls

            If TypeOf (c) Is PictureBox AndAlso c IsNot PictureBox98 Then
                If CType(c, PictureBox).Image Is greenprofile Then
                    bselected = True
                    Exit For
                End If

            End If
        Next
        If bselected = False Then
            MsgBox("Please you have to select at least one seat")
            Exit Sub
        End If
        If nametext.Text = String.Empty Then
            MsgBox("Please enter your name")
        ElseIf surname.Text = String.Empty Then
            MsgBox("Please enter your last name")

        End If

        Dim stConString As String
        stConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\I.C.T\source\repos\MovieTicketBookingSolution\Database3.accdb"

        Dim stSQLInsert As String


        Dim conBookings As System.Data.OleDb.OleDbConnection

        conBookings = New System.Data.OleDb.OleDbConnection
        conBookings.ConnectionString = stConString
        conBookings.Open()

        Dim cmdMakeBookings As OleDbCommand
        cmdMakeBookings = New OleDbCommand
        'cmdMakeBookings.CommandText = stSQLInsert
        cmdMakeBookings.Connection = conBookings



        Dim iSeatNUmber As Integer
        For Each c In Me.Controls

            If TypeOf (c) Is PictureBox AndAlso c IsNot PictureBox98 Then
                If CType(c, PictureBox).Image Is greenprofile Then
                    iSeatNUmber = Mid(CType(c, PictureBox).Name, 11)

                    stSQLInsert = "INSERT INTO tblBookings (Name,Lastname,Seat) VALUES('" & nametext.Text & "','" & surname.Text & "'," & iSeatNUmber & ")"
                    cmdMakeBookings.CommandText = stSQLInsert
                    cmdMakeBookings.ExecuteNonQuery()
                End If

            End If
        Next

        Me.Hide()
        success.Show()
    End Sub

    Private Sub lblprice_Click(sender As Object, e As EventArgs) Handles lblprice.Click

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub
End Class
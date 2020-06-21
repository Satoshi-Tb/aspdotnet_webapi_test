Public Class WebForm1
    Inherits System.Web.UI.Page


    Private ViewData As DataTable = New DataTable()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        InitData()
    End Sub

    Private Sub InitData()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' プログレスバー表示のため、わざと遅延
        System.Threading.Thread.Sleep(5000)  ' 5秒停止
        Label1.Text = Date.Now.ToString()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Label2.Text = Date.Now.ToString()
    End Sub
End Class
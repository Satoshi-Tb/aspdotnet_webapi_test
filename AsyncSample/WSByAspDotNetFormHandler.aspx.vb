Imports System.Web
Imports AsyncSample.Dtos

Public Class WSByAspDotNetFormHandler
    Inherits System.Web.UI.Page

    Private Shared Departments As List(Of Department) = Department.CreateData()

    ''' <summary>
    ''' ASP.NetWebフォームハンドラによるWebサービス実現
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' 操作チェック
        Dim action As String = Me.Request.Form("")

        Try
            ' 一覧取得
            Dim jss = New Script.Serialization.JavaScriptSerializer()
            Response.ContentType = "text/javascript"
            Response.Output.Write(jss.Serialize(Departments))
            Response.StatusCode = Net.HttpStatusCode.OK

        Catch ex As Exception
            Response.StatusCode = Net.HttpStatusCode.BadGateway
        Finally
            Response.End()
        End Try
    End Sub

End Class
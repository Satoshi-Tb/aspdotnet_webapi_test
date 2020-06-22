Imports System.Web
Imports AsyncSample.Dtos

Public Class WSByAspDotNetFormHandler
    Inherits System.Web.UI.Page

    Private Shared Departments As List(Of Department) = Department.CreateData()

    ''' <summary>
    ''' ASP.NetWebフォームハンドラによるWebサービス実現。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' MEMO:
        ' ・HTMLを返さずに、要求に応じた処理を行う。
        ' ・view(.aspx)は使用しない。
        ' ・照会結果は、JSONで返す。→標準APIで、オブジェクトをJSON化することはできるみたい。
        ' ・要求ごとの処理（照会/登録/更新/削除）は、ひとまずactionパラメータで対応。ベストプラクティス模索中。
        ' ・メソッドは、すべての操作でpostを前提。
        ' ・パラメータバインドはひとまず使わずに、手動バインド。viewにサーバーコントロール配置すればバインドできると
        ' 　思うが、さすがにそのやり方は良いコーディングではないと考える。


        ' パラメータによる操作チェック。
        ' これはさすがに今一つに見える
        Dim action As String = Me.Request.Form("action")

        Try
            If String.IsNullOrEmpty(action) Then
                Response.StatusCode = Net.HttpStatusCode.BadRequest
            Else
                Select Case action
                    Case "create"
                        ' 手動でパラメータバインド
                        Dim dpt As New Department() With {.Name = Request.Form("name"), .Comment = Request.Form("comment")}

                        Dim maxId = Departments.AsEnumerable().Select(Of Integer)(Function(d) d.DeptId).Max()

                        dpt.DeptId = maxId + 1

                        Departments.Add(dpt)
                        Response.StatusCode = Net.HttpStatusCode.OK

                    Case "show"
                        ' 一覧検索と、ID指定検索を同じ処理にすると見通しが悪い。（とりあえずのサンプル）
                        Dim jss = New Script.Serialization.JavaScriptSerializer() 'Sharedで用意してもよいか
                        Dim result As Object

                        ' 検索条件取得
                        Dim dptId As String = Me.Request.Form("deptId")


                        If String.IsNullOrEmpty(dptId) Then
                            '一覧検索
                            result = Departments
                        Else
                            ' ID指定
                            result = Departments.AsEnumerable().Where(Function(d) d.DeptId = dptId).SingleOrDefault()
                        End If

                        If result Is Nothing Then
                            Response.StatusCode = Net.HttpStatusCode.NotFound
                        Else
                            Response.ContentType = "text/javascript"
                            Response.Output.Write(jss.Serialize(result))  '  オブジェクトをJSON形式に変換
                            Response.StatusCode = Net.HttpStatusCode.OK
                        End If


                    Case Else
                        Response.StatusCode = Net.HttpStatusCode.BadRequest
                End Select
            End If

        Catch ex As Exception
            Response.StatusCode = Net.HttpStatusCode.BadGateway
        Finally
            Response.End()
        End Try
    End Sub

End Class
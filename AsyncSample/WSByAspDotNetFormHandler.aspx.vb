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
        Dim action As String = Me.Request.Form("action")

        Try
            If String.IsNullOrEmpty(action) Then
                Response.StatusCode = Net.HttpStatusCode.BadRequest
            Else
                Select Case action
                    Case "btnCreate2"
                        Create()

                    Case "btnList2"
                        Find()

                    Case "btnSearch2"
                        FindBy()

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


    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub Find()
        Dim jss = New Script.Serialization.JavaScriptSerializer()
        Response.ContentType = "text/javascript"
        Response.Output.Write(jss.Serialize(Departments))  '  オブジェクトをJSON形式に変換
        Response.StatusCode = Net.HttpStatusCode.OK
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub FindBy()

        ' 検索条件取得
        Dim dptId As String = Me.Request.Form("deptId")


        If String.IsNullOrEmpty(dptId) Then
            ' 通常ありえない
            Response.StatusCode = Net.HttpStatusCode.BadRequest
        Else
            ' ID指定
            Dim result = Departments.AsEnumerable().Where(Function(d) d.DeptId = dptId).SingleOrDefault()
            If result Is Nothing Then
                Response.StatusCode = Net.HttpStatusCode.NotFound
            Else
                Dim jss = New Script.Serialization.JavaScriptSerializer()
                Response.ContentType = "text/javascript"
                Response.Output.Write(jss.Serialize(result))  '  オブジェクトをJSON形式に変換
                Response.StatusCode = Net.HttpStatusCode.OK
            End If
        End If


    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub Create()
        ' 手動でパラメータバインド
        Dim dpt As New Department() With {.Name = Request.Form("name"), .Comment = Request.Form("comment")}

        Dim maxId = Departments.AsEnumerable().Select(Of Integer)(Function(d) d.DeptId).Max()

        dpt.DeptId = maxId + 1

        Departments.Add(dpt)
        Response.StatusCode = Net.HttpStatusCode.OK
    End Sub

End Class
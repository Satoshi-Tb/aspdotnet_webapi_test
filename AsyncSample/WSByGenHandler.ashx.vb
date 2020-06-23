Imports System.Web
Imports System.Web.Services
Imports System.Net.Http
Imports AsyncSample.Dtos



Public Class WSByGenHandler
    Implements System.Web.IHttpHandler

    Private Shared Departments As List(Of Department) = Department.CreateData()

    ''' <summary>
    ''' ジェネリックハンドラによるWebサービス実装
    ''' </summary>
    ''' <param name="context"></param>
    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Dim req = context.Request
        Dim res = context.Response

        ' POST以外の場合、終了
        If Not req.HttpMethod.Equals(HttpMethod.Post.Method) Then
            res.StatusCode = Net.HttpStatusCode.BadRequest
            Return
        End If

        ' パラメータによる操作チェック。
        ' ボタン押下以外からのリクエストを考えると、IDタグよりも
        ' 処理名称（create, searchなど）の方がベターと思える。
        Dim action As String = req.Form("action")

        Try
            If String.IsNullOrEmpty(action) Then
                res.StatusCode = Net.HttpStatusCode.BadRequest
            Else
                Select Case action
                    Case "btnCreate3"
                        Create(context)

                    Case "btnList3"
                        Find(context)

                    Case "btnSearch3"
                        FindBy(context)

                    Case Else
                        res.StatusCode = Net.HttpStatusCode.BadRequest
                End Select
            End If

        Catch ex As Exception
            res.StatusCode = Net.HttpStatusCode.InternalServerError
        End Try

    End Sub

    ''' <summary>
    ''' 再利用可能かどうか。マルチスレッド利用可能かどうか、と考えてよい。
    ''' マルチスレッド利用可能にする場合、スレッドセーフな設計（インスタンス変数の取り扱いなど）が必要。
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub Find(ByVal context As HttpContext)
        Dim res = context.Response
        Dim jss = New Script.Serialization.JavaScriptSerializer()
        res.ContentType = "text/javascript"
        res.Output.Write(jss.Serialize(Departments))  '  オブジェクトをJSON形式に変換
        res.StatusCode = Net.HttpStatusCode.OK
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub FindBy(ByVal context As HttpContext)
        Dim req = context.Request
        Dim res = context.Response

        ' 検索条件取得
        Dim dptId As String = req.Form("deptId")


        If String.IsNullOrEmpty(dptId) Then
            ' 通常ありえない
            res.StatusCode = Net.HttpStatusCode.BadRequest
        Else
            ' ID指定
            Dim result = Departments.AsEnumerable().Where(Function(d) d.DeptId = dptId).SingleOrDefault()
            If result Is Nothing Then
                res.StatusCode = Net.HttpStatusCode.NotFound
            Else
                Dim jss = New Script.Serialization.JavaScriptSerializer()
                res.ContentType = "text/javascript"
                res.Output.Write(jss.Serialize(result))  '  オブジェクトをJSON形式に変換
                res.StatusCode = Net.HttpStatusCode.OK
            End If
        End If


    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub Create(ByVal context As HttpContext)
        Dim req = context.Request
        Dim res = context.Response
        ' 手動でパラメータバインド
        Dim dpt As New Department() With {.Name = req.Form("name"), .Comment = req.Form("comment")}

        Dim maxId = Departments.AsEnumerable().Select(Of Integer)(Function(d) d.DeptId).Max()

        dpt.DeptId = maxId + 1

        Departments.Add(dpt)
        res.StatusCode = Net.HttpStatusCode.OK
    End Sub
End Class
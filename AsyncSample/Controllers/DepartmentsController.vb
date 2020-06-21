Imports System.Net
Imports System.Web.Http
Imports System.Web.Http.Description
Imports AsyncSample.Models

Namespace Controllers
    ' ■Controllerクラスの使用について
    ' ・使う必要あるか。また、WebフォームにMVCを取り入れると、ややこしくならないだろうか。
    '   →プロジェクト設定にて、WebのCoreAPIの参照追加をチェックすると、同一プロジェクトで
    '     Webフォーム/MVC/WebAPIが利用可能なるとのこと。
    '     言い換えれば、特定のライブラリ参照が無いと共存できないと思われる。


    ' ■API (URL) の設計について
    ' 1: REST
    ' メリット：
    ' ・一般的な方法。（ちょっと自信ない）
    ' ・機能をシンプルに表現できる
    ' デメリット：
    ' ・情報取得をGETで行う。条件内容の秘匿が必要な場合、よろしくない。例えば名称や番号など。
    '   （回避するやり方があるかもしれない）

    Public Class DepartmentsController
        Inherits ApiController

        Private Shared Departments As New List(Of Department)(New Department() {
            New Department() With {.DeptId = 1, .Name = "TSI1"},
            New Department() With {.DeptId = 2, .Name = "TSI2"},
            New Department() With {.DeptId = 3, .Name = "OSI1"},
            New Department() With {.DeptId = 4, .Name = "OSI2"}
        })

        ''' <summary>
        ''' GET: api/Departments
        ''' </summary>
        ''' <returns></returns>
        Function GetDepartments() As IQueryable(Of Department)
            Return Departments.AsQueryable()
        End Function

        ''' <summary>
        ''' GET: api/Departments/5
        ''' </summary>
        ''' <param name="id"></param>
        ''' <returns></returns>
        <ResponseType(GetType(Department))>
        Function GetDepartment(ByVal id As Integer) As IHttpActionResult

            Dim dpt = Departments.AsEnumerable().Where(Function(d) d.DeptId = id).SingleOrDefault()

            If (dpt Is Nothing) Then
                Return NotFound()
            End If


            Return Ok(dpt)
        End Function

        ''' <summary>
        ''' POST: api/Departments
        ''' </summary>
        ''' <param name="department"></param>
        ''' <returns></returns>
        <ResponseType(GetType(Department))>
        Function PostDepartment(ByVal department As Department) As IHttpActionResult

            Dim maxId = Departments.AsEnumerable().Select(Of Integer)(Function(d) d.DeptId).Max()

            department.DeptId = maxId + 1

            ' session情報取得。
            ' department.Comment = HttpContext.Current.Session("SESSION_TEST")
            ' こうやってもダメ。普通にやると、ApiControllerは、HttpContext.Current.SessionはNothingになるみたい。
            ' 理由:
            ' Web API利用時にRouteに登録するHttpControllerRouteHandlerのGetHttpHandlerでは、IRequiresSessionStateインターフェース指定のないHttpControllerHandlerが使われるから
            ' なので、Web APIではセッションを利用しないことが前提（標準）→REST Fullな設計になっているからと思われる。
            ' 無理やりSession使えるようにする方法もあるみたいが、標準的な手法のHackが必要。あまり勧められないと考える。
            ' WebAPIで使うRoteHandlerを、セッション利用可能なクラス（＝IRequiresSessionStateを指定したカスタムクラス）に置き換える

            ' 本来は排他必要。あくまでテスト
            Departments.Add(department)

            Return Ok(department)
        End Function
    End Class
End Namespace
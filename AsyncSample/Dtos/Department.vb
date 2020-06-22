Namespace Dtos
    Public Class Department
        Public Property DeptId As Integer
        Public Property Name As String
        Public Property Comment As String


        Public Shared Function CreateData() As List(Of Department)
            Return New List(Of Department)(New Department() {
                New Department() With {.DeptId = 1, .Name = "東京営業部", .Comment = "東営"},
                New Department() With {.DeptId = 2, .Name = "東京開発部1", .Comment = "東開1"},
                New Department() With {.DeptId = 3, .Name = "東京開発部2", .Comment = "東開2"},
                New Department() With {.DeptId = 4, .Name = "大阪営業部", .Comment = "大営"},
                New Department() With {.DeptId = 5, .Name = "大阪開発部1", .Comment = "大開1"},
                New Department() With {.DeptId = 6, .Name = "大阪開発部2", .Comment = "大開2"}
            })
        End Function

    End Class
End Namespace

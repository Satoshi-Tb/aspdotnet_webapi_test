Public Class WebForm2
    Inherits System.Web.UI.Page
    Private Shared ReadOnly SESSION_KEY_TEST = "SESSION_TEST"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' session 利用テスト
        Session(SESSION_KEY_TEST) = $"hello from {Me.GetType.ToString}"
    End Sub

End Class
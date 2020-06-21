Imports System.Web.Http

Public Class Global_asax
    Inherits HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' アプリケーションの起動時に呼び出されます
        GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
    End Sub
End Class
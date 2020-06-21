<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UpdatePanel.aspx.vb" Inherits="AsyncSample.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>AsyncSample - UpdatePanel</title>
</head>
<body>
    <h2>非同期通信テスト - UpdatePanel</h2>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdPanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <p>部分更新エリア１</p>
                <p>
                    ・自動でAJAX通信行ってくれる。<br />
                    ・.Netの知識だけで、比較的手軽に実現できる。<br />
                    ・とはいえ、手編集してもらうには敷居が高い。文法しっていないと、コンパイルエラーが発生する。<br />
                    ・結論としては、採用不可。<br />
                </p>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                <asp:Button ID="Button1" runat="server" Text="更新（プログレスバー付）" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- 
            AssociatedUpdatePanelのレスポンスが、DisplayAfter(ms)経過されたら表示
            
            -->
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID ="UpdPanel1" DisplayAfter="1000">
            <ProgressTemplate>
                <span style="color: red; font-weight:900;">現在更新中...</span>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdPanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <p>部分更新エリア２</p>
                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                <asp:Button ID="Button2" runat="server" Text="更新" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>

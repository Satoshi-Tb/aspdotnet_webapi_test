<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AsyncWithjQuery.aspx.vb" Inherits="AsyncSample.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>AsyncSample - jQuery</title>
    <!-- 最新版でテストする。1系、2系はサポート対象外のため選択肢に入れない -->
    <script src="https://code.jquery.com/jquery-3.5.1.js"></script>
</head>
<body>
    <h2>非同期通信テスト - UpdatePanel</h2>
    <p>
        NOTE:<br />
        ・jQuery は、IE9以上に対応<br />
        ・HTMLタグのIDに注意。jQuer含む、javascriptでは、HTMLタグのIDを指定して処理を記述する機会が多い。<br />
        ・サーバーコントロールの場合、タグのID属性はサーバーIDと呼ばれる値であり、<br />
          &nbsp;&nbsp;これとは別にクライアントID（ClientID）が存在する。<br />
        ・いわゆる、HTMLタグのIDに当たるものが、クライアントIDに該当する。<br />
        ・基本的に、サーバーID＝クライアントIDと考えてよいが、異なる値になる場合がある。<br />
        ・例えば、マスターページや、ユーザーコントロールなど、コントロールがネスト関係になる場合などである。<br />
        ・ID命名の方法を制御するために、サーバーコントロールには、ClientIDModeプロパティが存在する。<br />
        ・Repeaterコントロールを使用した場合、ItemTemplateのループ内で繰り返し生成されるサーバーコントロールも該当。<br />
        ・例えば、ClientIDModeプロパティにStaticを指定すれば、必ずクライアントID＝サーバーIDとなる。<br />
        ・なお、ClientIDを直接参照すれば、ClientIDModeの設定によらず、常に望む値が参照可能<br />
          &nbsp;&nbsp;ただし参考書籍によるとあまり見た目が良くないので、お勧めしないとのこと<br />
        ・WebAPIモジュールを使うと、比較的容易に構築できそう<br />

    </p>
    <form id="form1" runat="server">

        <input id="listButton" type="button" value="一覧検索：Webサービス呼び出し" /><br />
        部署ID: <input type="text" id="departmentId" />
        <input id="searchButton" type="button" value="ID検索：Webサービス呼び出し" /><br />
        部署名: <input type="text" id="departmentName" />
        <input id="createButton" type="button" value="部署登録：Webサービス呼び出し" />
        <hr />
        <div id="result"></div>

    </form>
    <script type="text/javascript">
<!--
        $(function () {
            console.log("jQuery OK!");
            // キーワード補完されないのがつらい。CDNだとダメかな？

            $("#listButton").click(function () {

                var dptId = $("#departmentId").val();
                var apiUrl = "/api/Departments/"
                console.log("AJAX通信開始");
                console.log("url: " + apiUrl);

                // ajax通信開始
                $.ajax({
                    // WebサービスURL
                    url: apiUrl,
                    // データ形式はJSON
                    dataType: "json",
                    // 結果をid=resultのdivタグに設定
                    success: function (data) {
                        // divタグの中身をすべて消す
                        $("#result *").remove();

                        // JSONデータの配列を処理
                        $(data).each(function () {
                            $("<p>" + this.DeptId + ":" + this.Name + "</p>").appendTo("#result");

                        });
                    }
                });
            });


            $("#searchButton").click(function () {

                var dptId = $("#departmentId").val();
                var apiUrl = "/api/Departments/"
                console.log("AJAX通信開始");
                console.log("departmentId: " + dptId);
                console.log("url: " + apiUrl + dptId);

                if (!dptId) {
                    alert("部署IDを入力してください");
                    return;
                }

                // ajax通信開始
                $.ajax({
                    // WebサービスURL
                    url: apiUrl + encodeURI(dptId),
                    // データ形式はJSON
                    dataType: "json",
                    // 結果をid=resultのdivタグに設定
                    success: function (data) {
                        // resultデータには、Nameプロパティが存在する前提
                        $("#result").text(data.DeptId + ":" + data.Name)
                    }
                });
            });

            $("#createButton").click(function () {

                // 部署名データ作成
                var data = { "name": $("#departmentName").val() };
                var apiUrl = "/api/Departments/"
                console.log("AJAX通信開始");
                console.log("送信データ: " + data);
                console.log("url: " + apiUrl);

                if (!data.name) {
                    alert("部署名を入力してください");
                    return;
                }

                // ajax通信開始
                $.ajax({
                    type: "POST",
                    // WebサービスURL
                    url: apiUrl,
                    data: data,
                    success: function () {
                        $("#result").text("保存完了");
                    },
                    error: function () {
                        $("#result").text("保存失敗");
                    }
                });
            });
        });
-->
    </script>
</body>
</html>

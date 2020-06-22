<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AsyncWithjQuery.aspx.vb" Inherits="AsyncSample.WebForm2" %>

<!DOCTYPE html>

<html lang="ja">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>AsyncSample - jQuery</title>
    <!-- 最新版でテストする。1系、2系はサポート対象外のため選択肢に入れない -->
    <script src="https://code.jquery.com/jquery-3.5.1.js"></script>
</head>
<body>
    <h2>非同期通信テスト</h2>
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

        <h2>◆ASP.NET WebAPIサンプル</h2>

        <input id="listButton" type="button" value="一覧検索" />&nbsp;&nbsp;
        部署ID: <input type="text" id="departmentId"  style="width:50px;"/>
        <input id="searchButton" type="button" value="ID検索"/>&nbsp;&nbsp;
        <input id="clearButton" type="button" value="一覧クリア" class="create_button"/><br />
        <table style="margin-top: 10px;">
            <colgroup>
                <col width="100"/>
                <col width="200"/>
            </colgroup>
            <tbody>
                <tr>
                    <td>部署名</td>
                    <td>
                         <input type="text" id="departmentName" />
                    </td>
                </tr>
                <tr>
                    <td>コメント</td>
                    <td>
                         <input type="text" id="comment" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right;"><input id="createButton" type="button" value="部署登録"/></td>
                </tr>


            </tbody>

        </table>
        <hr />
        <div id="result1" class="result_area"></div>


        <h2>◆ASP.NET WebFormハンドラサンプル</h2>

        <input id="btnList2" type="button" value="一覧検索" />&nbsp;&nbsp;
        部署ID: <input type="text" id="departmentId2"  style="width:50px;"/>
        <input id="btnSearch2" type="button" value="ID検索"/>&nbsp;&nbsp;
        <input id="btnClear2" type="button" value="一覧クリア"  class="create_button"/><br />
        <table style="margin-top: 10px;">
            <colgroup>
                <col width="100"/>
                <col width="200"/>
            </colgroup>
            <tbody>
                <tr>
                    <td>部署名</td>
                    <td>
                         <input type="text" id="departmentName2" />
                    </td>
                </tr>
                <tr>
                    <td>コメント</td>
                    <td>
                         <input type="text" id="comment2" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right;"><input id="btnCreate2" type="button" value="部署登録" /></td>
                </tr>


            </tbody>

        </table>
        <hr />
        <div id="result2" class="result_area"></div>

    </form>
    <script type="text/javascript">
        $(function () {
            console.log("jQuery OK!");
            // キーワード補完されないのがつらい。CDNだとダメかな？
            clearResult = function () {
                $(".result_area").each(function () {
                    $(this).text("");
                    $(this).empty();
                });
            }

            // 結果一覧クリア
            $(".create_button").click(function () {
                clearResult();
            });

            // サービス起動
            /*
             * ASP.NET WebAPI版
             */

            // 一覧検索
            $("#listButton").click(function () {
                clearResult();
                var dptId = $("#departmentId").val();
                var apiUrl = "/api/Departments/"
                var result_area = "result1"
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
                        // JSONデータの配列を処理
                        $(data).each(function () {
                            $("<p>" + this.DeptId + ":" + this.Name + ", " + this.Comment + "</p>").appendTo("#" + result_area);

                        });
                    }
                });
            });

            // ID指定検索処理
            $("#searchButton").click(function () {
                clearResult();

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
                        $("#result").text(data.DeptId + ":" + data.Name + "," + data.Comment)
                    }
                });
            });

            // 登録処理
            $("#createButton").click(function () {
                clearResult();

                // 部署名データ作成
                var data = {
                    "name": $("#departmentName").val(),
                    "comment": $("#comment").val()
                };
                var apiUrl = "/api/Departments/"
                console.log("AJAX通信開始");
                console.log("送信データ: " + data);
                console.log("url: " + apiUrl);

                if (!data.name || !data.comment) {
                    alert("部署名、またはコメントが未入力です");
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


            /*
             * ASP.NET WebForm版
             */
            // 一覧検索
            $("#btnList2").click(function () {
                clearResult();

                // 送信データ作成
                var data = {
                    "action": "show"
                };

                var dptId = $("#departmentId").val();
                var apiUrl = "/WSByAspDotNetFormHandler.aspx"
                var result_area = "result2"
                console.log("AJAX通信開始");
                console.log("url: " + apiUrl);

                // ajax通信開始
                $.ajax({
                    // メソッド指定：デフォルトはget
                    type: "POST",
                    // WebサービスURL
                    url: apiUrl,
                    data: data,
                    // データ形式はJSON
                    dataType: "json",
                    // 結果をid=resultのdivタグに設定
                    success: function (data) {
                        // JSONデータの配列を処理
                        $(data).each(function () {
                            $("<p>" + this.DeptId + ":" + this.Name + ", " + this.Comment + "</p>").appendTo("#" + result_area);

                        });
                    }
                });
            });

                     
            // ID指定検索処理
            $("#btnSearch2").click(function () {
                clearResult();

                                // 送信データ作成
                var data = {
                    "action": "show",
                    "deptId": $("#departmentId2").val()
                };

                var apiUrl = "/WSByAspDotNetFormHandler.aspx"
                console.log("AJAX通信開始");
                console.log("departmentId: " + data.deptId);
                console.log("url: " + apiUrl);

                if (!data.deptId) {
                    alert("部署IDを入力してください");
                    return;
                }

                // ajax通信開始
                $.ajax({
                    type: "POST",
                    // WebサービスURL
                    url: apiUrl,
                    data: data,
                    // データ形式はJSON
                    dataType: "json",
                    // 結果をid=resultのdivタグに設定
                    success: function (data) {
                        // resultデータには、Nameプロパティが存在する前提
                        $("#result2").text(data.DeptId + ":" + data.Name + "," + data.Comment)
                    }
                });
            });

            // 登録処理
            $("#btnCreate2").click(function () {
                clearResult();

                // 送信データ作成
                var data = {
                    "name": $("#departmentName2").val(),
                    "comment": $("#comment2").val(),
                    "action": "create"
                };
                var apiUrl = "/WSByAspDotNetFormHandler.aspx"
                console.log("AJAX通信開始");
                console.log("送信データ: " + data);
                console.log("url: " + apiUrl);

                if (!data.name || !data.comment) {
                    alert("部署名、またはコメントが未入力です");
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
    </script>
</body>
</html>

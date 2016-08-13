#その他
本章では以下のような項目について解説します。  
1.バージョンアップ  
2.データベースのバックアップ    
3.データベースの復元  
4.アンインストール  
##1.バージョンアップ
データベースのバージョンアップとFindingsEditorのバージョンアップの2種類のバージョンアップがあります。  
###1.1. データベースのバージョンアップ
最新版のデータベースを利用している場合は_1.2.FindingsEditorのバージョンアップ_に進んでください.  
株式会社メイドインクリニックのホームページ<http://www.madeinclinic.jp/>にアクセスしてください。  
画面上部のソフトウェアをクリックしてください。  
画面右側の「バージョンアップ情報」をクリックしてください。  

![feversionup01](http://www.madeinclinic.jp/software/fe/images/findingseditor/feversionup01.png)  

画面をスクロールして「dbX\_XXtoy\_YY.txt」（X,Yはバージョン）を探し右クリックして「対象をファイルに保存」をクリックして保存してください。  

データベースを作成する際に利用したFE\_setupX\_XXフォルダ内のFE_setupアプリケーションを起動してください。  
「Do SQL」をクリックしてください。  

![feversionup02](http://www.madeinclinic.jp/software/fe/images/findingseditor/feversionup02.png)   


PostgreSQLのパスワードを入力して「ok」を押してください。  
実行するsqlファイルを選んで実行してください。なお実行するファイルは先ほど保存したdbX\_XXtoy\_YY.txtです。

「[SQL_File]処理が終了しました。」と表示されればデータベースのバージョンアップは完了です。  

###1.2. FindingsEditorのバージョンアップ
株式会社メイドインクリニックのホームページ<http://www.madeinclinic.jp/>にアクセスしてください。  
最新版のFindingsEditorをダウンロードしてください。 
すべて展開してFEアプリケーションを起動してください。  
左上の初期設定をクリックして「データベースサーバーIPアドレス」、「データベースパスワード」、「シェーマ保存フォルダ」を設定して「保存」をクリックしてください。  

![feversionup03](http://www.madeinclinic.jp/software/fe/images/findingseditor/feversionup03.png)   

以上がバージョンアップとなります。  

##2.データベースのバックアップ
データベースのバックアップはPostgreSQLをインストールした際に同時にインストールされる管理ツールであるpgAdminを利用します。  
pgAdminを起動してください。

「PostgreSQL 9.5」(数字はPostgreSQLのバージョンによる)を右クリックし、「接続」をクリック。  
または、「PostgreSQL 9.5」をダブルクリックしてPostgreSQLのパスワードを入力し「ok」をおしてください。

![febackup01](http://www.madeinclinic.jp/software/fe/images/findingseditor/febackup01.png)   

次に、「データベース」の左側の「＋」マークをクリックし、「endoDB」を右クリックして「バックアップ」をクリックします。  

![febackup02](http://www.madeinclinic.jp/software/fe/images/findingseditor/febackup02.png)   

ファイル名の右側の「…」をクリックしてファイルの保存先とファイル名を指定してください。    
「フォーマット」をクリックし、「Tar」を選びます。  
続けて「エンコーディング」をクリックし、「UTF8」を選択してください。
ファイル名、フォーマット、エンコーディングを入力後「#2 ダンプオプション」タブをクリックしてください。  

![febackup03](http://www.madeinclinic.jp/software/fe/images/findingseditor/febackup03.png)   

「SET SESSION AUTHORIZATION」にチェックを入れ、「バックアップ」をクリック。

別ウィンドウが立ち上がり、「プロセスは、０のリターンコードを返しました。」と表示されればバックアップ成功です。  
「完了」を押してください。以上がバックアップの方法となります。  

![febackup04](http://www.madeinclinic.jp/software/fe/images/findingseditor/febackup04.png)   

##3.データベースの復元
前項_2.データベースのバックアップ_で作成したバックアップファイルを使ってデータベースを復元（リストア）します。  
ただし復元する際、リストアのトラブルを回避するために以下の条件を満たしてください。  
　　・FE_setupを適用済みであること。  
　　・前項で作成したバックアップファイルがデスクトップにあること。

 pgAdminを起動してください。  
「PostgreSQL 9.5」(数字はPostgreSQLのバージョンによる)を右クリックし、「接続」をクリック。  
または、「PostgreSQL 9.5」をダブルクリックしてPostgreSQLのパスワードを入力し「ok」をおしてください。  
次に、「データベース」の左側の「＋」マークをクリックし、「endoDB」を右クリックして「削除(D)/抹消…」をクリック。  

![febackup05](http://www.madeinclinic.jp/software/fe/images/findingseditor/febackup05.png)  

「Are you sure you wish to drop database “endoDB”?」と表示されたら「はい」をクリック。  
データーベース「endoDB」が削除されたらデータベース「postgres」を右クリックして「リストア」をクリックしてください。  
なお、右クリックするのは「postgres」以外のデータベースでもかまいません。  

フォーマットを「Custom or tar」, ファイル名にデスクトップ上のバックアップファイルを指定して「#2 オプションのリストア」タブをクリック。  

![febackup06](http://www.madeinclinic.jp/software/fe/images/findingseditor/febackup06.png)   

CREATE DATABASE構文を含む」および「SET SESSION AUTHORIZATION」にチェックを入れ、「リストアー」をクリックします。  

![febackup07](http://www.madeinclinic.jp/software/fe/images/findingseditor/febackup07.png)   

別ウィンドウが立ち上がり、「プロセスは、０のリターンコードを返しました。」と表示されれば復元成功です。  
「完了」を押してください。以上が復元の方法となります。  

![febackup08](http://www.madeinclinic.jp/software/fe/images/findingseditor/febackup08.png)   

##4.アンインストール
本ソフトウェアはレジストリ等を一切使用しておりませんので、フォルダごとファイルを削除するだけでアンインストールが完了します。  
しかし、登録された患者情報等はPostgreSQLに保管されているため、コンピュータの廃棄など、データの完全消去を行いたい場合は、ハードディスクの完全消去が必要となりますのでご注意ください。  
PostgreSQLのアンインストールは使用しているOSに従って行ってください。    

#データベースの導入（PostgreSQL）
本ソフトウェアでは所見をデータベースに保存しています。その際、PostgreSQLを利用します。  
本項ではインターネット接続を前提として、PostgreSQLのサイトから最新版をダウンロードし、インストールする手順を記載しています。  
インターネットに接続されていない場合は、適宜USBメモリやCD-R、DVD-R等を用いてインストール用ファイルを手配し、作業を行ってください。  

##PostgreSQLダウンロード
プラウザを開いてアドレスバーに<http://www.postgresql.org/>を入力してください。
「download」をクリックしてください。  
次にページ中windowsをクリックしてください。  

 ![postgreHP01.png](http://www.madeinclinic.jp/software/fe/images/postgre/postgreHP01.png)   

本文中のdownloadをクリックしてください。   
最新版の「Win x86-64」もしくは「Win x86-32」をクリックしてダウンロードしてください。  
適宜、ご使用の環境にあったものをお選びください。
   
![postgreHP02.png](http://www.madeinclinic.jp/software/fe/images/postgre/postgreHP02.png)  
##Postgresインストール
保存先のアプリケーションを起動してください。  

![postgreinstall01.png](http://www.madeinclinic.jp/software/fe/images/postgre/postgreinstall01.png)
  
保存ディレクトリなどを確認しつつ「Next」を続けて押してください。  
 PasswordおよびRetype passwordに同じパスワードを入力します。  
このパスワードはデータベースの管理用パスワードですので、推測されにくいものが推奨されます。  
なお、Findings Editorの準備にも利用しますので厳重に管理してください。  
入力したら、「Next」をクリック。
  
![postgreinstall02.png](http://www.madeinclinic.jp/software/fe/images/postgre/postgreinstall02.png)   

Portに5432が入力されていることを確認して「Next」をクリックします。  
Localeに「Default locale」が選択されていることを確認し、「Next」をクリック。  
Rady to install インストールの準備が完了しました。 「Next」をクリック。   
「Stack Builder・・・」のチェックを外し、「Finish」をクリック。以上でPostgreSQLのインストールは完了です。 
 
![postgreinstall03.png](http://www.madeinclinic.jp/software/fe/images/postgre/postgreinstall03.png) 
  
##PostgreSQLにおけるSSL設定
始めに[サーバー証明書の作成](./INSTALL-ja.md)で作成したserver.crtとserver.keyをコピーしておきます。
2つのファイルをC:\Program Files\PostgreSQL\9.5\dataにペーストします。

![postgressl01.png](http://www.madeinclinic.jp/software/fe/images/postgre/postgressl01.png)  

このフォルダ上の「postgresql.conf」をメモ帳もしくは適当なテキストエディタで開きます。  
    
    #ssl = off

の部分を検索をかけて探しその下の行に「ssl = on」を追加してください。  
上書き保存を忘れないよう注意してください。

![postgressl02.png](http://www.madeinclinic.jp/software/fe/images/postgre/postgressl02.png)  

続いてpg_hda.confを開いてください。下にスクロールすると

    # IPv4 local connections:
    host    all             all             127.0.0.1/32            md5
    # IPv6 local connections:
    host    all             all             ::1/128                 md5

と表記される部分がありますこの場所の２か所の「host」を「hostssl」に書き換えて上書き保存してください。

![postgressl03.png](http://www.madeinclinic.jp/software/fe/images/postgre/postgressl03.png)  

##PostgreSQL再起動
SSLの設定を反映させるためにPostgreSQLを再起動します。
したがって、Windowsの再起動によって本項の手順を代替することができます。

画面左下のwindowsマークを右クリック。  
「コンピューターの管理」をクリック。  
「サービスとアプリケーション」をクリック。  
「サービス」をクリック。  
ウィンドウ中央右手のスクロールバーを下げて、「Postgresql-・・・」上で右クリックして再起動をクリック。  

![postgrerestartl.png](http://www.madeinclinic.jp/software/fe/images/postgre/postgrerestart.png)    

  PostgreSQLの導入は以上となります。  
次項[FindingsEditorの導入](./FEINSTALL-ja.md)に進んでください。

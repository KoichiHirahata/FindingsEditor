#サーバー証明書の作成
本ソフトウェアはPostgreSQLとの通信にSSLを使用します。  
このため、サーバー証明書を作成する必要があります。  
なお、各ホームページは、デザイン変更等により異なる場合があります。適宜読み替えてご対応ください。  
##OpenSSLのダウンロード
ブラウザを起動して普段利用している検索エンジンから「shining light productions」と検索するか  
アドレスバーに<https://slproweb.com/products/Win32OpenSSL.html>を入力して以下のページを開いてください。 
 
![opensslHP01](http://www.madeinclinic.jp/software/fe/images/openssl/opensslHP.png)    


画面右のスクロールバーを下へ移動し、「Win32 OpenSSL v1.0.x Light」（32bit版）  
または「Win64 OpenSSL v1.0.x Light」（64bit版）を探してDLしてください。。  
32bit版、64bit版は使用しているOSに合わせて選択してください。（「x」はダウンロード時の最新バージョンを示しています。）  

![opensslHP02](http://www.madeinclinic.jp/software/fe/images/openssl/opensslHP02.png)   
##   OpenSSLのインストール
保存先から起動してインストールしてください。  

![opensslinstall01](http://www.madeinclinic.jp/software/fe/images/openssl/opensslinstall01.png)   

「next」を押してください。  
規約に同意して「i accept the agreement」にチェックをいれて「next」を押してください。  
保存フォルダを指定してください。  
 デフォルトのまま「C:\OpenSSL-Win64」となっていれば「next」を押してください。
そのまま「next」で進んでもらい「install」をクリックしてください。  
インストールが終了すると以下の画面が出るので  
「The OpenSSL Project」への寄付をする場合には任意のチェックボックスにチェックして「Finish」をクリックします。寄付をしない場合はチェックを全て外して「Finish」をクリック。  

![opensslinstall02](http://www.madeinclinic.jp/software/fe/images/openssl/opensslinstall02.png)   

これでOpensslのインストールは終了です。

##Opensslを利用してサーバー証明書を作成する  
管理者としてコマンドプロンプトを開いてくださいWin10の場合はデスクトップ左下windowsのロゴ部で右クリックして「コマンドプロンプト（管理者）」で開くことができます。
  
![cmdadmin01](http://www.madeinclinic.jp/software/fe/images/openssl/cmdadmin01.png)   
 
opensslのアプリケーションが入っているディレクトリに移動してください。  
C:\OpenSSL-Win64\binにアプリが入っているので、「cd C:\OpenSSL-Win64\bin」を入力してエンターを押してください。
以下のように表示されます  

![cmdopenssl01.png](http://www.madeinclinic.jp/software/fe/images/openssl/cmdopenssl01.png)   

次に「openssl req -new -text -out server.req」を入力してエンターを押してください。  
「Enter PEM pass phrase:」と表示されたら、キーボードで適当なパスフレーズを入力しEnterキーを押します。(コマンドプロンプトにはパスワードは表示されません)  
「Verifying - Enter PEM pass phrase:」もう一度パスフレーズを入力してください

![cmdopenssl02.png](http://www.madeinclinic.jp/software/fe/images/openssl/cmdopenssl02.png)   

「Country Name (2 letter code)」と表示されたらISO規定の国コードである「JP」を入力し、Enterキーを押します。この項目は必須項目です。   

![cmdopenssl03.png](http://www.madeinclinic.jp/software/fe/images/openssl/cmdopenssl03.png)   

Enterキーを押すと以下のような項目についてきかれますが何も入力せずEnterを押してください。 


    State or Province Name (full name) [Some-State]:
    Locality Name (eg, city) []:  
    Organization Name (eg, company) [Internet Widgits Pty Ltd]:  
    Organizational Unit Name (eg, section) []:     


「Common Name」には導入するサーバの名前を入力します。例として「madeinclinic.co.jp」と入力してEnterキーを押します。この項目は必須項目です。  

![cmdopenssl04.png](http://www.madeinclinic.jp/software/fe/images/openssl/cmdopenssl04.png)   

この後も以下のような項目について聞かれますが何も入力せずにEnterを押してください

    Email Address []:
    A challenge password []:
    An optional company name []:

C:\OpenSSL-Win64\bin>だけが表示されれば大丈夫です。

つぎにコマンドプロンプトに「openssl rsa -in privkey.pem -out server.key」を入力してください。  
 
    Enter pass phrase for privkey.pem:   

と聞かれますので先ほど入力したパスフレーズを入力してください。

![cmdopenssl05.png](http://www.madeinclinic.jp/software/fe/images/openssl/cmdopenssl05.png)   

    writing RSA key
とコマンドプロンプトに表示されれば次に進んでください。  
次にコマンドプロンプトに  
「openssl req -x509 -in server.req -text -key server.key -out server.crt」  を入力します。  　
C:\OpenSSL-Win64\bin>だけが表示されれば大丈夫です。  

![cmdopenssl06.png](http://www.madeinclinic.jp/software/fe/images/openssl/cmdopenssl06.png)
  
エクスプローラーからC:\OpenSSL-Win64\binの中にserver.keyとserver.cｒｔがあれば完成です。
なおserver.crtはセキュリティ証明書と書かれる場合もあります。
  
![cmdopenssl07.png](http://www.madeinclinic.jp/software/fe/images/openssl/cmdopenssl07.png) 



#Findings Editorの導入
本項ではインターネット接続を前提として、株式会社メイドインクリニックのサイトから  
最新版をダウンロードし、インストールする手順を記載しています。  
インターネットに接続されていない場合は、適宜USBメモリやCD-R、DVD-R等を用いて  
インストール用ファイルを手配し、作業を行ってください。

##Findings Editorのダウンロード
プラウザを開いてアドレスバーに
株式会社メイドインクリニックのホームページアドレス  
<http://www.madeinclinic.jp/>を入力して、Enterを押します。  
画面上部のソフトウェアにマウスを合わせ「内視鏡・超音波所見入力システム（FindingsEditor）」
をクリック。  
 
 ![fedownload01](http://www.madeinclinic.jp/software/fe/images/findingseditor/fedownload01.png)   

下にスクロールして  
「クライアントソフト（FindingsEditorx_xx_x.zip）のダウンロードはこちら（www.madeinclinic.jp内）」をクリックして保存してください。  
同様に、
「環境設定ソフト（FE_setupx_xx.zip）のダウンロードはこちら（www.madeinclinic.jp内）」もダウンロードしてください。

 ![fedownload02](http://www.madeinclinic.jp/software/fe/images/findingseditor/fedownload02.png)  
##環境設定ソフト（FE_setup）を用いたセットアップ
保存先でダウンロードしたファイルを解凍してください。  
 FE_setupx_xxフォルダを開いてアプリケーションFE_setupを起動してください。  

![fedownload03](http://www.madeinclinic.jp/software/fe/images/findingseditor/fedownload03.png)   

「EndoDB作成」をクリックしてください。  
C:\Program Files\PostgreSQL\9.5\bin　に移動してpsql.exeを選択して開いてください。

![fedsetup01](http://www.madeinclinic.jp/software/fe/images/findingseditor/fesetup01.png)   

    PostgreSQL port number :  5432

5432が入力されていることを確認して「ok」を押します。  

    ユーザ postgres のパスワード:

コマンドプロンプトが起動してパスワードを尋ねられます。  
PostgreSQLインストール時に設定したパスフレーズを入力しEnterを押します。   
なおパスフレーズは入力しても表示されません。  

「処理が終了しました」と表示されるので「ok」を押してください。

![fedsetup02](http://www.madeinclinic.jp/software/fe/images/findingseditor/fesetup02.png)   

引き続き、「db_userパスワード設定」をクリック。  
「PostgreSQL password」に、PostgreSQLインストールの際に入力した管理パスワードを入力し、  
「OK」をクリック。  

![fedsetup03](http://www.madeinclinic.jp/software/fe/images/findingseditor/fesetup03.png)   

データベース接続用ユーザのパスワードを入力し、「OK」をクリック。  
「正常に更新されました」と表示されます。   
なお、このパスワードはFindings Editorの初期設定時に使用しますので、厳重に管理してください。  

##Findings Editor の起動
つづいて、FindingsEditorx_x_xフォルダを開いてください。  
アプリケーションFEを起動してください。  
初期設定をクリックしてください。  

![fe01](http://www.madeinclinic.jp/software/fe/images/findingseditor/fe01.png)   

「データベースサーバーIPアドレス」に「localhost」と入力します。  
「DBサーバーポート」には「5432」  
「データベースID」には「db_user」が入力されていることを確認し、
「パスワード変更」をクリックします。  
データベース接続用ユーザのパスワードで設定したパスワードを入力し、「テスト接続」をクリック。  
（前項でパスワードを設定していない場合、初期設定は「test」です。）  
「接続に成功しました。」と表示されたら、「OK」をクリック。  

![fe02](http://www.madeinclinic.jp/software/fe/images/findingseditor/fe02.png)   

シェーマ保存フォルダを指定しておきます。 
すべて指定し終えたら保存をクリックしてください。 
  
HOME画面にもどったら  

    ID:  test  
    パスワード: test   

を入力してログインしてください。  

![fe03](http://www.madeinclinic.jp/software/fe/images/findingseditor/fe03.png)   

次項では[FindingsEditorの初期設定](./FEINISET-ja.md)について記述しています


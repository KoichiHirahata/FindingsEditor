#Findings Editorの運用　便利な機能
本項ではFE上で使える以下のような便利な機能を紹介します。  
1.マイワードの登録    
2.クイックワードの登録  
3.既定の所見の作成・編集  
4.印刷用所見のフッターの変更  
5.画像の登録（GraphicRenamer）
6.画像の閲覧と印刷（PｔGraviewer）

##1.マイワードの登録
マイワードとは、所見などを入力する際にクリックのみで入力することのできる定型ワードです。  
マイワードは、ユーザごとに設定することができます。（管理者のみ、全体ユーザに対して設定することができます。_2.クイックワードの登録_を参照してください。）  

HOME画面から「オプション」→「マイワード」をクリックしてください。  
「データが見つかりませんでした。」と表示されることがありますが「ok」を押してください。  
ワードを入力します。1組3ワードですが、１ワード以上で登録できます。  
表示順はマイワードの標準となり、数字が少ない方が上位に表示されます。（標示順は半角数字で入力してください）
例としてマイワード1～9までを入力しました。  　

![femyword01](http://www.madeinclinic.jp/software/fe/images/findingseditor/femyword01.png)

閉じるを押します。　所見を編集する際に画像右側のようにマイワードが登録されています。
入力フォームにカーソルがある状態でマイワードをクリックするとそのワードが挿入されます。   

![femyword02](http://www.madeinclinic.jp/software/fe/images/findingseditor/femyword02.png)  

##2.クイックワードの登録（管理者のみ）
クイックワードは、全てのユーザのマイワードを編集・追加することができます。  
HOME画面から「オプション」→「管理」→「クイックワード」をクリックしてください。  
下へスクロール最下段の空欄にワードを追加します。  
マイワードと同様に1組3ワードを登録することができますが、最低1ワードから登録可能です。  
また、適宜表示順（数字が小さいほど上位に表示される）を入力します。  
次のように登録することでクイックワードの表示対象を決めることができます。   
・検査内容が「CF」の場合　→　診断医を「Colonoscopy」で登録  
・検査内容が「GF」の場合　→　診断医を「Upper endoscopy」で登録  
・検査内容が空欄の場合　 →　全てのユーザに表示されます。    

![fequickword01](http://www.madeinclinic.jp/software/fe/images/findingseditor/fequickword01.png)   

##3.既定の所見の作成・編集 
検査結果に特に異常が見つからない場合など、同じ所見を何度も入力することなく、所見の既定の内容を入力しておくことができます。  

HOME画面から「オプション」→「管理」→「既定の所見」をクリックしてください。  
初期設定では、GF、CF、腹部エコー、甲状腺エコー、頸動脈エコー、乳腺エコー、心エコーに既定の所見が設定されています。  
入力を行う際、改行はShiftキーを押しながらEnterキーを押してください。
変更後は右下「保存」をクリックしてください。

![fekiteishoken01](http://www.madeinclinic.jp/software/fe/images/findingseditor/fekiteishoken01.png)   

##4.印刷用所見のフッターの変更  
印刷用所見のフッター部の記載を変更することができます。  
初期設定では「○○クリニック」と入力されていますので、適宜変更してください。  

FEアプリケーションが入っているフォルダ内の「result.html」を適当なテキストエディタやメモ帳で開きます。  

![feshoken_print_footer01](http://www.madeinclinic.jp/software/fe/images/findingseditor/feshoken_print_footer01.png)   
下にｽｸﾛｰﾙして○○クリニックの部分を適当な表記に書き換え上書き保存します。

![feshoken_print_footer02](http://www.madeinclinic.jp/software/fe/images/findingseditor/feshoken_print_footer02.png) 

所見の印刷画面に進むと以下のように変更されていることがわかります。  

![feshoken_print_footer03](http://www.madeinclinic.jp/software/fe/images/findingseditor/feshoken_print_footer03.png)

##5.内視鏡画像の登録（GraphicRenamer）
外部ソフトウェアのGraphicRenamerと連携して、患者の画像（内視鏡画像等）を患者IDと紐づけて整理し、登録することができます。   
登録した画像は_6.画像の閲覧（PｔGraviewer）と印刷_に説明するように閲覧することができます。  
内視鏡画像を登録する際は各メーカーによって登録の仕方が異なります。したがってここでは、動作確認済のフジフィルム社とオリンパス社で撮影した内視鏡画像についてのみ説明します。


###5.1 GraphicRenamerの設定(共通)
FEのアプリケーションと同じフォルダ内のPｔGraviewerフォルダを開きGraphicRenamerアプリケーションを起動してください。  

![Graphicrenamer01](http://www.madeinclinic.jp/software/fe/images/graphicrenamer/Graphicrenamer01.png)  


設定をクリックして初期設定を行ってください。  
例として画像フォルダは「C:\FE-share\patient」を指定します。  
「Use database server of Findings Editor」にチェックが入っているか確認します。  
データベースサーバーのIPｱﾄﾞﾚｽを入力します。 

    データベースポート番号: 5432
    データベースID: db_user

が入力されているか確認します。  
「パスワード変更」をクリックしdb_userのパスワードを入力後「テスト」をクリック。  
「接続に成功しました。」と表示されたら「保存」をクリックします。

![Graphicrenamer02](http://www.madeinclinic.jp/software/fe/images/graphicrenamer/Graphicrenamer02.png) 

###5.2.1 画像登録(フジフィルム社) 
フジフィルム社製の内視鏡画像フォルダを用意してください。  
フォルダの中には内視鏡画像（JPEG）と、その画像の情報（thu）が入っていることを確認してください。   

![Graphicrenamer_fuji_01](http://www.madeinclinic.jp/software/fe/images/graphicrenamer/Graphicrenamer_fuji_01.png)   

先ほど確認したフォルダをフォルダごとGraphicRenamerの「内視鏡画像フォルダかJPEGファイル、PDFファイルをドラッグ＆ドロップしてください。」と書かれている位置にドラッグ＆ドロップしてください。  

![Graphicrenamer_fuji_02](http://www.madeinclinic.jp/software/fe/images/graphicrenamer/Graphicrenamer_fuji_02.png)    

ドロップすると、「フォルダがドロップされました。テキストボックスに設定されたIDとカレンダーは無視されます。」と表示されるので「OK」を押してください。  
検査情報設定画面が出るので確認してから「OK」を押してください。  
氏名：○○○○が内視鏡撮影時に登録されている氏名  
氏名（DB）：○○○○がFindingsEditorに登録されている氏名   
を表しています。  
一致していない場合は対応するIDを入力して「氏名:」と「氏名:(DB)」を一致させてから「OK」を押してください。  

![Graphicrenamer_fuji_03](http://www.madeinclinic.jp/software/fe/images/graphicrenamer/Graphicrenamer_fuji_03.png)   
 
以上がフジフィルム社製の登録の方法です。

###5.2.2 画像登録(オリンパス社) 
オリンパス社製の内視鏡画像フォルダを用意してください。  
オリンパス社の内視鏡画像フォルダは年月日で名づけられています。  
フォルダ内にはCVフォルダとDCIMフォルダがあると思いますので確認してください。  

![Graphicrenamer_olym_01](http://www.madeinclinic.jp/software/fe/images/graphicrenamer/Graphicrenamer_olym_01.png) 

確認したら年月日フォルダをフォルダごとGraphicRenamerの「内視鏡画像フォルダかJPEGファイル、PDFファイルをドラッグ＆ドロップしてください。」と書かれている位置にドラッグ＆ドロップしてください。  

![Graphicrenamer_olym_02](http://www.madeinclinic.jp/software/fe/images/graphicrenamer/Graphicrenamer_olym_02.png)  


ドロップすると、「フォルダがドロップされました。テキストボックスに設定されたIDとカレンダーは無視されます。」と表示されるので「OK」を押してください。  
検査情報設定画面が出るので確認してから「OK」を押してください。  
氏名：○○○○が内視鏡撮影時に登録されている氏名  
氏名（DB）：○○○○がFindingsEditorに登録されている氏名   
を表しています。  
一致していない場合は対応するIDを入力して「氏名:」と「氏名:(DB)」を一致させてから「OK」を押してください。  

![Graphicrenamer_olym_03](http://www.madeinclinic.jp/software/fe/images/graphicrenamer/Graphicrenamer_olym_03.png) 

以上がオリンパス社製の登録の方法です。   

##6.  画像の閲覧と印刷(PｔGraviewer)
_5.内視鏡画像の登録（GraphicRenamer）_で登録した画像を閲覧することができます。  
画像の閲覧はPｔGraviewerアプリケーションを直接起動する方法とFindings Editorの所見から連携してPｔGraviewerアプリケーションを立ち上げる方法があります。  
また、画像を印刷することもできます。  
###6.1.1. PｔGraviewerアプリケーションの起動（直接）
FEのアプリケーションと同じフォルダ内のPｔGraviewerフォルダを開きPtGraViewerアプリケーションを起動してください。 

![ptgraviewer01](http://www.madeinclinic.jp/software/fe/images/ptgraviewer/ptgraviewer01.png) 
 
閲覧したい患者IDを入力して決定を押してください。左側の空白部に登録された画像の代表のサムネイルと名前が表示されます。サムネイルをダブルクリックすることで右側に登録されたすべての画像のサムネイルを表示させることができます。  

![ptgraviewer02](http://www.madeinclinic.jp/software/fe/images/ptgraviewer/ptgraviewer02.png)  

###6.1.2. PｔGraViewerアプリケーションの起動（FE経由）
HOME画面から閲覧したい患者の所見を表示してください。  
内視鏡を撮影した日にちの所見の「画像」ボタンをクリックしてください。  

![ptgraviewer03](http://www.madeinclinic.jp/software/fe/images/ptgraviewer/ptgraviewer03.png)  

 PｔGraViewerアプリケーションが起動します。  
左側に登録された画像の代表のサムネイルと名前が表示されます。サムネイルをダブルクリックすることで右側に登録されたすべての画像のサムネイルを表示させることができます。  

![ptgraviewer04](http://www.madeinclinic.jp/software/fe/images/ptgraviewer/ptgraviewer04.png)  

###6.2. PｔGraViewerアプリケーションを使った画像閲覧
6.1.で右側にサムネイルが表示されたら拡大表示したい画像をダブルクリックしてください。
画像が拡大表示されます。  画像を切り替えたい場合は左側に表示されるサムネイルをクリックしてください。  

![ptgraviewer05](http://www.madeinclinic.jp/software/fe/images/ptgraviewer/ptgraviewer05.png)   
 
###6.3.  PｔGraViewerアプリケーションを使った画像印刷
印刷したい画像を選びます。左側のサムネイルのチェックボックスにチェックを入れることで選択できます。  
チェックをつける順番で印刷の順番が決まります。  
また一枚の用紙に最大で8枚の画像を印刷できます。  

![ptgraviewer06](http://www.madeinclinic.jp/software/fe/images/ptgraviewer/ptgraviewer06.png) 

チェックした後で左上の「印刷」ボタンをクリックしてください。  
印刷プレビューが立ち上がりますので、確認してプリントしてください。

![ptgraviewer07](http://www.madeinclinic.jp/software/fe/images/ptgraviewer/ptgraviewer07.png)   

次章[その他](./FERUNNING03-ja.md)ではデータベースのバージョンアップの仕方などについて説明します。  

 

 

 

#Findings Editorの初期設定
本項ではFIndingsEditorを利用する際の以下の初期設定について解説します。  
1. ユーザーの追加・削除  
2. 検査機器の登録  
3. 検査場所の登録  
4. 病棟/外来の登録  
5. 診断名の編集・登録  
6. 診療科の登録  

##1. ユーザーの追加・削除  

「オプション」→「管理」→「施行者」でユーザーを追加します。  

![fe04](http://www.madeinclinic.jp/software/fe/images/findingseditor/feadduser01.png)   

施行者追加をクリックしてください。  

すべての項目を記載してください。  

![fe05](http://www.madeinclinic.jp/software/fe/images/findingseditor/feadduser02.png)   

注１：「Doctor」と「Viewer」が選択可能ですが、「Viewer」は別ソフトウェア「FindingSite」との連携の際に使用するものですので、選択しないでください。  
注２：初期設定では「内科」と「外科」からのみ選択可能ですが、後述の「科（診療科）の登録」にて任意の科を登録できます。  
注３：所見入力の際に施行者や1次診断医、2次診断医として表示されるか否かを決定します。  
注４：管理者はユーザの追加・削除のほか、規定値の入力・編集等を行うことができます。
  このチェックを入れないことで、通常のユーザを作成することができます。

保存をクリックしてください。  

次に、初期設定のID「test」のユーザの「削除」ボタンをクリックし、  
Informationウィンドウの「はい」をクリック。

これでユーザーの追加・削除が完了しました。  
一旦ログアウトして追加したユーザーで再ログインしてください。  


##2. 検査機器の登録

「オプション」→「管理」→「検査機器」で検査機器を追加します。   

![feeditequipment01](http://www.madeinclinic.jp/software/fe/images/findingseditor/feeditequipment01.png)   

それぞれの項目を入力します。
入力後、「閉じる」をクリックしてFindingsEditorを再起動することで設定が反映されます。  
それぞれの項目は、以下を示しています。

<table>
<tr><td>番号：　　　　　　　</td><td>この表で表示される表示順を決定します。  
</td></tr>
<tr><td>名称：　　　　</td><td>任意の名称を登録できます。</td></tr>
<tr><td>カテゴリー：</td><td>GF、CF、Side View（側視鏡）、Smallbowel(小腸鏡)、Capsule、Broncho、USから選択します。</td></tr>
<tr><td>表示順（GF）※：</td><td>検査種別で「GF」を選択した場合の表示順を決定します。 </td></tr>
<tr><td>表示順（CF）※：　</td><td>検査種別で「CF」を選択した場合の表示順を決定します。  </td></tr>
<tr><td>表示順（側視）※：　</td><td>検査種別で「側視鏡」を選択した場合の表示順を決定します。  </td></tr>
<tr><td>表示順（小腸）※：　</td><td>検査種別で「Baloon」及び「カプセル内視鏡」を選択した場合の表示順を決定します。 </td></tr>
<tr><td>表示順（US）※：　</td><td>検査種別で各種「エコー」を選択した場合の表示順を決定します。</td></tr>
<tr><td>Scope：　	</td><td>検査種別で各種内視鏡を選択した場合の表示・非表示を決定します。</td></tr>
<tr><td>US：　</td><td>検査種別で各種エコーを選択した場合の表示・非表示を決定します。  </td></tr>
<tr><td>表示：　</td><td>検査種別に関係なく、表示・非表示を決定します。  </td></tr>
<tr><td>削除：　</td><td>登録した検査機器を削除する際に使用します。削除できる場合のみ「削除ボタン」が表示されます。  </td></tr>
</table>

※各表示順は、空欄で非表示となります。  

##3. 検査場所の登録

「オプション」→「管理」→「検査場所」で検査場所を追加します。  

![feeditplace01.png](http://www.madeinclinic.jp/software/fe/images/findingseditor/feeditplace01.png)  

それぞれの項目を入力します。
入力後、「閉じる」をクリックしてFindingsEditorを再起動することで設定が反映されます。  
それぞれの項目は、以下を示しています。

<table>
<tr><td>場所No.：</td><td>場所の番号を入力します。</td></tr>
<tr><td>場所名（部屋名）：</td><td>任意の場所名・部屋名を入力します。</td></tr>
<tr><td>表示順（Endoscopy）※：</td><td>検査種別で各種「内視鏡」を選択した場合の表示順を決定します。</td></tr>
<tr><td>表示順（US）※：</td><td>検査種別で各種「エコー」を選択した場合の表示順を決定します。</td></tr>
<tr><td>表示：</td><td>検査種別に関係なく、表示・非表示を決定します。</td></tr>
<tr><td>削除：</td><td>登録した検査場所を削除する際に使用します。削除できる場合のみ「削除ボタン」が表示されます</td></tr>
</table>
		
※各表示順は、空欄で非表示となります。

##4. 病棟/外来の登録  

「オプション」→「管理」→「病棟/外来」で病棟と外来を追加します。  

![feward01.png](http://www.madeinclinic.jp/software/fe/images/findingseditor/feward01.png)  

それぞれの項目を入力します。
入力後、「閉じる」をクリックしてFindingsEditorを再起動することで設定が反映されます。  
それぞれの項目は、以下を示しています。

<table>
<tr><td>番号：</td><td>番号を入力します。</td></tr>
<tr><td>名称：</td><td>任意の名称を入力します。</td></tr>
<tr><td>表示順※：</td><td>表示順を決定します。</td></tr>
<tr><td>表示：</td><td>表示・非表示を決定します。</td></tr>
<tr><td>削除：</td><td>登録した名称を削除する際に使用します。削除できる場合のみ「削除ボタン」が表示されます。</td></tr>
</table>
	
※表示順は、空欄で非表示となります。

##5. 診断名の編集・登録

「オプション」→「管理」→「病棟/外来」で病棟と外来を追加します。  

![feeditdiagnosis01.png](http://www.madeinclinic.jp/software/fe/images/findingseditor/feeditdiagnosis01.png)   

左部に列挙されているカテゴリ名を選択してそれぞれの項目を入力します。  
項目を入力すると「更新」という列に「ここをクリックで更新。」と表示されるのでクリックします。  
 「閉じる」をクリックしてFindingsEditorを再起動することで設定が反映されます。  
それぞれの項目は、以下を示しています。

<table>
<tr><td>番号：</td><td>ウィンドウ下部に示されている範囲で番号を入力します。</td></tr>
<tr><td>英語：</td><td>（任意）英語の診断名を入力します。</td></tr>
<tr><td>日本語：</td><td>日本語の診断名を入力します。</td></tr>
<tr><td>表示順：</td><td>表示順を数値で入力します。</td></tr>
<tr><td>表示：</td><td>所見入力画面に表示・非表示を選択します。</td></tr>
<tr><td>更新：</td><td>上記項目を入力後、ここをクリックすると更新します。</td></tr>
<tr><td>削除：</td><td>登録した名称を削除する際に使用します。削除できる場合のみ「削除ボタン」が表示されます。
</td></tr>
</table>

## 6. 診療科の登録 
	
「オプション」→「管理」→「科」で診療科を追加します。  

![feeditdiagnosis01.png](http://www.madeinclinic.jp/software/fe/images/findingseditor/feeditdiagnosis01.png) 

それぞれの項目を入力します。 入力後、「閉じる」をクリックしてFindingsEditorを再起動することで設定が反映されます。  
それぞれの項目は、以下を示しています。
	
<table>
<tr><td>番号：</td><td>名称に対応する番号を入力します。</td></tr>
<tr><td>名称：</td><td>名称を入力します。</td></tr>
<tr><td>表示順：</td><td>表示順序を数値で入力します。</td></tr>
<tr><td>表示：</td><td>所見入力画面に表示・非表示を選択します。</td></tr>
<tr><td>削除：</td><td>登録した名称を削除する際に使用します。削除できる場合のみ「削除ボタン」が表示されます。</td></tr>
</table>

以上がFindings Editorの初期設定となります。  
次項でFindings Editorを運用する際の[新規患者・検査の登録](./FERUNNING01-ja.md)を解説します。
	
	

	
#Findings Editorテストサーバ構築メモ

【構築環境】Raspberry Pi B

【使用OS】RASPBIAN JESSIE LITE
Minimal image based on Debian Jessie
Version:July 2017
Release date:2017-07-05
Kernel version:4.9

【主要インストールパッケージ】
・ufw
・sshguard
・samba
・postgresql
・簡易のSMTPサーバ（sSMTP）とmailコマンド（heirloom-mailx）
・logwatch
・tripwire
・rootkit
・clamav
・ddns-update.pl



##OSセットアップメモ

１．raspi-config／言語・タイムゾーン・キーボードの設定】
http://yamaryu0508.hatenablog.com/entry/2014/08/15/140000
ついでにホストネームを「fetest.shc.orz.hm」とし，パーティションの拡張も行う．

２．IPv6の無効化
http://l-w-i.net/t/raspbian/ipv6_001.txt

３．OSのアップデート
https://raspberrypi.akaneiro.jp/archives/1431

４．vimインストール
apt-get install vim


##ufwのセットアップ

１．apt-get install ufw

２．ufw status
Status: inactive

３．IPv6サポートのOFF
vim /etc/default/ufw

以下の設定を追加する．
IPV6=yes　→　IPV6=no
http://exoself.hatenablog.com/entry/2013/04/24/170540

４．ファイアウォールに以下の許可を設定する．
ufw allow 22（SSH）
ufw allow 5432（PostgreSQL）
ufw allow 445（samba）
https://goo.gl/bjJyFK

５．ufw enable
Status: active
To                         Action      From
--                         ------      ----
22                         ALLOW       Anywhere
5432                       ALLOW       Anywhere
445                        ALLOW       Anywhere


##sshguardのセットアップ

１．apt-get install sshguard
https://goo.gl/CKaoVE


##sambaのセットアップ（テスト用共有フォルダの設定）
http://denshikousaku.net/raspberry-pi-file-server-part1-basic-samba

１．apt-get install samba
sambaのインストール

２．vim /etc/samba/smb.conf
smb.confの設定（以下の箇所だけ設定）
[global]
   interfaces = 0.0.0.0/0 eth0
   bind interfaces only = yes

#最下部に以下を追加
[share]
path=/home/pi/share
read only=no
browsable=yes
guest ok = yes
force user = pi

３．共有ディレクトリの作成
※piユーザ権限にて行う
mkdir /home/pi/share
chmod 777 /home/pi/share

４．sudo /etc/init.d/samba restart

５．Windowsのエクスプローラから接続確認
\\IPアドレス（またはドメイン）\share
新規フォルダーを作成し，削除してみる

６．WinSCPを使用し，「share_bak」フォルダを/home/pi/share_bakに設置

７．crontabで定期的に「share」ディレクトリを削除し「share_bak」の内容を「share」ディレクトリにコピーする．
crontab -e
※下記が表示されたら「３」を選択する（カネコ好み）
no crontab for pi - using an empty one

Select an editor.  To change later, run 'select-editor'.
  1. /bin/ed
  2. /bin/nano        <---- easiest
  3. /usr/bin/vim.basic
  4. /usr/bin/vim.tiny

#最下部に以下を追記
00 * * * * /bin/rm -rf /home/pi/share/*; /bin/cp -r /home/pi/share_bak/* /home/pi/share


##PostgreSQLのセットアップ
http://www.feijoa.jp/work/hal/postgres/

※rootユーザで作業
１．apt-get install postgresql

２．confファイルの修正
vim /etc/postgresql/9.4/main/postgresql.conf
listen_addresses = 'localhost' 
↓
listen_addresses = '*' 

vim /etc/postgresql/9.4/main/pg_hba.conf
#IPv4 local connections:に以下を追記
host    all             all             0.0.0.0/0               trust

３．PostgreSQLのconfファイルの確認
/etc/init.d/postgresql restart
エラー（NG）が出なければ問題なし

４．WinSCPを使用して「20170802_109_setup2.dump」を/home/piに配置

５．postgresユーザに切り替え
su - postgres

６．postgresユーザのホームディレクトリに「20170802_109_setup2.dump」を配置
cp /home/pi/20170802_109_setup2.dump ./

７．PostgreSQLにdumpファイルを適用する
/usr/bin/dropdb -U postgres endoDB; /usr/bin/dropuser -U postgres db_user; /usr/bin/dropuser -U postgres db_users; /usr/bin/dropuser -U postgres func_owner; /usr/bin/psql -f /var/lib/postgresql/20170802_109_setup2.dump

８．crontabで定期的にデータベースを削除し，テンプレートデータベースに置き換える
※postgresユーザで作業
crontab -e

#最下部に以下を追記
00 * * * * /usr/bin/dropdb -U postgres endoDB; /usr/bin/dropuser -U postgres db_user; /usr/bin/dropuser -U postgres db_users; /usr/bin/dropuser -U postgres func_owner; /usr/bin/psql -f /var/lib/postgresql/20170802_109_setup2.dump

http://wp.tech-style.info/archives/563


##sSMTPサーバのセットアップ
https://goo.gl/FWx5Cd

１．apt-get install heirloom-mailx ssmtp

２．vim /etc/ssmtp/ssmtp.conf
root=madeinclinic.log@gmail.com
mailhub=smtp.gmail.com:587
AuthUser=madeinclinic@gmail.com
AuthPass=0101_MICMIC
UseSTARTTLS=YES
hostname=fetest.shc.orz.hm


##logwatchのセットアップ
http://wings2fly.jp/yaneura/raspberry-pi-security-logwatch/

１．apt-get install logwatch

２．logwatch.confの設定
cd /usr/share/logwatch/default.conf
sudo cp logwatch.conf logwatch.conf.org
sudo vi logwatch.conf

------ 以下設定変更部位 ---------
35行目付近。レポートを標準出力からメールへ変更
#Output = stdout
Output = mail

67行目付近に追加。アーカイブファイルをログに含める。デフォはYesだが明示しとく。
Archives = Yes

72行目付近。ログのレポート日付。yesterday,today,allから選択。確認のみ。
Range = yesterday

79行目付近。レポートレベルを上げる
#Detail = Low
Detail = High
------------ ここまで -----------

３．動作確認
logwatch --range all
「madeinclinic.log@gmail.com」にメールが送られる


##tripwire のセットアップ
http://wings2fly.jp/yaneura/raspberry-pi-security-tripwire/index.html

１．apt-get install tripwire

２．設定ファイル修正
7行目付近
インタラクティブモードで使用するエディタ。デフォルトの/usr/bin/editorだとnanoになるので、vimにするため/usr/bin/vimに変更。お好みで。
EDITOR = /usr/bin/vim

8行目付近
パスフレーズのプロンプト表示を遅くする。メモリ上にパスワードが保存される期間を短くできる。falseからtrueに変更して有効化
LATEPROMPTING = true ←falseをtrueに変更

9行目付近
監査対象のディレクトリ内でファイルが変更された際に、ファイルとディレクトの変更両方を検知してしまうのを防ぐ。falseからtrueに変更して有効化
LOOSEDIRECTORYCHECKING =true

11行目付近
メールで送付されるレポートの詳細レベル。3から最高の4に変更
EMAILREPORTLEVEL =4

12行目付近
レポートの詳細レベル。3から最高の4に変更
REPORTLEVEL =4

３．設定ファイル暗号署名版作成
twadmin -m F -c /etc/tripwire/tw.cfg -S /etc/tripwire/site.key /etc/tripwire/twcfg.txt

４．ポリシーファイル最適化スクリプト作成
vim /etc/tripwire/twpolmake.pl
※スクリプトは以下サイトの物を使用
※https://centossrv.com/tripwire.shtml

５．ポリシーファイル暗号署名版作成
twadmin -m P -c /etc/tripwire/tw.cfg -p /etc/tripwire/tw.pol -S /etc/tripwire/site.key /etc/tripwire/twpol.txt.new

６．データベース作成
tripwire -m i -s -c /etc/tripwire/tw.cfg

７．ファイルチェック
tripwire -m c -s -c /etc/tripwire/tw.cfg

８．定期自動実行スクリプト作成とスケジューラ登録
vim tripwire.sh
※スクリプトは以下サイトの物を使用
※http://wings2fly.jp/yaneura/raspberry-pi-security-tripwire/index.html


##chkrootkitセットアップ
http://wings2fly.jp/yaneura/raspberry-pi-security-chkrootkit/index.html

１．apt-get install chkrootkit

２．chkrootkit | grep INFECTED

３．vim /root/chkrootkit.sh
※スクリプトは以下サイトの物を使用
※http://wings2fly.jp/yaneura/raspberry-pi-security-chkrootkit/index.html

４．chmod 700 chkrootkit.sh

５．mv chkrootkit.sh /etc/cron.daily/

６．汚染前の安全なコマンド群を確保
※以下サイトを参照
※http://wings2fly.jp/yaneura/raspberry-pi-security-tripwire/index.html


##Clam AntiVirusのセットアップ
https://harvestasya.com/rbwp/clamantivirus-install/

１．apt-get install clamav

２．ウィルススキャン設定
vim /etc/cron.daily/clamscan


##DDNS設定セットアップ

１．/root/にddns-update.plを作成
http://ieserver.net/ddns-update.txt
$ACCOUNT         = "shc";     # アカウント(サブドメイン)名設定
$DOMAIN          = "orz.hm";     # ドメイン名設定
$PASSWORD        = "らずべりー";     # パスワード設定

２．chmod 700 ddns-update.pl

３．動作確認
./ddns-update.pl

２つのファイルが作成されていれば正常動作
current_ip
ip_update.log


##IPアドレスの変更
https://goo.gl/g6CSAL

１．vim /etc/dhcpcd.conf
ファイル末尾に以下を追記
#static ip
interface eth0
static ip_address=192.168.11.199
static routers=192.168.11.1
static domain_name_servers=192.168.11.1

２．Raspberry piを再起動


##Tips１　CPU温度を表示させる
http://qiita.com/kouhe1/items/5f3f0e6fdb55e7164deb

例１）vcgencmd measure_temp

例２）watch -d -c -n 1 vcgencmd measure_temp
1秒ごとに表示


##Tips２　RaspberryPi: リードオンリーRoot-FS
http://qiita.com/mt08/items/8e65c4824f05a2335aab
※未実装


##Tips３　SSHアタックの確認方法
http://yomon.hatenablog.com/entry/2016/05/24/192046

journalctl -ef


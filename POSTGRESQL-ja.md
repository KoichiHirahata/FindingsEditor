#�f�[�^�x�[�X�̓����iPostgreSQL�j
�{�\�t�g�E�F�A�ł͏������f�[�^�x�[�X�ɕۑ����Ă��܂��B���̍ہAPostgreSQL�𗘗p���܂��B  
�{���ł̓C���^�[�l�b�g�ڑ���O��Ƃ��āAPostgreSQL�̃T�C�g����ŐV�ł��_�E�����[�h���A�C���X�g�[������菇���L�ڂ��Ă��܂��B  
�C���^�[�l�b�g�ɐڑ�����Ă��Ȃ��ꍇ�́A�K�XUSB��������CD-R�ADVD-R����p���ăC���X�g�[���p�t�@�C������z���A��Ƃ��s���Ă��������B  

##PostgreSQL�_�E�����[�h
�v���E�U���J���ăA�h���X�o�[��<http://www.postgresql.org/>����͂��Ă��������B
�udownload�v���N���b�N���Ă��������B  
���Ƀy�[�W��windows���N���b�N���Ă��������B  

 ![postgreHP01.png](http://www.madeinclinic.jp/software/fe/images/postgre/postgreHP01.png)   

�{������download���N���b�N���Ă��������B   
�ŐV�ł́uWin x86-64�v�������́uWin x86-32�v���N���b�N���ă_�E�����[�h���Ă��������B  
�K�X�A���g�p�̊��ɂ��������̂����I�т��������B
   
![postgreHP02.png](http://www.madeinclinic.jp/software/fe/images/postgre/postgreHP02.png)  
##Postgres�C���X�g�[��
�ۑ���̃A�v���P�[�V�������N�����Ă��������B  

![postgreinstall01.png](http://www.madeinclinic.jp/software/fe/images/postgre/postgreinstall01.png)
  
�ۑ��f�B���N�g���Ȃǂ��m�F���uNext�v�𑱂��ĉ����Ă��������B  
 Password�����Retype password�ɓ����p�X���[�h����͂��܂��B  
���̃p�X���[�h�̓f�[�^�x�[�X�̊Ǘ��p�p�X���[�h�ł��̂ŁA��������ɂ������̂���������܂��B  
�Ȃ��AFindings Editor�̏����ɂ����p���܂��̂Ō��d�ɊǗ����Ă��������B  
���͂�����A�uNext�v���N���b�N�B
  
![postgreinstall02.png](http://www.madeinclinic.jp/software/fe/images/postgre/postgreinstall02.png)   

Port��5432�����͂���Ă��邱�Ƃ��m�F���āuNext�v���N���b�N���܂��B  
Locale�ɁuDefault locale�v���I������Ă��邱�Ƃ��m�F���A�uNext�v���N���b�N�B  
Rady to install �C���X�g�[���̏������������܂����B �uNext�v���N���b�N�B   
�uStack Builder�E�E�E�v�̃`�F�b�N���O���A�uFinish�v���N���b�N�B�ȏ��PostgreSQL�̃C���X�g�[���͊����ł��B 
 
![postgreinstall03.png](http://www.madeinclinic.jp/software/fe/images/postgre/postgreinstall03.png) 
  
##PostgreSQL�ɂ�����SSL�ݒ�
�n�߂�[�T�[�o�[�ؖ����̍쐬](./INSTALL-ja.md)�ō쐬����server.crt��server.key���R�s�[���Ă����܂��B
2�̃t�@�C����C:\Program Files\PostgreSQL\9.5\data�Ƀy�[�X�g���܂��B

![postgressl01.png](http://www.madeinclinic.jp/software/fe/images/postgre/postgressl01.png)  

���̃t�H���_��́upostgresql.conf�v���������������͓K���ȃe�L�X�g�G�f�B�^�ŊJ���܂��B  
    
    #ssl = off

�̕����������������ĒT�����̉��̍s�Ɂussl = on�v��ǉ����Ă��������B  
�㏑���ۑ���Y��Ȃ��悤���ӂ��Ă��������B

![postgressl02.png](http://www.madeinclinic.jp/software/fe/images/postgre/postgressl02.png)  

������pg_hda.conf���J���Ă��������B���ɃX�N���[�������

    # IPv4 local connections:
    host    all             all             127.0.0.1/32            md5
    # IPv6 local connections:
    host    all             all             ::1/128                 md5

�ƕ\�L����镔��������܂����̏ꏊ�̂Q�����́uhost�v���uhostssl�v�ɏ��������ď㏑���ۑ����Ă��������B

![postgressl03.png](http://www.madeinclinic.jp/software/fe/images/postgre/postgressl03.png)  

##PostgreSQL�ċN��
SSL�̐ݒ�𔽉f�����邽�߂�PostgreSQL���ċN�����܂��B
���������āAWindows�̍ċN���ɂ���Ė{���̎菇���ւ��邱�Ƃ��ł��܂��B

��ʍ�����windows�}�[�N���E�N���b�N�B  
�u�R���s���[�^�[�̊Ǘ��v���N���b�N�B  
�u�T�[�r�X�ƃA�v���P�[�V�����v���N���b�N�B  
�u�T�[�r�X�v���N���b�N�B  
�E�B���h�E�����E��̃X�N���[���o�[�������āA�uPostgresql-�E�E�E�v��ŉE�N���b�N���čċN�����N���b�N�B  

![postgrerestartl.png](http://www.madeinclinic.jp/software/fe/images/postgre/postgrerestart.png)    

  PostgreSQL�̓����͈ȏ�ƂȂ�܂��B  
����[FindingsEditor�̓���](./FEINSTALL-ja.md)�ɐi��ł��������B

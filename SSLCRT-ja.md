#�T�[�o�[�ؖ����̍쐬
�{�\�t�g�E�F�A��PostgreSQL�Ƃ̒ʐM��SSL���g�p���܂��B  
���̂��߁A�T�[�o�[�ؖ������쐬����K�v������܂��B  
�Ȃ��A�e�z�[���y�[�W�́A�f�U�C���ύX���ɂ��قȂ�ꍇ������܂��B�K�X�ǂݑւ��Ă��Ή����������B  
##OpenSSL�̃_�E�����[�h
�u���E�U���N�����ĕ��i���p���Ă��錟���G���W������ushining light productions�v�ƌ������邩  
�A�h���X�o�[��<https://slproweb.com/products/Win32OpenSSL.html>����͂��Ĉȉ��̃y�[�W���J���Ă��������B 
 
![opensslHP01](http://www.madeinclinic.jp/software/fe/images/openssl/opensslHP.png)    


��ʉE�̃X�N���[���o�[�����ֈړ����A�uWin32 OpenSSL v1.0.x Light�v�i32bit�Łj  
�܂��́uWin64 OpenSSL v1.0.x Light�v�i64bit�Łj��T����DL���Ă��������B�B  
32bit�ŁA64bit�ł͎g�p���Ă���OS�ɍ��킹�đI�����Ă��������B�i�ux�v�̓_�E�����[�h���̍ŐV�o�[�W�����������Ă��܂��B�j  

![opensslHP02](http://www.madeinclinic.jp/software/fe/images/openssl/opensslHP02.png)   
##   OpenSSL�̃C���X�g�[��
�ۑ��悩��N�����ăC���X�g�[�����Ă��������B  

![opensslinstall01](http://www.madeinclinic.jp/software/fe/images/openssl/opensslinstall01.png)   

�unext�v�������Ă��������B  
�K��ɓ��ӂ��āui accept the agreement�v�Ƀ`�F�b�N������āunext�v�������Ă��������B  
�ۑ��t�H���_���w�肵�Ă��������B  
 �f�t�H���g�̂܂܁uC:\OpenSSL-Win64�v�ƂȂ��Ă���΁unext�v�������Ă��������B
���̂܂܁unext�v�Ői��ł��炢�uinstall�v���N���b�N���Ă��������B  
�C���X�g�[�����I������ƈȉ��̉�ʂ��o��̂�  
�uThe OpenSSL Project�v�ւ̊�t������ꍇ�ɂ͔C�ӂ̃`�F�b�N�{�b�N�X�Ƀ`�F�b�N���āuFinish�v���N���b�N���܂��B��t�����Ȃ��ꍇ�̓`�F�b�N��S�ĊO���āuFinish�v���N���b�N�B  

![opensslinstall02](http://www.madeinclinic.jp/software/fe/images/openssl/opensslinstall02.png)   

�����Openssl�̃C���X�g�[���͏I���ł��B

##Openssl�𗘗p���ăT�[�o�[�ؖ������쐬����  
�Ǘ��҂Ƃ��ăR�}���h�v�����v�g���J���Ă�������Win10�̏ꍇ�̓f�X�N�g�b�v����windows�̃��S���ŉE�N���b�N���āu�R�}���h�v�����v�g�i�Ǘ��ҁj�v�ŊJ�����Ƃ��ł��܂��B
  
![cmdadmin01](http://www.madeinclinic.jp/software/fe/images/openssl/cmdadmin01.png)   
 
openssl�̃A�v���P�[�V�����������Ă���f�B���N�g���Ɉړ����Ă��������B  
C:\OpenSSL-Win64\bin�ɃA�v���������Ă���̂ŁA�ucd C:\OpenSSL-Win64\bin�v����͂��ăG���^�[�������Ă��������B
�ȉ��̂悤�ɕ\������܂�  

![cmdopenssl01.png](http://www.madeinclinic.jp/software/fe/images/openssl/cmdopenssl01.png)   

���Ɂuopenssl req -new -text -out server.req�v����͂��ăG���^�[�������Ă��������B  
�uEnter PEM pass phrase:�v�ƕ\�����ꂽ��A�L�[�{�[�h�œK���ȃp�X�t���[�Y����͂�Enter�L�[�������܂��B(�R�}���h�v�����v�g�ɂ̓p�X���[�h�͕\������܂���)  
�uVerifying - Enter PEM pass phrase:�v������x�p�X�t���[�Y����͂��Ă�������

![cmdopenssl02.png](http://www.madeinclinic.jp/software/fe/images/openssl/cmdopenssl02.png)   

�uCountry Name (2 letter code)�v�ƕ\�����ꂽ��ISO�K��̍��R�[�h�ł���uJP�v����͂��AEnter�L�[�������܂��B���̍��ڂ͕K�{���ڂł��B   

![cmdopenssl03.png](http://www.madeinclinic.jp/software/fe/images/openssl/cmdopenssl03.png)   

Enter�L�[�������ƈȉ��̂悤�ȍ��ڂɂ��Ă�����܂����������͂���Enter�������Ă��������B 


    State or Province Name (full name) [Some-State]:
    Locality Name (eg, city) []:  
    Organization Name (eg, company) [Internet Widgits Pty Ltd]:  
    Organizational Unit Name (eg, section) []:     


�uCommon Name�v�ɂ͓�������T�[�o�̖��O����͂��܂��B��Ƃ��āumadeinclinic.co.jp�v�Ɠ��͂���Enter�L�[�������܂��B���̍��ڂ͕K�{���ڂł��B  

![cmdopenssl04.png](http://www.madeinclinic.jp/software/fe/images/openssl/cmdopenssl04.png)   

���̌���ȉ��̂悤�ȍ��ڂɂ��ĕ�����܂����������͂�����Enter�������Ă�������

    Email Address []:
    A challenge password []:
    An optional company name []:

C:\OpenSSL-Win64\bin>�������\�������Α��v�ł��B

���ɃR�}���h�v�����v�g�Ɂuopenssl rsa -in privkey.pem -out server.key�v����͂��Ă��������B  
 
    Enter pass phrase for privkey.pem:   

�ƕ�����܂��̂Ő�قǓ��͂����p�X�t���[�Y����͂��Ă��������B

![cmdopenssl05.png](http://www.madeinclinic.jp/software/fe/images/openssl/cmdopenssl05.png)   

    writing RSA key
�ƃR�}���h�v�����v�g�ɕ\�������Ύ��ɐi��ł��������B  
���ɃR�}���h�v�����v�g��  
�uopenssl req -x509 -in server.req -text -key server.key -out server.crt�v  ����͂��܂��B  �@
C:\OpenSSL-Win64\bin>�������\�������Α��v�ł��B  

![cmdopenssl06.png](http://www.madeinclinic.jp/software/fe/images/openssl/cmdopenssl06.png)
  
�G�N�X�v���[���[����C:\OpenSSL-Win64\bin�̒���server.key��server.c����������Ί����ł��B
�Ȃ�server.crt�̓Z�L�����e�B�ؖ����Ə������ꍇ������܂��B
  
![cmdopenssl07.png](http://www.madeinclinic.jp/software/fe/images/openssl/cmdopenssl07.png) 



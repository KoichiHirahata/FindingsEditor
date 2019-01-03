using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Windows;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace JedApp
{
    public class Settings
    {
        public static string DBSrvIP { get; set; } //IP address of DB server
        public static string DBSrvPort { get; set; } //Port number of DB server
        public static string DBconnectID { get; set; } //ID of DB user
        public static string DBconnectPw { get; set; } //Pw of DB user
        public static string figureFolder { get; set; } //Root folder of figures.
        public static string ptInfoPlugin { get; set; } //File location of the plug-in to get patient information
        public static int? IdLength { get; set; }
        public static string DBname { get; set; }
        public static string StartupPath { get; set; }
        public static string settingFile_location { get; set; } //Config file path
        public static string endoPrintFile { get; set; } //Template xls file for endoscopy conclusion.
        public static string sslSetting { get; set; } //SSL setting string
        public static string lang { get { return "ja"; } }

        public static void InitiateSettings()
        {
            string exePath = Environment.GetCommandLineArgs()[0];
            string exeFullPath = Path.GetFullPath(exePath);
            StartupPath = Path.GetDirectoryName(exeFullPath);
            settingFile_location = StartupPath + "\\settings.config";

            ReadSettings();
            DBname = "endoDB";
            sslSetting = "SSL Mode=Require;Trust Server Certificate=true"; //Use this when you want to connect using SSL
            ptInfoPlugin = checkPtInfoPlugin();
        }

        public static string retConnStr()
        {
            return "Host=" + DBSrvIP + ";Port=" + DBSrvPort + ";Username=" + DBconnectID + ";Password=" + DBconnectPw
                + ";Database=" + DBname + ";" + sslSetting;
        }

        #region SaveSettings
        public static void SaveSettings()
        {
            SettingsForSave st = new SettingsForSave();
            st.DBSrvIP = DBSrvIP;
            st.DBSrvPort = DBSrvPort;
            st.DBconnectID = DBconnectID;
            st.DBconnectPw = PasswordEncoder.Encrypt(DBconnectPw);
            st.figureFolder = figureFolder;
            st.ptInfoPlugin = ptInfoPlugin;
            st.IdLength = IdLength;

            //Write to a binary file
            //Create a BinaryFormatter object
            BinaryFormatter bf1 = new BinaryFormatter();
            //Open the file
            FileStream fs1 = new FileStream(settingFile_location, FileMode.Create);
            //Serialize it and save to the binery file
            bf1.Serialize(fs1, st);
            fs1.Close();
        }
        #endregion

        #region ReadSettings
        public static void ReadSettings()
        {
            if (File.Exists(settingFile_location) == true)
            {
                SettingsForSave st = new SettingsForSave();

                //Read the binary file
                //Create a BinaryFormatter object
                BinaryFormatter bf2 = new BinaryFormatter();
                IgnoreAssemblyBinder iab = new IgnoreAssemblyBinder();
                bf2.Binder = iab;
                //Open the file
                FileStream fs2 = new FileStream(settingFile_location, FileMode.Open);

                bool settingFileError = false;
                //Deserialize the binary file
                try
                {
                    st = (SettingsForSave)bf2.Deserialize(fs2);
                    fs2.Close();
                }
                catch (InvalidOperationException)
                {
                    settingFileError = true;
                    fs2.Close();
                    deleteSettingFileOrShutdown();
                }
                catch (SerializationException)
                {
                    settingFileError = true;
                    fs2.Close();
                    deleteSettingFileOrShutdown();
                }

                if (settingFileError == false)
                {
                    DBSrvIP = st.DBSrvIP;
                    DBSrvPort = st.DBSrvPort;
                    DBconnectID = st.DBconnectID;
                    DBconnectPw = PasswordEncoder.Decrypt(st.DBconnectPw);
                    figureFolder = st.figureFolder;
                    ptInfoPlugin = st.ptInfoPlugin;
                    IdLength = st.IdLength;
                }
            }
        }
        #endregion

        public static void deleteSettingFileOrShutdown()
        {
            MessageBoxResult ret;
            ret = MessageBox.Show("設定ファイルが壊れている可能性があります。ファイルを削除しますか？", "Error", MessageBoxButton.YesNo, MessageBoxImage.Error);
            if (ret == MessageBoxResult.Yes)
            {
                FileControl.delFile(settingFile_location);
                System.Diagnostics.Process.Start(StartupPath);
                Environment.Exit(0);
            }
            else
            {
                MessageBox.Show("ソフトを終了します", "Shutting down...", MessageBoxButton.OK, MessageBoxImage.Information);
                Environment.Exit(0);
            }
        }

        public static string checkPtInfoPlugin()
        {
            if (File.Exists(StartupPath + "\\plugins.ini"))
            {
                string text = FileControl.readFromFile(StartupPath + "\\plugins.ini");
                string plugin_location = FileControl.readItemSettingFromText(text, "Patient information=");
                if (File.Exists(plugin_location))
                { return plugin_location; }
                else
                { return ""; }
            }
            else
            { return ""; }
        }

        #region password
        public class PasswordEncoder
        {
            private PasswordEncoder() { }

            // 128bit(16byte)のIV（初期ベクタ）とKey（暗号キー）
            private const string AesIV = @"&%jqiIurtmsleW58";
            private const string AesKey = @"3uJi<9!$kM0MJxme";

            /// 文字列をAESで暗号化
            public static string Encrypt(string text)
            {
                try
                {
                    // AES暗号化サービスプロバイダ
                    AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                    aes.BlockSize = 128;
                    aes.KeySize = 128;
                    aes.IV = Encoding.UTF8.GetBytes(AesIV);
                    aes.Key = Encoding.UTF8.GetBytes(AesKey);
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    // 文字列をバイト型配列に変換
                    byte[] src = Encoding.Unicode.GetBytes(text);

                    // 暗号化する
                    using (ICryptoTransform encrypt = aes.CreateEncryptor())
                    {
                        byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);

                        // バイト型配列からBase64形式の文字列に変換
                        return Convert.ToBase64String(dest);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }

            /// 文字列をAESで復号化
            public static string Decrypt(string text)
            {
                try
                {
                    // AES暗号化サービスプロバイダ
                    AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                    aes.BlockSize = 128;
                    aes.KeySize = 128;
                    aes.IV = Encoding.UTF8.GetBytes(AesIV);
                    aes.Key = Encoding.UTF8.GetBytes(AesKey);
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    // Base64形式の文字列からバイト型配列に変換
                    byte[] src = System.Convert.FromBase64String(text);

                    // 複号化する
                    using (ICryptoTransform decrypt = aes.CreateDecryptor())
                    {
                        byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
                        return Encoding.Unicode.GetString(dest);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }

        }

        public class IgnoreAssemblyBinder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                return Type.GetType(typeName);
            }
        }
        #endregion
    }
}

using System;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FindingsEdior
{
    public class Settings
    {
        public static string DBSrvIP { get; set; } //IP address of DB server
        public static string DBSrvPort { get; set; } //Port number of DB server
        public static string DBconnectID { get; set; } //ID of DB user
        public static string DBconnectPw { get; set; } //Pw of DB user
        public static string DBname { get; set; }
        public static string settingFile_location { get; set; } //Config file path
        //public static Boolean isJP { get; set; } //Property for storing that machine's language is Japanese or not.
        public static string lang { get; set; } //language
        public static string endoPrintFile { get; set; } //Template xls file for endoscopy conclusion.
        public static string figureFolder { get; set; } //Root folder of figures.
        public static string sslSetting { get; set; } //SSL setting string
        public static string ptInfoPlugin { get; set; } //File location of the plug-in to get patient information

        Settings()
        {
            Settings.DBSrvIP = "";
            Settings.DBSrvPort = "";
            Settings.DBconnectID = "";
            Settings.DBconnectPw = "";
        }

        public static void initiateSettings()
        {
            settingFile_location = Application.StartupPath + "\\settings.config";
            readSettings();
            DBname = "endoDB";
            //isJP = (Application.CurrentCulture.TwoLetterISOLanguageName == "ja");
            lang = Application.CurrentCulture.TwoLetterISOLanguageName;
            //Settings.sslSetting = ""; //Use this when you want to connect without using SSL
            sslSetting = "SSL Mode=Require;Trust Server Certificate=true"; //Use this when you want to connect using SSL
            ptInfoPlugin = checkPtInfoPlugin();
        }

        public static string retConnStr()
        {
            return "Host=" + DBSrvIP + ";Port=" + DBSrvPort + ";Username=" + DBconnectID + ";Password=" + DBconnectPw
                + ";Database=" + DBname + ";" + sslSetting;
        }

        public static void saveSettings()
        {
            Settings4file st = new Settings4file();
            st.DBSrvIP = DBSrvIP;
            st.DBSrvPort = DBSrvPort;
            st.DBconnectID = DBconnectID;
            st.DBconnectPw = PasswordEncoder.Encrypt(DBconnectPw);
            st.figureFolder = figureFolder;

            //Write to a binary file
            //Create a BinaryFormatter object
            BinaryFormatter bf1 = new BinaryFormatter();
            //Open the file
            FileStream fs1 = new FileStream(settingFile_location, FileMode.Create);
            //Serialize it and save to the binery file
            bf1.Serialize(fs1, st);
            fs1.Close();

        }

        public static void readSettings()
        {
            if (File.Exists(settingFile_location) == true)
            {
                Settings4file st = new Settings4file();

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
                    st = (Settings4file)bf2.Deserialize(fs2);
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
                    Settings.DBSrvIP = st.DBSrvIP;
                    Settings.DBSrvPort = st.DBSrvPort;
                    Settings.DBconnectID = st.DBconnectID;
                    Settings.DBconnectPw = PasswordEncoder.Decrypt(st.DBconnectPw);
                    Settings.endoPrintFile = st.endoPrintFile;
                    Settings.figureFolder = st.figureFolder;
                }
            }
        }

        public static void deleteSettingFileOrShutdown()
        {
            DialogResult ret;
            ret = MessageBox.Show(FindingsEditor.Properties.Resources.SettingFileBroken, "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (ret == DialogResult.Yes)
            {
                file_control.delFile(settingFile_location);
                System.Diagnostics.Process.Start(Application.ExecutablePath);
                Environment.Exit(0);
            }
            else
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.ShuttingDown, "Shutting down...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Environment.Exit(0);
            }
        }

        public static string checkPtInfoPlugin()
        {
            if (File.Exists(Application.StartupPath + "\\plugins.ini"))
            {
                string text = file_control.readFromFile(Application.StartupPath + "\\plugins.ini");
                string plugin_location = file_control.readItemSettingFromText(text, "Patient information=");
                if (File.Exists(plugin_location))
                { return plugin_location; }
                else
                { return ""; }
            }
            else
            { return ""; }
        }
    }
}

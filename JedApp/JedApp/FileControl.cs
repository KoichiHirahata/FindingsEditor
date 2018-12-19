using System;
using System.IO;
using System.Windows;

namespace JedApp
{
    class FileControl
    {
        public static void delFile(string fileName)
        {
            if (File.Exists(fileName) == true)
            {
                try
                {
                    File.Delete(fileName);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("ファイルが使用中です。\r\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show("アクセスが拒否されました。\r\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("ファイルが存在しません。", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static string readFromFile(string fileName)
        {
            string text = "";
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                { text = sr.ReadToEnd(); }
            }
            catch (Exception e)
            { MessageBox.Show(e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            return text;
        }

        public static string readItemSettingFromText(string text, string itemName)
        {
            int index;
            index = text.IndexOf(itemName);
            if (index == -1)
            {
                MessageBox.Show("[settings.config]サポートされていないファイルタイプです。", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return "";
            }
            else
            { return getUntilNewLine(text, index + itemName.Length); }
        }

        public static string getUntilNewLine(string text, int strPoint)
        {
            string ret = "";
            for (int i = strPoint; i < text.Length; i++)
            {
                if ((text[i].ToString() != "\r") && (text[i].ToString() != "\n"))
                { ret += text[i].ToString(); }
                else
                { break; }
            }

            for (int i = 0; i < ret.Length; i++)
            {
                if (ret.Substring(0, 1) == "\t" || ret.Substring(0, 1) == " " || ret.Substring(0, 1) == "　")
                { ret = ret.Substring(1); }
                else
                { break; }
            }

            for (int i = 0; i < ret.Length; i++)
            {
                if (ret.Substring(ret.Length - 1) == "\t" || ret.Substring(ret.Length - 1) == " " || ret.Substring(ret.Length - 1) == "　")
                { ret = ret.Substring(0, ret.Length - 1); }
                else
                { break; }
            }

            return ret;
        }
    }
}

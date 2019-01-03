using System;
using System.Data;
using System.Windows;
using Npgsql;

namespace JedApp
{
    /// <summary>
    /// Configure.xaml の相互作用ロジック
    /// </summary>
    public partial class Configure : Window
    {
        public Configure()
        {
            InitializeComponent();

            tbSrvIP.Text = Settings.DBSrvIP;
            tbSrvPort.Text = Settings.DBSrvPort;
            tbUserID.Text = Settings.DBconnectID;
            //this.tbDBpw.Text = Settings.DBconnectPw; //I don't recomend to use this code.
            if (Settings.DBconnectPw == null)
            { pwState.Content = "DB Password:(Not configured)"; }
            else
            { pwState.Content = "DB Password:（Configured）"; }
            tbUserPw.Visibility = Visibility.Hidden;

            tbIdLength.Text = Settings.IdLength.ToString();

            tbPlugin.Text = Settings.ptInfoPlugin;
        }

        private void btChangePw_Click(object sender, RoutedEventArgs e)
        {
            tbUserPw.Visibility = Visibility.Visible;
            tbUserPw.Focus();
        }

        private Boolean testConnect()
        {
            #region ErrorCheck
            if (tbSrvIP.Text.Length == 0)
            {
                MessageBox.Show("IPが設定されていません", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (tbSrvPort.Text.Length == 0)
            {
                MessageBox.Show("ポートが設定されていません", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (tbUserID.Text.Length == 0)
            {
                MessageBox.Show("IDが設定されていません", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            #endregion

            string temp_pw;

            if (tbUserPw.Visibility == Visibility.Visible)
            {
                if (tbUserPw.Text.Length == 0)
                {
                    MessageBox.Show("パスワードが設定されていません", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                temp_pw = tbUserPw.Text;
            }
            else
            {
                if (Settings.DBconnectPw == null)
                {
                    MessageBox.Show("パスワードが設定されていません", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                temp_pw = Settings.DBconnectPw;
            }

            var connString = "Host=" + tbSrvIP.Text + ";Port=" + tbSrvPort.Text + ";Username=" +
                    tbUserID.Text + ";Password=" + temp_pw + ";Database=endoDB;" + Settings.sslSetting;

            using (var conn = new NpgsqlConnection(connString))
            {
                try
                { conn.Open(); }
                catch (NpgsqlException)
                {
                    MessageBox.Show("接続できませんでした。[NpgsqlException]", "Connection error", MessageBoxButton.OK, MessageBoxImage.Error);
                    conn.Close();
                    return false;
                }
                catch (System.IO.IOException)
                {
                    MessageBox.Show("接続が切断されました", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    conn.Close();
                    return false;
                }

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    MessageBox.Show("接続できませんでした。", "Connection error", MessageBoxButton.OK, MessageBoxImage.Error);
                    conn.Close();
                    return false;
                }
            }
        }

        private void btTestConnect_Click(object sender, RoutedEventArgs e)
        {
            if (testConnect())
            { MessageBox.Show("Connected successfully", "Successfully connected.", MessageBoxButton.OK, MessageBoxImage.Information); }
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            if (testConnect())
            {
                int? tempIdLength;

                if (String.IsNullOrWhiteSpace(tbIdLength.Text))
                {
                    tempIdLength = null;
                }
                else if (int.TryParse(tbIdLength.Text, out int s))
                {
                    tempIdLength = s;
                }
                else
                {
                    MessageBox.Show("IDの長さの設定が不正です", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                Settings.DBSrvIP = tbSrvIP.Text;
                Settings.DBSrvPort = tbSrvPort.Text;
                Settings.DBconnectID = tbUserID.Text;
                if (tbUserPw.Visibility == Visibility.Visible)
                { Settings.DBconnectPw = tbUserPw.Text; }
                Settings.IdLength = tempIdLength;
                Settings.ptInfoPlugin = tbPlugin.Text;
                Settings.SaveSettings();

                Close();
            }
            else
            {
                MessageBox.Show("Test connection failed.");
            }
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        { Close(); }
    }
}

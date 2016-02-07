using System;
using System.Windows;
using System.IO;
using Npgsql;

namespace FindingsEditor
{
    /// <summary>
    /// initialSettings.xaml の相互作用ロジック
    /// </summary>
    public partial class initialSettings : Window
    {
        public initialSettings()
        {
            InitializeComponent();

            tbDbSrvIpAddress.Text = Settings.DBSrvIP;
            tbDbSrvPort.Text = Settings.DBSrvPort;
            tbDbUserId.Text = Settings.DBconnectID;
            //tbDbPw.Text = Settings.DBconnectPw; //I wouldn't recomend to use this code.
            if (Settings.DBconnectPw == null)
            { lbDbUserPw.Content = Properties.Resources.DbUserPw + Properties.Resources.PwHasNotBeenConfigured; }
            else
            { lbDbUserPw.Content = Properties.Resources.DbUserPw + Properties.Resources.PwHasBeenConfigured; }
            tbDbPw.Visibility = Visibility.Collapsed;

            if (!String.IsNullOrWhiteSpace(Settings.figureFolder))
            { tbFigureFolder.Text = Settings.figureFolder; }
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        { Close(); }

        private void btPwSet_Click(object sender, RoutedEventArgs e)
        {
            tbDbPw.Visibility = Visibility.Visible;
            tbDbPw.Focus();
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbFigureFolder.Text))
            {
                if (!Directory.Exists(tbFigureFolder.Text))
                {
                    MessageBox.Show("[Figure folder]" + Properties.Resources.FolderDoesNotExist, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (testConnect())
            {
                Settings.figureFolder = tbFigureFolder.Text;
                Settings.DBSrvIP = tbDbSrvIpAddress.Text;
                Settings.DBSrvPort = tbDbSrvPort.Text;
                Settings.DBconnectID = tbDbUserId.Text;
                if (tbDbPw.Visibility == Visibility.Visible)
                { Settings.DBconnectPw = tbDbPw.Text; }
                Settings.saveSettings();
                Close();
            }
        }

        private void btTestConnect_Click(object sender, RoutedEventArgs e)
        {
            if (testConnect())
            { MessageBox.Show(Properties.Resources.ConnectionSuccessful, Properties.Resources.ConnectionSuccessful, MessageBoxButton.OK, MessageBoxImage.Information); }
        }

        private Boolean testConnect()
        {
            #region Error check
            if (tbDbSrvIpAddress.Text.Length == 0)
            {
                MessageBox.Show(Properties.Resources.ServerIpHasNotBeenConfigured, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (tbDbSrvPort.Text.Length == 0)
            {
                MessageBox.Show(Properties.Resources.ServerPortHasNotBeenConfigured, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (tbDbUserId.Text.Length == 0)
            {
                MessageBox.Show(Properties.Resources.IdRequired, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            #endregion

            string temp_pw;

            if (tbDbPw.Visibility == Visibility.Visible)
            {
                if (tbDbPw.Text.Length == 0)
                {
                    MessageBox.Show(Properties.Resources.PwHasNotBeenConfigured, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                temp_pw = tbDbPw.Text;
            }
            else
            {
                if (Settings.DBconnectPw == null)
                {
                    MessageBox.Show(Properties.Resources.PwHasNotBeenConfigured, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                temp_pw = Settings.DBconnectPw;
            }

            #region Npgsql
            NpgsqlConnection conn;
            try
            {
                conn = new NpgsqlConnection("Server=" + tbDbSrvIpAddress.Text + ";Port=" + tbDbSrvPort.Text + ";User Id=" +
                    tbDbUserId.Text + ";Password=" + temp_pw + ";Database=" + Settings.DBname + ";" + Settings.sslSetting);
            }
            catch (ArgumentException)
            {
                MessageBox.Show(Properties.Resources.ConnectionStringIsWrong, "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            #endregion

            try
            { conn.Open(); }
            catch (NpgsqlException)
            {
                MessageBox.Show(Properties.Resources.CouldNotEstablishConnection, "Connection error", MessageBoxButton.OK, MessageBoxImage.Error);
                conn.Close();
                return false;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(Properties.Resources.ConnectionClosedByServer, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                conn.Close();
                return false;
            }

            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
                return true;
            }
            else
            {
                MessageBox.Show(Properties.Resources.CouldNotEstablishConnection, "Connection error", MessageBoxButton.OK, MessageBoxImage.Error);
                conn.Close();
                return false;
            }
        }

        private void btFigureFolder_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();  //Make instance of OpenFileDialog class

            fbd.Description = Properties.Resources.SelectFolder; //Set description of dialog
            fbd.RootFolder = Environment.SpecialFolder.Desktop; //Set root folder. Deault is desktop
            fbd.SelectedPath = @"C:\"; //Set default pass
            fbd.ShowNewFolderButton = true; //Allow user to make new folder. Default is true

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { tbFigureFolder.Text = fbd.SelectedPath; }
        }
    }
}

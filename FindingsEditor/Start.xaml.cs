using System;
using System.Windows;
using System.Windows.Input;

namespace FindingsEditor
{
    /// <summary>
    /// Start.xaml の相互作用ロジック
    /// </summary>
    public partial class Start : Window
    {
        public Start()
        {
            InitializeComponent();
            Settings.initiateSettings();
            tbID.Focus();
        }

        private void fLogin()
        {
            if (Settings.DBSrvIP == null)
            {
                MessageBox.Show(Properties.Resources.ServerIpHasNotBeenConfigured, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Settings.DBconnectPw == null)
            {
                MessageBox.Show(Properties.Resources.ServerPwHasNotBeenConfigured, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            switch (db_operator.idPwCheck(this.tbID.Text, tbPw.Password))
            {
                case db_operator.idPwCheckResult.success:
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        private void btLogin_Click(object sender, EventArgs e)
        { fLogin(); }

        private void tbPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                fLogin();
        }

        private void tbID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                tbPw.Focus();
        }

        private void miInitialSettings_Click(object sender, RoutedEventArgs e)
        {
            initialSettings isw = new initialSettings();
            isw.Owner = this;
            isw.ShowDialog();
        }

        private void miVersion_Click(object sender, RoutedEventArgs e)
        {
            Version v = new Version();
            v.Owner = this;
            v.ShowDialog();
        }
    }
}

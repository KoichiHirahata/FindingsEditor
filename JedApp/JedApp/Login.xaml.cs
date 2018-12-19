using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JedApp
{
    /// <summary>
    /// Login.xaml の相互作用ロジック
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            Settings.InitiateSettings();
            tbId.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { TryLogin(); }

        private void TryLogin()
        {
            if (Settings.DBSrvIP == null)
            {
                MessageBox.Show("[Server IP] 設定情報が不正です", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Settings.DBconnectPw == null)
            {
                MessageBox.Show("[Server Password] 設定情報が不正です", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            switch (DbOperator.idPwCheck(tbId.Text, tbPw.Password))
            {
                case DbOperator.idPwCheckResult.success:
                    Close();
                    break;
                default:
                    break;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Configure cf = new Configure();
            cf.ShowDialog();
        }

        private void tbPw_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                TryLogin();
            }
        }

        private void tbId_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                tbPw.Focus();
            }
        }

    }
}

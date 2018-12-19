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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JedApp
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel vm = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = vm;
            initLogin();

            DB.DB_init();
            DbOperator.SetOperatorInfo();
            lbUserName.Content = DbOperator.operatorName;
        }

        private void initLogin()
        {
            Visibility = Visibility.Hidden;
            DbOperator.AuthDone = false;
            Login lg = new Login();
            lg.ShowDialog();

            if (!DbOperator.AuthDone)
            { Environment.Exit(0); }

            Visibility = Visibility.Visible;
        }
    }
}

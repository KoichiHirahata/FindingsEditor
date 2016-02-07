using System;
using System.Windows;

namespace FindingsEditor
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            initLogin();
            CLocalDB.WriteToLocalDB();  //Write necessary data to DB.localDB

            btSchedule.Focus();
        }

        #region Login
        private void initLogin()
        {
            Visibility = Visibility.Collapsed;
            db_operator.authDone = false;
            Start stf = new Start();
            stf.ShowDialog();
            if (!db_operator.authDone)
            { Environment.Exit(0); }

            Visibility = Visibility.Visible;
            lbUserName.Content = db_operator.operatorName;

            if (db_operator.admin_user)
            {
                miManagement.Visibility = Visibility.Visible;
                miPlugin.Visibility = Visibility.Visible;
            }
            else
            {
                miManagement.Visibility = Visibility.Collapsed;
                miPlugin.Visibility = Visibility.Collapsed;
            }

            tbPtId.Text = "";
        }
        #endregion

        private void miVersion_Click(object sender, RoutedEventArgs e)
        {
            Version v = new Version();
            v.Owner = this;
            v.ShowDialog();
        }

        private void miLogout_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(Properties.Resources.DoYouWishToLogOut, "Information", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            { initLogin(); }
        }

        private void btNewExamination_Click(object sender, RoutedEventArgs e)
        {
            NewExam ne = new NewExam();
            ne.Owner = this;
            ne.ShowDialog();
        }

        #region New patient
        private void btNewPt_Click(object sender, RoutedEventArgs e)
        {
            EditPatient ep = new EditPatient(getNewIDsample(), true, true);
            ep.Owner = this;
            ep.ShowDialog();
        }

        //After getting the max number of ID, try convert to int, and if it can, add 1 to the max number, convert to string, and return it
        private string getNewIDsample()
        {
            string maxID = feFunctions.getSelectString("SELECT max(pt_id) from patient", Settings.DBSrvIP, Settings.DBSrvPort, Settings.DBconnectID, Settings.DBconnectPw, Settings.DBname); //IDのmaxをゲットしてくる。
            return feFunctions.maxPlus1(maxID); //After adding 1 to the max number and return it
        }
        #endregion

        private void btSchedule_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = DateTime.Now;
            ExamList el = new ExamList(dt.ToString("yyyy-MM-dd"), dt.ToString("yyyy-MM-dd"), null, null, null, false);

            if (el.exam_list.Rows.Count == 0)//If there was no exam, dispose ExamList form.
            {
                MessageBox.Show(Properties.Resources.NoExamOnRecord, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                el.Close();
            }
            else
            {
                el.Owner = this;
                el.ShowDialog();
            }
        }
    }
}

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace FindingsEditor
{
    /// <summary>
    /// NewExam.xaml の相互作用ロジック
    /// </summary>
    public partial class NewExam : Window
    {
        private patient pt1;

        public NewExam()
        {
            InitializeComponent();

            dpExamDate.SelectedDate = DateTime.Today;
            //Initialize cbExamType
            cbExamType.ItemsSource = CLocalDB.localDB.Tables["exam_type"].DefaultView;
            cbExamType.SelectedValuePath = "type_no";
            cbExamType.DisplayMemberPath = "exam_name";
            cbExamType.SelectedIndex = -1;
            //Initialize cbWard
            cbWard.ItemsSource = CLocalDB.localDB.Tables["ward"].DefaultView;
            cbWard.SelectedValuePath = "ward_no";
            cbWard.DisplayMemberPath = "ward";
            cbWard.SelectedIndex = -1;
            //Initialize cbDepartment
            cbDepartment.ItemsSource = CLocalDB.localDB.Tables["department"].DefaultView;
            cbDepartment.SelectedValuePath = "code";
            cbDepartment.DisplayMemberPath = "name1";
            cbDepartment.SelectedIndex = -1;
            //Initialize cbOrderDr
            cbOrderedDr.ItemsSource = CLocalDB.localDB.Tables["orderDr"].DefaultView;
            cbOrderedDr.DisplayMemberPath = "op_name";
            cbOrderedDr.SelectedIndex = -1;
            //this.cbOrderDr.Text = null;

            dpExamDate.Focus();
        }

        private void ptLoad()
        {
            if (this.tbPtId.Text.Length == 0)
            {
                MessageBox.Show(Properties.Resources.IdRequired, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            switch (patient.numberOfPatients(this.tbPtId.Text))
            {
                case 0:
                    if (Settings.ptInfoPlugin != "")
                    {
                        #region Get patient's information with plug-in
                        string command = Settings.ptInfoPlugin;

                        ProcessStartInfo psInfo = new ProcessStartInfo();

                        psInfo.FileName = command;
                        psInfo.Arguments = tbPtId.Text;
                        psInfo.CreateNoWindow = true; // Do not open console window
                        psInfo.UseShellExecute = false; // Do not use shell

                        psInfo.RedirectStandardOutput = true;

                        Process p = Process.Start(psInfo);
                        string output = p.StandardOutput.ReadToEnd();

                        output = output.Replace("\r\r\n", "\n"); // Replace new line code
                        #endregion

                        if (MessageBox.Show(output, "Information", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
                        {
                            #region Make new patient data
                            pt1 = new patient(this.tbPtId.Text, true);
                            string ptName = file_control.readItemSettingFromText(output, "Patient Name:");
                            if (ptName != "")
                            {
                                pt1.ptName = file_control.readItemSettingFromText(output, "Patient Name:");
                                this.Pt_name.Content = pt1.ptName;

                                #region Patient's birthdate
                                string ptBirthDay = file_control.readItemSettingFromText(output, "Birth date:");

                                if (ptBirthDay != "")
                                { pt1.ptBirthday = DateTime.Parse(ptBirthDay); }
                                else
                                { pt1.ptBirthday = DateTime.Now; }

                                this.Pt_birthday.Content = pt1.ptBirthday.ToShortDateString();
                                this.Pt_age.Content = pt1.getPtAge().ToString();
                                #endregion

                                #region Gender
                                string ptGender = file_control.readItemSettingFromText(output, "Gender:");
                                switch (ptGender)
                                {
                                    case "0":
                                        pt1.ptGender = patient.gender.female;
                                        this.Pt_gender.Content = Properties.Resources.Female;
                                        break;
                                    case "1":
                                        pt1.ptGender = patient.gender.male;
                                        this.Pt_gender.Content = Properties.Resources.Male;
                                        break;
                                    default:
                                        pt1.ptGender = patient.gender.male;
                                        this.Pt_gender.Content = Properties.Resources.Male;
                                        break;
                                }
                                #endregion

                                #region Save patient's information
                                feFunctions.functionResult save_res = pt1.writePtAllData(pt1);
                                if (save_res == feFunctions.functionResult.success)
                                { break; }
                                else if (save_res == feFunctions.functionResult.failed)
                                { MessageBox.Show(Properties.Resources.DataBaseError, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
                                else if (save_res == feFunctions.functionResult.connectionError)
                                { MessageBox.Show(Properties.Resources.ConnectionFailed, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
                                #endregion
                            }
                            else if (ptName == "")
                            { MessageBox.Show(Properties.Resources.PluginCouldntGetPtName, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
                            #endregion
                        }
                        else
                        { MessageBox.Show(Properties.Resources.ProcedureHasBeenCancelled); }
                    }

                    // If plug-in procedure was canseled, or plug-in not exist, codes below will be run
                    MessageBox.Show(Properties.Resources.CouldntFindPatientData, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    EditPatient ep = new EditPatient(tbPtId.Text, true, false);
                    ep.Owner = this;
                    ep.ShowDialog();
                    if (patient.numberOfPatients(tbPtId.Text) == 1)
                        ptLoad();
                    break;
                case 1:
                    pt1 = new patient(this.tbPtId.Text, false);
                    pt1.readPtData(pt1.ptID);
                    Pt_name.Content = pt1.ptName;
                    if (pt1.ptGender == patient.gender.female)
                    { Pt_gender.Content = Properties.Resources.Female; }
                    else
                    { Pt_gender.Content = Properties.Resources.Male; }
                    Pt_birthday.Content = pt1.ptBirthday.ToShortDateString();
                    Pt_age.Content = pt1.getPtAge().ToString();
                    break;
                default:
                    MessageBox.Show(Properties.Resources.DataBaseError, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }

        private void btPtEdit_Click(object sender, RoutedEventArgs e)
        {
            #region Error check
            if (this.tbPtId.Text.Length == 0)
            {
                MessageBox.Show(Properties.Resources.IdRequired, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            #endregion

            EditPatient ep = new EditPatient(tbPtId.Text, false, false);
            ep.Owner = this;
            ep.ShowDialog();
            //Show new data.
            ptLoad();
        }

        private void tbPtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ptLoad();
        }

        private void btPtLoad_Click(object sender, RoutedEventArgs e)
        { ptLoad(); }

        private void btConfirm_Click(object sender, RoutedEventArgs e)
        {
            #region Error check
            if (dpExamDate.SelectedDate == null)
            {
                MessageBox.Show("[" + Properties.Resources.ExamDate + "]" + Properties.Resources.NotSelected, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (tbPtId.Text.Length == 0)
            {
                MessageBox.Show(Properties.Resources.IdRequired, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (patient.numberOfPatients(this.tbPtId.Text) == 0)
            {
                MessageBox.Show(Properties.Resources.CouldntFindPatientData, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(cbExamType.Text))
            {
                MessageBox.Show("[" + Properties.Resources.ExamType + "]" + Properties.Resources.NotSelected, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            #endregion

            string sql1 = "INSERT INTO exam(pt_id";
            string sql2 = " VALUES(:p0";

            if (this.tbPurpose.Text.Length != 0)
            {
                sql1 += ", purpose";
                sql2 += ", :p1";
            }

            if (cbOrderedDr.Text.Length != 0)
            {
                sql1 += ", order_dr";
                sql2 += ", :p2";
            }

            sql1 += ", exam_day, exam_type, exam_status, exam_visible";
            sql2 += ", :p3, :p4, 0, true";

            if (!string.IsNullOrWhiteSpace(cbDepartment.Text))
            {
                sql1 += ", department";
                sql2 += ", " + cbDepartment.SelectedValue.ToString();
            }

            if (!string.IsNullOrWhiteSpace(cbWard.Text))
            {
                sql1 += ", ward_id";
                sql2 += ", '" + cbWard.SelectedValue.ToString() + "'";
            }

            string SQL = sql1 + ")" + sql2 + ");";
            switch (feFunctions.ExeNonQuery(SQL, tbPtId.Text,
                tbPurpose.Text,
                cbOrderedDr.Text,
                dpExamDate.SelectedDate.Value.ToString(),
                cbExamType.SelectedValue.ToString()))
            {
                case feFunctions.functionResult.connectionError:
                    MessageBox.Show(Properties.Resources.ConnectionFailed, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case feFunctions.functionResult.failed:
                    MessageBox.Show(Properties.Resources.DataBaseError, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case feFunctions.functionResult.success:
                    this.Close();
                    break;
            }
        }
    }
}

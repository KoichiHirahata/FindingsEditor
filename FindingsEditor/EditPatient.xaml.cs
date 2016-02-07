using System;
using System.Diagnostics;
using System.Windows;

namespace FindingsEditor
{
    /// <summary>
    /// EditPatient.xaml の相互作用ロジック
    /// </summary>
    public partial class EditPatient : Window
    {
        private Boolean pNewPt { get; set; }
        private patient pt1;

        public EditPatient(string PtID, Boolean newPt, bool ID_editable)
        {
            InitializeComponent();

            pt1 = new patient(PtID, newPt);
            pNewPt = newPt;
            if (newPt)
            { tbPtId.Text = pt1.ptID; }
            else
            {
                if (patient.numberOfPatients(PtID) == 1)
                { readPtData(); }
                else
                { MessageBox.Show(Properties.Resources.IdRequired, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }

            if (ID_editable)
            { tbPtId.IsEnabled = true; }
            else
            { tbPtId.IsEnabled = false; }
        }

        private void readPtData()
        {
            tbPtId.Text = pt1.ptID;
            tbPtName.Text = pt1.ptName;
            if (pt1.ptGender == patient.gender.female)
            { rbFemale.IsChecked = true; }
            else
            { rbMale.IsChecked = true; }
            dpBirthday.Text = pt1.ptBirthday.ToShortDateString();
        }

        #region Save
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            if (pt1.ptID == tbPtId.Text)
            { savePt(); }
            else
            {
                int ret = pt1.getNumOfExams();
                if (ret == -1)
                { MessageBox.Show(Properties.Resources.ProcedureHasBeenCancelled, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
                else if (ret == 0)
                { savePt(); }
                else if (ret > 0)
                { MessageBox.Show(Properties.Resources.ExamRecordAlreadyExist + Properties.Resources.ProcedureHasBeenCancelled, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }

        private void savePt()
        {
            if (tbPtId.Text.Length == 0)
            {
                MessageBox.Show(Properties.Resources.IdRequired, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if ((rbFemale.IsChecked == false) && (rbMale.IsChecked == false))
            {
                MessageBox.Show(Properties.Resources.SelectGender, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //When ID has been changed...
            if (this.tbPtId.Text != pt1.ptID)
            {
                if (pt1.newPt)
                {
                    switch (patient.checkIdDuplicate(this.tbPtId.Text))
                    {
                        case patient.idDuplicateResult.NotExist:
                            if (MessageBox.Show(Properties.Resources.YouGoingToChangePtId, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
                            {
                                pt1.ptID = this.tbPtId.Text;
                                pt1.ptName = this.tbPtName.Text;
                                if (rbFemale.IsChecked == true)
                                    pt1.ptGender = patient.gender.female;
                                else if (rbMale.IsChecked == true)
                                    pt1.ptGender = patient.gender.male;
                                pt1.ptBirthday = dpBirthday.SelectedDate.Value;
                                pt1.newPt = true;
                                switch (pt1.writePtAllData(pt1))
                                {
                                    case feFunctions.functionResult.success:
                                        Close();
                                        break;
                                    case feFunctions.functionResult.failed:
                                        Close();
                                        MessageBox.Show(Properties.Resources.DataBaseError, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        break;
                                    case feFunctions.functionResult.connectionError:
                                        this.Close();
                                        MessageBox.Show(Properties.Resources.ConnectionFailed, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        break;
                                }
                            }
                            else
                                return;
                            break;
                        case patient.idDuplicateResult.None:
                            MessageBox.Show(Properties.Resources.IdAlreadyExists, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case patient.idDuplicateResult.Duplicated:
                            MessageBox.Show(Properties.Resources.IdAlreadyExists, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case patient.idDuplicateResult.Error:
                            MessageBox.Show(Properties.Resources.DataBaseError, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
                else
                {
                    MessageBox.Show(Properties.Resources.IdMayNotBeChanged, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.tbPtId.Text = pt1.ptID;
                }
            }
            else    //Regular procedure
            {
                pt1.ptName = this.tbPtName.Text;
                if (rbFemale.IsChecked == true)
                    pt1.ptGender = patient.gender.female;
                else if (rbMale.IsChecked == true)
                    pt1.ptGender = patient.gender.male;
                pt1.ptBirthday = dpBirthday.SelectedDate.Value;
                switch (pt1.writePtAllData(pt1))
                {
                    case feFunctions.functionResult.success:
                        Close();
                        break;
                    case feFunctions.functionResult.failed:
                        Close();
                        MessageBox.Show(Properties.Resources.DataBaseError, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case feFunctions.functionResult.connectionError:
                        this.Close();
                        MessageBox.Show(Properties.Resources.ConnectionFailed, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
        }
        #endregion

        private void btCancel_Click(object sender, RoutedEventArgs e)
        { Close(); }

        private void btLoadPtData_Click(object sender, RoutedEventArgs e)
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

            #region Set patient data
            string ptName = file_control.readItemSettingFromText(output, "Patient Name:");
            if (ptName != "")
            {
                this.tbPtName.Text = file_control.readItemSettingFromText(output, "Patient Name:");

                #region Patient's birthdate
                string ptBirthDay = file_control.readItemSettingFromText(output, "Birth date:");

                if (ptBirthDay != "")
                { dpBirthday.SelectedDate = DateTime.Parse(ptBirthDay); }
                #endregion

                #region Gender
                string ptGender = file_control.readItemSettingFromText(output, "Gender:");
                switch (ptGender)
                {
                    case "0":
                        rbFemale.IsChecked = true;
                        break;
                    case "1":
                        rbMale.IsChecked = true;
                        break;
                    default:
                        break;
                }
                #endregion

            }
            else if (ptName == "")
            { MessageBox.Show(Properties.Resources.PluginCouldntGetPtName, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            #endregion
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Settings.ptInfoPlugin == "")
            { btLoadPtData.Visibility = Visibility.Collapsed; }
            else
            { btLoadPtData.Visibility = Visibility.Visible; }
        }
    }
}

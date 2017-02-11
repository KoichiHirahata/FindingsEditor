using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Diagnostics;

namespace FindingsEdior
{
    public partial class EditPt : Form
    {
        private Boolean pNewPt { get; set; }
        private patient pt1;

        public EditPt(string PtID, Boolean newPt, Boolean ID_editable)
        {
            InitializeComponent();

            pt1 = new patient(PtID, newPt);
            pNewPt = newPt;
            if (newPt)
            { this.tbPtID.Text = pt1.ptID; }
            else
            {
                if (patient.numberOfPatients(PtID) == 1)
                { readPtData(); }
                else
                { MessageBox.Show(FindingsEditor.Properties.Resources.NoPatient, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            if (ID_editable)
            { tbPtID.Enabled = true; }
            else
            { tbPtID.Enabled = false; }
        }

        private void readPtData()
        {
            this.tbPtID.Text = pt1.ptID;
            this.tbPtName.Text = pt1.ptName;
            if (pt1.ptGender == patient.gender.female)
            { this.rbFemale.Checked = true; }
            else
            { this.rbMale.Checked = true; }
            this.dateTimePicker1.Text = pt1.ptBirthday.ToShortDateString();
        }

        #region Save
        private void btSave_Click(object sender, EventArgs e)
        {
            if (pt1.ptID == tbPtID.Text)
            { savePt(); }
            else
            {
                int ret = pt1.getNumOfExams();
                if (ret == -1)
                { MessageBox.Show(FindingsEditor.Properties.Resources.ProcedureCancelled, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else if (ret == 0)
                { savePt(); }
                else if (ret > 0)
                { MessageBox.Show(FindingsEditor.Properties.Resources.ExamsAlreadyExist + FindingsEditor.Properties.Resources.ProcedureCancelled, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void savePt()
        {
            if (this.tbPtID.Text.Length == 0)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.NoID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((rbFemale.Checked == false) && (rbMale.Checked == false))
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.DetermineGender, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //IDが変更されていた時の処理
            if (this.tbPtID.Text != pt1.ptID)
            {
                if (pt1.newPt)
                {
                    switch (patient.checkIdDuplicate(this.tbPtID.Text))
                    {
                        case patient.idDuplicateResult.NotExist:
                            if (MessageBox.Show(FindingsEditor.Properties.Resources.IDchanging, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                pt1.ptID = this.tbPtID.Text;
                                pt1.ptName = this.tbPtName.Text;
                                if (this.rbFemale.Checked)
                                    pt1.ptGender = patient.gender.female;
                                else if (this.rbMale.Checked)
                                    pt1.ptGender = patient.gender.male;
                                pt1.ptBirthday = this.dateTimePicker1.Value;
                                pt1.newPt = true;
                                switch (pt1.writePtAllData(pt1))
                                {
                                    case uckyFunctions.functionResult.success:
                                        this.Close();
                                        break;
                                    case uckyFunctions.functionResult.failed:
                                        this.Close();
                                        MessageBox.Show(FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    case uckyFunctions.functionResult.connectionError:
                                        this.Close();
                                        MessageBox.Show(FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                }
                            }
                            else
                                return;
                            break;
                        case patient.idDuplicateResult.None:
                            MessageBox.Show(FindingsEditor.Properties.Resources.IdDuplicated, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case patient.idDuplicateResult.Duplicated:
                            MessageBox.Show(FindingsEditor.Properties.Resources.IdDuplicated, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case patient.idDuplicateResult.Error:
                            MessageBox.Show(FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                else
                {
                    MessageBox.Show(FindingsEditor.Properties.Resources.CanNotChangeId, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.tbPtID.Text = pt1.ptID;
                }
            }
            else    //通常処理
            {
                pt1.ptName = this.tbPtName.Text;
                if (this.rbFemale.Checked)
                    pt1.ptGender = patient.gender.female;
                else if (this.rbMale.Checked)
                    pt1.ptGender = patient.gender.male;
                pt1.ptBirthday = this.dateTimePicker1.Value;
                switch (pt1.writePtAllData(pt1))
                {
                    case uckyFunctions.functionResult.success:
                        this.Close();
                        break;
                    case uckyFunctions.functionResult.failed:
                        this.Close();
                        MessageBox.Show(FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.connectionError:
                        this.Close();
                        MessageBox.Show(FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }
        #endregion

        private void btCancel_Click(object sender, EventArgs e)
        { this.Close(); }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btSave.Focus();
                savePt();
            }
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            #region Get patient's information with plug-in
            string command = Settings.ptInfoPlugin;

            ProcessStartInfo psInfo = new ProcessStartInfo();

            psInfo.FileName = command;
            psInfo.Arguments = tbPtID.Text;
            psInfo.CreateNoWindow = true; // Do not open console window
            psInfo.UseShellExecute = false; // Do not use shell

            psInfo.RedirectStandardOutput = true;

            Process p = Process.Start(psInfo);
            string output = p.StandardOutput.ReadToEnd();

            output = output.Replace("\r\r\n", "\n"); // Replace new line code
            #endregion

            #region Make new patient data
            string ptName = file_control.readItemSettingFromText(output, "Patient Name:");
            if (ptName != "")
            {
                this.tbPtName.Text= file_control.readItemSettingFromText(output, "Patient Name:");

                #region Patient's birthdate
                string ptBirthDay = file_control.readItemSettingFromText(output, "Birth date:");

                if (ptBirthDay != "")
                { this.dateTimePicker1.Value = DateTime.Parse(ptBirthDay); }
                #endregion

                #region Gender
                string ptGender = file_control.readItemSettingFromText(output, "Gender:");
                switch (ptGender)
                {
                    case "0":
                        rbFemale.Checked = true;
                        break;
                    case "1":
                        rbMale.Checked = true;
                        break;
                    default:
                        break;
                }
                #endregion

            #endregion
            }
            else if (ptName == "")
            { MessageBox.Show(FindingsEditor.Properties.Resources.PluginCouldntGetPtName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void EditPt_Shown(object sender, EventArgs e)
        {
            if (Settings.ptInfoPlugin == "")
            { btLoad.Visible = false; }
            else
            { btLoad.Visible = true; }
        }
    }
}

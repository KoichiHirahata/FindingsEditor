using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace FindingsEdior
{
    public partial class CreateExam : Form
    {
        private patient pt1;

        public CreateExam()
        {
            InitializeComponent();
            // Initialize cbExamType
            cbExamType.DataSource = CLocalDB.localDB.Tables["exam_type"];
            cbExamType.ValueMember = "type_no";
            cbExamType.DisplayMember = "exam_name";
            this.cbExamType.SelectedIndex = -1;
            // Initialize cbWard
            cbWard.DataSource = CLocalDB.localDB.Tables["ward"];
            cbWard.ValueMember = "ward_no";
            cbWard.DisplayMember = "ward";
            cbWard.SelectedIndex = -1;
            // Initialize cbDepartment
            cbDepartment.DataSource = CLocalDB.localDB.Tables["department"];
            cbDepartment.ValueMember = "code";
            cbDepartment.DisplayMember = "name1";
            cbDepartment.SelectedIndex = -1;
            //Initialize cbOrderDr
            cbOrderDr.DataSource = CLocalDB.localDB.Tables["orderDr"];
            cbOrderDr.DisplayMember = "op_name";
            cbOrderDr.SelectedIndex = -1;
            // this.cbOrderDr.Text = null;

            // Elase text of patient information labels
            Pt_name.Text = "";
            Pt_gender.Text = "";
            Pt_birthday.Text = "";
            Pt_age.Text = "";
        }

        private void btPtLoad_Click(object sender, EventArgs e)
        {
            ptLoad();
        }

        private void ptLoad()
        {
            if (this.tbPtId.Text.Length == 0)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.NoID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                        if (MessageBox.Show(output, "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            #region Make new patient data
                            pt1 = new patient(this.tbPtId.Text, true);
                            string ptName = file_control.readItemSettingFromText(output, "Patient Name:");
                            if (ptName != "")
                            {
                                pt1.ptName = file_control.readItemSettingFromText(output, "Patient Name:");
                                this.Pt_name.Text = pt1.ptName;

                                #region Patient's birthdate
                                string ptBirthDay = file_control.readItemSettingFromText(output, "Birth date:");

                                if (ptBirthDay != "")
                                { pt1.ptBirthday = DateTime.Parse(ptBirthDay); }
                                else
                                { pt1.ptBirthday = DateTime.Now; }

                                this.Pt_birthday.Text = pt1.ptBirthday.ToShortDateString();
                                this.Pt_age.Text = pt1.getPtAge().ToString();
                                #endregion

                                #region Gender
                                string ptGender = file_control.readItemSettingFromText(output, "Gender:");
                                switch (ptGender)
                                {
                                    case "0":
                                        pt1.ptGender = patient.gender.female;
                                        this.Pt_gender.Text = FindingsEditor.Properties.Resources.Female;
                                        break;
                                    case "1":
                                        pt1.ptGender = patient.gender.male;
                                        this.Pt_gender.Text = FindingsEditor.Properties.Resources.Male;
                                        break;
                                    default:
                                        pt1.ptGender = patient.gender.male;
                                        this.Pt_gender.Text = FindingsEditor.Properties.Resources.Male;
                                        break;
                                }
                                #endregion

                                #region Save patient's information
                                uckyFunctions.functionResult save_res = pt1.writePtAllData(pt1);
                                if (save_res == uckyFunctions.functionResult.success)
                                { break; }
                                else if (save_res == uckyFunctions.functionResult.failed)
                                { MessageBox.Show(FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                else if (save_res == uckyFunctions.functionResult.connectionError)
                                { MessageBox.Show(FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                #endregion
                            }
                            else if (ptName == "")
                            { MessageBox.Show(FindingsEditor.Properties.Resources.PluginCouldntGetPtName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                            #endregion
                        }
                        else
                        { MessageBox.Show(FindingsEditor.Properties.Resources.ProcedureCancelled); }
                    }

                    // If plug-in procedure was canseled, or plug-in not exist, codes below will be run
                    MessageBox.Show(FindingsEditor.Properties.Resources.NoPatient, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EditPt ep = new EditPt(this.tbPtId.Text, true, false);
                    ep.ShowDialog(this);
                    if (patient.numberOfPatients(this.tbPtId.Text) == 1)
                        ptLoad();
                    break;
                case 1:
                    pt1 = new patient(this.tbPtId.Text, false);
                    pt1.readPtData(pt1.ptID);
                    this.Pt_name.Text = pt1.ptName;
                    if (pt1.ptGender == patient.gender.female)
                    { this.Pt_gender.Text = FindingsEditor.Properties.Resources.Female; }
                    else
                    { this.Pt_gender.Text = FindingsEditor.Properties.Resources.Male; }
                    this.Pt_birthday.Text = pt1.ptBirthday.ToShortDateString();
                    this.Pt_age.Text = pt1.getPtAge().ToString();
                    break;
                default:
                    MessageBox.Show(FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void tbPtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                ptLoad();
        }

        private void btConfirm_Click(object sender, EventArgs e)
        {
            #region Error check
            if (this.tbPtId.Text.Length == 0)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.NoID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (patient.numberOfPatients(this.tbPtId.Text) == 0)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.NoPatient, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(cbExamType.Text))
            {
                MessageBox.Show("[" + FindingsEditor.Properties.Resources.ExamType + "]" + FindingsEditor.Properties.Resources.NotSelected, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (this.cbOrderDr.Text.Length != 0)
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
            switch (uckyFunctions.ExeNonQuery(SQL, this.tbPtId.Text,
                this.tbPurpose.Text,
                this.cbOrderDr.Text,
                this.dtpExamDate.Value.ToString(),
                this.cbExamType.SelectedValue.ToString()))
            {
                case uckyFunctions.functionResult.connectionError:
                    MessageBox.Show(FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case uckyFunctions.functionResult.failed:
                    MessageBox.Show(FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case uckyFunctions.functionResult.success:
                    this.Close();
                    break;
            }
        }

        private void btPtEdit_Click(object sender, EventArgs e)
        {
            #region Error check
            if (this.tbPtId.Text.Length == 0)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.NoID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            EditPt ep = new EditPt(tbPtId.Text, false, false);
            ep.ShowDialog(this);
            //Show new data.
            ptLoad();
        }
    }
}

using System;
using System.Windows.Forms;
using System.Diagnostics;
using Npgsql;

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
            cbExamType.SelectedIndex = -1;
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
            // cbOrderDr.Text = null;

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
            if (tbPtId.Text.Length == 0)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.NoID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            switch (patient.numberOfPatients(tbPtId.Text))
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
                            pt1 = new patient(tbPtId.Text, true);
                            string ptName = file_control.readItemSettingFromText(output, "Patient Name:");
                            if (ptName != "")
                            {
                                pt1.ptName = file_control.readItemSettingFromText(output, "Patient Name:");
                                Pt_name.Text = pt1.ptName;

                                #region Patient's birthdate
                                string ptBirthDay = file_control.readItemSettingFromText(output, "Birth date:");

                                if (ptBirthDay != "")
                                { pt1.ptBirthday = DateTime.Parse(ptBirthDay); }
                                else
                                { pt1.ptBirthday = DateTime.Now; }

                                Pt_birthday.Text = pt1.ptBirthday.ToShortDateString();
                                Pt_age.Text = pt1.getPtAge().ToString();
                                #endregion

                                #region Gender
                                string ptGender = file_control.readItemSettingFromText(output, "Gender:");
                                switch (ptGender)
                                {
                                    case "0":
                                        pt1.ptGender = patient.gender.female;
                                        Pt_gender.Text = FindingsEditor.Properties.Resources.Female;
                                        break;
                                    case "1":
                                        pt1.ptGender = patient.gender.male;
                                        Pt_gender.Text = FindingsEditor.Properties.Resources.Male;
                                        break;
                                    default:
                                        pt1.ptGender = patient.gender.male;
                                        Pt_gender.Text = FindingsEditor.Properties.Resources.Male;
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
                    EditPt ep = new EditPt(tbPtId.Text, true, false);
                    ep.ShowDialog(this);
                    if (patient.numberOfPatients(tbPtId.Text) == 1)
                        ptLoad();
                    break;
                case 1:
                    pt1 = new patient(tbPtId.Text, false);
                    pt1.readPtData(pt1.ptID);
                    Pt_name.Text = pt1.ptName;
                    if (pt1.ptGender == patient.gender.female)
                    { Pt_gender.Text = FindingsEditor.Properties.Resources.Female; }
                    else
                    { Pt_gender.Text = FindingsEditor.Properties.Resources.Male; }
                    Pt_birthday.Text = pt1.ptBirthday.ToShortDateString();
                    Pt_age.Text = pt1.getPtAge().ToString();
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
            if (tbPtId.Text.Length == 0)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.NoID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (patient.numberOfPatients(tbPtId.Text) == 0)
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

            try
            {
                using (var conn = new NpgsqlConnection(Settings.retConnStr()))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "INSERT INTO exam(pt_id, purpose, order_dr, exam_day, exam_type, exam_status, exam_visible, department, ward_id)"
                            + " VALUES(@pid, @purpose, @orderDr, @e_day, @e_type, 0, true, @dep, @ward);";
                        cmd.Parameters.AddWithValue("pid", tbPtId.Text);
                        cmd.Parameters.AddWithValue("purpose", tbPurpose.Text);
                        cmd.Parameters.AddWithValue("orderDr", cbOrderDr.Text);
                        cmd.Parameters.AddWithValue("e_day", dtpExamDate.Value);
                        cmd.Parameters.AddWithValue("e_type", cbExamType.SelectedValue);
                        cmd.Parameters.AddWithValue("dep", (string.IsNullOrWhiteSpace(cbDepartment.Text)) ? DBNull.Value : cbExamType.SelectedValue);
                        cmd.Parameters.AddWithValue("ward", (string.IsNullOrWhiteSpace(cbWard.Text)) ? DBNull.Value : cbWard.SelectedValue);
                        cmd.ExecuteNonQuery();
                        Close();
                    }
                }
            }
            catch (NpgsqlException nex)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.DataBaseError + "\r\n" + nex.Message
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.DataBaseError + "\r\n" + ex.Message
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.DataBaseError + "\r\n" + ex.Message
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btPtEdit_Click(object sender, EventArgs e)
        {
            #region Error check
            if (tbPtId.Text.Length == 0)
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

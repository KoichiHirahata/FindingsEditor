using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Npgsql;

namespace FindingsEdior
{
    public partial class PatientMain : Form
    {
        private patient pt1;
        private Boolean pCanEdit { get; set; }
        Timer timer = new Timer();

        public PatientMain(string PtID)
        {
            InitializeComponent();
            pt1 = new patient(PtID, false);
            this.dgvExams.Font = new Font(dgvExams.Font.Name, 12);

            if (pt1.ptExist)
            {
                if (uckyFunctions.canEdit("patient", "pt_id", "LIKE", "'" + PtID + "'"))
                {
                    pCanEdit = true;
                    uckyFunctions.updateLockTimeIP("patient", "pt_id", "LIKE", "'" + pt1.ptID + "'");
                    btEditPtData.Visible = true;
                    tbPtInfo.ReadOnly = false;
                    btConfirm.Visible = true;
                }
                else
                {
                    MessageBox.Show(Properties.Resources.ReadOnlyMode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pCanEdit = false;
                    btEditPtData.Visible = false;
                    tbPtInfo.ReadOnly = true;
                    btConfirm.Visible = false;
                }

                readPtData();
                readExams();
                //以下、タイマー処理
                timer.Interval = 30000;  //単位はmsec。
                timer.Tick += new EventHandler(timer_Tick);
                timer.Start();
            }
            else
            { MessageBox.Show(Properties.Resources.NoPatient, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }
        //This function is necessary for lock
        private void timer_Tick(object sender, EventArgs e)
        { uckyFunctions.updateLockTimeIP("patient", "pt_id", "LIKE", "'" + this.Pt_ID.Text + "'"); }

        private void readPtData()
        {
            this.Pt_ID.Text = pt1.ptID;
            this.Pt_name.Text = pt1.ptName;
            if (pt1.ptGender == patient.gender.female)
            { this.Pt_gender.Text = Properties.Resources.Female; }
            else
            { this.Pt_gender.Text = Properties.Resources.Male; }
            DateTime bday = pt1.ptBirthday;
            this.Pt_birthday.Text = bday.ToShortDateString();
            this.Pt_age.Text = pt1.getPtAge().ToString();
            this.tbPtInfo.Text = pt1.ptInfo;
        }

        #region DataGridView
        private void readExams()
        {
            #region Npgsql connection
            NpgsqlConnection conn;

            try
            {
                conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                    Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);
            }
            catch (ArgumentException)
            {
                MessageBox.Show(Properties.Resources.WrongConnectingString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            string sql;
            string status_name;
            string exam_type_name;

            if (Settings.lang == "ja")
            {
                status_name = "name_jp";
                exam_type_name = "name_jp";
            }
            else
            {
                status_name = "name_eng";
                exam_type_name = "name_eng";
            }

            sql = "SELECT exam_id, exam_day, exam_type." + exam_type_name + " AS exam_name, status." + status_name + " AS status_name,"
                + " exam_type.type_no AS exam_type_no, exam_type.name_eng AS type_name_en"
                + " FROM exam INNER JOIN exam_type ON exam.exam_type = exam_type.type_no"
                + " INNER JOIN status ON exam.exam_status = status.status_no"
                + " WHERE pt_id ='" + pt1.ptID + "' ORDER BY exam_day DESC";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            try
            { da.Fill(dt); }
            catch (NpgsqlException)
            {
                MessageBox.Show(Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return;
            }

            if (dt.Rows.Count == 0)
            { return; }
            else
            {
                dgvExams.Columns.Clear();    //これしとかないと検索するたびにボタンが増え続ける。
                dgvExams.DataSource = dt;

                #region btEdit
                DataGridViewButtonColumn btEdit = new DataGridViewButtonColumn(); //DataGridViewButtonColumnの作成
                btEdit.Name = "btEdit";
                btEdit.UseColumnTextForButtonValue = true;    //ボタンにテキスト表示
                btEdit.Text = Properties.Resources.Edit;
                dgvExams.Columns.Add(btEdit);
                #endregion

                #region Add btImage
                DataGridViewButtonColumn btImage = new DataGridViewButtonColumn(); //DataGridViewButtonColumnの作成
                btImage.Name = "btImage";  //列の名前を設定
                btImage.UseColumnTextForButtonValue = true;    //ボタンにテキスト表示
                btImage.Text = Properties.Resources.Image;  //ボタンの表示テキスト設定
                dgvExams.Columns.Add(btImage);           //ボタン追加
                #endregion

                #region Add btPrint
                DataGridViewButtonColumn btPrint = new DataGridViewButtonColumn(); //DataGridViewButtonColumnの作成
                btPrint.Name = "btPrint";
                btPrint.UseColumnTextForButtonValue = true;    //ボタンにテキスト表示
                btPrint.Text = Properties.Resources.Print;
                dgvExams.Columns.Add(btPrint);
                #endregion

                #region Change columns header text
                dgvExams.Columns["exam_id"].Visible = false;
                dgvExams.Columns["exam_name"].HeaderText = Properties.Resources.ExamType;
                dgvExams.Columns["exam_day"].HeaderText = Properties.Resources.Date;
                dgvExams.Columns["status_name"].HeaderText = Properties.Resources.Status;
                dgvExams.Columns["btEdit"].HeaderText = Properties.Resources.Edit;
                dgvExams.Columns["btImage"].HeaderText = Properties.Resources.Image;
                dgvExams.Columns["btPrint"].HeaderText = Properties.Resources.Print;
                #endregion

                #region Set invisible
                dgvExams.Columns["exam_type_no"].Visible = false;
                dgvExams.Columns["type_name_en"].Visible = false;
                #endregion

                foreach (DataGridViewColumn dc in dgvExams.Columns)
                { dc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; }

                this.dgvExams.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.Columns[e.ColumnIndex].Name == "btEdit")
            { editExam(dgvExams.Rows[e.RowIndex].Cells["exam_id"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["exam_type_no"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["type_name_en"].Value.ToString()); }
            else if (dgv.Columns[e.ColumnIndex].Name == "btImage")
            { uckyFunctions.showImages(pt1.ptID, uckyFunctions.dateTo8char(dgv.Rows[e.RowIndex].Cells["exam_day"].Value.ToString(), Settings.lang)); }
            else if (dgv.Columns[e.ColumnIndex].Name == "btPrint")
            { printExam(dgv.Rows[e.RowIndex].Cells["exam_id"].Value.ToString()); }
        }

        private void dgvExams_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataGridView dgv = (DataGridView)sender;
                if (dgv.RowCount == 0)
                { return; }

                if (dgv.Columns[dgv.CurrentCell.ColumnIndex].Name == "btEdit")
                {
                    editExam(dgv.Rows[dgv.CurrentCell.RowIndex].Cells["exam_id"].Value.ToString(), dgv.Rows[dgv.CurrentCell.RowIndex].Cells["exam_type_no"].Value.ToString(), dgv.Rows[dgv.CurrentCell.RowIndex].Cells["type_name_en"].Value.ToString());
                    return;
                }
                else if (dgv.Columns[dgv.CurrentCell.ColumnIndex].Name == "btImage")
                {
                    uckyFunctions.showImages(pt1.ptID, uckyFunctions.dateTo8char(dgv.Rows[dgv.CurrentCell.RowIndex].Cells["exam_day"].Value.ToString(), Settings.lang));
                    return;
                }
                else if (dgv.Columns[dgv.CurrentCell.ColumnIndex].Name == "btPrint")
                {
                    printExam(dgv.Rows[dgv.CurrentCell.RowIndex].Cells["exam_id"].Value.ToString());
                    return;
                }
            }
        }

        private void editExam(string exam_id_str, string exam_type_no, string type_name_en)
        {
            if (int.Parse(exam_type_no) >= 10000)
            {
                if (Directory.Exists(Application.StartupPath + @"\plugin\" + type_name_en))
                {
                    if (File.Exists(Application.StartupPath + @"\plugin\" + type_name_en + @"\" + type_name_en + ".exe"))
                    {
                        System.Diagnostics.Process p = System.Diagnostics.Process.Start(Application.StartupPath + @"\plugin\" + type_name_en + @"\" + type_name_en + ".exe",
                            "/DBSrvIP:" + Settings.DBSrvIP
                            + " /DBSrvPort:" + Settings.DBSrvPort
                            + " /DBconnectID:" + Settings.DBconnectID
                            + " /DBconnectPw:" + Settings.DBconnectPw
                            + " /exam_id:" + exam_id_str
                            + " /operator_id:" + db_operator.operatorID);
                    }
                    else
                    { MessageBox.Show("[Plugin]" + Properties.Resources.FileNotExist, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else
                { MessageBox.Show("[Plugin]" + Properties.Resources.FolderNotExist, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else
            {
                EditFindings ef = new EditFindings(exam_id_str);
                ef.ShowDialog(this);
                ef.Dispose();
            }
        }

        private void printExam(string exam_id_str)
        {
            #region Error Check
            if (!System.IO.File.Exists(Application.StartupPath + @"\result.html"))
            {
                MessageBox.Show("[Result template file]" + Properties.Resources.FileNotExist, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion
            ExamResult er = new ExamResult(exam_id_str);
            er.ShowDialog(this);
            er.Dispose();
        }
        #endregion

        private void btConfirm_Click(object sender, EventArgs e)
        {
            pt1.ptInfo = tbPtInfo.Text;
            pt1.writePtInfo(pt1);
        }

        private void PatientMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.tbPtInfo.Text != pt1.ptInfo)
            {
                switch (MessageBox.Show(Properties.Resources.SummarySaveYN, "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                {
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    case DialogResult.Yes:
                        pt1.ptInfo = tbPtInfo.Text;
                        pt1.writePtInfo(pt1);
                        break;
                    case DialogResult.No:
                        break;
                }
            }
        }

        private void btEditPtData_Click(object sender, EventArgs e)
        {
            EditPt ep = new EditPt(Pt_ID.Text, false, true);
            ep.ShowDialog(this);
            //Show new data.
            pt1 = new patient(Pt_ID.Text, false);
            readPtData();
        }

        private void PatientMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Stop();
            if (pCanEdit)
            { uckyFunctions.delLockTimeIP("patient", "pt_id", "LIKE", "'" + this.Pt_ID.Text + "'"); }
        }
    }
}

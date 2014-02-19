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

namespace endoDB
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
            {
                MessageBox.Show(Properties.Resources.NoPatient, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //タイマー処理に必要な関数。ここでupdateLockTimeを呼び出す。
        private void timer_Tick(object sender, EventArgs e)
        {
            uckyFunctions.updateLockTimeIP("patient", "pt_id", "LIKE", "'" + this.Pt_ID.Text + "'");
        }

        private void readPtData()
        {
            this.Pt_ID.Text = pt1.ptID;
            this.Pt_name.Text = pt1.ptName;
            if (pt1.ptGender == patient.gender.female)
            {
                this.Pt_gender.Text = Properties.Resources.Female;
            }
            else
            {
                this.Pt_gender.Text = Properties.Resources.Male;
            }
            DateTime bday = pt1.ptBirthday;
            this.Pt_birthday.Text = bday.ToShortDateString();
            this.Pt_age.Text = pt1.getPtAge().ToString();
            this.tbPtInfo.Text = pt1.ptInfo;
        }

        private void readExams()
        {
            NpgsqlConnection conn;

            try
            {
                conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                    Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;");
            }
            catch (ArgumentException)
            {
                MessageBox.Show(Properties.Resources.WrongConnectingString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //
            //ここから下がデータの読み込み部分。
            //
            string sql;
            if (Settings.isJP)
            {
                sql = "SELECT exam_id, exam_day, name_jp AS exam_name FROM exam INNER JOIN exam_type ON exam.exam_type = exam_type.type_no"
                + " WHERE pt_id ='" + pt1.ptID + "' ORDER BY exam_day DESC";
            }
            else
            {
                sql = "SELECT exam_id, exam_day, name_eng AS exam_name FROM exam INNER JOIN exam_type ON exam.exam_type = exam_type.type_no"
                + " WHERE pt_id ='" + pt1.ptID + "' ORDER BY exam_day DESC";
            }

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
            }
            catch (NpgsqlException)
            {
                MessageBox.Show(Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return;
            }

            if (dt.Rows.Count == 0)
            {
                return;
            }
            else
            {
                this.dgvExams.Columns.Clear();    //これしとかないと検索するたびにボタンが増え続ける。
                this.dgvExams.DataSource = dt;

                //列名のテキストを変更する
                this.dgvExams.Columns["exam_id"].Visible = false;
                this.dgvExams.Columns["exam_name"].HeaderText = Properties.Resources.ExamType;
                this.dgvExams.Columns["exam_day"].HeaderText = Properties.Resources.Date;

                DataGridViewButtonColumn btColumn = new DataGridViewButtonColumn(); //DataGridViewButtonColumnの作成
                btColumn.Name = Properties.Resources.ptSelect;  //列の名前を設定
                btColumn.UseColumnTextForButtonValue = true;    //ボタンにテキスト表示
                btColumn.Text = Properties.Resources.ptSelect;  //ボタンの表示テキスト設定
                dgvExams.Columns.Add(btColumn);           //ボタン追加

                foreach (DataGridViewColumn dc in dgvExams.Columns)
                {
                    dc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;  //列幅自動調整
                }

                this.dgvExams.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            //"Button"列ならば、ボタンがクリックされた
            if (dgv.Columns[e.ColumnIndex].Name == Properties.Resources.ptSelect)
            {
                //MessageBox.Show(e.RowIndex.ToString() + "行のボタンがクリックされました。");
                EditFindings ef = new EditFindings(dgvExams.Rows[e.RowIndex].Cells["exam_id"].Value.ToString());
                ef.ShowDialog(this);
                ef.Dispose();
            }
        }

        private void btConfirm_Click(object sender, EventArgs e)
        {
            pt1.ptInfo = tbPtInfo.Text;
            pt1.writePtInfo(pt1);
        }

        private void btEditPtData_Click(object sender, EventArgs e)
        {
            EditPt ep = new EditPt(pt1.ptID, false);
            ep.ShowDialog(this);
            pt1.readPtData(pt1.ptID);
            readPtData();
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
            uckyFunctions.delLockTimeIP("patient", "pt_id", "LIKE", "'" + this.Pt_ID.Text + "'");
        }

        private void btEditPtData_Click_1(object sender, EventArgs e)
        {
            EditPt ep = new EditPt(Pt_ID.Text, false);
            ep.ShowDialog(this);
            //Show new data.
            pt1 = new patient(Pt_ID.Text, false);
            readPtData();
        }
    }
}

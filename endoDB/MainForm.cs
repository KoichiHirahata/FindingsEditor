using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace endoDB
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            initLogin();
            CLocalDB.WriteToLocalDB();  //Write necessary data to DB.localDB
        }

        #region Login
        private void initLogin()
        {
            this.Visible = false;
            db_operator.authDone = false;
            StartForm stf = new StartForm();
            stf.ShowDialog(this);
            stf.Dispose();
            if (!db_operator.authDone)
            { Environment.Exit(0); }

            this.Visible = true;
            this.lbUserName.Text = db_operator.operatorName;

            if (db_operator.admin_user)
            { this.managementToolStripMenuItem.Visible = true; }
            else
            { this.managementToolStripMenuItem.Visible = false; }

            this.tbPtID.Text = "";
        }
        #endregion

        #region PatientView
        private void btPtView_Click(object sender, EventArgs e)
        { ptView(); }

        private void tbPtID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                ptView();
        }

        private void ptView()
        {
            if (string.IsNullOrWhiteSpace(tbPtID.Text))
            {
                MessageBox.Show(Properties.Resources.NoID, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            patient pt1 = new patient(this.tbPtID.Text, false);
            if (pt1.ptExist)
            {
                PatientMain pm = new PatientMain(this.tbPtID.Text);
                pm.ShowDialog(this);
            }
            else
            { MessageBox.Show(Properties.Resources.OpenFormFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        #endregion

        #region New patient
        private void btNewPt_Click(object sender, EventArgs e)
        {
            EditPt ep = new EditPt(getNewIDsample(), true, true);
            ep.ShowDialog(this);
        }

        //IDのmaxを取り込み、それがintに変換できたら変換した上で+1し、文字列にして返す関数
        private string getNewIDsample()
        {
            string maxID = uckyFunctions.getSelectString("SELECT max(pt_id) from patient", Settings.DBSrvIP, Settings.DBSrvPort, Settings.DBconnectID, Settings.DBconnectPw, "endoDB"); //IDのmaxをゲットしてくる。
            return uckyFunctions.maxPlus1(maxID); //maxに1足してreturn。
        }
        #endregion

        private void btSearchPt_Click(object sender, EventArgs e)
        {
            SearchPt spt = new SearchPt();
            spt.Show();
        }

        private void btNewExamination_Click(object sender, EventArgs e)
        {
            CreateExam ce = new CreateExam();
            ce.ShowDialog(this);
        }

        private void btSchedule_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            ExamList el = new ExamList(dt.ToString("yyyy-MM-dd"), dt.ToString("yyyy-MM-dd"), null, null, null, false);

            if (el.exam_list.Rows.Count == 0)//If there was no exam, dispose ExamList form.
            { MessageBox.Show(Properties.Resources.NoExam, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else
            { el.ShowDialog(this); }
            el.Dispose();
        }

        private void btDateSearch_Click(object sender, EventArgs e)
        {
            ExamList el = new ExamList(dtpExamDate.Value.ToString("yyyy-MM-dd"), dtpExamDate.Value.ToString("yyyy-MM-dd"), null, null, null, false);
            if (el.exam_list.Rows.Count == 0)//If there was no exam, dispose ExamList form.
            { MessageBox.Show(Properties.Resources.NoExam, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else
            { el.ShowDialog(this); }
            el.Dispose();
        }

        #region MenuStrip
        #region search exams
        private void searchByDepartmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchDepart sd = new SearchDepart();
            sd.ShowDialog(this);
        }

        private void searchByOperator1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchByOp sbo = new SearchByOp(false);
            sbo.ShowDialog(this);
        }

        private void searchByOperator15ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchByOp sbo = new SearchByOp(true);
            sbo.ShowDialog(this);
        }
        #endregion

        #region option
        private void myWordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditWords ew = new EditWords();
            ew.ShowDialog(this);
        }

        private void operatorManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            operatorList opl = new operatorList();
            opl.ShowDialog(this);
        }

        private void wordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditAllWords eaw = new EditAllWords();
            eaw.ShowDialog(this);
        }

        private void defaultFindingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditDefaultFindings edf = new EditDefaultFindings();
            edf.ShowDialog(this);
        }

        private void equipmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditEquipments ee = new EditEquipments();
            ee.ShowDialog(this);
        }

        private void placeRoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditPlace ep = new EditPlace();
            ep.ShowDialog(this);
        }

        private void wardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditWard ew = new EditWard();
            ew.ShowDialog(this);
        }

        private void diagnosesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditDiagnoses ed = new EditDiagnoses();
            ed.ShowDialog(this);
        }

        private void departmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditDepartment ed = new EditDepartment();
            ed.ShowDialog(this);
        }
        #endregion

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Properties.Resources.InformLogout, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            { initLogin(); }
        }
        #endregion
    }
}

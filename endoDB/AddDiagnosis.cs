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
    public partial class AddDiagnosis : Form
    {
        private int exam_id { get; set; }

        private int bt1start { get; set; }
        private int bt1end { get; set; }
        private int bt2start { get; set; }
        private int bt2end { get; set; }
        private int bt3start { get; set; }
        private int bt3end { get; set; }
        private int bt4start { get; set; }
        private int bt4end { get; set; }
        private int bt5start { get; set; }
        private int bt5end { get; set; }
        private int bt6start { get; set; }
        private int bt6end { get; set; }
        private int bt7start { get; set; }
        private int bt7end { get; set; }
        private int bt8start { get; set; }
        private int bt8end { get; set; }
        private int bt9start { get; set; }
        private int bt9end { get; set; }
        private int bt10start { get; set; }
        private int bt10end { get; set; }
        private int bt11start { get; set; }
        private int bt11end { get; set; }
        private int bt12start { get; set; }
        private int bt12end { get; set; }

        public Boolean add_diag { get; set; }//default: false  EditFindings needs this property to know this form added diagnosis or not. 
        public int diag_code { get; set; }
        public Boolean suspect { get; set; }
        public string stockSQL { get; set; }

        public AddDiagnosis(int _exam_id, int _examType)
        {
            InitializeComponent();
            add_diag = false;
            exam_id = _exam_id;
            this.dgv.Font = new Font(dgv.Font.Name, 12);
            this.dgv.DataSource = CLocalDB.localDB.Tables["diag_name"];

            this.dgv.Columns[1].ReadOnly = true; //Switch name_jp or name_eng to read only.

            #region Add chbSusp
            DataGridViewCheckBoxColumn chbSusp = new DataGridViewCheckBoxColumn();
            chbSusp.Name = "chbSusp";  //列の名前を設定
            chbSusp.TrueValue = "true";
            chbSusp.FalseValue = "false";
            dgv.Columns.Add(chbSusp);
            #endregion

            #region Add btSelect
            DataGridViewButtonColumn btSelect = new DataGridViewButtonColumn(); //DataGridViewButtonColumnの作成
            btSelect.Name = "btSelect";  //列の名前を設定
            btSelect.UseColumnTextForButtonValue = true;  //ボタンにテキスト表示
            btSelect.Text = Properties.Resources.Select;  //ボタンの表示テキスト設定
            dgv.Columns.Add(btSelect);     //ボタン追加
            #endregion

            #region Initialize columns
            this.dgv.Columns[0].Visible = false;
            this.dgv.Columns[2].Visible = false;

            this.dgv.Columns[1].HeaderText = Properties.Resources.Diagnosis;
            this.dgv.Columns[3].HeaderText = Properties.Resources.Suspect;
            this.dgv.Columns[4].HeaderText = null;
            #endregion

            setButtons(_examType);
            resizeColumns();
        }

        private void setButtons(int _examType)
        {
            DataRow[] drs;
            switch (_examType)
            {
                #region GF
                case 1:
                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=1000");
                    lb1.Text = drs[0]["name"].ToString();
                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=1001");
                    bt1.Text = drs[0]["name"].ToString();
                    bt1start = int.Parse(drs[0]["start_no"].ToString());
                    bt1end = int.Parse(drs[0]["end_no"].ToString());
                    bt2.Visible = false;
                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=200000");
                    bt3.Text = drs[0]["name"].ToString();
                    bt3start = int.Parse(drs[0]["start_no"].ToString());
                    bt3end = int.Parse(drs[0]["end_no"].ToString());

                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=2000");
                    lb2.Text = drs[0]["name"].ToString();
                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=2001");
                    bt4.Text = drs[0]["name"].ToString();
                    bt4start = int.Parse(drs[0]["start_no"].ToString());
                    bt4end = int.Parse(drs[0]["end_no"].ToString());
                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=3000");
                    bt5.Text = drs[0]["name"].ToString();
                    bt5start = int.Parse(drs[0]["start_no"].ToString());
                    bt5end = int.Parse(drs[0]["end_no"].ToString());
                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=4000");
                    bt6.Text = drs[0]["name"].ToString();
                    bt6start = int.Parse(drs[0]["start_no"].ToString());
                    bt6end = int.Parse(drs[0]["end_no"].ToString());

                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=5000");
                    lb3.Text = drs[0]["name"].ToString();
                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=5001");
                    bt7.Text = drs[0]["name"].ToString();
                    bt7start = int.Parse(drs[0]["start_no"].ToString());
                    bt7end = int.Parse(drs[0]["end_no"].ToString());
                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=6000");
                    bt8.Text = drs[0]["name"].ToString();
                    bt8start = int.Parse(drs[0]["start_no"].ToString());
                    bt8end = int.Parse(drs[0]["end_no"].ToString());
                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=7000");
                    bt9.Text = drs[0]["name"].ToString();
                    bt9start = int.Parse(drs[0]["start_no"].ToString());
                    bt9end = int.Parse(drs[0]["end_no"].ToString());

                    lb4.Text = "";
                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=10001");
                    bt10.Text = drs[0]["name"].ToString();
                    bt10start = int.Parse(drs[0]["start_no"].ToString());
                    bt10end = int.Parse(drs[0]["end_no"].ToString());
                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=20000");
                    bt11.Text = drs[0]["name"].ToString();
                    bt11start = int.Parse(drs[0]["start_no"].ToString());
                    bt11end = int.Parse(drs[0]["end_no"].ToString());
                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=100001");
                    bt12.Text = drs[0]["name"].ToString();
                    bt12start = int.Parse(drs[0]["start_no"].ToString());
                    bt12end = int.Parse(drs[0]["end_no"].ToString());
                    break;
                #endregion

                #region CF
                case 2:
                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=30000");
                    lb1.Text = drs[0]["name"].ToString();
                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=30001");
                    bt1.Text = drs[0]["name"].ToString();
                    bt1start = int.Parse(drs[0]["start_no"].ToString());
                    bt1end = int.Parse(drs[0]["end_no"].ToString());
                    bt2.Visible = false;
                    bt3.Visible = false;

                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=40000");
                    lb2.Text = drs[0]["name"].ToString();
                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=40001");
                    bt4.Text = drs[0]["name"].ToString();
                    bt4start = int.Parse(drs[0]["start_no"].ToString());
                    bt4end = int.Parse(drs[0]["end_no"].ToString());
                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=50000");
                    bt5.Text = drs[0]["name"].ToString();
                    bt5start = int.Parse(drs[0]["start_no"].ToString());
                    bt5end = int.Parse(drs[0]["end_no"].ToString());
                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=60000");
                    bt6.Text = drs[0]["name"].ToString();
                    bt6start = int.Parse(drs[0]["start_no"].ToString());
                    bt6end = int.Parse(drs[0]["end_no"].ToString());

                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=70000");
                    lb3.Text = drs[0]["name"].ToString();
                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=70001");
                    bt7.Text = drs[0]["name"].ToString();
                    bt7start = int.Parse(drs[0]["start_no"].ToString());
                    bt7end = int.Parse(drs[0]["end_no"].ToString());
                    bt8.Visible = false;
                    bt9.Visible = false;

                    lb4.Text = "";
                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=100001");
                    bt10.Text = drs[0]["name"].ToString();
                    bt10start = int.Parse(drs[0]["start_no"].ToString());
                    bt10end = int.Parse(drs[0]["end_no"].ToString());
                    drs = CLocalDB.localDB.Tables["diag_category"].Select("id=200001");
                    bt11.Text = drs[0]["name"].ToString();
                    bt11start = int.Parse(drs[0]["start_no"].ToString());
                    bt11end = int.Parse(drs[0]["end_no"].ToString());
                    bt12.Visible = false;
                    break;
                #endregion
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
        }

        private void resizeColumns()
        {
            foreach (DataGridViewColumn dc in dgv.Columns)
            { dc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; }
        }

        private void buttonFunction(int i)
        {
            switch (i)
            {
                case 1:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt1start + ")AND(no<=" + bt1end + ")";
                    break;
                case 2:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt2start + ")AND(no<=" + bt2end + ")";
                    break;
                case 3:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt3start + ")AND(no<=" + bt3end + ")";
                    break;
                case 4:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt4start + ")AND(no<=" + bt4end + ")";
                    break;
                case 5:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt5start + ")AND(no<=" + bt5end + ")";
                    break;
                case 6:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt6start + ")AND(no<=" + bt6end + ")";
                    break;
                case 7:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt7start + ")AND(no<=" + bt7end + ")";
                    break;
                case 8:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt8start + ")AND(no<=" + bt8end + ")";
                    break;
                case 9:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt9start + ")AND(no<=" + bt9end + ")";
                    break;
                case 10:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt10start + ")AND(no<=" + bt10end + ")";
                    break;
                case 11:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt11start + ")AND(no<=" + bt11end + ")";
                    break;
                case 12:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt12start + ")AND(no<=" + bt12end + ")";
                    break;
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView temp_dgv = (DataGridView)sender;

            if (temp_dgv.Columns[e.ColumnIndex].Name == "btSelect")
            {
                add_diag = true;

                diag_code = (int)temp_dgv.Rows[e.RowIndex].Cells["no"].Value;
                if ((string)temp_dgv.Rows[e.RowIndex].Cells["chbSusp"].Value == "true")
                { suspect = true; }
                else
                { suspect = false; }

                stockSQL = "INSERT INTO diag(exam_no, diag_code, suspect) "
                    + "VALUES(" + exam_id + "," + temp_dgv.Rows[e.RowIndex].Cells["no"].Value.ToString() + ","
                    + suspect.ToString() + ");";

                this.Close();
            }
        }

        private void bt1_Click(object sender, EventArgs e)
        { buttonFunction(1); }

        private void bt2_Click(object sender, EventArgs e)
        { buttonFunction(2); }

        private void bt3_Click(object sender, EventArgs e)
        { buttonFunction(3); }

        private void bt4_Click(object sender, EventArgs e)
        { buttonFunction(4); }

        private void bt5_Click(object sender, EventArgs e)
        { buttonFunction(5); }

        private void bt6_Click(object sender, EventArgs e)
        { buttonFunction(6); }

        private void bt7_Click(object sender, EventArgs e)
        { buttonFunction(7); }

        private void bt8_Click(object sender, EventArgs e)
        { buttonFunction(8); }

        private void bt9_Click(object sender, EventArgs e)
        { buttonFunction(9); }

        private void bt10_Click(object sender, EventArgs e)
        { buttonFunction(10); }

        private void bt11_Click(object sender, EventArgs e)
        { buttonFunction(11); }

        private void bt12_Click(object sender, EventArgs e)
        { buttonFunction(12); }
    }
}

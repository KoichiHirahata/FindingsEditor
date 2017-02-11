using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Npgsql;

namespace FindingsEdior
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
        public string premodifier { get; set; }
        public string postmodifier { get; set; }
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
            btSelect.Text = FindingsEditor.Properties.Resources.Select;  //ボタンの表示テキスト設定
            dgv.Columns.Add(btSelect);     //ボタン追加
            #endregion

            #region Initialize columns
            this.dgv.Columns[0].Visible = false;
            this.dgv.Columns[2].Visible = false;

            this.dgv.Columns[1].HeaderText = FindingsEditor.Properties.Resources.Diagnosis;
            this.dgv.Columns["chbSusp"].HeaderText = FindingsEditor.Properties.Resources.Suspect;
            this.dgv.Columns["btSelect"].HeaderText = "";
            #endregion

            setButtons(_examType);
            resizeColumns();

            switch (Settings.lang)
            {
                case "ja":
                    cbPremodifier.ImeMode = ImeMode.On;
                    cbPostmodifier.ImeMode = ImeMode.On;
                    break;
            }
        }

        #region Initialize Buttons
        private void setButtons(int _examType)
        {
            switch (_examType)
            {
                #region GF
                case 1:
                    setButtonLabels(1, false, 1000);
                    setButtonSettings(1, true, 1001);
                    setButtonSettings(2, false);
                    setButtonSettings(3, true, 200000);

                    setButtonLabels(2, false, 2000);
                    setButtonSettings(4, true, 2001);
                    setButtonSettings(5, true, 3000);
                    setButtonSettings(6, true, 4000);

                    setButtonLabels(3, false, 5000);
                    setButtonSettings(7, true, 5001);
                    setButtonSettings(8, true, 6000);
                    setButtonSettings(9, true, 7000);

                    setButtonLabels(4, true);
                    setButtonSettings(10, true, 10001);
                    setButtonSettings(11, true, 20000);
                    setButtonSettings(12, true, 100001);
                    break;
                #endregion

                #region CF
                case 2:
                    setButtonLabels(1, false, 30000);
                    setButtonSettings(1, true, 30001);
                    setButtonSettings(2, false);
                    setButtonSettings(3, false);

                    setButtonLabels(2, false, 40000);
                    setButtonSettings(4, true, 40001);
                    setButtonSettings(5, true, 50000);
                    setButtonSettings(6, true, 60000);

                    setButtonLabels(3, false, 70000);
                    setButtonSettings(7, true, 70001);
                    setButtonSettings(8, false);
                    setButtonSettings(9, false);

                    setButtonLabels(4, true);
                    setButtonSettings(10, true, 100001);
                    setButtonSettings(11, true, 200001);
                    setButtonSettings(12, false);
                    break;
                #endregion

                #region abdominal US
                case 1001:
                    setButtonLabels(1, false, 1000000);
                    setButtonSettings(1, true, 1010000);
                    setButtonSettings(2, true, 1020000);
                    setButtonSettings(3, true, 1030000);

                    setButtonLabels(2, true);
                    setButtonSettings(4, true, 1040000);
                    setButtonSettings(5, true, 1050000);
                    setButtonSettings(6, true, 1060000);

                    setButtonLabels(3, true);
                    bt7.Visible = false;
                    bt8.Visible = false;
                    bt9.Visible = false;

                    setButtonLabels(4, true);
                    bt10.Visible = false;
                    bt11.Visible = false;
                    bt12.Visible = false;
                    break;
                #endregion

                #region default
                default:
                    setButtonLabels(1, true);
                    setButtonSettings(1, false);
                    setButtonSettings(2, false);
                    setButtonSettings(3, false);

                    setButtonLabels(2, true);
                    setButtonSettings(4, false);
                    setButtonSettings(5, false);
                    setButtonSettings(6, false);

                    setButtonLabels(3, true);
                    setButtonSettings(7, false);
                    setButtonSettings(8, false);
                    setButtonSettings(9, false);

                    setButtonLabels(4, true);
                    setButtonSettings(10, false);
                    setButtonSettings(11, false);
                    setButtonSettings(12, false);
                    break;
                #endregion
            }
        }

        private void setButtonLabels(int labelNumber, bool isBlank, int idNumber = 0)
        {
            DataRow[] drs;
            string labelText = "";

            if (!isBlank && idNumber != 0)
            {
                drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + idNumber.ToString());
                if (drs.Length != 0)
                { labelText = drs[0]["name"].ToString(); }
            }

            switch (labelNumber)
            {
                case 1:
                    lb1.Text = labelText;
                    break;
                case 2:
                    lb2.Text = labelText;
                    break;
                case 3:
                    lb3.Text = labelText;
                    break;
                case 4:
                    lb4.Text = labelText;
                    break;
            }
        }

        private void setButtonSettings(int buttonNumber, bool isVisible, int idNumber = 0)
        {
            DataRow[] drs;
            string buttonText = "";
            int buttonStart = -1;
            int buttonEnd = -1;

            if (isVisible && idNumber != 0)
            {
                drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + idNumber.ToString());
                if (drs.Length != 0)
                {
                    buttonText = drs[0]["name"].ToString();
                    buttonStart = int.Parse(drs[0]["start_no"].ToString());
                    buttonEnd = int.Parse(drs[0]["end_no"].ToString());
                }
            }

            switch (buttonNumber)
            {
                case 1:
                    bt1.Visible = isVisible;
                    bt1.Text = buttonText;
                    bt1start = buttonStart;
                    bt1end = buttonEnd;
                    break;
                case 2:
                    bt2.Visible = isVisible;
                    bt2.Text = buttonText;
                    bt2start = buttonStart;
                    bt2end = buttonEnd;
                    break;
                case 3:
                    bt3.Visible = isVisible;
                    bt3.Text = buttonText;
                    bt3start = buttonStart;
                    bt3end = buttonEnd;
                    break;
                case 4:
                    bt4.Visible = isVisible;
                    bt4.Text = buttonText;
                    bt4start = buttonStart;
                    bt4end = buttonEnd;
                    break;
                case 5:
                    bt5.Visible = isVisible;
                    bt5.Text = buttonText;
                    bt5start = buttonStart;
                    bt5end = buttonEnd;
                    break;
                case 6:
                    bt6.Visible = isVisible;
                    bt6.Text = buttonText;
                    bt6start = buttonStart;
                    bt6end = buttonEnd;
                    break;
                case 7:
                    bt7.Visible = isVisible;
                    bt7.Text = buttonText;
                    bt7start = buttonStart;
                    bt7end = buttonEnd;
                    break;
                case 8:
                    bt8.Visible = isVisible;
                    bt8.Text = buttonText;
                    bt8start = buttonStart;
                    bt8end = buttonEnd;
                    break;
                case 9:
                    bt9.Visible = isVisible;
                    bt9.Text = buttonText;
                    bt9start = buttonStart;
                    bt9end = buttonEnd;
                    break;
                case 10:
                    bt10.Visible = isVisible;
                    bt10.Text = buttonText;
                    bt10start = buttonStart;
                    bt10end = buttonEnd;
                    break;
                case 11:
                    bt11.Visible = isVisible;
                    bt11.Text = buttonText;
                    bt11start = buttonStart;
                    bt11end = buttonEnd;
                    break;
                case 12:
                    bt12.Visible = isVisible;
                    bt12.Text = buttonText;
                    bt12start = buttonStart;
                    bt12end = buttonEnd;
                    break;
            }
        }
        #endregion

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
                    setPremodifier(bt1start, bt1end);
                    setPostmodifier(bt1start, bt1end);
                    break;
                case 2:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt2start + ")AND(no<=" + bt2end + ")";
                    setPremodifier(bt2start, bt2end);
                    setPostmodifier(bt2start, bt2end);
                    break;
                case 3:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt3start + ")AND(no<=" + bt3end + ")";
                    setPremodifier(bt3start, bt3end);
                    setPostmodifier(bt3start, bt3end);
                    break;
                case 4:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt4start + ")AND(no<=" + bt4end + ")";
                    setPremodifier(bt4start, bt4end);
                    setPostmodifier(bt4start, bt4end);
                    break;
                case 5:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt5start + ")AND(no<=" + bt5end + ")";
                    setPremodifier(bt5start, bt5end);
                    setPostmodifier(bt5start, bt5end);
                    break;
                case 6:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt6start + ")AND(no<=" + bt6end + ")";
                    setPremodifier(bt6start, bt6end);
                    setPostmodifier(bt6start, bt6end);
                    break;
                case 7:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt7start + ")AND(no<=" + bt7end + ")";
                    setPremodifier(bt7start, bt7end);
                    setPostmodifier(bt7start, bt7end);
                    break;
                case 8:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt8start + ")AND(no<=" + bt8end + ")";
                    setPremodifier(bt8start, bt8end);
                    setPostmodifier(bt8start, bt8end);
                    break;
                case 9:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt9start + ")AND(no<=" + bt9end + ")";
                    setPremodifier(bt9start, bt9end);
                    setPostmodifier(bt9start, bt9end);
                    break;
                case 10:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt10start + ")AND(no<=" + bt10end + ")";
                    setPremodifier(bt10start, bt10end);
                    setPostmodifier(bt10start, bt10end);
                    break;
                case 11:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt11start + ")AND(no<=" + bt11end + ")";
                    setPremodifier(bt11start, bt11end);
                    setPostmodifier(bt11start, bt11end);
                    break;
                case 12:
                    CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter = "(no>=" + bt12start + ")AND(no<=" + bt12end + ")";
                    setPremodifier(bt12start, bt12end);
                    setPostmodifier(bt12start, bt12end);
                    break;
            }
        }

        private void setPremodifier(int start, int end)
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

            try
            { conn.Open(); }
            catch (NpgsqlException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.CouldntOpenConn, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.ConnClosed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
            #endregion

            string sql;

            sql = "SELECT premodifier AS key, premodifier FROM diag WHERE premodifier <> '' AND diag_code >= " + start.ToString()
                + " AND diag_code <= " + end.ToString() + " GROUP BY premodifier ORDER BY count(premodifier) DESC";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            
            DataRow newRow = dt.NewRow();
            newRow["key"] = "";
            newRow["premodifier"] = "";
            dt.Rows.InsertAt(newRow, 0);
            cbPremodifier.DataSource = dt;
            cbPremodifier.DisplayMember = "premodifier";
            cbPremodifier.ValueMember = "key";
        }

        private void setPostmodifier(int start, int end)
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

            try
            { conn.Open(); }
            catch (NpgsqlException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.CouldntOpenConn, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.ConnClosed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
            #endregion

            string sql;

            sql = "SELECT postmodifier AS key, postmodifier FROM diag WHERE postmodifier <> '' AND diag_code >= " + start.ToString()
                + " AND diag_code <= " + end.ToString() + " GROUP BY postmodifier ORDER BY count(postmodifier) DESC";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();

            DataRow newRow = dt.NewRow();
            newRow["key"] = "";
            newRow["postmodifier"] = "";
            dt.Rows.InsertAt(newRow, 0);
            cbPostmodifier.DataSource = dt;
            cbPostmodifier.DisplayMember = "postmodifier";
            cbPostmodifier.ValueMember = "key";
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView temp_dgv = (DataGridView)sender;

            if (temp_dgv.Columns[e.ColumnIndex].Name == "btSelect")
            {
                #region Error check
                if (cbPremodifier.Text.Length > 255)
                {
                    MessageBox.Show(FindingsEditor.Properties.Resources.Location2Long, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                add_diag = true;

                diag_code = (int)temp_dgv.Rows[e.RowIndex].Cells["no"].Value;
                if ((string)temp_dgv.Rows[e.RowIndex].Cells["chbSusp"].Value == "true")
                { suspect = true; }
                else
                { suspect = false; }

                if (cbPremodifier.Text.Length == 0)
                { premodifier = ""; }
                else
                { premodifier = cbPremodifier.Text; }

                if (cbPostmodifier.Text.Length == 0)
                { postmodifier = ""; }
                else
                { postmodifier = cbPostmodifier.Text; }

                stockSQL = "INSERT INTO diag(exam_no, diag_code, suspect, premodifier, postmodifier) "
                    + "VALUES(" + exam_id + "," + temp_dgv.Rows[e.RowIndex].Cells["no"].Value.ToString() + ","
                    + suspect.ToString() + ",'" + premodifier + "','" + postmodifier + "');";

                this.Close();
            }
        }

        #region Functions for isValidRangeOfDiagnoses()
        public static bool isValidStringOfTheRangeOfDiagnoses(string rangeStr)
        {
            if (String.IsNullOrWhiteSpace(rangeStr))
            { return false; }

            int topLocate = rangeStr.IndexOf("(no>=");
            int midLocate = rangeStr.IndexOf(")AND(no<=");

            if (topLocate != 0)
            { return false; }

            if (midLocate == 5)
            { return false; }

            if (topLocate == -1 || midLocate == -1)
            { return false; }

            return true;
        }

        public static int getStartOfTheRange(string rangeStr)
        {
            int midLocate = rangeStr.IndexOf(")AND(no<=");
            return int.Parse(rangeStr.Substring(5, midLocate - 5));
        }

        public static int getEndOfTheRange(string rangeStr)
        {
            int midLocate = rangeStr.IndexOf(")AND(no<=");
            return int.Parse(rangeStr.Substring(midLocate + 9, rangeStr.Length - midLocate - 10));
        }
        #endregion

        private bool isValidRangeOfDiagnoses(string rangeStr)
        {
            if (!isValidStringOfTheRangeOfDiagnoses(rangeStr))
            { return false; }

            int topLocate = rangeStr.IndexOf("(no>=");
            int midLocate = rangeStr.IndexOf(")AND(no<=");

            int startOfTheRange = getStartOfTheRange(rangeStr);
            int endOfTheRange = getEndOfTheRange(rangeStr);

            List<int> startsOfButtons = new List<int>();
            startsOfButtons.Add(bt1start);
            startsOfButtons.Add(bt2start);
            startsOfButtons.Add(bt3start);
            startsOfButtons.Add(bt4start);
            startsOfButtons.Add(bt5start);
            startsOfButtons.Add(bt6start);
            startsOfButtons.Add(bt7start);
            startsOfButtons.Add(bt8start);
            startsOfButtons.Add(bt9start);
            startsOfButtons.Add(bt10start);
            startsOfButtons.Add(bt11start);
            startsOfButtons.Add(bt12start);

            startsOfButtons.RemoveAll(s => s == -1);
            if (startsOfButtons.Min() > startOfTheRange || startsOfButtons.Count() == 0)
            { return false; }

            List<int> endsOfButtons = new List<int>();
            endsOfButtons.Add(bt1end);
            endsOfButtons.Add(bt2end);
            endsOfButtons.Add(bt3end);
            endsOfButtons.Add(bt4end);
            endsOfButtons.Add(bt5end);
            endsOfButtons.Add(bt6end);
            endsOfButtons.Add(bt7end);
            endsOfButtons.Add(bt8end);
            endsOfButtons.Add(bt9end);
            endsOfButtons.Add(bt10end);
            endsOfButtons.Add(bt11end);
            endsOfButtons.Add(bt12end);

            endsOfButtons.RemoveAll(e => e == -1);
            if (endsOfButtons.Max() < endOfTheRange || endsOfButtons.Count() == 0)
            { return false; }

            return true;
        }

        private void bt1_Click(object sender, EventArgs e)
        {
            buttonFunction(1);
            cbPremodifier.Focus();
        }

        private void bt2_Click(object sender, EventArgs e)
        {
            buttonFunction(2);
            cbPremodifier.Focus();
        }

        private void bt3_Click(object sender, EventArgs e)
        {
            buttonFunction(3);
            cbPremodifier.Focus();
        }

        private void bt4_Click(object sender, EventArgs e)
        {
            buttonFunction(4);
            cbPremodifier.Focus();
        }

        private void bt5_Click(object sender, EventArgs e)
        {
            buttonFunction(5);
            cbPremodifier.Focus();
        }

        private void bt6_Click(object sender, EventArgs e)
        {
            buttonFunction(6);
            cbPremodifier.Focus();
        }

        private void bt7_Click(object sender, EventArgs e)
        {
            buttonFunction(7);
            cbPremodifier.Focus();
        }

        private void bt8_Click(object sender, EventArgs e)
        {
            buttonFunction(8);
            cbPremodifier.Focus();
        }

        private void bt9_Click(object sender, EventArgs e)
        {
            buttonFunction(9);
            cbPremodifier.Focus();
        }

        private void bt10_Click(object sender, EventArgs e)
        {
            buttonFunction(10);
            cbPremodifier.Focus();
        }

        private void bt11_Click(object sender, EventArgs e)
        {
            buttonFunction(11);
            cbPremodifier.Focus();
        }

        private void bt12_Click(object sender, EventArgs e)
        {
            buttonFunction(12);
            cbPremodifier.Focus();
        }

        private void AddDiagnosis_Shown(object sender, EventArgs e)
        {
            if (!isValidRangeOfDiagnoses(CLocalDB.localDB.Tables["diag_name"].DefaultView.RowFilter))
            { buttonFunction(1); }
        }
    }
}

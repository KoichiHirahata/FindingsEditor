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

namespace FindingsEdior
{
    public partial class SearchPt : Form
    {
        public SearchPt()
        {
            InitializeComponent();
            dgvPatientList.RowHeadersVisible = false;
            dgvPatientList.MultiSelect = false;
            dgvPatientList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPatientList.Font = new Font(dgvPatientList.Font.Name, 12);
            dgvPatientList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void btSearchPt_Click(object sender, EventArgs e)
        { searchPt(); }

        private void tbSearchString_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            { searchPt(); }
        }

        private void searchPt()
        {
            #region Npgsql
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

            string sql = "SELECT pt_id, pt_name, birthday FROM patient WHERE pt_name like '%" + tbSearchString.Text + "%'";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataSet ds = new DataSet("t_patient");
            da.Fill(ds, "t_patient");
            if (ds.Tables["t_patient"].Rows.Count == 0)
            { MessageBox.Show(Properties.Resources.NoPatient, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else
            {
                this.dgvPatientList.Columns.Clear();    //これしとかないと検索するたびにボタンが増え続ける。
                this.dgvPatientList.DataSource = ds.Tables["t_patient"];

                //列名のテキストを変更する
                this.dgvPatientList.Columns[0].HeaderText = "ID";
                this.dgvPatientList.Columns[1].HeaderText = Properties.Resources.ptName;
                this.dgvPatientList.Columns[2].HeaderText = Properties.Resources.Birthday;

                DataGridViewButtonColumn btColumn = new DataGridViewButtonColumn(); //DataGridViewButtonColumnの作成
                btColumn.Name = Properties.Resources.ptSelect;  //列の名前を設定
                btColumn.UseColumnTextForButtonValue = true;    //ボタンにテキスト表示
                btColumn.Text = Properties.Resources.ptSelect;  //ボタンの表示テキスト設定
                dgvPatientList.Columns.Add(btColumn);           //ボタン追加

                this.dgvPatientList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;  //列幅自動調整
                this.dgvPatientList.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void dgvPatientList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Point p = dgvPatientList.PointToClient(Cursor.Position);
            DataGridView.HitTestInfo hti = dgvPatientList.HitTest(p.X, p.Y);  // 取得したポイントからHitTestでセル位置取得

            if (hti.Type == DataGridViewHitTestType.Cell)
            {
                DataRowView drv = dgvPatientList.SelectedRows[0].DataBoundItem as DataRowView;
                DataRow dr = drv.Row;
                PatientMain pm = new PatientMain(dr[0].ToString());
                pm.Show();
                this.Close();
            }
        }

        private void dgvPatientList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            Point p = dgvPatientList.PointToClient(Cursor.Position);
            DataGridView.HitTestInfo hti = dgvPatientList.HitTest(p.X, p.Y);  // 取得したポイントからHitTestでセル位置取得

            if ((hti.Type == DataGridViewHitTestType.Cell) && (dgv.Columns[e.ColumnIndex].Name == Properties.Resources.ptSelect))
            {
                DataRowView drv = dgvPatientList.SelectedRows[0].DataBoundItem as DataRowView;
                DataRow dr = drv.Row;
                PatientMain pm = new PatientMain(dr[0].ToString());
                pm.Show();
                this.Close();
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        { this.Close(); }
    }
}

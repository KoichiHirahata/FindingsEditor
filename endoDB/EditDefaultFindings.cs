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
    public partial class EditDefaultFindings : Form
    {
        private DataTable dt = new DataTable();
        private string sql = "SELECT exam_type, findings FROM default_findings ORDER BY exam_type"; //●●●移植時要変更

        public EditDefaultFindings()
        {
            InitializeComponent();
            //this.dgv.RowHeadersVisible = false;
            this.dgv.MultiSelect = false;
            this.dgv.Font = new Font(dgv.Font.Name, 12);
            this.dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv.DataSource = dt;

            showList();

            //●●●移植時要変更
            dgv.Columns["exam_type"].Visible = false;
            dgv.Columns["findings"].HeaderText = Properties.Resources.Findings;
            dgv.Columns["findings"].DataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            DataGridViewComboBoxColumn eTypeColumn = new DataGridViewComboBoxColumn();
            eTypeColumn.DataPropertyName = "exam_type";    //バインドされているデータを表示する
            eTypeColumn.DataSource = CLocalDB.localDB.Tables["all_exam_type"];
            eTypeColumn.ValueMember = "type_no";
            eTypeColumn.DisplayMember = "exam_name";
            eTypeColumn.HeaderText = Properties.Resources.ExamType;
            this.dgv.Columns.Add(eTypeColumn);

            this.dgv.Columns[1].DisplayIndex = 1;
            this.dgv.Columns[2].DisplayIndex = 0;
            dgv.Columns[2].ReadOnly = true;

            resizeColumns();
        }

        private void showList()
        {
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
            try
            {
                conn.Open();
            }
            catch (NpgsqlException)
            {
                MessageBox.Show(Properties.Resources.CouldntOpenConn, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(Properties.Resources.ConnClosed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return;
            }

            //
            //ここから下がデータの読み込み部分。
            //

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show(Properties.Resources.NoData, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.dgv.Refresh();
            }
            conn.Close();
        }

        protected void resizeColumns()
        {
            foreach (DataGridViewColumn dc in dgv.Columns)
            {
                dc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void saveDataTable() //●●●移植時要変更
        {
            DataTable dt2 = dt.GetChanges();
            if (dt2 == null)
                return;

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
            try
            {
                conn.Open();
            }
            catch (NpgsqlException)
            {
                MessageBox.Show(Properties.Resources.CouldntOpenConn, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(Properties.Resources.ConnClosed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return;
            }

            //
            //ここから下がデータの書き込み部分。
            //
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);

            //UpdateCommand
            da.UpdateCommand = new NpgsqlCommand("UPDATE default_findings SET findings = :ub"
                + " WHERE exam_type = :UKey", conn);

            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("UKey", DbType.Int16));
            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("ub", DbType.String));

            da.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
            da.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;

            da.UpdateCommand.Parameters[0].SourceColumn = "exam_type";
            da.UpdateCommand.Parameters[1].SourceColumn = "findings";

            da.Update(dt2);
            conn.Close();
        }

        private enum Duplication { NotDuplicated, Duplicated, Error }

        private Duplication checkDuplicate(string checkKey)
        {
            int i = 0;
            foreach (DataGridViewRow dr in this.dgv.Rows)
            {
                if (!dr.IsNewRow)
                {
                    //MessageBox.Show(dr.Cells[1].Value.ToString() + ", CheckKey=" + checkKey.ToString()+ ", i=" + i.ToString());
                    if (dr.Cells["exam_type"].Value.ToString() == checkKey)
                    {
                        i += 1;
                    }
                }
            }

            if (i == 0)
            {
                MessageBox.Show("[" + checkKey + "]" + Properties.Resources.SoftwareError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Duplication.Error;
            }
            else if (i > 1)
            {
                MessageBox.Show("[" + Properties.Resources.Number + "]" + Properties.Resources.Duplicated, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //●●●移植時要変更
                return Duplication.Duplicated;
            }
            else
            {
                return Duplication.NotDuplicated;
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            this.Validate(); //Without this code, new data will disappear.
            DataTable dt2 = dt.GetChanges();
            if (dt2 != null)
            {
                //int d;  //For TryParse.
                foreach (DataRow dr in dt2.Rows)
                {
                    if (string.IsNullOrWhiteSpace(dr[0].ToString()) == true)
                    {
                        MessageBox.Show("[" + Properties.Resources.Number + "]" + Properties.Resources.BlankNotAllowed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //●●●移植時要変更
                        return;
                    }

                    if (checkDuplicate(dr[0].ToString()) == Duplication.Duplicated)
                    {
                        return;
                    }
                    else if (checkDuplicate(dr[0].ToString()) == Duplication.Error)
                    {
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(dr[1].ToString()) == true)
                    {
                        MessageBox.Show("[" + Properties.Resources.ObjectName + "]" + Properties.Resources.BlankNotAllowed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //●●●移植時要変更
                        return;
                    }
                }
                saveDataTable();
                this.Close();
            }
        }
    }
}
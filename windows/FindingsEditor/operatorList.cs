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
    public partial class operatorList : Form
    {
        private DataTable opList = new DataTable();

        public operatorList()
        {
            InitializeComponent();
            this.dgvOperatorList.RowHeadersVisible = false;
            this.dgvOperatorList.MultiSelect = false;
            this.dgvOperatorList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvOperatorList.Font = new Font(dgvOperatorList.Font.Name, 12);
            this.dgvOperatorList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvOperatorList.DataSource = opList;

            showOperatorList();

            //列名のテキストを変更する
            this.dgvOperatorList.Columns[0].HeaderText = "ID";
            this.dgvOperatorList.Columns[1].HeaderText = FindingsEditor.Properties.Resources.Name;
            this.dgvOperatorList.Columns[2].HeaderText = FindingsEditor.Properties.Resources.Department;
            this.dgvOperatorList.Columns[3].HeaderText = FindingsEditor.Properties.Resources.Category;
            this.dgvOperatorList.Columns[3].Visible = false;
            this.dgvOperatorList.Columns[4].HeaderText = FindingsEditor.Properties.Resources.allow_fc;
            this.dgvOperatorList.Columns[5].HeaderText = FindingsEditor.Properties.Resources.admin;
            this.dgvOperatorList.Columns[6].HeaderText = FindingsEditor.Properties.Resources.Visible;

            DataGridViewComboBoxColumn opCategory = new DataGridViewComboBoxColumn();
            opCategory.DataPropertyName = "op_category";    //バインドされているデータを表示する
            opCategory.DataSource = CLocalDB.localDB.Tables["op_category"];
            opCategory.ValueMember = "opc_no";
            opCategory.DisplayMember = "opc_name";
            opCategory.HeaderText = FindingsEditor.Properties.Resources.Category;
            this.dgvOperatorList.Columns.Add(opCategory);

            DataGridViewButtonColumn btColumn = new DataGridViewButtonColumn(); //DataGridViewButtonColumnの作成
            btColumn.Name = FindingsEditor.Properties.Resources.Edit;  //列の名前を設定
            btColumn.UseColumnTextForButtonValue = true;    //ボタンにテキスト表示
            btColumn.Text = FindingsEditor.Properties.Resources.Edit;  //ボタンの表示テキスト設定
            dgvOperatorList.Columns.Add(btColumn);          //ボタン追加

            DataGridViewButtonColumn btDelColumn = new DataGridViewButtonColumn(); //DataGridViewButtonColumnの作成
            btDelColumn.Name = FindingsEditor.Properties.Resources.Delete;  //列の名前を設定
            btDelColumn.UseColumnTextForButtonValue = true;  //ボタンにテキスト表示
            btDelColumn.Text = FindingsEditor.Properties.Resources.Delete;  //ボタンの表示テキスト設定
            dgvOperatorList.Columns.Add(btDelColumn);     //ボタン追加

            dgvOperatorList.Columns[0].DisplayIndex = 0;
            dgvOperatorList.Columns[1].DisplayIndex = 1;
            dgvOperatorList.Columns[2].DisplayIndex = 2;
            opCategory.DisplayIndex = 3;
            dgvOperatorList.Columns[4].DisplayIndex = 4;
            dgvOperatorList.Columns[5].DisplayIndex = 5;
            dgvOperatorList.Columns[6].DisplayIndex = 6;
            btColumn.DisplayIndex = 7;
            btDelColumn.DisplayIndex = 8;

            resizeColumns();
        }

        private void showOperatorList()
        {
            #region Npgsql connection
            NpgsqlConnection conn;

            try
            {
                conn = new NpgsqlConnection(Settings.retConnStr());
            }
            catch (ArgumentException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.WrongConnectingString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            { conn.Open(); }
            catch (NpgsqlException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.CouldntOpenConn, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.ConnClosed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return;
            }
            #endregion

            //ここから下がデータの読み込み部分。
            string sql = "SELECT operator_id, op_name, department, op_category, allow_fc, admin_op, op_visible FROM operator ORDER BY op_order";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(opList);
            if (opList.Rows.Count == 0)
            { MessageBox.Show(FindingsEditor.Properties.Resources.NoOperator, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else
            { this.dgvOperatorList.Refresh(); }
            conn.Close();
        }

        private void resizeColumns()
        {
            foreach (DataGridViewColumn dc in dgvOperatorList.Columns)
            { dc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; }
        }

        private void btClose_Click(object sender, EventArgs e)
        { this.Close(); }

        private void dgvOperatorList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            //Open EditOperator form
            if (dgv.Columns[e.ColumnIndex].Name == FindingsEditor.Properties.Resources.Edit)
            {
                if (!uckyFunctions.canEdit("operator", "operator_id", "LIKE", "'" + dgv.Rows[e.RowIndex].Cells[2].Value.ToString() + "'"))
                {
                    MessageBox.Show(FindingsEditor.Properties.Resources.PermissionDenied, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                EditOperator eo = new EditOperator(false, dgv.Rows[e.RowIndex].Cells[2].Value.ToString());
                eo.ShowDialog(this);
                opList.Rows.Clear();
                showOperatorList();
                resizeColumns();
                return;
            }

            //Delete operator
            if (dgv.Columns[e.ColumnIndex].Name == FindingsEditor.Properties.Resources.Delete)
            {
                examOperator examOp = new examOperator(dgv.Rows[e.RowIndex].Cells[2].Value.ToString(), false);
                switch (examOp.examCount())
                {
                    case -1:
                        MessageBox.Show(FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 0:
                        if (MessageBox.Show(FindingsEditor.Properties.Resources.ConfirmDel, "Information", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            examOp.deleteExamOp();
                            opList.Rows.Clear();
                            showOperatorList();
                            resizeColumns();
                            break;
                        }
                        else
                        {
                            break;
                        }
                    default:
                        MessageBox.Show(FindingsEditor.Properties.Resources.CouldntDelOp, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
        }

        private void btAddOperator_Click(object sender, EventArgs e)
        {
            EditOperator eo = new EditOperator(true, null);
            eo.ShowDialog(this);
            opList.Rows.Clear();
            showOperatorList();
            resizeColumns();
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            int orderNumber = dgvOperatorList.SelectedRows[0].Index;
            switch (orderNumber)
            {
                case 0:
                    break;
                default:
                    examOperator.saveOrder(dgvOperatorList.SelectedRows[0].Cells[2].Value.ToString(), (short)(orderNumber - 1));
                    examOperator.saveOrder(dgvOperatorList.Rows[orderNumber - 1].Cells[2].Value.ToString(), (short)(orderNumber));
                    opList.Rows.Clear();
                    showOperatorList();
                    dgvOperatorList.CurrentCell = dgvOperatorList.Rows[orderNumber - 1].Cells[2]; //remain selected.
                    break;
            }
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            int orderNumber = dgvOperatorList.SelectedRows[0].Index;

            if (orderNumber < (opList.Rows.Count - 1))
            {
                examOperator.saveOrder(dgvOperatorList.SelectedRows[0].Cells[2].Value.ToString(), (short)(orderNumber + 1));
                examOperator.saveOrder(dgvOperatorList.Rows[orderNumber + 1].Cells[2].Value.ToString(), (short)(orderNumber));
                opList.Rows.Clear();
                showOperatorList();
                dgvOperatorList.CurrentCell = dgvOperatorList.Rows[orderNumber + 1].Cells[2]; //remain selected.
            }
        }
    }
}

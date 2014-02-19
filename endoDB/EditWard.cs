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
    public partial class EditWard : EditFormBase
    {
        private string sql = "SELECT ward_no, ward, ward_order, ward_visible FROM ward ORDER BY ward_no"; //●●●移植時要変更

        public EditWard()
        {
            InitializeComponent();
            this.Width = 500;

            showList();

            //●●●移植時要変更
            this.dgv.Columns[0].HeaderText = Properties.Resources.Number;
            this.dgv.Columns[1].HeaderText = Properties.Resources.ObjectName;
            this.dgv.Columns[2].HeaderText = Properties.Resources.Order;
            this.dgv.Columns[3].HeaderText = Properties.Resources.Visible;

            DataGridViewButtonColumn btDelColumn = new DataGridViewButtonColumn(); //DataGridViewButtonColumnの作成
            btDelColumn.Name = Properties.Resources.Delete;  //列の名前を設定
            btDelColumn.UseColumnTextForButtonValue = true;  //ボタンにテキスト表示
            btDelColumn.Text = Properties.Resources.Delete;  //ボタンの表示テキスト設定
            dgv.Columns.Add(btDelColumn);     //ボタン追加

            resizeColumns();
            setKeyReadOnly();
        }

        private void showList()
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

        protected override void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView temp_dgv = (DataGridView)sender;

            //Delete row
            if (temp_dgv.Columns[e.ColumnIndex].Name == Properties.Resources.Delete)
            {
                this.Validate(); //Without this code, new data will disappear.
                saveDataTable(); //Without this code, deleting new record will call error;

                //If object was used, refuse delete.
                int i = uckyFunctions.CountTimes("exam", "ward_id", temp_dgv.Rows[e.RowIndex].Cells[1].Value.ToString(), "string"); //●●●移植時要変更
                if (i > 0)
                {
                    MessageBox.Show(Properties.Resources.CouldntDelData, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (i == -1)
                {
                    MessageBox.Show("[dgv_CellContentClick]Error was occured.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (!temp_dgv.Rows[e.RowIndex].IsNewRow)
                    {
                        if (MessageBox.Show("[" + temp_dgv.Rows[e.RowIndex].Cells[1].Value.ToString() + "]" + Properties.Resources.ConfirmDel, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            temp_dgv.Rows.Remove(temp_dgv.Rows[e.RowIndex]);
                            saveDataTable();
                            dt.Rows.Clear();
                            showList();
                            resizeColumns();
                        }
                    }
                }
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
                    Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;");
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

            //InsertCommand
            da.InsertCommand = new NpgsqlCommand("INSERT INTO ward(ward_no, ward, ward_order, ward_visible) " +
                        " values (:a, :b, :c, :d)", conn);

            da.InsertCommand.Parameters.Add(new NpgsqlParameter("a", DbType.Int16));
            da.InsertCommand.Parameters.Add(new NpgsqlParameter("b", DbType.String));
            da.InsertCommand.Parameters.Add(new NpgsqlParameter("c", DbType.Int16));
            da.InsertCommand.Parameters.Add(new NpgsqlParameter("d", DbType.Boolean));

            da.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
            da.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
            da.InsertCommand.Parameters[2].Direction = ParameterDirection.Input;
            da.InsertCommand.Parameters[3].Direction = ParameterDirection.Input;

            da.InsertCommand.Parameters[0].SourceColumn = "ward_no";
            da.InsertCommand.Parameters[1].SourceColumn = "ward";
            da.InsertCommand.Parameters[2].SourceColumn = "ward_order";
            da.InsertCommand.Parameters[3].SourceColumn = "ward_visible";

            //UpdateCommand
            da.UpdateCommand = new NpgsqlCommand("UPDATE ward SET ward = :ub, ward_order = :uc,"
                + " ward_visible = :ud"
                + " WHERE ward_no = :UKey", conn);

            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("UKey", DbType.String));
            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("ub", DbType.String));
            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("uc", DbType.Int16));
            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("ud", DbType.Boolean));

            da.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
            da.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
            da.UpdateCommand.Parameters[2].Direction = ParameterDirection.Input;
            da.UpdateCommand.Parameters[3].Direction = ParameterDirection.Input;

            da.UpdateCommand.Parameters[0].SourceColumn = "ward_no";
            da.UpdateCommand.Parameters[1].SourceColumn = "ward";
            da.UpdateCommand.Parameters[2].SourceColumn = "ward_order";
            da.UpdateCommand.Parameters[3].SourceColumn = "ward_visible";

            //DeleteCommand
            da.DeleteCommand = new NpgsqlCommand("DELETE FROM ward WHERE ward_no = :DelKey", conn);
            da.DeleteCommand.Parameters.Add(new NpgsqlParameter("DelKey", DbType.String));
            da.DeleteCommand.Parameters[0].SourceColumn = "ward_no";

            da.Update(dt2);
            conn.Close();
        }

        private void EditPlace_FormClosing(object sender, FormClosingEventArgs e)
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
                        e.Cancel = true;
                        return;
                    }

                    if (checkDuplicate(dr[0].ToString()) == Duplication.Duplicated)
                    {
                        e.Cancel = true;
                        return;
                    }
                    else if (checkDuplicate(dr[0].ToString()) == Duplication.Error)
                    {
                        e.Cancel = true;
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(dr[1].ToString()) == true)
                    {
                        MessageBox.Show("[" + Properties.Resources.ObjectName + "]" + Properties.Resources.BlankNotAllowed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //●●●移植時要変更
                        e.Cancel = true;
                        return;
                    }
                }
                saveDataTable();
            }
        }

        protected override void dgv_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView temp_dgv = (DataGridView)sender;
            if (temp_dgv.Columns[e.ColumnIndex].Name == "ward_no") //●●●移植時要変更
            {
                if (!temp_dgv.Rows[e.RowIndex].IsNewRow)
                {
                    checkDuplicate(temp_dgv.Rows[e.RowIndex].Cells[1].Value.ToString());
                }
            }
        }

        private Duplication checkDuplicate(string checkKey)
        {
            int i = 0;
            foreach (DataGridViewRow dr in this.dgv.Rows)
            {
                if (!dr.IsNewRow)
                {
                    //MessageBox.Show(dr.Cells[1].Value.ToString() + ", CheckKey=" + checkKey.ToString()+ ", i=" + i.ToString());
                    if (dr.Cells[1].Value.ToString() == checkKey)
                    {
                        i += 1;
                    }
                }
            }

            if (i == 0)
            {
                MessageBox.Show(Properties.Resources.SoftwareError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}

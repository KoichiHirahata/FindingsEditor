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
    public partial class EditEquipments : EditFormBase
    {
        private string sql = "SELECT equipment_no, name, equipment_type, gf_order, cf_order, sv_order, s_order, us_order, scope, us, equipment_visible FROM equipment ORDER BY equipment_no"; //●●●移植時要変更

        public EditEquipments()
        {
            InitializeComponent();
            this.Width = 950;

            showList();

            //●●●移植時要変更
            this.dgv.Columns[0].HeaderText = FindingsEditor.Properties.Resources.Number;
            this.dgv.Columns[1].HeaderText = FindingsEditor.Properties.Resources.ObjectName;
            this.dgv.Columns[2].HeaderText = FindingsEditor.Properties.Resources.Category;
            this.dgv.Columns[2].Visible = false;
            this.dgv.Columns[3].HeaderText = FindingsEditor.Properties.Resources.Order + " (GF)";
            this.dgv.Columns[4].HeaderText = FindingsEditor.Properties.Resources.Order + " (CF)";
            this.dgv.Columns[5].HeaderText = FindingsEditor.Properties.Resources.Order + " (" + FindingsEditor.Properties.Resources.SideView + ")";
            this.dgv.Columns[6].HeaderText = FindingsEditor.Properties.Resources.Order + " (" + FindingsEditor.Properties.Resources.SmallBowel + ")";
            this.dgv.Columns[7].HeaderText = FindingsEditor.Properties.Resources.Order + " (US)";
            this.dgv.Columns[8].HeaderText = "Scope";
            this.dgv.Columns[9].HeaderText = "US";
            this.dgv.Columns[10].HeaderText = FindingsEditor.Properties.Resources.Visible;

            DataGridViewComboBoxColumn eTypeColumn = new DataGridViewComboBoxColumn();
            eTypeColumn.DataPropertyName = "equipment_type";    //バインドされているデータを表示する
            eTypeColumn.DataSource =　CLocalDB.localDB.Tables["equipment_type"];
            eTypeColumn.ValueMember = "type_no";
            eTypeColumn.DisplayMember = "name";
            eTypeColumn.HeaderText = FindingsEditor.Properties.Resources.Category;
            this.dgv.Columns.Add(eTypeColumn);

            DataGridViewButtonColumn btDelColumn = new DataGridViewButtonColumn(); //DataGridViewButtonColumnの作成
            btDelColumn.Name = FindingsEditor.Properties.Resources.Delete;  //列の名前を設定
            btDelColumn.UseColumnTextForButtonValue = true;  //ボタンにテキスト表示
            btDelColumn.Text = FindingsEditor.Properties.Resources.Delete;  //ボタンの表示テキスト設定
            dgv.Columns.Add(btDelColumn);     //ボタン追加

            this.dgv.Columns[0].DisplayIndex = 0;
            this.dgv.Columns[1].DisplayIndex = 1;
            eTypeColumn.DisplayIndex = 2;
            this.dgv.Columns[3].DisplayIndex = 3;
            this.dgv.Columns[4].DisplayIndex = 4;
            this.dgv.Columns[5].DisplayIndex = 5;
            this.dgv.Columns[6].DisplayIndex = 6;
            this.dgv.Columns[7].DisplayIndex = 7;
            this.dgv.Columns[8].DisplayIndex = 8;
            this.dgv.Columns[9].DisplayIndex = 9;
            this.dgv.Columns[10].DisplayIndex = 10;
            btDelColumn.DisplayIndex = 11;

            resizeColumns();
            setKeyReadOnly();
        }

        private void showList()
        {
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
            {
                conn.Open();
            }
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

            //
            //ここから下がデータの読み込み部分。
            //

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.NoData, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (temp_dgv.Columns[e.ColumnIndex].Name == FindingsEditor.Properties.Resources.Delete)
            {
                this.Validate(); //Without this code, new data will disappear.
                saveDataTable(); //Without this code, deleting new record will call error;

                //If object was used, refuse delete.
                int i = uckyFunctions.CountTimes("exam", "equipment", temp_dgv.Rows[e.RowIndex].Cells[1].Value.ToString(), "int");
                if (i > 0) //●●●移植時要変更
                {
                    MessageBox.Show(FindingsEditor.Properties.Resources.CouldntDelData, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (i == -1)
                {
                    MessageBox.Show("[dgv_CellContentClick]Error was occured.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (!temp_dgv.Rows[e.RowIndex].IsNewRow)
                    {
                        if (MessageBox.Show("[" + temp_dgv.Rows[e.RowIndex].Cells[1].Value.ToString() + "]" + FindingsEditor.Properties.Resources.ConfirmDel, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
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
                conn = new NpgsqlConnection(Settings.retConnStr());
            }
            catch (ArgumentException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.WrongConnectingString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                conn.Open();
            }
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

            //
            //ここから下がデータの書き込み部分。
            //
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);

            //InsertCommand
            da.InsertCommand = new NpgsqlCommand("INSERT INTO equipment(equipment_no, name, equipment_type, gf_order, cf_order, sv_order, s_order, us_order, scope, us, equipment_visible) " +
                        " values (:a, :b, :c, :d, :e, :f, :g, :h, :i, :j, :k)", conn);

            da.InsertCommand.Parameters.Add(new NpgsqlParameter("a", DbType.Int16));
            da.InsertCommand.Parameters.Add(new NpgsqlParameter("b", DbType.String));
            da.InsertCommand.Parameters.Add(new NpgsqlParameter("c", DbType.Int16));
            da.InsertCommand.Parameters.Add(new NpgsqlParameter("d", DbType.Int16));
            da.InsertCommand.Parameters.Add(new NpgsqlParameter("e", DbType.Int16));
            da.InsertCommand.Parameters.Add(new NpgsqlParameter("f", DbType.Int16));
            da.InsertCommand.Parameters.Add(new NpgsqlParameter("g", DbType.Int16));
            da.InsertCommand.Parameters.Add(new NpgsqlParameter("h", DbType.Int16));
            da.InsertCommand.Parameters.Add(new NpgsqlParameter("i", DbType.Boolean));
            da.InsertCommand.Parameters.Add(new NpgsqlParameter("j", DbType.Boolean));
            da.InsertCommand.Parameters.Add(new NpgsqlParameter("k", DbType.Boolean));

            da.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
            da.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
            da.InsertCommand.Parameters[2].Direction = ParameterDirection.Input;
            da.InsertCommand.Parameters[3].Direction = ParameterDirection.Input;
            da.InsertCommand.Parameters[4].Direction = ParameterDirection.Input;
            da.InsertCommand.Parameters[5].Direction = ParameterDirection.Input;
            da.InsertCommand.Parameters[6].Direction = ParameterDirection.Input;
            da.InsertCommand.Parameters[7].Direction = ParameterDirection.Input;
            da.InsertCommand.Parameters[8].Direction = ParameterDirection.Input;
            da.InsertCommand.Parameters[9].Direction = ParameterDirection.Input;
            da.InsertCommand.Parameters[10].Direction = ParameterDirection.Input;

            da.InsertCommand.Parameters[0].SourceColumn = "equipment_no";
            da.InsertCommand.Parameters[1].SourceColumn = "name";
            da.InsertCommand.Parameters[2].SourceColumn = "equipment_type";
            da.InsertCommand.Parameters[3].SourceColumn = "gf_order";
            da.InsertCommand.Parameters[4].SourceColumn = "cf_order";
            da.InsertCommand.Parameters[5].SourceColumn = "sv_order";
            da.InsertCommand.Parameters[6].SourceColumn = "s_order";
            da.InsertCommand.Parameters[7].SourceColumn = "us_order";
            da.InsertCommand.Parameters[8].SourceColumn = "scope";
            da.InsertCommand.Parameters[9].SourceColumn = "us";
            da.InsertCommand.Parameters[10].SourceColumn = "equipment_visible";

            //UpdateCommand
            da.UpdateCommand = new NpgsqlCommand("UPDATE equipment SET name = :ub, equipment_type = :uc,"
                + "gf_order = :ud, cf_order = :ue, sv_order = :uf, s_order = :ug, us_order = :uh,"
                + " scope = :ui, us = :uj, equipment_visible = :uk"
                + " WHERE equipment_no = :UKey", conn);

            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("UKey", DbType.Int16));
            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("ub", DbType.String));
            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("uc", DbType.Int16));
            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("ud", DbType.Int16));
            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("ue", DbType.Int16));
            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("uf", DbType.Int16));
            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("ug", DbType.Int16));
            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("uh", DbType.Int16));
            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("ui", DbType.Boolean));
            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("uj", DbType.Boolean));
            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("uk", DbType.Boolean));

            da.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
            da.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
            da.UpdateCommand.Parameters[2].Direction = ParameterDirection.Input;
            da.UpdateCommand.Parameters[3].Direction = ParameterDirection.Input;
            da.UpdateCommand.Parameters[4].Direction = ParameterDirection.Input;
            da.UpdateCommand.Parameters[5].Direction = ParameterDirection.Input;
            da.UpdateCommand.Parameters[6].Direction = ParameterDirection.Input;
            da.UpdateCommand.Parameters[7].Direction = ParameterDirection.Input;
            da.UpdateCommand.Parameters[8].Direction = ParameterDirection.Input;
            da.UpdateCommand.Parameters[9].Direction = ParameterDirection.Input;
            da.UpdateCommand.Parameters[10].Direction = ParameterDirection.Input;

            da.UpdateCommand.Parameters[0].SourceColumn = "equipment_no";
            da.UpdateCommand.Parameters[1].SourceColumn = "name";
            da.UpdateCommand.Parameters[2].SourceColumn = "equipment_type";
            da.UpdateCommand.Parameters[3].SourceColumn = "gf_order";
            da.UpdateCommand.Parameters[4].SourceColumn = "cf_order";
            da.UpdateCommand.Parameters[5].SourceColumn = "sv_order";
            da.UpdateCommand.Parameters[6].SourceColumn = "s_order";
            da.UpdateCommand.Parameters[7].SourceColumn = "us_order";
            da.UpdateCommand.Parameters[8].SourceColumn = "scope";
            da.UpdateCommand.Parameters[9].SourceColumn = "us";
            da.UpdateCommand.Parameters[10].SourceColumn = "equipment_visible";

            //DeleteCommand
            da.DeleteCommand = new NpgsqlCommand("DELETE FROM equipment WHERE equipment_no = :DelKey", conn);
            da.DeleteCommand.Parameters.Add(new NpgsqlParameter("DelKey", DbType.Int16));
            da.DeleteCommand.Parameters[0].SourceColumn = "equipment_no";

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
                        MessageBox.Show("[" + FindingsEditor.Properties.Resources.Number + "]" + FindingsEditor.Properties.Resources.BlankNotAllowed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //●●●移植時要変更
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
                        MessageBox.Show("[" + FindingsEditor.Properties.Resources.ObjectName + "]" + FindingsEditor.Properties.Resources.BlankNotAllowed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //●●●移植時要変更
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
            if (temp_dgv.Columns[e.ColumnIndex].Name == "equipment_no") //●●●移植時要変更
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
                MessageBox.Show(FindingsEditor.Properties.Resources.SoftwareError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Duplication.Error;
            }
            else if (i > 1)
            {
                MessageBox.Show("[" + FindingsEditor.Properties.Resources.Number + "]" + FindingsEditor.Properties.Resources.Duplicated, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //●●●移植時要変更
                return Duplication.Duplicated;
            }
            else
            {
                return Duplication.NotDuplicated;
            }
        }
    }
}

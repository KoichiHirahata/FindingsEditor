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
    public partial class EditWords : EditFormBase
    {
        private string sql = "SELECT no, words1, words2, words3, word_order FROM words"
            +" WHERE operator='" + db_operator.operatorID + "'"
            +" ORDER BY word_order"; //●●●移植時要変更

        public EditWords()
        {
            InitializeComponent();
            this.Width = 950;
            showList();

            //●●●移植時要変更
            dgv.Columns["no"].Visible = false;
            dgv.Columns["word_order"].HeaderText = Properties.Resources.Order;

            DataGridViewButtonColumn btDelColumn = new DataGridViewButtonColumn(); //DataGridViewButtonColumnの作成
            btDelColumn.Name = Properties.Resources.Delete;  //列の名前を設定
            btDelColumn.UseColumnTextForButtonValue = true;  //ボタンにテキスト表示
            btDelColumn.Text = Properties.Resources.Delete;  //ボタンの表示テキスト設定
            dgv.Columns.Add(btDelColumn);     //ボタン追加

            dgv.Columns["words1"].DataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.Columns["words2"].DataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.Columns["words3"].DataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            this.dgv.Columns["words1"].DisplayIndex = 0;
            this.dgv.Columns["words2"].DisplayIndex = 1;
            this.dgv.Columns["words3"].DisplayIndex = 2;
            this.dgv.Columns["word_order"].DisplayIndex = 3;
            btDelColumn.DisplayIndex = 4;

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

        protected override void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView temp_dgv = (DataGridView)sender;

            //Delete row.  This form delete data without checking. In common case, checking of using object is needed.
            if (temp_dgv.Columns[e.ColumnIndex].Name == Properties.Resources.Delete)
            {
                this.Validate(); //Without this code, new data will disappear.
                saveDataTable(); //Without this code, deleting new record will call error;

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

        private void saveDataTable() //●●●移植時要変更
        {
            DataTable dt2 = dt.GetChanges();
            if (dt2 == null)
                return;

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
            #endregion

            //
            //ここから下がデータの書き込み部分。
            //
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);

            //InsertCommand
            da.InsertCommand = new NpgsqlCommand("INSERT INTO words(words1, words2, words3, word_order, operator) " +
                        " values (:a, :b, :c, :d, '" + db_operator.operatorID + "')", conn);

            da.InsertCommand.Parameters.Add(new NpgsqlParameter("a", DbType.String));
            da.InsertCommand.Parameters.Add(new NpgsqlParameter("b", DbType.String));
            da.InsertCommand.Parameters.Add(new NpgsqlParameter("c", DbType.String));
            da.InsertCommand.Parameters.Add(new NpgsqlParameter("d", DbType.Int16));

            da.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
            da.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
            da.InsertCommand.Parameters[2].Direction = ParameterDirection.Input;
            da.InsertCommand.Parameters[3].Direction = ParameterDirection.Input;

            da.InsertCommand.Parameters[0].SourceColumn = "words1";
            da.InsertCommand.Parameters[1].SourceColumn = "words2";
            da.InsertCommand.Parameters[2].SourceColumn = "words3";
            da.InsertCommand.Parameters[3].SourceColumn = "word_order";

            //UpdateCommand
            da.UpdateCommand = new NpgsqlCommand("UPDATE words SET words1 = :ua, words2 = :ub,"
                //+ "words3 = :uc, word_order = :ud, operator = :ue"
                + "words3 = :uc, word_order = :ud"
                + " WHERE no = :UKey", conn);

            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("UKey", DbType.Int16));
            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("ua", DbType.String));
            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("ub", DbType.String));
            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("uc", DbType.String));
            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("ud", DbType.Int16));

            da.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
            da.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
            da.UpdateCommand.Parameters[2].Direction = ParameterDirection.Input;
            da.UpdateCommand.Parameters[3].Direction = ParameterDirection.Input;
            da.UpdateCommand.Parameters[4].Direction = ParameterDirection.Input;

            da.UpdateCommand.Parameters[0].SourceColumn = "no";
            da.UpdateCommand.Parameters[1].SourceColumn = "words1";
            da.UpdateCommand.Parameters[2].SourceColumn = "words2";
            da.UpdateCommand.Parameters[3].SourceColumn = "words3";
            da.UpdateCommand.Parameters[4].SourceColumn = "word_order";

            //DeleteCommand
            da.DeleteCommand = new NpgsqlCommand("DELETE FROM words WHERE no = :DelKey", conn);
            da.DeleteCommand.Parameters.Add(new NpgsqlParameter("DelKey", DbType.Int16));
            da.DeleteCommand.Parameters[0].SourceColumn = "no";

            da.Update(dt2);
            conn.Close();
        }

        private void EditWords_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Validate(); //Without this code, new data will disappear.
            DataTable dt2 = dt.GetChanges();
            if (dt2 != null)
            {
                //int d;  //For TryParse.
                //foreach (DataRow dr in dt2.Rows)
                //{
                //    if (string.IsNullOrWhiteSpace(dr[0].ToString()) == true)
                //    {
                //        MessageBox.Show("[" + Properties.Resources.Number + "]" + Properties.Resources.BlankNotAllowed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //●●●移植時要変更
                //        e.Cancel = true;
                //        return;
                //    }

                //    if (checkDuplicate(dr[0].ToString()) == Duplication.Duplicated)
                //    {
                //        e.Cancel = true;
                //        return;
                //    }
                //    else if (checkDuplicate(dr[0].ToString()) == Duplication.Error)
                //    {
                //        e.Cancel = true;
                //        return;
                //    }

                //    if (string.IsNullOrWhiteSpace(dr[1].ToString()) == true)
                //    {
                //        MessageBox.Show("[" + Properties.Resources.ObjectName + "]" + Properties.Resources.BlankNotAllowed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //●●●移植時要変更
                //        e.Cancel = true;
                //        return;
                //    }
                //}
                saveDataTable();
                CLocalDB.delWords();
                CLocalDB.getWords();
            }
        }

        //protected override void dgv_CellLeave(object sender, DataGridViewCellEventArgs e)
        //{
        //    DataGridView temp_dgv = (DataGridView)sender;
        //    if (temp_dgv.Columns[e.ColumnIndex].Name == "equipment_no") //●●●移植時要変更
        //    {
        //        if (!temp_dgv.Rows[e.RowIndex].IsNewRow)
        //        {
        //            checkDuplicate(temp_dgv.Rows[e.RowIndex].Cells[1].Value.ToString());
        //        }
        //    }
        //}

        //private Duplication checkDuplicate(string checkKey)
        //{
        //    int i = 0;
        //    foreach (DataGridViewRow dr in this.dgv.Rows)
        //    {
        //        if (!dr.IsNewRow)
        //        {
        //            //MessageBox.Show(dr.Cells[1].Value.ToString() + ", CheckKey=" + checkKey.ToString()+ ", i=" + i.ToString());
        //            if (dr.Cells[1].Value.ToString() == checkKey)
        //            {
        //                i += 1;
        //            }
        //        }
        //    }

        //    if (i == 0)
        //    {
        //        MessageBox.Show(Properties.Resources.SoftwareError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return Duplication.Error;
        //    }
        //    else if (i > 1)
        //    {
        //        MessageBox.Show("[" + Properties.Resources.Number + "]" + Properties.Resources.Duplicated, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //●●●移植時要変更
        //        return Duplication.Duplicated;
        //    }
        //    else
        //    {
        //        return Duplication.NotDuplicated;
        //    }
        //}
    }
}

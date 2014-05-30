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
    public partial class EditDepartment : EditFormBase
    {
        private string sql = "SELECT code, name1, dp_order, dp_visible FROM department ORDER BY code"; //***This code have to be changed when reuse

        public EditDepartment()
        {
            InitializeComponent();
            this.Width = 500;

            showList();

            //***This code have to be changed when reuse
            this.dgv.Columns[0].HeaderText = Properties.Resources.Number;
            this.dgv.Columns[1].HeaderText = Properties.Resources.ObjectName;
            this.dgv.Columns[2].HeaderText = Properties.Resources.Order;
            this.dgv.Columns[3].HeaderText = Properties.Resources.Visible;

            DataGridViewButtonColumn btDelColumn = new DataGridViewButtonColumn(); //Create DataGridViewButtonColumn
            btDelColumn.Name = Properties.Resources.Delete;
            btDelColumn.UseColumnTextForButtonValue = true;  //Set text on button visible
            btDelColumn.Text = Properties.Resources.Delete;
            dgv.Columns.Add(btDelColumn);

            resizeColumns();
            setKeyReadOnly();
        }

        private void showList()
        {
            #region Npgsql
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
            { conn.Open(); }
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

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            { MessageBox.Show(Properties.Resources.NoData, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else
            { this.dgv.Refresh(); }
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
                int i = uckyFunctions.CountTimes("exam", "department", temp_dgv.Rows[e.RowIndex].Cells[1].Value.ToString(), "int"); //***This code have to be changed when reuse
                if (i > 0)
                { MessageBox.Show(Properties.Resources.CouldntDelData, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else if (i == -1)
                { MessageBox.Show("[dgv_CellContentClick]Error was occured.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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

        private void saveDataTable() //***This code have to be changed when reuse
        {
            DataTable dt2 = dt.GetChanges();
            if (dt2 == null)
                return;

            #region Npgsql
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
            { conn.Open(); }
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

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);

            //InsertCommand
            da.InsertCommand = new NpgsqlCommand("INSERT INTO department(code, name1, dp_order, dp_visible) " +
                        " values (:a, :b, :c, :d)", conn);

            da.InsertCommand.Parameters.Add(new NpgsqlParameter("a", DbType.Int16));
            da.InsertCommand.Parameters.Add(new NpgsqlParameter("b", DbType.String));
            da.InsertCommand.Parameters.Add(new NpgsqlParameter("c", DbType.Int16));
            da.InsertCommand.Parameters.Add(new NpgsqlParameter("d", DbType.Boolean));

            da.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
            da.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
            da.InsertCommand.Parameters[2].Direction = ParameterDirection.Input;
            da.InsertCommand.Parameters[3].Direction = ParameterDirection.Input;

            da.InsertCommand.Parameters[0].SourceColumn = "code";
            da.InsertCommand.Parameters[1].SourceColumn = "name1";
            da.InsertCommand.Parameters[2].SourceColumn = "dp_order";
            da.InsertCommand.Parameters[3].SourceColumn = "dp_visible";

            //UpdateCommand
            da.UpdateCommand = new NpgsqlCommand("UPDATE department SET name1 = :ub, dp_order = :uc,"
                + " dp_visible = :ud"
                + " WHERE code = :UKey", conn);

            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("UKey", DbType.Int16));
            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("ub", DbType.String));
            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("uc", DbType.Int16));
            da.UpdateCommand.Parameters.Add(new NpgsqlParameter("ud", DbType.Boolean));

            da.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
            da.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
            da.UpdateCommand.Parameters[2].Direction = ParameterDirection.Input;
            da.UpdateCommand.Parameters[3].Direction = ParameterDirection.Input;

            da.UpdateCommand.Parameters[0].SourceColumn = "code";
            da.UpdateCommand.Parameters[1].SourceColumn = "name1";
            da.UpdateCommand.Parameters[2].SourceColumn = "dp_order";
            da.UpdateCommand.Parameters[3].SourceColumn = "dp_visible";

            //DeleteCommand
            da.DeleteCommand = new NpgsqlCommand("DELETE FROM department WHERE code = :DelKey", conn);
            da.DeleteCommand.Parameters.Add(new NpgsqlParameter("DelKey", DbType.Int16));
            da.DeleteCommand.Parameters[0].SourceColumn = "code";

            da.Update(dt2);
            conn.Close();
        }

        private void EditDepartment_FormClosing(object sender, FormClosingEventArgs e) //This function have to be resistered in form property when reuse
        {
            this.Validate(); //Without this code, new data will disappear.
            DataTable dt2 = dt.GetChanges();
            if (dt2 != null)
            {
                foreach (DataRow dr in dt2.Rows)
                {
                    if (string.IsNullOrWhiteSpace(dr[0].ToString()) == true)
                    {
                        MessageBox.Show("[" + Properties.Resources.Number + "]" + Properties.Resources.BlankNotAllowed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //***This code have to be changed when reuse
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
                        MessageBox.Show("[" + Properties.Resources.ObjectName + "]" + Properties.Resources.BlankNotAllowed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //***This code have to be changed when reuse
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
            if (temp_dgv.Columns[e.ColumnIndex].Name == "code") //***This code have to be changed when reuse
            {
                if (!temp_dgv.Rows[e.RowIndex].IsNewRow)
                { checkDuplicate(temp_dgv.Rows[e.RowIndex].Cells[1].Value.ToString()); }
            }
        }

        private Duplication checkDuplicate(string checkKey)
        {
            int i = 0;
            foreach (DataGridViewRow dr in this.dgv.Rows)
            {
                if (!dr.IsNewRow)
                {
                    if (dr.Cells[1].Value.ToString() == checkKey)
                    { i += 1; }
                }
            }

            if (i == 0)
            {
                MessageBox.Show(Properties.Resources.SoftwareError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Duplication.Error;
            }
            else if (i > 1)
            {
                MessageBox.Show("[" + Properties.Resources.Number + "]" + Properties.Resources.Duplicated, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //***This code have to be changed when reuse
                return Duplication.Duplicated;
            }
            else
            { return Duplication.NotDuplicated; }
        }
    }
}

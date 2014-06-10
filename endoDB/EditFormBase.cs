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
    public partial class EditFormBase : Form
    {
        protected DataTable dt = new DataTable();

        public EditFormBase()
        {
            InitializeComponent();

            this.dgv.RowHeadersVisible = false;
            this.dgv.MultiSelect = false;
            this.dgv.Font = new Font(dgv.Font.Name, 12);
            this.dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv.DataSource = dt;
        }

        protected void resizeColumns()
        {
            foreach (DataGridViewColumn dc in dgv.Columns)
            { dc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; }
        }

        protected void setKeyReadOnly()
        {
            foreach (DataGridViewRow dr in dgv.Rows)
            {
                if (!dr.IsNewRow)
                { dr.Cells[1].ReadOnly = true; }
            }
        }

        protected void btClose_Click(object sender, EventArgs e)
        { this.Close(); }

        protected enum Duplication { NotDuplicated, Duplicated, Error }
        protected enum funcResult { Success, failed }

        protected virtual void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        protected virtual void dgv_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}

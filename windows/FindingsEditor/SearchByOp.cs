using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindingsEdior
{
    public partial class SearchByOp : Form
    {
        private Boolean op1_5;
        public SearchByOp(Boolean _op1_5)
        {
            InitializeComponent();

            op1_5 = _op1_5;
            cbOperator.DataSource = CLocalDB.localDB.Tables["allOp"];
            cbOperator.ValueMember = "operator_id";
            cbOperator.DisplayMember = "op_name";
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            ExamList el = new ExamList(dtpFrom.Value.ToString("yyyy-MM-dd"), dtpTo.Value.ToString("yyyy-MM-dd"), null, null, cbOperator.SelectedValue.ToString(), op1_5);
            el.ShowDialog(this);
            this.Close();
        }
    }
}

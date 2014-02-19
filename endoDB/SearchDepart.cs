using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace endoDB
{
    public partial class SearchDepart : Form
    {
        public SearchDepart()
        {
            InitializeComponent();

            cbDepartment.DataSource = CLocalDB.localDB.Tables["department"];
            cbDepartment.ValueMember = "code";
            cbDepartment.DisplayMember = "name1";
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            ExamList el = new ExamList(dtpFrom.Value.ToString("yyyy-MM-dd"), dtpTo.Value.ToString("yyyy-MM-dd"), null, cbDepartment.SelectedValue.ToString(), null, false);
            el.ShowDialog(this);
            this.Close();
        }
    }
}

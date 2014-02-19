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
    public partial class EditOperator : Form
    {
        private Boolean isNew { get; set; }
        private examOperator examOp { get; set; }

        public EditOperator(Boolean _isNew, string _operator_id)
        {
            InitializeComponent();

            //cbCategory initialization
            this.cbCategory.DataSource = CLocalDB.localDB.Tables["op_category"];
            this.cbCategory.ValueMember = "opc_no";
            this.cbCategory.DisplayMember = "opc_name";

            //cbDepartment initialization
            this.cbDepartment.DataSource = CLocalDB.localDB.Tables["department"];
            this.cbDepartment.ValueMember = "code";
            this.cbDepartment.DisplayMember = "name1";

            isNew = _isNew;

            if (isNew)
            {
                examOp = new examOperator(null, true);
                this.tbOperatorID.ReadOnly = false;
                this.cbCategory.SelectedIndex = -1;
                this.cbDepartment.SelectedIndex = -1;
                examOp.op_order = 9998;
            }
            else
            {
                examOp = new examOperator(_operator_id);
                this.tbOperatorID.Text = examOp.operator_id;
                this.tbOperatorID.ReadOnly = true;
                this.tbOperatorName.Text = examOp.op_name;
                this.cbCategory.SelectedValue = examOp.op_category;
                this.cbDepartment.SelectedValue = examOp.department;
                this.cbAllowFc.Checked = examOp.allow_fc;
                this.cbOperatorVisible.Checked = examOp.op_visible;
                this.cbAdminOp.Checked = examOp.admin_op;
                this.tbOperatorPw.Text = examOp.pw;
                this.tbConfirmPw.Text = examOp.pw;
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (this.tbOperatorID.TextLength == 0)
            {
                MessageBox.Show(Properties.Resources.NoID, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.tbOperatorName.TextLength == 0)
            {
                MessageBox.Show(Properties.Resources.NoName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.cbCategory.SelectedValue == null)
            {
                MessageBox.Show(Properties.Resources.NoCategory, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.cbDepartment.SelectedValue == null)
            {
                MessageBox.Show(Properties.Resources.NoDepartment, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.tbOperatorPw.Text != this.tbConfirmPw.Text)
            {
                MessageBox.Show(Properties.Resources.ConfirmPwIsWrong, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.tbOperatorPw.TextLength == 0)
            {
                MessageBox.Show(Properties.Resources.NoPw, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (isNew)
            {
                if (examOperator.numberOfOperator(tbOperatorID.Text) != 0)
                {
                    MessageBox.Show(Properties.Resources.IdDuplicated, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                if (examOperator.numberOfOperator(tbOperatorID.Text) > 1)
                {
                    MessageBox.Show(Properties.Resources.IdDuplicated, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            switch (examOp.saveOperatorData(this.tbOperatorID.Text, this.tbOperatorName.Text, (short)this.cbDepartment.SelectedValue,
                this.tbOperatorPw.Text, this.cbAdminOp.Checked, (short)this.cbCategory.SelectedValue,
                this.cbOperatorVisible.Checked, this.cbAllowFc.Checked, examOp.op_order))
            {
                case uckyFunctions.functionResult.success:
                    this.Close();
                    break;
                case uckyFunctions.functionResult.failed:
                    MessageBox.Show(Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case uckyFunctions.functionResult.connectionError:
                    MessageBox.Show(Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

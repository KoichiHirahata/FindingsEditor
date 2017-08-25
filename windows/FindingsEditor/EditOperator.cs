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
    public partial class EditOperator : Form
    {
        private Boolean isNew { get; set; }
        private examOperator examOp { get; set; }
        Timer timer = new Timer();

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

                #region timer
                uckyFunctions.updateLockTimeIP("operator", "operator_id", "LIKE", "'" + examOp.operator_id + "'");

                //Below timer procedure
                timer.Interval = 30000;  //Unit is msec
                timer.Tick += new EventHandler(timer_Tick);
                timer.Start();
                #endregion
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            #region Error check
            if (this.tbOperatorID.TextLength == 0)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.NoID, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.tbOperatorName.TextLength == 0)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.NoName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.cbCategory.SelectedValue == null)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.NoCategory, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.cbDepartment.SelectedValue == null)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.NoDepartment, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.tbOperatorPw.Text != this.tbConfirmPw.Text)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.ConfirmPwIsWrong, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.tbOperatorPw.TextLength == 0)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.NoPw, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (isNew)
            {
                if (examOperator.numberOfOperator(tbOperatorID.Text) != 0)
                {
                    MessageBox.Show(FindingsEditor.Properties.Resources.IdDuplicated, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                if (examOperator.numberOfOperator(tbOperatorID.Text) > 1)
                {
                    MessageBox.Show(FindingsEditor.Properties.Resources.IdDuplicated, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            #endregion

            switch (examOp.saveOperatorData(tbOperatorID.Text, tbOperatorName.Text, (short)cbDepartment.SelectedValue,
                tbOperatorPw.Text, cbAdminOp.Checked, (short)cbCategory.SelectedValue,
                cbOperatorVisible.Checked, cbAllowFc.Checked, examOp.op_order))
            {
                case uckyFunctions.functionResult.success:
                    Close();
                    break;
                case uckyFunctions.functionResult.failed:
                    MessageBox.Show(FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case uckyFunctions.functionResult.connectionError:
                    MessageBox.Show(FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        { this.Close(); }

        private void timer_Tick(object sender, EventArgs e)
        { uckyFunctions.updateLockTimeIP("operator", "operator_id", "LIKE", "'" + examOp.operator_id + "'"); }

        private void EditOperator_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Stop();
            if (isNew == false)
            {
                if (uckyFunctions.canEdit("operator", "operator_id", "LIKE", "'" + examOp.operator_id + "'"))
                { uckyFunctions.delLockTimeIP("operator", "operator_id", "LIKE", "'" + examOp.operator_id + "'"); }
            }
        }
    }
}

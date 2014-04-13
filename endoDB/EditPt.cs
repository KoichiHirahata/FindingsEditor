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
    public partial class EditPt : Form
    {
        private Boolean pNewPt { get; set; }
        private patient pt1;

        public EditPt(string PtID, Boolean newPt, Boolean ID_editable)
        {
            InitializeComponent();

            pt1 = new patient(PtID, newPt);
            pNewPt = newPt;
            if (newPt)
            { this.tbPtID.Text = pt1.ptID; }
            else
            {
                if (patient.numberOfPatients(PtID) == 1)
                { readPtData(); }
                else
                { MessageBox.Show(Properties.Resources.NoPatient, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            if (ID_editable)
            { tbPtID.Enabled = true; }
            else
            { tbPtID.Enabled = false; }
        }

        private void readPtData()
        {
            this.tbPtID.Text = pt1.ptID;
            this.tbPtName.Text = pt1.ptName;
            if (pt1.ptGender == patient.gender.female)
            { this.rbFemale.Checked = true; }
            else
            { this.rbMale.Checked = true; }
            this.dateTimePicker1.Text = pt1.ptBirthday.ToShortDateString();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (pt1.ptID == tbPtID.Text)
            { savePt(); }
            else
            {
                int ret = pt1.getNumOfExams();
                if (ret == -1)
                { MessageBox.Show(Properties.Resources.ProcedureCancelled, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else if (ret == 0)
                { savePt(); }
                else if (ret > 0)
                { MessageBox.Show(Properties.Resources.ExamsAlreadyExist + Properties.Resources.ProcedureCancelled, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void savePt()
        {
            if (this.tbPtID.Text.Length == 0)
            {
                MessageBox.Show(Properties.Resources.NoID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((rbFemale.Checked == false) && (rbMale.Checked == false))
            {
                MessageBox.Show(Properties.Resources.DetermineGender, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //IDが変更されていた時の処理
            if (this.tbPtID.Text != pt1.ptID)
            {
                if (pt1.newPt)
                {
                    switch (patient.checkIdDuplicate(this.tbPtID.Text))
                    {
                        case patient.idDuplicateResult.NotExist:
                            if (MessageBox.Show(Properties.Resources.IDchanging, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                pt1.ptID = this.tbPtID.Text;
                                pt1.ptName = this.tbPtName.Text;
                                if (this.rbFemale.Checked)
                                    pt1.ptGender = patient.gender.female;
                                else if (this.rbMale.Checked)
                                    pt1.ptGender = patient.gender.male;
                                pt1.ptBirthday = this.dateTimePicker1.Value;
                                pt1.newPt = true;
                                switch (pt1.writePtAllData(pt1))
                                {
                                    case uckyFunctions.functionResult.success:
                                        this.Close();
                                        break;
                                    case uckyFunctions.functionResult.failed:
                                        this.Close();
                                        MessageBox.Show(Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    case uckyFunctions.functionResult.connectionError:
                                        this.Close();
                                        MessageBox.Show(Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                }
                            }
                            else
                                return;
                            break;
                        case patient.idDuplicateResult.None:
                            MessageBox.Show(Properties.Resources.IdDuplicated, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case patient.idDuplicateResult.Duplicated:
                            MessageBox.Show(Properties.Resources.IdDuplicated, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case patient.idDuplicateResult.Error:
                            MessageBox.Show(Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                else
                {
                    MessageBox.Show(Properties.Resources.CanNotChangeId, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.tbPtID.Text = pt1.ptID;
                }
            }
            else    //通常処理
            {
                pt1.ptName = this.tbPtName.Text;
                if (this.rbFemale.Checked)
                    pt1.ptGender = patient.gender.female;
                else if (this.rbMale.Checked)
                    pt1.ptGender = patient.gender.male;
                pt1.ptBirthday = this.dateTimePicker1.Value;
                switch (pt1.writePtAllData(pt1))
                {
                    case uckyFunctions.functionResult.success:
                        this.Close();
                        break;
                    case uckyFunctions.functionResult.failed:
                        this.Close();
                        MessageBox.Show(Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.connectionError:
                        this.Close();
                        MessageBox.Show(Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        { this.Close(); }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btSave.Focus();
                savePt();
            }
        }
    }
}

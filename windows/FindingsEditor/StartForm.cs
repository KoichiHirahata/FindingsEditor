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
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
            if (System.IO.File.Exists(Application.StartupPath + "\\title.jpg"))
            { pbTitle.ImageLocation = Application.StartupPath + "\\title.jpg"; }
            else
            { MessageBox.Show("[title.jpg]" + Properties.Resources.FileNotExist, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            Settings.initiateSettings();
        }

        private void fLogin()
        {
            if (Settings.DBSrvIP == null)
            {
                MessageBox.Show(Properties.Resources.ServerIP, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Settings.DBconnectPw == null)
            {
                MessageBox.Show(Properties.Resources.pwUnconfigured, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            switch (db_operator.idPwCheck(this.tbID.Text, this.tbPass.Text))
            {
                case db_operator.idPwCheckResult.success:
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        private void btLogin_Click(object sender, EventArgs e)
        { fLogin(); }

        private void tbPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                fLogin();
        }

        private void tbID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                this.tbPass.Focus();
        }

        private void initialSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialSettings is_form = new initialSettings();
            is_form.Owner = this;
            is_form.ShowDialog();
        }
    }
}

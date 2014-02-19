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
    public partial class StartForm : Form
    {
        private MainForm mf;

        public StartForm(MainForm mf1)
        {
            InitializeComponent();
            Settings.settingFile_location = Application.StartupPath + "\\setting.config";
            Settings.readSettings();
            Settings.isJP = (Application.CurrentCulture.TwoLetterISOLanguageName == "ja");
            mf = mf1;
        }

        private string IDpwCheck()
        {
            NpgsqlConnection conn;
            try
            {
                conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                    Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;");
            }
            catch (ArgumentException)
            {
                MessageBox.Show(Properties.Resources.WrongConnectingString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            try
            {
                conn.Open();
            }
            catch (NpgsqlException)
            {
                MessageBox.Show(Properties.Resources.CouldntOpenConn, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return null;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(Properties.Resources.ConnClosed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return null;
            }

            string sql = "SELECT * FROM operator WHERE operator_id='" + this.tbID.Text + "'";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                conn.Close();
                MessageBox.Show(Properties.Resources.WrongIDorPW, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            else
            {
                DataRow row = dt.Rows[0];
                if (this.tbPass.Text == row["pw"].ToString())
                {
                    string appUser = row["op_name"].ToString();
                    conn.Close();
                    return appUser;
                }
                else
                {
                    conn.Close();
                    MessageBox.Show(Properties.Resources.WrongIDorPW, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
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
        {
            fLogin();
        }

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

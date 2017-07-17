using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Npgsql;

namespace FindingsEdior
{
    public partial class initialSettings : Form
    {
        public initialSettings()
        {
            InitializeComponent();

            this.tbDBSrv.Text = Settings.DBSrvIP;
            this.tbDBsrvPort.Text = Settings.DBSrvPort;
            this.tbDbID.Text = Settings.DBconnectID;
            //this.tbDBpw.Text = Settings.DBconnectPw; //I don't recomend to use this code.
            if (Settings.DBconnectPw == null)
            { this.pwState.Text = FindingsEditor.Properties.Resources.pwUnconfigured; }
            else
            { this.pwState.Text = FindingsEditor.Properties.Resources.pwConfigured; }
            this.tbDBpw.Visible = false;

            if (!String.IsNullOrWhiteSpace(Settings.figureFolder))
            { tbFigureFolder.Text = Settings.figureFolder; }
        }


        private void btCancel_Click(object sender, EventArgs e)
        { this.Close(); }

        private void btPwSet_Click(object sender, EventArgs e)
        {
            this.tbDBpw.Visible = true;
            tbDBpw.Focus();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbFigureFolder.Text))
            {
                if (!Directory.Exists(tbFigureFolder.Text))
                {
                    MessageBox.Show("[Figure folder]" + FindingsEditor.Properties.Resources.FolderNotExist, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if(testConnect())
            {
                Settings.figureFolder = tbFigureFolder.Text;
                Settings.DBSrvIP = this.tbDBSrv.Text;
                Settings.DBSrvPort = this.tbDBsrvPort.Text;
                Settings.DBconnectID = this.tbDbID.Text;
                if (this.tbDBpw.Visible == true)
                { Settings.DBconnectPw = this.tbDBpw.Text; }
                Settings.saveSettings();
                this.Close();
            }
        }

        private void btTestConnect_Click(object sender, EventArgs e)
        {
            if (testConnect())
            { MessageBox.Show(FindingsEditor.Properties.Resources.ConnectSuccess, "Successfully connected.", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private Boolean testConnect()
        {
            if (this.tbDBSrv.Text.Length == 0)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.ServerIP, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.tbDBsrvPort.Text.Length == 0)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.portUnconfigured, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.tbDbID.Text.Length == 0)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.NoID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string temp_pw;

            if (this.tbDBpw.Visible)
            {
                if (this.tbDBpw.Text.Length == 0)
                {
                    MessageBox.Show(FindingsEditor.Properties.Resources.pwUnconfigured, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                temp_pw = this.tbDBpw.Text;
            }
            else
            {
                if (Settings.DBconnectPw == null)
                {
                    MessageBox.Show(FindingsEditor.Properties.Resources.pwUnconfigured, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                temp_pw = Settings.DBconnectPw;
            }

            #region Npgsql
            // ログ取得を有効にする。
            /*NpgsqlEventLog.Level = LogLevel.Debug;
            NpgsqlEventLog.LogName = "NpgsqlTests.LogFile";
            NpgsqlEventLog.EchoMessages = true;
            */

            NpgsqlConnection conn;
            try
            {
                conn = new NpgsqlConnection(Settings.retConnStr());
            }
            catch (ArgumentException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.WrongConnectingString, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            #endregion

            try
            { conn.Open(); }
            catch (NpgsqlException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.CouldntOpenConn, "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return false;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.ConnClosed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return false;
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                return true;
            }
            else
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.CouldntOpenConn, "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return false;
            }
        }

        private void btFigureFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();  //Make instance of OpenFileDialog class

            fbd.Description = FindingsEditor.Properties.Resources.SelectFolder; //Set description of dialog
            fbd.RootFolder = Environment.SpecialFolder.Desktop; //Set root folder. Deault is desktop
            fbd.SelectedPath = @"C:\"; //Set default pass
            fbd.ShowNewFolderButton = true; //Allow user to make new folder. Default is true

            if (fbd.ShowDialog() == DialogResult.OK)
            { tbFigureFolder.Text = fbd.SelectedPath; }
        }
    }
}

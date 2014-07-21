using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace endoDB
{
    public partial class Plugin : Form
    {
        public Plugin()
        {
            InitializeComponent();
        }

        private void btPtInfoSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.FileName = "";
            if (System.IO.Directory.Exists(Application.StartupPath + "\\plugin"))
            { ofd.InitialDirectory = Application.StartupPath + "\\plugin"; }
            else
            { ofd.InitialDirectory = Application.StartupPath; }
            ofd.Filter = "Exe file(*.exe)|*.exe|All files(*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Title = Properties.Resources.SelectFile;

            if (ofd.ShowDialog() == DialogResult.OK)
            { tbPtInfo.Text = ofd.FileName; }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            string text = "Patient information=" + tbPtInfo.Text + "\r\n";

            #region Save to plugins.ini
            StreamWriter sw = new StreamWriter(Application.StartupPath + @"\plugins.ini", false);
            sw.Write(text);
            sw.Close();
            #endregion

            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        { this.Close(); }

        private void Plugin_Shown(object sender, EventArgs e)
        { tbPtInfo.Text = Settings.checkPtInfoPlugin(); }
    }
}

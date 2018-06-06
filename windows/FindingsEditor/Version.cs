using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FindingsEdior
{
    public partial class Version : Form
    {
        public Version()
        {
            InitializeComponent();

            string db_version = uckyFunctions.getSelectString("SELECT db_version FROM db_version", Settings.DBSrvIP, Settings.DBSrvPort, Settings.DBconnectID, Settings.DBconnectPw, Settings.DBname);

            if (db_version != null)
            { lbDbVersion.Text = "DataBase Version: " + db_version; }
            else
            { lbDbVersion.Text = ""; }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }
    }
}

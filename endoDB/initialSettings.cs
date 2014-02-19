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

namespace endoDB
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
            {
                this.pwState.Text = Properties.Resources.pwUnconfigured;
            }
            else
            {
                this.pwState.Text = Properties.Resources.pwConfigured;
            }
            this.tbDBpw.Visible = false;

            if (!String.IsNullOrWhiteSpace(Settings.endoPrintFile))
            {
                this.tbEndoPrintFile.Text = Settings.endoPrintFile;
            }

            if (!String.IsNullOrWhiteSpace(Settings.figureFolder))
            {
                tbFigureFolder.Text = Settings.figureFolder;
            }
        }


        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btPwSet_Click(object sender, EventArgs e)
        {
            this.tbDBpw.Visible = true;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbEndoPrintFile.Text))
            {
                if (!File.Exists(tbEndoPrintFile.Text))
                {
                    MessageBox.Show("[Endoscopy template file]" + Properties.Resources.FileNotExist, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(tbFigureFolder.Text))
            {
                if (!Directory.Exists(tbFigureFolder.Text))
                {
                    MessageBox.Show("[Figure folder]" + Properties.Resources.FolderNotExist, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            Settings.endoPrintFile = tbEndoPrintFile.Text;
            Settings.figureFolder = tbFigureFolder.Text;
            Settings.DBSrvIP = this.tbDBSrv.Text;
            Settings.DBSrvPort = this.tbDBsrvPort.Text;
            Settings.DBconnectID = this.tbDbID.Text;
            if (this.tbDBpw.Visible == true)
            {
                Settings.DBconnectPw = this.tbDBpw.Text;
            }
            Settings.saveSettings();
            this.Close();
        }

        private void btTestConnect_Click(object sender, EventArgs e)
        {
            if (this.tbDBSrv.Text.Length == 0)
            {
                MessageBox.Show(Properties.Resources.ServerIP, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.tbDBsrvPort.Text.Length == 0)
            {
                MessageBox.Show(Properties.Resources.portUnconfigured, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.tbDbID.Text.Length == 0)
            {
                MessageBox.Show(Properties.Resources.NoID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string temp_pw;

            if (this.tbDBpw.Visible)
            {
                if (this.tbDBpw.Text.Length == 0)
                {
                    MessageBox.Show(Properties.Resources.pwUnconfigured, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                temp_pw = this.tbDBpw.Text;
            }
            else
            {
                if (Settings.DBconnectPw == null)
                {
                    MessageBox.Show(Properties.Resources.pwUnconfigured, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                temp_pw = Settings.DBconnectPw;
            }

            // ログ取得を有効にする。
            /*NpgsqlEventLog.Level = LogLevel.Debug;
            NpgsqlEventLog.LogName = "NpgsqlTests.LogFile";
            NpgsqlEventLog.EchoMessages = true;
            */

            NpgsqlConnection conn;
            try
            {
                conn = new NpgsqlConnection("Server=" + this.tbDBSrv.Text + ";Port=" + this.tbDBsrvPort.Text + ";User Id=" +
                    this.tbDbID.Text + ";Password=" + temp_pw + ";Database=endoDB;");
            }
            catch (ArgumentException)
            {
                MessageBox.Show(Properties.Resources.WrongConnectingString, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                conn.Open();
            }
            catch (NpgsqlException)
            {
                MessageBox.Show(Properties.Resources.CouldntOpenConn, "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(Properties.Resources.ConnClosed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return;
            }

            if (conn.State == ConnectionState.Open)
            {
                MessageBox.Show(Properties.Resources.ConnectSuccess, "Successfully connected.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(Properties.Resources.CouldntOpenConn, "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }

        private void btSelectEndoFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();  //OpenFileDialogクラスのインスタンスを作成
            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            //ofd.FileName = "default.html";

            //はじめに表示されるフォルダを指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            ofd.InitialDirectory = Application.StartupPath.ToString();
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            ofd.Filter ="Xlsファイル(*.xls)|*.xls|すべてのファイル(*.*)|*.*";
            ofd.FilterIndex = 1; //[ファイルの種類]ではじめに「Xlsファイル」が選択されているようにする
            ofd.Title = Properties.Resources.Select;   //タイトルを設定する
            ofd.RestoreDirectory = true; //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbEndoPrintFile.Text = ofd.FileName;
            }
        }

        private void btFigureFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();  //OpenFileDialogクラスのインスタンスを作成

            //上部に表示する説明テキストを指定する
            fbd.Description = "フォルダを指定してください。";
            //ルートフォルダを指定する
            //デフォルトでDesktop
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            //最初に選択するフォルダを指定する
            //RootFolder以下にあるフォルダである必要がある
            fbd.SelectedPath = @"C:\";
            //ユーザーが新しいフォルダを作成できるようにする
            //デフォルトでTrue
            fbd.ShowNewFolderButton = true;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbFigureFolder.Text = fbd.SelectedPath;
            }
        }
    }
}

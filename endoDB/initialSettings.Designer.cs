namespace endoDB
{
    partial class initialSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(initialSettings));
            this.tbDBsrvPort = new System.Windows.Forms.TextBox();
            this.lbDBsrvPort = new System.Windows.Forms.Label();
            this.tbDbID = new System.Windows.Forms.TextBox();
            this.lbDbID = new System.Windows.Forms.Label();
            this.btTestConnect = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.btPwSet = new System.Windows.Forms.Button();
            this.tbDBpw = new System.Windows.Forms.TextBox();
            this.pwState = new System.Windows.Forms.Label();
            this.lbSrvSample = new System.Windows.Forms.Label();
            this.lbDBSrv = new System.Windows.Forms.Label();
            this.tbDBSrv = new System.Windows.Forms.TextBox();
            this.lbFigureFolder = new System.Windows.Forms.Label();
            this.btFigureFolder = new System.Windows.Forms.Button();
            this.tbFigureFolder = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbDBsrvPort
            // 
            resources.ApplyResources(this.tbDBsrvPort, "tbDBsrvPort");
            this.tbDBsrvPort.Name = "tbDBsrvPort";
            // 
            // lbDBsrvPort
            // 
            resources.ApplyResources(this.lbDBsrvPort, "lbDBsrvPort");
            this.lbDBsrvPort.Name = "lbDBsrvPort";
            // 
            // tbDbID
            // 
            resources.ApplyResources(this.tbDbID, "tbDbID");
            this.tbDbID.Name = "tbDbID";
            // 
            // lbDbID
            // 
            resources.ApplyResources(this.lbDbID, "lbDbID");
            this.lbDbID.Name = "lbDbID";
            // 
            // btTestConnect
            // 
            resources.ApplyResources(this.btTestConnect, "btTestConnect");
            this.btTestConnect.Name = "btTestConnect";
            this.btTestConnect.UseVisualStyleBackColor = true;
            this.btTestConnect.Click += new System.EventHandler(this.btTestConnect_Click);
            // 
            // btCancel
            // 
            resources.ApplyResources(this.btCancel, "btCancel");
            this.btCancel.Name = "btCancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btSave
            // 
            resources.ApplyResources(this.btSave, "btSave");
            this.btSave.Name = "btSave";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btPwSet
            // 
            resources.ApplyResources(this.btPwSet, "btPwSet");
            this.btPwSet.Name = "btPwSet";
            this.btPwSet.UseVisualStyleBackColor = true;
            this.btPwSet.Click += new System.EventHandler(this.btPwSet_Click);
            // 
            // tbDBpw
            // 
            resources.ApplyResources(this.tbDBpw, "tbDBpw");
            this.tbDBpw.Name = "tbDBpw";
            // 
            // pwState
            // 
            resources.ApplyResources(this.pwState, "pwState");
            this.pwState.Name = "pwState";
            // 
            // lbSrvSample
            // 
            resources.ApplyResources(this.lbSrvSample, "lbSrvSample");
            this.lbSrvSample.Name = "lbSrvSample";
            // 
            // lbDBSrv
            // 
            resources.ApplyResources(this.lbDBSrv, "lbDBSrv");
            this.lbDBSrv.Name = "lbDBSrv";
            // 
            // tbDBSrv
            // 
            resources.ApplyResources(this.tbDBSrv, "tbDBSrv");
            this.tbDBSrv.Name = "tbDBSrv";
            // 
            // lbFigureFolder
            // 
            resources.ApplyResources(this.lbFigureFolder, "lbFigureFolder");
            this.lbFigureFolder.Name = "lbFigureFolder";
            // 
            // btFigureFolder
            // 
            resources.ApplyResources(this.btFigureFolder, "btFigureFolder");
            this.btFigureFolder.Name = "btFigureFolder";
            this.btFigureFolder.UseVisualStyleBackColor = true;
            this.btFigureFolder.Click += new System.EventHandler(this.btFigureFolder_Click);
            // 
            // tbFigureFolder
            // 
            resources.ApplyResources(this.tbFigureFolder, "tbFigureFolder");
            this.tbFigureFolder.Name = "tbFigureFolder";
            // 
            // initialSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbFigureFolder);
            this.Controls.Add(this.btFigureFolder);
            this.Controls.Add(this.tbFigureFolder);
            this.Controls.Add(this.tbDBsrvPort);
            this.Controls.Add(this.lbDBsrvPort);
            this.Controls.Add(this.tbDbID);
            this.Controls.Add(this.lbDbID);
            this.Controls.Add(this.btTestConnect);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btPwSet);
            this.Controls.Add(this.tbDBpw);
            this.Controls.Add(this.pwState);
            this.Controls.Add(this.lbSrvSample);
            this.Controls.Add(this.lbDBSrv);
            this.Controls.Add(this.tbDBSrv);
            this.Name = "initialSettings";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbDBsrvPort;
        private System.Windows.Forms.Label lbDBsrvPort;
        private System.Windows.Forms.TextBox tbDbID;
        private System.Windows.Forms.Label lbDbID;
        private System.Windows.Forms.Button btTestConnect;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btPwSet;
        private System.Windows.Forms.TextBox tbDBpw;
        private System.Windows.Forms.Label pwState;
        private System.Windows.Forms.Label lbSrvSample;
        private System.Windows.Forms.Label lbDBSrv;
        private System.Windows.Forms.TextBox tbDBSrv;
        private System.Windows.Forms.Label lbFigureFolder;
        private System.Windows.Forms.Button btFigureFolder;
        private System.Windows.Forms.TextBox tbFigureFolder;
    }
}
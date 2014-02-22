namespace endoDB
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.searchExaminationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchByDepartmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchByOperator1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchByOperator15ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.myWordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editOperatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultFindingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.equipmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.placeRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diagnosesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbUserName = new System.Windows.Forms.Label();
            this.lbUser = new System.Windows.Forms.Label();
            this.btSearchPt = new System.Windows.Forms.Button();
            this.btSchedule = new System.Windows.Forms.Button();
            this.btNewPt = new System.Windows.Forms.Button();
            this.btPtView = new System.Windows.Forms.Button();
            this.lbPtID = new System.Windows.Forms.Label();
            this.tbPtID = new System.Windows.Forms.TextBox();
            this.btNewExamination = new System.Windows.Forms.Button();
            this.btDateSearch = new System.Windows.Forms.Button();
            this.lbDate = new System.Windows.Forms.Label();
            this.dtpExamDate = new System.Windows.Forms.DateTimePicker();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchExaminationToolStripMenuItem,
            this.optionToolStripMenuItem,
            this.logOutToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // searchExaminationToolStripMenuItem
            // 
            this.searchExaminationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchByDepartmentToolStripMenuItem,
            this.searchByOperator1ToolStripMenuItem,
            this.searchByOperator15ToolStripMenuItem});
            this.searchExaminationToolStripMenuItem.Name = "searchExaminationToolStripMenuItem";
            resources.ApplyResources(this.searchExaminationToolStripMenuItem, "searchExaminationToolStripMenuItem");
            // 
            // searchByDepartmentToolStripMenuItem
            // 
            this.searchByDepartmentToolStripMenuItem.Name = "searchByDepartmentToolStripMenuItem";
            resources.ApplyResources(this.searchByDepartmentToolStripMenuItem, "searchByDepartmentToolStripMenuItem");
            this.searchByDepartmentToolStripMenuItem.Click += new System.EventHandler(this.searchByDepartmentToolStripMenuItem_Click);
            // 
            // searchByOperator1ToolStripMenuItem
            // 
            this.searchByOperator1ToolStripMenuItem.Name = "searchByOperator1ToolStripMenuItem";
            resources.ApplyResources(this.searchByOperator1ToolStripMenuItem, "searchByOperator1ToolStripMenuItem");
            this.searchByOperator1ToolStripMenuItem.Click += new System.EventHandler(this.searchByOperator1ToolStripMenuItem_Click);
            // 
            // searchByOperator15ToolStripMenuItem
            // 
            this.searchByOperator15ToolStripMenuItem.Name = "searchByOperator15ToolStripMenuItem";
            resources.ApplyResources(this.searchByOperator15ToolStripMenuItem, "searchByOperator15ToolStripMenuItem");
            this.searchByOperator15ToolStripMenuItem.Click += new System.EventHandler(this.searchByOperator15ToolStripMenuItem_Click);
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myWordsToolStripMenuItem,
            this.managementToolStripMenuItem});
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            resources.ApplyResources(this.optionToolStripMenuItem, "optionToolStripMenuItem");
            // 
            // myWordsToolStripMenuItem
            // 
            this.myWordsToolStripMenuItem.Name = "myWordsToolStripMenuItem";
            resources.ApplyResources(this.myWordsToolStripMenuItem, "myWordsToolStripMenuItem");
            this.myWordsToolStripMenuItem.Click += new System.EventHandler(this.myWordsToolStripMenuItem_Click);
            // 
            // managementToolStripMenuItem
            // 
            this.managementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editOperatorToolStripMenuItem,
            this.wordsToolStripMenuItem,
            this.defaultFindingsToolStripMenuItem,
            this.equipmentsToolStripMenuItem,
            this.placeRoomToolStripMenuItem,
            this.wardToolStripMenuItem,
            this.diagnosesToolStripMenuItem});
            this.managementToolStripMenuItem.Name = "managementToolStripMenuItem";
            resources.ApplyResources(this.managementToolStripMenuItem, "managementToolStripMenuItem");
            // 
            // editOperatorToolStripMenuItem
            // 
            this.editOperatorToolStripMenuItem.Name = "editOperatorToolStripMenuItem";
            resources.ApplyResources(this.editOperatorToolStripMenuItem, "editOperatorToolStripMenuItem");
            this.editOperatorToolStripMenuItem.Click += new System.EventHandler(this.operatorManagementToolStripMenuItem_Click);
            // 
            // wordsToolStripMenuItem
            // 
            this.wordsToolStripMenuItem.Name = "wordsToolStripMenuItem";
            resources.ApplyResources(this.wordsToolStripMenuItem, "wordsToolStripMenuItem");
            this.wordsToolStripMenuItem.Click += new System.EventHandler(this.wordsToolStripMenuItem_Click);
            // 
            // defaultFindingsToolStripMenuItem
            // 
            this.defaultFindingsToolStripMenuItem.Name = "defaultFindingsToolStripMenuItem";
            resources.ApplyResources(this.defaultFindingsToolStripMenuItem, "defaultFindingsToolStripMenuItem");
            this.defaultFindingsToolStripMenuItem.Click += new System.EventHandler(this.defaultFindingsToolStripMenuItem_Click);
            // 
            // equipmentsToolStripMenuItem
            // 
            this.equipmentsToolStripMenuItem.Name = "equipmentsToolStripMenuItem";
            resources.ApplyResources(this.equipmentsToolStripMenuItem, "equipmentsToolStripMenuItem");
            this.equipmentsToolStripMenuItem.Click += new System.EventHandler(this.equipmentsToolStripMenuItem_Click);
            // 
            // placeRoomToolStripMenuItem
            // 
            this.placeRoomToolStripMenuItem.Name = "placeRoomToolStripMenuItem";
            resources.ApplyResources(this.placeRoomToolStripMenuItem, "placeRoomToolStripMenuItem");
            this.placeRoomToolStripMenuItem.Click += new System.EventHandler(this.placeRoomToolStripMenuItem_Click);
            // 
            // wardToolStripMenuItem
            // 
            this.wardToolStripMenuItem.Name = "wardToolStripMenuItem";
            resources.ApplyResources(this.wardToolStripMenuItem, "wardToolStripMenuItem");
            this.wardToolStripMenuItem.Click += new System.EventHandler(this.wardToolStripMenuItem_Click);
            // 
            // diagnosesToolStripMenuItem
            // 
            this.diagnosesToolStripMenuItem.Name = "diagnosesToolStripMenuItem";
            resources.ApplyResources(this.diagnosesToolStripMenuItem, "diagnosesToolStripMenuItem");
            this.diagnosesToolStripMenuItem.Click += new System.EventHandler(this.diagnosesToolStripMenuItem_Click);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            resources.ApplyResources(this.logOutToolStripMenuItem, "logOutToolStripMenuItem");
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // lbUserName
            // 
            resources.ApplyResources(this.lbUserName, "lbUserName");
            this.lbUserName.Name = "lbUserName";
            // 
            // lbUser
            // 
            resources.ApplyResources(this.lbUser, "lbUser");
            this.lbUser.Name = "lbUser";
            // 
            // btSearchPt
            // 
            this.btSearchPt.BackColor = System.Drawing.SystemColors.ButtonFace;
            resources.ApplyResources(this.btSearchPt, "btSearchPt");
            this.btSearchPt.Name = "btSearchPt";
            this.btSearchPt.UseVisualStyleBackColor = false;
            this.btSearchPt.Click += new System.EventHandler(this.btSearchPt_Click);
            // 
            // btSchedule
            // 
            this.btSchedule.BackColor = System.Drawing.SystemColors.ButtonFace;
            resources.ApplyResources(this.btSchedule, "btSchedule");
            this.btSchedule.Name = "btSchedule";
            this.btSchedule.UseVisualStyleBackColor = false;
            this.btSchedule.Click += new System.EventHandler(this.btSchedule_Click);
            // 
            // btNewPt
            // 
            this.btNewPt.BackColor = System.Drawing.SystemColors.ButtonFace;
            resources.ApplyResources(this.btNewPt, "btNewPt");
            this.btNewPt.Name = "btNewPt";
            this.btNewPt.UseVisualStyleBackColor = false;
            this.btNewPt.Click += new System.EventHandler(this.btNewPt_Click);
            // 
            // btPtView
            // 
            this.btPtView.BackColor = System.Drawing.SystemColors.ButtonFace;
            resources.ApplyResources(this.btPtView, "btPtView");
            this.btPtView.Name = "btPtView";
            this.btPtView.UseVisualStyleBackColor = false;
            this.btPtView.Click += new System.EventHandler(this.btPtView_Click);
            // 
            // lbPtID
            // 
            resources.ApplyResources(this.lbPtID, "lbPtID");
            this.lbPtID.Name = "lbPtID";
            // 
            // tbPtID
            // 
            resources.ApplyResources(this.tbPtID, "tbPtID");
            this.tbPtID.Name = "tbPtID";
            this.tbPtID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPtID_KeyDown);
            // 
            // btNewExamination
            // 
            this.btNewExamination.BackColor = System.Drawing.SystemColors.ButtonFace;
            resources.ApplyResources(this.btNewExamination, "btNewExamination");
            this.btNewExamination.Name = "btNewExamination";
            this.btNewExamination.UseVisualStyleBackColor = false;
            this.btNewExamination.Click += new System.EventHandler(this.btNewExamination_Click);
            // 
            // btDateSearch
            // 
            this.btDateSearch.BackColor = System.Drawing.SystemColors.ButtonFace;
            resources.ApplyResources(this.btDateSearch, "btDateSearch");
            this.btDateSearch.Name = "btDateSearch";
            this.btDateSearch.UseVisualStyleBackColor = false;
            this.btDateSearch.Click += new System.EventHandler(this.btDateSearch_Click);
            // 
            // lbDate
            // 
            resources.ApplyResources(this.lbDate, "lbDate");
            this.lbDate.Name = "lbDate";
            // 
            // dtpExamDate
            // 
            resources.ApplyResources(this.dtpExamDate, "dtpExamDate");
            this.dtpExamDate.Name = "dtpExamDate";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.dtpExamDate);
            this.Controls.Add(this.btDateSearch);
            this.Controls.Add(this.lbDate);
            this.Controls.Add(this.btNewExamination);
            this.Controls.Add(this.btSearchPt);
            this.Controls.Add(this.btSchedule);
            this.Controls.Add(this.btNewPt);
            this.Controls.Add(this.btPtView);
            this.Controls.Add(this.lbPtID);
            this.Controls.Add(this.tbPtID);
            this.Controls.Add(this.lbUserName);
            this.Controls.Add(this.lbUser);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.Label lbUserName;
        private System.Windows.Forms.Label lbUser;
        private System.Windows.Forms.Button btSearchPt;
        private System.Windows.Forms.Button btSchedule;
        private System.Windows.Forms.Button btNewPt;
        private System.Windows.Forms.Button btPtView;
        private System.Windows.Forms.Label lbPtID;
        private System.Windows.Forms.TextBox tbPtID;
        private System.Windows.Forms.ToolStripMenuItem myWordsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem managementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editOperatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem equipmentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wordsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultFindingsToolStripMenuItem;
        private System.Windows.Forms.Button btNewExamination;
        private System.Windows.Forms.Button btDateSearch;
        private System.Windows.Forms.Label lbDate;
        private System.Windows.Forms.ToolStripMenuItem placeRoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wardToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker dtpExamDate;
        private System.Windows.Forms.ToolStripMenuItem searchExaminationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchByDepartmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchByOperator1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchByOperator15ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem diagnosesToolStripMenuItem;
    }
}
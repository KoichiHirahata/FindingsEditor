namespace endoDB
{
    partial class PatientMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatientMain));
            this.dgvExams = new System.Windows.Forms.DataGridView();
            this.btConfirm = new System.Windows.Forms.Button();
            this.lbInfomation = new System.Windows.Forms.Label();
            this.tbPtInfo = new System.Windows.Forms.TextBox();
            this.PatientBox = new System.Windows.Forms.GroupBox();
            this.Pt_age = new System.Windows.Forms.Label();
            this.lbAge = new System.Windows.Forms.Label();
            this.Pt_gender = new System.Windows.Forms.Label();
            this.Pt_name = new System.Windows.Forms.Label();
            this.lbPatientName = new System.Windows.Forms.Label();
            this.Pt_birthday = new System.Windows.Forms.Label();
            this.lbGender = new System.Windows.Forms.Label();
            this.lbBirthday = new System.Windows.Forms.Label();
            this.btEditPtData = new System.Windows.Forms.Button();
            this.Pt_ID = new System.Windows.Forms.Label();
            this.lbPatientID = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExams)).BeginInit();
            this.PatientBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvExams
            // 
            this.dgvExams.AllowUserToAddRows = false;
            resources.ApplyResources(this.dgvExams, "dgvExams");
            this.dgvExams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExams.Name = "dgvExams";
            this.dgvExams.RowHeadersVisible = false;
            this.dgvExams.RowTemplate.Height = 21;
            this.dgvExams.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick);
            // 
            // btConfirm
            // 
            resources.ApplyResources(this.btConfirm, "btConfirm");
            this.btConfirm.Name = "btConfirm";
            this.btConfirm.UseVisualStyleBackColor = true;
            this.btConfirm.Click += new System.EventHandler(this.btConfirm_Click);
            // 
            // lbInfomation
            // 
            resources.ApplyResources(this.lbInfomation, "lbInfomation");
            this.lbInfomation.Name = "lbInfomation";
            // 
            // tbPtInfo
            // 
            resources.ApplyResources(this.tbPtInfo, "tbPtInfo");
            this.tbPtInfo.Name = "tbPtInfo";
            // 
            // PatientBox
            // 
            this.PatientBox.Controls.Add(this.Pt_age);
            this.PatientBox.Controls.Add(this.lbAge);
            this.PatientBox.Controls.Add(this.Pt_gender);
            this.PatientBox.Controls.Add(this.Pt_name);
            this.PatientBox.Controls.Add(this.lbPatientName);
            this.PatientBox.Controls.Add(this.Pt_birthday);
            this.PatientBox.Controls.Add(this.lbGender);
            this.PatientBox.Controls.Add(this.lbBirthday);
            resources.ApplyResources(this.PatientBox, "PatientBox");
            this.PatientBox.Name = "PatientBox";
            this.PatientBox.TabStop = false;
            // 
            // Pt_age
            // 
            resources.ApplyResources(this.Pt_age, "Pt_age");
            this.Pt_age.Name = "Pt_age";
            // 
            // lbAge
            // 
            resources.ApplyResources(this.lbAge, "lbAge");
            this.lbAge.Name = "lbAge";
            // 
            // Pt_gender
            // 
            resources.ApplyResources(this.Pt_gender, "Pt_gender");
            this.Pt_gender.Name = "Pt_gender";
            // 
            // Pt_name
            // 
            resources.ApplyResources(this.Pt_name, "Pt_name");
            this.Pt_name.Name = "Pt_name";
            // 
            // lbPatientName
            // 
            resources.ApplyResources(this.lbPatientName, "lbPatientName");
            this.lbPatientName.Name = "lbPatientName";
            // 
            // Pt_birthday
            // 
            resources.ApplyResources(this.Pt_birthday, "Pt_birthday");
            this.Pt_birthday.Name = "Pt_birthday";
            // 
            // lbGender
            // 
            resources.ApplyResources(this.lbGender, "lbGender");
            this.lbGender.Name = "lbGender";
            // 
            // lbBirthday
            // 
            resources.ApplyResources(this.lbBirthday, "lbBirthday");
            this.lbBirthday.Name = "lbBirthday";
            // 
            // btEditPtData
            // 
            resources.ApplyResources(this.btEditPtData, "btEditPtData");
            this.btEditPtData.Name = "btEditPtData";
            this.btEditPtData.UseVisualStyleBackColor = true;
            this.btEditPtData.Click += new System.EventHandler(this.btEditPtData_Click);
            // 
            // Pt_ID
            // 
            resources.ApplyResources(this.Pt_ID, "Pt_ID");
            this.Pt_ID.Name = "Pt_ID";
            // 
            // lbPatientID
            // 
            resources.ApplyResources(this.lbPatientID, "lbPatientID");
            this.lbPatientID.Name = "lbPatientID";
            // 
            // PatientMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvExams);
            this.Controls.Add(this.btConfirm);
            this.Controls.Add(this.lbInfomation);
            this.Controls.Add(this.tbPtInfo);
            this.Controls.Add(this.PatientBox);
            this.Controls.Add(this.btEditPtData);
            this.Controls.Add(this.Pt_ID);
            this.Controls.Add(this.lbPatientID);
            this.Name = "PatientMain";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PatientMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PatientMain_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExams)).EndInit();
            this.PatientBox.ResumeLayout(false);
            this.PatientBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvExams;
        private System.Windows.Forms.Button btConfirm;
        private System.Windows.Forms.Label lbInfomation;
        private System.Windows.Forms.TextBox tbPtInfo;
        private System.Windows.Forms.GroupBox PatientBox;
        private System.Windows.Forms.Label Pt_age;
        private System.Windows.Forms.Label lbAge;
        private System.Windows.Forms.Label Pt_gender;
        private System.Windows.Forms.Label Pt_name;
        private System.Windows.Forms.Label lbPatientName;
        private System.Windows.Forms.Label Pt_birthday;
        private System.Windows.Forms.Label lbGender;
        private System.Windows.Forms.Label lbBirthday;
        private System.Windows.Forms.Button btEditPtData;
        private System.Windows.Forms.Label Pt_ID;
        private System.Windows.Forms.Label lbPatientID;
    }
}
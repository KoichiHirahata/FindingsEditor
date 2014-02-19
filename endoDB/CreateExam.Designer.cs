namespace endoDB
{
    partial class CreateExam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateExam));
            this.dtpExamDate = new System.Windows.Forms.DateTimePicker();
            this.lbExamDate = new System.Windows.Forms.Label();
            this.cbExamType = new System.Windows.Forms.ComboBox();
            this.lbExamType = new System.Windows.Forms.Label();
            this.lbPtId = new System.Windows.Forms.Label();
            this.tbPtId = new System.Windows.Forms.TextBox();
            this.btPtLoad = new System.Windows.Forms.Button();
            this.PatientBox = new System.Windows.Forms.GroupBox();
            this.Pt_age = new System.Windows.Forms.Label();
            this.lbAge = new System.Windows.Forms.Label();
            this.Pt_gender = new System.Windows.Forms.Label();
            this.Pt_name = new System.Windows.Forms.Label();
            this.lbPatientName = new System.Windows.Forms.Label();
            this.Pt_birthday = new System.Windows.Forms.Label();
            this.lbGender = new System.Windows.Forms.Label();
            this.lbBirthday = new System.Windows.Forms.Label();
            this.lbDepartment = new System.Windows.Forms.Label();
            this.cbDepartment = new System.Windows.Forms.ComboBox();
            this.lbWard = new System.Windows.Forms.Label();
            this.cbWard = new System.Windows.Forms.ComboBox();
            this.tbPurpose = new System.Windows.Forms.TextBox();
            this.lbPurpose = new System.Windows.Forms.Label();
            this.btConfirm = new System.Windows.Forms.Button();
            this.lbOrderDr = new System.Windows.Forms.Label();
            this.cbOrderDr = new System.Windows.Forms.ComboBox();
            this.btPtEdit = new System.Windows.Forms.Button();
            this.PatientBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpExamDate
            // 
            resources.ApplyResources(this.dtpExamDate, "dtpExamDate");
            this.dtpExamDate.Name = "dtpExamDate";
            // 
            // lbExamDate
            // 
            resources.ApplyResources(this.lbExamDate, "lbExamDate");
            this.lbExamDate.Name = "lbExamDate";
            // 
            // cbExamType
            // 
            resources.ApplyResources(this.cbExamType, "cbExamType");
            this.cbExamType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExamType.FormattingEnabled = true;
            this.cbExamType.Name = "cbExamType";
            // 
            // lbExamType
            // 
            resources.ApplyResources(this.lbExamType, "lbExamType");
            this.lbExamType.Name = "lbExamType";
            // 
            // lbPtId
            // 
            resources.ApplyResources(this.lbPtId, "lbPtId");
            this.lbPtId.Name = "lbPtId";
            // 
            // tbPtId
            // 
            resources.ApplyResources(this.tbPtId, "tbPtId");
            this.tbPtId.Name = "tbPtId";
            this.tbPtId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPtId_KeyDown);
            // 
            // btPtLoad
            // 
            resources.ApplyResources(this.btPtLoad, "btPtLoad");
            this.btPtLoad.Name = "btPtLoad";
            this.btPtLoad.UseVisualStyleBackColor = true;
            this.btPtLoad.Click += new System.EventHandler(this.btPtLoad_Click);
            // 
            // PatientBox
            // 
            resources.ApplyResources(this.PatientBox, "PatientBox");
            this.PatientBox.Controls.Add(this.Pt_age);
            this.PatientBox.Controls.Add(this.lbAge);
            this.PatientBox.Controls.Add(this.Pt_gender);
            this.PatientBox.Controls.Add(this.Pt_name);
            this.PatientBox.Controls.Add(this.lbPatientName);
            this.PatientBox.Controls.Add(this.Pt_birthday);
            this.PatientBox.Controls.Add(this.lbGender);
            this.PatientBox.Controls.Add(this.lbBirthday);
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
            // lbDepartment
            // 
            resources.ApplyResources(this.lbDepartment, "lbDepartment");
            this.lbDepartment.Name = "lbDepartment";
            // 
            // cbDepartment
            // 
            resources.ApplyResources(this.cbDepartment, "cbDepartment");
            this.cbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDepartment.FormattingEnabled = true;
            this.cbDepartment.Name = "cbDepartment";
            // 
            // lbWard
            // 
            resources.ApplyResources(this.lbWard, "lbWard");
            this.lbWard.Name = "lbWard";
            // 
            // cbWard
            // 
            resources.ApplyResources(this.cbWard, "cbWard");
            this.cbWard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWard.FormattingEnabled = true;
            this.cbWard.Name = "cbWard";
            // 
            // tbPurpose
            // 
            resources.ApplyResources(this.tbPurpose, "tbPurpose");
            this.tbPurpose.Name = "tbPurpose";
            // 
            // lbPurpose
            // 
            resources.ApplyResources(this.lbPurpose, "lbPurpose");
            this.lbPurpose.Name = "lbPurpose";
            // 
            // btConfirm
            // 
            resources.ApplyResources(this.btConfirm, "btConfirm");
            this.btConfirm.Name = "btConfirm";
            this.btConfirm.UseVisualStyleBackColor = true;
            this.btConfirm.Click += new System.EventHandler(this.btConfirm_Click);
            // 
            // lbOrderDr
            // 
            resources.ApplyResources(this.lbOrderDr, "lbOrderDr");
            this.lbOrderDr.Name = "lbOrderDr";
            // 
            // cbOrderDr
            // 
            resources.ApplyResources(this.cbOrderDr, "cbOrderDr");
            this.cbOrderDr.FormattingEnabled = true;
            this.cbOrderDr.Name = "cbOrderDr";
            // 
            // btPtEdit
            // 
            resources.ApplyResources(this.btPtEdit, "btPtEdit");
            this.btPtEdit.Name = "btPtEdit";
            this.btPtEdit.UseVisualStyleBackColor = true;
            this.btPtEdit.Click += new System.EventHandler(this.btPtEdit_Click);
            // 
            // CreateExam
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btPtEdit);
            this.Controls.Add(this.lbOrderDr);
            this.Controls.Add(this.cbOrderDr);
            this.Controls.Add(this.btConfirm);
            this.Controls.Add(this.lbPurpose);
            this.Controls.Add(this.tbPurpose);
            this.Controls.Add(this.lbWard);
            this.Controls.Add(this.cbWard);
            this.Controls.Add(this.lbDepartment);
            this.Controls.Add(this.cbDepartment);
            this.Controls.Add(this.PatientBox);
            this.Controls.Add(this.btPtLoad);
            this.Controls.Add(this.tbPtId);
            this.Controls.Add(this.lbPtId);
            this.Controls.Add(this.lbExamType);
            this.Controls.Add(this.cbExamType);
            this.Controls.Add(this.dtpExamDate);
            this.Controls.Add(this.lbExamDate);
            this.Name = "CreateExam";
            this.PatientBox.ResumeLayout(false);
            this.PatientBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpExamDate;
        private System.Windows.Forms.Label lbExamDate;
        private System.Windows.Forms.ComboBox cbExamType;
        private System.Windows.Forms.Label lbExamType;
        private System.Windows.Forms.Label lbPtId;
        private System.Windows.Forms.TextBox tbPtId;
        private System.Windows.Forms.Button btPtLoad;
        private System.Windows.Forms.GroupBox PatientBox;
        private System.Windows.Forms.Label Pt_age;
        private System.Windows.Forms.Label lbAge;
        private System.Windows.Forms.Label Pt_gender;
        private System.Windows.Forms.Label Pt_name;
        private System.Windows.Forms.Label lbPatientName;
        private System.Windows.Forms.Label Pt_birthday;
        private System.Windows.Forms.Label lbGender;
        private System.Windows.Forms.Label lbBirthday;
        private System.Windows.Forms.Label lbDepartment;
        private System.Windows.Forms.ComboBox cbDepartment;
        private System.Windows.Forms.Label lbWard;
        private System.Windows.Forms.ComboBox cbWard;
        private System.Windows.Forms.TextBox tbPurpose;
        private System.Windows.Forms.Label lbPurpose;
        private System.Windows.Forms.Button btConfirm;
        private System.Windows.Forms.Label lbOrderDr;
        private System.Windows.Forms.ComboBox cbOrderDr;
        private System.Windows.Forms.Button btPtEdit;
    }
}
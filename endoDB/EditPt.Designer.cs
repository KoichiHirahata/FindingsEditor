namespace endoDB
{
    partial class EditPt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPt));
            this.btCancel = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.tbPtID = new System.Windows.Forms.TextBox();
            this.rbFemale = new System.Windows.Forms.RadioButton();
            this.rbMale = new System.Windows.Forms.RadioButton();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.tbPtName = new System.Windows.Forms.TextBox();
            this.lbPatientName = new System.Windows.Forms.Label();
            this.lbGender = new System.Windows.Forms.Label();
            this.lbBirthday = new System.Windows.Forms.Label();
            this.lbPatientID = new System.Windows.Forms.Label();
            this.SuspendLayout();
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
            // tbPtID
            // 
            resources.ApplyResources(this.tbPtID, "tbPtID");
            this.tbPtID.Name = "tbPtID";
            // 
            // rbFemale
            // 
            resources.ApplyResources(this.rbFemale, "rbFemale");
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.TabStop = true;
            this.rbFemale.UseVisualStyleBackColor = true;
            // 
            // rbMale
            // 
            resources.ApplyResources(this.rbMale, "rbMale");
            this.rbMale.Name = "rbMale";
            this.rbMale.TabStop = true;
            this.rbMale.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            resources.ApplyResources(this.dateTimePicker1, "dateTimePicker1");
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker1_KeyDown);
            // 
            // tbPtName
            // 
            resources.ApplyResources(this.tbPtName, "tbPtName");
            this.tbPtName.Name = "tbPtName";
            // 
            // lbPatientName
            // 
            resources.ApplyResources(this.lbPatientName, "lbPatientName");
            this.lbPatientName.Name = "lbPatientName";
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
            // lbPatientID
            // 
            resources.ApplyResources(this.lbPatientID, "lbPatientID");
            this.lbPatientID.Name = "lbPatientID";
            // 
            // EditPt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.tbPtID);
            this.Controls.Add(this.rbFemale);
            this.Controls.Add(this.rbMale);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.tbPtName);
            this.Controls.Add(this.lbPatientName);
            this.Controls.Add(this.lbGender);
            this.Controls.Add(this.lbBirthday);
            this.Controls.Add(this.lbPatientID);
            this.Name = "EditPt";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.TextBox tbPtID;
        private System.Windows.Forms.RadioButton rbFemale;
        private System.Windows.Forms.RadioButton rbMale;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox tbPtName;
        private System.Windows.Forms.Label lbPatientName;
        private System.Windows.Forms.Label lbGender;
        private System.Windows.Forms.Label lbBirthday;
        private System.Windows.Forms.Label lbPatientID;
    }
}
namespace FindingsEdior
{
    partial class EditOperator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditOperator));
            this.btCancel = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.lbCategory = new System.Windows.Forms.Label();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.tbOperatorPw = new System.Windows.Forms.TextBox();
            this.lbOperatorPw = new System.Windows.Forms.Label();
            this.cbAdminOp = new System.Windows.Forms.CheckBox();
            this.cbOperatorVisible = new System.Windows.Forms.CheckBox();
            this.tbConfirmPw = new System.Windows.Forms.TextBox();
            this.lbConfirmPw = new System.Windows.Forms.Label();
            this.tbOperatorName = new System.Windows.Forms.TextBox();
            this.lbOperatorName = new System.Windows.Forms.Label();
            this.tbOperatorID = new System.Windows.Forms.TextBox();
            this.lbOperatorID = new System.Windows.Forms.Label();
            this.lbDepartment = new System.Windows.Forms.Label();
            this.cbDepartment = new System.Windows.Forms.ComboBox();
            this.cbAllowFc = new System.Windows.Forms.CheckBox();
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
            // lbCategory
            // 
            resources.ApplyResources(this.lbCategory, "lbCategory");
            this.lbCategory.Name = "lbCategory";
            // 
            // cbCategory
            // 
            this.cbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbCategory, "cbCategory");
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Name = "cbCategory";
            // 
            // tbOperatorPw
            // 
            resources.ApplyResources(this.tbOperatorPw, "tbOperatorPw");
            this.tbOperatorPw.Name = "tbOperatorPw";
            // 
            // lbOperatorPw
            // 
            resources.ApplyResources(this.lbOperatorPw, "lbOperatorPw");
            this.lbOperatorPw.Name = "lbOperatorPw";
            // 
            // cbAdminOp
            // 
            resources.ApplyResources(this.cbAdminOp, "cbAdminOp");
            this.cbAdminOp.Name = "cbAdminOp";
            this.cbAdminOp.UseVisualStyleBackColor = true;
            // 
            // cbOperatorVisible
            // 
            resources.ApplyResources(this.cbOperatorVisible, "cbOperatorVisible");
            this.cbOperatorVisible.Name = "cbOperatorVisible";
            this.cbOperatorVisible.UseVisualStyleBackColor = true;
            // 
            // tbConfirmPw
            // 
            resources.ApplyResources(this.tbConfirmPw, "tbConfirmPw");
            this.tbConfirmPw.Name = "tbConfirmPw";
            // 
            // lbConfirmPw
            // 
            resources.ApplyResources(this.lbConfirmPw, "lbConfirmPw");
            this.lbConfirmPw.Name = "lbConfirmPw";
            // 
            // tbOperatorName
            // 
            resources.ApplyResources(this.tbOperatorName, "tbOperatorName");
            this.tbOperatorName.Name = "tbOperatorName";
            // 
            // lbOperatorName
            // 
            resources.ApplyResources(this.lbOperatorName, "lbOperatorName");
            this.lbOperatorName.Name = "lbOperatorName";
            // 
            // tbOperatorID
            // 
            resources.ApplyResources(this.tbOperatorID, "tbOperatorID");
            this.tbOperatorID.Name = "tbOperatorID";
            // 
            // lbOperatorID
            // 
            resources.ApplyResources(this.lbOperatorID, "lbOperatorID");
            this.lbOperatorID.Name = "lbOperatorID";
            // 
            // lbDepartment
            // 
            resources.ApplyResources(this.lbDepartment, "lbDepartment");
            this.lbDepartment.Name = "lbDepartment";
            // 
            // cbDepartment
            // 
            this.cbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbDepartment, "cbDepartment");
            this.cbDepartment.FormattingEnabled = true;
            this.cbDepartment.Name = "cbDepartment";
            // 
            // cbAllowFc
            // 
            resources.ApplyResources(this.cbAllowFc, "cbAllowFc");
            this.cbAllowFc.Name = "cbAllowFc";
            this.cbAllowFc.UseVisualStyleBackColor = true;
            // 
            // EditOperator
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbAllowFc);
            this.Controls.Add(this.lbDepartment);
            this.Controls.Add(this.cbDepartment);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.lbCategory);
            this.Controls.Add(this.cbCategory);
            this.Controls.Add(this.tbOperatorPw);
            this.Controls.Add(this.lbOperatorPw);
            this.Controls.Add(this.cbAdminOp);
            this.Controls.Add(this.cbOperatorVisible);
            this.Controls.Add(this.tbConfirmPw);
            this.Controls.Add(this.lbConfirmPw);
            this.Controls.Add(this.tbOperatorName);
            this.Controls.Add(this.lbOperatorName);
            this.Controls.Add(this.tbOperatorID);
            this.Controls.Add(this.lbOperatorID);
            this.Name = "EditOperator";
            this.ShowInTaskbar = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditOperator_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Label lbCategory;
        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.TextBox tbOperatorPw;
        private System.Windows.Forms.Label lbOperatorPw;
        private System.Windows.Forms.CheckBox cbAdminOp;
        private System.Windows.Forms.CheckBox cbOperatorVisible;
        private System.Windows.Forms.TextBox tbConfirmPw;
        private System.Windows.Forms.Label lbConfirmPw;
        private System.Windows.Forms.TextBox tbOperatorName;
        private System.Windows.Forms.Label lbOperatorName;
        private System.Windows.Forms.TextBox tbOperatorID;
        private System.Windows.Forms.Label lbOperatorID;
        private System.Windows.Forms.Label lbDepartment;
        private System.Windows.Forms.ComboBox cbDepartment;
        private System.Windows.Forms.CheckBox cbAllowFc;
    }
}
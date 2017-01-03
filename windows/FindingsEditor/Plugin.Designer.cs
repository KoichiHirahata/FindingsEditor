namespace FindingsEdior
{
    partial class Plugin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Plugin));
            this.tbPtInfo = new System.Windows.Forms.TextBox();
            this.btPtInfoSelect = new System.Windows.Forms.Button();
            this.lbPtInfo = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbPtInfo
            // 
            resources.ApplyResources(this.tbPtInfo, "tbPtInfo");
            this.tbPtInfo.Name = "tbPtInfo";
            // 
            // btPtInfoSelect
            // 
            resources.ApplyResources(this.btPtInfoSelect, "btPtInfoSelect");
            this.btPtInfoSelect.Name = "btPtInfoSelect";
            this.btPtInfoSelect.UseVisualStyleBackColor = true;
            this.btPtInfoSelect.Click += new System.EventHandler(this.btPtInfoSelect_Click);
            // 
            // lbPtInfo
            // 
            resources.ApplyResources(this.lbPtInfo, "lbPtInfo");
            this.lbPtInfo.Name = "lbPtInfo";
            // 
            // btSave
            // 
            resources.ApplyResources(this.btSave, "btSave");
            this.btSave.Name = "btSave";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btCancel
            // 
            resources.ApplyResources(this.btCancel, "btCancel");
            this.btCancel.Name = "btCancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // Plugin
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.lbPtInfo);
            this.Controls.Add(this.btPtInfoSelect);
            this.Controls.Add(this.tbPtInfo);
            this.Name = "Plugin";
            this.ShowInTaskbar = false;
            this.Shown += new System.EventHandler(this.Plugin_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPtInfo;
        private System.Windows.Forms.Button btPtInfoSelect;
        private System.Windows.Forms.Label lbPtInfo;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btCancel;
    }
}
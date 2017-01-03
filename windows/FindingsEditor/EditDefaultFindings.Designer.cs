namespace FindingsEdior
{
    partial class EditDefaultFindings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditDefaultFindings));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btSave = new System.Windows.Forms.Button();
            this.lbLineFeeds = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToOrderColumns = true;
            resources.ApplyResources(this.dgv, "dgv");
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Name = "dgv";
            this.dgv.RowTemplate.Height = 21;
            // 
            // btSave
            // 
            resources.ApplyResources(this.btSave, "btSave");
            this.btSave.Name = "btSave";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // lbLineFeeds
            // 
            resources.ApplyResources(this.lbLineFeeds, "lbLineFeeds");
            this.lbLineFeeds.Name = "lbLineFeeds";
            // 
            // EditDefaultFindings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbLineFeeds);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.dgv);
            this.Name = "EditDefaultFindings";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Label lbLineFeeds;
    }
}
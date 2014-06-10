namespace endoDB
{
    partial class SearchPt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchPt));
            this.btSearchPt = new System.Windows.Forms.Button();
            this.tbSearchString = new System.Windows.Forms.TextBox();
            this.lbSearch4 = new System.Windows.Forms.Label();
            this.dgvPatientList = new System.Windows.Forms.DataGridView();
            this.btClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatientList)).BeginInit();
            this.SuspendLayout();
            // 
            // btSearchPt
            // 
            resources.ApplyResources(this.btSearchPt, "btSearchPt");
            this.btSearchPt.Name = "btSearchPt";
            this.btSearchPt.UseVisualStyleBackColor = true;
            this.btSearchPt.Click += new System.EventHandler(this.btSearchPt_Click);
            // 
            // tbSearchString
            // 
            resources.ApplyResources(this.tbSearchString, "tbSearchString");
            this.tbSearchString.Name = "tbSearchString";
            this.tbSearchString.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearchString_KeyDown);
            // 
            // lbSearch4
            // 
            resources.ApplyResources(this.lbSearch4, "lbSearch4");
            this.lbSearch4.Name = "lbSearch4";
            // 
            // dgvPatientList
            // 
            this.dgvPatientList.AllowUserToAddRows = false;
            this.dgvPatientList.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dgvPatientList, "dgvPatientList");
            this.dgvPatientList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPatientList.Name = "dgvPatientList";
            this.dgvPatientList.ReadOnly = true;
            this.dgvPatientList.RowTemplate.Height = 21;
            this.dgvPatientList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPatientList_CellContentClick);
            this.dgvPatientList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPatientList_CellDoubleClick);
            // 
            // btClose
            // 
            resources.ApplyResources(this.btClose, "btClose");
            this.btClose.Name = "btClose";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // SearchPt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.dgvPatientList);
            this.Controls.Add(this.btSearchPt);
            this.Controls.Add(this.tbSearchString);
            this.Controls.Add(this.lbSearch4);
            this.Name = "SearchPt";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatientList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSearchPt;
        private System.Windows.Forms.TextBox tbSearchString;
        private System.Windows.Forms.Label lbSearch4;
        private System.Windows.Forms.DataGridView dgvPatientList;
        private System.Windows.Forms.Button btClose;
    }
}
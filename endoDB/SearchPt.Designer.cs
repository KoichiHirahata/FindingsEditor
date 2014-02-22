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
            this.btSearchPt.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.btSearchPt.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btSearchPt.Location = new System.Drawing.Point(312, 20);
            this.btSearchPt.Name = "btSearchPt";
            this.btSearchPt.Size = new System.Drawing.Size(70, 27);
            this.btSearchPt.TabIndex = 6;
            this.btSearchPt.Text = "Search";
            this.btSearchPt.UseVisualStyleBackColor = true;
            // 
            // tbSearchString
            // 
            this.tbSearchString.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.tbSearchString.Location = new System.Drawing.Point(103, 22);
            this.tbSearchString.Name = "tbSearchString";
            this.tbSearchString.Size = new System.Drawing.Size(203, 23);
            this.tbSearchString.TabIndex = 5;
            // 
            // lbSearch4
            // 
            this.lbSearch4.AutoSize = true;
            this.lbSearch4.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.lbSearch4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbSearch4.Location = new System.Drawing.Point(14, 25);
            this.lbSearch4.Name = "lbSearch4";
            this.lbSearch4.Size = new System.Drawing.Size(83, 16);
            this.lbSearch4.TabIndex = 4;
            this.lbSearch4.Text = "Search for:";
            // 
            // dgvPatientList
            // 
            this.dgvPatientList.AllowUserToAddRows = false;
            this.dgvPatientList.AllowUserToDeleteRows = false;
            this.dgvPatientList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPatientList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPatientList.Location = new System.Drawing.Point(17, 63);
            this.dgvPatientList.Name = "dgvPatientList";
            this.dgvPatientList.ReadOnly = true;
            this.dgvPatientList.RowTemplate.Height = 21;
            this.dgvPatientList.Size = new System.Drawing.Size(365, 259);
            this.dgvPatientList.TabIndex = 7;
            // 
            // btClose
            // 
            this.btClose.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.btClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btClose.Location = new System.Drawing.Point(312, 333);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(70, 27);
            this.btClose.TabIndex = 8;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // SearchPt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 372);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.dgvPatientList);
            this.Controls.Add(this.btSearchPt);
            this.Controls.Add(this.tbSearchString);
            this.Controls.Add(this.lbSearch4);
            this.Name = "SearchPt";
            this.ShowInTaskbar = false;
            this.Text = "SearchPt";
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
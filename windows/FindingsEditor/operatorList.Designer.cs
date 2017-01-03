namespace FindingsEdior
{
    partial class operatorList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(operatorList));
            this.btClose = new System.Windows.Forms.Button();
            this.dgvOperatorList = new System.Windows.Forms.DataGridView();
            this.btUp = new System.Windows.Forms.Button();
            this.btDown = new System.Windows.Forms.Button();
            this.btAddOperator = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOperatorList)).BeginInit();
            this.SuspendLayout();
            // 
            // btClose
            // 
            resources.ApplyResources(this.btClose, "btClose");
            this.btClose.Name = "btClose";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // dgvOperatorList
            // 
            this.dgvOperatorList.AllowUserToAddRows = false;
            resources.ApplyResources(this.dgvOperatorList, "dgvOperatorList");
            this.dgvOperatorList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOperatorList.MultiSelect = false;
            this.dgvOperatorList.Name = "dgvOperatorList";
            this.dgvOperatorList.ReadOnly = true;
            this.dgvOperatorList.RowTemplate.Height = 21;
            this.dgvOperatorList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOperatorList_CellContentClick);
            // 
            // btUp
            // 
            resources.ApplyResources(this.btUp, "btUp");
            this.btUp.Name = "btUp";
            this.btUp.UseVisualStyleBackColor = true;
            this.btUp.Click += new System.EventHandler(this.btUp_Click);
            // 
            // btDown
            // 
            resources.ApplyResources(this.btDown, "btDown");
            this.btDown.Name = "btDown";
            this.btDown.UseVisualStyleBackColor = true;
            this.btDown.Click += new System.EventHandler(this.btDown_Click);
            // 
            // btAddOperator
            // 
            resources.ApplyResources(this.btAddOperator, "btAddOperator");
            this.btAddOperator.Name = "btAddOperator";
            this.btAddOperator.UseVisualStyleBackColor = true;
            this.btAddOperator.Click += new System.EventHandler(this.btAddOperator_Click);
            // 
            // operatorList
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btAddOperator);
            this.Controls.Add(this.btDown);
            this.Controls.Add(this.btUp);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.dgvOperatorList);
            this.Name = "operatorList";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.dgvOperatorList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.DataGridView dgvOperatorList;
        private System.Windows.Forms.Button btUp;
        private System.Windows.Forms.Button btDown;
        private System.Windows.Forms.Button btAddOperator;
    }
}
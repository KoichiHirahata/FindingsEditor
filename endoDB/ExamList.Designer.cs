namespace endoDB
{
    partial class ExamList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExamList));
            this.dgvExamList = new System.Windows.Forms.DataGridView();
            this.btClose = new System.Windows.Forms.Button();
            this.btBlankDraft = new System.Windows.Forms.Button();
            this.btDone = new System.Windows.Forms.Button();
            this.btChecked = new System.Windows.Forms.Button();
            this.btCanceled = new System.Windows.Forms.Button();
            this.btShowAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExamList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvExamList
            // 
            this.dgvExamList.AllowUserToAddRows = false;
            this.dgvExamList.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dgvExamList, "dgvExamList");
            this.dgvExamList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExamList.MultiSelect = false;
            this.dgvExamList.Name = "dgvExamList";
            this.dgvExamList.ReadOnly = true;
            this.dgvExamList.RowTemplate.Height = 21;
            this.dgvExamList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExamList_CellContentClick);
            this.dgvExamList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvExamList_KeyDown);
            // 
            // btClose
            // 
            resources.ApplyResources(this.btClose, "btClose");
            this.btClose.Name = "btClose";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btBlankDraft
            // 
            resources.ApplyResources(this.btBlankDraft, "btBlankDraft");
            this.btBlankDraft.Name = "btBlankDraft";
            this.btBlankDraft.UseVisualStyleBackColor = true;
            this.btBlankDraft.Click += new System.EventHandler(this.btBlankDraft_Click);
            // 
            // btDone
            // 
            resources.ApplyResources(this.btDone, "btDone");
            this.btDone.Name = "btDone";
            this.btDone.UseVisualStyleBackColor = true;
            this.btDone.Click += new System.EventHandler(this.btDone_Click);
            // 
            // btChecked
            // 
            resources.ApplyResources(this.btChecked, "btChecked");
            this.btChecked.Name = "btChecked";
            this.btChecked.UseVisualStyleBackColor = true;
            this.btChecked.Click += new System.EventHandler(this.btChecked_Click);
            // 
            // btCanceled
            // 
            resources.ApplyResources(this.btCanceled, "btCanceled");
            this.btCanceled.Name = "btCanceled";
            this.btCanceled.UseVisualStyleBackColor = true;
            this.btCanceled.Click += new System.EventHandler(this.btCanceled_Click);
            // 
            // btShowAll
            // 
            resources.ApplyResources(this.btShowAll, "btShowAll");
            this.btShowAll.Name = "btShowAll";
            this.btShowAll.UseVisualStyleBackColor = true;
            this.btShowAll.Click += new System.EventHandler(this.btShowAll_Click);
            // 
            // ExamList
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btShowAll);
            this.Controls.Add(this.btCanceled);
            this.Controls.Add(this.btChecked);
            this.Controls.Add(this.btDone);
            this.Controls.Add(this.btBlankDraft);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.dgvExamList);
            this.Name = "ExamList";
            this.ShowInTaskbar = false;
            this.Shown += new System.EventHandler(this.ExamList_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExamList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvExamList;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btBlankDraft;
        private System.Windows.Forms.Button btDone;
        private System.Windows.Forms.Button btChecked;
        private System.Windows.Forms.Button btCanceled;
        private System.Windows.Forms.Button btShowAll;
    }
}
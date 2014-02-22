namespace endoDB
{
    partial class SearchByOp
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
            this.btSearch = new System.Windows.Forms.Button();
            this.lbTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lbFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.cbOperator = new System.Windows.Forms.ComboBox();
            this.lbOperator = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btSearch
            // 
            this.btSearch.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btSearch.Location = new System.Drawing.Point(25, 180);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(247, 32);
            this.btSearch.TabIndex = 13;
            this.btSearch.Text = "Search";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // lbTo
            // 
            this.lbTo.AutoSize = true;
            this.lbTo.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbTo.Location = new System.Drawing.Point(85, 133);
            this.lbTo.Name = "lbTo";
            this.lbTo.Size = new System.Drawing.Size(28, 16);
            this.lbTo.TabIndex = 12;
            this.lbTo.Text = "To:";
            // 
            // dtpTo
            // 
            this.dtpTo.CalendarFont = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtpTo.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtpTo.Location = new System.Drawing.Point(119, 128);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(153, 23);
            this.dtpTo.TabIndex = 11;
            // 
            // lbFrom
            // 
            this.lbFrom.AutoSize = true;
            this.lbFrom.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbFrom.Location = new System.Drawing.Point(67, 86);
            this.lbFrom.Name = "lbFrom";
            this.lbFrom.Size = new System.Drawing.Size(46, 16);
            this.lbFrom.TabIndex = 10;
            this.lbFrom.Text = "From:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CalendarFont = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtpFrom.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtpFrom.Location = new System.Drawing.Point(119, 81);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(153, 23);
            this.dtpFrom.TabIndex = 9;
            // 
            // cbOperator
            // 
            this.cbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperator.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cbOperator.FormattingEnabled = true;
            this.cbOperator.Location = new System.Drawing.Point(119, 24);
            this.cbOperator.Name = "cbOperator";
            this.cbOperator.Size = new System.Drawing.Size(153, 24);
            this.cbOperator.TabIndex = 8;
            // 
            // lbOperator
            // 
            this.lbOperator.AutoSize = true;
            this.lbOperator.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbOperator.Location = new System.Drawing.Point(41, 27);
            this.lbOperator.Name = "lbOperator";
            this.lbOperator.Size = new System.Drawing.Size(72, 16);
            this.lbOperator.TabIndex = 7;
            this.lbOperator.Text = "Operator:";
            // 
            // SearchByOp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 237);
            this.Controls.Add(this.btSearch);
            this.Controls.Add(this.lbTo);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.lbFrom);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.cbOperator);
            this.Controls.Add(this.lbOperator);
            this.Name = "SearchByOp";
            this.ShowInTaskbar = false;
            this.Text = "Search by operator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.Label lbTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label lbFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.ComboBox cbOperator;
        private System.Windows.Forms.Label lbOperator;
    }
}
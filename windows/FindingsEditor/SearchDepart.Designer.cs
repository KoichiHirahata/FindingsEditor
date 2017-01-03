namespace FindingsEdior
{
    partial class SearchDepart
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
            this.lbDepartment = new System.Windows.Forms.Label();
            this.cbDepartment = new System.Windows.Forms.ComboBox();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lbFrom = new System.Windows.Forms.Label();
            this.lbTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbDepartment
            // 
            this.lbDepartment.AutoSize = true;
            this.lbDepartment.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbDepartment.Location = new System.Drawing.Point(21, 30);
            this.lbDepartment.Name = "lbDepartment";
            this.lbDepartment.Size = new System.Drawing.Size(91, 16);
            this.lbDepartment.TabIndex = 0;
            this.lbDepartment.Text = "Department:";
            // 
            // cbDepartment
            // 
            this.cbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDepartment.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cbDepartment.FormattingEnabled = true;
            this.cbDepartment.Location = new System.Drawing.Point(118, 27);
            this.cbDepartment.Name = "cbDepartment";
            this.cbDepartment.Size = new System.Drawing.Size(153, 24);
            this.cbDepartment.TabIndex = 1;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CalendarFont = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtpFrom.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtpFrom.Location = new System.Drawing.Point(118, 84);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(153, 23);
            this.dtpFrom.TabIndex = 2;
            // 
            // lbFrom
            // 
            this.lbFrom.AutoSize = true;
            this.lbFrom.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbFrom.Location = new System.Drawing.Point(66, 89);
            this.lbFrom.Name = "lbFrom";
            this.lbFrom.Size = new System.Drawing.Size(46, 16);
            this.lbFrom.TabIndex = 3;
            this.lbFrom.Text = "From:";
            // 
            // lbTo
            // 
            this.lbTo.AutoSize = true;
            this.lbTo.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbTo.Location = new System.Drawing.Point(84, 136);
            this.lbTo.Name = "lbTo";
            this.lbTo.Size = new System.Drawing.Size(28, 16);
            this.lbTo.TabIndex = 5;
            this.lbTo.Text = "To:";
            // 
            // dtpTo
            // 
            this.dtpTo.CalendarFont = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtpTo.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtpTo.Location = new System.Drawing.Point(118, 131);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(153, 23);
            this.dtpTo.TabIndex = 4;
            // 
            // btSearch
            // 
            this.btSearch.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btSearch.Location = new System.Drawing.Point(24, 183);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(247, 32);
            this.btSearch.TabIndex = 6;
            this.btSearch.Text = "Search";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // SearchDepart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 237);
            this.Controls.Add(this.btSearch);
            this.Controls.Add(this.lbTo);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.lbFrom);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.cbDepartment);
            this.Controls.Add(this.lbDepartment);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchDepart";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search by department";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbDepartment;
        private System.Windows.Forms.ComboBox cbDepartment;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lbFrom;
        private System.Windows.Forms.Label lbTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btSearch;
    }
}
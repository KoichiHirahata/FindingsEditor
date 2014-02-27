namespace endoDB
{
    partial class EditFindings
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditFindings));
            this.lbPatient = new System.Windows.Forms.Label();
            this.tcExam = new System.Windows.Forms.TabControl();
            this.tpInformation = new System.Windows.Forms.TabPage();
            this.btExamCanceled = new System.Windows.Forms.Button();
            this.btClearOp5 = new System.Windows.Forms.Button();
            this.cbOperator5 = new System.Windows.Forms.ComboBox();
            this.lbOperator5 = new System.Windows.Forms.Label();
            this.btClearOp4 = new System.Windows.Forms.Button();
            this.cbOperator4 = new System.Windows.Forms.ComboBox();
            this.lbOperator4 = new System.Windows.Forms.Label();
            this.btClearOp3 = new System.Windows.Forms.Button();
            this.cbOperator3 = new System.Windows.Forms.ComboBox();
            this.lbOperator3 = new System.Windows.Forms.Label();
            this.btClearOp2 = new System.Windows.Forms.Button();
            this.cbOperator2 = new System.Windows.Forms.ComboBox();
            this.lbOperator2 = new System.Windows.Forms.Label();
            this.btClearOp1 = new System.Windows.Forms.Button();
            this.cbOperator1 = new System.Windows.Forms.ComboBox();
            this.lbOperator1 = new System.Windows.Forms.Label();
            this.cbPlace = new System.Windows.Forms.ComboBox();
            this.lbPlace = new System.Windows.Forms.Label();
            this.cbEquipment = new System.Windows.Forms.ComboBox();
            this.lbEquipment = new System.Windows.Forms.Label();
            this.cbWard = new System.Windows.Forms.ComboBox();
            this.lbWard = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbOrderDr = new System.Windows.Forms.ComboBox();
            this.cbDepartment = new System.Windows.Forms.ComboBox();
            this.lbDepartment = new System.Windows.Forms.Label();
            this.tbPurpose = new System.Windows.Forms.TextBox();
            this.lbPurpose = new System.Windows.Forms.Label();
            this.lbDate = new System.Windows.Forms.Label();
            this.tpFindings = new System.Windows.Forms.TabPage();
            this.btSetNormal = new System.Windows.Forms.Button();
            this.lbFindings = new System.Windows.Forms.Label();
            this.cbChecker = new System.Windows.Forms.ComboBox();
            this.cbDiagnosed = new System.Windows.Forms.ComboBox();
            this.lbDiagnosed = new System.Windows.Forms.Label();
            this.btDiagnosed = new System.Windows.Forms.Button();
            this.btChecked = new System.Windows.Forms.Button();
            this.lbChecker = new System.Windows.Forms.Label();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.lbCheckersComment = new System.Windows.Forms.Label();
            this.btReverseOrder = new System.Windows.Forms.Button();
            this.btCopyDiag = new System.Windows.Forms.Button();
            this.tbFindings = new System.Windows.Forms.TextBox();
            this.btAddDiag = new System.Windows.Forms.Button();
            this.dgvDiagnoses = new System.Windows.Forms.DataGridView();
            this.tpFigure1 = new System.Windows.Forms.TabPage();
            this.btErace1 = new System.Windows.Forms.Button();
            this.cbBrushWidth1 = new System.Windows.Forms.ComboBox();
            this.pbBrushWidth1 = new System.Windows.Forms.PictureBox();
            this.pbColor1 = new System.Windows.Forms.PictureBox();
            this.btColorSelect1 = new System.Windows.Forms.Button();
            this.pbFigure1 = new System.Windows.Forms.PictureBox();
            this.tpFigure2 = new System.Windows.Forms.TabPage();
            this.btErace2 = new System.Windows.Forms.Button();
            this.cbBrushWidth2 = new System.Windows.Forms.ComboBox();
            this.pbBrushWidth2 = new System.Windows.Forms.PictureBox();
            this.pbColor2 = new System.Windows.Forms.PictureBox();
            this.btColorSelect2 = new System.Windows.Forms.Button();
            this.pbFigure2 = new System.Windows.Forms.PictureBox();
            this.cbExamStatus = new System.Windows.Forms.ComboBox();
            this.dgvWords = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btCopy2ClipBoard = new System.Windows.Forms.Button();
            this.btSaveClose = new System.Windows.Forms.Button();
            this.tcExam.SuspendLayout();
            this.tpInformation.SuspendLayout();
            this.tpFindings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiagnoses)).BeginInit();
            this.tpFigure1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBrushWidth1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFigure1)).BeginInit();
            this.tpFigure2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBrushWidth2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFigure2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWords)).BeginInit();
            this.SuspendLayout();
            // 
            // lbPatient
            // 
            resources.ApplyResources(this.lbPatient, "lbPatient");
            this.lbPatient.Name = "lbPatient";
            this.toolTip1.SetToolTip(this.lbPatient, resources.GetString("lbPatient.ToolTip"));
            // 
            // tcExam
            // 
            resources.ApplyResources(this.tcExam, "tcExam");
            this.tcExam.Controls.Add(this.tpInformation);
            this.tcExam.Controls.Add(this.tpFindings);
            this.tcExam.Controls.Add(this.tpFigure1);
            this.tcExam.Controls.Add(this.tpFigure2);
            this.tcExam.Name = "tcExam";
            this.tcExam.SelectedIndex = 0;
            this.toolTip1.SetToolTip(this.tcExam, resources.GetString("tcExam.ToolTip"));
            // 
            // tpInformation
            // 
            resources.ApplyResources(this.tpInformation, "tpInformation");
            this.tpInformation.Controls.Add(this.btExamCanceled);
            this.tpInformation.Controls.Add(this.btClearOp5);
            this.tpInformation.Controls.Add(this.cbOperator5);
            this.tpInformation.Controls.Add(this.lbOperator5);
            this.tpInformation.Controls.Add(this.btClearOp4);
            this.tpInformation.Controls.Add(this.cbOperator4);
            this.tpInformation.Controls.Add(this.lbOperator4);
            this.tpInformation.Controls.Add(this.btClearOp3);
            this.tpInformation.Controls.Add(this.cbOperator3);
            this.tpInformation.Controls.Add(this.lbOperator3);
            this.tpInformation.Controls.Add(this.btClearOp2);
            this.tpInformation.Controls.Add(this.cbOperator2);
            this.tpInformation.Controls.Add(this.lbOperator2);
            this.tpInformation.Controls.Add(this.btClearOp1);
            this.tpInformation.Controls.Add(this.cbOperator1);
            this.tpInformation.Controls.Add(this.lbOperator1);
            this.tpInformation.Controls.Add(this.cbPlace);
            this.tpInformation.Controls.Add(this.lbPlace);
            this.tpInformation.Controls.Add(this.cbEquipment);
            this.tpInformation.Controls.Add(this.lbEquipment);
            this.tpInformation.Controls.Add(this.cbWard);
            this.tpInformation.Controls.Add(this.lbWard);
            this.tpInformation.Controls.Add(this.label2);
            this.tpInformation.Controls.Add(this.cbOrderDr);
            this.tpInformation.Controls.Add(this.cbDepartment);
            this.tpInformation.Controls.Add(this.lbDepartment);
            this.tpInformation.Controls.Add(this.tbPurpose);
            this.tpInformation.Controls.Add(this.lbPurpose);
            this.tpInformation.Controls.Add(this.lbDate);
            this.tpInformation.Name = "tpInformation";
            this.toolTip1.SetToolTip(this.tpInformation, resources.GetString("tpInformation.ToolTip"));
            this.tpInformation.UseVisualStyleBackColor = true;
            // 
            // btExamCanceled
            // 
            resources.ApplyResources(this.btExamCanceled, "btExamCanceled");
            this.btExamCanceled.Name = "btExamCanceled";
            this.toolTip1.SetToolTip(this.btExamCanceled, resources.GetString("btExamCanceled.ToolTip"));
            this.btExamCanceled.UseVisualStyleBackColor = true;
            this.btExamCanceled.Click += new System.EventHandler(this.btExamCanceled_Click);
            // 
            // btClearOp5
            // 
            resources.ApplyResources(this.btClearOp5, "btClearOp5");
            this.btClearOp5.Name = "btClearOp5";
            this.toolTip1.SetToolTip(this.btClearOp5, resources.GetString("btClearOp5.ToolTip"));
            this.btClearOp5.UseVisualStyleBackColor = true;
            this.btClearOp5.Click += new System.EventHandler(this.btClearOp5_Click);
            // 
            // cbOperator5
            // 
            resources.ApplyResources(this.cbOperator5, "cbOperator5");
            this.cbOperator5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperator5.FormattingEnabled = true;
            this.cbOperator5.Name = "cbOperator5";
            this.toolTip1.SetToolTip(this.cbOperator5, resources.GetString("cbOperator5.ToolTip"));
            this.cbOperator5.SelectedIndexChanged += new System.EventHandler(this.cbOperator5_SelectedIndexChanged);
            // 
            // lbOperator5
            // 
            resources.ApplyResources(this.lbOperator5, "lbOperator5");
            this.lbOperator5.Name = "lbOperator5";
            this.toolTip1.SetToolTip(this.lbOperator5, resources.GetString("lbOperator5.ToolTip"));
            // 
            // btClearOp4
            // 
            resources.ApplyResources(this.btClearOp4, "btClearOp4");
            this.btClearOp4.Name = "btClearOp4";
            this.toolTip1.SetToolTip(this.btClearOp4, resources.GetString("btClearOp4.ToolTip"));
            this.btClearOp4.UseVisualStyleBackColor = true;
            this.btClearOp4.Click += new System.EventHandler(this.btClearOp4_Click);
            // 
            // cbOperator4
            // 
            resources.ApplyResources(this.cbOperator4, "cbOperator4");
            this.cbOperator4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperator4.FormattingEnabled = true;
            this.cbOperator4.Name = "cbOperator4";
            this.toolTip1.SetToolTip(this.cbOperator4, resources.GetString("cbOperator4.ToolTip"));
            this.cbOperator4.SelectedIndexChanged += new System.EventHandler(this.cbOperator4_SelectedIndexChanged);
            // 
            // lbOperator4
            // 
            resources.ApplyResources(this.lbOperator4, "lbOperator4");
            this.lbOperator4.Name = "lbOperator4";
            this.toolTip1.SetToolTip(this.lbOperator4, resources.GetString("lbOperator4.ToolTip"));
            // 
            // btClearOp3
            // 
            resources.ApplyResources(this.btClearOp3, "btClearOp3");
            this.btClearOp3.Name = "btClearOp3";
            this.toolTip1.SetToolTip(this.btClearOp3, resources.GetString("btClearOp3.ToolTip"));
            this.btClearOp3.UseVisualStyleBackColor = true;
            this.btClearOp3.Click += new System.EventHandler(this.btClearOp3_Click);
            // 
            // cbOperator3
            // 
            resources.ApplyResources(this.cbOperator3, "cbOperator3");
            this.cbOperator3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperator3.FormattingEnabled = true;
            this.cbOperator3.Name = "cbOperator3";
            this.toolTip1.SetToolTip(this.cbOperator3, resources.GetString("cbOperator3.ToolTip"));
            this.cbOperator3.SelectedIndexChanged += new System.EventHandler(this.cbOperator3_SelectedIndexChanged);
            // 
            // lbOperator3
            // 
            resources.ApplyResources(this.lbOperator3, "lbOperator3");
            this.lbOperator3.Name = "lbOperator3";
            this.toolTip1.SetToolTip(this.lbOperator3, resources.GetString("lbOperator3.ToolTip"));
            // 
            // btClearOp2
            // 
            resources.ApplyResources(this.btClearOp2, "btClearOp2");
            this.btClearOp2.Name = "btClearOp2";
            this.toolTip1.SetToolTip(this.btClearOp2, resources.GetString("btClearOp2.ToolTip"));
            this.btClearOp2.UseVisualStyleBackColor = true;
            this.btClearOp2.Click += new System.EventHandler(this.btClearOp2_Click);
            // 
            // cbOperator2
            // 
            resources.ApplyResources(this.cbOperator2, "cbOperator2");
            this.cbOperator2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperator2.FormattingEnabled = true;
            this.cbOperator2.Name = "cbOperator2";
            this.toolTip1.SetToolTip(this.cbOperator2, resources.GetString("cbOperator2.ToolTip"));
            this.cbOperator2.SelectedIndexChanged += new System.EventHandler(this.cbOperator2_SelectedIndexChanged);
            // 
            // lbOperator2
            // 
            resources.ApplyResources(this.lbOperator2, "lbOperator2");
            this.lbOperator2.Name = "lbOperator2";
            this.toolTip1.SetToolTip(this.lbOperator2, resources.GetString("lbOperator2.ToolTip"));
            // 
            // btClearOp1
            // 
            resources.ApplyResources(this.btClearOp1, "btClearOp1");
            this.btClearOp1.Name = "btClearOp1";
            this.toolTip1.SetToolTip(this.btClearOp1, resources.GetString("btClearOp1.ToolTip"));
            this.btClearOp1.UseVisualStyleBackColor = true;
            this.btClearOp1.Click += new System.EventHandler(this.btClearOp1_Click);
            // 
            // cbOperator1
            // 
            resources.ApplyResources(this.cbOperator1, "cbOperator1");
            this.cbOperator1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperator1.FormattingEnabled = true;
            this.cbOperator1.Name = "cbOperator1";
            this.toolTip1.SetToolTip(this.cbOperator1, resources.GetString("cbOperator1.ToolTip"));
            this.cbOperator1.SelectedIndexChanged += new System.EventHandler(this.cbOperator1_SelectedIndexChanged);
            // 
            // lbOperator1
            // 
            resources.ApplyResources(this.lbOperator1, "lbOperator1");
            this.lbOperator1.Name = "lbOperator1";
            this.toolTip1.SetToolTip(this.lbOperator1, resources.GetString("lbOperator1.ToolTip"));
            // 
            // cbPlace
            // 
            resources.ApplyResources(this.cbPlace, "cbPlace");
            this.cbPlace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlace.FormattingEnabled = true;
            this.cbPlace.Name = "cbPlace";
            this.toolTip1.SetToolTip(this.cbPlace, resources.GetString("cbPlace.ToolTip"));
            this.cbPlace.SelectedIndexChanged += new System.EventHandler(this.cbPlace_SelectedIndexChanged);
            // 
            // lbPlace
            // 
            resources.ApplyResources(this.lbPlace, "lbPlace");
            this.lbPlace.Name = "lbPlace";
            this.toolTip1.SetToolTip(this.lbPlace, resources.GetString("lbPlace.ToolTip"));
            // 
            // cbEquipment
            // 
            resources.ApplyResources(this.cbEquipment, "cbEquipment");
            this.cbEquipment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEquipment.FormattingEnabled = true;
            this.cbEquipment.Name = "cbEquipment";
            this.toolTip1.SetToolTip(this.cbEquipment, resources.GetString("cbEquipment.ToolTip"));
            this.cbEquipment.SelectedIndexChanged += new System.EventHandler(this.cbEquipment_SelectedIndexChanged);
            // 
            // lbEquipment
            // 
            resources.ApplyResources(this.lbEquipment, "lbEquipment");
            this.lbEquipment.Name = "lbEquipment";
            this.toolTip1.SetToolTip(this.lbEquipment, resources.GetString("lbEquipment.ToolTip"));
            // 
            // cbWard
            // 
            resources.ApplyResources(this.cbWard, "cbWard");
            this.cbWard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWard.FormattingEnabled = true;
            this.cbWard.Name = "cbWard";
            this.toolTip1.SetToolTip(this.cbWard, resources.GetString("cbWard.ToolTip"));
            this.cbWard.SelectedIndexChanged += new System.EventHandler(this.cbWard_SelectedIndexChanged);
            // 
            // lbWard
            // 
            resources.ApplyResources(this.lbWard, "lbWard");
            this.lbWard.Name = "lbWard";
            this.toolTip1.SetToolTip(this.lbWard, resources.GetString("lbWard.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // cbOrderDr
            // 
            resources.ApplyResources(this.cbOrderDr, "cbOrderDr");
            this.cbOrderDr.Name = "cbOrderDr";
            this.toolTip1.SetToolTip(this.cbOrderDr, resources.GetString("cbOrderDr.ToolTip"));
            this.cbOrderDr.SelectedIndexChanged += new System.EventHandler(this.cbOrderDr_SelectedIndexChanged);
            this.cbOrderDr.TextUpdate += new System.EventHandler(this.cbOrderDr_TextUpdate);
            this.cbOrderDr.TextChanged += new System.EventHandler(this.cbOrderDr_TextChanged);
            // 
            // cbDepartment
            // 
            resources.ApplyResources(this.cbDepartment, "cbDepartment");
            this.cbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDepartment.FormattingEnabled = true;
            this.cbDepartment.Name = "cbDepartment";
            this.toolTip1.SetToolTip(this.cbDepartment, resources.GetString("cbDepartment.ToolTip"));
            this.cbDepartment.SelectedIndexChanged += new System.EventHandler(this.cbDepartment_SelectedIndexChanged);
            // 
            // lbDepartment
            // 
            resources.ApplyResources(this.lbDepartment, "lbDepartment");
            this.lbDepartment.Name = "lbDepartment";
            this.toolTip1.SetToolTip(this.lbDepartment, resources.GetString("lbDepartment.ToolTip"));
            // 
            // tbPurpose
            // 
            resources.ApplyResources(this.tbPurpose, "tbPurpose");
            this.tbPurpose.Name = "tbPurpose";
            this.toolTip1.SetToolTip(this.tbPurpose, resources.GetString("tbPurpose.ToolTip"));
            this.tbPurpose.TextChanged += new System.EventHandler(this.tbPurpose_TextChanged);
            // 
            // lbPurpose
            // 
            resources.ApplyResources(this.lbPurpose, "lbPurpose");
            this.lbPurpose.Name = "lbPurpose";
            this.toolTip1.SetToolTip(this.lbPurpose, resources.GetString("lbPurpose.ToolTip"));
            // 
            // lbDate
            // 
            resources.ApplyResources(this.lbDate, "lbDate");
            this.lbDate.Name = "lbDate";
            this.toolTip1.SetToolTip(this.lbDate, resources.GetString("lbDate.ToolTip"));
            // 
            // tpFindings
            // 
            resources.ApplyResources(this.tpFindings, "tpFindings");
            this.tpFindings.Controls.Add(this.btSetNormal);
            this.tpFindings.Controls.Add(this.lbFindings);
            this.tpFindings.Controls.Add(this.cbChecker);
            this.tpFindings.Controls.Add(this.cbDiagnosed);
            this.tpFindings.Controls.Add(this.lbDiagnosed);
            this.tpFindings.Controls.Add(this.btDiagnosed);
            this.tpFindings.Controls.Add(this.btChecked);
            this.tpFindings.Controls.Add(this.lbChecker);
            this.tpFindings.Controls.Add(this.tbComment);
            this.tpFindings.Controls.Add(this.lbCheckersComment);
            this.tpFindings.Controls.Add(this.btReverseOrder);
            this.tpFindings.Controls.Add(this.btCopyDiag);
            this.tpFindings.Controls.Add(this.tbFindings);
            this.tpFindings.Controls.Add(this.btAddDiag);
            this.tpFindings.Controls.Add(this.dgvDiagnoses);
            this.tpFindings.Name = "tpFindings";
            this.toolTip1.SetToolTip(this.tpFindings, resources.GetString("tpFindings.ToolTip"));
            this.tpFindings.UseVisualStyleBackColor = true;
            // 
            // btSetNormal
            // 
            resources.ApplyResources(this.btSetNormal, "btSetNormal");
            this.btSetNormal.Name = "btSetNormal";
            this.toolTip1.SetToolTip(this.btSetNormal, resources.GetString("btSetNormal.ToolTip"));
            this.btSetNormal.UseVisualStyleBackColor = true;
            this.btSetNormal.Click += new System.EventHandler(this.btSetNormal_Click);
            // 
            // lbFindings
            // 
            resources.ApplyResources(this.lbFindings, "lbFindings");
            this.lbFindings.Name = "lbFindings";
            this.toolTip1.SetToolTip(this.lbFindings, resources.GetString("lbFindings.ToolTip"));
            // 
            // cbChecker
            // 
            resources.ApplyResources(this.cbChecker, "cbChecker");
            this.cbChecker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChecker.FormattingEnabled = true;
            this.cbChecker.Name = "cbChecker";
            this.toolTip1.SetToolTip(this.cbChecker, resources.GetString("cbChecker.ToolTip"));
            // 
            // cbDiagnosed
            // 
            resources.ApplyResources(this.cbDiagnosed, "cbDiagnosed");
            this.cbDiagnosed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDiagnosed.FormattingEnabled = true;
            this.cbDiagnosed.Name = "cbDiagnosed";
            this.toolTip1.SetToolTip(this.cbDiagnosed, resources.GetString("cbDiagnosed.ToolTip"));
            // 
            // lbDiagnosed
            // 
            resources.ApplyResources(this.lbDiagnosed, "lbDiagnosed");
            this.lbDiagnosed.Name = "lbDiagnosed";
            this.toolTip1.SetToolTip(this.lbDiagnosed, resources.GetString("lbDiagnosed.ToolTip"));
            // 
            // btDiagnosed
            // 
            resources.ApplyResources(this.btDiagnosed, "btDiagnosed");
            this.btDiagnosed.Name = "btDiagnosed";
            this.toolTip1.SetToolTip(this.btDiagnosed, resources.GetString("btDiagnosed.ToolTip"));
            this.btDiagnosed.UseVisualStyleBackColor = true;
            this.btDiagnosed.Click += new System.EventHandler(this.btDiagnosed_Click);
            // 
            // btChecked
            // 
            resources.ApplyResources(this.btChecked, "btChecked");
            this.btChecked.Name = "btChecked";
            this.toolTip1.SetToolTip(this.btChecked, resources.GetString("btChecked.ToolTip"));
            this.btChecked.UseVisualStyleBackColor = true;
            this.btChecked.Click += new System.EventHandler(this.btChecked_Click);
            // 
            // lbChecker
            // 
            resources.ApplyResources(this.lbChecker, "lbChecker");
            this.lbChecker.Name = "lbChecker";
            this.toolTip1.SetToolTip(this.lbChecker, resources.GetString("lbChecker.ToolTip"));
            // 
            // tbComment
            // 
            resources.ApplyResources(this.tbComment, "tbComment");
            this.tbComment.Name = "tbComment";
            this.toolTip1.SetToolTip(this.tbComment, resources.GetString("tbComment.ToolTip"));
            this.tbComment.Click += new System.EventHandler(this.tbComment_Click);
            this.tbComment.TextChanged += new System.EventHandler(this.tbComment_TextChanged);
            this.tbComment.Enter += new System.EventHandler(this.tbComment_Enter);
            this.tbComment.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbComment_KeyUp);
            this.tbComment.Leave += new System.EventHandler(this.tbComment_Leave);
            // 
            // lbCheckersComment
            // 
            resources.ApplyResources(this.lbCheckersComment, "lbCheckersComment");
            this.lbCheckersComment.Name = "lbCheckersComment";
            this.toolTip1.SetToolTip(this.lbCheckersComment, resources.GetString("lbCheckersComment.ToolTip"));
            // 
            // btReverseOrder
            // 
            resources.ApplyResources(this.btReverseOrder, "btReverseOrder");
            this.btReverseOrder.Name = "btReverseOrder";
            this.toolTip1.SetToolTip(this.btReverseOrder, resources.GetString("btReverseOrder.ToolTip"));
            this.btReverseOrder.UseVisualStyleBackColor = true;
            this.btReverseOrder.Click += new System.EventHandler(this.btReverseOrder_Click);
            // 
            // btCopyDiag
            // 
            resources.ApplyResources(this.btCopyDiag, "btCopyDiag");
            this.btCopyDiag.Name = "btCopyDiag";
            this.toolTip1.SetToolTip(this.btCopyDiag, resources.GetString("btCopyDiag.ToolTip"));
            this.btCopyDiag.UseVisualStyleBackColor = true;
            this.btCopyDiag.Click += new System.EventHandler(this.btCopyDiag_Click);
            // 
            // tbFindings
            // 
            resources.ApplyResources(this.tbFindings, "tbFindings");
            this.tbFindings.Name = "tbFindings";
            this.toolTip1.SetToolTip(this.tbFindings, resources.GetString("tbFindings.ToolTip"));
            this.tbFindings.Click += new System.EventHandler(this.tbFindings_Click);
            this.tbFindings.TextChanged += new System.EventHandler(this.tbFindings_TextChanged);
            this.tbFindings.Enter += new System.EventHandler(this.tbFindings_Enter);
            this.tbFindings.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbFindings_KeyUp);
            this.tbFindings.Leave += new System.EventHandler(this.tbFindings_Leave);
            // 
            // btAddDiag
            // 
            resources.ApplyResources(this.btAddDiag, "btAddDiag");
            this.btAddDiag.Name = "btAddDiag";
            this.toolTip1.SetToolTip(this.btAddDiag, resources.GetString("btAddDiag.ToolTip"));
            this.btAddDiag.UseVisualStyleBackColor = true;
            this.btAddDiag.Click += new System.EventHandler(this.btAddDiag_Click);
            // 
            // dgvDiagnoses
            // 
            resources.ApplyResources(this.dgvDiagnoses, "dgvDiagnoses");
            this.dgvDiagnoses.AllowUserToAddRows = false;
            this.dgvDiagnoses.AllowUserToDeleteRows = false;
            this.dgvDiagnoses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDiagnoses.ColumnHeadersVisible = false;
            this.dgvDiagnoses.Name = "dgvDiagnoses";
            this.dgvDiagnoses.ReadOnly = true;
            this.dgvDiagnoses.RowHeadersVisible = false;
            this.dgvDiagnoses.RowTemplate.Height = 21;
            this.toolTip1.SetToolTip(this.dgvDiagnoses, resources.GetString("dgvDiagnoses.ToolTip"));
            this.dgvDiagnoses.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDiagnoses_CellContentClick);
            // 
            // tpFigure1
            // 
            resources.ApplyResources(this.tpFigure1, "tpFigure1");
            this.tpFigure1.Controls.Add(this.btErace1);
            this.tpFigure1.Controls.Add(this.cbBrushWidth1);
            this.tpFigure1.Controls.Add(this.pbBrushWidth1);
            this.tpFigure1.Controls.Add(this.pbColor1);
            this.tpFigure1.Controls.Add(this.btColorSelect1);
            this.tpFigure1.Controls.Add(this.pbFigure1);
            this.tpFigure1.Name = "tpFigure1";
            this.toolTip1.SetToolTip(this.tpFigure1, resources.GetString("tpFigure1.ToolTip"));
            this.tpFigure1.UseVisualStyleBackColor = true;
            // 
            // btErace1
            // 
            resources.ApplyResources(this.btErace1, "btErace1");
            this.btErace1.Name = "btErace1";
            this.toolTip1.SetToolTip(this.btErace1, resources.GetString("btErace1.ToolTip"));
            this.btErace1.UseVisualStyleBackColor = true;
            this.btErace1.Click += new System.EventHandler(this.btErace_Click);
            // 
            // cbBrushWidth1
            // 
            resources.ApplyResources(this.cbBrushWidth1, "cbBrushWidth1");
            this.cbBrushWidth1.DisplayMember = "1 px;2px";
            this.cbBrushWidth1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBrushWidth1.FormattingEnabled = true;
            this.cbBrushWidth1.Name = "cbBrushWidth1";
            this.toolTip1.SetToolTip(this.cbBrushWidth1, resources.GetString("cbBrushWidth1.ToolTip"));
            this.cbBrushWidth1.SelectedIndexChanged += new System.EventHandler(this.cbBrushWidth1_SelectedIndexChanged);
            // 
            // pbBrushWidth1
            // 
            resources.ApplyResources(this.pbBrushWidth1, "pbBrushWidth1");
            this.pbBrushWidth1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbBrushWidth1.Name = "pbBrushWidth1";
            this.pbBrushWidth1.TabStop = false;
            this.toolTip1.SetToolTip(this.pbBrushWidth1, resources.GetString("pbBrushWidth1.ToolTip"));
            // 
            // pbColor1
            // 
            resources.ApplyResources(this.pbColor1, "pbColor1");
            this.pbColor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbColor1.Name = "pbColor1";
            this.pbColor1.TabStop = false;
            this.toolTip1.SetToolTip(this.pbColor1, resources.GetString("pbColor1.ToolTip"));
            // 
            // btColorSelect1
            // 
            resources.ApplyResources(this.btColorSelect1, "btColorSelect1");
            this.btColorSelect1.Name = "btColorSelect1";
            this.toolTip1.SetToolTip(this.btColorSelect1, resources.GetString("btColorSelect1.ToolTip"));
            this.btColorSelect1.UseVisualStyleBackColor = true;
            this.btColorSelect1.Click += new System.EventHandler(this.btColorSelect_Click);
            // 
            // pbFigure1
            // 
            resources.ApplyResources(this.pbFigure1, "pbFigure1");
            this.pbFigure1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbFigure1.Name = "pbFigure1";
            this.pbFigure1.TabStop = false;
            this.toolTip1.SetToolTip(this.pbFigure1, resources.GetString("pbFigure1.ToolTip"));
            this.pbFigure1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbFigure1_MouseDown);
            this.pbFigure1.MouseLeave += new System.EventHandler(this.pbFigure1_MouseLeave);
            this.pbFigure1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbFigure1_MouseMove);
            this.pbFigure1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbFigure1_MouseUp);
            // 
            // tpFigure2
            // 
            resources.ApplyResources(this.tpFigure2, "tpFigure2");
            this.tpFigure2.Controls.Add(this.btErace2);
            this.tpFigure2.Controls.Add(this.cbBrushWidth2);
            this.tpFigure2.Controls.Add(this.pbBrushWidth2);
            this.tpFigure2.Controls.Add(this.pbColor2);
            this.tpFigure2.Controls.Add(this.btColorSelect2);
            this.tpFigure2.Controls.Add(this.pbFigure2);
            this.tpFigure2.Name = "tpFigure2";
            this.toolTip1.SetToolTip(this.tpFigure2, resources.GetString("tpFigure2.ToolTip"));
            this.tpFigure2.UseVisualStyleBackColor = true;
            // 
            // btErace2
            // 
            resources.ApplyResources(this.btErace2, "btErace2");
            this.btErace2.Name = "btErace2";
            this.toolTip1.SetToolTip(this.btErace2, resources.GetString("btErace2.ToolTip"));
            this.btErace2.UseVisualStyleBackColor = true;
            this.btErace2.Click += new System.EventHandler(this.btErace_Click);
            // 
            // cbBrushWidth2
            // 
            resources.ApplyResources(this.cbBrushWidth2, "cbBrushWidth2");
            this.cbBrushWidth2.DisplayMember = "1 px;2px";
            this.cbBrushWidth2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBrushWidth2.FormattingEnabled = true;
            this.cbBrushWidth2.Name = "cbBrushWidth2";
            this.toolTip1.SetToolTip(this.cbBrushWidth2, resources.GetString("cbBrushWidth2.ToolTip"));
            this.cbBrushWidth2.SelectedIndexChanged += new System.EventHandler(this.cbBrushWidth2_SelectedIndexChanged);
            // 
            // pbBrushWidth2
            // 
            resources.ApplyResources(this.pbBrushWidth2, "pbBrushWidth2");
            this.pbBrushWidth2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbBrushWidth2.Name = "pbBrushWidth2";
            this.pbBrushWidth2.TabStop = false;
            this.toolTip1.SetToolTip(this.pbBrushWidth2, resources.GetString("pbBrushWidth2.ToolTip"));
            // 
            // pbColor2
            // 
            resources.ApplyResources(this.pbColor2, "pbColor2");
            this.pbColor2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbColor2.Name = "pbColor2";
            this.pbColor2.TabStop = false;
            this.toolTip1.SetToolTip(this.pbColor2, resources.GetString("pbColor2.ToolTip"));
            // 
            // btColorSelect2
            // 
            resources.ApplyResources(this.btColorSelect2, "btColorSelect2");
            this.btColorSelect2.Name = "btColorSelect2";
            this.toolTip1.SetToolTip(this.btColorSelect2, resources.GetString("btColorSelect2.ToolTip"));
            this.btColorSelect2.UseVisualStyleBackColor = true;
            this.btColorSelect2.Click += new System.EventHandler(this.btColorSelect_Click);
            // 
            // pbFigure2
            // 
            resources.ApplyResources(this.pbFigure2, "pbFigure2");
            this.pbFigure2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbFigure2.Name = "pbFigure2";
            this.pbFigure2.TabStop = false;
            this.toolTip1.SetToolTip(this.pbFigure2, resources.GetString("pbFigure2.ToolTip"));
            this.pbFigure2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbFigure2_MouseDown);
            this.pbFigure2.MouseLeave += new System.EventHandler(this.pbFigure2_MouseLeave);
            this.pbFigure2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbFigure2_MouseMove);
            this.pbFigure2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbFigure2_MouseUp);
            // 
            // cbExamStatus
            // 
            resources.ApplyResources(this.cbExamStatus, "cbExamStatus");
            this.cbExamStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExamStatus.FormattingEnabled = true;
            this.cbExamStatus.Name = "cbExamStatus";
            this.toolTip1.SetToolTip(this.cbExamStatus, resources.GetString("cbExamStatus.ToolTip"));
            // 
            // dgvWords
            // 
            resources.ApplyResources(this.dgvWords, "dgvWords");
            this.dgvWords.AllowUserToAddRows = false;
            this.dgvWords.AllowUserToDeleteRows = false;
            this.dgvWords.AllowUserToResizeColumns = false;
            this.dgvWords.AllowUserToResizeRows = false;
            this.dgvWords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWords.ColumnHeadersVisible = false;
            this.dgvWords.MultiSelect = false;
            this.dgvWords.Name = "dgvWords";
            this.dgvWords.ReadOnly = true;
            this.dgvWords.RowHeadersVisible = false;
            this.dgvWords.RowTemplate.Height = 21;
            this.toolTip1.SetToolTip(this.dgvWords, resources.GetString("dgvWords.ToolTip"));
            this.dgvWords.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWords_CellClick);
            // 
            // btCopy2ClipBoard
            // 
            resources.ApplyResources(this.btCopy2ClipBoard, "btCopy2ClipBoard");
            this.btCopy2ClipBoard.Name = "btCopy2ClipBoard";
            this.toolTip1.SetToolTip(this.btCopy2ClipBoard, resources.GetString("btCopy2ClipBoard.ToolTip"));
            this.btCopy2ClipBoard.UseVisualStyleBackColor = true;
            this.btCopy2ClipBoard.Click += new System.EventHandler(this.btCopy2ClipBoard_Click);
            // 
            // btSaveClose
            // 
            resources.ApplyResources(this.btSaveClose, "btSaveClose");
            this.btSaveClose.Name = "btSaveClose";
            this.toolTip1.SetToolTip(this.btSaveClose, resources.GetString("btSaveClose.ToolTip"));
            this.btSaveClose.UseVisualStyleBackColor = true;
            this.btSaveClose.Click += new System.EventHandler(this.btSaveClose_Click);
            // 
            // EditFindings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btSaveClose);
            this.Controls.Add(this.btCopy2ClipBoard);
            this.Controls.Add(this.dgvWords);
            this.Controls.Add(this.cbExamStatus);
            this.Controls.Add(this.tcExam);
            this.Controls.Add(this.lbPatient);
            this.Name = "EditFindings";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditFindings_FormClosing);
            this.tcExam.ResumeLayout(false);
            this.tpInformation.ResumeLayout(false);
            this.tpInformation.PerformLayout();
            this.tpFindings.ResumeLayout(false);
            this.tpFindings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiagnoses)).EndInit();
            this.tpFigure1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbBrushWidth1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFigure1)).EndInit();
            this.tpFigure2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbBrushWidth2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFigure2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWords)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbPatient;
        private System.Windows.Forms.TabControl tcExam;
        private System.Windows.Forms.TabPage tpInformation;
        private System.Windows.Forms.TabPage tpFindings;
        private System.Windows.Forms.Label lbDate;
        private System.Windows.Forms.TextBox tbPurpose;
        private System.Windows.Forms.Label lbPurpose;
        private System.Windows.Forms.ComboBox cbDepartment;
        private System.Windows.Forms.Label lbDepartment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbOrderDr;
        private System.Windows.Forms.ComboBox cbWard;
        private System.Windows.Forms.Label lbWard;
        private System.Windows.Forms.ComboBox cbEquipment;
        private System.Windows.Forms.Label lbEquipment;
        private System.Windows.Forms.ComboBox cbPlace;
        private System.Windows.Forms.Label lbPlace;
        private System.Windows.Forms.ComboBox cbOperator1;
        private System.Windows.Forms.Label lbOperator1;
        private System.Windows.Forms.Button btClearOp1;
        private System.Windows.Forms.Button btClearOp5;
        private System.Windows.Forms.ComboBox cbOperator5;
        private System.Windows.Forms.Label lbOperator5;
        private System.Windows.Forms.Button btClearOp4;
        private System.Windows.Forms.ComboBox cbOperator4;
        private System.Windows.Forms.Label lbOperator4;
        private System.Windows.Forms.Button btClearOp3;
        private System.Windows.Forms.ComboBox cbOperator3;
        private System.Windows.Forms.Label lbOperator3;
        private System.Windows.Forms.Button btClearOp2;
        private System.Windows.Forms.ComboBox cbOperator2;
        private System.Windows.Forms.Label lbOperator2;
        private System.Windows.Forms.DataGridView dgvDiagnoses;
        private System.Windows.Forms.Button btAddDiag;
        private System.Windows.Forms.TextBox tbFindings;
        private System.Windows.Forms.Button btCopyDiag;
        private System.Windows.Forms.Button btReverseOrder;
        private System.Windows.Forms.Button btChecked;
        private System.Windows.Forms.Label lbChecker;
        private System.Windows.Forms.TextBox tbComment;
        private System.Windows.Forms.Label lbCheckersComment;
        private System.Windows.Forms.Label lbDiagnosed;
        private System.Windows.Forms.Button btDiagnosed;
        private System.Windows.Forms.ComboBox cbChecker;
        private System.Windows.Forms.ComboBox cbDiagnosed;
        private System.Windows.Forms.ComboBox cbExamStatus;
        private System.Windows.Forms.Button btExamCanceled;
        private System.Windows.Forms.TabPage tpFigure1;
        private System.Windows.Forms.PictureBox pbFigure1;
        private System.Windows.Forms.Button btColorSelect1;
        private System.Windows.Forms.PictureBox pbColor1;
        private System.Windows.Forms.PictureBox pbBrushWidth1;
        private System.Windows.Forms.ComboBox cbBrushWidth1;
        private System.Windows.Forms.TabPage tpFigure2;
        private System.Windows.Forms.ComboBox cbBrushWidth2;
        private System.Windows.Forms.PictureBox pbBrushWidth2;
        private System.Windows.Forms.PictureBox pbColor2;
        private System.Windows.Forms.Button btColorSelect2;
        private System.Windows.Forms.PictureBox pbFigure2;
        private System.Windows.Forms.Button btErace1;
        private System.Windows.Forms.Button btErace2;
        private System.Windows.Forms.DataGridView dgvWords;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lbFindings;
        private System.Windows.Forms.Button btCopy2ClipBoard;
        private System.Windows.Forms.Button btSetNormal;
        private System.Windows.Forms.Button btSaveClose;
    }
}
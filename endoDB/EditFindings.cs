using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Npgsql;
using System.IO;
using System.Collections;

namespace endoDB
{
    public partial class EditFindings : Form
    {
        private Exam exam;
        private Boolean canEdit;
        private Boolean canEditPatho; //This will control the right of edit pathology results.
        private DataTable dt = new DataTable();//Temporary datatable for diagnoses.
        private DataTable stockedSQLs = new DataTable();
        private int no4SqlIndex = 0;
        private Color currentColor = Color.Black;
        private Boolean isDrawing = false;
        private Point prevLocation;
        private DataView dvWords = new DataView(CLocalDB.localDB.Tables["words"]);
        private string selectedTb; //TextBox name where insert words.
        private int selectionStart = -1; //Insertion point for words.
        private Timer timer = new Timer();
        private string figureFileNameBase;
        private FileStream fs;
        private Boolean edited;

        public EditFindings(string _exam_id)
        {
            InitializeComponent();
            exam = new Exam(_exam_id);

            #region canEdit & Timer
            canEdit = true;
            canEditPatho = true;

            #region editorial control
            if (db_operator.canDiag == false)
            { canEdit = false; }
            else if (db_operator.allowFC == false && exam.exam_status == 3)
            { canEdit = false; }
            #endregion

            #region Timer
            //Check locktime after checking of editorial control above
            canEditPatho = uckyFunctions.canEdit("exam", "exam_id", "=", "'" + exam.exam_id.ToString() + "'");

            if (canEdit)
            { canEdit = canEditPatho; }

            if (canEditPatho)
            {
                uckyFunctions.updateLockTimeIP("exam", "exam_id", "=", "'" + exam.exam_id.ToString() + "'");

                //Timer procedure
                timer.Interval = 30000;  //Unit is msec
                timer.Tick += new EventHandler(timer_Tick);
                timer.Start();

                if (!canEdit)
                { MessageBox.Show(Properties.Resources.YouCanMakeChangesOnlyToPatho, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            else
            {
                //MessageBox.Show(Properties.Resources.ReadOnlyMode + Environment.NewLine + Properties.Resources.ChangesWillLost, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show(Properties.Resources.ReadOnlyMode, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            #endregion

            #endregion

            #region Header
            this.lbPatient.Text = "ID: " + exam.pt_id + "   " + Properties.Resources.Name + ": " + exam.pt_name;

            if (!canEdit)
            { btSaveClose.Text = Properties.Resources.Close; }

            //cbExamStatus initialization
            this.cbExamStatus.DataSource = CLocalDB.localDB.Tables["exam_status"];
            this.cbExamStatus.ValueMember = "status_no";
            this.cbExamStatus.DisplayMember = "status_name";
            if (string.IsNullOrWhiteSpace(exam.exam_status.ToString()) || (exam.exam_status == 0))
            { this.cbExamStatus.SelectedValue = 1; }
            else
            { this.cbExamStatus.SelectedValue = exam.exam_status; }
            #endregion

            #region information
            this.lbDate.Text = Properties.Resources.Date + ": " + exam.exam_day.ToLongDateString()
                + "    " + Properties.Resources.ExamType + ": " + exam.getExamTypeName();

            #region tbPurpose initialization
            this.tbPurpose.Text = exam.purpose;
            if (canEdit)
            { tbPurpose.ReadOnly = false; }
            else
            { tbPurpose.ReadOnly = true; }
            #endregion

            #region cbDepartment initialization
            this.cbDepartment.DataSource = CLocalDB.localDB.Tables["department"];
            this.cbDepartment.ValueMember = "code";
            this.cbDepartment.DisplayMember = "name1";
            this.cbDepartment.SelectedValue = exam.department;
            if (!canEdit)
            { cbDepartment.Enabled = false; }
            #endregion

            #region cbOrderDr initialization
            this.cbOrderDr.DataSource = CLocalDB.localDB.Tables["orderDr"];
            this.cbOrderDr.DisplayMember = "op_name";
            if (string.IsNullOrWhiteSpace(exam.order_dr))
            { cbOrderDr.SelectedIndex = -1; }
            else
            {
                cbOrderDr.SelectedIndex = -1;//これをしないと項目にないテキストがちゃんと反映されない。
                this.cbOrderDr.Text = exam.order_dr;
            }
            if (!canEdit)
            { cbOrderDr.Enabled = false; }
            #endregion

            #region cbWard initialization
            this.cbWard.DataSource = CLocalDB.localDB.Tables["ward"];
            this.cbWard.ValueMember = "ward_no";
            this.cbWard.DisplayMember = "ward";
            this.cbWard.SelectedValue = exam.ward_id;
            if (!canEdit)
            { cbWard.Enabled = false; }
            #endregion

            #region cbEquipment initialization
            switch (exam.exam_type)
            {
                case 1:
                    this.cbEquipment.DataSource = CLocalDB.localDB.Tables["equipmentGF"];
                    break;
                case 2:
                    this.cbEquipment.DataSource = CLocalDB.localDB.Tables["equipmentCF"];
                    break;
                case 3:
                    this.cbEquipment.DataSource = CLocalDB.localDB.Tables["equipmentSV"];
                    break;
                case 4:
                    this.cbEquipment.DataSource = CLocalDB.localDB.Tables["equipmentS"];
                    break;
                case 1001:
                case 1002:
                case 1003:
                case 1004:
                case 1005:
                case 1006:
                case 1007:
                case 1008:
                case 1009:
                case 1010:
                case 1011:
                case 1012:
                    this.cbEquipment.DataSource = CLocalDB.localDB.Tables["equipmentUS"];
                    break;
            }
            this.cbEquipment.ValueMember = "equipment_no";
            this.cbEquipment.DisplayMember = "name";
            this.cbEquipment.SelectedValue = exam.equipment;
            if (!canEdit)
            { cbEquipment.Enabled = false; }
            #endregion

            #region cbPlace initialization
            switch (exam.exam_type)
            {
                case 99:
                    this.cbPlace.DataSource = CLocalDB.localDB.Tables["placeUS"];
                    break;
                default:
                    this.cbPlace.DataSource = CLocalDB.localDB.Tables["placeEndo"];
                    break;
            }
            this.cbPlace.ValueMember = "place_no";
            this.cbPlace.DisplayMember = "name1";
            if (string.IsNullOrWhiteSpace(exam.place_no.ToString()))
            { this.cbPlace.SelectedIndex = -1; }
            else
            { this.cbPlace.SelectedValue = exam.place_no; }
            if (!canEdit)
            { cbPlace.Enabled = false; }
            #endregion

            #region cbOperator1 initialization
            this.cbOperator1.DataSource = CLocalDB.localDB.Tables["operator1"];
            this.cbOperator1.ValueMember = "operator_id";
            this.cbOperator1.DisplayMember = "op_name";
            if (string.IsNullOrWhiteSpace(exam.operator1))
            { this.cbOperator1.SelectedIndex = -1; }
            else
            { this.cbOperator1.SelectedValue = exam.operator1; }

            if (!canEdit)
            {
                cbOperator1.Enabled = false;
                btClearOp1.Visible = false;
            }

            if (shouldFillOperatorWithUser(canEdit, exam.exam_status))
            { cbOperator1.SelectedValue = db_operator.operatorID; }
            #endregion

            #region cbOperator2 initialization
            this.cbOperator2.DataSource = CLocalDB.localDB.Tables["operator2"];
            this.cbOperator2.ValueMember = "operator_id";
            this.cbOperator2.DisplayMember = "op_name";
            if (string.IsNullOrWhiteSpace(exam.operator2))
            { this.cbOperator2.SelectedIndex = -1; }
            else
            { this.cbOperator2.SelectedValue = exam.operator2; }

            if (!canEdit)
            {
                cbOperator2.Enabled = false;
                btClearOp2.Visible = false;
            }
            #endregion

            #region cbOperator3 initialization
            this.cbOperator3.DataSource = CLocalDB.localDB.Tables["operator3"];
            this.cbOperator3.ValueMember = "operator_id";
            this.cbOperator3.DisplayMember = "op_name";
            if (string.IsNullOrWhiteSpace(exam.operator3))
            { this.cbOperator3.SelectedIndex = -1; }
            else
            { this.cbOperator3.SelectedValue = exam.operator3; }

            if (!canEdit)
            {
                cbOperator3.Enabled = false;
                btClearOp3.Visible = false;
            }
            #endregion

            #region cbOperator4 initialization
            this.cbOperator4.DataSource = CLocalDB.localDB.Tables["operator4"];
            this.cbOperator4.ValueMember = "operator_id";
            this.cbOperator4.DisplayMember = "op_name";
            if (string.IsNullOrWhiteSpace(exam.operator4))
            { this.cbOperator4.SelectedIndex = -1; }
            else
            { this.cbOperator4.SelectedValue = exam.operator4; }

            if (!canEdit)
            {
                cbOperator4.Enabled = false;
                btClearOp4.Visible = false;
            }
            #endregion

            #region cbOperator5 initialization
            this.cbOperator5.DataSource = CLocalDB.localDB.Tables["operator5"];
            this.cbOperator5.ValueMember = "operator_id";
            this.cbOperator5.DisplayMember = "op_name";
            if (string.IsNullOrWhiteSpace(exam.operator5))
            { this.cbOperator5.SelectedIndex = -1; }
            else
            { this.cbOperator5.SelectedValue = exam.operator5; }

            if (!canEdit)
            {
                cbOperator5.Enabled = false;
                btClearOp5.Visible = false;
            }
            #endregion

            if (!canEdit)
            { btExamCanceled.Visible = false; }

            #endregion

            #region Findings

            #region dgvDiagnoses initialization
            this.dgvDiagnoses.Font = new Font(dgvDiagnoses.Font.Name, 12);
            this.dgvDiagnoses.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvDiagnoses.DataSource = dt;
            initiateDt();

            #region Add btDelColumn
            if (canEdit)
            {
                DataGridViewButtonColumn btDelColumn = new DataGridViewButtonColumn(); //Create DataGridViewButtonColumn
                btDelColumn.Name = Properties.Resources.Delete;  //Set column name
                btDelColumn.UseColumnTextForButtonValue = true;  //Show text on the button
                btDelColumn.Text = Properties.Resources.Delete;  //Set text on the button
                dgvDiagnoses.Columns.Add(btDelColumn);     //Add the button
            }
            #endregion

            resizeColumns();

            #region Set Visible to false
            dgvDiagnoses.Columns["diag_no"].Visible = false;
            dgvDiagnoses.Columns["diag_code"].Visible = false;
            dgvDiagnoses.Columns["suspect"].Visible = false;
            dgvDiagnoses.Columns["SQL_index"].Visible = false;
            dgvDiagnoses.Columns["premodifier"].Visible = false;
            dgvDiagnoses.Columns["postmodifier"].Visible = false;
            #endregion
            #endregion

            #region Initialize buttons beside dgvDiagnoses
            if (!canEdit)
            {
                btSetNormal.Visible = false;
                btAddDiag.Visible = false;
                btReverseOrder.Visible = false;
                btCopyDiag.Visible = false;
            }
            #endregion

            #region Initialize stockedSQLs
            stockedSQLs.Columns.Add("SQL", System.Type.GetType("System.String"));
            stockedSQLs.Columns.Add("index", System.Type.GetType("System.String"));
            #endregion

            #region tbFinding initialization
            string sql;
            if (string.IsNullOrWhiteSpace(exam.findings))
            {
                sql = "SELECT findings FROM default_findings WHERE exam_type=" + exam.exam_type.ToString();
                tbFindings.Text = uckyFunctions.getSelectString(sql, Settings.DBSrvIP, Settings.DBSrvPort, Settings.DBconnectID, Settings.DBconnectPw, "endoDB");
            }
            else
            { this.tbFindings.Text = exam.findings.Replace("\n", "\r\n").Replace("\r\r", "\r"); }//Replace code is necessary because we have data made with Linux machine.

            if (canEdit)
            { tbFindings.ReadOnly = false; }
            else
            { tbFindings.ReadOnly = true; }
            #endregion

            #region tbComment initialization
            this.tbComment.Text = exam.comment.Replace("\n", "\r\n").Replace("\r\r", "\r");

            if (canEdit)
            { tbComment.ReadOnly = false; }
            else
            { tbComment.ReadOnly = true; }
            #endregion

            #region btDiagnosed initialization
            if (db_operator.canDiag)
            { btDiagnosed.Visible = true; }
            else
            { btDiagnosed.Visible = false; }

            if (!canEdit)
            { btDiagnosed.Visible = false; }
            #endregion

            #region cbDiagnosed initialization
            this.cbDiagnosed.DataSource = CLocalDB.localDB.Tables["DiagDr"];
            this.cbDiagnosed.ValueMember = "operator_id";
            this.cbDiagnosed.DisplayMember = "op_name";
            if (string.IsNullOrWhiteSpace(exam.diag_dr))
            { this.cbDiagnosed.SelectedIndex = -1; }
            else
            { this.cbDiagnosed.SelectedValue = exam.diag_dr; }
            #endregion

            #region btChecked initialization
            if (db_operator.allowFC)
            { btChecked.Visible = true; }
            else
            { btChecked.Visible = false; }

            if (!canEdit)
            { btChecked.Visible = false; }
            #endregion

            #region cbChecker initialization
            this.cbChecker.DataSource = CLocalDB.localDB.Tables["Checker"];
            this.cbChecker.ValueMember = "operator_id";
            this.cbChecker.DisplayMember = "op_name";
            if (string.IsNullOrWhiteSpace(exam.final_diag_dr))
            { this.cbChecker.SelectedIndex = -1; }
            else
            { this.cbChecker.SelectedValue = exam.final_diag_dr; }
            #endregion

            #region tbPatho initialization
            this.tbPatho.Text = exam.patho_result.Replace("\n", "\r\n").Replace("\r\r", "\r");

            if (canEditPatho)
            { tbPatho.ReadOnly = false; }
            else
            { tbPatho.ReadOnly = true; }
            #endregion
            #endregion

            #region Figure
            checkFigureFolder(); //Check figure folder exist or not, and if folder not existed, create folder.
            figureFileNameBase = Settings.figureFolder + @"\" + exam.exam_day.Year.ToString() + @"\" + exam.exam_id.ToString();

            #region pbFigure1 initialization
            if (!System.IO.File.Exists(figureFileNameBase + "_1.png"))//If there is no figure file, fill pbFigure1 with white.
            { fillPicBoxWhite(pbFigure1); }
            else
            {
                fs = File.Open(figureFileNameBase + "_1.png", FileMode.Open, FileAccess.ReadWrite);
                pbFigure1.Image = System.Drawing.Image.FromStream(fs);
                fs.Close();
            }

            if (canEdit)
            { pbFigure1.Enabled = true; }
            else
            { pbFigure1.Enabled = false; }
            #endregion

            #region pbFigure2 initialization
            if (!System.IO.File.Exists(figureFileNameBase + "_2.png"))//If there is no figure file, fill pbFigure1 with white.
            { fillPicBoxWhite(pbFigure2); }
            else
            {
                fs = File.Open(figureFileNameBase + "_2.png", FileMode.Open, FileAccess.ReadWrite);
                pbFigure2.Image = System.Drawing.Image.FromStream(fs);
                fs.Close();
            }

            if (canEdit)
            { pbFigure2.Enabled = true; }
            else
            { pbFigure2.Enabled = false; }
            #endregion

            #region pbColor initialization
            this.pbColor1.BackColor = currentColor;
            this.pbColor2.BackColor = currentColor;
            #endregion

            #region cbBrushWidth initialization
            DataTable dtBrushWidth = new DataTable();
            dtBrushWidth.Columns.Add("width", Type.GetType("System.Int16"));
            dtBrushWidth.Columns.Add("WidthStr", Type.GetType("System.String"));
            for (int i = 2; i < 16; i++)
            { dtBrushWidth.Rows.Add(i, i.ToString() + " px"); }
            cbBrushWidth1.DataSource = dtBrushWidth;
            cbBrushWidth2.DataSource = dtBrushWidth;
            cbBrushWidth1.ValueMember = "width";
            cbBrushWidth2.ValueMember = "width";
            cbBrushWidth1.DisplayMember = "WidthStr";
            cbBrushWidth2.DisplayMember = "WidthStr";
            setPbBrushWidth();
            #endregion

            #endregion

            #region Words
            if (canEdit)
            { dgvWords.Visible = true; }
            else
            { dgvWords.Visible = false; }

            selectedTb = "";
            switch (exam.exam_type)
            {
                case 1:
                    dvWords.RowFilter = "operator='Upper endoscopy' OR operator='" + db_operator.operatorID + "'";
                    dvWords.Sort = "operator, word_order";
                    break;
                case 2:
                    dvWords.RowFilter = "operator='Colonoscopy' OR operator='" + db_operator.operatorID + "'";
                    dvWords.Sort = "operator, word_order";
                    break;
                case 1001:
                    dvWords.RowFilter = "operator='Abdomen US' OR operator='" + db_operator.operatorID + "'";
                    dvWords.Sort = "operator, word_order";
                    break;
            }
            dgvWords.DataSource = dvWords;
            dgvWords.Font = new Font(dgvWords.Font.Name, 12);
            dgvWords.Columns["no"].Visible = false;
            dgvWords.Columns["operator"].Visible = false;
            dgvWords.Columns["word_order"].Visible = false;
            #endregion

            edited = false;
        }

        #region information
        public static Boolean shouldFillOperatorWithUser(Boolean can_edit, int exam_status)
        {
            if (can_edit && exam_status == 0)
            { return true; }
            else
            { return false; }
        }

        #region ClearOperatorButton
        private void btClearOp1_Click(object sender, EventArgs e)
        {
            edited = true;
            this.cbOperator1.SelectedIndex = -1;
        }

        private void btClearOp2_Click(object sender, EventArgs e)
        {
            edited = true;
            this.cbOperator2.SelectedIndex = -1;
        }

        private void btClearOp3_Click(object sender, EventArgs e)
        {
            edited = true;
            this.cbOperator3.SelectedIndex = -1;
        }

        private void btClearOp4_Click(object sender, EventArgs e)
        {
            edited = true;
            this.cbOperator4.SelectedIndex = -1;
        }

        private void btClearOp5_Click(object sender, EventArgs e)
        {
            edited = true;
            this.cbOperator5.SelectedIndex = -1;
        }
        #endregion
        #endregion

        #region Diagnoses
        private void initiateDt()
        {
            #region Npgsql connection
            NpgsqlConnection conn;

            try
            {
                conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                    Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);
            }
            catch (ArgumentException)
            {
                MessageBox.Show(Properties.Resources.WrongConnectingString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                conn.Open();
            }
            catch (NpgsqlException)
            {
                MessageBox.Show(Properties.Resources.CouldntOpenConn, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(Properties.Resources.ConnClosed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return;
            }
            #endregion

            string sql;
            if (Settings.lang == "ja")
            {
                sql = "SELECT diag_no, diag_code, name_jp AS name, suspect, premodifier, postmodifier FROM diag INNER JOIN diag_name ON diag.diag_code = diag_name.no"
                    + " WHERE exam_no = " + exam.exam_id.ToString();
            }
            else
            {
                sql = "SELECT diag_no, diag_code, name_eng AS name, suspect, premodifier, postmodifier FROM diag INNER JOIN diag_name ON diag.diag_code = diag_name.no"
                    + " WHERE exam_no = " + exam.exam_id.ToString();
            }

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(dt);
            dt.Columns.Add("SQL_index", Type.GetType("System.Int32"));//Add index column for delete row.
            setDiagnosesStr(); //Generate diagnoses string.
            DataView dv = dt.DefaultView;
            dv.Sort = "diag_no ASC";
            conn.Close();
        }

        private void setDiagnosesStr()
        {
            string susp;
            string display_str;
            foreach (DataGridViewRow dr in dgvDiagnoses.Rows)
            {
                if (!dr.IsNewRow)
                {
                    if (string.IsNullOrWhiteSpace(dr.Cells["suspect"].Value.ToString()))
                    { susp = ""; }
                    else if ((Boolean)dr.Cells["suspect"].Value)
                    { susp = Properties.Resources.Suspect; }
                    else
                    { susp = ""; }

                    display_str = "";
                    if (!string.IsNullOrWhiteSpace(dr.Cells["premodifier"].Value.ToString()))
                    { display_str = dr.Cells["premodifier"].Value.ToString(); }

                    display_str += dr.Cells["name"].Value.ToString();

                    if (!string.IsNullOrWhiteSpace(dr.Cells["postmodifier"].Value.ToString()))
                    { display_str += dr.Cells["postmodifier"].Value.ToString(); }

                    dr.Cells["name"].Value = display_str + susp;
                }
            }
        }

        private void btAddDiag_Click(object sender, EventArgs e)
        {
            edited = true;
            AddDiagnosis ad = new AddDiagnosis(exam.exam_id, exam.exam_type);
            ad.ShowDialog(this);

            if (ad.add_diag)
            {
                #region Get data from AddDiagnosis
                #region dt
                DataRow newRow = dt.NewRow();
                newRow["diag_code"] = ad.diag_code;
                newRow["premodifier"] = ad.premodifier;
                newRow["postmodifier"] = ad.postmodifier;
                DataRow[] drs;
                drs = CLocalDB.localDB.Tables["diag_name"].Select("no=" + ad.diag_code);

                string diagText = "";

                if (!String.IsNullOrWhiteSpace(ad.premodifier))
                { diagText = ad.premodifier; }

                diagText += drs[0]["name"].ToString();

                if (!String.IsNullOrWhiteSpace(ad.postmodifier))
                { diagText += ad.postmodifier; }

                newRow["suspect"] = ad.suspect;
                if (ad.suspect)
                { newRow["name"] = diagText + Properties.Resources.Suspect; }
                else
                { newRow["name"] = diagText; }

                newRow["SQL_index"] = no4SqlIndex;
                dt.Rows.Add(newRow);
                resizeColumns();
                #endregion

                #region stockedSQLs
                DataRow newStock = stockedSQLs.NewRow();
                newStock["SQL"] = ad.stockSQL;
                newStock["index"] = no4SqlIndex.ToString();
                stockedSQLs.Rows.Add(newStock);
                #endregion

                no4SqlIndex++;
                #endregion
            }
            ad.Dispose();
        }

        private void btSetNormal_Click(object sender, EventArgs e)
        {
            edited = true;

            #region dt
            DataRow newRow = dt.NewRow();
            newRow["diag_code"] = 0;
            DataRow[] drs;
            drs = CLocalDB.localDB.Tables["diag_name"].Select("no=0");
            newRow["name"] = drs[0]["name"].ToString();
            newRow["suspect"] = false;
            newRow["premodifier"] = "";
            newRow["postmodifier"] = "";
            newRow["SQL_index"] = no4SqlIndex;
            dt.Rows.Add(newRow);
            #endregion

            #region stockedSQLs
            string sql = "INSERT INTO diag(exam_no, diag_code, suspect, premodifier, postmodifier) " + "VALUES(" + exam.exam_id + ", 0, false, '', '');";

            DataRow newStock = stockedSQLs.NewRow();
            newStock["SQL"] = sql;
            newStock["index"] = no4SqlIndex.ToString();
            stockedSQLs.Rows.Add(newStock);
            #endregion

            no4SqlIndex++;
        }

        private void dgvDiagnoses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView temp_dgv = (DataGridView)sender;
            string sql;
            //Delete row
            if (temp_dgv.Columns[e.ColumnIndex].Name == Properties.Resources.Delete)
            {
                if (!temp_dgv.Rows[e.RowIndex].IsNewRow)
                {
                    if (MessageBox.Show(Properties.Resources.ConfirmDel, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        edited = true;
                        if (string.IsNullOrWhiteSpace(temp_dgv.Rows[e.RowIndex].Cells["SQL_index"].Value.ToString()))//元からあった行を削除するSQLをstock
                        {
                            sql = "DELETE FROM diag WHERE diag_no=" + temp_dgv.Rows[e.RowIndex].Cells["diag_no"].Value.ToString();

                            DataRow newStock = stockedSQLs.NewRow();
                            newStock["SQL"] = sql;
                            newStock["index"] = no4SqlIndex;
                            stockedSQLs.Rows.Add(newStock);
                            //dgvDiagnoses.Rows.RemoveAt(e.RowIndex);
                        }
                        else //Delete stocked SQL written for new diagnosis.
                        {
                            int i = (int)temp_dgv.Rows[e.RowIndex].Cells["SQL_index"].Value;
                            DataRow[] drs;
                            drs = stockedSQLs.Select("index=" + i);
                            drs[0].Delete();
                        }

                        dgvDiagnoses.Rows.RemoveAt(e.RowIndex);
                    }
                }
            }
        }

        private void btReverseOrder_Click(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            if (dv.Sort == "diag_no ASC")
            { dv.Sort = "diag_no DESC"; }
            else
            { dv.Sort = "diag_no ASC"; }
        }

        private void btCopyDiag_Click(object sender, EventArgs e)
        {
            edited = true;
            string diagnoses = "";
            int i = 0;
            foreach (DataGridViewRow dr in dgvDiagnoses.Rows)
            {
                i++;
                diagnoses += "#" + i.ToString() + ". " + dr.Cells["name"].Value.ToString() + Environment.NewLine;
            }

            if (i == 0)
            {
                MessageBox.Show(Properties.Resources.NoDiagnosis, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (selectionStart <= 0)
            {
                tbFindings.Text = tbFindings.Text.Insert(0, diagnoses);
            }
            else
            {
                tbFindings.Text = tbFindings.Text.Insert(selectionStart, diagnoses);
            }
        }

        private void resizeColumns()
        {
            foreach (DataGridViewColumn dc in dgvDiagnoses.Columns)
            { dc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; }
        }
        #endregion

        #region saveData
        private void EditFindings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (canEditPatho && edited)
            {
                if (MessageBox.Show(Properties.Resources.SaveChangesYN, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                { return; }
                else
                {
                    saveFindingsData();

                    if (cbExamStatus.SelectedValue.ToString() == "1")
                    {
                        if (MessageBox.Show(Properties.Resources.StillDraft, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        {
                            tcExam.SelectedTab = tpFindings;
                            btDiagnosed.Focus();
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }
        }

        private void btSaveClose_Click(object sender, EventArgs e)
        {
            if (canEditPatho && edited)
            {
                saveFindingsData();
                edited = false;

                if (cbExamStatus.SelectedValue.ToString() == "1")
                {
                    if (MessageBox.Show(Properties.Resources.StillDraft, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    {
                        tcExam.SelectedTab = tpFindings;
                        btDiagnosed.Focus();
                        return;
                    }
                }
            }
            this.Close();
        }

        private void saveFindingsData()
        {
            exam.purpose = tbPurpose.Text;
            if (!string.IsNullOrWhiteSpace(cbDepartment.Text))
                exam.department = int.Parse(cbDepartment.SelectedValue.ToString());

            exam.order_dr = cbOrderDr.Text;

            if (!string.IsNullOrWhiteSpace(cbWard.Text))
                exam.ward_id = cbWard.SelectedValue.ToString();

            if (!string.IsNullOrWhiteSpace(cbEquipment.Text))
                exam.equipment = int.Parse(cbEquipment.SelectedValue.ToString());

            if (!string.IsNullOrWhiteSpace(cbPlace.Text))
                exam.place_no = int.Parse(cbPlace.SelectedValue.ToString());

            #region operator
            if (string.IsNullOrWhiteSpace(this.cbOperator1.Text))
            { exam.operator1 = null; }
            else
            { exam.operator1 = cbOperator1.SelectedValue.ToString(); }

            if (string.IsNullOrWhiteSpace(this.cbOperator2.Text))
            { exam.operator2 = null; }
            else
            { exam.operator2 = cbOperator2.SelectedValue.ToString(); }

            if (string.IsNullOrWhiteSpace(this.cbOperator3.Text))
            { exam.operator3 = null; }
            else
            { exam.operator3 = cbOperator3.SelectedValue.ToString(); }

            if (string.IsNullOrWhiteSpace(this.cbOperator4.Text))
            { exam.operator4 = null; }
            else
            { exam.operator4 = cbOperator4.SelectedValue.ToString(); }

            if (string.IsNullOrWhiteSpace(this.cbOperator5.Text))
            { exam.operator5 = null; }
            else
            { exam.operator5 = cbOperator5.SelectedValue.ToString(); }
            #endregion

            for (int i = 0; i < stockedSQLs.Rows.Count; i++)//Run all stocked SQLs for diagnoses.
            {
                if (uckyFunctions.ExeNonQuery(stockedSQLs.Rows[i]["SQL"].ToString()) == uckyFunctions.functionResult.failed)
                { MessageBox.Show(Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            exam.findings = tbFindings.Text;

            if (string.IsNullOrWhiteSpace(this.cbDiagnosed.Text))
            { exam.diag_dr = null; }
            else
            { exam.diag_dr = cbDiagnosed.SelectedValue.ToString(); }

            exam.comment = tbComment.Text;
            exam.patho_result = tbPatho.Text;

            if (string.IsNullOrWhiteSpace(this.cbChecker.Text))
            { exam.final_diag_dr = null; }
            else
            { exam.final_diag_dr = cbChecker.SelectedValue.ToString(); }

            saveFigure(pbFigure1, "1");
            saveFigure(pbFigure2, "2");

            exam.exam_status = int.Parse(cbExamStatus.SelectedValue.ToString());
            exam.saveFindingsEtc();
        }
        #endregion

        #region tbPurpose
        private void getSelectionStartOfPurpose()
        {
            selectedTb = "tbPurpose";
            selectionStart = tbPurpose.SelectionStart;
        }

        private void tbPurpose_KeyUp(object sender, KeyEventArgs e)
        {
            getSelectionStartOfPurpose();
        }

        private void tbPurpose_Click(object sender, EventArgs e)
        {
            getSelectionStartOfPurpose();
        }

        private void tbPurpose_Enter(object sender, EventArgs e)
        {
            getSelectionStartOfPurpose();
        }

        private void tbPurpose_Leave(object sender, EventArgs e)
        {
            getSelectionStartOfPurpose();
        }
        #endregion

        #region tbFindings
        private void getSelectionStartOfFindings()
        {
            selectedTb = "tbFindings";
            selectionStart = tbFindings.SelectionStart;
        }

        private void tbFindings_KeyUp(object sender, KeyEventArgs e)
        { getSelectionStartOfFindings(); }

        private void tbFindings_Click(object sender, EventArgs e)
        { getSelectionStartOfFindings(); }

        private void tbFindings_Enter(object sender, EventArgs e)
        { getSelectionStartOfFindings(); }

        private void tbFindings_Leave(object sender, EventArgs e)
        { getSelectionStartOfFindings(); }
        #endregion

        #region tbComment
        private void getSelectionStartOfComment()
        {
            selectedTb = "tbComment";
            selectionStart = tbComment.SelectionStart;
        }

        private void tbComment_KeyUp(object sender, KeyEventArgs e)
        {
            getSelectionStartOfComment();
        }

        private void tbComment_Click(object sender, EventArgs e)
        {
            getSelectionStartOfComment();
        }

        private void tbComment_Enter(object sender, EventArgs e)
        {
            getSelectionStartOfComment();
        }

        private void tbComment_Leave(object sender, EventArgs e)
        {
            getSelectionStartOfComment();
        }
        #endregion

        #region tbPatho
        private void getSelectionStartOfPathology()
        {
            selectedTb = "tbPatho";
            selectionStart = tbPatho.SelectionStart;
        }

        private void tbPatho_KeyUp(object sender, KeyEventArgs e)
        { getSelectionStartOfPathology(); }

        private void tbPatho_Click(object sender, EventArgs e)
        { getSelectionStartOfPathology(); }

        private void tbPatho_Enter(object sender, EventArgs e)
        { getSelectionStartOfPathology(); }

        private void tbPatho_Leave(object sender, EventArgs e)
        { getSelectionStartOfPathology(); }
        #endregion

        #region ExamStatus
        private void btDiagnosed_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbDiagnosed.Text))
            {
                edited = true;
                cbDiagnosed.SelectedValue = db_operator.operatorID;
                setExamStatus(2);
            }
            else
            {
                if (MessageBox.Show(Properties.Resources.ConfirmChanging, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    edited = true;
                    cbDiagnosed.SelectedValue = db_operator.operatorID;
                    setExamStatus(2);
                }
            }
        }

        private void btChecked_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbChecker.Text))
            {
                edited = true;
                cbChecker.SelectedValue = db_operator.operatorID;
                setExamStatus(3);
            }
            else
            {
                if (MessageBox.Show(Properties.Resources.ConfirmChanging, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    edited = true;
                    cbChecker.SelectedValue = db_operator.operatorID;
                    setExamStatus(3);
                }
            }
        }

        private void setExamStatus(int i)
        {
            if ((int.Parse(cbExamStatus.SelectedValue.ToString()) < i) || (int.Parse(cbExamStatus.SelectedValue.ToString()) == 9))
            {
                cbExamStatus.SelectedValue = i;
            }
        }

        private void btExamCanceled_Click(object sender, EventArgs e)
        {
            cbExamStatus.SelectedValue = 9;
            edited = true;
            this.Close();
        }
        #endregion

        #region Figure
        /// <summary>Check figure folder exist or not, and if folder not existed, create folder.</summary>
        private void checkFigureFolder()
        {
            string saveFolder = Settings.figureFolder + @"\" + exam.exam_day.Year.ToString();
            if (!System.IO.Directory.Exists(saveFolder))
            {
                System.IO.Directory.CreateDirectory(saveFolder);
            }
        }

        /// <summary>Fill pbFigure with white.</summary>
        private void fillPicBoxWhite(PictureBox pb)
        {
            Bitmap canvas = new Bitmap(pb.Width, pb.Height);
            Graphics g = Graphics.FromImage(canvas);
            Rectangle rect = new Rectangle(0, 0, pb.Width, pb.Height);
            g.FillRectangle(Brushes.White, rect);
            g.Dispose();
            pb.Image = canvas;
        }

        private void saveFigure(PictureBox _pb, string _number)
        {
            Bitmap canvas = new Bitmap(_pb.Width, _pb.Height);
            Graphics g = Graphics.FromImage(canvas);
            Rectangle rect = new Rectangle(0, 0, _pb.Width, _pb.Height);
            g.FillRectangle(Brushes.White, rect);
            g.Dispose();
            if (uckyFunctions.ImageComp(_pb.Image, (Image)canvas))
            { return; }

            if (!System.IO.File.Exists(figureFileNameBase + "_" + _number + ".png"))
            {
                _pb.Image.Save(figureFileNameBase + "_" + _number + ".png", System.Drawing.Imaging.ImageFormat.Png);
            }
            else
            {
                fs = File.Open(figureFileNameBase + "_" + _number + ".png", FileMode.Open, FileAccess.ReadWrite);
                _pb.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                fs.Close();
            }
        }

        private void pbFigure1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                edited = true;
                isDrawing = true;
                drawPoint(e.X, e.Y, pbFigure1);
                prevLocation = e.Location;
            }
        }

        private void pbFigure2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                edited = true;
                isDrawing = true;
                drawPoint(e.X, e.Y, pbFigure2);
                prevLocation = e.Location;
            }
        }

        private void pbFigure1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                drawLine(e.X, e.Y, pbFigure1);
                prevLocation = e.Location;
            }
        }

        private void pbFigure2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                drawLine(e.X, e.Y, pbFigure2);
                prevLocation = e.Location;
            }
        }

        private void pbFigure1_MouseUp(object sender, MouseEventArgs e)
        {
            drawPoint(e.X, e.Y, pbFigure1);
            isDrawing = false;
        }

        private void pbFigure2_MouseUp(object sender, MouseEventArgs e)
        {
            drawPoint(e.X, e.Y, pbFigure2);
            isDrawing = false;
        }

        private void pbFigure1_MouseLeave(object sender, EventArgs e)
        {
            isDrawing = false;
        }
        private void pbFigure2_MouseLeave(object sender, EventArgs e)
        {
            isDrawing = false;
        }

        private void drawPoint(int x, int y, PictureBox pb)
        {
            Bitmap canvas = new Bitmap(pb.Image);
            Graphics g = Graphics.FromImage(canvas);
            SolidBrush b = new SolidBrush(currentColor);
            int i = int.Parse(cbBrushWidth1.SelectedValue.ToString());
            g.FillEllipse(b, x - (int)(i / 2), y - (int)(i / 2), i, i);
            b.Dispose();
            g.Dispose();
            pb.Image = canvas;
        }

        private void drawLine(int x, int y, PictureBox pb)
        {
            Bitmap canvas = new Bitmap(pb.Image);
            Graphics g = Graphics.FromImage(canvas);
            SolidBrush b = new SolidBrush(currentColor);
            int i = int.Parse(cbBrushWidth1.SelectedValue.ToString());
            Pen p = new Pen(b, i);
            Point currentPoint = new Point(x, y);
            g.DrawLine(p, prevLocation, currentPoint);
            g.FillEllipse(b, new Rectangle(prevLocation - new Size((int)(i / 2), (int)(i / 2)), new Size(i, i)));
            b.Dispose();
            g.Dispose();
            pb.Image = canvas;
        }

        private void btColorSelect_Click(object sender, EventArgs e)
        {
            setColor();
        }

        private void btErace_Click(object sender, EventArgs e)
        {
            currentColor = Color.White;
            pbColor1.BackColor = currentColor;
            pbColor2.BackColor = currentColor;
            setPbBrushWidth();
        }

        private void setColor()
        {
            var colorDlg = new ColorDialog();
            if (DialogResult.OK == colorDlg.ShowDialog())
            {
                currentColor = colorDlg.Color;
                pbColor1.BackColor = currentColor;
                pbColor2.BackColor = currentColor;
                setPbBrushWidth();
            }
        }

        private void cbBrushWidth1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setPbBrushWidth();
        }

        private void cbBrushWidth2_SelectedIndexChanged(object sender, EventArgs e)
        {
            setPbBrushWidth();
        }

        private void setPbBrushWidth()
        {
            Bitmap canvas = new Bitmap(pbBrushWidth1.Width, pbBrushWidth1.Height);
            Graphics g = Graphics.FromImage(canvas);
            SolidBrush b = new SolidBrush(currentColor);
            int i = int.Parse(cbBrushWidth1.SelectedValue.ToString());
            int position = 14 - (int)(Math.Round((double)i / 2, MidpointRounding.AwayFromZero));
            g.FillEllipse(b, position, position, i, i);
            b.Dispose();
            g.Dispose();
            pbBrushWidth1.Image = canvas;
            pbBrushWidth2.Image = canvas;
        }
        #endregion

        #region words
        private void dgvWords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (tcExam.SelectedIndex == 2 || tcExam.SelectedIndex == 3)
            {
                MessageBox.Show(Properties.Resources.ClickInsPosition, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (selectionStart == -1)
            {
                MessageBox.Show(Properties.Resources.ClickInsPosition, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (selectedTb == "tbFindings")
            {
                tbFindings.Text = tbFindings.Text.ToString().Insert(selectionStart, @dgv[e.ColumnIndex, e.RowIndex].Value.ToString());
                selectionStart += dgv[e.ColumnIndex, e.RowIndex].Value.ToString().Length;
                tbFindings.SelectionStart = selectionStart;
                tbFindings.SelectionLength = 0;
                tbFindings.Focus();
            }
            else if (selectedTb == "tbComment")
            {
                tbComment.Text = tbComment.Text.ToString().Insert(selectionStart, @dgv[e.ColumnIndex, e.RowIndex].Value.ToString());
                selectionStart += dgv[e.ColumnIndex, e.RowIndex].Value.ToString().Length;
                tbComment.SelectionStart = selectionStart;
                tbComment.SelectionLength = 0;
                tbComment.Focus();
            }
            else if (selectedTb == "tbPurpose")
            {
                tbPurpose.Text = tbPurpose.Text.ToString().Insert(selectionStart, @dgv[e.ColumnIndex, e.RowIndex].Value.ToString());
                selectionStart += dgv[e.ColumnIndex, e.RowIndex].Value.ToString().Length;
                tbPurpose.SelectionStart = selectionStart;
                tbPurpose.SelectionLength = 0;
                tbPurpose.Focus();
            }
            else if (selectedTb == "tbPatho")
            {
                tbPatho.Text = tbPatho.Text.ToString().Insert(selectionStart, @dgv[e.ColumnIndex, e.RowIndex].Value.ToString());
                selectionStart += dgv[e.ColumnIndex, e.RowIndex].Value.ToString().Length;
                tbPatho.SelectionStart = selectionStart;
                tbPatho.SelectionLength = 0;
                tbPatho.Focus();
            }
        }
        #endregion

        private void btCopy2ClipBoard_Click(object sender, EventArgs e)
        {
            string str = "";
            str = "<" + exam.getExamTypeName() + ">" + Environment.NewLine;

            int i = 0;
            foreach (DataGridViewRow dr in dgvDiagnoses.Rows)
            {
                i++;
                str += "#" + i.ToString() + ". " + dr.Cells["name"].Value.ToString() + Environment.NewLine;
            }

            str += tbFindings.Text.ToString() + Environment.NewLine;

            string op_id;
            examOperator temp_op;
            if (!string.IsNullOrWhiteSpace(cbDiagnosed.Text.ToString()))
            {
                op_id = cbDiagnosed.SelectedValue.ToString();
                temp_op = new examOperator(op_id);
                str += "<" + Properties.Resources.DiagnosedDr + ">" + temp_op.op_name + Environment.NewLine;
            }

            if (!string.IsNullOrWhiteSpace(tbComment.Text.ToString()))
            {
                str += Properties.Resources.Comment + ":" + Environment.NewLine + tbComment.Text.ToString() + Environment.NewLine;
            }

            if (!string.IsNullOrWhiteSpace(cbChecker.Text.ToString()))
            {
                op_id = cbChecker.SelectedValue.ToString();
                temp_op = new examOperator(op_id);
                str += "<" + Properties.Resources.Checker + ">" + temp_op.op_name + Environment.NewLine;
            }

            Clipboard.SetDataObject(str);
        }

        #region Timer
        //This function necessary for timer procedure. Call updateLockTime.
        private void timer_Tick(object sender, EventArgs e)
        { uckyFunctions.updateLockTimeIP("exam", "exam_id", "=", "'" + exam.exam_id.ToString() + "'"); }

        private void EditFindings_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Stop();
            if (canEdit)
            { uckyFunctions.delLockTimeIP("exam", "exam_id", "=", "'" + exam.exam_id.ToString() + "'"); }
        }
        #endregion

        #region edited to true
        private void tbPurpose_TextChanged(object sender, EventArgs e)
        { edited = true; }

        private void cbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        { edited = true; }

        private void cbWard_SelectedIndexChanged(object sender, EventArgs e)
        { edited = true; }

        private void cbPlace_SelectedIndexChanged(object sender, EventArgs e)
        { edited = true; }

        #region cbOrderDr
        private void cbOrderDr_TextUpdate(object sender, EventArgs e)
        { edited = true; }

        private void cbOrderDr_TextChanged(object sender, EventArgs e)
        { edited = true; }

        private void cbOrderDr_SelectedIndexChanged(object sender, EventArgs e)
        { edited = true; }
        #endregion

        private void cbEquipment_SelectedIndexChanged(object sender, EventArgs e)
        { edited = true; }

        private void cbOperator1_SelectedIndexChanged(object sender, EventArgs e)
        { edited = true; }

        private void cbOperator2_SelectedIndexChanged(object sender, EventArgs e)
        { edited = true; }

        private void cbOperator3_SelectedIndexChanged(object sender, EventArgs e)
        { edited = true; }

        private void cbOperator4_SelectedIndexChanged(object sender, EventArgs e)
        { edited = true; }

        private void cbOperator5_SelectedIndexChanged(object sender, EventArgs e)
        { edited = true; }

        private void tbFindings_TextChanged(object sender, EventArgs e)
        { edited = true; }

        private void tbComment_TextChanged(object sender, EventArgs e)
        { edited = true; }

        private void tbPatho_TextChanged(object sender, EventArgs e)
        { edited = true; }
        #endregion
    }
}
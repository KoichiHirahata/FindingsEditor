using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Timers;
using System.Windows;

namespace FindingsEditor
{
    /// <summary>
    /// EditFindings.xaml の相互作用ロジック
    /// </summary>
    public partial class EditFindings : Window
    {
        private Exam exam;
        private Boolean canEdit;
        private Boolean canEditPatho; //This will control the right of edit pathology results.
        private ObservableCollection<DiagnosisOfExam> DiagnosesList { get; set; }
        //private DataTable dt = new DataTable();//Temporary datatable for diagnoses.
        private DataTable stockedSQLs = new DataTable();
        private int no4SqlIndex = 0;
        //private Color currentColor = Color.Black;
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
            canEditPatho = feFunctions.canEdit("exam", "exam_id", "=", "'" + exam.exam_id.ToString() + "'");

            if (canEdit)
            { canEdit = canEditPatho; }

            if (canEditPatho)
            {
                feFunctions.updateLockTimeIP("exam", "exam_id", "=", "'" + exam.exam_id.ToString() + "'");

                //Timer procedure
                timer.Interval = 30000;  //Unit is msec
                timer.Elapsed += new ElapsedEventHandler(timer_Tick);
                timer.Start();

                if (!canEdit)
                { MessageBox.Show(Properties.Resources.YouCanMakeChangesOnlyToPatho, "Information", MessageBoxButton.OK, MessageBoxImage.Information); }
            }
            else
            { MessageBox.Show(Properties.Resources.ReadOnlyMode, "Information", MessageBoxButton.OK, MessageBoxImage.Information); }
            #endregion

            #endregion

            #region Information
            lbPatient.Content = "ID: " + exam.pt_id + "   " + Properties.Resources.Name + ": " + exam.pt_name;
            lbDate.Content = Properties.Resources.Date + Properties.Resources.Colon + exam.exam_day.ToShortDateString() + "  "
                + Properties.Resources.ExamType + Properties.Resources.Colon + exam.getExamTypeName();
            lbAge.Content = Properties.Resources.Age + Properties.Resources.Colon + exam.getAge().ToString(); 

            if (!canEdit)
            { btSaveClose.Content = Properties.Resources.Close; }

            #region cbExamStatus initialization
            cbExamStatus.ItemsSource = CLocalDB.localDB.Tables["exam_status"].DefaultView;
            cbExamStatus.SelectedValuePath = "status_no";
            cbExamStatus.DisplayMemberPath = "status_name";
            if (string.IsNullOrWhiteSpace(exam.exam_status.ToString()) || (exam.exam_status == 0))
            { cbExamStatus.SelectedValue = 1; }
            else
            { cbExamStatus.SelectedValue = exam.exam_status; }
            #endregion

            #region tbPurpose initialization
            tbPurpose.Text = exam.purpose;
            if (canEdit)
            { tbPurpose.IsReadOnly = false; }
            else
            { tbPurpose.IsReadOnly = true; }
            #endregion

            #region cbDepartment initialization
            cbDepartment.ItemsSource = CLocalDB.localDB.Tables["department"].DefaultView;
            cbDepartment.SelectedValuePath = "code";
            cbDepartment.DisplayMemberPath = "name1";
            cbDepartment.SelectedValue = exam.department;
            if (!canEdit)
            { cbDepartment.IsEnabled = false; }
            #endregion

            #region cbOrderedDr initialization
            cbOrderedDr.ItemsSource = CLocalDB.localDB.Tables["orderDr"].DefaultView;
            cbOrderedDr.DisplayMemberPath = "op_name";
            if (string.IsNullOrWhiteSpace(exam.order_dr))
            { cbOrderedDr.SelectedIndex = -1; }
            else
            {
                cbOrderedDr.SelectedIndex = -1;//これをしないと項目にないテキストがちゃんと反映されない。
                this.cbOrderedDr.Text = exam.order_dr;
            }
            if (!canEdit)
            { cbOrderedDr.IsEnabled = false; }
            #endregion

            #region cbWard initialization
            cbWard.ItemsSource = CLocalDB.localDB.Tables["ward"].DefaultView;
            cbWard.SelectedValuePath = "ward_no";
            cbWard.DisplayMemberPath = "ward";
            cbWard.SelectedValue = exam.ward_id;
            if (!canEdit)
            { cbWard.IsEnabled = false; }
            #endregion

            #region cbEquipment initialization
            switch (exam.exam_type)
            {
                case 1:
                    cbEquipment.ItemsSource = CLocalDB.localDB.Tables["equipmentGF"].DefaultView;
                    break;
                case 2:
                    cbEquipment.ItemsSource = CLocalDB.localDB.Tables["equipmentCF"].DefaultView;
                    break;
                case 3:
                    cbEquipment.ItemsSource = CLocalDB.localDB.Tables["equipmentSV"].DefaultView;
                    break;
                case 4:
                    cbEquipment.ItemsSource = CLocalDB.localDB.Tables["equipmentS"].DefaultView;
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
                    cbEquipment.ItemsSource = CLocalDB.localDB.Tables["equipmentUS"].DefaultView;
                    break;
            }
            cbEquipment.SelectedValuePath = "equipment_no";
            cbEquipment.DisplayMemberPath = "name";
            cbEquipment.SelectedValue = exam.equipment;
            if (!canEdit)
            { cbEquipment.IsEnabled = false; }
            #endregion

            #region cbPlace initialization
            switch (exam.exam_type)
            {
                case 99:
                    cbPlace.ItemsSource = CLocalDB.localDB.Tables["placeUS"].DefaultView;
                    break;
                default:
                    cbPlace.ItemsSource = CLocalDB.localDB.Tables["placeEndo"].DefaultView;
                    break;
            }
            cbPlace.SelectedValuePath = "place_no";
            cbPlace.DisplayMemberPath = "name1";
            if (string.IsNullOrWhiteSpace(exam.place_no.ToString()))
            { cbPlace.SelectedIndex = -1; }
            else
            { cbPlace.SelectedValue = exam.place_no; }
            if (!canEdit)
            { cbPlace.IsEnabled = false; }
            #endregion

            #region cbOperator1 initialization
            cbOperator1.ItemsSource = CLocalDB.localDB.Tables["operator1"].DefaultView;
            cbOperator1.SelectedValuePath = "operator_id";
            cbOperator1.DisplayMemberPath = "op_name";
            if (string.IsNullOrWhiteSpace(exam.operator1))
            { cbOperator1.SelectedIndex = -1; }
            else
            { cbOperator1.SelectedValue = exam.operator1; }

            if (!canEdit)
            {
                cbOperator1.IsEnabled = false;
                btClearOp1.Visibility = Visibility.Collapsed;
            }

            if (shouldFillOperatorWithUser(canEdit, exam.exam_status))
            { cbOperator1.SelectedValue = db_operator.operatorID; }
            #endregion

            #region cbOperator2 initialization
            cbOperator2.ItemsSource = CLocalDB.localDB.Tables["operator2"].DefaultView;
            cbOperator2.SelectedValuePath = "operator_id";
            cbOperator2.DisplayMemberPath = "op_name";
            if (string.IsNullOrWhiteSpace(exam.operator2))
            { cbOperator2.SelectedIndex = -1; }
            else
            { cbOperator2.SelectedValue = exam.operator2; }

            if (!canEdit)
            {
                cbOperator2.IsEnabled = false;
                btClearOp2.Visibility = Visibility.Collapsed;
            }
            #endregion

            #region cbOperator3 initialization
            cbOperator3.ItemsSource = CLocalDB.localDB.Tables["operator3"].DefaultView;
            cbOperator3.SelectedValuePath = "operator_id";
            cbOperator3.DisplayMemberPath = "op_name";
            if (string.IsNullOrWhiteSpace(exam.operator3))
            { cbOperator3.SelectedIndex = -1; }
            else
            { cbOperator3.SelectedValue = exam.operator3; }

            if (!canEdit)
            {
                cbOperator3.IsEnabled = false;
                btClearOp3.Visibility = Visibility.Collapsed;
            }
            #endregion

            #region cbOperator4 initialization
            cbOperator4.ItemsSource = CLocalDB.localDB.Tables["operator4"].DefaultView;
            cbOperator4.SelectedValuePath = "operator_id";
            cbOperator4.DisplayMemberPath = "op_name";
            if (string.IsNullOrWhiteSpace(exam.operator4))
            { cbOperator4.SelectedIndex = -1; }
            else
            { cbOperator4.SelectedValue = exam.operator4; }

            if (!canEdit)
            {
                cbOperator4.IsEnabled = false;
                btClearOp4.Visibility = Visibility.Collapsed;
            }
            #endregion

            #region cbOperator5 initialization
            cbOperator5.ItemsSource = CLocalDB.localDB.Tables["operator5"].DefaultView;
            cbOperator5.SelectedValuePath = "operator_id";
            cbOperator5.DisplayMemberPath = "op_name";
            if (string.IsNullOrWhiteSpace(exam.operator5))
            { cbOperator5.SelectedIndex = -1; }
            else
            { cbOperator5.SelectedValue = exam.operator5; }

            if (!canEdit)
            {
                cbOperator5.IsEnabled = false;
                btClearOp5.Visibility = Visibility.Collapsed;
            }
            #endregion

            if (!canEdit)
            { btExamCanceled.Visibility = Visibility.Collapsed; }

            #endregion

            #region Findings

            #region dgvDiagnoses initialization
            dgDiagnoses.ItemsSource = DiagnosesList;
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
                tbFindings.Text = uckyFunctions.getSelectString(sql, Settings.DBSrvIP, Settings.DBSrvPort, Settings.DBconnectID, Settings.DBconnectPw, Settings.DBname);
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



        }

        #region information
        public static bool shouldFillOperatorWithUser(bool can_edit, int exam_status)
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
            cbOperator1.SelectedIndex = -1;
        }

        private void btClearOp2_Click(object sender, EventArgs e)
        {
            edited = true;
            cbOperator2.SelectedIndex = -1;
        }

        private void btClearOp3_Click(object sender, EventArgs e)
        {
            edited = true;
            cbOperator3.SelectedIndex = -1;
        }

        private void btClearOp4_Click(object sender, EventArgs e)
        {
            edited = true;
            cbOperator4.SelectedIndex = -1;
        }

        private void btClearOp5_Click(object sender, EventArgs e)
        {
            edited = true;
            this.cbOperator5.SelectedIndex = -1;
        }
        #endregion
        #endregion






        #region Timer
        //This function necessary for timer procedure. Call updateLockTime.
        private void timer_Tick(object sender, EventArgs e)
        { feFunctions.updateLockTimeIP("exam", "exam_id", "=", "'" + exam.exam_id.ToString() + "'"); }

        private void Window_Closed(object sender, EventArgs e)
        {
            timer.Stop();
            if (canEdit)
            { feFunctions.delLockTimeIP("exam", "exam_id", "=", "'" + exam.exam_id.ToString() + "'"); }
        }
        #endregion
    }

    /// <summary>
    /// 実際の検査の診断
    /// Diagnosis of examination class
    /// </summary>
    public class DiagnosisOfExam
    {

    }
}

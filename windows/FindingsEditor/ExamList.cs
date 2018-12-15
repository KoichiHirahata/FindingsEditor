using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Npgsql;

namespace FindingsEdior
{
    public partial class ExamList : Form
    {
        public DataTable exam_list = new DataTable();
        private string dateFrom;
        private string dateTo;
        private string pt_id;
        private string department;
        private string operator1;
        private Boolean op1_5; //True means searching operator among operator1 to 5 

        public ExamList(string _date_from, string _date_to, string _pt_id, string _department, string _operator1, Boolean _op1_5)
        {
            InitializeComponent();

            dgvExamList.RowHeadersVisible = false;
            dgvExamList.MultiSelect = false;
            dgvExamList.Font = new Font(dgvExamList.Font.Name, 12);
            dgvExamList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvExamList.DataSource = exam_list;
            dateFrom = _date_from;
            dateTo = _date_to;
            pt_id = _pt_id;
            department = _department;
            operator1 = _operator1;
            op1_5 = _op1_5;

            SearchExam(dateFrom, dateTo, pt_id, department, operator1, op1_5);

            #region Add btSelect
            DataGridViewButtonColumn btSelect = new DataGridViewButtonColumn(); //DataGridViewButtonColumnの作成
            btSelect.Name = "btSelect";  //列の名前を設定
            btSelect.UseColumnTextForButtonValue = true;    //ボタンにテキスト表示
            btSelect.Text = FindingsEditor.Properties.Resources.ptSelect;  //ボタンの表示テキスト設定
            dgvExamList.Columns.Add(btSelect);           //ボタン追加
            #endregion

            #region Add btImage
            DataGridViewButtonColumn btImage = new DataGridViewButtonColumn(); //DataGridViewButtonColumnの作成
            btImage.Name = "btImage";  //列の名前を設定
            btImage.UseColumnTextForButtonValue = true;    //ボタンにテキスト表示
            btImage.Text = FindingsEditor.Properties.Resources.Image;  //ボタンの表示テキスト設定
            dgvExamList.Columns.Add(btImage);           //ボタン追加
            #endregion

            #region Add btDelColumn
            DataGridViewButtonColumn btDelColumn = new DataGridViewButtonColumn(); //DataGridViewButtonColumnの作成
            btDelColumn.Name = "btDelColumn";  //列の名前を設定
            btDelColumn.UseColumnTextForButtonValue = true;    //ボタンにテキスト表示
            btDelColumn.Text = FindingsEditor.Properties.Resources.Delete;  //ボタンの表示テキスト設定
            dgvExamList.Columns.Add(btDelColumn);           //ボタン追加
            #endregion

            #region Add btPrint
            DataGridViewButtonColumn btPrint = new DataGridViewButtonColumn(); //DataGridViewButtonColumnの作成
            btPrint.Name = "btPrint";  //列の名前を設定
            btPrint.UseColumnTextForButtonValue = true;    //ボタンにテキスト表示
            btPrint.Text = FindingsEditor.Properties.Resources.Print;  //ボタンの表示テキスト設定
            dgvExamList.Columns.Add(btPrint);           //ボタン追加
            #endregion

            #region Design and Settings
            exam_list.DefaultView.RowFilter = "exam_status < 2";  //Show only blank/draft findings.

            dgvExamList.Columns["exam_id"].Visible = false;
            dgvExamList.Columns["exam_status"].Visible = false;
            dgvExamList.Columns["exam_type_no"].Visible = false;
            dgvExamList.Columns["type_name_en"].Visible = false;

            //Without below settings, this application will down.
            dgvExamList.Columns["btSelect"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvExamList.Columns["btImage"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvExamList.Columns["btDelColumn"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvExamList.Columns["btPrint"].SortMode = DataGridViewColumnSortMode.NotSortable;
            #endregion

            #region Change columns header text
            this.dgvExamList.Columns["pt_id"].HeaderText = "ID";
            this.dgvExamList.Columns["pt_name"].HeaderText = FindingsEditor.Properties.Resources.ptName;
            this.dgvExamList.Columns["exam_day"].HeaderText = FindingsEditor.Properties.Resources.Date;
            this.dgvExamList.Columns["exam_type_name"].HeaderText = FindingsEditor.Properties.Resources.ExamType;
            this.dgvExamList.Columns["name1"].HeaderText = FindingsEditor.Properties.Resources.Department;
            this.dgvExamList.Columns["ward"].HeaderText = FindingsEditor.Properties.Resources.Ward;
            this.dgvExamList.Columns["status_name"].HeaderText = FindingsEditor.Properties.Resources.Status;
            //dgvExamList.Columns["btSelect"].HeaderText = FindingsEditor.Properties.Resources.ptSelect; //If this code is enabled, this application will stop with clicking at columnheader.
            //dgvExamList.Columns["btImage"].HeaderText = FindingsEditor.Properties.Resources.Image; //Same as above.
            //dgvExamList.Columns["btDelColumn"].HeaderText = FindingsEditor.Properties.Resources.Delete; //Same as above.
            //dgvExamList.Columns["btPrint"].HeaderText = FindingsEditor.Properties.Resources.Print; //Same as above.
            dgvExamList.Columns["btSelect"].HeaderText = "";
            dgvExamList.Columns["btImage"].HeaderText = "";
            dgvExamList.Columns["btDelColumn"].HeaderText = "";
            dgvExamList.Columns["btPrint"].HeaderText = "";
            #endregion

            resizeColumns();
        }

        private void SearchExam(string _date_from, string _date_to, string _pt_id, string _department, string _operator, Boolean _op1_5)
        {
            try
            {
                #region Error Check
                if(_date_from != null && DateTime.TryParse(_date_from,out DateTime dt) == false)
                {
                    MessageBox.Show("[SearchExam] _date_from invalid:" + _date_from, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_date_from != null && DateTime.TryParse(_date_to, out DateTime dt_to) == false)
                {
                    MessageBox.Show("[SearchExam] _date_to invalid:" + _date_to, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                #region Npgsql connection
                NpgsqlConnection conn;

                try
                {
                    conn = new NpgsqlConnection(Settings.retConnStr());
                }
                catch (ArgumentException)
                {
                    MessageBox.Show(FindingsEditor.Properties.Resources.WrongConnectingString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                string sql = "select * from get_exam_list(@uid, @upw, @lang, @date_from, @date_to, @p_id, @dep, @op, @op1_5);";
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);

                #region Add Npgsql parameters
                command.Parameters.AddWithValue("@uid", NpgsqlTypes.NpgsqlDbType.Text, db_operator.operatorID);
                command.Parameters.AddWithValue("@upw", NpgsqlTypes.NpgsqlDbType.Text, db_operator.operatorPw);
                command.Parameters.AddWithValue("@lang", NpgsqlTypes.NpgsqlDbType.Text, Settings.lang);

                #region date_from
                if(String.IsNullOrWhiteSpace(_date_from))
                {
                    command.Parameters.AddWithValue("@date_from", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@date_from", NpgsqlTypes.NpgsqlDbType.Date, _date_from);
                }
                #endregion

                #region date_to
                if (String.IsNullOrWhiteSpace(_date_to))
                {
                    command.Parameters.AddWithValue("@date_to", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@date_to", NpgsqlTypes.NpgsqlDbType.Date, _date_to);
                }
                #endregion

                #region pt_id
                if (String.IsNullOrWhiteSpace(_pt_id))
                {
                    command.Parameters.AddWithValue("@p_id", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@p_id", _pt_id);
                }
                #endregion

                #region department
                if (String.IsNullOrWhiteSpace(_department))
                {
                    command.Parameters.AddWithValue("@dep", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@dep", NpgsqlTypes.NpgsqlDbType.Smallint,_department);
                }
                #endregion

                #region operator
                if (String.IsNullOrWhiteSpace(_operator))
                {
                    command.Parameters.AddWithValue("@op", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@op", _operator);
                }
                #endregion

                command.Parameters.AddWithValue("@op1_5", NpgsqlTypes.NpgsqlDbType.Boolean,_op1_5);
                #endregion

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(command);
                da.Fill(exam_list);
            }
            #region catch
            catch (Exception ex)
            {
                MessageBox.Show("[SearchExam]" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void resizeColumns()
        {
            foreach (DataGridViewColumn dc in dgvExamList.Columns)
            { dc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; }
        }

        private void btClose_Click(object sender, EventArgs e)
        { this.Close(); }

        #region dgv Buttons
        private void dgvExamList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            if (dgv.Columns[e.ColumnIndex].Name == "btSelect")
            {
                selectExam(dgv.Rows[e.RowIndex].Cells["exam_id"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["exam_type_no"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["type_name_en"].Value.ToString());
                return;
            }
            else if (dgv.Columns[e.ColumnIndex].Name == "btImage")
            {
                uckyFunctions.showImages(dgv.Rows[e.RowIndex].Cells["pt_id"].Value.ToString(),
                    uckyFunctions.dateTo8char(dgv.Rows[e.RowIndex].Cells["exam_day"].Value.ToString(), Settings.lang));
                return;
            }
            else if (dgv.Columns[e.ColumnIndex].Name == "btDelColumn")
            {
                delExam(dgv.Rows[e.RowIndex].Cells["exam_id"].Value.ToString());
                return;
            }
            else if (dgv.Columns[e.ColumnIndex].Name == "btPrint")
            {
                printExam(dgv.Rows[e.RowIndex].Cells["exam_id"].Value.ToString());
                return;
            }
        }

        private void dgvExamList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataGridView dgv = (DataGridView)sender;
                if (dgv.RowCount == 0)
                { return; }

                if (dgv.Columns[dgv.CurrentCell.ColumnIndex].Name == "btSelect")
                {
                    selectExam(dgv.Rows[dgv.CurrentCell.RowIndex].Cells["exam_id"].Value.ToString(), dgv.Rows[dgv.CurrentCell.RowIndex].Cells["exam_type_no"].Value.ToString(), dgv.Rows[dgv.CurrentCell.RowIndex].Cells["type_name_en"].Value.ToString());
                    return;
                }
                else if (dgv.Columns[dgv.CurrentCell.ColumnIndex].Name == "btImage")
                {
                    uckyFunctions.showImages(dgv.Rows[dgv.CurrentCell.RowIndex].Cells["pt_id"].Value.ToString(),
                        uckyFunctions.dateTo8char(dgv.Rows[dgv.CurrentCell.RowIndex].Cells["exam_day"].Value.ToString(), Settings.lang));
                    return;
                }
                else if (dgv.Columns[dgv.CurrentCell.ColumnIndex].Name == "btDelColumn")
                {
                    delExam(dgv.Rows[dgv.CurrentCell.RowIndex].Cells["exam_id"].Value.ToString());
                    return;
                }
                else if (dgv.Columns[dgv.CurrentCell.ColumnIndex].Name == "btPrint")
                {
                    printExam(dgv.Rows[dgv.CurrentCell.RowIndex].Cells["exam_id"].Value.ToString());
                    return;
                }
            }
        }

        #region dgv buttons functions
        private void selectExam(string exam_id_str, string exam_type_no, string type_name_en)
        {
            if (int.Parse(exam_type_no) >= 10000)
            {
                if (Directory.Exists(Application.StartupPath + @"\plugin\" + type_name_en))
                {
                    if (File.Exists(Application.StartupPath + @"\plugin\" + type_name_en + @"\" + type_name_en + ".exe"))
                    {
                        System.Diagnostics.Process p = System.Diagnostics.Process.Start(Application.StartupPath + @"\plugin\" + type_name_en + @"\" + type_name_en + ".exe",
                          "/DBSrvIP:" + Settings.DBSrvIP
                          + " /DBSrvPort:" + Settings.DBSrvPort
                          + " /DBconnectID:" + Settings.DBconnectID
                          + " /DBconnectPw:" + Settings.DBconnectPw
                          + " /exam_id:" + exam_id_str
                          + " /operator_id:" + db_operator.operatorID);
                    }
                    else
                    { MessageBox.Show("[Plugin]" + FindingsEditor.Properties.Resources.FileNotExist, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else
                { MessageBox.Show("[Plugin]" + FindingsEditor.Properties.Resources.FolderNotExist, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else
            {
                EditFindings ef = new EditFindings(exam_id_str);
                ef.ShowDialog(this);
                ef.Dispose();
            }
            refreshDgv();
        }

        private void delExam(string exam_id_str)
        {
            Exam exam = new Exam(exam_id_str);

            //If findings was blank, delete exam.
            //If findings was not blank, make exam invisible.
            if (MessageBox.Show(FindingsEditor.Properties.Resources.ConfirmDel, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (exam.findings.Length == 0)
                { exam.delExam(); }
                else
                { exam.makeInvisible(); }

                exam_list.Rows.Clear();
                SearchExam(dateFrom, dateTo, pt_id, department, operator1, op1_5);
                resizeColumns();
            }
        }

        private void printExam(string exam_id_str)
        {
            #region Error Check
            if (!File.Exists(Application.StartupPath + @"\result.html"))
            {
                MessageBox.Show("[Result template file]" + FindingsEditor.Properties.Resources.FileNotExist, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion
            ExamResult er = new ExamResult(exam_id_str);
            er.ShowDialog(this);
            er.Dispose();
        }
        #endregion
        #endregion

        #region Filters
        private void btBlankDraft_Click(object sender, EventArgs e)
        {
            exam_list.DefaultView.RowFilter = "exam_status < 2";
            refreshDgv();
            setFocus2Select();
        }//Show only blank/draft findings.

        private void btDone_Click(object sender, EventArgs e)
        {
            exam_list.DefaultView.RowFilter = "exam_status = 2";
            refreshDgv();
            setFocus2Select();
        }

        private void btChecked_Click(object sender, EventArgs e)
        {
            exam_list.DefaultView.RowFilter = "exam_status = 3";
            refreshDgv();
            setFocus2Select();
        }

        private void btCanceled_Click(object sender, EventArgs e)
        {
            exam_list.DefaultView.RowFilter = "exam_status = 9";
            refreshDgv();
            setFocus2Select();
        }

        private void btShowAll_Click(object sender, EventArgs e)
        {
            exam_list.DefaultView.RowFilter = "exam_status < 10";
            refreshDgv();
            setFocus2Select();
        }

        private void ExamList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Alt == true)
            {
                exam_list.DefaultView.RowFilter = "exam_status < 10";
                refreshDgv();
                setFocus2Select();
            }
            else if (e.KeyCode == Keys.B && e.Alt == true)
            {
                exam_list.DefaultView.RowFilter = "exam_status < 2";
                refreshDgv();
                setFocus2Select();
            }
            else if (e.KeyCode == Keys.D && e.Alt == true)
            {
                exam_list.DefaultView.RowFilter = "exam_status = 2";
                refreshDgv();
                setFocus2Select();
            }
            else if (e.KeyCode == Keys.C && e.Alt == true)
            {
                exam_list.DefaultView.RowFilter = "exam_status = 3";
                refreshDgv();
                setFocus2Select();
            }
            else if (e.KeyCode == Keys.E && e.Alt == true)
            {
                exam_list.DefaultView.RowFilter = "exam_status = 9";
                refreshDgv();
                setFocus2Select();
            }
        }
        #endregion

        private void ExamList_Shown(object sender, EventArgs e)
        { setFocus2Select(); }

        private void setFocus2Select()
        {
            dgvExamList.Focus();
            if (dgvExamList.RowCount != 0)
            { dgvExamList.CurrentCell = dgvExamList["btSelect", 0]; }
        }

        private void refreshDgv()
        {
            exam_list.Rows.Clear();
            SearchExam(dateFrom, dateTo, pt_id, department, operator1, op1_5);
            resizeColumns();
        }
    }
}

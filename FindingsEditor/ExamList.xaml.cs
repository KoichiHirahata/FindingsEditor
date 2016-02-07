using System;
using System.Windows;
using System.Data;
using Npgsql;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace FindingsEditor
{
    /// <summary>
    /// ExamList.xaml の相互作用ロジック
    /// </summary>
    public partial class ExamList : Window
    {
        public DataTable exam_list = new DataTable();
        private BindingListCollectionView cv;
        private string dateFrom;
        private string dateTo;
        private string pt_id;
        private string department;
        private string operator1;
        private Boolean op1_5; //True means searching operator among operator1 to 5

        public ExamList(string _date_from, string _date_to, string _pt_id, string _department, string _operator1, bool _op1_5)
        {
            InitializeComponent();

            dateFrom = _date_from;
            dateTo = _date_to;
            pt_id = _pt_id;
            department = _department;
            operator1 = _operator1;
            op1_5 = _op1_5;

            bool isEmpty = searchExam(dateFrom, dateTo, pt_id, department, operator1, op1_5);

            if (!isEmpty)
            {
                cv = new BindingListCollectionView(exam_list.DefaultView);
                dgExamList.DataContext = cv;

                setDateColumnFormat();
                filterGridView(filterStr.BlankDraft);
            }
        }

        private bool searchExam(string _date_from, string _date_to, string _pt_id, string _department, string _operator, bool _op1_5)
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
                MessageBox.Show(Properties.Resources.ConnectionStringIsWrong, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
            #endregion

            string status_name;
            string exam_type_name;
            if (Settings.lang == "ja")
            {
                status_name = "name_jp";
                exam_type_name = "name_jp";
            }
            else
            {
                status_name = "name_eng";
                exam_type_name = "name_eng";
            }
            string sql =
                "SELECT exam_id, exam.pt_id, pt_name, exam_day, exam_type." + exam_type_name + " AS exam_type_name, department.name1 AS department_name, ward, status."
                + status_name
                + " AS status_name, exam_status, exam_type.type_no AS exam_type_no, exam_type.name_eng AS type_name_en FROM exam" //"exam_type_no" and "type_name_en" are needed to use plugins
                + " INNER JOIN patient ON exam.pt_id = patient.pt_id"
                + " INNER JOIN exam_type ON exam.exam_type = exam_type.type_no"
                + " LEFT JOIN department ON exam.department = department.code"
                + " LEFT JOIN ward ON exam.ward_id = ward.ward_no"
                + " INNER JOIN status ON exam.exam_status = status.status_no"
                + " WHERE exam_visible = true";
            if (_date_from != null)
            { sql += " AND exam_day>='" + _date_from + "' AND exam_day<='" + _date_to + "'"; }
            if (_pt_id != null)
            { sql += " AND exam.pt_id='" + _pt_id + "'"; }
            if (_department != null)
            { sql += " AND exam.department='" + _department + "'"; }
            if (_operator != null)
            {
                if (_op1_5)
                {
                    sql += " AND (exam.operator1='" + _operator
                        + "' OR exam.operator2='" + _operator
                        + "' OR exam.operator3='" + _operator
                        + "' OR exam.operator4='" + _operator
                        + "' OR exam.operator5='" + _operator + "')";
                }
                else
                { sql += " AND exam.operator1='" + _operator + "'"; }
            }

            sql += " ORDER BY exam_id";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(exam_list);

            return exam_list.Rows.Count == 0;
        }

        private void setDateColumnFormat()
        {
            switch (Settings.lang)
            {
                case "ja":
                    clDate.Binding.StringFormat = "yyyy/MM/dd";
                    break;
                default:
                    clDate.Binding.StringFormat = "MM/dd/yyyy";
                    break;
            }
        }

        private enum filterStr { ShowAll, BlankDraft, Done, Checked, Canceled };
        private void filterGridView(filterStr fs)
        {
            switch (fs)
            {
                case filterStr.ShowAll:
                    cv.CustomFilter = null;
                    break;
                case filterStr.BlankDraft:
                    cv.CustomFilter = "exam_status < 2";
                    break;
                case filterStr.Done:
                    cv.CustomFilter = "exam_status = 2";
                    break;
                case filterStr.Checked:
                    cv.CustomFilter = "exam_status = 3";
                    break;
                case filterStr.Canceled:
                    cv.CustomFilter = "exam_status = 9";
                    break;
            }
        }

        #region FilteringButtons
        private void btShowAll_Click(object sender, RoutedEventArgs e)
        { filterGridView(filterStr.ShowAll); }

        private void btBlankDraft_Click(object sender, RoutedEventArgs e)
        { filterGridView(filterStr.BlankDraft); }

        private void btDone_Click(object sender, RoutedEventArgs e)
        { filterGridView(filterStr.Done); }

        private void btChecked_Click(object sender, RoutedEventArgs e)
        { filterGridView(filterStr.Checked); }

        private void btCanceled_Click(object sender, RoutedEventArgs e)
        { filterGridView(filterStr.Canceled); }
        #endregion

        #region GridViewButtons
        private void select_Click(object sender, RoutedEventArgs e)
        {
            EditFindings ef = new EditFindings();
            ef.Owner = this;
            ef.ShowDialog();
        }

        private int getExamId()
        {
            DataGridRow dgr = (DataGridRow)dgExamList.ItemContainerGenerator.ContainerFromIndex(int.Parse(dgExamList.SelectedIndex.ToString()));
            TextBlock exam_id_block = dgExamList.Columns[0].GetCellContent(dgr) as TextBlock;
            return int.Parse(exam_id_block.Text);
        }
        #endregion
    }
}

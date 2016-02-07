using System;
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
        private DataTable dt = new DataTable();//Temporary datatable for diagnoses.
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



        }

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
}

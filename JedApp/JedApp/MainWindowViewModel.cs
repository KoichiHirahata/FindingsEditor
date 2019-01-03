using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JedApp
{
    class MainWindowViewModel : ViewModelBase
    {
        #region ExamList
        private ObservableCollection<Exam> _ExamList;

        public ObservableCollection<Exam> ExamList //ToDo クラスちゃんと設定する
        {
            get
            {
                return _ExamList;
            }
            set
            {
                _ExamList = value;
                RaisePropertyChanged(nameof(ExamList));
            }
        }
        #endregion

        public MainWindowViewModel()
        {

        }

        #region SetExamListWithDate 日付（とstatus）を指定してExamListを取得する関数
        /// <summary>
        /// 日付（とstatus）を指定してExamListを取得する関数
        /// </summary>
        /// <param name="_dt"></param>
        /// <param name="_status"></param>
        public void SetExamListWithDate(DateTime _dt, int? _status = 0)
        {
            if (_status == null)
            {
                ExamList = Exam.GetExamList(_dt.ToString("yyyy-MM-dd"), _dt.ToString("yyyy-MM-dd"), null, null, null, false);
            }
            else
            {
                ExamList = new ObservableCollection<Exam>(Exam.GetExamList(_dt.ToString("yyyy-MM-dd"), _dt.ToString("yyyy-MM-dd"), null, null, null, false)
                    .Where(x => x.exam_status == _status));
            }
        }
        #endregion
    }
}

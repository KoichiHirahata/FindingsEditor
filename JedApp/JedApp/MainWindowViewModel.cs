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
        public ObservableCollection<Patient> ExamList //ToDo クラスちゃんと設定する
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
        private ObservableCollection<Patient> _ExamList;
        #endregion

        public MainWindowViewModel()
        {

        }
    }
}

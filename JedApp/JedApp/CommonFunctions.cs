using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JedApp
{
    class CommonFunctions
    {
        public enum functionResult { success, failed, connectionError };

        /// <summary>
        /// 生年月日から年齢を計算する 
        /// </summary>
        /// <param name="birthDate"></param>
        /// <param name="today"></param>
        /// <returns></returns>
        public static int GetAge(DateTime birthDate, DateTime today)
        {
            return ((today.Year * 10000 + today.Month * 100 + today.Day) -
            (birthDate.Year * 10000 + birthDate.Month * 100 + birthDate.Day)) / 10000;
        }

        public static string GetWareki(DateTime _date)
        {
            CultureInfo culture = new CultureInfo("ja-JP", true);
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();

            return _date.ToString("ggyy年M月d日", culture);
        }

        public static string dateTo8char(string date_str, string lang)
        {
            string yyyy;
            string mm;
            string dd;
            int tempIndex;
            switch (lang)
            {
                case "ja":
                    yyyy = date_str.Substring(0, 4);
                    tempIndex = date_str.IndexOf("/", date_str.IndexOf("/") + 1);
                    mm = date_str.Substring(5, tempIndex - 5);
                    if (mm.Length == 1)
                    { mm = "0" + mm; }
                    dd = date_str.Substring(tempIndex + 1);
                    if (dd.Length == 1)
                    { dd = "0" + dd; }
                    break;
                default:
                    if (date_str.IndexOf("/") == 1)
                    { mm = "0" + date_str.Substring(0, 1); }
                    else
                    { mm = date_str.Substring(0, 2); }

                    tempIndex = date_str.IndexOf("/", date_str.IndexOf("/") + 1);
                    dd = date_str.Substring(date_str.IndexOf("/") + 1, tempIndex - date_str.IndexOf("/") - 1);
                    if (dd.Length == 1)
                    { dd = "0" + dd; }

                    yyyy = date_str.Substring(tempIndex + 1);
                    break;
            }
            return yyyy + mm + dd;
        }

        public static bool IsSameFloatList(List<float> list1, List<float> list2)
        {
            if (list1.Count == list2.Count)
            {
                int tempInt = 0;
                foreach (var item in list1)
                {
                    if (!list2.Contains(item))
                    {
                        tempInt++;
                    }
                }
                //MessageBox.Show(tempInt.ToString());
                return (tempInt > 0) ? false : true;
            }
            else
            {
                return false;
            }
        }

        public static string EmphasizeListedText(string _sourceText, List<string> _emphasisTextList)
        {
            foreach (var item in _emphasisTextList)
            {
                _sourceText = _sourceText.Replace(item, "<span style='color:red;'>" + item + "</span>");
            }

            return _sourceText;
        }
    }
}

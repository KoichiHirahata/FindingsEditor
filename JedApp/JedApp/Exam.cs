using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;

namespace JedApp
{
    public class Exam
    {
        public int exam_id { get; set; }
        public string pt_id { get; set; }
        public string pt_name { get; set; }
        public DateTime exam_day { get; set; }
        public string exam_type_name { get; set; }
        public string name1 { get; set; }
        public string ward { get; set; }
        public string status_name { get; set; }
        #region exam_status
        private int __exam_status;
        public int exam_status
        {
            get
            {
                return __exam_status;
            }
            set
            {
                __exam_status = value;
                if (value == 3)
                {
                    Editable = false; //２次チェックまで終わってたら編集できない仕様
                }
                else
                {
                    Editable = true;
                }
            }
        }
        #endregion
        public int exam_type_no { get; set; }
        public string type_name_en { get; set; }
        public bool Editable { get; set; }

        #region GetExamList(string _date_from, string _date_to, string _pt_id, string _department, string _operator, Boolean _op1_5)
        public static ObservableCollection<Exam> GetExamList(string _date_from, string _date_to, string _pt_id, string _department, string _operator, Boolean _op1_5)
        {
            ObservableCollection<Exam> ret = new ObservableCollection<Exam>();

            try
            {
                using (var conn = new NpgsqlConnection(Settings.retConnStr()))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "select * from get_exam_list(@uid, @upw, @lang, @date_from, @date_to, @p_id, @dep, @op, @op1_5);";

                        #region Add Npgsql parameters
                        cmd.Parameters.AddWithValue("@uid", NpgsqlTypes.NpgsqlDbType.Text, DbOperator.operatorID);
                        cmd.Parameters.AddWithValue("@upw", NpgsqlTypes.NpgsqlDbType.Text, DbOperator.operatorPw);
                        cmd.Parameters.AddWithValue("@lang", NpgsqlTypes.NpgsqlDbType.Text, Settings.lang);

                        #region date_from
                        if (String.IsNullOrWhiteSpace(_date_from) || DateTime.TryParse(_date_from, out DateTime date_from) == false)
                        {
                            cmd.Parameters.AddWithValue("@date_from", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@date_from", NpgsqlTypes.NpgsqlDbType.Date, date_from);
                        }
                        #endregion

                        #region date_to
                        if (String.IsNullOrWhiteSpace(_date_to) || DateTime.TryParse(_date_to, out DateTime date_to) == false)
                        {
                            cmd.Parameters.AddWithValue("@date_to", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@date_to", NpgsqlTypes.NpgsqlDbType.Date, date_to);
                        }
                        #endregion

                        #region pt_id
                        if (String.IsNullOrWhiteSpace(_pt_id))
                        {
                            cmd.Parameters.AddWithValue("@p_id", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@p_id", _pt_id);
                        }
                        #endregion

                        #region department
                        if (String.IsNullOrWhiteSpace(_department))
                        {
                            cmd.Parameters.AddWithValue("@dep", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@dep", NpgsqlTypes.NpgsqlDbType.Smallint, _department);
                        }
                        #endregion

                        #region operator
                        if (String.IsNullOrWhiteSpace(_operator))
                        {
                            cmd.Parameters.AddWithValue("@op", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@op", _operator);
                        }
                        #endregion

                        cmd.Parameters.AddWithValue("@op1_5", NpgsqlTypes.NpgsqlDbType.Boolean, _op1_5);
                        #endregion

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ret.Add(new Exam()
                                {
                                    exam_id = int.Parse(reader["exam_id"].ToString()),
                                    pt_id = reader["pt_id"].ToString(),
                                    pt_name = reader["pt_name"].ToString(),
                                    exam_day = DateTime.Parse(reader["exam_day"].ToString()),
                                    exam_type_name = reader["exam_type_name"].ToString(),
                                    name1 = reader["name1"].ToString(),
                                    ward = reader["ward"].ToString(),
                                    status_name = reader["status_name"].ToString(),
                                    exam_status = int.Parse(reader["exam_status"].ToString()),
                                    exam_type_no = int.Parse(reader["exam_type_no"].ToString()),
                                    type_name_en = reader["type_name_en"].ToString()
                                });
                            }
                        }
                    }
                    conn.Close();
                }
                return ret;
            }
            #region catch
            catch (Exception ex)
            {
                MessageBox.Show("[GetExamList] " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return ret;
            }
            #endregion
        }
        #endregion
    }
}

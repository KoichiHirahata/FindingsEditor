using System;
using System.Collections.Generic;
using Npgsql;
using System.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace JedApp
{
    class DB
    {
        public static List<FeOperator> operators = new List<FeOperator>();

        public static void DB_init()
        {
            GetOperators();
        }

        private static void GetOperators()
        {
            try
            {
                using (var conn = new NpgsqlConnection(Settings.retConnStr()))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT * FROM get_operators(@id, @pw)";
                        cmd.Parameters.AddWithValue("id", DbOperator.operatorID);
                        cmd.Parameters.AddWithValue("pw", DbOperator.operatorPw);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                operators.Add(new FeOperator(reader["operator_id"].ToString()
                                    , reader["op_name"].ToString()
                                    , ((reader["op_order"] != DBNull.Value) ? int.Parse(reader["op_order"].ToString()) : null as int?)
                                    , ((reader["department"] != DBNull.Value) ? int.Parse(reader["department"].ToString()) : null as int?)
                                    , int.Parse(reader["op_category"].ToString())
                                    , ((reader["op_visible"] != DBNull.Value) ? bool.Parse(reader["op_visible"].ToString()) : false)
                                    ));
                            }
                        }
                        conn.Close();
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("[GetOperators]" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("[GetOperators]" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("[GetOperators]" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static FeOperator GetFeOperator(string opId)
        {
            try
            {
                if (operators.FindIndex(x => x.operator_id == opId) != -1)
                {
                    return operators.Find(x => x.operator_id == opId);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return null;
        }
    }

    class FeOperator : IEquatable<FeOperator>
    {
        public string operator_id { get; set; }
        public string op_name { get; set; }
        public int? op_order { get; set; }
        public int? department { get; set; }
        public int op_category { get; set; }
        public bool op_visible { get; set; }

        public FeOperator(string _operator_id, string _op_name, int? _op_order, int? _department, int _op_category, bool _op_visible)
        {
            operator_id = _operator_id;
            op_name = _op_name;
            op_order = _op_order;
            department = _department;
            op_category = _op_category;
            op_visible = _op_visible;
        }

        public override string ToString()
        {
            return "ID: " + operator_id
                + "   Name: " + op_name
                + "   Order: " + op_order.ToString()
                + "   Department: " + department.ToString()
                + "   Category: " + op_category.ToString()
                + "   Visible: " + op_visible.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            FeOperator objAsFeOperator = obj as FeOperator;

            if (objAsFeOperator == null)
            {
                return false;
            }
            else
            {
                return Equals(objAsFeOperator);
            }
        }

        public override int GetHashCode()
        {
            return operator_id.GetHashCode();
        }

        public bool Equals(FeOperator other)
        {
            if (other == null) return false;
            return (operator_id.Equals(other.operator_id));
        }
        // Should also override == and != operators.
    }

    public class Patient
    {
        public string pt_id { get; set; }
        public string pt_name { get; set; }
        public string furigana { get; set; }
        public DateTime? birthday { get; set; }
        public int? gender { get; set; }
        public string gender_str { get; set; }
        public string pt_memo { get; set; }

        public static Patient GetPtInfo(string _pt_id)
        {
            try
            {
                using (var conn = new NpgsqlConnection(Settings.retConnStr()))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT * FROM get_pt_info(@id, @pw, @pt_id)";
                        cmd.Parameters.AddWithValue("id", DbOperator.operatorID);
                        cmd.Parameters.AddWithValue("pw", DbOperator.operatorPw);
                        cmd.Parameters.AddWithValue("pt_id", _pt_id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            string tmpGenderStr = null;

                            if (reader.Read())
                            {
                                if (reader["gender"] != DBNull.Value)
                                {
                                    switch (reader["gender"].ToString())
                                    {
                                        case "0":
                                            tmpGenderStr = "女性";
                                            break;
                                        case "1":
                                            tmpGenderStr = "男性";
                                            break;
                                        default:
                                            break;
                                    }
                                }

                                Patient retPt = new Patient
                                {
                                    pt_id = _pt_id,
                                    pt_name = reader["pt_name"].ToString(),
                                    furigana = (reader["furigana"] != DBNull.Value) ? reader["furigana"].ToString() : null,
                                    birthday = (reader["birthday"] != DBNull.Value) ? DateTime.Parse(reader["birthday"].ToString()) : null as DateTime?,
                                    gender = (reader["gender"] != DBNull.Value) ? int.Parse(reader["gender"].ToString()) : null as int?,
                                    gender_str = tmpGenderStr,
                                    pt_memo = (reader["pt_memo"] != DBNull.Value) ? reader["pt_memo"].ToString() : null
                                };
                                conn.Close();
                                return retPt;
                            }
                            else
                            {
                                if (!String.IsNullOrWhiteSpace(Settings.ptInfoPlugin))
                                {
                                    string command = Settings.ptInfoPlugin;
                                    //string command = Settings.startupPath + "\\plugin\\connOrca.exe";

                                    if (File.Exists(command))
                                    {
                                        try
                                        {
                                            ProcessStartInfo psInfo = new ProcessStartInfo();

                                            psInfo.FileName = command;
                                            psInfo.Arguments = _pt_id;
                                            psInfo.CreateNoWindow = true; // Do not open console window
                                            psInfo.UseShellExecute = false; // Do not use shell

                                            psInfo.RedirectStandardOutput = true;

                                            Process p = Process.Start(psInfo);
                                            string output = p.StandardOutput.ReadToEnd();

                                            output = output.Replace("\r\r\n", "\n"); // Replace new line code

                                            conn.Close();

                                            if (!String.IsNullOrWhiteSpace(output))
                                            {
                                                using (var conn2 = new NpgsqlConnection(Settings.retConnStr()))
                                                {
                                                    conn2.Open();
                                                    using (var cmd2 = new NpgsqlCommand())
                                                    {
                                                        cmd2.Connection = conn2;
                                                        cmd2.CommandText = "select * from insert_patient(@id, @pw, @pt_id, @p_name, @p_birthday, @p_gender, @furigana);";

                                                        cmd2.Parameters.AddWithValue("id", DbOperator.operatorID);
                                                        cmd2.Parameters.AddWithValue("pw", DbOperator.operatorPw);
                                                        cmd2.Parameters.AddWithValue("pt_id", _pt_id);
                                                        cmd2.Parameters.AddWithValue("p_name", FileControl.readItemSettingFromText(output, "Patient Name:"));
                                                        cmd2.Parameters.AddWithValue("p_birthday", FileControl.readItemSettingFromText(output, "Birth date:"));
                                                        cmd2.Parameters.AddWithValue("p_gender", int.Parse(FileControl.readItemSettingFromText(output, "Gender:")));
                                                        cmd2.Parameters.AddWithValue("furigana", FileControl.readItemSettingFromText(output, "Kana:"));

                                                        cmd2.ExecuteNonQuery();
                                                    }
                                                    conn2.Close();
                                                }

                                                switch (FileControl.readItemSettingFromText(output, "Gender:"))
                                                {
                                                    case "0":
                                                        tmpGenderStr = "女性";
                                                        break;
                                                    case "1":
                                                        tmpGenderStr = "男性";
                                                        break;
                                                    default:
                                                        break;
                                                }

                                                Patient retPt = new Patient
                                                {
                                                    pt_id = _pt_id,
                                                    pt_name = FileControl.readItemSettingFromText(output, "Patient Name:"),
                                                    furigana = FileControl.readItemSettingFromText(output, "Kana:"),
                                                    birthday = DateTime.Parse(FileControl.readItemSettingFromText(output, "Birth date:")),
                                                    gender = int.Parse(FileControl.readItemSettingFromText(output, "Gender:")),
                                                    gender_str = tmpGenderStr,
                                                    pt_memo = null
                                                };

                                                return retPt;
                                            }
                                            else
                                            {
                                                //プラグインで該当患者が見つからなければnullを返す
                                                return null;
                                            }
                                        }
                                        #region catch
                                        catch (NpgsqlException ex)
                                        {
                                            MessageBox.Show("[Npgsql]" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                            conn.Close();
                                            return null;
                                        }
                                        catch (InvalidOperationException ex)
                                        {
                                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                            conn.Close();
                                            return null;
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                            conn.Close();
                                            return null;
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        MessageBox.Show("Pluginがありません", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return null;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Pluginが設定されていません", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return null;
                                }
                            }
                        }
                    }
                }
            }
            #region catch
            catch (NpgsqlException ex)
            {
                MessageBox.Show("[Npgsql]" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion

            return null;
        }
    }
}

using System;
using Npgsql;
using System.Windows;

namespace JedApp
{
    class DbOperator
    {
        public static string operatorID { get; set; }
        public static string operatorPw { get; set; }
        public static string operatorName { get; set; }
        public static int op_category { get; set; }
        public static bool Admin_user { get; set; }
        public static bool AuthDone { get; set; }
        public static bool CanDiag { get; set; }
        public static bool AllowFC { get; set; }

        DbOperator()
        {
            reset_op();
        }

        public static void reset_op()
        {
            operatorID = "";
            operatorPw = "";
            operatorName = "";
            Admin_user = false;
            AuthDone = false;
            CanDiag = false;
            AllowFC = false;
        }

        public enum idPwCheckResult { success, failed, connectionError };

        public static idPwCheckResult idPwCheck(string opId, string opPw)
        {
            try
            {
                using (var conn = new NpgsqlConnection(Settings.retConnStr()))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT * FROM is_correct_pw(@id, @pw)";
                        cmd.Parameters.AddWithValue("id", opId);
                        cmd.Parameters.AddWithValue("pw", opPw);

                        using (var reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            if (reader["is_correct_pw"] == DBNull.Value)
                            {
                                conn.Close();
                                MessageBox.Show("IDまたはパスワードが不正です", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return idPwCheckResult.failed;
                            }
                            else if (bool.Parse(reader["is_correct_pw"].ToString()))
                            {
                                operatorID = opId;
                                operatorPw = opPw;
                                AuthDone = true;

                                //パスワードが正しければ、次のDB関数を実行へ。
                            }
                            else
                            {
                                conn.Close();
                                MessageBox.Show("IDまたはパスワードが不正です", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return idPwCheckResult.failed;
                            }
                        }

                        cmd.CommandText = "SELECT * FROM get_operator_info(@id, @pw)";

                        using (var reader = cmd.ExecuteReader())
                        {
                            reader.Read();

                            //返ってきているが利用していないデータ：department, op_category_name

                            operatorName = reader["op_name"].ToString();
                            op_category = int.Parse(reader["op_category"].ToString());
                            Admin_user = Boolean.Parse(reader["admin_op"].ToString());
                            CanDiag = Boolean.Parse(reader["can_diag"].ToString());
                            AllowFC = Boolean.Parse(reader["allow_fc"].ToString());

                            conn.Close();

                            return idPwCheckResult.success;
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("[idPwCheck]" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("[idPwCheck]" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("[idPwCheck]" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return idPwCheckResult.failed;
        }

        public static void SetOperatorInfo()
        {
            FeOperator fo = DB.GetFeOperator(operatorID);

            if (fo != null)
            {
                operatorName = fo.op_name;
            }

            try
            {
                using (var conn = new NpgsqlConnection(Settings.retConnStr()))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT * FROM is_correct_admin_pw(@id, @pw)";
                        cmd.Parameters.AddWithValue("id", operatorID);
                        cmd.Parameters.AddWithValue("pw", operatorPw);

                        using (var reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            if (reader["is_correct_admin_pw"] == DBNull.Value)
                            {
                                Admin_user = false;
                            }
                            else if (bool.Parse(reader["is_correct_admin_pw"].ToString()))
                            {
                                Admin_user = true;
                            }
                            else
                            {
                                Admin_user = false;
                            }
                        }

                        cmd.CommandText = "SELECT * FROM is_correct_dr_pw(@id, @pw)";
                        using (var reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            if (reader["is_correct_dr_pw"] == DBNull.Value)
                            {
                                conn.Close();
                                CanDiag = false;
                            }
                            else if (bool.Parse(reader["is_correct_dr_pw"].ToString()))
                            {
                                conn.Close();
                                CanDiag = true;
                                //DbOperator.allowFC = (Boolean)row["allow_fc"];
                            }
                            else
                            {
                                conn.Close();
                                CanDiag = false;
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("[idPwCheck]" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

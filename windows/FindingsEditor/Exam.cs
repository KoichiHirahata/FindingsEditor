using System;
using System.Windows.Forms;
using System.Data;
using Npgsql;

namespace FindingsEdior
{
    public class Exam
    {
        public int exam_id { get; set; }
        public string pt_id { get; set; }
        public string pt_name { get; set; }
        public string purpose { get; set; }
        public int? department { get; set; }
        public string order_dr { get; set; }
        public string ward_id { get; set; }
        public DateTime exam_day { get; set; }
        public int exam_type { get; set; }
        public int? pathology { get; set; }  //0 means no pathology test. 1 means pathology test exist.
        public string patho_no { get; set; }  //pathology report ID.
        public int? reply_patho { get; set; }  //reply exist or not.
        public string patho_result { get; set; } //Pathology results
        public string operator1 { get; set; }
        public string operator2 { get; set; }
        public string operator3 { get; set; }
        public string operator4 { get; set; }
        public string operator5 { get; set; }
        public string diag_dr { get; set; }
        public string final_diag_dr { get; set; }
        public int? equipment { get; set; }
        public int? place_no { get; set; }
        public string findings { get; set; }
        public string comment { get; set; }
        public int exam_status { get; set; }
        public int? explanation { get; set; }

        public Exam(string _exam_id)
        {
            exam_id = int.Parse(_exam_id);
            #region initial settings
            //These settings are needed for notice these properties were changed or not.
            department = 0;
            equipment = 0;
            place_no = 0;
            #endregion
            readExamData(exam_id);
        }

        private void readExamData(int _exam_id)
        {
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

            try
            { conn.Open(); }
            catch (NpgsqlException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.CouldntOpenConn, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.ConnClosed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return;
            }
            #endregion

            //ここから下がデータの読み込み部分。
            string sql = "SELECT exam_id, exam.pt_id AS ptID, pt_name, purpose, department, order_dr, ward_id, exam_day, exam_type,"
                + " pathology, patho_no, reply_patho,"
                + " operator1, operator2, operator3, operator4, operator5, diag_dr, final_diag_dr,"
                + " equipment, place_no, findings, comment, exam_status, explanation, patho_result"
                + " FROM exam"
                + " INNER JOIN patient ON exam.pt_id = patient.pt_id"
                + " WHERE exam_id = " + _exam_id.ToString();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                conn.Close();
                MessageBox.Show(FindingsEditor.Properties.Resources.NoData, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DataRow row = dt.Rows[0];
                pt_id = row["ptID"].ToString();
                pt_name = row["pt_name"].ToString();
                purpose = row["purpose"].ToString();
                if (row["department"].ToString().Length != 0)
                    department = int.Parse(row["department"].ToString());
                order_dr = row["order_dr"].ToString();
                ward_id = row["ward_id"].ToString();
                exam_day = (DateTime)row["exam_day"];
                if (row["exam_type"].ToString().Length != 0)
                    exam_type = int.Parse(row["exam_type"].ToString());
                if (row["pathology"].ToString().Length != 0)
                    pathology = int.Parse(row["pathology"].ToString());
                patho_no = row["patho_no"].ToString();
                if (row["reply_patho"].ToString().Length != 0)
                    reply_patho = int.Parse(row["reply_patho"].ToString());
                operator1 = row["operator1"].ToString();
                operator2 = row["operator2"].ToString();
                operator3 = row["operator3"].ToString();
                operator4 = row["operator4"].ToString();
                operator5 = row["operator5"].ToString();
                diag_dr = row["diag_dr"].ToString();
                final_diag_dr = row["final_diag_dr"].ToString();
                if (row["equipment"].ToString().Length != 0)
                    equipment = int.Parse(row["equipment"].ToString());
                if (row["place_no"].ToString().Length != 0)
                    place_no = int.Parse(row["place_no"].ToString());
                findings = row["findings"].ToString();
                comment = row["comment"].ToString();
                if (row["exam_status"].ToString().Length != 0)
                    exam_status = int.Parse(row["exam_status"].ToString());
                if (row["explanation"].ToString().Length != 0)
                    explanation = int.Parse(row["explanation"].ToString());
                patho_result = row["patho_result"].ToString();
                conn.Close();
            }
        }

        public void saveFindingsEtc()
        {
            try
            {
                using (var conn = new NpgsqlConnection(Settings.retConnStr()))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE exam SET purpose=@p0, order_dr=@p1, pathology=@p2, patho_no=@p3, reply_patho=@p4, "
                            + "findings=@p5, comment=@p6, exam_status=@p7, explanation=@p8, patho_result=@p9, department=@dep, "
                            + "ward_id=@ward, operator1=@op1, operator2=@op2, operator3=@op3, operator4=@op4, operator5=@op5, "
                            + "diag_dr=@ddr, final_diag_dr=@fdr, equipment=@eq, place_no=@pl "
                            + "WHERE exam_id=@e_id;";
                        cmd.Parameters.AddWithValue("p0", (String.IsNullOrWhiteSpace(purpose)) ? "" : purpose);
                        cmd.Parameters.AddWithValue("p1", (String.IsNullOrWhiteSpace(order_dr)) ? "" : order_dr);
                        cmd.Parameters.AddWithValue("p2", pathology.HasValue ? (object)pathology : DBNull.Value);
                        cmd.Parameters.AddWithValue("p3", (String.IsNullOrWhiteSpace(patho_no)) ? "" : patho_no);
                        cmd.Parameters.AddWithValue("p4", reply_patho.HasValue ? (object)reply_patho : DBNull.Value);
                        cmd.Parameters.AddWithValue("p5", (String.IsNullOrWhiteSpace(findings)) ? "" : findings);
                        cmd.Parameters.AddWithValue("p6", (String.IsNullOrWhiteSpace(comment)) ? "" : comment);
                        cmd.Parameters.AddWithValue("p7", exam_status);
                        cmd.Parameters.AddWithValue("p8", explanation.HasValue ? (object)explanation : DBNull.Value);
                        cmd.Parameters.AddWithValue("p9", (String.IsNullOrWhiteSpace(patho_result)) ? "" : patho_result);
                        cmd.Parameters.AddWithValue("dep", (department != 0) ? (object)department : DBNull.Value);
                        cmd.Parameters.AddWithValue("ward", (String.IsNullOrWhiteSpace(ward_id)) ? (object)DBNull.Value : ward_id);
                        cmd.Parameters.AddWithValue("op1", (String.IsNullOrWhiteSpace(operator1)) ? (object)DBNull.Value : operator1);
                        cmd.Parameters.AddWithValue("op2", (String.IsNullOrWhiteSpace(operator2)) ? (object)DBNull.Value : operator2);
                        cmd.Parameters.AddWithValue("op3", (String.IsNullOrWhiteSpace(operator3)) ? (object)DBNull.Value : operator3);
                        cmd.Parameters.AddWithValue("op4", (String.IsNullOrWhiteSpace(operator4)) ? (object)DBNull.Value : operator4);
                        cmd.Parameters.AddWithValue("op5", (String.IsNullOrWhiteSpace(operator5)) ? (object)DBNull.Value : operator5);
                        cmd.Parameters.AddWithValue("ddr", (String.IsNullOrWhiteSpace(diag_dr)) ? (object)DBNull.Value : diag_dr);
                        cmd.Parameters.AddWithValue("fdr", (String.IsNullOrWhiteSpace(final_diag_dr)) ? (object)DBNull.Value : final_diag_dr);
                        cmd.Parameters.AddWithValue("eq", (equipment != 0) ? (object)equipment : DBNull.Value);
                        cmd.Parameters.AddWithValue("pl", (place_no != 0) ? (object)place_no : DBNull.Value);
                        cmd.Parameters.AddWithValue("e_id", exam_id);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (NpgsqlException nex)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.DataBaseError + "\r\n" + nex.Message
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.DataBaseError + "\r\n" + ex.Message
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.DataBaseError + "\r\n" + ex.Message
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void makeInvisible()
        {
            string sql = "UPDATE exam SET exam_visible=false WHERE exam_id=" + exam_id;
            uckyFunctions.ExeNonQuery(sql);
        }

        public void delExam()
        {
            string sql = "DELETE FROM exam WHERE exam_id=" + exam_id;
            uckyFunctions.ExeNonQuery(sql);
        }

        public string getExamTypeName()
        {
            string sql;
            if (Settings.lang == "ja")
            { sql = "SELECT name_jp FROM exam_type WHERE type_no=" + exam_type.ToString(); }
            else
            { sql = "SELECT name_eng FROM exam_type WHERE type_no=" + exam_type.ToString(); }
            return uckyFunctions.getSelectString(sql, Settings.DBSrvIP, Settings.DBSrvPort, Settings.DBconnectID, Settings.DBconnectPw, Settings.DBname);
        }

        public string getEnglishExamTypeName()
        {
            string sql;
            sql = "SELECT name_eng FROM exam_type WHERE type_no=" + exam_type.ToString();
            return uckyFunctions.getSelectString(sql, Settings.DBSrvIP, Settings.DBSrvPort, Settings.DBconnectID, Settings.DBconnectPw, Settings.DBname);
        }

        public string getDepartmentName()
        {
            string sql;
            sql = "SELECT name1 FROM department WHERE code=" + department.ToString();
            return uckyFunctions.getSelectString(sql, Settings.DBSrvIP, Settings.DBSrvPort, Settings.DBconnectID, Settings.DBconnectPw, Settings.DBname);
        }

        public string getWardName()
        {
            DataRow[] drs;
            if (String.IsNullOrWhiteSpace(ward_id))
            { return ""; }
            else
            {
                drs = CLocalDB.localDB.Tables["ward"].Select("ward_no='" + ward_id + "'");
                return drs[0]["ward"].ToString();
            }
        }

        public string getAllOperators()
        {
            string str = null;
            DataRow[] drs;
            if (!string.IsNullOrWhiteSpace(operator1))
            {
                drs = CLocalDB.localDB.Tables["allOp"].Select("operator_id='" + operator1 + "'");
                str = drs[0]["op_name"].ToString();
            }

            if (!string.IsNullOrWhiteSpace(operator2))
            {
                drs = CLocalDB.localDB.Tables["allOp"].Select("operator_id='" + operator2 + "'");
                str += "/" + drs[0]["op_name"].ToString();
            }

            if (!string.IsNullOrWhiteSpace(operator3))
            {
                drs = CLocalDB.localDB.Tables["allOp"].Select("operator_id='" + operator3 + "'");
                str += "/" + drs[0]["op_name"].ToString();
            }

            if (!string.IsNullOrWhiteSpace(operator4))
            {
                drs = CLocalDB.localDB.Tables["allOp"].Select("operator_id='" + operator4 + "'");
                str += "/" + drs[0]["op_name"].ToString();
            }

            if (!string.IsNullOrWhiteSpace(operator5))
            {
                drs = CLocalDB.localDB.Tables["allOp"].Select("operator_id='" + operator5 + "'");
                str += "/" + drs[0]["op_name"].ToString();
            }

            return str;
        }

        public string getPlaceName()
        {
            string sql;
            sql = "SELECT name1 FROM place WHERE place_no=" + place_no.ToString();
            return uckyFunctions.getSelectString(sql, Settings.DBSrvIP, Settings.DBSrvPort, Settings.DBconnectID, Settings.DBconnectPw, Settings.DBname);
        }

        public string getEquipmentName()
        {
            string sql;
            sql = "SELECT name FROM equipment WHERE equipment_no=" + equipment.ToString();
            return uckyFunctions.getSelectString(sql, Settings.DBSrvIP, Settings.DBSrvPort, Settings.DBconnectID, Settings.DBconnectPw, Settings.DBname);
        }

        public string getDiagDr()
        {
            if (string.IsNullOrWhiteSpace(diag_dr))
            { return null; }
            else
            {
                DataRow[] drs;
                drs = CLocalDB.localDB.Tables["allOp"].Select("operator_id='" + diag_dr + "'");
                return drs[0]["op_name"].ToString();
            }
        }

        public string getFinalDiagDr()
        {
            if (string.IsNullOrWhiteSpace(final_diag_dr))
            { return null; }
            else
            {
                DataRow[] drs;
                drs = CLocalDB.localDB.Tables["allOp"].Select("operator_id='" + final_diag_dr + "'");
                return drs[0]["op_name"].ToString();
            }
        }

        public string getExamStatus()
        {
            DataRow[] drs;
            drs = CLocalDB.localDB.Tables["exam_status"].Select("status_no=" + exam_status);
            return drs[0]["status_name"].ToString();
        }

        public string getDiagnoses()
        {
            #region Npgsql connection
            NpgsqlConnection conn;
            conn = new NpgsqlConnection(Settings.retConnStr());

            try
            { conn.Open(); }
            catch (NpgsqlException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.CouldntOpenConn, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.ConnClosed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
            #endregion

            string sql;
            if (Settings.lang == "ja")
            { sql = "SELECT name_jp AS name, suspect, premodifier, postmodifier FROM diag INNER JOIN diag_name ON diag.diag_code=diag_name.no WHERE exam_no=" + exam_id + " ORDER BY diag_no"; }
            else
            { sql = "SELECT name_eng AS name, suspect, premodifier, postmodifier FROM diag INNER JOIN diag_name ON diag.diag_code=diag_name.no WHERE exam_no=" + exam_id + " ORDER BY diag_no"; }

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();

            if (dt.Rows.Count == 0)
                return "";

            string str = null;
            foreach (DataRow dr in dt.Rows)
            {
                if (!string.IsNullOrWhiteSpace(dr["premodifier"].ToString()))
                { str += dr["premodifier"].ToString(); }

                str += dr["name"].ToString();

                if (!string.IsNullOrWhiteSpace(dr["postmodifier"].ToString()))
                { str += dr["postmodifier"].ToString(); }

                if (!string.IsNullOrWhiteSpace(dr["suspect"].ToString()))
                {
                    if ((Boolean)dr["suspect"])
                    { str += FindingsEditor.Properties.Resources.Suspect; }
                }
                str += Environment.NewLine;
            }
            return str;
        }
    }
}

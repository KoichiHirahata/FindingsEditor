using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
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
        public int department { get; set; }
        public string order_dr { get; set; }
        public string ward_id { get; set; }
        public DateTime exam_day { get; set; }
        public int exam_type { get; set; }
        public int pathology { get; set; }  //0 means no pathology test. 1 means pathology test exist.
        public string patho_no { get; set; }  //pathology report ID.
        public int reply_patho { get; set; }  //reply exist or not.
        public string patho_result { get; set; } //Pathology results
        public string operator1 { get; set; }
        public string operator2 { get; set; }
        public string operator3 { get; set; }
        public string operator4 { get; set; }
        public string operator5 { get; set; }
        public string diag_dr { get; set; }
        public string final_diag_dr { get; set; }
        public int equipment { get; set; }
        public int place_no { get; set; }
        public string findings { get; set; }
        public string comment { get; set; }
        public int exam_status { get; set; }
        public int explanation { get; set; }

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
                conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                    Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);
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
                this.pt_id = row["ptID"].ToString();
                this.pt_name = row["pt_name"].ToString();
                this.purpose = row["purpose"].ToString();
                if (row["department"].ToString().Length != 0)
                    this.department = int.Parse(row["department"].ToString());
                this.order_dr = row["order_dr"].ToString();
                this.ward_id = row["ward_id"].ToString();
                this.exam_day = (DateTime)row["exam_day"];
                if (row["exam_type"].ToString().Length != 0)
                    this.exam_type = int.Parse(row["exam_type"].ToString());
                if (row["pathology"].ToString().Length != 0)
                    this.pathology = int.Parse(row["pathology"].ToString());
                this.patho_no = row["patho_no"].ToString();
                if (row["reply_patho"].ToString().Length != 0)
                    this.reply_patho = int.Parse(row["reply_patho"].ToString());
                this.operator1 = row["operator1"].ToString();
                this.operator2 = row["operator2"].ToString();
                this.operator3 = row["operator3"].ToString();
                this.operator4 = row["operator4"].ToString();
                this.operator5 = row["operator5"].ToString();
                this.diag_dr = row["diag_dr"].ToString();
                this.final_diag_dr = row["final_diag_dr"].ToString();
                if (row["equipment"].ToString().Length != 0)
                    this.equipment = int.Parse(row["equipment"].ToString());
                if (row["place_no"].ToString().Length != 0)
                    this.place_no = int.Parse(row["place_no"].ToString());
                this.findings = row["findings"].ToString();
                this.comment = row["comment"].ToString();
                if (row["exam_status"].ToString().Length != 0)
                    this.exam_status = int.Parse(row["exam_status"].ToString());
                if (row["explanation"].ToString().Length != 0)
                    this.explanation = int.Parse(row["explanation"].ToString());
                this.patho_result = row["patho_result"].ToString();
                conn.Close();
            }
        }

        public void saveFindingsEtc()
        {
            string sql = "UPDATE exam SET purpose=:p0, order_dr=:p1, pathology=:p2, patho_no=:p3, reply_patho=:p4, "
                + "findings=:p5, comment=:p6, exam_status=:p7, explanation=:p8, patho_result=:p9 "
                + "WHERE exam_id=" + exam_id.ToString();

            uckyFunctions.ExeNonQuery(sql, purpose, order_dr, pathology.ToString(), patho_no, reply_patho.ToString(),
                findings, comment, exam_status.ToString(), explanation.ToString(), patho_result.ToString());

            string column;
            if (department != 0)
            {
                column = "department";
                sql = "UPDATE exam SET " + column + "=:p0 WHERE exam_id=" + exam_id.ToString();
                switch (uckyFunctions.ExeNonQuery(sql, department.ToString()))
                {
                    case uckyFunctions.functionResult.connectionError:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.failed:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.success:
                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(ward_id))
            {
                column = "ward_id";
                sql = "UPDATE exam SET " + column + "=:p0 WHERE exam_id=" + exam_id.ToString();
                switch (uckyFunctions.ExeNonQuery(sql, ward_id))
                {
                    case uckyFunctions.functionResult.connectionError:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.failed:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.success:
                        break;
                }
            }

            #region operator1
            column = "operator1";
            if (!string.IsNullOrWhiteSpace(operator1))
            {
                sql = "UPDATE exam SET " + column + "=:p0 WHERE exam_id=" + exam_id.ToString();
                switch (uckyFunctions.ExeNonQuery(sql, operator1))
                {
                    case uckyFunctions.functionResult.connectionError:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.failed:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.success:
                        break;
                }
            }
            else
            {
                sql = "UPDATE exam SET " + column + "=NULL WHERE exam_id=" + exam_id.ToString();
                switch (uckyFunctions.ExeNonQuery(sql, operator1))
                {
                    case uckyFunctions.functionResult.connectionError:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.failed:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.success:
                        break;
                }
            }
            #endregion

            #region operator2
            column = "operator2";
            if (!string.IsNullOrWhiteSpace(operator2))
            {
                sql = "UPDATE exam SET " + column + "=:p0 WHERE exam_id=" + exam_id.ToString();
                switch (uckyFunctions.ExeNonQuery(sql, operator2))
                {
                    case uckyFunctions.functionResult.connectionError:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.failed:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.success:
                        break;
                }
            }
            else
            {
                sql = "UPDATE exam SET " + column + "=NULL WHERE exam_id=" + exam_id.ToString();
                switch (uckyFunctions.ExeNonQuery(sql, operator1))
                {
                    case uckyFunctions.functionResult.connectionError:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.failed:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.success:
                        break;
                }
            }
            #endregion

            #region operator3
            column = "operator3";
            if (!string.IsNullOrWhiteSpace(operator3))
            {
                sql = "UPDATE exam SET " + column + "=:p0 WHERE exam_id=" + exam_id.ToString();
                switch (uckyFunctions.ExeNonQuery(sql, operator3))
                {
                    case uckyFunctions.functionResult.connectionError:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.failed:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.success:
                        break;
                }
            }
            else
            {
                sql = "UPDATE exam SET " + column + "=NULL WHERE exam_id=" + exam_id.ToString();
                switch (uckyFunctions.ExeNonQuery(sql, operator1))
                {
                    case uckyFunctions.functionResult.connectionError:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.failed:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.success:
                        break;
                }
            }
            #endregion

            #region operator4
            column = "operator4";
            if (!string.IsNullOrWhiteSpace(operator4))
            {
                sql = "UPDATE exam SET " + column + "=:p0 WHERE exam_id=" + exam_id.ToString();
                switch (uckyFunctions.ExeNonQuery(sql, operator4))
                {
                    case uckyFunctions.functionResult.connectionError:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.failed:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.success:
                        break;
                }
            }
            else
            {
                sql = "UPDATE exam SET " + column + "=NULL WHERE exam_id=" + exam_id.ToString();
                switch (uckyFunctions.ExeNonQuery(sql, operator1))
                {
                    case uckyFunctions.functionResult.connectionError:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.failed:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.success:
                        break;
                }
            }
            #endregion

            #region operator5
            column = "operator5";
            if (!string.IsNullOrWhiteSpace(operator5))
            {
                sql = "UPDATE exam SET " + column + "=:p0 WHERE exam_id=" + exam_id.ToString();
                switch (uckyFunctions.ExeNonQuery(sql, operator5))
                {
                    case uckyFunctions.functionResult.connectionError:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.failed:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.success:
                        break;
                }
            }
            else
            {
                sql = "UPDATE exam SET " + column + "=NULL WHERE exam_id=" + exam_id.ToString();
                switch (uckyFunctions.ExeNonQuery(sql, operator1))
                {
                    case uckyFunctions.functionResult.connectionError:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.failed:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.success:
                        break;
                }
            }
            #endregion

            #region diag_dr
            if (!string.IsNullOrWhiteSpace(diag_dr))
            {
                column = "diag_dr";
                sql = "UPDATE exam SET " + column + "=:p0 WHERE exam_id=" + exam_id.ToString();
                switch (uckyFunctions.ExeNonQuery(sql, diag_dr))
                {
                    case uckyFunctions.functionResult.connectionError:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.failed:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.success:
                        break;
                }
            }
            #endregion

            #region final_diag_dr
            if (!string.IsNullOrWhiteSpace(final_diag_dr))
            {
                column = "final_diag_dr";
                sql = "UPDATE exam SET " + column + "=:p0 WHERE exam_id=" + exam_id.ToString();
                switch (uckyFunctions.ExeNonQuery(sql, final_diag_dr))
                {
                    case uckyFunctions.functionResult.connectionError:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.failed:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.success:
                        break;
                }
            }
            #endregion

            if (equipment != 0)
            {
                column = "equipment";
                sql = "UPDATE exam SET " + column + "=:p0 WHERE exam_id=" + exam_id.ToString();
                switch (uckyFunctions.ExeNonQuery(sql, equipment.ToString()))
                {
                    case uckyFunctions.functionResult.connectionError:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.failed:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.success:
                        break;
                }
            }
            if (place_no != 0)
            {
                column = "place_no";
                sql = "UPDATE exam SET " + column + "=:p0 WHERE exam_id=" + exam_id.ToString();
                switch (uckyFunctions.ExeNonQuery(sql, place_no.ToString()))
                {
                    case uckyFunctions.functionResult.connectionError:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.failed:
                        MessageBox.Show("[" + column + "]" + FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case uckyFunctions.functionResult.success:
                        break;
                }
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
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

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

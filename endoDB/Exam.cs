using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.Data;
using Npgsql;

namespace endoDB
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
            readExamData(exam_id);
        }

        private void readExamData(int _exam_id)
        {
            #region Npgsql connection
            NpgsqlConnection conn;
            try
            {
                conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                    Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;");
            }
            catch (ArgumentException)
            {
                MessageBox.Show(Properties.Resources.WrongConnectingString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                conn.Open();
            }
            catch (NpgsqlException)
            {
                MessageBox.Show(Properties.Resources.CouldntOpenConn, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(Properties.Resources.ConnClosed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return;
            }
            #endregion

            //ここから下がデータの読み込み部分。
            string sql = "SELECT exam_id, exam.pt_id AS ptID, pt_name, purpose, department, order_dr, ward_id, exam_day, exam_type,"
                + " pathology, patho_no, reply_patho,"
                + " operator1, operator2, operator3, operator4, operator5, diag_dr, final_diag_dr,"
                + " equipment, place_no, findings, comment, exam_status, explanation"
                + " FROM exam"
                + " INNER JOIN patient ON exam.pt_id = patient.pt_id"
                + " WHERE exam_id = " + _exam_id.ToString();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                conn.Close();
                MessageBox.Show(Properties.Resources.NoData, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                conn.Close();
            }
        }

        public void saveFindingsEtc()
        {
            string sql = "UPDATE exam SET purpose=:p0, department=:p1, order_dr=:p2, ward_id=:p3, "
                + "pathology=:p4, patho_no=:p5, reply_patho=:p6, "
                + "operator1=:p7, operator2=:p8, operator3=:p9, operator4=:p10, operator5=:p11, "
                + "diag_dr=:p12, final_diag_dr=:p13, equipment=:p14, place_no=:p15, "
                + "findings=:p16, comment=:p17, exam_status=:p18, explanation=:p19 "
                + "WHERE exam_id=" + exam_id.ToString();
            switch (uckyFunctions.ExeNonQuery(sql, purpose, department.ToString(), order_dr, ward_id,
                pathology.ToString(), patho_no, reply_patho.ToString(),
                operator1, operator2, operator3, operator4, operator5,
                diag_dr, final_diag_dr, equipment.ToString(), place_no.ToString(),
                findings, comment, exam_status.ToString(), explanation.ToString()))
            {
                case uckyFunctions.functionResult.connectionError:
                    MessageBox.Show(Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case uckyFunctions.functionResult.failed:
                    MessageBox.Show(Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case uckyFunctions.functionResult.success:
                    break;
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
            if (Settings.isJP)
            { sql = "SELECT name_jp FROM exam_type WHERE type_no=" + exam_type.ToString(); }
            else
            { sql = "SELECT name_eng FROM exam_type WHERE type_no=" + exam_type.ToString(); }
            return uckyFunctions.getSelectString(sql, Settings.DBSrvIP, Settings.DBSrvPort, Settings.DBconnectID, Settings.DBconnectPw, "endoDB");
        }

        public string getDepartmentName()
        {
            string sql;
            sql = "SELECT name1 FROM department WHERE code=" + department.ToString();
            return uckyFunctions.getSelectString(sql, Settings.DBSrvIP, Settings.DBSrvPort, Settings.DBconnectID, Settings.DBconnectPw, "endoDB");
        }

        public string getWardName()
        {
            DataRow[] drs;
            drs = CLocalDB.localDB.Tables["ward"].Select("ward_no='" + ward_id + "'");
            return drs[0]["ward"].ToString();
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
            return uckyFunctions.getSelectString(sql, Settings.DBSrvIP, Settings.DBSrvPort, Settings.DBconnectID, Settings.DBconnectPw, "endoDB");
        }

        public string getEquipmentName()
        {
            string sql;
            sql = "SELECT name FROM equipment WHERE equipment_no=" + equipment.ToString();
            return uckyFunctions.getSelectString(sql, Settings.DBSrvIP, Settings.DBSrvPort, Settings.DBconnectID, Settings.DBconnectPw, "endoDB");
        }

        public string getDiagDr()
        {
            if (string.IsNullOrWhiteSpace(diag_dr))
            {
                return null;
            }
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
            {
                return null;
            }
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
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;");

            try
            {
                conn.Open();
            }
            catch (NpgsqlException)
            {
                MessageBox.Show(Properties.Resources.CouldntOpenConn, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(Properties.Resources.ConnClosed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }

            string sql;
            if (Settings.isJP)
            {
                sql = "SELECT name_jp AS name, suspect FROM diag INNER JOIN diag_name ON diag.diag_code=diag_name.no WHERE exam_no=" + exam_id + " ORDER BY diag_no";
            }
            else
            {
                sql = "SELECT name_eng AS name, suspect FROM diag INNER JOIN diag_name ON diag.diag_code=diag_name.no WHERE exam_no=" + exam_id + " ORDER BY diag_no";
            }

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();

            if (dt.Rows.Count == 0)
                return null;

            string str = null;
            foreach (DataRow dr in dt.Rows)
            {
                str += dr["name"].ToString();
                if (!string.IsNullOrWhiteSpace(dr["suspect"].ToString()))
                {
                    if ((Boolean)dr["suspect"])
                    {
                        str += Properties.Resources.Suspect;
                    }
                }
                str += Environment.NewLine;
            }
            return str;
        }
    }
}

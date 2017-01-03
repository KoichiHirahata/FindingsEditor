using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.Data;
using System.Net;
using Npgsql;

namespace FindingsEdior
{
    class CLocalDB
    {
        public static DataSet localDB = new DataSet();

        public static void WriteToLocalDB()
        {
            getExamType();      //exam_type table
            getAllExamType();      //exam_type table
            getWard();          //ward table
            getDepartment();    //department table
            getOperators();      //operator table where visible=true
            getAllOp(); //operator table 
            getOpCategory();    //op_category table
            getEquipmentType(); //equipment_type
            getEquipmentGF(); //equipment_type order by gf_order
            getEquipmentCF(); //equipment_type order by cf_order
            getEquipmentSV(); //equipment_type order by sv_order
            getEquipmentS(); //equipment_type order by s_order
            getEquipmentUS(); //equipment_type order by us_order
            getPlaceEndo(); //place order by place_order_endo
            getPlaceUS(); //place order by place_order_us
            getDiagnoses(); //diag_name order by diag_order
            getDiagCategory();//diag_category table
            getExamStatus(); //status table
            getWords(); //words table
        }

        #region ExamType
        private static void getExamType()
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

            try
            { conn.Open(); }
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
            #endregion

            string sql;

            if (Settings.lang == "ja")
            {
                sql = "SELECT type_no, name_jp AS exam_name FROM exam_type WHERE type_visible = true ORDER BY type_order";
            }
            else
            {
                sql = "SELECT type_no, name_eng AS exam_name FROM exam_type WHERE type_visible = true ORDER BY type_order";
            }

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(localDB, "exam_type");
            if (localDB.Tables["exam_type"].Rows.Count == 0)
                MessageBox.Show(Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            conn.Close();
        }

        private static void getAllExamType()
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

            try
            { conn.Open(); }
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
            #endregion

            string sql;

            if (Settings.lang == "ja")
            {
                sql = "SELECT type_no, name_jp AS exam_name FROM exam_type ORDER BY type_order";
            }
            else
            {
                sql = "SELECT type_no, name_eng AS exam_name FROM exam_type ORDER BY type_order";
            }

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(localDB, "all_exam_type");
            if (localDB.Tables["all_exam_type"].Rows.Count == 0)
                MessageBox.Show(Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            conn.Close();
        }
        #endregion

        private static void getWard()
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

            try
            { conn.Open(); }
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
            #endregion

            string sql = "SELECT ward_no, ward FROM ward WHERE ward_visible = true ORDER BY ward_order";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(localDB, "ward");
            if (localDB.Tables["ward"].Rows.Count == 0)
            { MessageBox.Show("[ward]" + Properties.Resources.NoRecord, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            conn.Close();
        }

        private static void getDepartment()
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

            try
            { conn.Open(); }
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
            #endregion

            string sql = "SELECT code, name1 FROM department WHERE dp_visible = true ORDER BY dp_order";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(localDB, "department");
            if (localDB.Tables["department"].Rows.Count == 0)
                MessageBox.Show("[department]" + Properties.Resources.NoRecord, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            conn.Close();
        }

        #region operators
        private static void getOperators()
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

            try
            { conn.Open(); }
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
            #endregion

            string sql = "SELECT operator_id, op_name FROM operator WHERE op_visible = true ORDER BY op_order";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(localDB, "orderDr");
            if (localDB.Tables["orderDr"].Rows.Count == 0)
            { MessageBox.Show("[orderDr]" + Properties.Resources.NoRecord, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                DataTable operator1;
                DataTable operator2;
                DataTable operator3;
                DataTable operator4;
                DataTable operator5;
                DataTable DiagDr;
                DataTable Checker;
                operator1 = localDB.Tables["orderDr"].Copy();
                operator2 = localDB.Tables["orderDr"].Copy();
                operator3 = localDB.Tables["orderDr"].Copy();
                operator4 = localDB.Tables["orderDr"].Copy();
                operator5 = localDB.Tables["orderDr"].Copy();
                DiagDr = localDB.Tables["orderDr"].Copy();
                Checker = localDB.Tables["orderDr"].Copy();
                operator1.TableName = "operator1";
                operator2.TableName = "operator2";
                operator3.TableName = "operator3";
                operator4.TableName = "operator4";
                operator5.TableName = "operator5";
                DiagDr.TableName = "DiagDr";
                Checker.TableName = "Checker";
                localDB.Tables.Add(operator1);
                localDB.Tables.Add(operator2);
                localDB.Tables.Add(operator3);
                localDB.Tables.Add(operator4);
                localDB.Tables.Add(operator5);
                localDB.Tables.Add(DiagDr);
                localDB.Tables.Add(Checker);
            }
            conn.Close();
        }

        private static void getAllOp()
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

            try
            { conn.Open(); }
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
            #endregion

            string sql = "SELECT operator_id, op_name FROM operator ORDER BY op_order";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(localDB, "allOp");
            if (localDB.Tables["allOp"].Rows.Count == 0)
            { MessageBox.Show("[allOp]" + Properties.Resources.NoRecord, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            conn.Close();
        }

        private static void getOpCategory()
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

            try
            { conn.Open(); }
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
            #endregion

            string sql = "SELECT opc_no, opc_name FROM op_category WHERE opc_visible = true ORDER BY opc_order";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(localDB, "op_category");
            if (localDB.Tables["op_category"].Rows.Count == 0)
            { MessageBox.Show("[op_category]" + Properties.Resources.NoRecord, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            conn.Close();
        }
        #endregion

        #region Equipment
        private static void getEquipmentType()
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

            try
            { conn.Open(); }
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
            #endregion

            string sql = "SELECT type_no, name FROM equipment_type WHERE type_visible = true ORDER BY type_order";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(localDB, "equipment_type");
            if (localDB.Tables["equipment_type"].Rows.Count == 0)
            { MessageBox.Show("[equipment_type]" + Properties.Resources.NoRecord, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            conn.Close();
        }

        private static void getEquipmentGF()
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

            try
            { conn.Open(); }
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
            #endregion

            string sql = "SELECT equipment_no, name FROM equipment WHERE equipment_visible = true AND scope = true ORDER BY gf_order";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(localDB, "equipmentGF");
            conn.Close();
        }

        private static void getEquipmentCF()
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

            try
            { conn.Open(); }
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
            #endregion

            string sql = "SELECT equipment_no, name FROM equipment WHERE equipment_visible = true AND scope = true ORDER BY cf_order";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(localDB, "equipmentCF");
            conn.Close();
        }

        private static void getEquipmentSV()
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

            try
            { conn.Open(); }
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
            #endregion

            string sql = "SELECT equipment_no, name FROM equipment WHERE equipment_visible = true AND scope = true ORDER BY sv_order";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(localDB, "equipmentSV");
            conn.Close();
        }

        private static void getEquipmentS()
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

            try
            { conn.Open(); }
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
            #endregion

            string sql = "SELECT equipment_no, name FROM equipment WHERE equipment_visible = true AND scope = true ORDER BY s_order";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(localDB, "equipmentS");
            conn.Close();
        }

        private static void getEquipmentUS()
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

            try
            { conn.Open(); }
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
            #endregion

            string sql = "SELECT equipment_no, name FROM equipment WHERE equipment_visible = true AND us = true ORDER BY us_order";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(localDB, "equipmentUS");
            conn.Close();
        }
        #endregion

        #region Place
        private static void getPlaceEndo()
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

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
            #endregion

            string sql = "SELECT place_no, name1 FROM place WHERE place_visible = true ORDER BY place_order_endo";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(localDB, "placeEndo");
            conn.Close();
        }

        private static void getPlaceUS()
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

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
            #endregion

            string sql = "SELECT place_no, name1 FROM place WHERE place_visible = true ORDER BY place_order_us";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(localDB, "placeUS");
            conn.Close();
        }
        #endregion

        #region Diag
        private static void getDiagnoses()
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

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
            #endregion

            string sql;
            if (Settings.lang=="ja")
            { sql = "SELECT no, name_jp as name, diag_visible FROM diag_name ORDER BY diag_order"; }
            else
            { sql = "SELECT no, name_eng as name, diag_visible FROM diag_name ORDER BY diag_order"; }

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(localDB, "diag_name");
            if (localDB.Tables["diag_name"].Rows.Count == 0)
            { MessageBox.Show("[localDB diag_name]" + Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            conn.Close();
        }

        private static void getDiagCategory()
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

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
            #endregion

            string sql;
            if (Settings.lang == "ja")
            { sql = "SELECT id, start_no, end_no, name_jp AS name, bt_order, visible FROM diag_category"; }
            else
            { sql = "SELECT id, start_no, end_no, name_eng AS name, bt_order, visible FROM diag_category"; }

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(localDB, "diag_category");
            if (localDB.Tables["diag_category"].Rows.Count == 0)
            { MessageBox.Show("[localDB diag_category]" + Properties.Resources.NoRecord, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            conn.Close();
        }
        #endregion

        private static void getExamStatus()
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

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
            #endregion

            string sql;
            if (Settings.lang == "ja")
            { sql = "SELECT status_no,name_jp AS status_name FROM status WHERE status_visible=true ORDER BY status_order"; }
            else
            { sql = "SELECT status_no,name_eng AS status_name FROM status WHERE status_visible=true ORDER BY status_order"; }

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(localDB, "exam_status");
            if (localDB.Tables["exam_status"].Rows.Count == 0)
            { MessageBox.Show("[exam_status]" + Properties.Resources.NoRecord, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            conn.Close();
        }

        #region words
        public static void getWords()
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);

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
            #endregion

            string sql = "SELECT no, words1, words2, words3, operator, word_order FROM words ORDER BY word_order";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(localDB, "words");
            if (localDB.Tables["words"].Rows.Count == 0)
            { MessageBox.Show("[words]" + Properties.Resources.NoRecord, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            conn.Close();
        }

        public static void delWords()
        { CLocalDB.localDB.Tables["words"].Clear(); }
        #endregion
    }
}

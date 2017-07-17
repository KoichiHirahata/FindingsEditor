using System;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Data;
using System.Net;
using Npgsql;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace FindingsEdior
{

    public class IgnoreAssemblyBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            return Type.GetType(typeName);
        }
    }

    public class uckyFunctions
    {
        #region 便利関数
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

        /// Run transaction
        public static functionResult ExeNonQuery(string sql, params string[] p_str)
        {
            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                #region Npgsql connection
                NpgsqlConnection conn = new NpgsqlConnection();

                conn.ConnectionString = Settings.retConnStr();

                // トランザクションを開始します。
                try
                {
                    conn.Open();
                }
                catch (NpgsqlException)
                {
                    conn.Close();
                    return functionResult.connectionError;
                }
                catch (System.IO.IOException)
                {
                    conn.Close();
                    return functionResult.connectionError;
                }
                #endregion

                NpgsqlTransaction transaction = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                try
                {
                    command.CommandText = sql;
                    command.Connection = conn;
                    //MessageBox.Show(sql);

                    string p;
                    for (int i = 0; i < p_str.Length; i++)
                    {
                        p = ":p" + i.ToString();
                        //MessageBox.Show(p + " to " + p_str[i]);
                        command.Parameters.Add(new NpgsqlParameter(p, p_str[i]));
                    }

                    command.Transaction = transaction;
                    command.ExecuteNonQuery();

                    //Commit transaction
                    transaction.Commit();
                }
                catch (System.Exception) // ex)
                {
                    //Roll back transaction
                    transaction.Rollback();
                    conn.Close();
                    //throw ex;
                    return functionResult.failed;
                }
                conn.Close();
            }
            return functionResult.success;
        }

        /// This function runs SQL without transaction log for test.
        public static functionResult RunSQLWithoutTransactionLog(string sql, params string[] p_str)
        {
            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                #region Npgsql connection
                NpgsqlConnection conn = new NpgsqlConnection();

                conn.ConnectionString = Settings.retConnStr();

                // トランザクションを開始します。
                try
                {
                    conn.Open();
                }
                catch (NpgsqlException)
                {
                    conn.Close();
                    return functionResult.connectionError;
                }
                catch (System.IO.IOException)
                {
                    conn.Close();
                    return functionResult.connectionError;
                }
                #endregion

                NpgsqlTransaction transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);

                command.CommandText = sql;
                command.Connection = conn;

                string p;
                for (int i = 0; i < p_str.Length; i++)
                {
                    p = ":p" + i.ToString();
                    //MessageBox.Show(p + " to " + p_str[i]);
                    command.Parameters.Add(new NpgsqlParameter(p, p_str[i]));
                }
                command.ExecuteNonQuery();
                conn.Close();
            }
            return functionResult.success;
        }

        /// <summary>
        ///This function will return string, if you specify SQL that returns unique string. If there is no data, return null.
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="DBserver">DB server IP address</param>
        /// <param name="DBport">DB server port number</param>
        /// <param name="DBuser">DB user ID</param>
        /// <param name="DBpw">DB user password</param>
        /// <param name="DBname">DB name</param>
        /// <returns>string</returns>
        public static string getSelectString(string sql, string DBserver, string DBport, string DBuser, string DBpw, string DBname)
        {
            string resultStr;
            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                #region Npgsql
                NpgsqlConnection conn = new NpgsqlConnection();
                conn.ConnectionString = @"Server=" + DBserver + ";Port=" + DBport + ";User Id=" + DBuser + ";Password=" + DBpw + ";Database=" + DBname + ";" + Settings.sslSetting;

                try
                { conn.Open(); }
                catch (NpgsqlException)
                {
                    conn.Close();
                    return null;
                }
                catch (System.IO.IOException)
                {
                    conn.Close();
                    return null;
                }
                #endregion

                try
                {
                    command.CommandText = sql;
                    command.Connection = conn;
                    resultStr = (string)command.ExecuteScalar();
                }
                catch (System.Exception) // ex)
                {
                    conn.Close();
                    //throw ex;
                    return null;
                }
                conn.Close();
            }
            return resultStr;
        }

        //中身がintなカラムについて、SELECT文で一意な結果を取得するSQLを放り込むとintで結果を返してくれる関数.If there was no data, or error occured, return -1.
        public static int getSelectInt(string sql, string DBserver, string DBport, string DBuser, string DBpw, string DBname)
        {
            int resultInt;
            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                #region Npgsql
                NpgsqlConnection conn = new NpgsqlConnection();
                conn.ConnectionString = @"Server=" + DBserver + ";Port=" + DBport + ";User Id=" + DBuser + ";Password=" + DBpw + ";Database=" + DBname + ";" + Settings.sslSetting;

                try
                { conn.Open(); }
                catch (NpgsqlException)
                {
                    conn.Close();
                    MessageBox.Show("[NpgsqlException]Error was occured.");
                    return -1;
                }
                catch (System.IO.IOException)
                {
                    conn.Close();
                    MessageBox.Show("[IOException]Error was occured.");
                    return -1;
                }
                #endregion

                try
                {
                    command.CommandText = @sql;
                    command.Connection = conn;
                    resultInt = (int)command.ExecuteScalar();
                }
                catch (System.Exception) // ex)
                {
                    conn.Close();
                    //throw ex;
                    MessageBox.Show("[Exception]Error was occured.");
                    return -1;
                }
                conn.Close();
            }
            return resultInt;
        }

        //stringを受け取って、intに変換できたら1足してstringにして戻す関数
        public static string maxPlus1(string maxStr)
        {
            int num;
            if (int.TryParse(maxStr, out num))
            {
                return (num + 1).ToString();
            }
            else
            {
                return maxStr + 1;
            }
        }

        /// <summary>画像が完全に一致するかどうかを比較する関数</summary>
        public static Boolean ImageComp(Image image1, Image image2)
        {
            Bitmap img1 = (Bitmap)image1;
            Bitmap img2 = (Bitmap)image2;

            //高さや幅が違えばfalse  
            if (img1.Width != img2.Width || img1.Height != img2.Height) return false;
            //LockBitsでBitmapDataを取得  
            BitmapData bd1 = img1.LockBits(new Rectangle(0, 0, img1.Width, img1.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, img1.PixelFormat);
            BitmapData bd2 = img2.LockBits(new Rectangle(0, 0, img2.Width, img2.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, img2.PixelFormat);
            if ((bd1.Stride * img1.Height) != (bd2.Stride * img2.Height)) return false;
            int bsize = bd1.Stride * img1.Height;
            byte[] byte1 = new byte[bsize];
            byte[] byte2 = new byte[bsize];
            //バイト配列にコピー  
            Marshal.Copy(bd1.Scan0, byte1, 0, bsize);
            Marshal.Copy(bd2.Scan0, byte2, 0, bsize);
            //ロックを解除  
            img1.UnlockBits(bd1);
            img2.UnlockBits(bd2);

            //MD5ハッシュを取る  
            System.Security.Cryptography.MD5CryptoServiceProvider md5 =
                new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hash1 = md5.ComputeHash(byte1);
            byte[] hash2 = md5.ComputeHash(byte2);
            //比較  
            return hash1.SequenceEqual(hash2);
        }
        #endregion

        #region Low versatility functions
        public static Boolean canEdit(string tableName, string columnName, string symbol, string keyString)
        {
            #region Npgsql
            NpgsqlConnection conn;

            try
            {
                conn = new NpgsqlConnection(Settings.retConnStr());

                conn.Open();
            }
            catch (ArgumentException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.WrongConnectingString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            #endregion

            string sql =
                "SELECT lock_time, terminal_ip FROM " + tableName + " WHERE " + columnName + " " + symbol + " " + keyString;

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            try
            { da.Fill(dt); }
            catch (NpgsqlException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return false;
            }

            conn.Close();

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.NoData, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if ((string.IsNullOrWhiteSpace(dt.Rows[0]["lock_time"].ToString())) || (string.IsNullOrWhiteSpace(dt.Rows[0]["terminal_ip"].ToString())))
                return true;

            //check lock time
            DateTime now = DateTime.Now;
            DateTime l_time = DateTime.Parse(dt.Rows[0]["lock_time"].ToString()).AddMinutes(2);
            if (l_time < now)
            { return true; }
            else
            {
                //check IP address.
                string hostname = Dns.GetHostName();
                IPAddress[] ip = Dns.GetHostAddresses(hostname);

                if (dt.Rows[0]["terminal_ip"].ToString() == ip[0].ToString())
                { return true; }
                else
                { return false; }
            }
        }

        public static void updateLockTimeIP(string tableName, string columnName, string symbol, string keyString)
        {
            string hostname = Dns.GetHostName();
            IPAddress[] ip = Dns.GetHostAddresses(hostname);
            string SQL = "UPDATE " + tableName + " SET lock_time=current_timestamp, terminal_ip='" + ip[0].ToString() +
                "' WHERE " + columnName + " " + symbol + " " + keyString;
            switch (uckyFunctions.ExeNonQuery(SQL))
            {
                case uckyFunctions.functionResult.success:
                    return;
                case uckyFunctions.functionResult.failed:
                    MessageBox.Show(FindingsEditor.Properties.Resources.LockFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                case uckyFunctions.functionResult.connectionError:
                    MessageBox.Show(FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }
        }

        public static void delLockTimeIP(string tableName, string columnName, string symbol, string keyString)
        {
            string SQL = "UPDATE " + tableName + " SET lock_time=NULL, terminal_ip=NULL" +
                " WHERE " + columnName + " " + symbol + " " + keyString;
            //MessageBox.Show(SQL);
            switch (uckyFunctions.ExeNonQuery(SQL))
            {
                case uckyFunctions.functionResult.success:
                    return;
                case uckyFunctions.functionResult.failed:
                    MessageBox.Show(FindingsEditor.Properties.Resources.LockFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                case uckyFunctions.functionResult.connectionError:
                    MessageBox.Show(FindingsEditor.Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }
        }

        //Research how many times object used
        public static int CountTimes(string table, string column, string searchStr, string type)
        {
            string sql = null;
            switch (type)
            {
                case "int":
                    sql = "SELECT " + column + " FROM " + table + " WHERE " + column + "=" + searchStr;
                    break;
                case "string":
                    sql = "SELECT " + column + " FROM " + table + " WHERE " + column + " = '" + searchStr + "';";
                    break;
                default:
                    MessageBox.Show("Error was occured at functon[CountTimes]");
                    break;
            }
            if (sql == null)
                return -1;

            #region Npgsql
            NpgsqlConnection conn;

            try
            {
                conn = new NpgsqlConnection(Settings.retConnStr());
            }
            catch (ArgumentException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.WrongConnectingString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            try
            { conn.Open(); }
            catch (NpgsqlException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.CouldntOpenConn, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return -1;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.ConnClosed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return -1;
            }
            #endregion

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();

            return int.Parse(dt.Rows.Count.ToString());
        }

        //Start PtGraViewer with options of patient's ID, date of examination
        public static void showImages(string pt_id_str, string exam_date)
        {
            //MessageBox.Show("/pt:" + pt_id_str + " /date:" + exam_date);
            if (System.IO.File.Exists(Application.StartupPath + @"\PtGraViewer\PtGraViewer.exe"))
            { System.Diagnostics.Process.Start(Application.StartupPath + @"\PtGraViewer\PtGraViewer.exe", "/pt:" + pt_id_str + " /date:" + exam_date); }
        }
        #endregion
    }

    #region db_operator //this class means user.
    public class db_operator
    {
        public static string operatorID { get; set; }
        public static string operatorName { get; set; }
        public static Boolean admin_user { get; set; }
        public static Boolean authDone { get; set; }
        public static Boolean canDiag { get; set; }
        public static Boolean allowFC { get; set; }

        db_operator()
        {
            reset_op();
        }

        public static void reset_op()
        {
            db_operator.operatorID = "";
            db_operator.operatorName = "";
            db_operator.admin_user = false;
            db_operator.authDone = false;
            db_operator.canDiag = false;
            db_operator.allowFC = false;
        }

        public enum idPwCheckResult { success, failed, connectionError };

        public static idPwCheckResult idPwCheck(string opId, string opPw)
        {
            #region Npgsql
            NpgsqlConnection conn;
            conn = new NpgsqlConnection(Settings.retConnStr());

            try
            {
                conn.Open();
            }
            catch (NpgsqlException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.CouldntOpenConn, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return idPwCheckResult.connectionError;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.ConnClosed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return idPwCheckResult.connectionError;
            }
            #endregion

            string sql = "SELECT operator_id, op_name, pw, admin_op, allow_fc, can_diag FROM operator"
                + " INNER JOIN op_category ON operator.op_category=op_category.opc_no WHERE operator_id='" + opId + "'";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                conn.Close();
                MessageBox.Show(FindingsEditor.Properties.Resources.WrongIDorPW, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return idPwCheckResult.failed;
            }
            else
            {
                DataRow row = dt.Rows[0];
                if (opPw == row["pw"].ToString())
                {
                    db_operator.operatorID = opId;
                    db_operator.operatorName = row["op_name"].ToString();
                    db_operator.admin_user = (Boolean)row["admin_op"];
                    db_operator.authDone = true;
                    db_operator.canDiag = (Boolean)row["can_diag"];
                    db_operator.allowFC = (Boolean)row["allow_fc"];
                    conn.Close();
                    return idPwCheckResult.success;
                }
                else
                {
                    conn.Close();
                    MessageBox.Show(FindingsEditor.Properties.Resources.WrongIDorPW, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return idPwCheckResult.failed;
                }
            }
        }

    }
    #endregion

    #region patient
    public class patient
    {
        public string ptID;
        public string ptName;
        public enum gender { male, female }
        public gender ptGender;
        public DateTime ptBirthday;
        public string ptInfo;
        public Boolean ptExist;
        public Boolean newPt;

        public patient(string patientID, Boolean NewPatient)
        {
            this.ptID = patientID;
            this.ptExist = false;
            newPt = NewPatient;
            if (newPt == false)
            { readPtData(patientID); }
        }

        public void readPtData(string patientID)
        {
            #region Npgsql
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

            string sql = "SELECT * FROM patient WHERE pt_id='" + patientID + "'";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                conn.Close();
                MessageBox.Show(FindingsEditor.Properties.Resources.NoPatient, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DataRow row = dt.Rows[0];
                this.ptID = row["pt_id"].ToString();
                this.ptName = row["pt_name"].ToString();
                if ((short)row["gender"] == 0)
                {
                    this.ptGender = gender.female;
                }
                else
                {
                    this.ptGender = gender.male;
                }
                if (row["birthday"] != null)
                    this.ptBirthday = (DateTime)row["birthday"];
                this.ptInfo = row["pt_memo"].ToString();
                this.ptExist = true;

                conn.Close();
            }
        }

        public void writePtInfo(patient pt_source)
        {
            string SQL = "UPDATE patient SET pt_memo=:p0 WHERE pt_id=:p1;";
            if (uckyFunctions.ExeNonQuery(SQL, pt_source.ptInfo, pt_source.ptID) == uckyFunctions.functionResult.success)
            { MessageBox.Show(FindingsEditor.Properties.Resources.UpdateDone, "Successfully updated", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else
            { MessageBox.Show(FindingsEditor.Properties.Resources.UpdateFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public uckyFunctions.functionResult writePtAllData(patient pt_source)
        {
            string SQL;
            short gender;
            if (pt_source.ptGender == patient.gender.female)
            { gender = 0; }
            else
            { gender = 1; }

            if (this.newPt == true)
            {
                SQL = "INSERT INTO patient(pt_ID,pt_name,gender,birthday)"
                    + "VALUES(:p0, :p1, :p2, :p3);";

                return uckyFunctions.ExeNonQuery(SQL, pt_source.ptID, pt_source.ptName, gender.ToString(), pt_source.ptBirthday.ToString());
            }
            else
            {
                SQL = "UPDATE patient SET pt_name=:p0, gender=:p1, birthday=:p2, "
                     + "pt_memo=:p3 WHERE pt_id=:p4;";

                return uckyFunctions.ExeNonQuery(SQL, pt_source.ptName, gender.ToString(), pt_source.ptBirthday.ToString(),
                    pt_source.ptInfo, pt_source.ptID);
            }
        }

        public int getPtAge()
        { return uckyFunctions.GetAge(this.ptBirthday, DateTime.Today); }

        public static int numberOfPatients(string patientID)
        {
            #region Npgsql
            NpgsqlConnection conn;
            try
            {
                conn = new NpgsqlConnection(Settings.retConnStr());
            }
            catch (ArgumentException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.WrongConnectingString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            try
            { conn.Open(); }
            catch (NpgsqlException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.CouldntOpenConn, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return -1;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.ConnClosed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return -1;
            }
            #endregion

            string sql = "SELECT * FROM patient WHERE pt_id='" + patientID + "' FOR UPDATE";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt.Rows.Count;
        }

        public enum idDuplicateResult { NotExist, None, Duplicated, Error };

        public static idDuplicateResult checkIdDuplicate(string patientID)
        {
            switch (numberOfPatients(patientID))
            {
                case -1:
                    MessageBox.Show(FindingsEditor.Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return idDuplicateResult.Error;
                case 0:
                    //MessageBox.Show(FindingsEditor.Properties.Resources.NoPatient, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return idDuplicateResult.NotExist;
                case 1:
                    return idDuplicateResult.None;
                default:
                    return idDuplicateResult.Duplicated;
            }
        }

        public int getNumOfExams() //Return -1 on error.
        {
            #region Npgsql
            NpgsqlConnection conn;
            try
            {
                conn = new NpgsqlConnection(Settings.retConnStr());
            }
            catch (ArgumentException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.WrongConnectingString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            try
            {
                conn.Open();
            }
            catch (NpgsqlException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.CouldntOpenConn, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return -1;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.ConnClosed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return -1;
            }
            #endregion

            string sql = "SELECT exam_id FROM exam WHERE pt_id='" + ptID + "' FOR UPDATE";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt.Rows.Count;
        }
    }
    #endregion

    #region examOperator
    public sealed class examOperator
    {
        public string operator_id { get; set; }
        public string op_name { get; set; }
        public short op_order { get; set; }
        public short department { get; set; }
        public string pw { get; set; }
        public Boolean admin_op { get; set; }
        public short op_category { get; set; }
        public Boolean op_visible { get; set; }
        public Boolean allow_fc { get; set; }

        private Boolean newOperator { get; set; }

        public examOperator(string operator_id)
        {
            readOperatorData(operator_id);
            this.newOperator = false;
        }

        public examOperator(string operator_id, Boolean _newOperator)
        {
            this.newOperator = _newOperator;
            if (!_newOperator)
                readOperatorData(operator_id);
        }

        public void readOperatorData(string operator_id)
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
            {
                conn.Open();
            }
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
            string sql = "SELECT operator_id, op_name, op_order, department, pw, admin_op, op_category, op_visible, allow_fc "
                + "FROM operator WHERE operator_id='" + operator_id + "'";

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
                this.operator_id = row["operator_id"].ToString();
                this.op_name = row["op_name"].ToString();
                if (!row.IsNull("op_order"))
                    this.op_order = (short)row["op_order"];
                if (!row.IsNull("department"))
                    this.department = (short)row["department"];
                if (!row.IsNull("pw"))
                    this.pw = row["pw"].ToString();
                if (!row.IsNull("admin_op"))
                    this.admin_op = (Boolean)row["admin_op"];
                if (!row.IsNull("op_category"))
                    this.op_category = (short)row["op_category"];
                if (!row.IsNull("op_visible"))
                    this.op_visible = (Boolean)row["op_visible"];
                if (!row.IsNull("allow_fc"))
                    this.allow_fc = (Boolean)row["allow_fc"];

                conn.Close();
            }
        }

        public uckyFunctions.functionResult saveOperatorData(string operator_id, string op_name, short department, string pw,
            Boolean admin_op, short op_category, Boolean op_visible, Boolean allow_fc, short op_order = 9999)
        {
            string sql;
            uckyFunctions.functionResult thisFuncResult;

            if (pw == null)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.NoPw, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return uckyFunctions.functionResult.failed;
            }

            if (this.newOperator)
            {
                sql = "INSERT INTO operator (operator_id, op_name, department, pw, admin_op, op_category, "
                    + "op_visible, allow_fc, op_order)"
                    + " VALUES(:p0, :p1, :p2, :p3, :p4, "
                    + ":p5, :p6, :p7, :p8);";

                thisFuncResult = uckyFunctions.ExeNonQuery(sql, operator_id, op_name, department.ToString(), pw, admin_op.ToString(),
                    op_category.ToString(), op_visible.ToString(), allow_fc.ToString(), op_order.ToString());
            }
            else
            {
                sql = "UPDATE operator SET op_name=:p0, department=:p1, pw=:p2, "
                    + "admin_op=:p3, op_category=:p4, op_visible=:p5, allow_fc=:p6, "
                    + "op_order=:p7 "
                    + "WHERE operator_id=:p8;";

                thisFuncResult = uckyFunctions.ExeNonQuery(sql, op_name, department.ToString(), pw,
                    admin_op.ToString(), op_category.ToString(), op_visible.ToString(), allow_fc.ToString(),
                    op_order.ToString(), operator_id);
            }

            return thisFuncResult;
        }

        public static uckyFunctions.functionResult saveOrder(string operator_id, short op_order)
        {
            string sql;
            uckyFunctions.functionResult thisFuncResult;

            sql = "UPDATE operator SET op_order=" + op_order + " WHERE operator_id='" + operator_id + "'";

            thisFuncResult = uckyFunctions.ExeNonQuery(sql);
            return thisFuncResult;
        }

        //This function return number of exams that the operator had underwent.
        //If any errors occur, this function return -1.
        public int examCount()
        {
            #region Npgsql
            NpgsqlConnection conn;
            try
            {
                conn = new NpgsqlConnection(Settings.retConnStr());
            }
            catch (ArgumentException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.WrongConnectingString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            try
            {
                conn.Open();
            }
            catch (NpgsqlException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.CouldntOpenConn, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return -1;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.ConnClosed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return -1;
            }
            #endregion

            string sql = "SELECT COUNT(*) FROM exam WHERE operator1='" + operator_id + "' OR operator2='" + operator_id
                + "' OR operator3='" + operator_id + "' OR operator4='" + operator_id
                + "' OR operator5='" + operator_id + "'";

            NpgsqlCommand command = new NpgsqlCommand(sql, conn);
            string a = command.ExecuteScalar().ToString();
            conn.Close();
            return int.Parse(a);
        }

        public static int numberOfOperator(string opID)
        {
            #region Npgsql
            NpgsqlConnection conn;
            try
            {
                conn = new NpgsqlConnection(Settings.retConnStr());
            }
            catch (ArgumentException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.WrongConnectingString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            try
            {
                conn.Open();
            }
            catch (NpgsqlException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.CouldntOpenConn, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return -1;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(FindingsEditor.Properties.Resources.ConnClosed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return -1;
            }
            #endregion

            string sql = "SELECT * FROM operator WHERE operator_id='" + opID + "'";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt.Rows.Count;
        }

        public void deleteExamOp()
        {
            string sql = "DELETE FROM operator WHERE operator_id='" + operator_id + "'";
            uckyFunctions.ExeNonQuery(sql);
        }
    }
    #endregion
}

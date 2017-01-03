using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Npgsql;

namespace FindingsEdior
{
    public partial class EditDiagnoses : Form
    {
        private DataTable dt = new DataTable();
        private int start_no;
        private int end_no;

        public EditDiagnoses()
        {
            InitializeComponent();

            #region TreeView
            DataRow[] drs;
            int i;

            #region laryngopharynx
            i = 1001;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId Laryngopharynx = new NodeWithId(drs[0]["name"].ToString(), i);
            tv.Nodes.Add(Laryngopharynx);
            #endregion

            #region esophagus
            i = 2001;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId eso_inf = new NodeWithId(drs[0]["name"].ToString(), i);

            i = 3000;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId eso_tum = new NodeWithId(drs[0]["name"].ToString(), i);

            i = 4000;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId eso_etc = new NodeWithId(drs[0]["name"].ToString(), i);

            i = 2000;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId esophagus = new NodeWithId(drs[0]["name"].ToString(), i);
            esophagus.Nodes.Add(eso_inf);
            esophagus.Nodes.Add(eso_tum);
            esophagus.Nodes.Add(eso_etc);
            tv.Nodes.Add(esophagus);
            #endregion

            #region Stomach
            i = 5001;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId stomach_inf = new NodeWithId(drs[0]["name"].ToString(), i);

            i = 6000;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId stomach_tum = new NodeWithId(drs[0]["name"].ToString(), i);

            i = 7000;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId stomach_etc = new NodeWithId(drs[0]["name"].ToString(), i);

            i = 5000;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId stomach = new NodeWithId(drs[0]["name"].ToString(), i);
            stomach.Nodes.Add(stomach_inf);
            stomach.Nodes.Add(stomach_tum);
            stomach.Nodes.Add(stomach_etc);
            tv.Nodes.Add(stomach);
            #endregion

            #region Duodenum
            i = 10001;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId Duodenum = new NodeWithId(drs[0]["name"].ToString(), i);
            tv.Nodes.Add(Duodenum);
            #endregion

            #region SideView
            i = 20000;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId SideView = new NodeWithId(drs[0]["name"].ToString(), i);
            tv.Nodes.Add(SideView);
            #endregion

            #region SmallBowel
            i = 30001;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId SmallBowel = new NodeWithId(drs[0]["name"].ToString(), i);
            tv.Nodes.Add(SmallBowel);
            #endregion

            #region Colon
            i = 40001;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId Colon_inf = new NodeWithId(drs[0]["name"].ToString(), i);

            i = 50000;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId Colon_tum = new NodeWithId(drs[0]["name"].ToString(), i);

            i = 60000;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId Colon_etc = new NodeWithId(drs[0]["name"].ToString(), i);

            i = 40000;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId Colon = new NodeWithId(drs[0]["name"].ToString(), i);
            Colon.Nodes.Add(Colon_inf);
            Colon.Nodes.Add(Colon_tum);
            Colon.Nodes.Add(Colon_etc);
            tv.Nodes.Add(Colon);
            #endregion

            #region Anus
            i = 30001;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId Anus = new NodeWithId(drs[0]["name"].ToString(), i);
            tv.Nodes.Add(Anus);
            #endregion

            #region Procedure
            i = 100001;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId Procedure = new NodeWithId(drs[0]["name"].ToString(), i);
            tv.Nodes.Add(Procedure);
            #endregion

            #region Study
            i = 200001;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId Study = new NodeWithId(drs[0]["name"].ToString(), i);
            tv.Nodes.Add(Study);
            #endregion

            #region Broncho
            i = 300001;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId Broncho = new NodeWithId(drs[0]["name"].ToString(), i);
            tv.Nodes.Add(Broncho);
            #endregion

            #region Abdomen
            #region Gallbladder
            i = 1010000;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId Gallbladder = new NodeWithId(drs[0]["name"].ToString(), i);
            #endregion

            #region ExtrahepaticBileDuct
            i = 1020000;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId ExtrahepaticBileDuct = new NodeWithId(drs[0]["name"].ToString(), i);
            #endregion

            #region Liver
            i = 1030000;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId Liver = new NodeWithId(drs[0]["name"].ToString(), i);
            #endregion

            #region Spleen
            i = 1040000;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId Spleen = new NodeWithId(drs[0]["name"].ToString(), i);
            #endregion

            #region Pancreas
            i = 1050000;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId Pancreas = new NodeWithId(drs[0]["name"].ToString(), i);
            #endregion

            #region Kidney
            i = 1060000;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId Kidney = new NodeWithId(drs[0]["name"].ToString(), i);
            #endregion

            i = 1000000;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id=" + i.ToString());
            NodeWithId Abdmen = new NodeWithId(drs[0]["name"].ToString(), i);
            Abdmen.Nodes.Add(Gallbladder);
            Abdmen.Nodes.Add(ExtrahepaticBileDuct);
            Abdmen.Nodes.Add(Liver);
            Abdmen.Nodes.Add(Spleen);
            Abdmen.Nodes.Add(Pancreas);
            Abdmen.Nodes.Add(Kidney);
            tv.Nodes.Add(Abdmen);
            #endregion

            #endregion

            #region dgv
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.DataSource = dt;
            showList();

            #region Add btDelete
            DataGridViewButtonColumn btDelete = new DataGridViewButtonColumn(); //DataGridViewButtonColumnの作成
            btDelete.Name = "btDelete";  //列の名前を設定
            btDelete.UseColumnTextForButtonValue = true;    //ボタンにテキスト表示
            btDelete.Text = Properties.Resources.Delete;  //ボタンの表示テキスト設定
            dgv.Columns.Add(btDelete);           //ボタン追加
            #endregion

            #region Header text
            dgv.Columns["no"].HeaderText = "No";
            dgv.Columns["name_eng"].HeaderText = Properties.Resources.English;
            dgv.Columns["name_jp"].HeaderText = Properties.Resources.Japanese;
            dgv.Columns["diag_order"].HeaderText = Properties.Resources.Order;
            dgv.Columns["diag_visible"].HeaderText = Properties.Resources.Visible;
            dgv.Columns["btDelete"].HeaderText = Properties.Resources.Delete;
            dgv.Columns["tbUpdate"].HeaderText = Properties.Resources.Update;
            #endregion

            #region Design and Settings
            dgv.Columns["noInsert"].Visible = false;
            dgv.Columns["tbUpdate"].ReadOnly = true;

            //カラム幅自動に設定
            foreach (DataGridViewColumn dc in dgv.Columns)
            { dc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; }

            //並び替え
            DataView dv = dt.DefaultView;
            dv.Sort = "no ASC";
            #endregion

            #region event
            this.dgv.CellValueChanged += new DataGridViewCellEventHandler(this.dgv_CellValueChanged);
            this.dgv.CurrentCellDirtyStateChanged += new EventHandler(this.dgv_CurrentCellDirtyStateChanged);
            #endregion

            #endregion
        }

        #region TreeView
        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            NodeWithId tmp_node = (NodeWithId)e.Node.Clone();
            DataRow[] drs;
            drs = CLocalDB.localDB.Tables["diag_category"].Select("id='" + tmp_node.nodeId.ToString() + "'");
            if (drs[0]["bt_order"].ToString() == "0")
            { return; }

            start_no = int.Parse(drs[0]["start_no"].ToString());
            end_no = int.Parse(drs[0]["end_no"].ToString());
            dt.DefaultView.RowFilter = "(no>=" + start_no + ")AND(no<=" + end_no + ")";

            lbNoRange.Text = start_no.ToString() + " <= No <= " + end_no.ToString();
        }
        #endregion

        #region dgv
        private void showList()
        {
            #region connnection
            NpgsqlConnection conn;

            try
            {
                conn = new NpgsqlConnection("Server=" + Settings.DBSrvIP + ";Port=" + Settings.DBSrvPort + ";User Id=" +
                    Settings.DBconnectID + ";Password=" + Settings.DBconnectPw + ";Database=endoDB;" + Settings.sslSetting);
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

            string sql = "SELECT no, name_eng, name_jp, diag_order, diag_visible FROM diag_name";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            { MessageBox.Show(Properties.Resources.NoData, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else
            { this.dgv.Refresh(); }
            conn.Close();

            #region Add noInsert
            dt.Columns.Add("noInsert", System.Type.GetType("System.Boolean"));
            for (int i = 0; i < dt.Rows.Count; i++)
            { dt.Rows[i]["noInsert"] = true; }
            #endregion

            dt.Columns.Add("tbUpdate", System.Type.GetType("System.String"));
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string sql;
            #region btDelete
            if (dgv.Columns[e.ColumnIndex].Name == "btDelete")
            {
                if ((dgv.Rows[e.RowIndex].Cells["tbUpdate"].Value != null) && (dgv.Rows[e.RowIndex].Cells["tbUpdate"].Value.ToString() != ""))//更新テキストが出てたら削除できない。
                {
                    MessageBox.Show(Properties.Resources.YouCanDelAfterUpdate, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (MessageBox.Show(Properties.Resources.ConfirmDel, "Information", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    sql = "DELETE FROM diag_name WHERE no=" + dgv.Rows[e.RowIndex].Cells["no"].Value.ToString();
                    if (uckyFunctions.ExeNonQuery(sql) == uckyFunctions.functionResult.failed)
                    { MessageBox.Show(Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    else
                    {
                        dgv.Rows.RemoveAt(e.RowIndex);
                        dgv.Refresh();
                    }
                }
            }
            #endregion
            #region tbUpdate
            else if (dgv.Columns[e.ColumnIndex].Name == "tbUpdate")
            {
                if (dgv.Rows[e.RowIndex].Cells["tbUpdate"].Value.ToString() == Properties.Resources.Click2Update)
                {
                    #region ErrorCheck
                    if (string.IsNullOrWhiteSpace(dgv.Rows[e.RowIndex].Cells["no"].Value.ToString()))
                    {
                        MessageBox.Show(Properties.Resources.NoIsNeeded, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(dgv.Rows[e.RowIndex].Cells["name_eng"].Value.ToString()) && string.IsNullOrWhiteSpace(dgv.Rows[e.RowIndex].Cells["name_jp"].Value.ToString()))
                    {
                        MessageBox.Show(Properties.Resources.DiagnosisIsRequired, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (int.Parse(dgv.Rows[e.RowIndex].Cells["no"].Value.ToString()) < start_no || int.Parse(dgv.Rows[e.RowIndex].Cells["no"].Value.ToString()) > end_no)
                    {
                        lbNoRange.ForeColor = Color.Red;
                        lbNoRange.Font = new Font(lbNoRange.Font, FontStyle.Bold);
                        MessageBox.Show(Properties.Resources.NoOutOfRange + Environment.NewLine + start_no.ToString() + " <= No <=" + end_no.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lbNoRange.ForeColor = Color.Black;
                        lbNoRange.Font = new Font(lbNoRange.Font, FontStyle.Regular);
                        return;
                    }

                    for (int i = 0; i < (dgv.RowCount - 1); i++)
                    {
                        if (dgv.Rows[i].Cells["no"].Value.ToString() == dgv.Rows[e.RowIndex].Cells["no"].Value.ToString() && i != e.RowIndex)
                        {
                            MessageBox.Show("[No]" + Properties.Resources.Duplicated, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    #endregion

                    //diag_visibleがnullだったらfalseに設定しておく。これしないとこの下でバグる。
                    if (string.IsNullOrWhiteSpace(dgv.Rows[e.RowIndex].Cells["diag_visible"].Value.ToString()))
                    { dgv.Rows[e.RowIndex].Cells["diag_visible"].Value = false; }

                    if (dgv.Rows[e.RowIndex].Cells["noInsert"].Value.ToString() == "True")
                    {
                        sql = "UPDATE diag_name SET ";
                        if (!String.IsNullOrWhiteSpace(dgv.Rows[e.RowIndex].Cells["name_eng"].Value.ToString()))
                        { sql += "name_eng=:p0, "; }

                        if (!String.IsNullOrWhiteSpace(dgv.Rows[e.RowIndex].Cells["name_jp"].Value.ToString()))
                        { sql += "name_jp=:p1, "; }

                        if (!String.IsNullOrWhiteSpace(dgv.Rows[e.RowIndex].Cells["diag_order"].Value.ToString()))
                        { sql += "diag_order=:p2, "; }

                        sql += "diag_visible=:p3 WHERE no=:p4"; //Exist of "no" has already checked above.

                        switch (uckyFunctions.ExeNonQuery(sql, dgv.Rows[e.RowIndex].Cells["name_eng"].Value.ToString(),
                            dgv.Rows[e.RowIndex].Cells["name_jp"].Value.ToString(),
                            dgv.Rows[e.RowIndex].Cells["diag_order"].Value.ToString(),
                            dgv.Rows[e.RowIndex].Cells["diag_visible"].Value.ToString(),
                            dgv.Rows[e.RowIndex].Cells["no"].Value.ToString()))
                        {
                            case uckyFunctions.functionResult.connectionError:
                                MessageBox.Show(Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            case uckyFunctions.functionResult.failed:
                                MessageBox.Show(Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            case uckyFunctions.functionResult.success:
                                dgv.Rows[e.RowIndex].Cells["tbUpdate"].Value = "";
                                break;
                        }
                    }
                    else
                    {
                        string sql1 = "INSERT INTO diag_name(no";
                        string sql2 = "VALUES(:p0";

                        if (!string.IsNullOrWhiteSpace(dgv.Rows[e.RowIndex].Cells["name_eng"].Value.ToString()))
                        {
                            sql1 += ", name_eng";
                            sql2 += ", :p1";
                        }

                        if (!string.IsNullOrWhiteSpace(dgv.Rows[e.RowIndex].Cells["name_jp"].Value.ToString()))
                        {
                            sql1 += ", name_jp";
                            sql2 += ", :p2";
                        }

                        if (!string.IsNullOrWhiteSpace(dgv.Rows[e.RowIndex].Cells["diag_order"].Value.ToString()))
                        {
                            sql1 += ", diag_order";
                            sql2 += ", :p3";
                        }

                        sql = sql1 + ", diag_visible) " + sql2 + ", :p4);";
                        switch (uckyFunctions.ExeNonQuery(sql, dgv.Rows[e.RowIndex].Cells["no"].Value.ToString(),
                            dgv.Rows[e.RowIndex].Cells["name_eng"].Value.ToString(),
                            dgv.Rows[e.RowIndex].Cells["name_jp"].Value.ToString(),
                            dgv.Rows[e.RowIndex].Cells["diag_order"].Value.ToString(),
                            dgv.Rows[e.RowIndex].Cells["diag_visible"].Value.ToString()))
                        {
                            case uckyFunctions.functionResult.connectionError:
                                MessageBox.Show(Properties.Resources.ConnectFailed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            case uckyFunctions.functionResult.failed:
                                MessageBox.Show(Properties.Resources.DataBaseError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            case uckyFunctions.functionResult.success:
                                dgv.Rows[e.RowIndex].Cells["tbUpdate"].Value = "";
                                dgv.Refresh();
                                break;
                        }
                    }
                }
            }
            #endregion
        }

        private void dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv.IsCurrentCellDirty)
            { dgv.CommitEdit(DataGridViewDataErrorContexts.Commit); }
        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.Columns[e.ColumnIndex].HeaderText != Properties.Resources.Update)
            { dgv.Rows[e.RowIndex].Cells["tbUpdate"].Value = Properties.Resources.Click2Update; }
        }
        #endregion

        private void btClose_Click(object sender, EventArgs e)
        { this.Close(); }

        private void EditDiagnoses_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["tbUpdate"].ToString() == Properties.Resources.Click2Update)
                {
                    if (MessageBox.Show(Properties.Resources.ChangesNotSavedDiscardOrNot, "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
                        == DialogResult.Cancel)
                    { e.Cancel = true; }
                }
            }
        }
    }

    public class NodeWithId : TreeNode
    {
        public int nodeId { get; set; }
        public NodeWithId(string nodeName, int idNum)
            : base(nodeName)
        { nodeId = idNum; }
        public override object Clone()
        {
            NodeWithId rtn_obj = new NodeWithId(this.Name, this.nodeId);
            return rtn_obj;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace endoDB
{
    public partial class ExamResult : Form
    {
        string html;
        public ExamResult(string _exam_id)
        {
            InitializeComponent();
            webBrowser1.AllowWebBrowserDrop = false;
            webBrowser1.IsWebBrowserContextMenuEnabled = false;
            webBrowser1.WebBrowserShortcutsEnabled = false;

            StreamReader sr = new StreamReader("result.html");
            html = sr.ReadToEnd();
            sr.Close();

            Exam exam = new Exam(_exam_id);

            #region ReplaceStrings
            html = html.Replace("[[[title]]]", Properties.Resources.ExamReport);
            html = html.Replace("[[[pt_id]]]", exam.pt_id);
            html = html.Replace("[[[lbName]]]", Properties.Resources.Name + ":");
            html = html.Replace("[[[Name]]]", exam.pt_name);
            html = html.Replace("[[[lbExamDate]]]", Properties.Resources.ExamDate + ":");
            html = html.Replace("[[[ExamDate]]]", exam.exam_day.ToLongDateString());
            html = html.Replace("[[[lbPurpose]]]", Properties.Resources.Purpose + ":");
            html = html.Replace("[[[Purpose]]]", exam.purpose);
            html = html.Replace("[[[ExamType]]]", exam.getExamTypeName());
            html = html.Replace("[[[lbDepartment]]]", Properties.Resources.Department + ":");
            html = html.Replace("[[[Department]]]", exam.getDepartmentName());
            html = html.Replace("[[[lbOrderedDr]]]", Properties.Resources.OrderedDr + ":");
            html = html.Replace("[[[OrderedDr]]]", exam.order_dr);
            html = html.Replace("[[[lbWard]]]", Properties.Resources.Ward + ":");
            html = html.Replace("[[[Ward]]]", exam.getWardName());
            html = html.Replace("[[[lbOperators]]]", Properties.Resources.Operators + ":");
            html = html.Replace("[[[Operators]]]", exam.getAllOperators());
            html = html.Replace("[[[lbEquipment]]]", Properties.Resources.Equipment + ":");
            html = html.Replace("[[[Equipment]]]", exam.getEquipmentName());
            html = html.Replace("[[[lbPlace]]]", Properties.Resources.PlaceName + ":");
            html = html.Replace("[[[Place]]]", exam.getPlaceName());
            html = html.Replace("[[[lbDiagnosedDr]]]", Properties.Resources.DiagnosedDr + ":");
            html = html.Replace("[[[DiagnosedDr]]]", exam.getDiagDr());
            html = html.Replace("[[[lbChecker]]]", Properties.Resources.Checker + ":");
            html = html.Replace("[[[Checker]]]", exam.getFinalDiagDr());
            html = html.Replace("[[[lbDiagnoses]]]", Properties.Resources.Diagnoses + ":");
            html = html.Replace("[[[Diagnoses]]]", exam.getDiagnoses().Replace("\n", "<br />"));
            html = html.Replace("[[[lbFindings]]]", Properties.Resources.Findings + ":");
            html = html.Replace("[[[Findings]]]", exam.findings.Replace("\n", "<br />"));
            html = html.Replace("[[[lbCheckerComment]]]", Properties.Resources.Comment + ":");
            html = html.Replace("[[[CheckerComment]]]", exam.comment.Replace("\n", "<br />"));
            #endregion

            webBrowser1.DocumentText = html;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        { webBrowser1.ShowPrintPreviewDialog(); }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        { webBrowser1.ShowPageSetupDialog(); }
    }
}

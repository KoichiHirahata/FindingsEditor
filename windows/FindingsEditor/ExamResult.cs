using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FindingsEdior
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
            html = html.Replace("[[[title]]]", FindingsEditor.Properties.Resources.ExamReport);
            html = html.Replace("[[[pt_id]]]", exam.pt_id);
            html = html.Replace("[[[lbName]]]", FindingsEditor.Properties.Resources.Name + ":");
            html = html.Replace("[[[Name]]]", exam.pt_name);
            html = html.Replace("[[[lbExamDate]]]", FindingsEditor.Properties.Resources.ExamDate + ":");
            html = html.Replace("[[[ExamDate]]]", exam.exam_day.ToLongDateString());
            html = html.Replace("[[[lbPurpose]]]", FindingsEditor.Properties.Resources.Purpose + ":");
            html = html.Replace("[[[Purpose]]]", exam.purpose);
            html = html.Replace("[[[ExamType]]]", exam.getExamTypeName());
            html = html.Replace("[[[lbDepartment]]]", FindingsEditor.Properties.Resources.Department + ":");
            html = html.Replace("[[[Department]]]", exam.getDepartmentName());
            html = html.Replace("[[[lbOrderedDr]]]", FindingsEditor.Properties.Resources.OrderedDr + ":");
            html = html.Replace("[[[OrderedDr]]]", exam.order_dr);
            html = html.Replace("[[[lbWard]]]", FindingsEditor.Properties.Resources.Ward + ":");
            html = html.Replace("[[[Ward]]]", exam.getWardName());
            html = html.Replace("[[[lbOperators]]]", FindingsEditor.Properties.Resources.Operators + ":");
            html = html.Replace("[[[Operators]]]", exam.getAllOperators());
            html = html.Replace("[[[lbEquipment]]]", FindingsEditor.Properties.Resources.Equipment + ":");
            html = html.Replace("[[[Equipment]]]", exam.getEquipmentName());
            html = html.Replace("[[[lbPlace]]]", FindingsEditor.Properties.Resources.PlaceName + ":");
            html = html.Replace("[[[Place]]]", exam.getPlaceName());
            html = html.Replace("[[[lbDiagnosedDr]]]", FindingsEditor.Properties.Resources.DiagnosedDr + ":");
            html = html.Replace("[[[DiagnosedDr]]]", exam.getDiagDr());
            html = html.Replace("[[[lbChecker]]]", FindingsEditor.Properties.Resources.Checker + ":");
            html = html.Replace("[[[Checker]]]", exam.getFinalDiagDr());
            html = html.Replace("[[[lbDiagnoses]]]", FindingsEditor.Properties.Resources.Diagnoses + ":");
            html = html.Replace("[[[Diagnoses]]]", exam.getDiagnoses().Replace("\n", "<br />"));
            html = html.Replace("img src=\"\" alt=\"image1\"",
                "img src=\"" + Settings.figureFolder + "\\" + exam.exam_day.Year.ToString() + "\\" + exam.exam_id + "_1.png\"");
            html = html.Replace("img src=\"\" alt=\"image2\"",
                "img src=\"" + Settings.figureFolder + "\\" + exam.exam_day.Year.ToString() + "\\" + exam.exam_id + "_2.png\"");
            html = html.Replace("[[[lbFindings]]]", FindingsEditor.Properties.Resources.Findings + ":");
            html = html.Replace("[[[Findings]]]", exam.findings.Replace("\n", "<br />"));
            html = html.Replace("[[[lbCheckerComment]]]", FindingsEditor.Properties.Resources.Comment + ":");
            html = html.Replace("[[[CheckerComment]]]", exam.comment.Replace("\n", "<br />"));
            #endregion

            webBrowser1.DocumentText = html;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        { webBrowser1.ShowPrintPreviewDialog(); }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        { webBrowser1.ShowPageSetupDialog(); }

        private void webBrowser1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.P && e.Control == true)
            { webBrowser1.Print(); }
        }

    }
}

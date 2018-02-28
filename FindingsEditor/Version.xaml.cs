using System;
using System.Windows;
using System.Reflection;

namespace FindingsEditor
{
    /// <summary>
    /// Version.xaml の相互作用ロジック
    /// </summary>
    public partial class Version : Window
    {
        public Version()
        {
            InitializeComponent();
            lbVersion.Content += Assembly.GetEntryAssembly().GetName().Version.Major.ToString()
                + "." + Assembly.GetEntryAssembly().GetName().Version.Minor.ToString()
                + "." + Assembly.GetEntryAssembly().GetName().Version.Build.ToString();

            string db_version = feFunctions.getSelectString("SELECT db_version FROM db_version", Settings.DBSrvIP, Settings.DBSrvPort, Settings.DBconnectID, Settings.DBconnectPw, Settings.DBname);
            if (db_version != null)
            { lbDbVersion.Content = "DataBase Version: " + db_version; }
            else
            { lbDbVersion.Content = ""; }

            AssemblyCopyrightAttribute cra = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCopyrightAttribute));
            lbCopyRight.Content = cra.Copyright.ToString();

            hlMicUrl.NavigateUri = new Uri(Properties.Resources.MicUrl.ToString());
        }

        private void hlMicUrl_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        { System.Diagnostics.Process.Start(e.Uri.ToString()); }
    }
}

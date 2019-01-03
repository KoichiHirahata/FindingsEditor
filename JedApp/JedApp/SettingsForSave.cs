using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JedApp
{
    [Serializable()]
    class SettingsForSave
    {
        public string DBSrvIP { get; set; } //データベースサーバーのIPアドレスを格納するプロパティ
        public string DBSrvPort { get; set; } //データベースサーバーのポート番号を格納するプロパティ
        public string DBconnectID { get; set; } //データベースに接続するためのIDを格納するプロパティ
        public string DBconnectPw { get; set; } //データベースに接続するためのパスワードを格納するプロパティ
        public string figureFolder { get; set; }
        public string ptInfoPlugin { get; set; } //File location of the plug-in to get patient information
        public int? IdLength { get; set; }
    }
}

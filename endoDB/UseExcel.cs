using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace endoDB
{
    public class UseExcel
    {
        /// <summary>This function read value from xls file as string.</summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string readXls(string fileName, string cellAddr)
        {
            Application xlApp = null;
            Workbook wb = null;
            Range aRange = null;
            string rangeValue = null;
            //ファイルが存在しない、あるいは開けない、書き込み権限を実行ユーザが持っていないなどといった場合は例外が発生するので、実際の実装では妥当性の確認を行うように心がけてください。
            try
            {
                try
                {
                    xlApp = new Application();
                }
                catch
                {
                    throw new Exception(Properties.Resources.ExcelNeeded);
                }

                if (xlApp != null)
                {
                    //xlApp.Visible = true; //This code will show you Excel window.
                    wb = xlApp.Workbooks.Open(
                        fileName,  // オープンするExcelファイル名
                        Type.Missing, // （省略可能）UpdateLinks (0 / 1 / 2 / 3)
                        Type.Missing, // （省略可能）ReadOnly (True / False )
                        Type.Missing, // （省略可能）Format
                        // 1:タブ / 2:カンマ (,) / 3:スペース / 4:セミコロン (;)
                        // 5:なし / 6:引数 Delimiterで指定された文字
                        Type.Missing, // （省略可能）Password
                        Type.Missing, // （省略可能）WriteResPassword
                        Type.Missing, // （省略可能）IgnoreReadOnlyRecommended
                        Type.Missing, // （省略可能）Origin
                        Type.Missing, // （省略可能）Delimiter
                        Type.Missing, // （省略可能）Editable
                        Type.Missing, // （省略可能）Notify
                        Type.Missing, // （省略可能）Converter
                        Type.Missing, // （省略可能）AddToMru
                        Type.Missing, // （省略可能）Local
                        Type.Missing  // （省略可能）CorruptLoad
                        );
                    ((Worksheet)wb.Sheets[1]).Select(Type.Missing);
                    aRange = xlApp.get_Range(cellAddr, Type.Missing) as Range;

                    if (aRange != null)
                    {
                        rangeValue = aRange.Value.ToString();
                    }
                    else
                    {
                        rangeValue = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (wb != null)
                {
                    wb.Close(true, Type.Missing, Type.Missing);
                    Marshal.ReleaseComObject(wb);
                    wb = null;
                }

                if (xlApp != null)
                {
                    xlApp.Quit();
                    Marshal.ReleaseComObject(xlApp);
                    xlApp = null;
                }

                System.GC.Collect();
            }
            return rangeValue;
        }

        public static string findCell(string fileName, string searchStr)
        {
            Application xlApp = null;
            Workbook wb = null;
            Worksheet ws = null;
            Range aRange = null;
            Range firstFind = null;
            string str = null;

            try
            {
                try
                {
                    xlApp = new Application();
                }
                catch
                {
                    throw new Exception(Properties.Resources.ExcelNeeded);
                }

                if (xlApp != null)
                {
                    //xlApp.Visible = true; //This code will show you Excel window.
                    wb = xlApp.Workbooks.Open(
                        fileName,  // オープンするExcelファイル名
                        Type.Missing, // （省略可能）UpdateLinks (0 / 1 / 2 / 3)
                        Type.Missing, // （省略可能）ReadOnly (True / False )
                        Type.Missing, // （省略可能）Format
                        // 1:タブ / 2:カンマ (,) / 3:スペース / 4:セミコロン (;)
                        // 5:なし / 6:引数 Delimiterで指定された文字
                        Type.Missing, // （省略可能）Password
                        Type.Missing, // （省略可能）WriteResPassword
                        Type.Missing, // （省略可能）IgnoreReadOnlyRecommended
                        Type.Missing, // （省略可能）Origin
                        Type.Missing, // （省略可能）Delimiter
                        Type.Missing, // （省略可能）Editable
                        Type.Missing, // （省略可能）Notify
                        Type.Missing, // （省略可能）Converter
                        Type.Missing, // （省略可能）AddToMru
                        Type.Missing, // （省略可能）Local
                        Type.Missing  // （省略可能）CorruptLoad
                        );
                    ws = (Worksheet)wb.Sheets[1];
                    aRange = ws.UsedRange;
                    firstFind = aRange.Find(searchStr, Type.Missing, XlFindLookIn.xlValues, XlLookAt.xlPart,
                       XlSearchOrder.xlByRows, XlSearchDirection.xlNext, false,
                       Type.Missing, Type.Missing);
                    str = firstFind.get_Address(Type.Missing, Type.Missing, XlReferenceStyle.xlA1, Type.Missing, Type.Missing);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (aRange != null)
                {
                    Marshal.ReleaseComObject(aRange);
                    aRange = null;
                }

                if (firstFind != null)
                {
                    Marshal.ReleaseComObject(firstFind);
                    firstFind = null;
                }

                if (ws != null)
                {
                    Marshal.ReleaseComObject(ws);
                    ws = null;
                }
                
                if (wb != null)
                {
                    wb.Close(true, Type.Missing, Type.Missing);
                    Marshal.ReleaseComObject(wb);
                    wb = null;
                }

                if (xlApp != null)
                {
                    xlApp.Quit();
                    Marshal.ReleaseComObject(xlApp);
                    xlApp = null;
                }

                System.GC.Collect();
            }
            return str;
        }

        public static void editXls(string fileName, string cellAddr, string str)
        {
            //ファイルが存在しない、あるいは開けない、書き込み権限を実行ユーザが持っていないなどといった場合は例外が発生するので、実際の実装では妥当性の確認を行うように心がけてください。
            Application xlApp = new Application();
            if (xlApp != null)
            {
                //xlApp.Visible = true; //This code will show you Excel window.
                Workbook wb = xlApp.Workbooks.Open(
                    fileName,  // オープンするExcelファイル名
                    Type.Missing, // （省略可能）UpdateLinks (0 / 1 / 2 / 3)
                    Type.Missing, // （省略可能）ReadOnly (True / False )
                    Type.Missing, // （省略可能）Format
                    // 1:タブ / 2:カンマ (,) / 3:スペース / 4:セミコロン (;)
                    // 5:なし / 6:引数 Delimiterで指定された文字
                    Type.Missing, // （省略可能）Password
                    Type.Missing, // （省略可能）WriteResPassword
                    Type.Missing, // （省略可能）IgnoreReadOnlyRecommended
                    Type.Missing, // （省略可能）Origin
                    Type.Missing, // （省略可能）Delimiter
                    Type.Missing, // （省略可能）Editable
                    Type.Missing, // （省略可能）Notify
                    Type.Missing, // （省略可能）Converter
                    Type.Missing, // （省略可能）AddToMru
                    Type.Missing, // （省略可能）Local
                    Type.Missing  // （省略可能）CorruptLoad
                    );
                Worksheet ws = (Worksheet)wb.Sheets[1];
                Range aRange = ws.get_Range(cellAddr, Type.Missing) as Range;

                if (aRange != null)
                {
                    aRange.Value = str;
                }
                wb.Close(true, Type.Missing, Type.Missing);
                xlApp.Quit();
            }
        }
    }
}

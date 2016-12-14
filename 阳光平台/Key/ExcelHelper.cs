using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace SHYSInterface
{
    /// <summary>
    /// excel工具类,得根据excel表的设置特殊处理
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// 采用odbc的方式导入excel表格数据(性能较快，但需要特殊处理excel表头数据)
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static DataSet QueryByODBC(string filePath, string sheetName)
        {
            string connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                      + filePath + ";Extended Properties='Excel 8.0;HDR=False;IMEX=1'";
            OleDbConnection oleConn = new OleDbConnection(connection);
            DataSet dataSet = new DataSet();
            try
            {
                oleConn.Open();
                string sql = "SELECT * FROM [" + sheetName + "$]";
                OleDbDataAdapter oleDaExcel = new OleDbDataAdapter(sql, oleConn);
                oleDaExcel.Fill(dataSet, sheetName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                oleConn.Close();
            }
            return dataSet;
        }

        /// <summary>
        /// 将excel表格转换成datatable
        /// </summary>
        /// <param name="worksheet"></param>
        /// <returns></returns>
        private static DataTable WorksheetToDataTable(Excel.Worksheet worksheet)
        {
            DataTable table = new DataTable();
            for (int i = 0; i < worksheet.UsedRange.Cells.Rows.Count; ++i)
            {
                DataRow dr = table.NewRow();
                table.Rows.Add(dr);
                for (int j = 0; j < worksheet.UsedRange.Cells.Columns.Count; ++j)
                {
                    if (i == 0)
                    {
                        DataColumn dc = new DataColumn();
                        dc.DataType = Type.GetType("System.String");
                        dc.ColumnName = ((Excel.Range)worksheet.Cells[i + 1, j + 1]).Text.ToString();
                        table.Columns.Add(dc);
                    }
                    else
                    {
                        dr[j] = ((Excel.Range)worksheet.Cells[i + 1, j + 1]).Text.ToString();
                    }
                }
            }
            return table;
        }

        /// <summary>
        /// 采用标准excel格式导入excel表数据（性能较差）
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static DataSet QueryByExcel(string filePath, string sheetName)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = false;
            object oMissiong = System.Reflection.Missing.Value;
            Excel.Workbook workbook = app.Workbooks.Open(filePath, oMissiong, oMissiong,
                    oMissiong, oMissiong, oMissiong, oMissiong, oMissiong,
                    oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong);
            DataSet dataSet = new DataSet();
            try 
            {
                Excel.Sheets sheets = workbook.Worksheets;
                for (int i = 0; i < sheets.Count; i++)
                {
                    Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(i + 1);
                    if (worksheet.Name == sheetName)
                    {
                        dataSet.Tables.Add(WorksheetToDataTable(worksheet));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                workbook.Close(false, oMissiong, oMissiong);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                app.Workbooks.Close();
                app.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
            }
            return dataSet;
        }
    }
}

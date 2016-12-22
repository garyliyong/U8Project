using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace DataBaseHelper
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
        /// 采用odbc的方式更新excel表格数据(性能较快，但需要特殊处理excel表头数据)
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static bool UpdateByODBC(string filePath, string sheetName, Dictionary<string, string> values, 
            Dictionary<string, string> wheres)
        {
            if (values == null || values.Count == 0 || wheres == null || wheres.Count == 0)
            {
                return false;
            }
            string connection = "Provider=Microsoft.JET.OLEDB.4.0;Data Source="
                      + filePath + ";Extended Properties='Excel 8.0;HDR=yes;IMEX=2'";
            OleDbConnection oleConn = new OleDbConnection(connection);
            DataSet dataSet = new DataSet();
            bool result = false;
            try
            {
                oleConn.Open();
                int index = 0;
                StringBuilder sql = new StringBuilder("UPDATE [" + sheetName + "$]");
                sql.Append(" SET ");
                foreach (var dict in values)
                {
                    sql.Append(dict.Key).Append("=").Append("'").Append(
                            dict.Value).Append("'");
                    if (index++ != values.Count - 1)
                    {
                        sql.Append(",");
                    }
                }
                sql.Append(" WHERE ");
                index = 0;
                foreach (var dict in wheres)
                {
                    sql.Append(dict.Key).Append("=").Append("'").Append(
                            dict.Value).Append("'");
                    if (index++ != wheres.Count - 1)
                    {
                        sql.Append("AND");
                    }
                }
                OleDbCommand cmd = new OleDbCommand(sql.ToString(), oleConn); 
                int rows = cmd.ExecuteNonQuery(); 
                if (rows > 0)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                oleConn.Close();
            }
            return result;
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

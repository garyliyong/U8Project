using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Collections;



namespace SHYSInterface
{
    class Public
    {

        public static string ufconnstr_870 = "";//870数据连接
        public static string ufconnstr_111 = "";//11.1数据连接
        public static string SupplyConnStr = "";//SupplyData

        public static string cacc_id_870 = "";
        public static string cacc_id_111 = "";

        //public static string company1 = "Data Source=wade_pc;Initial Catalog=UFDATA_001_2012;User ID=sa;Password=123";//公司1
        public static string adoconnstr = "";
        public static string userCode = "demo";
        public static string userName = "demo";
        public static string ddate = "2010-07-19";
        public static string company1_cacc_id = "001";
        public static string company2_cacc_id = "999";
        public static string factory_cacc_id = "002";
        public static string strufdata = "UFDATA_999_2012";

        public static string cPTCode = "";
        public static string ReceiptInBox = "";
        public static string ReceiptInBoxBak = "";
        public static string ReceiptInBoxError = "";

        public static string ShipInBox = "";
        public static string ShipInBoxBak = "";
        public static string ShipInBoxError = "";

        public static string ReceiptOutBox = "";
        public static string ShipOutBox = "";
        public static string ShipOutBoxSW = "";
        

        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="dgvPara"></param>
        public static void ExportExcel(DataGridView dgvPara)
        {
            //int intColIndex = 1;                            //列序号
            int intRowCount = dgvPara.RowCount;             //行数
            int intColCount = dgvPara.ColumnCount;          //列数
            //object[,] objData;                             //保存DataGridView中的数据
            string strFileName = "";

            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = "";
                dlg.Filter = "Excel|*.xls";
                //dlg.InitialDirectory = Directory.GetCurrentDirectory();
                if (dlg.ShowDialog() == DialogResult.Cancel) return;
                strFileName = dlg.FileName;
                if (strFileName.Trim() == " ")
                    return;
                Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;
                if (System.IO.File.Exists(strFileName))
                {
                    System.IO.File.Delete(strFileName);
                }


                int iColCountv = 0;
                for (int i = 0; i < dgvPara.Columns.Count; i++)
                {
                    if (dgvPara.Columns[i].Visible == true)
                    {
                        iColCountv++;
                    }
                }

                //判断：如果行数或者列数有问题，则不予导出
                if (intRowCount == 0)
                {
                    MessageBox.Show("列表中行数为零!", "提示");
                    return;
                }
                if (intColCount == 0)
                {
                    MessageBox.Show("列表中列数为零!", "提示");
                    return;
                }
                if (intRowCount > 65536)
                {
                    MessageBox.Show("数据记录不能超过65536条!", "提示");
                    return;
                }
                if (intColCount > 255)
                {
                    MessageBox.Show("列数不能大于255!", "提示");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("输出失败，可能的原因是：" + ex.ToString(), "提示");
                return;
            }

            string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFileName + ";Extended Properties=Excel 8.0";
            OleDbConnection objConn = new OleDbConnection(connString);
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                //先查看此Excel中是否有相关Table，如果有的话就删除，然后导入新的。
                //生成创建表的脚本
                stringBuilder.Append("CREATE TABLE ");
                stringBuilder.Append("Sheet1" + " ( ");
                foreach (DataGridViewColumn col in dgvPara.Columns)
                {
                    if (col.Visible == false)
                        continue;
                    stringBuilder.Append(string.Format("[{0}] {1},", col.HeaderText, "VarChar"));
                }
                if (stringBuilder.Length > 0)
                {
                    stringBuilder = stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
                stringBuilder.Append(")");

                OleDbCommand objCmd = new OleDbCommand();
                objCmd.Connection = objConn;

                //插入新表
                objCmd.CommandText = stringBuilder.ToString();
                objConn.Open();
                //插入新表
                objCmd.ExecuteNonQuery();

                stringBuilder.Remove(0, stringBuilder.Length);

                stringBuilder.Append("INSERT INTO ");
                stringBuilder.Append("Sheet1 ( ");
                //先插入标头
                foreach (DataGridViewColumn col in dgvPara.Columns)
                {
                    if (col.Visible == false)
                        continue;
                    stringBuilder.Append("[" + col.HeaderText + "],");
                }
                if (stringBuilder.Length > 0)
                {
                    stringBuilder = stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
                stringBuilder.Append(") values (");

                foreach (DataGridViewColumn col in dgvPara.Columns)
                {
                    if (col.Visible == false)
                        continue;
                    stringBuilder.Append("@" + col.HeaderText.Replace(" ", "").Replace("%", "") + ",");
                }
                if (stringBuilder.Length > 0)
                {
                    stringBuilder = stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
                stringBuilder.Append(")");

                //建立插入动作的Command
                objCmd.CommandText = stringBuilder.ToString();
                OleDbParameterCollection oleParam = objCmd.Parameters;

                oleParam.Clear();
                foreach (DataGridViewColumn col in dgvPara.Columns)
                {
                    if (col.Visible == false)
                        continue;
                    //此处是本版本改进中最实用的地方
                    oleParam.Add(new OleDbParameter("@" + col.HeaderText, OleDbType.VarChar));
                }

                //遍历DataTable将数据插入新建的Excel文件中
                int l = 0;
                foreach (DataGridViewRow row in dgvPara.Rows)
                {
                    l = 0;
                    for (int i = 0; i < dgvPara.Columns.Count; i++)
                    {
                        if (dgvPara.Columns[i].Visible == false)
                            continue;
                        oleParam[l].Value = Convert.ToString(row.Cells[i].Value);
                        l++;

                    }

                    objCmd.ExecuteNonQuery();
                }
                //objConn.Close();
                //objConn.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show("输出失败，可能的原因是：" + ex.ToString(), "提示");
                return;
            }
            finally
            {
                objConn.Close();
                objConn.Dispose();
            }

            MessageBox.Show("输出成功！", "提示");

        }

        /// <summary>
        /// 显示日期字段
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string DispDate(object o)
        {
            if (o == System.DBNull.Value || Convert.ToString(o) == "")
            {
                return null;
            }
            else
            {
            }
            return Convert.ToDateTime(o).ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 将字段生成拼接字段
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string SqlParm(object o)
        {
            if (o == System.DBNull.Value || Convert.ToString(o) == "")
            {
                return "null";
            }
            else
            {
                return "N'" + Convert.ToString(o).Replace("'", "''") + "'";
            }
        }

        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="value"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static decimal ChinaRound(decimal d, int i)
        {
            if (d >= 0)
            {
                d += Convert.ToDecimal(5 * Math.Pow(10, -(i + 1)));
            }
            else
            {
                d += Convert.ToDecimal(-5 * Math.Pow(10, -(i + 1)));
            }
            string str = d.ToString();
            string[] strs = str.Split('.');
            int idot = str.IndexOf('.');
            string prestr = strs[0];
            string poststr = strs[1];
            if (poststr.Length > i)
            {
                poststr = str.Substring(idot + 1, i);
            }
            string strd = prestr + "." + poststr;
            d = Decimal.Parse(strd);
            return d;
        }

        /// <summary>
        /// 在dgv里找到行
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="colname">列名</param>
        /// <param name="value">值</param>
        /// <param name="start">起始</param>
        /// <param name="end">终止</param>
        /// <returns></returns>
        public static int FindDgvRow(DataGridView dgv, string[] colname, string[] value, int start, int end)
        {
            int iLen = colname.GetUpperBound(0);
            for (int i = start; i < end; i++)
            {
                for (int j = 0; j <= iLen; j++)
                {
                    if (Convert.ToString(dgv.Rows[i].Cells[colname[j]].Value) != value[j])
                        break;
                    if (j == iLen)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="startrow"></param>
        /// <param name="endrow"></param>
        /// <param name="copydgv"></param>
        /// <param name="targetdgv"></param>
        /// <param name="beforerow"></param>
        public static void DgvCopyRow(int startrow, int endrow, DataGridView copydgv, DataGridView targetdgv, int beforerow)
        {
            int row = 0;
            for (int i = startrow; i <= endrow; i++)
            {
                targetdgv.Rows.Insert(beforerow + row, 1);

                for (int l = 0; l < copydgv.Columns.Count; l++)
                {
                    targetdgv.Rows[beforerow + row].Cells[l].Value = Convert.ToString(copydgv.Rows[i].Cells[l].Value);

                }
                row++;
            }
        }

        /// <summary>
        /// 是否是日期
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsDate(object input)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(input);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// 是否是数字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsDecimal(object input)
        {
            try
            {
                decimal dec = Convert.ToDecimal(input);
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 判断字符串是否在一个数组里
        /// </summary>
        /// <param name="strz"></param>
        /// <param name="strm"></param>
        /// <returns></returns>
        public static bool IsInStrings(string strz, string[] strm)
        {
            for (int i = 0; i <= strm.GetUpperBound(0); i++)
            {
                if (strz == strm[i]) return true;
            }
            return false;
        }

        /// <summary>
        /// 判断字符串是否在另一个字符串里,a是否在b里
        /// </summary>
        /// <param name="stra"></param>
        /// <param name="strb"></param>
        /// <returns></returns>
        public static bool IsInString(string stra, string strb)
        {
            if (strb.IndexOf(stra) == -1)
                return false;
            return true;
        }

        /// <summary>
        /// 生成保存dgv的语句
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="tbName">表名</param>
        /// <param name="conn">连接</param>
        /// <returns></returns>
        public static string GetUpdateStr(DataGridView dgv, string tbName, SqlConnection conn)
        {
            string strPKName = dgv.Columns[0].Name;
            string strPKCode = "";
            //SqlCommand sqlcmd = null;
            string strSql = "";
            string strInsert = "insert into " + tbName + " (";
            //生成插入语句前半部分
            for (int i = 1; i < dgv.ColumnCount; i++)
            {
                if (Convert.ToString(dgv.Columns[i].DataPropertyName) == "")
                    continue;
                strInsert += dgv.Columns[i].Name + ",";
            }
            strInsert = strInsert.Substring(0, strInsert.Length - 1);
            strInsert = strInsert + ") values (";

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                strPKCode = Convert.ToString(dgv.Rows[i].Cells[strPKName].Value);
                //strPKCode = dgv.Rows[i].Cells[strPKName].Value == null ? null : dgv.Rows[i].Cells[strPKName].Value.ToString();
                //sqlcmd = new SqlCommand(string.Format("select {0} from {1} where {2} = '{3}'", strPKName, tbName, strPKName, strPKCode), conn);
                //object o = sqlcmd.ExecuteScalar();
                //if (o != null && o != System.DBNull.Value && o.ToString() != "")//update
                if (Convert.ToInt32(SqlAccess.ExecuteScalar("select " + strPKName + " from " + tbName + " where " + strPKName + " = N'" + strPKCode + "'", conn)) > 0)
                {
                    strSql = strSql + string.Format("update {0} set ", tbName);
                    for (int j = 1; j < dgv.ColumnCount; j++)
                    {
                        if (Convert.ToString(dgv.Columns[j].DataPropertyName) == "")
                            continue;

                        strSql = strSql + dgv.Columns[j].Name + " = " + Public.SqlParm(dgv.Rows[i].Cells[j].Value) + ",";

                    }
                    strSql = strSql.Substring(0, strSql.Length - 1);
                    strSql = strSql + string.Format(" where {0} = N'{1}'\n", strPKName, strPKCode);
                }
                else//insert
                {
                    strSql = strSql + strInsert;
                    for (int j = 1; j < dgv.ColumnCount; j++)
                    {
                        if (Convert.ToString(dgv.Columns[j].DataPropertyName) == "")
                            continue;

                        strSql = strSql + Public.SqlParm(dgv.Rows[i].Cells[j].Value) + ",";
                    }
                    strSql = strSql.Substring(0, strSql.Length - 1);
                    strSql = strSql + ")\n";
                }


            }
            //判断一下删除的
            DataTable dt = SqlAccess.ExecuteSqlDataTable("select " + strPKName + " from " + tbName, conn);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strPKCode = Convert.ToString(dt.Rows[i][strPKName]);
                if (Public.FindDgvRow(dgv, new string[] { strPKName }, new string[] { strPKCode }, 0, dgv.Rows.Count) == -1)
                {
                    strSql += "delete from " + tbName + " where " + strPKName + " = " + Public.SqlParm(strPKCode) + "\n";
                }
            }
            return strSql;
        }

        /// <summary>
        /// 判断dgv是否修改，要有datasource
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public static Boolean IsDgvModified(DataGridView dgv)
        {
            System.Data.DataTable dt = (System.Data.DataTable)dgv.DataSource;
            DataRowState rowStates = DataRowState.Modified | DataRowState.Deleted | DataRowState.Added;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                if ((row.RowState & rowStates) != 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 得到数字
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static decimal GetNum(object o)
        {
            if (!IsDecimal(o))
            {
                return 0;
            }
            return Convert.ToDecimal(Convert.ToDouble(o));
        }

        /// <summary>
        /// 不区分大小写替换
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="strRe">需转换的字段</param>
        /// <param name="strTo">转换为</param>
        /// <returns></returns>
        public static string MyReplace(string strSource, string strRe, string strTo)
        {
            string strSl, strRl;
            strSl = strSource.ToLower();
            strRl = strRe.ToLower();
            int start = strSl.IndexOf(strRl);
            if (start != -1)
            {
                strSource = strSource.Substring(0, start) + strTo
                + MyReplace(strSource.Substring(start + strRe.Length), strRe, strTo);
            }
            return strSource;
        }

        /// <summary>
        /// 读取excel文件，传语句
        /// </summary>
        /// <param name="FileName">完整路径</param>
        /// <param name="strExcel">查询语句</param>
        /// <returns></returns>
        public static DataTable ReadExcel(string FileName, string strExcel)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;';";
            OleDbConnection xlsConn = new OleDbConnection(strConn);
            OleDbDataAdapter XlsAdapter = null;
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                xlsConn.Open();
                XlsAdapter = new OleDbDataAdapter(strExcel, strConn);
                XlsAdapter.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                xlsConn.Close();
                xlsConn.Dispose();

            }

        }

        /// <summary>
        /// 读取excel文件，自动读首页
        /// </summary>
        /// <param name="FileName">完整路径</param>
        /// <returns></returns>
        public static System.Data.DataTable ReadExcel(string FileName)
        {
            string sSheetName = GetSheetName(FileName);

            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;';";
            OleDbConnection xlsConn = new OleDbConnection(strConn);
            string strExcel = "select * from [" + sSheetName + "$]";
            OleDbDataAdapter XlsAdapter = null;
            DataTable dt = new System.Data.DataTable();
            try
            {
                xlsConn.Open();
                XlsAdapter = new OleDbDataAdapter(strExcel, strConn);
                XlsAdapter.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {
                throw new Exception("读取数据出错，可能的原因是：" + ex.ToString());
            }
            finally
            {
                xlsConn.Close();
                xlsConn.Dispose();

            }

        }

        /// <summary>
        /// 返回Excel表中的首页名称(传入路径)
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetSheetName(string filePath)
        {
            string sheetName = "";

            System.IO.FileStream tmpStream = File.OpenRead(filePath);
            byte[] fileByte = new byte[tmpStream.Length];
            tmpStream.Read(fileByte, 0, fileByte.Length);
            tmpStream.Close();

            byte[] tmpByte = new byte[]{Convert.ToByte(11),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),
               Convert.ToByte(11),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),
               Convert.ToByte(30),Convert.ToByte(16),Convert.ToByte(0),Convert.ToByte(0)};

            int index = GetSheetIndex(fileByte, tmpByte);
            if (index > -1)
            {

                index += 16 + 12;
                System.Collections.ArrayList sheetNameList = new System.Collections.ArrayList();

                for (int i = index; i < fileByte.Length - 1; i++)
                {
                    byte temp = fileByte[i];
                    if (temp != Convert.ToByte(0))
                        sheetNameList.Add(temp);
                    else
                        break;
                }
                byte[] sheetNameByte = new byte[sheetNameList.Count];
                for (int i = 0; i < sheetNameList.Count; i++)
                    sheetNameByte[i] = Convert.ToByte(sheetNameList[i]);

                sheetName = System.Text.Encoding.Default.GetString(sheetNameByte);
            }
            if (sheetName == "")
                sheetName = "Sheet1";
            return sheetName;
        }

        /// <summary>
        /// 只供方法GetSheetName()使用
        /// </summary>
        /// <returns></returns>
        private static int GetSheetIndex(byte[] FindTarget, byte[] FindItem)
        {
            int index = -1;

            int FindItemLength = FindItem.Length;
            if (FindItemLength < 1) return -1;
            int FindTargetLength = FindTarget.Length;
            if ((FindTargetLength - 1) < FindItemLength) return -1;

            for (int i = FindTargetLength - FindItemLength - 1; i > -1; i--)
            {
                System.Collections.ArrayList tmpList = new System.Collections.ArrayList();
                int find = 0;
                for (int j = 0; j < FindItemLength; j++)
                {
                    if (FindTarget[i + j] == FindItem[j]) find += 1;
                }
                if (find == FindItemLength)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        /// <summary>
        /// 写log日志
        /// </summary>
        /// <param name="logfilepath"></param>
        /// <param name="logText"></param>
        //public static void WriteLog(string logfilepath, string logText)
        //{
        //    try
        //    {
        //        StreamWriter sw;//文件写入流
        //        FileStream fs;
        //        if (!File.Exists(logfilepath))
        //        {
        //            fs = new FileStream(logfilepath, FileMode.Create); //若文件不存在，创建
        //            sw = new StreamWriter(fs);
        //        }
        //        else
        //        {
        //            fs = new FileStream(logfilepath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);//若文件存在，追加
        //            sw = new StreamWriter(fs);
        //        }
        //        sw.WriteLine(logText);
        //        sw.Flush();
        //        sw.Close();
        //        fs.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// 写log数据库
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="fClass"></param>
        /// <param name="fFileName"></param>
        /// <param name="fFileLine"></param>
        /// <param name="fStyle"></param>
        /// <param name="fMemo"></param>
        //public static void WriteLog(SqlConnection conn, string cacc_id, string fClass, string fTable, string fCode, string fMemo, string fSql)
        //{
        //    SqlCommand cmd;
        //    try
        //    {
        //        if (fSql.Length > 250)
        //            fSql = fSql.Substring(0, 250);
        //        cmd = new SqlCommand("insert into u8nc_Log(fDate,fClass,fTable,fCode,fMemo,fSql,cacc_id)"
        //                + " values(getdate(),'" + fClass + "','" + fTable + "','" + fCode + "','" + fMemo + "'," + Public.SqlParm(fSql) + ",'" + cacc_id + "')", conn);
        //        cmd.CommandType = CommandType.Text;
        //        cmd.Prepare();
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static DataTable GetDireItem(string sql, SqlConnection conn)
        {
            DataSet ds = null;

            try
            {

                SqlDataAdapter ad = new SqlDataAdapter(sql, conn);
                ds = new DataSet();
                ad.Fill(ds, "item");
            }
            catch
            {
                //
            }

            return ds.Tables["item"];
        }
        //得到符号前后字段
        public static string GetStr(string as_bz, string str, int flag)
        {
            int pos = 0;
            pos = str.IndexOf(as_bz, 0);
            //前面
            if (flag == 1)
            {
                return str.Substring(0, pos);
            }
            else //后面
            {
                return str.Substring(pos + 1, str.Length - pos - 1);
            }
        }

        

        //获取ID
        public static int GetParentID(int flag, string cVouchType, string cAcc_Id, String constr)
        {
            SqlConnection conn = new SqlConnection(constr);
            if (conn.State.ToString().ToLower() != "open")
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_GetID";
            SqlParameter myParm1 = cmd.Parameters.Add("@RemoteId", SqlDbType.VarChar, 2);
            SqlParameter myParm2 = cmd.Parameters.Add("@cAcc_Id", SqlDbType.VarChar, 3);
            SqlParameter myParm3 = cmd.Parameters.Add("@cVouchType", SqlDbType.VarChar, 50);
            SqlParameter myParm4 = cmd.Parameters.Add("@iAmount", SqlDbType.Int, 4);
            SqlParameter myParm5 = cmd.Parameters.Add("@iFatherId", SqlDbType.Int, 4);
            SqlParameter myParm6 = cmd.Parameters.Add("@iChildId", SqlDbType.Int, 4);
            myParm5.Direction = ParameterDirection.Output;
            myParm6.Direction = ParameterDirection.Output;

            myParm1.Value = "00";
            myParm2.Value = cAcc_Id;
            myParm3.Value = cVouchType;     // "DISPATCH";
            myParm4.Value = 1;
            myParm5.Value = 0;
            myParm6.Value = 0;
            cmd.ExecuteNonQuery();
           
            if (flag == 0)
            {
                conn.Close();
                return (int)cmd.Parameters["@iFatherId"].Value;

            }
            else
            {
                conn.Close();
                return (int)cmd.Parameters["@iChildId"].Value;

            }



        }

      

        public static string GetParentCode(string CardNumber, string cContent, string connstr)//获取 单号
        {
            string s;
            SqlConnection CXmyconn = new SqlConnection(connstr);
            if (CXmyconn.State.ToString().ToLower() != "open")
            {
                CXmyconn.Open();
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = CXmyconn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "if exists(select * from VoucherHistory with(nolock)  Where CardNumber='" + CardNumber + "' and isnull(cContent,'') ='" + cContent + "') " +
                "select '" + cContent + "'+RIGHT('000000000'+Convert(varchar,Convert(int,cNumber)+1),10) as Maxnumber From VoucherHistory with(nolock) Where CardNumber='" + CardNumber + "' and isnull(cContent,'') ='" + cContent + "' " +
                "else " +
                "begin " +
                "insert into VoucherHistory(CardNumber,cNumber,bEmpty,cContent)values('" + CardNumber + "','1',0,'" + cContent + "') " +
                "select '" + cContent + "'+'0000000001' as cNumber " +
                "end ";
            SqlDataReader myReader = cmd.ExecuteReader();
            if (myReader.Read())
            {
                s = myReader.GetValue(0).ToString();
                myReader.Close();
            }
            else
            {
                s = "0000000001";
                myReader.Close();
            }
            CXmyconn.Close();
            return s;

        }

        public static string GetParentCode(string cContent, string connstr)//获取 单号
        {
            string s;
            SqlConnection CXmyconn = new SqlConnection(connstr);
            if (CXmyconn.State.ToString().ToLower() != "open")
            {
                CXmyconn.Open();
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = CXmyconn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "if exists(select * from SQ_CGYC_M with(nolock)  Where substring(ccode,1,10)='" + cContent + "') " +
                "select '" + cContent + "'+RIGHT('000'+Convert(varchar,Convert(int,substring(max(ccode),11,3))+1),3) as Maxnumber From SQ_CGYC_M with(nolock) Where substring(ccode,1,10)='" + cContent + "' ";

            SqlDataReader myReader = cmd.ExecuteReader();
            if (myReader.Read())
            {
                s = myReader.GetValue(0).ToString();
                myReader.Close();
            }
            else
            {
                s = cContent + "001";
                myReader.Close();
            }
            CXmyconn.Close();
            return s;

        }
        public static int getSZ(String str)
        {
            int getInt = 0;
            try
            {
                switch (str)
                {
                    case "一级":
                        getInt = 1;
                        break;
                    case "二级":
                        getInt = 2;
                        break;
                    case "三级":
                        getInt = 3;
                        break;
                    case "四级":
                        getInt = 4;
                        break;
                    case "五级":
                        getInt = 5;
                        break;
                    default:
                        getInt = 0;
                        break;
                }
                return getInt;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        //
        public static string Getconstr(string constr)
        {

            int k = constr.IndexOf("Current Language");
            return constr.Substring(20, k - 20 - 1);
        }

        //得到字符串
        //从以as_bz分隔的一串信息(str)中获取第pos个子串
        //分隔符 as_bz
        //输入参数:str
        //  序号:  pos  
        public static string GetInfo(string as_bz, string str, int pos)
        {
            int pos1 = -1;
            int pos2 = 0;
            int i = 0;
            pos2 = str.IndexOf(as_bz, 0);

            if (pos2 < 0)
            {
                if (pos == 1)
                {
                    return str;
                }
                else
                {
                    return "-1";
                }
            }
            i = 1;
            while (i < pos)
            {
                pos1 = pos2;
                pos2 = str.IndexOf(as_bz, pos1 + 1);

                if (pos2 > 0)
                {
                    i++;
                }
                else
                {
                    break;
                }
            }
            if (i < pos - 1)
            {
                return "-1";
            }
            if (i == pos - 1)
            {
                return str.Substring(pos1 + 1);
            }
            else
            {
                return str.Substring(pos1 + 1, pos2 - pos1 - 1);
            }
        }

        //得到字符串str中有多少个as_bz
        public static int GetInfoGS(string as_bz, string str)
        {
            int pos1 = 0;
            int pos2 = 0;
            int i = 0;
            pos2 = str.IndexOf(as_bz, 0);

            if (pos2 < 0)
            {

                return 0;

            }
            i = 1;
            while (pos1 < str.Length)
            {
                pos1 = pos2;
                pos2 = str.IndexOf(as_bz, pos1 + 1);

                if (pos2 > 0)
                {
                    i++;
                }
                else
                {
                    break;
                }
            }
            return i;

        }
        //累计加零
        public static string Getcount(int count, int sz)
        {
            int j = count - sz.ToString().Length;
            string str = "";
            if (j > 0)
            {
                for (int i = 0; i < j; i++)
                {
                    str = str + "0";
                }
            }

            return str + sz.ToString();
        }


        public static DataTable CheckDataTable(DataTable dt, string tableName,string cHeadKey , string cFlag)
        {
          //  WriteLog.writeLog("校验开始" + DateTime.Now + "tableName" + tableName);
            dt.Columns.Add("Flag");//标识
            // string cValue = "";
            DataTable dtcompare = null;
            Hashtable htCompare = new Hashtable();
            string cBodyKey = "";
            try
            {
                dtcompare = SqlAccess.ExecuteSqlDataTable(" select distinct cdefine28 from " + tableName + "  with(nolock) where cdefine28 like'" + cHeadKey + "%'", ufconnstr_111);

                if (dtcompare != null && dtcompare.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtcompare.Rows)
                    {
                        htCompare.Add(dr[0].ToString(),"");
                    }
                }
                else {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        
                            dt.Rows[i]["Flag"] = "0";
                    }
                    return dt;
                
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //判断
                    cBodyKey = ClsSystem.gnvl(dt.Rows[i]["SERIALKEY"], "");

                    if (htCompare.ContainsKey(cHeadKey + dt.Rows[i]["SERIALKEY"]))
                    {
                        dt.Rows[i]["Flag"] = "1";
                        //  dt.Rows[i]["cFlag"] = "1";

                    }
                    else
                    {
                        dt.Rows[i]["Flag"] = "0";
                    }
                }

                DataView rowfilter1 = new DataView(dt);
                rowfilter1.RowFilter = "Flag = '" + cFlag + "'";
                rowfilter1.RowStateFilter = DataViewRowState.CurrentRows;
                DataTable dts = rowfilter1.ToTable();

                return dts;
            }
            catch (Exception ex)
            {
                MessageBox.Show("具体原因是：\n" + ex.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return dt;
            }
            finally
            {
              //  WriteLog.writeLog("校验结束" + DateTime.Now + "tableName" + tableName);
            }
           
          
           // dt.Columns.Add("Flag");//标识


           //// string cValue = "";
           // DataTable dt = null;
           // string cBodyKey = "";
           // try
           // {
           //     dt = new DataTable();
           //     for (int i = 0; i < dt.Rows.Count; i++)
           //     {
           //         //判断
           //         if (i == 0)
           //         { 
                    
           //         }
           //         cBodyKey = ClsSystem.gnvl(dt.Rows[i]["SERIALKEY"], "");
           //         cValue = ClsSystem.gnvl(SqlAccess.ExecuteScalar(" select cdefine28 from " + tableName + "  with(nolock) where cdefine28='" + cHeadKey + cBodyKey + "'", ufconnstr_111), "");
           //        // dt = ClsSystem.gnvl(SqlAccess.ExecuteSqlDataTable(" select cdefine28 from " + tableName + "  with(nolock) where cdefine28 like'" + cHeadKey + "%'", ufconnstr_111), "");

           //         if (cValue != "")
           //         {
           //             dt.Rows[i]["Flag"] = "1";
           //             //  dt.Rows[i]["cFlag"] = "1";

           //         }
           //         else
           //         {
           //             dt.Rows[i]["Flag"] = "0";
           //         }
           //     }

           //     DataView rowfilter1 = new DataView(dt);
           //     rowfilter1.RowFilter = "Flag = '" + cFlag + "'";
           //     rowfilter1.RowStateFilter = DataViewRowState.CurrentRows;
           //     DataTable dts = rowfilter1.ToTable();

           //     return dts;
           // }
           // catch (Exception ex)
           // {
           //     MessageBox.Show("具体原因是：\n" + ex.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
           //     return dt;
           // }
           // finally
           // {

           // }
        }


        public static bool GetPass(string caccid, string ddate)
        {
            string sql = "";
            DateTime dt1 =DateTime.Now;
            DateTime dt2 = Convert.ToDateTime(ddate);
            TimeSpan span = dt1.Subtract(dt2);
            int dayDiff = span.Days + 1;
            if (dayDiff >= 30)
            {
                sql = @" update UA_Account_Ex set cFinKind='2' where cAcc_Id='" + caccid + "'";
                SqlAccess.ExecuteSql(sql, Program.ConnectionString);
            }

            sql = @" select cFinKind from UA_Account_Ex where cAcc_Id='" + caccid + "'";
            string result =ClsSystem.gnvl( SqlAccess.ExecuteScalar(sql, Program.ConnectionString),"");
            if (result == "2")
            {
                return true;
            }
            return false;
        }

    }



}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Windows.Forms;
using UFIDA.U8.UAP.Common;

namespace SHYSInterface.退货单
{
    public class SqlHelperUtility
    {
        /// <summary>
        /// 根据提供的键值对从数据库中取出内容更新内存中的数据表
        /// </summary>
        /// <param name="table">需要更新的数据表</param>
        /// <param name="connStr">数据库链接串</param>
        /// <param name="Keys">键值对</param>
        public static void UpdateDataTableByKey(ref DataTable table, string connStr, 
            Hashtable Keys)
        {
            StringBuilder selectPart = new StringBuilder("SELECT ");
            for (int i = 0; i < table.Columns.Count; ++i)
            {
                if (i != table.Columns.Count - 1)
                {
                    selectPart.Append(table.Columns[i].ColumnName).Append(",");
                }
                else
                {
                    selectPart.Append(table.Columns[i].ColumnName);
                }
            }

            string fromPart = " FROM " + table.TableName;

            StringBuilder wherePart = new StringBuilder(" WHERE ");
            int index = 0;
            foreach (object key in Keys.Keys)
            {
                index++;
                if (index != Keys.Count)
                {
                    wherePart.Append(key.ToString()).Append(" = ").Append(Keys[key].ToString()).Append(",");
                }
                else
                {
                    wherePart.Append(key.ToString()).Append(" = ").Append(Keys[key].ToString());
                }
            }

            string sqlScript = selectPart.Append(fromPart).Append(wherePart.ToString()).ToString();

            DataSet result = UFIDA.U8.UAP.Common.SqlHelper.ExecuteDataset(connStr, CommandType.Text, sqlScript);

            if (result.Tables.Count == 0)
            {
                throw new Exception("数据库查询失败!");
            }
            table = result.Tables[0].Copy();
        }

        /// <summary>
        /// 查询数据库
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="stringList"></param>
        /// <param name="connStr"></param>
        /// <param name="result"></param>
        public static DataTable SqlHelpQuery(string tableName,List<string> stringList,
            string connStr, Hashtable Keys, string strOrder)
        {
            StringBuilder selectPart = new StringBuilder("SELECT ");
            for (int i = 0; i < stringList.Count; ++i)
            {
                if (i != stringList.Count - 1)
                {
                    selectPart.Append(stringList[i]).Append(",");
                }
                else
                {
                    selectPart.Append(stringList[i]);
                }
            }
            string fromPart = " FROM " + tableName;
            string sqlScript = selectPart.Append(fromPart).ToString();
            if (Keys != null && Keys.Count > 0)
            {
                StringBuilder wherePart = new StringBuilder(" WHERE ");
                int index = 0;
                foreach (object key in Keys.Keys)
                {
                    index++;
                    if (index != Keys.Count)
                    {
                        wherePart.Append(key.ToString()).Append(" = ").Append(Keys[key].ToString()).Append(",");
                    }
                    else
                    {
                        wherePart.Append(key.ToString()).Append(" = ").Append(Keys[key].ToString());
                    }
                }
                sqlScript += wherePart.ToString();
            }
            if (strOrder.Length != 0)
            {
                sqlScript += " ORDER BY " + strOrder;
            }
            DataSet result = UFIDA.U8.UAP.Common.SqlHelper.ExecuteDataset(connStr, CommandType.Text, sqlScript);
            if (result.Tables.Count == 0)
            {
                throw new Exception("查询数据库失败!");
            }
            return result.Tables[0];
        }

        /// <summary>
        /// 查询数据库某一行记录
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sqlScript"></param>
        /// <param name="result"></param>
        public static void SqlHelpQueryOneLine(string connectionString,
            string sqlScript, Hashtable result)
        {
            SqlDataReader reader = UFIDA.U8.UAP.Common.SqlHelper.ExecuteReader(connectionString,
                CommandType.Text, sqlScript);
            try
            {
                if (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        result[reader.GetName(i)] = reader.GetValue(i);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                reader.Close();
            }
        }

        public static void SqlHelpWrite(string tableName,List<string> stringList,
            string connStr, List<string> strValues)
        {
            StringBuilder selectPart = new StringBuilder("INSERT INTO ");
            selectPart.Append(tableName).Append(" (");
            for (int i = 0; i < stringList.Count; ++i)
            {
                if (i != stringList.Count - 1)
                {
                    selectPart.Append(stringList[i]).Append(",");
                }
                else
                {
                    selectPart.Append(stringList[i]);
                }
            }
            selectPart.Append(" )");
            string fromPart = " VALUES ( " ;
            for (int i = 0; i < strValues.Count; ++i)
            {
                if (i != strValues.Count - 1)
                {
                    fromPart += "'" + strValues[i] + "'" + ",";
                }
                else
                {
                    fromPart += "'" + strValues[i] + "'";
                }
            }
            fromPart += " )";
            string sqlScript = selectPart.Append(fromPart).ToString();
            UFIDA.U8.UAP.Common.SqlHelper.ExecuteDataset(connStr, CommandType.Text, sqlScript);
        }


        public static void SqlHelpUpdate(string wherePart,string keyPart,string tableName,List<string> stringList,
            string connStr, List<string> strValues)
        {
            StringBuilder selectPart = new StringBuilder("UPDATE ");
            selectPart.Append(tableName).Append(" SET ");
            for (int i = 0; i < stringList.Count; ++i)
            {
                if (i != stringList.Count - 1)
                {
                    selectPart.Append(stringList[i]).Append("=").Append("'").Append(
                        strValues[i]).Append("',");
                }
                else
                {
                    selectPart.Append(stringList[i]).Append("=").Append("'").Append(
                        strValues[i]).Append("'");
                }
            }
            selectPart.Append(" WHERE ").Append(wherePart).Append(" = ").Append(keyPart);
            UFIDA.U8.UAP.Common.SqlHelper.ExecuteDataset(connStr, CommandType.Text, selectPart.ToString());
        }

        public static void SqlHelpDelete(string wherePart, string keyPart, string tableName, 
            string connStr)
        {
            string selectPart = "DELETE FROM " + tableName +
                " WHERE " + keyPart + " = '" + wherePart+"'";
            try
            {

                UFIDA.U8.UAP.Common.SqlHelper.ExecuteDataset(connStr, CommandType.Text, selectPart);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}

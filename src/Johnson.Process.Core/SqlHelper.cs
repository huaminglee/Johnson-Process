﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace Johnson.Process.Core
{
    public class SqlHelper
    {
        public static string ConnectString = System.Configuration.ConfigurationSettings.AppSettings["GZJohnsonProcess_ConnectionString"];
        public static string FailPdct_ConnectionString = System.Configuration.ConfigurationSettings.AppSettings["FailPdct_ConnectionString"];

        #region 执行一条SQL语句或存储过程，返回SqlDataReader
        /// <summary>
        /// Author:	LYQ, Create date: 2009.7.16
        /// 执行一条SQL语句或存储过程，返回SqlDataReader
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdText">执行的命令</param>
        /// <param name="cmdType"></param>
        /// <param name="cmdParams">sql语句或存储过程的参数</param>
        /// <returns>SqlDataReader结果集</returns>
        public static SqlDataReader ExecuteReader(SqlConnection conn, string cmdText, CommandType cmdType, SqlParameter[] cmdParams)
        {
            SqlCommand comm = CreateCmd(conn, cmdText, cmdType, cmdParams);
            if (comm == null)
                return null;
            SqlDataReader dataReader = comm.ExecuteReader();
            comm.Parameters.Clear();
            return dataReader;
        }
        #endregion
        #region 执行一条SQL语句或存储过程，返回SqlDataReader
        /// <summary>
        /// Author:	LYQ, Create date: 2009.7.16
        /// 执行一条SQL语句或存储过程，返回SqlDataReader
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdText">执行的命令</param>
        /// <param name="cmdType"></param>
        /// <param name="cmdParams">sql语句或存储过程的参数</param>
        /// <returns>SqlDataReader结果集</returns>
        public static SqlDataReader ExecuteReader(SqlConnection conn, SqlTransaction tran, string cmdText, CommandType cmdType, SqlParameter[] cmdParams)
        {
            SqlCommand comm = CreateCmd(conn, cmdText, cmdType, cmdParams);
            comm.Transaction = tran;
            if (comm == null)
                return null;
            SqlDataReader dataReader = comm.ExecuteReader();
            comm.Parameters.Clear();
            return dataReader;
        }
        #endregion

        /*-------------------------------------------------------------------------------------*/

        #region 执行一条SQL语句或存储过程，返回DataTable
        /// <summary>
        /// Author:	LYQ, Create date: 2009.7.16
        /// 执行一条SQL语句或存储过程，返回DataTable
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdText">执行的命令</param>
        /// <param name="cmdType"></param>
        /// <param name="cmdParams">sql语句或存储过程的参数</param>
        /// <returns>DataTable结果集</returns>
        public static DataTable ExecuteDataTable(SqlConnection conn, string cmdText, CommandType cmdType, SqlParameter[] cmdParams)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            DataTable dt = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmdText, conn);
            dataAdapter.SelectCommand.CommandType = cmdType;
            if (cmdParams != null)
            {
                foreach (SqlParameter param in cmdParams)
                {
                    if (param != null)
                        dataAdapter.SelectCommand.Parameters.Add(param);
                }
            }
            dataAdapter.Fill(dt);
            return dt;
        }
        #endregion
        #region 执行一条SQL语句或存储过程，返回DataTable
        /// <summary>
        /// Author:	LYQ, Create date: 2009.7.16
        /// 执行一条SQL语句或存储过程，返回DataTable
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdText">执行的命令</param>
        /// <param name="cmdType"></param>
        /// <param name="cmdParams">sql语句或存储过程的参数</param>
        /// <returns>DataTable结果集</returns>
        public static DataTable ExecuteDataTable(SqlConnection conn, SqlTransaction tran, string cmdText, CommandType cmdType, SqlParameter[] cmdParams)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            DataTable dt = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmdText, conn);
            dataAdapter.SelectCommand.CommandType = cmdType;
            dataAdapter.SelectCommand.Transaction = tran;
            if (cmdParams != null)
            {
                foreach (SqlParameter param in cmdParams)
                {
                    if (param != null)
                        dataAdapter.SelectCommand.Parameters.Add(param);
                }
            }
            dataAdapter.Fill(dt);
            return dt;
        }
        #endregion

        /*-------------------------------------------------------------------------------------*/

        #region 执行一条SQL语句或存储过程，返回DataSet
        /// <summary>
        /// Author:	LYQ, Create date: 2009.7.16
        /// 执行一条SQL语句或存储过程，返回DataSet
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdText">执行的命令</param>
        /// <param name="cmdType"></param>
        /// <param name="cmdParams">sql语句或存储过程的参数</param>
        /// <returns>DataTable结果集</returns>
        public static DataSet ExecuteDataSet(SqlConnection conn, string cmdText, CommandType cmdType, SqlParameter[] cmdParams)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            DataSet ds = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmdText, conn);
            dataAdapter.SelectCommand.CommandType = cmdType;
            if (cmdParams != null)
            {
                foreach (SqlParameter param in cmdParams)
                {
                    if (param != null)
                        dataAdapter.SelectCommand.Parameters.Add(param);
                }
            }
            dataAdapter.Fill(ds);
            return ds;
        }
        #endregion
        #region 执行一条SQL语句或存储过程，返回DataSet
        /// <summary>
        /// Author:	LYQ, Create date: 2009.7.16
        /// 执行一条SQL语句或存储过程，返回DataSet
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdText">执行的命令</param>
        /// <param name="cmdType"></param>
        /// <param name="cmdParams">sql语句或存储过程的参数</param>
        /// <returns>DataTable结果集</returns>
        public static DataSet ExecuteDataSet(SqlConnection conn, SqlTransaction tran, string cmdText, CommandType cmdType, SqlParameter[] cmdParams)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            DataSet ds = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmdText, conn);
            dataAdapter.SelectCommand.CommandType = cmdType;
            dataAdapter.SelectCommand.Transaction = tran;
            if (cmdParams != null)
            {
                foreach (SqlParameter param in cmdParams)
                {
                    if (param != null)
                        dataAdapter.SelectCommand.Parameters.Add(param);
                }
            }
            dataAdapter.Fill(ds);
            dataAdapter.SelectCommand.Parameters.Clear();
            return ds;
        }
        #endregion


        /*-------------------------------------------------------------------------------------*/
        #region 执行一条SQL语句或存储过程，返回第一行的第一列数据
        /// <summary>
        /// Author:	LYQ, Create date: 2009.7.16
        /// 执行一条SQL语句或存储过程，返回DataSet
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdText">执行的命令</param>
        /// <param name="cmdType"></param>
        /// <param name="cmdParams">sql语句或存储过程的参数</param>
        /// <returns>第一行的第一列数据</returns>
        public static Object ExecuteScalar(SqlConnection conn, string cmdText, CommandType cmdType, SqlParameter[] cmdParams)
        {
            //使用不同Command对象
            SqlCommand comm = CreateCmd(conn, cmdText, cmdType, cmdParams);
            if (comm == null)
                return null;
            Object o = comm.ExecuteScalar();
            return o;
        }
        #endregion
        #region 执行一条SQL语句或存储过程，返回第一行的第一列数据
        /// <summary>
        /// Author:	LYQ, Create date: 2009.7.16
        /// 执行一条SQL语句或存储过程，返回DataSet
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdText">执行的命令</param>
        /// <param name="cmdType"></param>
        /// <param name="cmdParams">sql语句或存储过程的参数</param>
        /// <returns>第一行的第一列数据</returns>
        public static Object ExecuteScalar(SqlConnection conn, SqlTransaction tran, string cmdText, CommandType cmdType, SqlParameter[] cmdParams)
        {
            //使用不同Command对象
            SqlCommand comm = CreateCmd(conn, cmdText, cmdType, cmdParams);
            comm.Transaction = tran;
            if (comm == null)
                return null;
            Object o = comm.ExecuteScalar();
            return o;
        }
        #endregion
        /*---------------------------------------------------------------------------------------*/

        #region 执行一条SQL语句或存储过程，返回影响的行数
        /// <summary>
        /// Author:	LYQ, Create date: 2009.7.16
        /// 执行一条SQL语句或存储过程，返回DataSet
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdText">执行的命令</param>
        /// <param name="cmdType"></param>
        /// <param name="cmdParams">sql语句或存储过程的参数</param>
        /// <returns>影响的行数</returns>
        public static int ExecuteNonQuery(SqlConnection conn, string cmdText, CommandType cmdType, SqlParameter[] cmdParams)
        {
            //使用不同Command对象
            SqlCommand comm = CreateCmd(conn, cmdText, cmdType, cmdParams);
            if (comm == null)
                return 0;
            int result = comm.ExecuteNonQuery();
            return result;
        }
        #endregion
        #region 执行一条SQL语句或存储过程，返回影响的行数
        /// <summary>
        /// Author:	LYQ, Create date: 2009.7.16
        /// 执行一条SQL语句或存储过程，返回DataSet
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdText">执行的命令</param>
        /// <param name="cmdType"></param>
        /// <param name="cmdParams">sql语句或存储过程的参数</param>
        /// <returns>影响的行数</returns>
        public static int ExecuteNonQuery(SqlConnection conn, SqlTransaction tran, string cmdText, CommandType cmdType, SqlParameter[] cmdParams)
        {
            //使用不同Command对象
            SqlCommand comm = CreateCmd(conn, cmdText, cmdType, cmdParams);
            comm.Transaction = tran;
            if (comm == null)
                return 0;
            int result = comm.ExecuteNonQuery();
            return result;
        }
        #endregion
        /*---------------------------------------------------------------------------------------*/
        #region 创建SqlCommand对象
        /// <summary>
        /// Author:	LYQ, Create date: 2009.7.16
        /// 执行一条SQL语句或存储过程，返回DataSet
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdText">执行的Sql语句或存储过程</param>
        /// <param name="cmdType">cmdText类型</param>
        /// <param name="cmdParams">Sql语句或存储过程的参数</param>
        /// <returns>SqlCommand对象</returns>
        private static SqlCommand CreateCmd(SqlConnection conn, string cmdText, CommandType cmdType, SqlParameter[] cmdParams)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            SqlCommand comm = new SqlCommand(cmdText, conn);
            comm.CommandType = cmdType;
            if (cmdParams != null)
            {
                foreach (SqlParameter param in cmdParams)
                {
                    if (param != null)
                        comm.Parameters.Add(param);
                }
            }
            return comm;
        }

        #endregion
    }
}

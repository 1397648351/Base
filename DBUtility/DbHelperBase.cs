/************************************************************************ 
 * 项目名称 ：DBUtility
 * 项目描述 ：
 * 文件名称 ：DbHelperBase.cs
 * 版 本 号 ：v1.0.0.0
 * 说    明 ：
 * 作    者 ：MyPC
 * IDE 环境 ：Visual Studio 2013
 * 创建时间 ：2018/6/8 17:03:57
 * 更新时间 ：2018/6/8 17:03:57
************************************************************************
 * Copyright @ Njbosa 2018. All rights reserved.
************************************************************************/

using System;
using System.Data;
using System.Data.Common;

namespace DBUtility
{
    public abstract class DbHelperBase
    {
        private const string Str_Ex = "ORM异常";
        protected abstract DbConnection DBConnectionObj { get; }
        protected abstract DbCommand DbCommandObj { get; }
        protected abstract DbDataAdapter DbDataAdapterObj { get; }
        protected DbTransaction DbTransObj;

        public string _ConnStr;
        bool _IsTrans = false;

        public DbHelperBase(string connStr)
        {
            _ConnStr = connStr;
        }

        public DbConnection CurrentConnection
        {
            get
            {
                return DBConnectionObj;
            }
        }

        /// <summary>
        /// 打开连接,如果已经打开则什么都不执行了
        /// </summary>
        private void OpenConnection()
        {
            if (DBConnectionObj.State != ConnectionState.Open)
            {
                DBConnectionObj.ConnectionString = _ConnStr;
                DBConnectionObj.Open();
            }
        }

        /// <summary>
        /// 关闭连接,如果没有开始事务或连接打开时才关闭
        /// </summary>
        private void CloseConnect()
        {
            if (!_IsTrans)
            {
                if (DBConnectionObj.State == ConnectionState.Open)
                {
                    DBConnectionObj.Close();
                    DBConnectionObj.Dispose();
                }
            }
        }

        /// <summary>
        /// 给当前DbCommand对象赋值,并且OpenConnection();
        /// </summary>
        private void SetCommandAndOpenConnect(string sqlText, CommandType cmdType = CommandType.Text, params DbParameter[] param)
        {
            //按说赋值Connection,CommandType,是不用多次赋值的
            DbCommandObj.CommandType = cmdType;
            DbCommandObj.Connection = DBConnectionObj;
            DbCommandObj.Parameters.Clear();
            if (param != null)
            {
                DbCommandObj.Parameters.AddRange(param);
            }
            DbCommandObj.CommandText = sqlText;
            OpenConnection();
        }

        /// <summary>
        /// 执行一条指定命令类型(SQL语句或存储过程等)的SQL语句,返回所影响行数
        /// </summary>
        public int ExecNonQuery(string sqlText, CommandType cmdType = CommandType.Text, params DbParameter[] param)
        {
            try
            {
                SetCommandAndOpenConnect(sqlText, cmdType, param);
                return DbCommandObj.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnect();
            }
        }

        /// <summary>
        /// 获得首行首列
        /// </summary>
        public object GetScalar(string sqlText, CommandType cmdType = CommandType.Text, params DbParameter[] param)
        {
            try
            {
                SetCommandAndOpenConnect(sqlText, cmdType, param);
                return DbCommandObj.ExecuteScalar();
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnect();
            }
        }
        /// <summary>
        /// 执行一条SQL语句返回DataSet对象
        /// </summary>
        public DataSet GetDataSet(string sqlText, CommandType cmdType = CommandType.Text, params DbParameter[] param)
        {
            try
            {
                SetCommandAndOpenConnect(sqlText, cmdType, param);
                DbDataAdapterObj.SelectCommand = DbCommandObj;
                DataSet ds = new DataSet();
                DbDataAdapterObj.Fill(ds);
                return ds;
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnect();
            }
        }

        /// <summary>
        /// 获得DataReader对象
        /// </summary>
        public DbDataReader GetDataReader(string sqlText, CommandType cmdType, params DbParameter[] param)
        {
            try
            {
                SetCommandAndOpenConnect(sqlText, cmdType, param);
                CommandBehavior cmdBehavior = CommandBehavior.CloseConnection;
                if (_IsTrans)
                {
                    cmdBehavior = CommandBehavior.Default;
                }
                DbDataReader dbReader = DbCommandObj.ExecuteReader(cmdBehavior);
                return dbReader;
            }
            catch
            {
                throw;
            }
            finally
            {
                //DataReader用dbReader对象来关闭
                //CloseConnect();
            }
        }

        /// <summary>
        /// 开始执行事务
        /// </summary>
        public void TransStart()
        {
            OpenConnection();
            DbTransObj = DBConnectionObj.BeginTransaction();
            DbCommandObj.Transaction = DbTransObj;
            _IsTrans = true;
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        public void TransCommit()
        {
            _IsTrans = false;
            DbTransObj.Commit();
            CloseConnect();
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public void TransRollback()
        {
            _IsTrans = false;
            DbTransObj.Rollback();
            CloseConnect();
        }
    }
}
